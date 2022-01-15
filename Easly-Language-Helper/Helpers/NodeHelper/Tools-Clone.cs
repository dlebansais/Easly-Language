namespace BaseNodeHelper;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;

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
        IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(root);

        // Careful, the resulting "empty" node can contain items for lists that are not allowed to be empty.
        Node ClonedRoot = CreateEmptyNode(root.GetType());

        foreach (string PropertyName in PropertyNames)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(root, PropertyName, out _))
            {
                NodeTreeHelperChild.GetChildNode(root, PropertyName, out Node ChildNode);

                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                NodeTreeHelperChild.SetChildNode(ClonedRoot, PropertyName, ClonedChildNode);
            }
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, PropertyName, out _))
            {
                NodeTreeHelperOptional.GetChildNode(root, PropertyName, out bool IsAssigned, out bool HasItem, out Node ChildNode);

                if (HasItem)
                {
                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                    NodeTreeHelperOptional.SetOptionalChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                    if (!IsAssigned)
                        NodeTreeHelperOptional.UnassignChildNode(ClonedRoot, PropertyName);
                }
            }
            else if (NodeTreeHelperList.IsNodeListProperty(root, PropertyName, out _))
            {
                NodeTreeHelperList.GetChildNodeList(root, PropertyName, out IReadOnlyList<Node> ChildNodeList);
                IList<Node> ClonedChildNodeList = DeepCloneNodeList(ChildNodeList, cloneCommentGuid);

                NodeTreeHelperList.ClearChildNodeList(ClonedRoot, PropertyName);
                for (int Index = 0; Index < ChildNodeList.Count; Index++)
                    NodeTreeHelperList.InsertIntoList(ClonedRoot, PropertyName, Index, ClonedChildNodeList[Index]);
            }
            else if (NodeTreeHelperBlockList.IsBlockListProperty(root, PropertyName, out _))
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
            else
            {
                Debug.Assert(NodeTreeHelper.IsDocumentProperty(root, PropertyName));
                NodeTreeHelper.CopyDocumentation(root, ClonedRoot, cloneCommentGuid);
            }
        }

        return ClonedRoot;
    }

    private static IList<Node> DeepCloneNodeList(IEnumerable<Node> rootList, bool cloneCommentGuid)
    {
        List<Node> Result = new();

        foreach (Node ChildNode in rootList)
        {
            Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
            Result.Add(ClonedChildNode);
        }

        return Result;
    }

    private static IBlockList DeepCloneBlockList(IBlockList rootBlockList, bool cloneCommentGuid)
    {
        Type BlockListType = rootBlockList.GetType();
        Type[] GenericArguments = BlockListType.GetGenericArguments();
        BlockListType = typeof(BlockList<>).MakeGenericType(GenericArguments);

        Assembly BlockListAssembly = BlockListType.Assembly;
        string BlockListFullName = SafeType.FullName(BlockListType);

        IBlockList ClonedBlockList = SafeType.CreateInstance<IBlockList>(BlockListAssembly, BlockListFullName);

        Type NodeListType = rootBlockList.NodeBlockList.GetType();

        Assembly NodeListAssembly = NodeListType.Assembly;
        string NodeListFullName = SafeType.FullName(NodeListType);

        IList ClonedNodeBlockList = SafeType.CreateInstanceFromDefaultConstructor<IList>(NodeListAssembly, NodeListFullName);

        PropertyInfo NodeBlockListProperty = SafeType.GetProperty(BlockListType, nameof(IBlockList.NodeBlockList));

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
