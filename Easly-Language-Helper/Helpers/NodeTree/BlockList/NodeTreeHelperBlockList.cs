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

    public static partial class NodeTreeHelperBlockList
    {
        public static bool IsBlockListProperty(Node node, string propertyName, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            if (!IsBlockListProperty(NodeType, propertyName, /*out childInterfaceType,*/ out childNodeType))
                return false;

            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            object Collection = Property.GetValue(node);
            Debug.Assert(Collection == null || Collection as IBlockList != null);

            return true;
        }

        public static bool IsBlockListProperty(Type nodeType, string propertyName, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            // childInterfaceType = null;
            childNodeType = null;

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsBlockListType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

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
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);

            return true;
        }

        public static void GetBlockType(IBlock block, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Type BlockType = block.GetType();
            Debug.Assert(BlockType.IsGenericType);

            Type[] GenericArguments = BlockType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

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
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);
        }

        public static IBlockList GetBlockList(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            return BlockList;
        }

        public static void GetChildBlockList(Node node, string propertyName, out IReadOnlyList<NodeTreeBlock> childBlockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            if (BlockList == null)
            {
                childBlockList = null;
                return;
            }
            else
            {
                IList NodeBlockList = BlockList.NodeBlockList;
                Debug.Assert(NodeBlockList != null);

                List<NodeTreeBlock> Result = new List<NodeTreeBlock>();

                foreach (object Item in NodeBlockList)
                {
                    IBlock Block = Item as IBlock;
                    Debug.Assert(Block != null);

                    Pattern ReplicationPattern = Block.ReplicationPattern;
                    Debug.Assert(ReplicationPattern != null);
                    Identifier SourceIdentifier = Block.SourceIdentifier;
                    Debug.Assert(SourceIdentifier != null);
                    IList NodeList = Block.NodeList;
                    Debug.Assert(NodeList != null);
                    Debug.Assert(NodeList.Count > 0);

                    List<Node> ResultNodeList = new List<Node>();
                    foreach (Node ChildNode in NodeList)
                        ResultNodeList.Add(ChildNode);

                    Result.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ResultNodeList));
                }

                childBlockList = Result.AsReadOnly();
            }
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

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);

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

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            // Type ItemType = GenericArguments[1];
            Type ItemType = GenericArguments[0];

            Debug.Assert(ItemType != null);
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

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            PropertyInfo NodeListProperty = PropertyType.GetProperty(nameof(BlockList<Node>.NodeBlockList));
            Debug.Assert(NodeListProperty != null);

            Type NodeListType = NodeListProperty.PropertyType;
            Debug.Assert(NodeListType.IsGenericType);

            Type[] GenericArguments = NodeListType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type BlockType = GenericArguments[0];
            Debug.Assert(BlockType != null);

            Debug.Assert(BlockType.IsInterface);

            return BlockType;
        }

        public static bool GetLastBlockIndex(Node node, string propertyName, out int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            blockIndex = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

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
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

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
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            return IsChildNode(Block, index, childNode);
        }

        public static bool IsChildNode(IBlock block, int index, Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = NodeList[index] as Node;
            Debug.Assert(NodeItem != null);

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
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            childBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(childBlock != null);
        }

        public static void GetChildNode(Node node, string propertyName, int blockIndex, int index, out Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = NodeList[index] as Node;
            Debug.Assert(NodeItem != null);

            childNode = NodeItem;
        }

        public static void GetChildNode(IBlock block, int index, out Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = NodeList[index] as Node;
            Debug.Assert(NodeItem != null);

            childNode = NodeItem;
        }

        public static void SetBlockList(Node node, string propertyName, IBlockList blockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            if (!Property.PropertyType.IsAssignableFrom(blockList.GetType())) throw new ArgumentException($"Property {nameof(propertyName)} of {nameof(node)} must not be read-only");

            Property.SetValue(node, blockList);
        }

        public static bool IsBlockPatternNode(Node node, string propertyName, int blockIndex, Pattern replicationPattern)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            return IsPatternNode(Block, replicationPattern);
        }

        public static bool IsPatternNode(IBlock block, Pattern replicationPattern)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            return replicationPattern == block.ReplicationPattern;
        }

        public static string GetPattern(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Pattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            return NodeTreeHelper.GetString(ReplicationPattern, nameof(Pattern.Text));
        }

        public static void SetPattern(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Pattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            NodeTreeHelper.SetString(ReplicationPattern, nameof(Pattern.Text), text);
        }

        public static bool IsBlockSourceNode(Node node, string propertyName, int blockIndex, Identifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            return IsSourceNode(Block, sourceIdentifier);
        }

        public static bool IsSourceNode(IBlock block, Identifier sourceIdentifier)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return sourceIdentifier == block.SourceIdentifier;
        }

        public static string GetSource(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Identifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            return NodeTreeHelper.GetString(SourceIdentifier, nameof(Identifier.Text));
        }

        public static void SetSource(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Identifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            NodeTreeHelper.SetString(SourceIdentifier, nameof(Identifier.Text), text);
        }

        public static void SetReplication(IBlock block, ReplicationStatus replication)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            block.GetType().GetProperty(nameof(IBlock.Replication)).SetValue(block, replication);
        }

        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            return NodeBlockList.Count == 0;
        }

        public static bool IsBlockListSingle(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList.Count == 0)
                return false;

            IBlock Block = NodeBlockList[0] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);
            Debug.Assert(NodeList.Count > 0);

            return NodeList.Count == 1;
        }

        public static IBlock CreateBlock(Node node, string propertyName, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            return CreateBlock(PropertyType, replication, replicationPattern, sourceIdentifier);
        }

        public static IBlock CreateBlock(IBlockList blockList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return CreateBlock(blockList.GetType(), replication, replicationPattern, sourceIdentifier);
        }

        public static IBlock CreateBlock(Type propertyType, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (propertyType == null) throw new ArgumentNullException(nameof(propertyType));
            if (!propertyType.IsGenericType) throw new ArgumentException($"{nameof(propertyType)} must be a generic type");
            Type GenericTypeDefinition = propertyType.GetGenericTypeDefinition();
            if (GenericTypeDefinition != typeof(IBlockList<>) && GenericTypeDefinition != typeof(BlockList<>) && GenericTypeDefinition != typeof(IBlock<>) && GenericTypeDefinition != typeof(Block<>)) throw new ArgumentException($"{nameof(propertyType)} must be a block or block list type");
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type[] TypeArguments = propertyType.GetGenericArguments();

            Type BlockType = typeof(Block<>).MakeGenericType(TypeArguments);
            IBlock NewBlock = BlockType.Assembly.CreateInstance(BlockType.FullName) as IBlock;
            Debug.Assert(NewBlock != null);

            Document EmptyComment = NodeHelper.CreateEmptyDocumentation();
            BlockType.GetProperty(nameof(Node.Documentation)).SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });
            IList NewNodeList = NodeListType.Assembly.CreateInstance(NodeListType.FullName) as IList;
            Debug.Assert(NewNodeList != null);

            BlockType.GetProperty(nameof(IBlock.Replication)).SetValue(NewBlock, replication);
            BlockType.GetProperty(nameof(IBlock.NodeList)).SetValue(NewBlock, NewNodeList);
            BlockType.GetProperty(nameof(IBlock.ReplicationPattern)).SetValue(NewBlock, replicationPattern);
            BlockType.GetProperty(nameof(IBlock.SourceIdentifier)).SetValue(NewBlock, sourceIdentifier);

            return NewBlock;
        }
    }
}
