namespace BaseNodeHelper;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;
using NotNullReflection;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates a new instance of a <see cref="Node"/> making a copy of all objects.
    /// </summary>
    /// <param name="root">The root node to copy.</param>
    /// <param name="cloneCommentGuid">The Uuid of the cloned object documentation.</param>
    /// <returns>The created instance.</returns>
    public static Node DeepCloneNode(Node root, bool cloneCommentGuid)
    {
        Contract.RequireNotNull(root, out Node Root);

        IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(Root);

        // Careful, the resulting "empty" node can contain items for lists that are not allowed to be empty.
        Node ClonedRoot = CreateEmptyNode(Type.FromGetType(Root));

        foreach (string PropertyName in PropertyNames)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(Root, PropertyName, out _))
            {
                NodeTreeHelperChild.GetChildNode(Root, PropertyName, out Node ChildNode);

                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                NodeTreeHelperChild.SetChildNode(ClonedRoot, PropertyName, ClonedChildNode);
            }
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(Root, PropertyName, out _))
            {
                NodeTreeHelperOptional.GetChildNode(Root, PropertyName, out bool IsAssigned, out Node ChildNode);

                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                NodeTreeHelperOptional.SetOptionalChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                if (!IsAssigned)
                    NodeTreeHelperOptional.UnassignChildNode(ClonedRoot, PropertyName);
            }
            else if (NodeTreeHelperList.IsNodeListProperty(Root, PropertyName, out _))
            {
                NodeTreeHelperList.GetChildNodeList(Root, PropertyName, out IReadOnlyList<Node> ChildNodeList);
                IList<Node> ClonedChildNodeList = DeepCloneNodeListInternal(ChildNodeList, cloneCommentGuid);

                NodeTreeHelperList.ClearChildNodeList(ClonedRoot, PropertyName);
                for (int Index = 0; Index < ChildNodeList.Count; Index++)
                    NodeTreeHelperList.InsertIntoList(ClonedRoot, PropertyName, Index, ClonedChildNodeList[Index]);
            }
            else if (NodeTreeHelperBlockList.IsBlockListProperty(Root, PropertyName, out _))
            {
                IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(Root, PropertyName);
                IBlockList ClonedBlockList = DeepCloneBlockListInternal(BlockList, cloneCommentGuid);
                NodeTreeHelperBlockList.SetBlockList(ClonedRoot, PropertyName, ClonedBlockList);
            }
            else if (NodeTreeHelper.IsBooleanProperty(Root, PropertyName))
                NodeTreeHelper.CopyBooleanProperty(Root, ClonedRoot, PropertyName);
            else if (NodeTreeHelper.IsEnumProperty(Root, PropertyName))
                NodeTreeHelper.CopyEnumProperty(Root, ClonedRoot, PropertyName);
            else if (NodeTreeHelper.IsStringProperty(Root, PropertyName))
                NodeTreeHelper.CopyStringProperty(Root, ClonedRoot, PropertyName);
            else if (NodeTreeHelper.IsGuidProperty(Root, PropertyName))
                NodeTreeHelper.CopyGuidProperty(Root, ClonedRoot, PropertyName);
            else
            {
                Debug.Assert(NodeTreeHelper.IsDocumentProperty(Root, PropertyName));
                NodeTreeHelper.CopyDocumentation(Root, ClonedRoot, cloneCommentGuid);
            }
        }

        return ClonedRoot;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="List{Node}"/> making a copy of all objects.
    /// </summary>
    /// <param name="rootList">The list of root nodes to copy.</param>
    /// <param name="cloneCommentGuid">The Uuid of the cloned object documentation.</param>
    /// <returns>The created instance.</returns>
    public static IList<Node> DeepCloneNodeList(IEnumerable<Node> rootList, bool cloneCommentGuid)
    {
        Contract.RequireNotNull(rootList, out IEnumerable<Node> RootList);

        return DeepCloneNodeListInternal(RootList, cloneCommentGuid);
    }

    private static IList<Node> DeepCloneNodeListInternal(IEnumerable<Node> rootList, bool cloneCommentGuid)
    {
        List<Node> Result = new();

        foreach (Node ChildNode in rootList)
        {
            Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
            Result.Add(ClonedChildNode);
        }

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a list of <see cref="Block{Node}"/> making a copy of all objects.
    /// </summary>
    /// <param name="rootBlockList">The list of root blocks to copy.</param>
    /// <param name="cloneCommentGuid">The Uuid of the cloned object documentation.</param>
    /// <returns>The created instance.</returns>
    public static IList<IBlock> DeepCloneBlockList(IList<IBlock> rootBlockList, bool cloneCommentGuid)
    {
        Contract.RequireNotNull(rootBlockList, out IList<IBlock> RootBlockList);

        Type BlockListType = Type.FromGetType(RootBlockList);
        List<IBlock> ClonedNodeBlockList = new();

        for (int BlockIndex = 0; BlockIndex < RootBlockList.Count; BlockIndex++)
        {
            IBlock Block = SafeType.ItemAt<IBlock>(RootBlockList, BlockIndex);
            Type ItemType = Type.FromGetType(Block);

            Pattern ClonedPattern = (Pattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
            Identifier ClonedSource = (Identifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
            IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(ItemType, Block.Replication, ClonedPattern, ClonedSource);
            ClonedNodeBlockList.Add(ClonedBlock);

            for (int Index = 0; Index < Block.NodeList.Count; Index++)
            {
                Node ChildNode = SafeType.ItemAt<Node>(Block.NodeList, Index);
                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
            }

            NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);
        }

        return ClonedNodeBlockList;
    }

    private static IBlockList DeepCloneBlockListInternal(IBlockList rootBlockList, bool cloneCommentGuid)
    {
        Type BlockListType = Type.FromGetType(rootBlockList);
        Type[] GenericArguments = BlockListType.GetGenericArguments();
        BlockListType = Type.FromTypeof<BlockList<Node>>().GetGenericTypeDefinition().MakeGenericType(GenericArguments);

        IBlockList ClonedBlockList = CreateInstance<IBlockList>(BlockListType);

        Type NodeListType = Type.FromGetType(rootBlockList.NodeBlockList);

        IList ClonedNodeBlockList = CreateInstanceFromDefaultConstructor<IList>(NodeListType);

        PropertyInfo NodeBlockListProperty = BlockListType.GetProperty(nameof(IBlockList.NodeBlockList));

        NodeBlockListProperty.SetValue(ClonedBlockList, ClonedNodeBlockList);
        NodeTreeHelper.CopyDocumentation(rootBlockList, ClonedBlockList, cloneCommentGuid);

        for (int BlockIndex = 0; BlockIndex < rootBlockList.NodeBlockList.Count; BlockIndex++)
        {
            IBlock Block = SafeType.ItemAt<IBlock>(rootBlockList.NodeBlockList, BlockIndex);

            Pattern ClonedPattern = (Pattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
            Identifier ClonedSource = (Identifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
            IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockListType, Block.Replication, ClonedPattern, ClonedSource);
            ClonedNodeBlockList.Add(ClonedBlock);

            for (int Index = 0; Index < Block.NodeList.Count; Index++)
            {
                Node ChildNode = SafeType.ItemAt<Node>(Block.NodeList, Index);
                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
            }

            NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);
        }

        return ClonedBlockList;
    }
}
