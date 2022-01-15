namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to check a tree of nodes.
    /// </summary>
    public static class NodeTreeDiagnostic
    {
        /// <summary>
        /// Checks whether a node is a valid root.
        /// </summary>
        /// <param name="root">The root node.</param>
        /// <param name="throwOnInvalid">If true, the will throw an exception if the node is invalid.</param>
        /// <returns>True if the node is a valid root node; otherwise, false.</returns>
        public static bool IsValid(Node root, bool throwOnInvalid = true)
        {
            Contract.RequireNotNull(root, out Node Root);

            List<Guid> GuidList = new List<Guid>();
            return IsValid(GuidList, Root, Root, throwOnInvalid);
        }

        private static bool IsValid(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid)
        {
            // If a node is duplicated, the Guid is also duplicated, and this test catches it.
            if (!IsValidGuid(guidList, root.Documentation.Uuid, originalRoot, root, throwOnInvalid))
                return false;

            guidList.Add(root.Documentation.Uuid);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(root);

            foreach (string PropertyName in PropertyNames)
                if (!IsValidProperty(guidList, originalRoot, root, throwOnInvalid, PropertyName))
                    return false;

            return true;
        }

        private static bool IsValidGuid(List<Guid> guidList, Guid guid, Node originalRoot, Node root, bool throwOnInvalid)
        {
            if (guid == Guid.Empty)
                return FailIsValidCheck(throwOnInvalid, "Empty Guid not allowed", originalRoot, root);

            if (guidList.Contains(guid))
                return FailIsValidCheck(throwOnInvalid, $"Duplicate Guid '{guid}'", originalRoot, root);

            return true;
        }

        private static bool IsValidProperty(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(root, propertyName, out _))
                return IsValidChildNode(guidList, originalRoot, root, throwOnInvalid, propertyName);
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, propertyName, out _))
                return IsValidOptionalChildNode(guidList, originalRoot, root, throwOnInvalid, propertyName);
            else if (NodeTreeHelperList.IsNodeListProperty(root, propertyName, out _))
                return IsValidNodeList(guidList, originalRoot, root, throwOnInvalid, propertyName);
            else if (NodeTreeHelperBlockList.IsBlockListProperty(root, propertyName, out _))
                return IsValidBlockList(guidList, originalRoot, root, throwOnInvalid, propertyName);
            else if (NodeTreeHelper.IsBooleanProperty(root, propertyName) || NodeTreeHelper.IsStringProperty(root, propertyName) || NodeTreeHelper.IsDocumentProperty(root, propertyName))
                return true;
            else if (NodeTreeHelper.IsEnumProperty(root, propertyName))
                return IsValidEnumProperty(originalRoot, root, throwOnInvalid, propertyName);
            else
            {
                Debug.Assert(NodeTreeHelper.IsGuidProperty(root, propertyName));
                return IsValidGuidProperty(guidList, originalRoot, root, throwOnInvalid, propertyName);
            }
        }

        private static bool IsValidChildNode(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            NodeTreeHelperChild.GetChildNode(root, propertyName, out Node ChildNode);
            if (!IsValid(guidList, originalRoot, ChildNode, throwOnInvalid))
                return false;

            return true;
        }

        private static bool IsValidOptionalChildNode(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            NodeTreeHelperOptional.GetChildNode(root, propertyName, out _, out bool HasItem, out Node ChildNode);

            if (HasItem)
            {
                if (!IsValid(guidList, originalRoot, ChildNode, throwOnInvalid))
                    return false;
            }

            return true;
        }

        private static bool IsValidNodeList(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            NodeTreeHelperList.GetChildNodeList(root, propertyName, out IReadOnlyList<Node> ChildNodeList);

            for (int Index = 0; Index < ChildNodeList.Count; Index++)
            {
                Node ChildNode = ChildNodeList[Index];
                if (!IsValid(guidList, originalRoot, ChildNode, throwOnInvalid))
                    return false;
            }

            if (ChildNodeList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(root, propertyName))
                return FailIsValidCheck(throwOnInvalid, $"Collection '{propertyName}' must not be empty", originalRoot, root);

            return true;
        }

        private static bool IsValidBlockList(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(root, propertyName);
            if (!IsValidBlockList(guidList, BlockList, originalRoot, root, throwOnInvalid))
                return false;

            for (int BlockIndex = 0; BlockIndex < BlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock Block = SafeType.ItemAt<IBlock>(BlockList.NodeBlockList, BlockIndex);

                if (!IsValidBlock(guidList, Block, originalRoot, root, throwOnInvalid))
                    return false;

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node ChildNode = SafeType.ItemAt<Node>(Block.NodeList, Index);

                    if (!IsValid(guidList, originalRoot, ChildNode, throwOnInvalid))
                        return false;
                }
            }

            if (BlockList.NodeBlockList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(root, propertyName))
                return FailIsValidCheck(throwOnInvalid, $"Collection '{propertyName}' must not be empty", originalRoot, root);

            return true;
        }

        private static bool IsValidEnumProperty(Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            Type RootType = root.GetType();
            NodeTreeHelper.GetEnumRange(RootType, propertyName, out int Min, out int Max);
            PropertyInfo EnumPropertyInfo = SafeType.GetProperty(RootType, propertyName);
            int Value = (int)Contract.NullSupressed(EnumPropertyInfo.GetValue(root));

            if (Value < Min || Value > Max)
                return FailIsValidCheck(throwOnInvalid, $"Value of property '{propertyName}' is out of range", originalRoot, root);

            return true;
        }

        private static bool IsValidGuidProperty(List<Guid> guidList, Node originalRoot, Node root, bool throwOnInvalid, string propertyName)
        {
            Guid PropertyGuid = NodeTreeHelper.GetGuid(root, propertyName);

            if (!IsValidGuid(guidList, PropertyGuid, originalRoot, root, throwOnInvalid))
                return false;

            guidList.Add(PropertyGuid);

            return true;
        }

        private static bool IsValidBlockList(List<Guid> guidList, IBlockList blockList, Node originalRoot, Node root, bool throwOnInvalid)
        {
            if (!IsValidGuid(guidList, blockList.Documentation.Uuid, originalRoot, root, throwOnInvalid))
                return false;

            guidList.Add(blockList.Documentation.Uuid);

            return true;
        }

        private static bool IsValidBlock(List<Guid> guidList, IBlock block, Node originalRoot, Node root, bool throwOnInvalid)
        {
            if (!IsValidGuid(guidList, block.Documentation.Uuid, originalRoot, root, throwOnInvalid))
                return false;

            guidList.Add(block.Documentation.Uuid);

            if (block.NodeList.Count == 0)
                return FailIsValidCheck(throwOnInvalid, "Node list must not be empty", originalRoot, root);

            Debug.Assert(block.Replication == ReplicationStatus.Normal || block.Replication == ReplicationStatus.Replicated);

            if (!IsValid(guidList, originalRoot, block.ReplicationPattern, throwOnInvalid))
                return false;

            if (!IsValid(guidList, originalRoot, block.SourceIdentifier, throwOnInvalid))
                return false;

            return true;
        }

        private static bool FailIsValidCheck(bool throwOnInvalid, string message, Node rootNode, Node invalidNode)
        {
            if (throwOnInvalid)
                throw new InvalidNodeException(message, rootNode, invalidNode);

            return false;
        }
    }
}
