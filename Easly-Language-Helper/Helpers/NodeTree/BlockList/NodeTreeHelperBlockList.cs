﻿#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeTreeHelperBlockList
    {
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

        public static Type BlockListInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListInterfaceType(NodeType, propertyName);
        }

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

        public static Type BlockListItemType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListItemType(NodeType, propertyName);
        }

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

        public static Type BlockListBlockType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListBlockType(NodeType, propertyName);
        }

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

        public static bool GetLastBlockIndex(Node node, string propertyName, out int blockIndex)
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

            return true;
        }

        public static bool GetLastBlockChildIndex(Node node, string propertyName, int blockIndex, out int index)
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

            return true;
        }

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

        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;

            return NodeBlockList.Count == 0;
        }

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
