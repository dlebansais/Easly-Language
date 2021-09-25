namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

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
        public static bool IsBlockListProperty(Node node, string propertyName, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            Type NodeType = node.GetType();
            if (!IsBlockListProperty(NodeType, propertyName, /*out childInterfaceType,*/ out childNodeType))
                return false;

            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);
            object Collection = SafeType.GetPropertyValue(Property, node);
            Debug.Assert(Collection is IBlockList);

            return true;
        }

        /// <summary>
        /// Checks whether a property of a node type is a block list.
        /// </summary>
        /// <param name="nodeType">The node type with the property to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="childNodeType">If successful, the block list node type upon return.</param>
        /// <returns>True if the property of the node is a block list; otherwise, false.</returns>
        public static bool IsBlockListProperty(Type nodeType, string propertyName, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            // childInterfaceType = null;
            childNodeType = null!;

            if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsBlockListType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();

            /*
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            Debug.Assert(childInterfaceType != null);
            Debug.Assert(childInterfaceType.IsInterface);

            childNodeType = GenericArguments[1];
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);
            */

            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];
            Debug.Assert(!childNodeType.IsInterface);

            return true;
        }

        /// <summary>
        /// Gets the type of nodes in a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="childNodeType">The node type upon return.</param>
        public static void GetBlockType(IBlock block, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Type BlockType = block.GetType();
            Debug.Assert(BlockType.IsGenericType);

            Type[] GenericArguments = BlockType.GetGenericArguments();

            /*
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            Debug.Assert(childInterfaceType != null);
            Debug.Assert(childInterfaceType.IsInterface);

            childNodeType = GenericArguments[1];
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);
            */

            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];
            Debug.Assert(!childNodeType.IsInterface);
        }

        /// <summary>
        /// Gets a block list in a node with the specified property.
        /// </summary>
        /// <param name="node">The node with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        public static IBlockList GetBlockList(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            return BlockList;
        }

        /// <summary>
        /// Gets a list of all nodes in a node with the specified property.
        /// </summary>
        /// <param name="node">The node with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="childBlockList">The list of nodes upon return.</param>
        public static void GetChildBlockList(Node node, string propertyName, out IReadOnlyList<NodeTreeBlock> childBlockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);
            IList NodeBlockList = BlockList.NodeBlockList;

            List<NodeTreeBlock> Result = new List<NodeTreeBlock>();

            foreach (IBlock Block in SafeType.Items<IBlock>(NodeBlockList))
            {
                Pattern ReplicationPattern = Block.ReplicationPattern;
                Identifier SourceIdentifier = Block.SourceIdentifier;
                IList NodeList = Block.NodeList;
                Debug.Assert(NodeList.Count > 0);

                List<Node> ResultNodeList = new List<Node>();
                foreach (Node ChildNode in SafeType.Items<Node>(NodeList))
                    ResultNodeList.Add(ChildNode);

                Result.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ResultNodeList));
            }

            childBlockList = Result.AsReadOnly();
        }

        /// <summary>
        /// Gets the type of a block list in a node with the specified property.
        /// </summary>
        /// <param name="node">The node with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The block list type.</returns>
        public static Type BlockListInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListInterfaceType(NodeType, propertyName);
        }

        /// <summary>
        /// Gets the type of a block list in a node type with the specified property.
        /// </summary>
        /// <param name="nodeType">The node type with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The block list type.</returns>
        public static Type BlockListInterfaceType(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];

            // Debug.Assert(InterfaceType.IsInterface);
            Debug.Assert(!InterfaceType.IsInterface);

            return InterfaceType;
        }

        /// <summary>
        /// Gets the type of block list nodes in a node with the specified property.
        /// </summary>
        /// <param name="node">The node with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The block list node type.</returns>
        public static Type BlockListItemType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListItemType(NodeType, propertyName);
        }

        /// <summary>
        /// Gets the type of block list nodes in a node type with the specified property.
        /// </summary>
        /// <param name="nodeType">The node type with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The block list node type.</returns>
        public static Type BlockListItemType(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            // Type ItemType = GenericArguments[1];
            Type ItemType = GenericArguments[0];

            Debug.Assert(!ItemType.IsInterface);

            return ItemType;
        }

        /// <summary>
        /// Gets the type of block list blocks in a node with the specified property.
        /// </summary>
        /// <param name="node">The node with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The block list block type.</returns>
        public static Type BlockListBlockType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListBlockType(NodeType, propertyName);
        }

        /// <summary>
        /// Gets the type of block list blocks in a node type with the specified property.
        /// </summary>
        /// <param name="nodeType">The node type with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The block list block type.</returns>
        public static Type BlockListBlockType(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            PropertyInfo NodeListProperty = SafeType.GetProperty(PropertyType, nameof(BlockList<Node>.NodeBlockList));

            Type NodeListType = NodeListProperty.PropertyType;
            Debug.Assert(NodeListType.IsGenericType);

            Type[] GenericArguments = NodeListType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type BlockType = GenericArguments[0];

            Debug.Assert(BlockType.IsInterface);

            return BlockType;
        }

        /// <summary>
        /// Gets the index of the last block in a block list in a node with the specified property.
        /// </summary>
        /// <param name="node">The node with the block list.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">The index of the last block upon return.</param>
        public static void GetLastBlockIndex(Node node, string propertyName, out int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            blockIndex = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            index = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            IList NodeList = Block.NodeList;

            index = NodeList.Count;
            Debug.Assert(index >= 0);
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            return IsChildNode(Block, index, childNode);
        }

        /// <summary>
        /// Checks whether a child node is in a block specified index.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="index">The index of the last node in the block upon return.</param>
        /// <param name="childNode">The child node to check.</param>
        public static bool IsChildNode(IBlock block, int index, Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = SafeType.ItemAt<Node>(NodeList, index);

            if (NodeItem != childNode)
                return false;

            return true;
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();

            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock BlockFromList = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            childBlock = BlockFromList;
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            IList NodeList = Block.NodeList;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = SafeType.ItemAt<Node>(NodeList, index);

            childNode = NodeItem;
        }

        /// <summary>
        /// Gets the node at the specified index in a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="index">The node index.</param>
        /// <param name="childNode">The node at the specified index upon return.</param>
        public static void GetChildNode(IBlock block, int index, out Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            IList NodeList = block.NodeList;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = SafeType.ItemAt<Node>(NodeList, index);

            childNode = NodeItem;
        }

        /// <summary>
        /// Sets the block list a given node.
        /// </summary>
        /// <param name="node">The node with the block list property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockList">The value to set.</param>
        public static void SetBlockList(Node node, string propertyName, IBlockList blockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            if (!PropertyType.IsAssignableFrom(blockList.GetType())) throw new ArgumentException($"Property {nameof(propertyName)} of {nameof(node)} must not be read-only");

            Property.SetValue(node, blockList);
        }

        /// <summary>
        /// Checks whether a block list is empty.
        /// </summary>
        /// <param name="blockList">The block list.</param>
        /// <returns>True if the block list is empty; otherwise, false.</returns>
        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;

            return NodeBlockList.Count == 0;
        }

        /// <summary>
        /// Checks whether a block list contains exactly one node.
        /// </summary>
        /// <param name="blockList">The block list.</param>
        /// <returns>True if the block list contains exactly one node; otherwise, false.</returns>
        public static bool IsBlockListSingle(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;

            if (NodeBlockList.Count == 0)
                return false;

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, 0);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList.Count > 0);

            return NodeList.Count == 1;
        }
    }
}
