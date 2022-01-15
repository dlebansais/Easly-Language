namespace BaseNodeHelper;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate block lists of nodes.
/// </summary>
public static partial class NodeTreeHelperBlockList
{
    /// <summary>
    /// Checks whether a property of a node is a block list.
    /// </summary>
    /// <param name="node">The node with the property to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the block list node type upon return.</param>
    /// <returns>True if the property of the node is a block list; otherwise, false.</returns>
    public static bool IsBlockListProperty(Node node, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Type NodeType = Node.GetType();

        return IsBlockListPropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Checks whether a property of a node type is a block list.
    /// </summary>
    /// <param name="nodeType">The node type with the property to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the block list node type upon return.</param>
    /// <returns>True if the property of the node is a block list; otherwise, false.</returns>
    public static bool IsBlockListProperty(Type nodeType, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return IsBlockListPropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Gets the type of nodes in a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="childNodeType">The node type upon return.</param>
    public static void GetBlockItemType(IBlock block, out Type childNodeType)
    {
        Contract.RequireNotNull(block, out IBlock Block);

        Type BlockType = Block.GetType();

        PropertyInfo NodeListProperty = SafeType.GetProperty(BlockType, nameof(IBlock.NodeList));
        Type PropertyType = NodeListProperty.PropertyType;

        Debug.Assert(PropertyType.IsGenericType);

        Type[] GenericArguments = PropertyType.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        childNodeType = GenericArguments[0];
    }

    /// <summary>
    /// Gets a block list in a node with the specified property.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    public static IBlockList GetBlockList(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        return BlockList;
    }

    /// <summary>
    /// Gets a list of all nodes in a node with the specified property.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childBlockList">The list of nodes upon return.</param>
    public static void GetChildBlockList(Node node, string propertyName, out IList<NodeTreeBlock> childBlockList)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);
        IList NodeBlockList = BlockList.NodeBlockList;

        List<NodeTreeBlock> Result = new();

        foreach (IBlock Block in SafeType.Items<IBlock>(NodeBlockList))
        {
            Pattern ReplicationPattern = Block.ReplicationPattern;
            Identifier SourceIdentifier = Block.SourceIdentifier;
            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList.Count > 0);

            List<Node> ResultNodeList = new();
            foreach (Node ChildNode in SafeType.Items<Node>(NodeList))
                ResultNodeList.Add(ChildNode);

            Result.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ResultNodeList));
        }

        childBlockList = Result;
    }

    /// <summary>
    /// Gets the type of a block list item in a node with the specified property.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The block list type.</returns>
    public static Type BlockListItemType(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Type NodeType = Node.GetType();

        return BlockListItemTypeInternal(NodeType, PropertyName);
    }

    /// <summary>
    /// Gets the type of a block list item in a node type with the specified property.
    /// </summary>
    /// <param name="nodeType">The node type with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The block list type.</returns>
    public static Type BlockListItemType(Type nodeType, string propertyName)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return BlockListItemTypeInternal(NodeType, PropertyName);
    }

    /// <summary>
    /// Gets the type of block list blocks in a node with the specified property.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The block list block type.</returns>
    public static Type BlockListBlockType(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Type NodeType = Node.GetType();

        return BlockListBlockTypeInternal(NodeType, PropertyName);
    }

    /// <summary>
    /// Gets the type of block list blocks in a node type with the specified property.
    /// </summary>
    /// <param name="nodeType">The node type with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The block list block type.</returns>
    public static Type BlockListBlockType(Type nodeType, string propertyName)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return BlockListBlockTypeInternal(NodeType, PropertyName);
    }

    /// <summary>
    /// Gets the index of the last block in a block list in a node with the specified property.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockIndex">The index of the last block upon return.</param>
    public static void GetLastBlockIndex(Node node, string propertyName, out int blockIndex)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        blockIndex = NodeBlockList.Count;
        Debug.Assert(blockIndex >= 0);
    }

    /// <summary>
    /// Gets the index of the last node in a block list in a node with the specified property.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockIndex">The block index.</param>
    /// <param name="index">The index of the last node in the block upon return.</param>
    public static void GetLastBlockChildIndex(Node node, string propertyName, int blockIndex, out int index)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);
        IList NodeList = Block.NodeList;

        index = NodeList.Count;
    }

    /// <summary>
    /// Checks whether a child node is in a block list of a node at the specified indexes.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockIndex">The block index.</param>
    /// <param name="index">The index of the last node in the block upon return.</param>
    /// <param name="childNode">The child node to check.</param>
    public static bool IsBlockChildNode(Node node, string propertyName, int blockIndex, int index, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

        return IsChildNodeInternal(Block, index, ChildNode);
    }

    /// <summary>
    /// Checks whether a child node is in a block specified index.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="index">The index of the last node in the block upon return.</param>
    /// <param name="childNode">The child node to check.</param>
    public static bool IsChildNode(IBlock block, int index, Node childNode)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        return IsChildNodeInternal(Block, index, ChildNode);
    }

    /// <summary>
    /// Gets the block at the specified index in a block list property of a given node.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockIndex">The block index.</param>
    /// <param name="childBlock">The block at the specified index upon return.</param>
    public static void GetChildBlock(Node node, string propertyName, int blockIndex, out IBlock childBlock)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        childBlock = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);
    }

    /// <summary>
    /// Gets the node at the specified indexes in a block list property of a given node.
    /// </summary>
    /// <param name="node">The node with the block list.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockIndex">The block index.</param>
    /// <param name="index">The node index in the block.</param>
    /// <param name="childNode">The node at the specified indexes upon return.</param>
    public static void GetChildNode(Node node, string propertyName, int blockIndex, int index, out Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

        childNode = GetChildNodeInternal(Block, index);
    }

    /// <summary>
    /// Gets the node at the specified index in a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="index">The node index.</param>
    /// <param name="childNode">The node at the specified index upon return.</param>
    public static void GetChildNode(IBlock block, int index, out Node childNode)
    {
        Contract.RequireNotNull(block, out IBlock Block);

        childNode = GetChildNodeInternal(Block, index);
    }

    /// <summary>
    /// Sets the block list a given node.
    /// </summary>
    /// <param name="node">The node with the block list property.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockList">The value to set.</param>
    public static void SetBlockList(Node node, string propertyName, IBlockList blockList)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(blockList, out IBlockList BlockList);

        Type NodeType = Node.GetType();

        if (!SafeType.CheckAndGetPropertyOf(NodeType, PropertyName, out PropertyInfo Property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

        Type PropertyType = Property.PropertyType;

        if (!NodeTreeHelper.IsBlockListInterfaceType(PropertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a block list property of {NodeType}");

        if (!PropertyType.IsAssignableFrom(BlockList.GetType()))
            throw new ArgumentException($"Property {nameof(blockList)} must conform to {PropertyType}");

        if (BlockList.NodeBlockList.Count == 0 && NodeHelper.IsCollectionNeverEmpty(Node, PropertyName))
            throw new NeverEmptyException(Node, PropertyName);

        Property.SetValue(Node, BlockList);
    }

    /// <summary>
    /// Checks whether a block list is empty.
    /// </summary>
    /// <param name="blockList">The block list.</param>
    /// <returns>True if the block list is empty; otherwise, false.</returns>
    public static bool IsBlockListEmpty(IBlockList blockList)
    {
        Contract.RequireNotNull(blockList, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        return NodeBlockList.Count == 0;
    }

    /// <summary>
    /// Checks whether a block list contains exactly one node.
    /// </summary>
    /// <param name="blockList">The block list.</param>
    /// <returns>True if the block list contains exactly one node; otherwise, false.</returns>
    public static bool IsBlockListSingle(IBlockList blockList)
    {
        Contract.RequireNotNull(blockList, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (NodeBlockList.Count == 0)
            return false;

        IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, 0);

        IList NodeList = Block.NodeList;
        Debug.Assert(NodeList.Count > 0);

        return NodeList.Count == 1;
    }

    private static bool IsBlockListPropertyInternal(Type nodeType, string propertyName, out Type childNodeType)
    {
        if (SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
        {
            Type PropertyType = Property.PropertyType;

            if (NodeTreeHelper.IsBlockListInterfaceType(PropertyType))
            {
                Debug.Assert(PropertyType.IsGenericType);

                Type[] GenericArguments = PropertyType.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 1);

                childNodeType = GenericArguments[0];

                return true;
            }
        }

        Contract.Unused(out childNodeType);
        return false;
    }

    private static void GetBlockListInternal(Node node, string propertyName, out PropertyInfo property, out Type propertyType, out IBlockList blockList)
    {
        Type NodeType = node.GetType();

        if (!SafeType.CheckAndGetPropertyOf(NodeType, propertyName, out property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

        propertyType = property.PropertyType;

        if (!NodeTreeHelper.IsBlockListInterfaceType(propertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a block list property of {NodeType}");

        blockList = SafeType.GetPropertyValue<IBlockList>(property, node);
    }

    private static Type BlockListItemTypeInternal(Type nodeType, string propertyName)
    {
        if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {nodeType}");

        Type PropertyType = Property.PropertyType;

        if (!NodeTreeHelper.IsBlockListInterfaceType(PropertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a block list property of {nodeType}");

        Debug.Assert(PropertyType.IsGenericType);

        Type[] GenericArguments = PropertyType.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type ItemType = GenericArguments[0];

        return ItemType;
    }

    private static Type BlockListBlockTypeInternal(Type nodeType, string propertyName)
    {
        if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {nodeType}");

        Type PropertyType = Property.PropertyType;

        if (!NodeTreeHelper.IsBlockListInterfaceType(PropertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a block list property of {nodeType}");

        PropertyInfo NodeListProperty = SafeType.GetProperty(PropertyType, nameof(BlockList<Node>.NodeBlockList));

        Type NodeListType = NodeListProperty.PropertyType;
        Debug.Assert(NodeListType.IsGenericType);

        Type[] GenericArguments = NodeListType.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type BlockType = GenericArguments[0];
        Debug.Assert(BlockType.IsInterface);

        return BlockType;
    }

    private static bool IsChildNodeInternal(IBlock block, int index, Node childNode)
    {
        IList NodeList = block.NodeList;

        if (index < 0 || index >= NodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Node NodeItem = SafeType.ItemAt<Node>(NodeList, index);

        return NodeItem == childNode;
    }

    private static Node GetChildNodeInternal(IBlock block, int index)
    {
        Contract.RequireNotNull(block, out IBlock Block);

        IList NodeList = Block.NodeList;

        if (index < 0 || index >= NodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Node NodeItem = SafeType.ItemAt<Node>(NodeList, index);

        return NodeItem;
    }
}
