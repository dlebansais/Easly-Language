using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;

namespace BaseNodeHelper
{
    public static class NodeTreeDiagnostic
    {
        public static bool IsValid(INode root, bool assertValid = true)
        {
            List<INode> NodeList = new List<INode>();
            List<Guid> GuidList = new List<Guid>();
            return IsValid(NodeList, GuidList, root, assertValid);
        }

        private static bool FailIsValidCheck(bool assertValid)
        {
            if (assertValid)
                Debug.Assert(false);

            return false;
        }

        private static bool IsValid(List<INode> nodeList, List<Guid> guidList, INode root, bool assertValid)
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
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(root, PropertyName, out INode ChildNode);
                    if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                        return FailIsValidCheck(assertValid);
                }

                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(root, PropertyName, out bool IsAssigned, out INode ChildNode);

                    if (ChildNode != null)
                    {
                        if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                            return FailIsValidCheck(assertValid);
                    }
                }

                else if (NodeTreeHelperList.IsNodeListProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(root, PropertyName, out IReadOnlyList<INode> ChildNodeList);
                    if (ChildNodeList == null)
                        return FailIsValidCheck(assertValid);

                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                    {
                        INode ChildNode = ChildNodeList[Index];
                        if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                            return FailIsValidCheck(assertValid);
                    }

                    if (ChildNodeList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(root, PropertyName))
                        return FailIsValidCheck(assertValid);
                }

                else if (NodeTreeHelperBlockList.IsBlockListProperty(root, PropertyName, out ChildInterfaceType, out ChildNodeType))
                {
                    IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(root, PropertyName);
                    if (!IsValidBlockList(nodeList, guidList, BlockList, assertValid))
                        return FailIsValidCheck(assertValid);

                    for (int BlockIndex = 0; BlockIndex < BlockList.NodeBlockList.Count; BlockIndex++)
                    {
                        IBlock Block = BlockList.NodeBlockList[BlockIndex] as IBlock;
                        if (!IsValidBlock(nodeList, guidList, Block, assertValid))
                            return FailIsValidCheck(assertValid);

                        for (int Index = 0; Index < Block.NodeList.Count; Index++)
                        {
                            INode ChildNode = Block.NodeList[Index] as INode;
                            if (!IsValid(nodeList, guidList, ChildNode, assertValid))
                                return FailIsValidCheck(assertValid);
                        }
                    }

                    if (BlockList.NodeBlockList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(root, PropertyName))
                        return FailIsValidCheck(assertValid);
                }

                else if (NodeTreeHelper.IsBooleanProperty(root, PropertyName))
                { }

                else if (NodeTreeHelper.IsEnumProperty(root, PropertyName))
                {
                    NodeTreeHelper.GetEnumRange(root.GetType(), PropertyName, out int Min, out int Max);
                    PropertyInfo EnumPropertyInfo = NodeTreeHelper.GetPropertyOf(root.GetType(), PropertyName);
                    int Value = (int)EnumPropertyInfo.GetValue(root);
                    if (Value < Min || Value > Max)
                        return FailIsValidCheck(assertValid);
                }

                else if (NodeTreeHelper.IsStringProperty(root, PropertyName))
                {
                    if (NodeTreeHelper.GetString(root, PropertyName) == null)
                        return FailIsValidCheck(assertValid);
                }

                else if (NodeTreeHelper.IsGuidProperty(root, PropertyName))
                {
                    Guid PropertyGuid = NodeTreeHelper.GetGuid(root, PropertyName);
                    if (PropertyGuid == Guid.Empty)
                        return FailIsValidCheck(assertValid);

                    if (guidList.Contains(PropertyGuid))
                        return FailIsValidCheck(assertValid);

                    guidList.Add(PropertyGuid);
                }

                else if (NodeTreeHelper.IsDocumentProperty(root, PropertyName))
                { }

                else
                    return FailIsValidCheck(assertValid);
            }

            return true;
        }

        private static bool IsValidBlockList(List<INode> nodeList, List<Guid> guidList, IBlockList blockList, bool assertValid)
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

        private static bool IsValidBlock(List<INode> nodeList, List<Guid> guidList, IBlock block, bool assertValid)
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
