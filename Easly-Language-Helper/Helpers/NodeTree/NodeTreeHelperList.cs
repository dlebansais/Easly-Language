namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

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
}
