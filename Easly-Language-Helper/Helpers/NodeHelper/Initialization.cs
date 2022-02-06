namespace BaseNodeHelper;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;
using Easly;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    private static void InitializeDocumentation(Node node)
    {
        Document EmptyDocument = CreateEmptyDocument();
        ((Node)node).Documentation = EmptyDocument;
    }

    private static void InitializeChildNode(Node node, string propertyName, Node childNode)
    {
        Type NodeType = node.GetType();
        PropertyInfo ItemProperty = SafeType.GetProperty(NodeType, propertyName);
        ItemProperty.SetValue(node, childNode);
    }

    private static void InitializeUnassignedOptionalChildNode(Node node, string propertyName)
    {
        Type NodeType = node.GetType();
        PropertyInfo ItemProperty = SafeType.GetProperty(NodeType, propertyName);

        Type PropertyType = ItemProperty.PropertyType;
        Type[] Generics = PropertyType.GetGenericArguments();
        Debug.Assert(Generics.Length == 1);

        Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
        string FullName = SafeType.FullName(ReferenceType);

        Assembly ReferenceAssembly = ReferenceType.Assembly;

        IOptionalReference EmptyReference = SafeType.CreateInstance<IOptionalReference>(ReferenceAssembly, FullName);

        Type ItemType = Generics[0];
        Node ItemNode = CreateDefaultFromType(ItemType);
        EmptyReference.Item = ItemNode;
        EmptyReference.Unassign();

        ItemProperty.SetValue(node, EmptyReference);
    }

    private static void InitializeEmptyNodeList(Node node, string propertyName, Type childNodeType)
    {
        Type[] Generics = new Type[] { childNodeType };
        Type ListType = typeof(List<>).MakeGenericType(Generics);
        string FullName = SafeType.FullName(ListType);

        Assembly ListAssembly = ListType.Assembly;

        IList EmptyList = SafeType.CreateInstanceFromDefaultConstructor<IList>(ListAssembly, FullName);

        Type NodeType = node.GetType();
        PropertyInfo ReferenceProperty = SafeType.GetProperty(NodeType, propertyName);

        ReferenceProperty.SetValue(node, EmptyList);
    }

    private static void InitializeSimpleNodeList(Node node, string propertyName, Type childNodeType, Node firstNode)
    {
        InitializeEmptyNodeList(node, propertyName, childNodeType);

        Type NodeType = node.GetType();
        PropertyInfo ItemProperty = SafeType.GetProperty(NodeType, propertyName);

        IList NodeList = SafeType.GetPropertyValue<IList>(ItemProperty, node);

        NodeList.Add(firstNode);
    }

    private static void InitializeEmptyBlockList(Node node, string propertyName, Type childNodeType)
    {
        Type[] Generics = new Type[] { childNodeType };
        Type BlockListType = typeof(BlockList<>).MakeGenericType(Generics);
        string FullName = SafeType.FullName(BlockListType);

        Assembly BlockListAssembly = BlockListType.Assembly;
        IBlockList EmptyBlockList = SafeType.CreateInstance<IBlockList>(BlockListAssembly, FullName);

        Document EmptyDocument = CreateEmptyDocument();

        Type EmptyBlockListType = EmptyBlockList.GetType();
        PropertyInfo DocumentationProperty = SafeType.GetProperty(EmptyBlockListType, nameof(Node.Documentation));

        DocumentationProperty.SetValue(EmptyBlockList, EmptyDocument);

        Type ListOfBlockType = typeof(List<>).MakeGenericType(new Type[] { typeof(IBlock<>).MakeGenericType(Generics) });

        FullName = SafeType.FullName(ListOfBlockType);

        BlockListAssembly = ListOfBlockType.Assembly;
        IList EmptyListOfBlock = SafeType.CreateInstanceFromDefaultConstructor<IList>(BlockListAssembly, FullName);

        PropertyInfo NodeBlockListProperty = SafeType.GetProperty(EmptyBlockListType, nameof(BlockList<Node>.NodeBlockList));

        NodeBlockListProperty.SetValue(EmptyBlockList, EmptyListOfBlock);

        Type NodeType = node.GetType();
        PropertyInfo ItemProperty = SafeType.GetProperty(NodeType, propertyName);

        ItemProperty.SetValue(node, EmptyBlockList);
    }

    private static void InitializeSimpleBlockList(Node node, string propertyName, Type childNodeType, Node firstNode)
    {
        InitializeEmptyBlockList(node, propertyName, childNodeType);

        Type[] Generics = new Type[] { childNodeType };
        Type BlockType = typeof(Block<>).MakeGenericType(Generics);

        Assembly BlockTypeAssembly = BlockType.Assembly;
        string BlockTypeFullName = SafeType.FullName(BlockType);

        IBlock EmptyBlock = SafeType.CreateInstance<IBlock>(BlockTypeAssembly, BlockTypeFullName);

        Document EmptyDocumentation = CreateEmptyDocument();

        Type EmptyBlockType = EmptyBlock.GetType();

        PropertyInfo DocumentationProperty = SafeType.GetProperty(EmptyBlockType, nameof(Node.Documentation));

        DocumentationProperty.SetValue(EmptyBlock, EmptyDocumentation);

        PropertyInfo ReplicationProperty = SafeType.GetProperty(EmptyBlockType, nameof(IBlock.Replication));

        ReplicationProperty.SetValue(EmptyBlock, ReplicationStatus.Normal);

        Pattern ReplicationPattern = CreateEmptyPattern();

        PropertyInfo ReplicationPatternProperty = SafeType.GetProperty(EmptyBlockType, nameof(IBlock.ReplicationPattern));

        ReplicationPatternProperty.SetValue(EmptyBlock, ReplicationPattern);

        Identifier SourceIdentifier = CreateEmptyIdentifier();

        PropertyInfo SourceIdentifierProperty = SafeType.GetProperty(EmptyBlockType, nameof(IBlock.SourceIdentifier));

        SourceIdentifierProperty.SetValue(EmptyBlock, SourceIdentifier);

        Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { Generics[0] });

        string NodeListFullName = SafeType.FullName(NodeListType);

        Assembly NodeListAssembly = NodeListType.Assembly;
        IList NodeList = SafeType.CreateInstanceFromDefaultConstructor<IList>(NodeListAssembly, NodeListFullName);

        PropertyInfo NodeListProperty = SafeType.GetProperty(EmptyBlockType, nameof(Block<Node>.NodeList));

        NodeListProperty.SetValue(EmptyBlock, NodeList);

        NodeList.Add(firstNode);

        Type NodeType = node.GetType();
        PropertyInfo ItemProperty = SafeType.GetProperty(NodeType, propertyName);

        IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(ItemProperty, node);

        Type BlockListType = BlockList.GetType();
        PropertyInfo NodeBlockListProperty = SafeType.GetProperty(BlockListType, nameof(BlockList<Node>.NodeBlockList));

        IList NodeBlockList = SafeType.GetPropertyValue<IList>(NodeBlockListProperty, BlockList);

        NodeBlockList.Add(EmptyBlock);
    }
}
