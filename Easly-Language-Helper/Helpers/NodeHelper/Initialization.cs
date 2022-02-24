namespace BaseNodeHelper;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Easly;
using NotNullReflection;

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
        Type NodeType = Type.FromGetType(node);
        PropertyInfo ItemProperty = NodeType.GetProperty(propertyName);
        ItemProperty.SetValue(node, childNode);
    }

    private static void InitializeUnassignedOptionalChildNode(Node node, string propertyName)
    {
        Type NodeType = Type.FromGetType(node);
        PropertyInfo ItemProperty = NodeType.GetProperty(propertyName);

        Type PropertyType = ItemProperty.PropertyType;
        Type[] Generics = PropertyType.GetGenericArguments();
        Debug.Assert(Generics.Length == 1);

        Type ReferenceType = Type.FromTypeof<OptionalReference<object>>().GetGenericTypeDefinition().MakeGenericType(Generics);

        IOptionalReference EmptyReference = CreateInstance<IOptionalReference>(ReferenceType);

        Type ItemType = Generics[0];
        Node ItemNode = CreateDefaultFromType(ItemType);
        EmptyReference.Item = ItemNode;
        EmptyReference.Unassign();

        ItemProperty.SetValue(node, EmptyReference);
    }

    private static void InitializeEmptyNodeList(Node node, string propertyName, Type childNodeType)
    {
        Type[] Generics = new Type[] { childNodeType };
        Type ListType = Type.FromTypeof<List<Node>>().GetGenericTypeDefinition().MakeGenericType(Generics);

        IList EmptyList = CreateInstanceFromDefaultConstructor<IList>(ListType);

        Type NodeType = Type.FromGetType(node);
        PropertyInfo ReferenceProperty = NodeType.GetProperty(propertyName);

        ReferenceProperty.SetValue(node, EmptyList);
    }

    private static void InitializeSimpleNodeList(Node node, string propertyName, Type childNodeType, Node firstNode)
    {
        InitializeEmptyNodeList(node, propertyName, childNodeType);

        Type NodeType = Type.FromGetType(node);
        PropertyInfo ItemProperty = NodeType.GetProperty(propertyName);

        IList NodeList = (IList)ItemProperty.GetValue(node);

        NodeList.Add(firstNode);
    }

    private static void InitializeEmptyBlockList(Node node, string propertyName, Type childNodeType)
    {
        Type[] Generics = new Type[] { childNodeType };
        Type BlockListType = Type.FromTypeof<BlockList<Node>>().GetGenericTypeDefinition().MakeGenericType(Generics);

        IBlockList EmptyBlockList = CreateInstance<IBlockList>(BlockListType);

        Document EmptyDocument = CreateEmptyDocument();

        Type EmptyBlockListType = Type.FromGetType(EmptyBlockList);
        PropertyInfo DocumentationProperty = EmptyBlockListType.GetProperty(nameof(Node.Documentation));

        DocumentationProperty.SetValue(EmptyBlockList, EmptyDocument);

        Type ListOfBlockType = Type.FromTypeof<List<Node>>().GetGenericTypeDefinition().MakeGenericType(new Type[] { Type.FromTypeof<IBlock<Node>>().GetGenericTypeDefinition().MakeGenericType(Generics) });

        IList EmptyListOfBlock = CreateInstanceFromDefaultConstructor<IList>(ListOfBlockType);

        PropertyInfo NodeBlockListProperty = EmptyBlockListType.GetProperty(nameof(BlockList<Node>.NodeBlockList));

        NodeBlockListProperty.SetValue(EmptyBlockList, EmptyListOfBlock);

        Type NodeType = Type.FromGetType(node);
        PropertyInfo ItemProperty = NodeType.GetProperty(propertyName);

        ItemProperty.SetValue(node, EmptyBlockList);
    }

    private static void InitializeSimpleBlockList(Node node, string propertyName, Type childNodeType, Node firstNode)
    {
        InitializeEmptyBlockList(node, propertyName, childNodeType);

        Type[] Generics = new Type[] { childNodeType };
        Type BlockType = Type.FromTypeof<Block<Node>>().GetGenericTypeDefinition().MakeGenericType(Generics);

        IBlock EmptyBlock = CreateInstance<IBlock>(BlockType);

        Document EmptyDocumentation = CreateEmptyDocument();

        Type EmptyBlockType = Type.FromGetType(EmptyBlock);

        PropertyInfo DocumentationProperty = EmptyBlockType.GetProperty(nameof(Node.Documentation));

        DocumentationProperty.SetValue(EmptyBlock, EmptyDocumentation);

        PropertyInfo ReplicationProperty = EmptyBlockType.GetProperty(nameof(IBlock.Replication));

        ReplicationProperty.SetValue(EmptyBlock, ReplicationStatus.Normal);

        Pattern ReplicationPattern = CreateEmptyPattern();

        PropertyInfo ReplicationPatternProperty = EmptyBlockType.GetProperty(nameof(IBlock.ReplicationPattern));

        ReplicationPatternProperty.SetValue(EmptyBlock, ReplicationPattern);

        Identifier SourceIdentifier = CreateEmptyIdentifier();

        PropertyInfo SourceIdentifierProperty = EmptyBlockType.GetProperty(nameof(IBlock.SourceIdentifier));

        SourceIdentifierProperty.SetValue(EmptyBlock, SourceIdentifier);

        Type NodeListType = Type.FromTypeof<List<Node>>().GetGenericTypeDefinition().MakeGenericType(new Type[] { Generics[0] });

        IList NodeList = CreateInstanceFromDefaultConstructor<IList>(NodeListType);

        PropertyInfo NodeListProperty = EmptyBlockType.GetProperty(nameof(Block<Node>.NodeList));

        NodeListProperty.SetValue(EmptyBlock, NodeList);

        NodeList.Add(firstNode);

        Type NodeType = Type.FromGetType(node);
        PropertyInfo ItemProperty = NodeType.GetProperty(propertyName);

        IBlockList BlockList = (IBlockList)ItemProperty.GetValue(node);

        Type BlockListType = Type.FromGetType(BlockList);
        PropertyInfo NodeBlockListProperty = BlockListType.GetProperty(nameof(BlockList<Node>.NodeBlockList));

        IList NodeBlockList = (IList)NodeBlockListProperty.GetValue(BlockList);

        NodeBlockList.Add(EmptyBlock);
    }
}
