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
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            ItemProperty.SetValue(node, childNode);
        }

        private static void InitializeUnassignedOptionalChildNode(Node node, string propertyName)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
            IOptionalReference EmptyReference = (IOptionalReference)ReferenceType.Assembly.CreateInstance(ReferenceType.FullName);

            ItemProperty.SetValue(node, EmptyReference);
        }

        private static void InitializeOptionalChildNode(Node node, string propertyName, Node childNode)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
            IOptionalReference EmptyReference = (IOptionalReference)ReferenceType.Assembly.CreateInstance(ReferenceType.FullName);
            ReferenceType.GetProperty(nameof(OptionalReference<Node>.Item)).SetValue(EmptyReference, childNode);
            EmptyReference.Unassign();

            ItemProperty.SetValue(node, EmptyReference);
        }

        private static void InitializeEmptyNodeList(Node node, string propertyName, Type childNodeType)
        {
            Type[] Generics = new Type[] { childNodeType };
            Type ListType = typeof(List<>).MakeGenericType(Generics);
            IList EmptyList = (IList)ListType.Assembly.CreateInstance(ListType.FullName);

            node.GetType().GetProperty(propertyName).SetValue(node, EmptyList);
        }

        private static void InitializeSimpleNodeList(Node node, string propertyName, Type childNodeType, Node firstNode)
        {
            InitializeEmptyNodeList(node, propertyName, childNodeType);

            IList NodeList = (IList)node.GetType().GetProperty(propertyName).GetValue(node);
            NodeList.Add(firstNode);
        }

        private static void InitializeEmptyBlockList(Node node, string propertyName, /*Type childInterfaceType,*/ Type childNodeType)
        {
            Type[] Generics = new Type[] { /*childInterfaceType,*/ childNodeType };
            Type BlockListType = typeof(BlockList<>).MakeGenericType(Generics);
            Type BlockType = typeof(Block<>).MakeGenericType(Generics);

            IBlockList EmptyBlockList = (IBlockList)BlockListType.Assembly.CreateInstance(BlockListType.FullName);

            Document EmptyEmptyDocumentation = CreateEmptyDocumentation();
            EmptyBlockList.GetType().GetProperty(nameof(Node.Documentation)).SetValue(EmptyBlockList, EmptyEmptyDocumentation);

            Type ListOfBlockType = typeof(List<>).MakeGenericType(new Type[] { typeof(Block<>).MakeGenericType(Generics) });
            IList EmptyListOfBlock = (IList)ListOfBlockType.Assembly.CreateInstance(ListOfBlockType.FullName);
            EmptyBlockList.GetType().GetProperty(nameof(BlockList<Node>.NodeBlockList)).SetValue(EmptyBlockList, EmptyListOfBlock);

            node.GetType().GetProperty(propertyName).SetValue(node, EmptyBlockList);
        }

        private static void InitializeSimpleBlockList(Node node, string propertyName, /*Type childInterfaceType,*/ Type childNodeType, Node firstNode)
        {
            InitializeEmptyBlockList(node, propertyName, /*childInterfaceType,*/ childNodeType);

            Type[] Generics = new Type[] { /*childInterfaceType,*/ childNodeType };
            Type BlockType = typeof(Block<>).MakeGenericType(Generics);
            IBlock EmptyBlock = (IBlock)BlockType.Assembly.CreateInstance(BlockType.FullName);

            Document EmptyEmptyDocumentation = CreateEmptyDocumentation();
            EmptyBlock.GetType().GetProperty(nameof(Node.Documentation)).SetValue(EmptyBlock, EmptyEmptyDocumentation);

            EmptyBlock.GetType().GetProperty(nameof(IBlock.Replication)).SetValue(EmptyBlock, ReplicationStatus.Normal);

            Pattern ReplicationPattern = CreateEmptyPattern();
            EmptyBlock.GetType().GetProperty(nameof(IBlock.ReplicationPattern)).SetValue(EmptyBlock, ReplicationPattern);

            Identifier SourceIdentifier = CreateEmptyIdentifier();
            EmptyBlock.GetType().GetProperty(nameof(IBlock.SourceIdentifier)).SetValue(EmptyBlock, SourceIdentifier);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { Generics[0] });
            IList NodeList = (IList)NodeListType.Assembly.CreateInstance(NodeListType.FullName);
            EmptyBlock.GetType().GetProperty(nameof(Block<Node>.NodeList)).SetValue(EmptyBlock, NodeList);

            NodeList.Add(firstNode);

            IBlockList BlockList = (IBlockList)node.GetType().GetProperty(propertyName).GetValue(node);

            IList NodeBlockList = (IList)BlockList.GetType().GetProperty(nameof(BlockList<Node>.NodeBlockList)).GetValue(BlockList, null);
            NodeBlockList.Add(EmptyBlock);
        }
    }
}
