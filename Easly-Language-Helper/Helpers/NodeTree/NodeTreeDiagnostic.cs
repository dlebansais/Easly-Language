#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

    public static class NodeTreeDiagnostic
    {
        public static bool IsValid(Node root, bool assertValid = true)
        {
            List<Node> NodeList = new List<Node>();
            List<Guid> GuidList = new List<Guid>();
            return IsValid(NodeList, GuidList, root, assertValid);
        }

        private static bool FailIsValidCheck(bool assertValid)
        {
            if (assertValid)
                Debug.Assert(false);

            return false;
        }

        private static bool IsValid(List<Node> nodeList, List<Guid> guidList, Node root, bool assertValid)
        {
            Debug.Assert(nodeList != null);
            Debug.Assert(guidList != null);

            if (root == null)
                return FailIsValidCheck(assertValid);

            if (root.Documentation == null)
                return FailIsValidCheck(assertValid);

            if (root.Documentation.Comment == null)
                return FailIsValidCheck(assertValid);

            if (root.Documentation.Uuid == Guid.Empty)
                return FailIsValidCheck(assertValid);

            if (nodeList.Contains(root))
                return FailIsValidCheck(assertValid);

            if (guidList.Contains(root.Documentation.Uuid))
                return FailIsValidCheck(assertValid);

            nodeList.Add(root);
            guidList.Add(root.Documentation.Uuid);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(root);

            foreach (string PropertyName in PropertyNames)
                if (!IsValidProperty(nodeList, guidList, root, assertValid, PropertyName))
                    return false;

            return true;
        }

        private static bool IsValidProperty(List<Node> nodeList, List<Guid> guidList, Node root, bool assertValid, string propertyName)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(root, propertyName, out _))
                return IsValidChildNode(nodeList, guidList, root, assertValid, propertyName);
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, propertyName, out _))
                return IsValidOptionalChildNode(nodeList, guidList, root, assertValid, propertyName);
            else if (NodeTreeHelperList.IsNodeListProperty(root, propertyName, out _))
                return IsValidNodeList(nodeList, guidList, root, assertValid, propertyName);
            else if (NodeTreeHelperBlockList.IsBlockListProperty(root, propertyName, /*out ChildInterfaceType,*/ out _))
                return IsValidBlockList(nodeList, guidList, root, assertValid, propertyName);
            else if (NodeTreeHelper.IsBooleanProperty(root, propertyName))
                return true;
            else if (NodeTreeHelper.IsEnumProperty(root, propertyName))
                return IsValidEnumProperty(root, assertValid, propertyName);
            else if (NodeTreeHelper.IsStringProperty(root, propertyName))
                return IsValidStringProperty(root, assertValid, propertyName);
            else if (NodeTreeHelper.IsGuidProperty(root, propertyName))
                return IsValidGuidProperty(guidList, root, assertValid, propertyName);
            else if (NodeTreeHelper.IsDocumentProperty(root, propertyName))
                return true;
            else
                return FailIsValidCheck(assertValid);
        }

        private static bool IsValidChildNode(List<Node> nodeList, List<Guid> guidList, Node root, bool assertValid, string propertyName)
        {
            NodeTreeHelperChild.GetChildNode(root, propertyName, out Node ChildNode);
            if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                return FailIsValidCheck(assertValid);

            return true;
        }

        private static bool IsValidOptionalChildNode(List<Node> nodeList, List<Guid> guidList, Node root, bool assertValid, string propertyName)
        {
            NodeTreeHelperOptional.GetChildNode(root, propertyName, out _, out Node ChildNode);

            if (ChildNode != null)
            {
                if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                    return FailIsValidCheck(assertValid);
            }

            return true;
        }

        private static bool IsValidNodeList(List<Node> nodeList, List<Guid> guidList, Node root, bool assertValid, string propertyName)
        {
            NodeTreeHelperList.GetChildNodeList(root, propertyName, out IReadOnlyList<Node> ChildNodeList);
            if (ChildNodeList == null)
                return FailIsValidCheck(assertValid);

            for (int Index = 0; Index < ChildNodeList.Count; Index++)
            {
                Node ChildNode = ChildNodeList[Index];
                if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                    return FailIsValidCheck(assertValid);
            }

            if (ChildNodeList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(root, propertyName))
                return FailIsValidCheck(assertValid);

            return true;
        }

        private static bool IsValidBlockList(List<Node> nodeList, List<Guid> guidList, Node root, bool assertValid, string propertyName)
        {
            IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(root, propertyName);
            if (!IsValidBlockList(nodeList, guidList, BlockList, assertValid))
                return FailIsValidCheck(assertValid);

            for (int BlockIndex = 0; BlockIndex < BlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock Block = BlockList.NodeBlockList[BlockIndex] as IBlock;
                if (!IsValidBlock(nodeList, guidList, Block, assertValid))
                    return FailIsValidCheck(assertValid);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node ChildNode = Block.NodeList[Index] as Node;
                    if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                        return FailIsValidCheck(assertValid);
                }
            }

            if (BlockList.NodeBlockList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(root, propertyName))
                return FailIsValidCheck(assertValid);

            return true;
        }

        private static bool IsValidEnumProperty(Node root, bool assertValid, string propertyName)
        {
            NodeTreeHelper.GetEnumRange(root.GetType(), propertyName, out int Min, out int Max);
            PropertyInfo EnumPropertyInfo = NodeTreeHelper.GetPropertyOf(root.GetType(), propertyName);
            int Value = (int)EnumPropertyInfo.GetValue(root);
            if (Value < Min || Value > Max)
                return FailIsValidCheck(assertValid);

            return true;
        }

        private static bool IsValidStringProperty(Node root, bool assertValid, string propertyName)
        {
            if (NodeTreeHelper.GetString(root, propertyName) == null)
                return FailIsValidCheck(assertValid);

            return true;
        }

        private static bool IsValidGuidProperty(List<Guid> guidList, Node root, bool assertValid, string propertyName)
        {
            Guid PropertyGuid = NodeTreeHelper.GetGuid(root, propertyName);
            if (PropertyGuid == Guid.Empty)
                return FailIsValidCheck(assertValid);

            if (guidList.Contains(PropertyGuid))
                return FailIsValidCheck(assertValid);

            guidList.Add(PropertyGuid);

            return true;
        }

        private static bool IsValidBlockList(List<Node> nodeList, List<Guid> guidList, IBlockList blockList, bool assertValid)
        {
            Debug.Assert(nodeList != null);
            Debug.Assert(guidList != null);

            if (blockList == null)
                return FailIsValidCheck(assertValid);

            if (blockList.Documentation == null)
                return FailIsValidCheck(assertValid);

            if (blockList.Documentation.Comment == null)
                return FailIsValidCheck(assertValid);

            if (blockList.Documentation.Uuid == Guid.Empty)
                return FailIsValidCheck(assertValid);

            if (guidList.Contains(blockList.Documentation.Uuid))
                return FailIsValidCheck(assertValid);

            guidList.Add(blockList.Documentation.Uuid);

            if (blockList.NodeBlockList == null)
                return FailIsValidCheck(assertValid);

            return true;
        }

        private static bool IsValidBlock(List<Node> nodeList, List<Guid> guidList, IBlock block, bool assertValid)
        {
            Debug.Assert(nodeList != null);
            Debug.Assert(guidList != null);

            if (block == null)
                return FailIsValidCheck(assertValid);

            if (block.Documentation == null)
                return FailIsValidCheck(assertValid);

            if (block.Documentation.Comment == null)
                return FailIsValidCheck(assertValid);

            if (block.Documentation.Uuid == Guid.Empty)
                return FailIsValidCheck(assertValid);

            if (guidList.Contains(block.Documentation.Uuid))
                return FailIsValidCheck(assertValid);

            guidList.Add(block.Documentation.Uuid);

            if (block.NodeList == null)
                return FailIsValidCheck(assertValid);

            if (block.NodeList.Count == 0)
                return FailIsValidCheck(assertValid);

            if (block.Replication != ReplicationStatus.Normal && block.Replication != ReplicationStatus.Replicated)
                return FailIsValidCheck(assertValid);

            if (!IsValid(nodeList, guidList, block.ReplicationPattern, assertValid))
                return FailIsValidCheck(assertValid);

            if (!IsValid(nodeList, guidList, block.SourceIdentifier, assertValid))
                return FailIsValidCheck(assertValid);

            return true;
        }
    }
}
