#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeHelper
    {
        public static Node DeepCloneNode(Node root, bool cloneCommentGuid)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(root);

            // Careful, the resulting "empty" node can contain items for lists that are not allowed to be empty.
            Node ClonedRoot = CreateEmptyNode(root.GetType());

            foreach (string PropertyName in PropertyNames)
            {
                Type /*ChildInterfaceType,*/ ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(root, PropertyName, out Node ChildNode);

                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                    NodeTreeHelperChild.SetChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(root, PropertyName, out bool IsAssigned, out Node ChildNode);

                    if (ChildNode != null)
                    {
                        Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                        NodeTreeHelperOptional.SetOptionalChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                        if (!IsAssigned)
                            NodeTreeHelperOptional.UnassignChildNode(ClonedRoot, PropertyName);
                    }
                }
                else if (NodeTreeHelperList.IsNodeListProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(root, PropertyName, out IReadOnlyList<Node> ChildNodeList);
                    IList<Node> ClonedChildNodeList = DeepCloneNodeList(ChildNodeList, cloneCommentGuid);

                    NodeTreeHelperList.ClearChildNodeList(ClonedRoot, PropertyName);
                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                        NodeTreeHelperList.InsertIntoList(ClonedRoot, PropertyName, Index, ClonedChildNodeList[Index]);
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(root, PropertyName, /*out ChildInterfaceType,*/ out ChildNodeType))
                {
                    IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(root, PropertyName);
                    IBlockList ClonedBlockList = DeepCloneBlockList(BlockList, cloneCommentGuid);
                    NodeTreeHelperBlockList.SetBlockList(ClonedRoot, PropertyName, ClonedBlockList);
                }
                else if (NodeTreeHelper.IsBooleanProperty(root, PropertyName))
                    NodeTreeHelper.CopyBooleanProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsEnumProperty(root, PropertyName))
                    NodeTreeHelper.CopyEnumProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsStringProperty(root, PropertyName))
                    NodeTreeHelper.CopyStringProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsGuidProperty(root, PropertyName))
                    NodeTreeHelper.CopyGuidProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsDocumentProperty(root, PropertyName))
                    NodeTreeHelper.CopyDocumentation(root, ClonedRoot, cloneCommentGuid);
                else
                    throw new ArgumentOutOfRangeException($"{nameof(PropertyName)}: {PropertyName}");
            }

            return ClonedRoot;
        }

        public static IList<Node> DeepCloneNodeList(IEnumerable<Node> rootList, bool cloneCommentGuid)
        {
            if (rootList == null) throw new ArgumentNullException(nameof(rootList));

            List<Node> Result = new List<Node>();

            foreach (Node ChildNode in rootList)
            {
                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                Result.Add(ClonedChildNode);
            }

            return Result;
        }

        public static IList<IBlock> DeepCloneBlockList(IEnumerable<IBlock> rootBlockList, bool cloneCommentGuid)
        {
            if (rootBlockList == null) throw new ArgumentNullException(nameof(rootBlockList));

            IList<IBlock> ClonedNodeBlockList = new List<IBlock>();

            foreach (IBlock Block in rootBlockList)
            {
                Type BlockType = Block.GetType();
                Type[] GenericArguments = BlockType.GetGenericArguments();
                BlockType = typeof(IBlock<>).MakeGenericType(GenericArguments);

                Pattern ClonedPattern = (Pattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
                Identifier ClonedSource = (Identifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
                IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockType, Block.Replication, ClonedPattern, ClonedSource);
                NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node? ChildNode = Block.NodeList[Index] as Node;
                    Debug.Assert(ChildNode != null);

                    if (ChildNode == null)
                        return null!;

                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                    NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
                }

                ClonedNodeBlockList.Add(ClonedBlock);
            }

            return ClonedNodeBlockList;
        }

        public static IBlockList DeepCloneBlockList(IBlockList rootBlockList, bool cloneCommentGuid)
        {
            if (rootBlockList == null) throw new ArgumentNullException(nameof(rootBlockList));

            Type BlockListType = rootBlockList.GetType();
            Type[] GenericArguments = BlockListType.GetGenericArguments();
            BlockListType = typeof(BlockList<>).MakeGenericType(GenericArguments);

            Assembly BlockListAssembly = BlockListType.Assembly;
            Debug.Assert(BlockListAssembly != null);

            if (BlockListAssembly == null)
                return null!;

            string BlockListFullName = SafeType.FullName(BlockListType);

            IBlockList? ClonedBlockList = BlockListAssembly.CreateInstance(BlockListFullName) as IBlockList;
            Debug.Assert(ClonedBlockList != null);

            if (ClonedBlockList == null)
                return null!;

            Type NodeListType = rootBlockList.NodeBlockList.GetType();

            Assembly NodeListAssembly = NodeListType.Assembly;
            Debug.Assert(NodeListAssembly != null);

            if (NodeListAssembly == null)
                return null!;

            string NodeListFullName = SafeType.FullName(NodeListType);

            IList? ClonedNodeBlockList = NodeListAssembly.CreateInstance(NodeListFullName) as IList;
            Debug.Assert(ClonedNodeBlockList != null);

            if (ClonedNodeBlockList == null)
                return null!;

            PropertyInfo? NodeBlockListProperty = BlockListType.GetProperty(nameof(IBlockList.NodeBlockList));
            Debug.Assert(NodeBlockListProperty != null);

            if (NodeBlockListProperty == null)
                return null!;

            NodeBlockListProperty.SetValue(ClonedBlockList, ClonedNodeBlockList);
            NodeTreeHelper.CopyDocumentation(rootBlockList, ClonedBlockList, cloneCommentGuid);

            for (int BlockIndex = 0; BlockIndex < rootBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock? Block = rootBlockList.NodeBlockList[BlockIndex] as IBlock;
                Debug.Assert(Block != null);

                if (Block == null)
                    return null!;

                Pattern ClonedPattern = (Pattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
                Identifier ClonedSource = (Identifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
                IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockListType, Block.Replication, ClonedPattern, ClonedSource);
                ClonedNodeBlockList.Add(ClonedBlock);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node? ChildNode = Block.NodeList[Index] as Node;
                    Debug.Assert(ChildNode != null);

                    if (ChildNode == null)
                        return null!;

                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                    NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
                }

                NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);
            }

            return ClonedBlockList;
        }
    }
}
