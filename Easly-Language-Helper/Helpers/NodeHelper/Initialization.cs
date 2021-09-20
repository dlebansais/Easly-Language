#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

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
        private static void InitializeDocumentation(Node node)
        {
            Document EmptyDocumentation = CreateEmptyDocumentation();
            ((Node)node).Documentation = EmptyDocumentation;
        }

        private static void InitializeChildNode(Node node, string propertyName, Node childNode)
        {
            Type? NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? ItemProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ItemProperty != null);

            if (ItemProperty == null)
                return;

            ItemProperty.SetValue(node, childNode);
        }

        private static void InitializeUnassignedOptionalChildNode(Node node, string propertyName)
        {
            Type? NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? ItemProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ItemProperty != null);

            if (ItemProperty == null)
                return;

            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
            string? FullName = ReferenceType.FullName;
            Debug.Assert(FullName != null);

            if (FullName == null)
                return;

            Assembly ReferenceAssembly = ReferenceType.Assembly;

            IOptionalReference? EmptyReference = ReferenceAssembly.CreateInstance(FullName) as IOptionalReference;
            Debug.Assert(EmptyReference != null);

            if (EmptyReference == null)
                return;

            ItemProperty.SetValue(node, EmptyReference);
        }

        private static void InitializeOptionalChildNode(Node node, string propertyName, Node childNode)
        {
            Type? NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? ItemProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ItemProperty != null);

            if (ItemProperty == null)
                return;

            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
            string? FullName = ReferenceType.FullName;
            Debug.Assert(FullName != null);

            if (FullName == null)
                return;

            Assembly ReferenceAssembly = ReferenceType.Assembly;

            IOptionalReference? EmptyReference = ReferenceAssembly.CreateInstance(FullName) as IOptionalReference;
            Debug.Assert(EmptyReference != null);

            if (EmptyReference == null)
                return;

            PropertyInfo? ReferenceProperty = ReferenceType.GetProperty(nameof(OptionalReference<Node>.Item));
            Debug.Assert(ReferenceProperty != null);

            if (ReferenceProperty == null)
                return;

            ReferenceProperty.SetValue(EmptyReference, childNode);

            EmptyReference.Unassign();

            ItemProperty.SetValue(node, EmptyReference);
        }

        private static void InitializeEmptyNodeList(Node node, string propertyName, Type childNodeType)
        {
            Type[] Generics = new Type[] { childNodeType };
            Type ListType = typeof(List<>).MakeGenericType(Generics);
            Debug.Assert(ListType != null);

            if (ListType == null)
                return;

            string? FullName = ListType.FullName;
            Debug.Assert(FullName != null);

            if (FullName == null)
                return;

            Assembly ListAssembly = ListType.Assembly;

            IList? EmptyList = ListAssembly.CreateInstance(FullName) as IList;
            Debug.Assert(EmptyList != null);

            if (EmptyList == null)
                return;

            Type NodeType = node.GetType();
            PropertyInfo? ReferenceProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ReferenceProperty != null);

            if (ReferenceProperty == null)
                return;

            ReferenceProperty.SetValue(node, EmptyList);
        }

        private static void InitializeSimpleNodeList(Node node, string propertyName, Type childNodeType, Node firstNode)
        {
            InitializeEmptyNodeList(node, propertyName, childNodeType);

            Type? NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? ItemProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ItemProperty != null);

            if (ItemProperty == null)
                return;

            IList? NodeList = ItemProperty.GetValue(node) as IList;
            Debug.Assert(NodeList != null);

            if (NodeList == null)
                return;

            NodeList.Add(firstNode);
        }

        private static void InitializeEmptyBlockList(Node node, string propertyName, /*Type childInterfaceType,*/ Type childNodeType)
        {
            Type[] Generics = new Type[] { /*childInterfaceType,*/ childNodeType };
            Type BlockListType = typeof(BlockList<>).MakeGenericType(Generics);
            Debug.Assert(BlockListType != null);

            if (BlockListType == null)
                return;

            string? FullName = BlockListType.FullName;
            Debug.Assert(FullName != null);

            if (FullName == null)
                return;

            Assembly BlockListAssembly = BlockListType.Assembly;
            Debug.Assert(BlockListAssembly != null);

            if (BlockListAssembly == null)
                return;

            IBlockList? EmptyBlockList = BlockListAssembly.CreateInstance(FullName) as IBlockList;
            Debug.Assert(EmptyBlockList != null);

            if (EmptyBlockList == null)
                return;

            Document EmptyEmptyDocumentation = CreateEmptyDocumentation();

            Type? EmptyBlockListType = EmptyBlockList.GetType();
            Debug.Assert(EmptyBlockListType != null);

            if (EmptyBlockListType == null)
                return;

            PropertyInfo? DocumentationProperty = EmptyBlockListType.GetProperty(nameof(Node.Documentation));
            Debug.Assert(DocumentationProperty != null);

            if (DocumentationProperty == null)
                return;

            DocumentationProperty.SetValue(EmptyBlockList, EmptyEmptyDocumentation);

            Type ListOfBlockType = typeof(List<>).MakeGenericType(new Type[] { typeof(IBlock<>).MakeGenericType(Generics) });

            FullName = ListOfBlockType.FullName;
            Debug.Assert(FullName != null);

            if (FullName == null)
                return;

            BlockListAssembly = ListOfBlockType.Assembly;
            Debug.Assert(BlockListAssembly != null);

            if (BlockListAssembly == null)
                return;

            IList? EmptyListOfBlock = BlockListAssembly.CreateInstance(FullName) as IList;
            Debug.Assert(EmptyListOfBlock != null);

            if (EmptyListOfBlock == null)
                return;

            PropertyInfo? NodeBlockListProperty = EmptyBlockListType.GetProperty(nameof(BlockList<Node>.NodeBlockList));
            Debug.Assert(NodeBlockListProperty != null);

            if (NodeBlockListProperty == null)
                return;

            NodeBlockListProperty.SetValue(EmptyBlockList, EmptyListOfBlock);

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? ItemtProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ItemtProperty != null);

            if (ItemtProperty == null)
                return;

            ItemtProperty.SetValue(node, EmptyBlockList);
        }

        private static void InitializeSimpleBlockList(Node node, string propertyName, /*Type childInterfaceType,*/ Type childNodeType, Node firstNode)
        {
            InitializeEmptyBlockList(node, propertyName, /*childInterfaceType,*/ childNodeType);

            Type[] Generics = new Type[] { /*childInterfaceType,*/ childNodeType };
            Type BlockType = typeof(Block<>).MakeGenericType(Generics);

            Assembly BlockTypeAssembly = BlockType.Assembly;
            Debug.Assert(BlockTypeAssembly != null);

            if (BlockTypeAssembly == null)
                return;

            string? BlockTypeFullName = BlockType.FullName;
            Debug.Assert(BlockTypeFullName != null);

            if (BlockTypeFullName == null)
                return;

            IBlock? EmptyBlock = BlockTypeAssembly.CreateInstance(BlockTypeFullName) as IBlock;
            Debug.Assert(EmptyBlock != null);

            if (EmptyBlock == null)
                return;

            Document EmptyEmptyDocumentation = CreateEmptyDocumentation();

            Type EmptyBlockType = EmptyBlock.GetType();

            PropertyInfo? DocumentationProperty = EmptyBlockType.GetProperty(nameof(Node.Documentation));
            Debug.Assert(DocumentationProperty != null);

            if (DocumentationProperty == null)
                return;

            DocumentationProperty.SetValue(EmptyBlock, EmptyEmptyDocumentation);

            PropertyInfo? ReplicationProperty = EmptyBlockType.GetProperty(nameof(IBlock.Replication));
            Debug.Assert(ReplicationProperty != null);

            if (ReplicationProperty == null)
                return;

            ReplicationProperty.SetValue(EmptyBlock, ReplicationStatus.Normal);

            Pattern ReplicationPattern = CreateEmptyPattern();

            PropertyInfo? ReplicationPatternProperty = EmptyBlockType.GetProperty(nameof(IBlock.ReplicationPattern));
            Debug.Assert(ReplicationPatternProperty != null);

            if (ReplicationPatternProperty == null)
                return;

            ReplicationPatternProperty.SetValue(EmptyBlock, ReplicationPattern);

            Identifier SourceIdentifier = CreateEmptyIdentifier();

            PropertyInfo? SourceIdentifierProperty = EmptyBlockType.GetProperty(nameof(IBlock.SourceIdentifier));
            Debug.Assert(SourceIdentifierProperty != null);

            if (SourceIdentifierProperty == null)
                return;

            SourceIdentifierProperty.SetValue(EmptyBlock, SourceIdentifier);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { Generics[0] });

            string? NodeListFullName = NodeListType.FullName;
            Debug.Assert(NodeListFullName != null);

            if (NodeListFullName == null)
                return;

            Assembly NodeListAssembly = NodeListType.Assembly;
            Debug.Assert(NodeListAssembly != null);

            if (NodeListAssembly == null)
                return;

            IList? NodeList = NodeListAssembly.CreateInstance(NodeListFullName) as IList;
            Debug.Assert(NodeList != null);

            if (NodeList == null)
                return;

            PropertyInfo? NodeListProperty = EmptyBlockType.GetProperty(nameof(Block<Node>.NodeList));
            Debug.Assert(NodeListProperty != null);

            if (NodeListProperty == null)
                return;

            NodeListProperty.SetValue(EmptyBlock, NodeList);

            NodeList.Add(firstNode);

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? ItemProperty = NodeType.GetProperty(propertyName);
            Debug.Assert(ItemProperty != null);

            if (ItemProperty == null)
                return;

            IBlockList? BlockList = ItemProperty.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return;

            Type BlockListType = BlockList.GetType();
            Debug.Assert(BlockListType != null);

            if (BlockListType == null)
                return;

            PropertyInfo? NodeBlockListProperty = BlockListType.GetProperty(nameof(BlockList<Node>.NodeBlockList));
            Debug.Assert(NodeBlockListProperty != null);

            if (NodeBlockListProperty == null)
                return;

            IList? NodeBlockList = NodeBlockListProperty.GetValue(BlockList, null) as IList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return;

            NodeBlockList.Add(EmptyBlock);
        }
    }
}
