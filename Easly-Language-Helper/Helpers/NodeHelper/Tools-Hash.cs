﻿namespace BaseNodeHelper
{
    using System;
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
        /// Gets the hash value of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The has value.</returns>
        public static ulong NodeHash(Node node)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);
            ulong Hash = 0;

            foreach (string PropertyName in PropertyNames)
                NodeHashPropertyName(node, PropertyName, ref Hash);

            return Hash;
        }

        private static void NodeHashPropertyName(Node node, string propertyName, ref ulong hash)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(node, propertyName, out _))
                NodeHashChildNode(node, propertyName, ref hash);
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, propertyName, out _))
                NodeHashOptionalChildNode(node, propertyName, ref hash);
            else if (NodeTreeHelperList.IsNodeListProperty(node, propertyName, out _))
                NodeHashNodeList(node, propertyName, ref hash);
            else if (NodeTreeHelperBlockList.IsBlockListProperty(node, propertyName, /*out Type ChildInterfaceType,*/ out _))
                NodeHashBlockList(node, propertyName, ref hash);
            else
                NodeHashOther(node, propertyName, ref hash);
        }

        private static void NodeHashChildNode(Node node, string propertyName, ref ulong hash)
        {
            NodeTreeHelperChild.GetChildNode(node, propertyName, out Node ChildNode);

            MergeHash(ref hash, NodeHash(ChildNode));
        }

        private static void NodeHashOptionalChildNode(Node node, string propertyName, ref ulong hash)
        {
            NodeTreeHelperOptional.GetChildNode(node, propertyName, out bool IsAssigned, out _, out Node ChildNode);

            MergeHash(ref hash, IsAssigned ? 1UL : 0);

            if (IsAssigned)
                MergeHash(ref hash, NodeHash(ChildNode));
        }

        private static void NodeHashNodeList(Node node, string propertyName, ref ulong hash)
        {
            NodeTreeHelperList.GetChildNodeList(node, propertyName, out IReadOnlyList<Node> ChildNodeList);

            foreach (Node ChildNode in ChildNodeList)
                MergeHash(ref hash, NodeHash(ChildNode));
        }

        private static void NodeHashBlockList(Node node, string propertyName, ref ulong hash)
        {
            NodeTreeHelperBlockList.GetChildBlockList(node, propertyName, out IReadOnlyList<NodeTreeBlock> ChildBlockList);

            for (int i = 0; i < ChildBlockList.Count; i++)
            {
                NodeTreeHelperBlockList.GetChildBlock(node, propertyName, i, out IBlock ChildBlock);
                IReadOnlyList<Node> NodeList = ChildBlockList[i].NodeList;

                MergeHash(ref hash, ValueHash(ChildBlock.Documentation.Comment));
                MergeHash(ref hash, ValueHash(ChildBlock.Documentation.Uuid));
                MergeHash(ref hash, ValueHash((int)ChildBlock.Replication));
                MergeHash(ref hash, NodeHash(ChildBlock.ReplicationPattern));
                MergeHash(ref hash, NodeHash(ChildBlock.SourceIdentifier));

                foreach (Node ChildNode in NodeList)
                    MergeHash(ref hash, NodeHash(ChildNode));
            }
        }

        private static void NodeHashOther(Node node, string propertyName, ref ulong hash)
        {
            Type NodeType = node.GetType();

            PropertyInfo Info = SafeType.GetProperty(NodeType, propertyName);

            if (Info.PropertyType == typeof(Document))
            {
                Document? Documentation = SafeType.GetPropertyValue<Document>(Info, node);

                MergeHash(ref hash, ValueHash(Documentation.Comment));
                MergeHash(ref hash, ValueHash(Documentation.Uuid));
            }
            else if (Info.PropertyType == typeof(bool))
            {
                bool? PropertyValue = Info.GetValue(node) as bool?;
                Debug.Assert(PropertyValue != null);

                if (PropertyValue == null)
                    return;

                MergeHash(ref hash, ValueHash(PropertyValue.Value));
            }
            else if (Info.PropertyType.IsEnum)
            {
                int? PropertyValue = Info.GetValue(node) as int?;
                Debug.Assert(PropertyValue != null);

                if (PropertyValue == null)
                    return;

                MergeHash(ref hash, ValueHash(PropertyValue.Value));
            }
            else if (Info.PropertyType == typeof(string))
            {
                string? PropertyValue = Info.GetValue(node) as string;
                Debug.Assert(PropertyValue != null);

                if (PropertyValue == null)
                    return;

                MergeHash(ref hash, ValueHash(PropertyValue));
            }
            else if (Info.PropertyType == typeof(Guid))
            {
                Guid? PropertyValue = Info.GetValue(node) as Guid?;
                Debug.Assert(PropertyValue != null);

                if (PropertyValue == null)
                    return;

                MergeHash(ref hash, ValueHash(PropertyValue.Value));
            }
            else
                throw new ArgumentOutOfRangeException($"{nameof(NodeType)}: {NodeType.FullName}");
        }

        private static ulong ValueHash(bool value)
        {
            return (ulong)value.GetHashCode();
        }

        private static ulong ValueHash(int value)
        {
            return (ulong)value.GetHashCode();
        }

        private static ulong ValueHash(string value)
        {
            return (ulong)value.GetHashCode();
        }

        private static ulong ValueHash(Guid value)
        {
            return (ulong)value.GetHashCode();
        }

        private static void MergeHash(ref ulong hash1, ulong hash2)
        {
            hash1 ^= hash2;
        }
    }
}
