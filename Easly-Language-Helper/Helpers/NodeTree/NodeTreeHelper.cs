using BaseNode;
using Easly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace BaseNodeHelper
{
    public static class NodeTreeHelperChild
    {
        public static bool IsChildNodeProperty(INode node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            return IsChildNodeProperty(node.GetType(), propertyName, out childNodeType);
        }

        public static bool IsChildNodeProperty(Type nodeType, string propertyName, out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
                return false;

            childNodeType = PropertyType;
            return true;
        }

        public static bool IsChildNode(INode node, string propertyName, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
                return false;

            if (Property.GetValue(node) != childNode)
                return false;

            return true;
        }

        public static void GetChildNode(INode node, string propertyName, out INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(Property.PropertyType));

            childNode = Property.GetValue(node) as INode;
        }

        public static Type ChildInterfaceType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type InterfaceType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static void SetChildNode(INode node, string propertyName, INode newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(Property.PropertyType));

            Type ChildNodeType = newChildNode.GetType();
            Debug.Assert(ChildNodeType.GetInterface(Property.PropertyType.FullName) != null);

            Property.SetValue(node, newChildNode);
        }
    }

    public static class NodeTreeHelperOptional
    {
        public static bool IsOptionalChildNodeProperty(INode node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            return IsOptionalChildNodeProperty(node.GetType(), propertyName, out childNodeType);
        }

        public static bool IsOptionalChildNodeProperty(Type nodeType, string propertyName, out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsOptionalReferenceType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);
            childNodeType = GenericArguments[0];

            return NodeTreeHelper.IsNodeInterfaceType(childNodeType);
        }

        public static bool IsOptionalChildNode(INode node, string propertyName, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsOptionalReferenceType(PropertyType))
                return false;

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);
            if (!Optional.IsAssigned)
                return false;

            INode NodeItem = Optional.Item as INode;
            Debug.Assert(NodeItem != null);

            if (NodeItem != childNode)
                return false;

            return true;
        }

        public static void GetChildNode(INode node, string propertyName, out bool isAssigned, out INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            isAssigned = false;
            childNode = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            isAssigned = Optional.IsAssigned;

            if (Optional.HasItem)
                childNode = Optional.Item as INode;
            else
                childNode = null;

            Debug.Assert(!isAssigned || childNode != null);
        }

        public static void GetChildNode(IOptionalReference optional, out bool isAssigned, out INode childNode)
        {
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            isAssigned = false;
            childNode = null;

            isAssigned = optional.IsAssigned;

            if (optional.HasItem)
                childNode = optional.Item as INode;
            else
                childNode = null;

            Debug.Assert(!isAssigned || childNode != null);
        }

        public static Type OptionalChildInterfaceType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static Type OptionalChildInterfaceType(IOptionalReference optional)
        {
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            Type OptionalType = optional.GetType();

            Debug.Assert(OptionalType.IsGenericType);
            Type[] GenericArguments = OptionalType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static IOptionalReference GetOptionalReference(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(Property.PropertyType));

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            return Optional;
        }

        public static void SetOptionalReference(INode node, string propertyName, IOptionalReference optional)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(Property.PropertyType));

            Property.SetValue(node, optional);
        }

        public static void SetOptionalChildNode(INode node, string propertyName, INode newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type ChildNodeType = newChildNode.GetType();

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(ChildNodeType.GetInterface(InterfaceType.FullName) != null);

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            PropertyInfo ItemProperty = Optional.GetType().GetProperty(nameof(IOptionalReference<INode>.Item));
            ItemProperty.SetValue(Optional, newChildNode);
            Optional.Assign();
        }

        public static void ClearOptionalChildNode(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            Optional.Clear();

            Debug.Assert(!Optional.HasItem);
        }

        public static bool IsChildNodeAssigned(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            return Optional.IsAssigned;
        }

        public static void AssignChildNode(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Assign();
        }

        public static void UnassignChildNode(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Unassign();
        }
    }

    public static class NodeTreeHelperList
    {
        public static bool IsNodeListProperty(INode node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();

            if (!IsNodeListProperty(NodeType, propertyName, out childNodeType))
                return false;

            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            object Collection = Property.GetValue(node);
            Debug.Assert(Collection == null || Collection is IList);

            return true;
        }

        public static bool IsNodeListProperty(Type nodeType, string propertyName, out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsNodeListType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);
            childNodeType = GenericArguments[0];

            return NodeTreeHelper.IsNodeInterfaceType(childNodeType);
        }

        public static void GetChildNodeList(INode node, string propertyName, out IReadOnlyList<INode> childNodeList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            if (Collection == null)
                childNodeList = null;
            else
            {
                List<INode> NodeList = new List<INode>();
                foreach (object Item in Collection)
                {
                    INode NodeItem = Item as INode;
                    NodeList.Add(NodeItem);
                }

                childNodeList = NodeList.AsReadOnly();
            }
        }

        public static void ClearChildNodeList(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Collection.Clear();
        }

        public static bool IsListChildNode(INode node, string propertyName, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Debug.Assert(index < Collection.Count);
            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = Collection[index] as INode;
            Debug.Assert(NodeItem != null);

            return NodeItem == childNode;
        }

        public static Type ListInterfaceType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = Property.PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static bool GetLastListIndex(INode node, string propertyName, out int lastIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            lastIndex = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            lastIndex = Collection.Count;
            return true;
        }

        public static void SetChildNodeList(INode node, string propertyName, IList childNodeList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNodeList == null) throw new ArgumentNullException(nameof(childNodeList));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));
            Debug.Assert(PropertyType.IsAssignableFrom(childNodeList.GetType()));

            Property.SetValue(node, childNodeList);
        }

        public static void InsertIntoList(INode node, string propertyName, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Debug.Assert(index <= Collection.Count);
            if (index > Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection.Insert(index, childNode);
        }

        public static void RemoveFromList(INode node, string propertyName, int index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Debug.Assert(index < Collection.Count);
            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection.RemoveAt(index);
        }

        public static void ReplaceNode(INode node, string propertyName, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection[index] = childNode;
        }

        public static void MoveNode(INode node, string propertyName, int index, int direction)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Debug.Assert(index < Collection.Count);
            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));
            Debug.Assert(index + direction < Collection.Count);
            if (index + direction >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(direction));

            INode ChildNode = Collection[index] as INode;
            Debug.Assert(ChildNode != null);

            Collection.RemoveAt(index);
            Collection.Insert(index + direction, ChildNode);
        }
    }

    public static class NodeTreeHelperBlockList
    {
        public static bool IsBlockListProperty(INode node, string propertyName, out Type childInterfaceType, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            if (!IsBlockListProperty(NodeType, propertyName, out childInterfaceType, out childNodeType))
                return false;

            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            object Collection = Property.GetValue(node);
            Debug.Assert(Collection == null || Collection as IBlockList != null);

            return true;
        }

        public static bool IsBlockListProperty(Type nodeType, string propertyName, out Type childInterfaceType, out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childInterfaceType = null;
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
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            Debug.Assert(childInterfaceType != null);
            Debug.Assert(childInterfaceType.IsInterface);

            childNodeType = GenericArguments[1];
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);

            return true;
        }

        public static void GetBlockType(IBlock block, out Type childInterfaceType, out Type childNodeType)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Type BlockType = block.GetType();
            Debug.Assert(BlockType.IsGenericType);

            Type[] GenericArguments = BlockType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            Debug.Assert(childInterfaceType != null);
            Debug.Assert(childInterfaceType.IsInterface);

            childNodeType = GenericArguments[1];
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);
        }

        public static IBlockList GetBlockList(INode node, string propertyName)
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

        public static void GetChildBlockList(INode node, string propertyName, out IReadOnlyList<INodeTreeBlock> childBlockList)
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

                List<INodeTreeBlock> Result = new List<INodeTreeBlock>();

                foreach (object Item in NodeBlockList)
                {
                    IBlock Block = Item as IBlock;
                    Debug.Assert(Block != null);

                    IPattern ReplicationPattern = Block.ReplicationPattern;
                    Debug.Assert(ReplicationPattern != null);
                    IIdentifier SourceIdentifier = Block.SourceIdentifier;
                    Debug.Assert(SourceIdentifier != null);
                    IList NodeList = Block.NodeList;
                    Debug.Assert(NodeList != null);
                    Debug.Assert(NodeList.Count > 0);

                    List<INode> ResultNodeList = new List<INode>();
                    foreach (INode ChildNode in NodeList)
                        ResultNodeList.Add(ChildNode);

                    Result.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ResultNodeList));
                }

                childBlockList = Result.AsReadOnly();
            }
        }

        public static void ClearChildBlockList(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            NodeBlockList.Clear();
        }

        public static Type BlockListInterfaceType(INode node, string propertyName)
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
            Debug.Assert(GenericArguments.Length == 2);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type BlockListItemType(INode node, string propertyName)
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
            Debug.Assert(GenericArguments.Length == 2);

            Type ItemType = GenericArguments[1];
            Debug.Assert(ItemType != null);
            Debug.Assert(!ItemType.IsInterface);

            return ItemType;
        }

        public static Type BlockListBlockType(INode node, string propertyName)
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

            PropertyInfo NodeListProperty = PropertyType.GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList));
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

        public static bool GetLastBlockIndex(INode node, string propertyName, out int blockIndex)
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

        public static bool GetLastBlockChildIndex(INode node, string propertyName, int blockIndex, out int index)
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

        public static bool IsBlockChildNode(INode node, string propertyName, int blockIndex, int index, INode childNode)
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

        public static bool IsChildNode(IBlock block, int index, INode childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = NodeList[index] as INode;
            Debug.Assert(NodeItem != null);

            if (NodeItem != childNode)
                return false;

            return true;
        }

        public static void GetChildBlock(INode node, string propertyName, int blockIndex, out IBlock childBlock)
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

        public static void GetChildNode(INode node, string propertyName, int blockIndex, int index, out INode childNode)
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

            INode NodeItem = NodeList[index] as INode;
            Debug.Assert(NodeItem != null);

            childNode = NodeItem;
        }

        public static void GetChildNode(IBlock block, int index, out INode childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = NodeList[index] as INode;
            Debug.Assert(NodeItem != null);

            childNode = NodeItem;
        }

        public static void SetBlockList(INode node, string propertyName, IBlockList blockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            if (!Property.PropertyType.IsAssignableFrom(blockList.GetType())) throw new ArgumentException(nameof(blockList));

            Property.SetValue(node, blockList);
        }

        public static bool IsBlockPatternNode(INode node, string propertyName, int blockIndex, IPattern replicationPattern)
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

        public static bool IsPatternNode(IBlock block, IPattern replicationPattern)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            return replicationPattern == block.ReplicationPattern;
        }

        public static string GetPattern(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            IPattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            return NodeTreeHelper.GetString(ReplicationPattern, nameof(IPattern.Text));
        }

        public static void SetPattern(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            IPattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            NodeTreeHelper.SetString(ReplicationPattern, nameof(IPattern.Text), text);
        }

        public static bool IsBlockSourceNode(INode node, string propertyName, int blockIndex, IIdentifier sourceIdentifier)
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

        public static bool IsSourceNode(IBlock block, IIdentifier sourceIdentifier)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return sourceIdentifier == block.SourceIdentifier;
        }

        public static string GetSource(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            IIdentifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            return NodeTreeHelper.GetString(SourceIdentifier, nameof(IIdentifier.Text));
        }

        public static void SetSource(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            IIdentifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            NodeTreeHelper.SetString(SourceIdentifier, nameof(IIdentifier.Text), text);
        }

        public static void SetReplication(IBlock block, ReplicationStatus replication)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            block.GetType().GetProperty(nameof(IBlock.Replication)).SetValue(block, replication);
        }

        public static void InsertIntoBlock(INode node, string propertyName, int blockIndex, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

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

            Debug.Assert(index <= NodeList.Count);
            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        public static void InsertIntoBlock(IBlock block, int index, INode childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index <= NodeList.Count);
            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        public static void RemoveFromBlock(INode node, string propertyName, int blockIndex, int index, out bool isBlockRemoved)
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

            NodeList.RemoveAt(index);

            if (NodeList.Count == 0)
            {
                NodeBlockList.RemoveAt(blockIndex);
                isBlockRemoved = true;
            }
            else
                isBlockRemoved = false;
        }

        public static void ReplaceNode(INode node, string propertyName, int blockIndex, int index, INode newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

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

            NodeList[index] = newChildNode;
        }

        public static void ReplaceInBlock(IBlock block, int index, INode newChildNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            return (NodeBlockList.Count == 0);
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

        public static IBlock CreateBlock(INode node, string propertyName, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
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

        public static IBlock CreateBlock(IBlockList blockList, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return CreateBlock(blockList.GetType(), replication, replicationPattern, sourceIdentifier);
        }

        public static IBlock CreateBlock(Type propertyType, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            if (propertyType == null) throw new ArgumentNullException(nameof(propertyType));
            if (!propertyType.IsGenericType || (propertyType.GetGenericTypeDefinition() != typeof(IBlockList<,>) && propertyType.GetGenericTypeDefinition() != typeof(BlockList<,>))) throw new ArgumentException(nameof(propertyType));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type[] TypeArguments = propertyType.GetGenericArguments();

            Type BlockType = typeof(Block<,>).MakeGenericType(TypeArguments);
            IBlock NewBlock = BlockType.Assembly.CreateInstance(BlockType.FullName) as IBlock;
            Debug.Assert(NewBlock != null);

            IDocument EmptyComment = NodeHelper.CreateEmptyDocumentation();
            BlockType.GetProperty(nameof(INode.Documentation)).SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });
            IList NewNodeList = NodeListType.Assembly.CreateInstance(NodeListType.FullName) as IList;
            Debug.Assert(NewNodeList != null);

            BlockType.GetProperty(nameof(IBlock.Replication)).SetValue(NewBlock, replication);
            BlockType.GetProperty(nameof(IBlock.NodeList)).SetValue(NewBlock, NewNodeList);
            BlockType.GetProperty(nameof(IBlock.ReplicationPattern)).SetValue(NewBlock, replicationPattern);
            BlockType.GetProperty(nameof(IBlock.SourceIdentifier)).SetValue(NewBlock, sourceIdentifier);

            return NewBlock;
        }

        public static void InsertIntoBlockList(INode node, string propertyName, int blockIndex, IBlock childBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (childBlock == null) throw new ArgumentNullException(nameof(childBlock));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex <= NodeBlockList.Count);
            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            NodeBlockList.Insert(blockIndex, childBlock);
        }

        public static void RemoveFromBlockList(INode node, string propertyName, int blockIndex)
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

            NodeBlockList.RemoveAt(blockIndex);
        }

        public static void SplitBlock(INode node, string propertyName, int blockIndex, int index, IBlock newChildBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildBlock == null) throw new ArgumentNullException(nameof(newChildBlock));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex <= NodeBlockList.Count);
            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock CurrentBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(CurrentBlock != null);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList != null);
            Debug.Assert(CurrentNodeList.Count > 1);

            Debug.Assert(index < CurrentNodeList.Count);
            if (index >= CurrentNodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            IList NewNodeList = newChildBlock.NodeList;
            Debug.Assert(NewNodeList != null);
            Debug.Assert(NewNodeList.Count == 0);

            NodeBlockList.Insert(blockIndex, newChildBlock);

            for (int i = 0; i < index; i++)
            {
                INode ChildNode = CurrentNodeList[0] as INode;
                Debug.Assert(ChildNode != null);

                CurrentNodeList.RemoveAt(0);
                NewNodeList.Insert(i, ChildNode);
            }

            Debug.Assert(CurrentNodeList.Count > 0);
            Debug.Assert(NewNodeList.Count > 0);
        }

        public static void MergeBlocks(INode node, string propertyName, int blockIndex, out IBlock mergedBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex <= 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

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

            mergedBlock = NodeBlockList[blockIndex - 1] as IBlock;
            Debug.Assert(mergedBlock != null);

            IBlock CurrentBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(CurrentBlock != null);

            IList MergedNodeList = mergedBlock.NodeList;
            Debug.Assert(MergedNodeList != null);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList != null);

            for (int i = 0; MergedNodeList.Count > 0; i++)
            {
                INode ChildNode = MergedNodeList[0] as INode;
                Debug.Assert(ChildNode != null);

                CurrentNodeList.Insert(i, ChildNode);
                MergedNodeList.RemoveAt(0);
            }

            NodeBlockList.RemoveAt(blockIndex - 1);
        }

        public static void MoveNode(IBlock block, int index, int direction)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));
            Debug.Assert(index + direction < NodeList.Count);
            if (index + direction >= NodeList.Count) throw new ArgumentException(nameof(direction));

            INode ChildNode = NodeList[index] as INode;
            Debug.Assert(ChildNode != null);

            NodeList.RemoveAt(index);
            NodeList.Insert(index + direction, ChildNode);
        }

        public static void MoveBlock(INode node, string propertyName, int blockIndex, int direction)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (blockIndex + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

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
            Debug.Assert(blockIndex + direction < NodeBlockList.Count);
            if (blockIndex + direction >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(direction));

            IBlock MovedBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(MovedBlock != null);

            NodeBlockList.RemoveAt(blockIndex);
            NodeBlockList.Insert(blockIndex + direction, MovedBlock);
        }
    }

    public static class NodeTreeHelper
    {
        public static IList<string> EnumChildNodeProperties(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            return EnumChildNodeProperties(NodeType);
        }

        public static IList<string> EnumChildNodeProperties(Type nodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (nodeType.GetInterface(typeof(INode).FullName) == null) throw new ArgumentException(nameof(nodeType));

            if (GetBaseNodeAncestor(nodeType, out Type AncestorType))
                nodeType = AncestorType;

            IList<PropertyInfo> Properties = GetTypeProperties(nodeType);
            Debug.Assert(Properties != null);

            List<string> Result = new List<string>();
            foreach (PropertyInfo Property in Properties)
                Result.Add(Property.Name);

            Result.Sort();
            return Result;
        }

        private static bool GetBaseNodeAncestor(Type nodeType, out Type ancestorType)
        {
            ancestorType = null;
            bool Result = false;

            string BaseNodeNamespace = typeof(INode).FullName.Substring(0, typeof(INode).FullName.IndexOf(".") + 1);
            while (nodeType != typeof(object) && !nodeType.FullName.StartsWith(BaseNodeNamespace))
                nodeType = nodeType.BaseType;
            Debug.Assert(nodeType != typeof(object));

            if (nodeType != typeof(INode) && nodeType != typeof(Node))
            {
                ancestorType = nodeType;
                Result = true;
            }

            return Result;
        }

        // Exotic calls to get properties for interfaces? WTF Mikeysoft...
        private static IList<PropertyInfo> GetTypeProperties(Type type)
        {
            PropertyInfo[] Properties = type.GetProperties();
            List<PropertyInfo> Result = new List<PropertyInfo>(Properties);

            foreach (Type Interface in type.GetInterfaces())
            {
                PropertyInfo[] InterfaceProperties = Interface.GetProperties();

                foreach (PropertyInfo NewProperty in InterfaceProperties)
                {
                    bool AlreadyListed = false;
                    foreach (PropertyInfo ExistingProperty in Result)
                        if (NewProperty.Name == ExistingProperty.Name)
                        {
                            AlreadyListed = true;
                            break;
                        }

                    if (!AlreadyListed)
                        Result.Add(NewProperty);
                }
            }

            return Result;
        }

        // Exotic calls to get a property for interfaces? WTF Mikeysoft...
        public static PropertyInfo GetPropertyOf(Type type, string propertyName)
        {
            PropertyInfo Property = type.GetProperty(propertyName);
            if (Property != null)
                return Property;

            foreach (Type Interface in type.GetInterfaces())
            {
                PropertyInfo InterfaceProperty = Interface.GetProperty(propertyName);
                if (InterfaceProperty != null)
                    return InterfaceProperty;
            }

            return null;
        }

        public static bool IsNodeInterfaceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsInterface && type.GetInterface(typeof(INode).FullName) != null;
        }

        public static Type NodeTypeToInterfaceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (type.IsInterface || type.IsAbstract) throw new ArgumentException(nameof(type));

            Type[] Interfaces = type.GetInterfaces();
            foreach (Type InterfaceType in Interfaces)
                if (InterfaceType.Name == $"I{type.Name}")
                    return InterfaceType;

            return null;
        }

        public static Type InterfaceTypeToNodeType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!type.IsInterface) throw new ArgumentException(nameof(type));

            string FullName = type.FullName;
            string[] Prefixes = FullName.Split('.');
            Debug.Assert(Prefixes.Length > 0);

            int InterfaceIndex = 0;
            while (InterfaceIndex + 1 < Prefixes.Length && !Prefixes[InterfaceIndex].Contains("`") && !Prefixes[InterfaceIndex].Contains("["))
                InterfaceIndex++;

            string LastName = Prefixes[InterfaceIndex];
            Debug.Assert(LastName.Length > 1);
            Debug.Assert(LastName[0] == 'I');

            string NodeTypeName = LastName.Substring(1);

            string NodeTypeFullName = "";
            for (int i = 0; i < Prefixes.Length; i++)
            {
                if (i > 0)
                    NodeTypeFullName += ".";

                if (i != InterfaceIndex)
                    NodeTypeFullName += Prefixes[i];
                else
                    NodeTypeFullName += NodeTypeName;
            }

            Type Result = type.Assembly.GetType(NodeTypeFullName);
            Debug.Assert(Result != null);

            return Result;
        }

        public static bool IsOptionalReferenceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IOptionalReference<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            return IsNodeInterfaceType(GenericArguments[0]);
        }

        public static bool IsNodeListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            return IsNodeInterfaceType(GenericArguments[0]);
        }

        public static bool IsBlockListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlockList<,>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            return IsNodeInterfaceType(GenericArguments[0]);
        }

        public static bool IsBlockType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlock<,>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            return IsNodeInterfaceType(GenericArguments[0]);
        }

        public static bool IsTextNode(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));

            return (Property != null);
        }

        public static string GetString(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType == typeof(string));

            string Text = Property.GetValue(node) as string;

            return Text;
        }

        public static void SetString(INode node, string propertyName, string text)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType == typeof(string));

            Property.SetValue(node, text);
        }

        private static void GetEnumMinMax(PropertyInfo property, out int min, out int max)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            Type PropertyType = property.PropertyType;
            if (!PropertyType.IsEnum && PropertyType != typeof(bool)) throw new ArgumentException(nameof(property));

            if (PropertyType == typeof(bool))
            {
                max = 1;
                min = 0;
            }
            else
            {
                Array Values = property.PropertyType.GetEnumValues();

                max = int.MinValue;
                min = int.MaxValue;
                foreach (int Value in Values)
                {
                    if (max < Value)
                        max = Value;
                    if (min > Value)
                        min = Value;
                }
            }
        }

        public static int GetEnumValue(INode node, string propertyName)
        {
            return GetEnumValueAndRange(node, propertyName, out int Min, out int Max);
        }

        public static int GetEnumValueAndRange(INode node, string propertyName, out int min, out int max)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out min, out max);
            int Result;

            if (PropertyType == typeof(bool))
            {
                bool BoolValue = (bool)Property.GetValue(node);
                Result = BoolValue ? 1 : 0;
            }
            else
                Result = (int)Property.GetValue(node);

            Debug.Assert(min <= Result && Result <= max);

            return Result;
        }

        public static void SetEnumValue(INode node, string propertyName, int value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out int Min, out int Max);
            Debug.Assert(Min <= value && value <= Max);

            if (PropertyType == typeof(bool))
                Property.SetValue(node, value == 1 ? true : false);
            else
                Property.SetValue(node, value);
        }

        public static void GetEnumRange(Type nodeType, string propertyName, out int min, out int max)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out min, out max);
        }

        public static Guid GetGuid(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Guid));

            Guid Result = (Guid)Property.GetValue(node);

            return Result;
        }

        public static string GetCommentText(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Debug.Assert(node.Documentation != null);

            return GetCommentText(node.Documentation);
        }

        public static string GetCommentText(IDocument documentation)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            string Text = documentation.Comment;
            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetCommentText(INode node, string text)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Debug.Assert(node.Documentation != null);

            SetCommentText(node.Documentation, text);
        }

        public static void SetCommentText(IDocument documentation, string text)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            Type DocumentationType = documentation.GetType();
            PropertyInfo CommentProperty = DocumentationType.GetProperty(nameof(IDocument.Comment));
            CommentProperty.SetValue(documentation, text);
        }

        public static void GetOptionalNodes(INode node, out IDictionary<string, IOptionalReference> optionalNodesTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            optionalNodesTable = new Dictionary<string, IOptionalReference>();

            Type NodeType = node.GetType();
            IList<PropertyInfo> Properties = GetTypeProperties(NodeType);
            Debug.Assert(Properties != null);

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (IsOptionalReferenceType(PropertyType))
                {
                    IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
                    Debug.Assert(Optional != null);

                    optionalNodesTable.Add(PropertyName, Optional);
                }
            }
        }

        public static void GetArgumentBlocks(INode node, out IDictionary<string, IBlockList<IArgument, Argument>> argumentBlocksTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            argumentBlocksTable = new Dictionary<string, IBlockList<IArgument, Argument>>();

            Type NodeType = node.GetType();
            IList<PropertyInfo> Properties = GetTypeProperties(NodeType);
            Debug.Assert(Properties != null);

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (IsBlockListType(PropertyType))
                {
                    Debug.Assert(PropertyType.IsGenericType);
                    Type[] GenericArguments = PropertyType.GetGenericArguments();
                    Debug.Assert(GenericArguments != null);
                    Debug.Assert(GenericArguments.Length == 2);

                    if (GenericArguments[0] == typeof(IArgument))
                    {
                        IBlockList<IArgument, Argument> ArgumentBlocks = Property.GetValue(node) as IBlockList<IArgument, Argument>;
                        Debug.Assert(ArgumentBlocks != null);

                        argumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                    }
                }
            }
        }

        public static bool IsDocumentProperty(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return IsDocumentProperty(NodeType, propertyName);
        }

        public static bool IsDocumentProperty(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            return PropertyType == typeof(IDocument);
        }

        public static void SetDocumentation(INode node, IDocument document)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (document == null) throw new ArgumentNullException(nameof(document));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(INode.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            Property.SetValue(node, document);
        }

        public static void CopyDocumentation(INode sourceNode, INode destinationNode, bool cloneCommentGuid)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();

            if (SourceNodeType != DestinationNodeType) throw new ArgumentException();

            PropertyInfo Property = SourceNodeType.GetProperty(nameof(INode.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            Guid GuidCopy = cloneCommentGuid ? sourceNode.Documentation.Uuid : Guid.NewGuid();
            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceNode.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationNode, DocumentCopy);
        }

        public static void CopyDocumentation(IBlock sourceBlock, IBlock destinationBlock, bool cloneCommentGuid)
        {
            if (sourceBlock == null) throw new ArgumentNullException(nameof(sourceBlock));
            if (destinationBlock == null) throw new ArgumentNullException(nameof(destinationBlock));

            Type SourceBlockType = sourceBlock.GetType();
            Type DestinationBlockType = sourceBlock.GetType();
            Debug.Assert(SourceBlockType == DestinationBlockType);

            PropertyInfo Property = SourceBlockType.GetProperty(nameof(IBlock.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            Guid GuidCopy = cloneCommentGuid ? sourceBlock.Documentation.Uuid : Guid.NewGuid();
            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlock.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationBlock, DocumentCopy);
        }

        public static void CopyDocumentation(IBlockList sourceBlockList, IBlockList destinationBlockList, bool cloneCommentGuid)
        {
            if (sourceBlockList == null) throw new ArgumentNullException(nameof(sourceBlockList));
            if (destinationBlockList == null) throw new ArgumentNullException(nameof(destinationBlockList));

            Type SourceBlockType = sourceBlockList.GetType();
            Type DestinationBlockType = sourceBlockList.GetType();
            Debug.Assert(SourceBlockType == DestinationBlockType);

            PropertyInfo Property = SourceBlockType.GetProperty(nameof(IBlock.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            Guid GuidCopy = cloneCommentGuid ? sourceBlockList.Documentation.Uuid : Guid.NewGuid();
            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlockList.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationBlockList, DocumentCopy);
        }

        public static bool IsBooleanProperty(INode parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(bool));
        }

        public static bool IsBooleanProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(bool));
        }

        public static void SetBooleanProperty(INode parentNode, string propertyName, bool value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyBooleanProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            CopyValueProperty<bool>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsStringProperty(INode parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(string));
        }

        public static bool IsStringProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(string));
        }

        public static void SetStringProperty(INode parentNode, string propertyName, string value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyStringProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            CopyValueProperty<string>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsGuidProperty(INode parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(Guid));
        }

        public static bool IsGuidProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(Guid));
        }

        public static void SetGuidProperty(INode parentNode, string propertyName, Guid value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyGuidProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            CopyValueProperty<Guid>(sourceNode, destinationNode, propertyName);
        }

        private static bool IsValueProperty(INode node, string propertyName, Type type)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Type NodeType = node.GetType();
            return IsValueProperty(NodeType, propertyName, type);
        }

        private static bool IsValueProperty(Type nodeType, string propertyName, Type type)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (type == null) throw new ArgumentNullException(nameof(type));

            PropertyInfo Property = GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (PropertyType != type)
                return false;

            return true;
        }

        private static void SetValueProperty<T>(INode node, string propertyName, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (value == null) throw new ArgumentNullException(nameof(value));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(node, value);
        }

        private static void CopyValueProperty<T>(INode sourceNode, INode destinationNode, string propertyName)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }

        public static bool IsEnumProperty(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return IsEnumProperty(NodeType, propertyName);
        }

        public static bool IsEnumProperty(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            return PropertyType.IsEnum;
        }

        public static void SetEnumProperty(INode node, string propertyName, object value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum);

            Property.SetValue(node, value);
        }

        public static bool IsAssignable(INode node, string propertyName, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type NodeType = childNode.GetType();
            Type PropertyType = Property.PropertyType;

            Type AssignedType = null;

            if (IsNodeInterfaceType(PropertyType))
                AssignedType = PropertyType;

            else if (PropertyType.IsGenericType)
            {
                Type[] GenericArguments = PropertyType.GetGenericArguments();
                Debug.Assert(GenericArguments != null);

                if (IsOptionalReferenceType(PropertyType))
                {
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }

                else if (IsNodeListType(PropertyType))
                {
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }

                else if (IsBlockListType(PropertyType))
                {
                    Debug.Assert(GenericArguments.Length == 2);
                    AssignedType = GenericArguments[0];
                }
            }

            if (AssignedType == null)
                return false;

            if (!AssignedType.IsAssignableFrom(NodeType))
                return false;

            return true;
        }

        public static void CopyEnumProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsEnum) throw new ArgumentException(nameof(propertyName));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }
    }
}
