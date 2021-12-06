namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to manipulate a tree of nodes.
    /// </summary>
    public static partial class NodeTreeHelper
    {
        /// <summary>
        /// Gets the content of a string property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The string content.</returns>
        public static string GetString(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);

            if (Property.PropertyType != typeof(string))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a string property");

            string Text = SafeType.GetPropertyValue<string>(Property, Node);

            return Text;
        }

        /// <summary>
        /// Sets the content of a string property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="text">The string content to set.</param>
        public static void SetString(Node node, string propertyName, string text)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);
            Contract.RequireNotNull(text, out string Text);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);

            if (Property.PropertyType != typeof(string))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a string property");

            Property.SetValue(Node, Text);
        }

        /// <summary>
        /// Gets the content of an enum property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The enum content.</returns>
        public static int GetEnumValue(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return GetEnumValueAndRange(Node, PropertyName, out int Min, out int Max);
        }

        /// <summary>
        /// Gets the content of an enum property of a node, as well as the min and max value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="min">The min value upon return.</param>
        /// <param name="max">The max value upon return.</param>
        /// <returns>The enum content.</returns>
        public static int GetEnumValueAndRange(Node node, string propertyName, out int min, out int max)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            if (!IsDiscretePropertyType(PropertyType))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a discrete value property (enumeration or boolean)");

            GetEnumMinMax(PropertyType, out min, out max);
            int Result;

            if (PropertyType == typeof(bool))
            {
                bool BoolValue = (bool)Property.GetValue(Node)!;

                Result = BoolValue ? 1 : 0;
            }
            else
                Result = (int)Property.GetValue(Node)!;

            Debug.Assert(min <= Result && Result <= max);

            return Result;
        }

        /// <summary>
        /// Sets the content of an enum property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The enum content.</param>
        public static void SetEnumValue(Node node, string propertyName, int value)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            if (!IsDiscretePropertyType(PropertyType))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a discrete value property (enumeration or boolean)");

            GetEnumMinMax(PropertyType, out int Min, out int Max);

            if (Min < value || value > Max)
                throw new ArgumentOutOfRangeException(nameof(value));

            if (PropertyType == typeof(bool))
                Property.SetValue(Node, value == 1 ? true : false);
            else
                Property.SetValue(Node, value);
        }

        /// <summary>
        /// Gets the range of values of an enum property of a node type.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="min">The min value upon return.</param>
        /// <param name="max">The max value upon return.</param>
        public static void GetEnumRange(Type nodeType, string propertyName, out int min, out int max)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            if (!IsDiscretePropertyType(PropertyType))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a discrete value property (enumeration or boolean)");

            GetEnumMinMax(PropertyType, out min, out max);
        }

        /// <summary>
        /// Gets the value of a <see cref="Guid"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property value.</returns>
        public static Guid GetGuid(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            if (PropertyType != typeof(Guid))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a {typeof(Guid)} property");

            Guid Result = (Guid)Property.GetValue(Node)!;

            return Result;
        }

        /// <summary>
        /// Gets the comment text of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The comment text.</returns>
        public static string GetCommentText(Node node)
        {
            Contract.RequireNotNull(node, out Node Node);

            return GetCommentText(Node.Documentation);
        }

        /// <summary>
        /// Gets the comment text of a node.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <returns>The comment text.</returns>
        public static string GetCommentText(Document documentation)
        {
            Contract.RequireNotNull(documentation, out Document Documentation);

            return Documentation.Comment;
        }

        /// <summary>
        /// Sets the comment text of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="text">The comment text.</param>
        public static void SetCommentText(Node node, string text)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(text, out string Text);

            SetCommentText(Node.Documentation, Text);
        }

        /// <summary>
        /// Sets the comment text of a node.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="text">The comment text.</param>
        public static void SetCommentText(Document documentation, string text)
        {
            Contract.RequireNotNull(documentation, out Document Documentation);
            Contract.RequireNotNull(text, out string Text);

            Type DocumentationType = Documentation.GetType();
            PropertyInfo CommentProperty = SafeType.GetProperty(DocumentationType, nameof(Document.Comment));

            CommentProperty.SetValue(Documentation, Text);
        }

        /// <summary>
        /// Checks whether the given property of a node is a documentation object.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the given property of a node is a documentation object; otherwise, false.</returns>
        public static bool IsDocumentProperty(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            return IsDocumentProperty(NodeType, PropertyName);
        }

        /// <summary>
        /// Checks whether the given property of a node type is a documentation object.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the given property of a node type is a documentation object; otherwise, false.</returns>
        public static bool IsDocumentProperty(Type nodeType, string propertyName)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            if (!SafeType.CheckAndGetPropertyOf(NodeType, PropertyName, out PropertyInfo Property))
                return false;

            Type PropertyType = Property.PropertyType;
            return PropertyType == typeof(Document);
        }

        /// <summary>
        /// Sets the documentation of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="document">The documentation.</param>
        public static void SetDocumentation(Node node, Document document)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(document, out Document Document);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, nameof(Node.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Property.SetValue(Node, Document);
        }

        /// <summary>
        /// Copy the documentation from a node to another.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="cloneCommentGuid">True if Uuid of the destination documentation is to be the same as the source, or should be a new Uuid.</param>
        public static void CopyDocumentation(Node sourceNode, Node destinationNode, bool cloneCommentGuid)
        {
            Contract.RequireNotNull(sourceNode, out Node SourceNode);
            Contract.RequireNotNull(destinationNode, out Node DestinationNode);

            Type SourceNodeType = SourceNode.GetType();
            Type DestinationNodeType = DestinationNode.GetType();

            if (SourceNodeType != DestinationNodeType)
                throw new ArgumentException($"{nameof(sourceNode)} and {nameof(destinationNode)} must be of the same type");

            PropertyInfo Property = SafeType.GetProperty(SourceNodeType, nameof(Node.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? SourceNode.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(SourceNode.Documentation.Comment, GuidCopy);
            Property.SetValue(DestinationNode, DocumentCopy);
        }

        /// <summary>
        /// Copy a documentation from a block to another.
        /// </summary>
        /// <param name="sourceBlock">The source block.</param>
        /// <param name="destinationBlock">The destination block.</param>
        /// <param name="cloneCommentGuid">True if Uuid of the destination documentation is to be the same as the source, or should be a new Uuid.</param>
        public static void CopyDocumentation(IBlock sourceBlock, IBlock destinationBlock, bool cloneCommentGuid)
        {
            Contract.RequireNotNull(sourceBlock, out IBlock SourceBlock);
            Contract.RequireNotNull(destinationBlock, out IBlock DestinationBlock);

            Type SourceBlockType = SourceBlock.GetType();
            Type DestinationBlockType = DestinationBlock.GetType();

            if (SourceBlockType != DestinationBlockType)
                throw new ArgumentException($"{nameof(sourceBlock)} and {nameof(destinationBlock)} must be of the same type");

            PropertyInfo Property = SafeType.GetProperty(SourceBlockType, nameof(IBlock.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? SourceBlock.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(SourceBlock.Documentation.Comment, GuidCopy);
            Property.SetValue(DestinationBlock, DocumentCopy);
        }

        /// <summary>
        /// Copy a documentation from a block list to another.
        /// </summary>
        /// <param name="sourceBlockList">The source block list.</param>
        /// <param name="destinationBlockList">The destination block list.</param>
        /// <param name="cloneCommentGuid">True if Uuid of the destination documentation is to be the same as the source, or should be a new Uuid.</param>
        public static void CopyDocumentation(IBlockList sourceBlockList, IBlockList destinationBlockList, bool cloneCommentGuid)
        {
            Contract.RequireNotNull(sourceBlockList, out IBlockList SourceBlockList);
            Contract.RequireNotNull(destinationBlockList, out IBlockList DestinationBlockList);

            Type SourceBlockListType = SourceBlockList.GetType();
            Type DestinationBlockListType = DestinationBlockList.GetType();

            if (SourceBlockListType != DestinationBlockListType)
                throw new ArgumentException($"{nameof(sourceBlockList)} and {nameof(destinationBlockList)} must be of the same type");

            PropertyInfo Property = SafeType.GetProperty(SourceBlockListType, nameof(IBlock.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? SourceBlockList.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(SourceBlockList.Documentation.Comment, GuidCopy);
            Property.SetValue(DestinationBlockList, DocumentCopy);
        }

        /// <summary>
        /// Checks whether the property of a node is a <see cref="bool"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is a <see cref="bool"/>; otherwise, false.</returns>
        public static bool IsBooleanProperty(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return IsValueProperty(Node, PropertyName, typeof(bool));
        }

        /// <summary>
        /// Checks whether the property of a node type is a <see cref="bool"/>.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is a <see cref="bool"/>; otherwise, false.</returns>
        public static bool IsBooleanProperty(Type nodeType, string propertyName)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return IsValueProperty(NodeType, PropertyName, typeof(bool));
        }

        /// <summary>
        /// Sets the value of a <see cref="bool"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetBooleanProperty(Node node, string propertyName, bool value)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            SetValueProperty(Node, PropertyName, value);
        }

        /// <summary>
        /// Copy the <see cref="bool"/> value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyBooleanProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            Contract.RequireNotNull(sourceNode, out Node SourceNode);
            Contract.RequireNotNull(destinationNode, out Node DestinationNode);

            CopyValueProperty<bool>(SourceNode, DestinationNode, propertyName);
        }

        /// <summary>
        /// Checks whether the property of a node is a <see cref="string"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is a <see cref="string"/>; otherwise, false.</returns>
        public static bool IsStringProperty(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return IsValueProperty(Node, PropertyName, typeof(string));
        }

        /// <summary>
        /// Checks whether the property of a node type is a <see cref="string"/>.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is a <see cref="string"/>; otherwise, false.</returns>
        public static bool IsStringProperty(Type nodeType, string propertyName)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return IsValueProperty(NodeType, PropertyName, typeof(string));
        }

        /// <summary>
        /// Sets the value of a <see cref="string"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetStringProperty(Node node, string propertyName, string value)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            SetValueProperty(Node, PropertyName, value);
        }

        /// <summary>
        /// Copy the <see cref="string"/> value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyStringProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            Contract.RequireNotNull(sourceNode, out Node SourceNode);
            Contract.RequireNotNull(destinationNode, out Node DestinationNode);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            CopyValueProperty<string>(SourceNode, DestinationNode, PropertyName);
        }

        /// <summary>
        /// Checks whether the property of a node is a <see cref="Guid"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is a <see cref="Guid"/>; otherwise, false.</returns>
        public static bool IsGuidProperty(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return IsValueProperty(Node, PropertyName, typeof(Guid));
        }

        /// <summary>
        /// Checks whether the property of a node type is a <see cref="Guid"/>.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is a <see cref="Guid"/>; otherwise, false.</returns>
        public static bool IsGuidProperty(Type nodeType, string propertyName)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            return IsValueProperty(NodeType, PropertyName, typeof(Guid));
        }

        /// <summary>
        /// Sets the value of a <see cref="Guid"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetGuidProperty(Node node, string propertyName, Guid value)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            SetValueProperty(Node, PropertyName, value);
        }

        /// <summary>
        /// Copy the <see cref="Guid"/> value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyGuidProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            Contract.RequireNotNull(sourceNode, out Node SourceNode);
            Contract.RequireNotNull(destinationNode, out Node DestinationNode);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            CopyValueProperty<Guid>(SourceNode, DestinationNode, PropertyName);
        }

        /// <summary>
        /// Checks whether the property of a node is an enum.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is an enum; otherwise, false.</returns>
        public static bool IsEnumProperty(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            return IsEnumProperty(NodeType, PropertyName);
        }

        /// <summary>
        /// Checks whether the property of a node type is an enum.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is an enum; otherwise, false.</returns>
        public static bool IsEnumProperty(Type nodeType, string propertyName)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            return PropertyType.IsEnum;
        }

        /// <summary>
        /// Sets the value of an enum property of a node.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetEnumProperty<TEnum>(Node node, string propertyName, TEnum value)
            where TEnum : System.Enum
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            if (!PropertyType.IsEnum)
                throw new ArgumentException($"{nameof(propertyName)} must be the name of an enumeration property");

            Property.SetValue(Node, value);
        }

        /// <summary>
        /// Copy the enum value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyEnumProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            Contract.RequireNotNull(sourceNode, out Node SourceNode);
            Contract.RequireNotNull(destinationNode, out Node DestinationNode);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type SourceNodeType = SourceNode.GetType();
            Type DestinationNodeType = DestinationNode.GetType();

            if (SourceNodeType != DestinationNodeType)
                throw new ArgumentException($"{nameof(sourceNode)} and {nameof(destinationNode)} must be of the same type");

            PropertyInfo Property = SafeType.GetProperty(SourceNodeType, PropertyName);
            Type PropertyType = Property.PropertyType;

            if (!PropertyType.IsEnum)
                throw new ArgumentException($"{nameof(propertyName)} must be the name of an enumeration property");

            Property.SetValue(DestinationNode, Property.GetValue(SourceNode));
        }

        private static bool IsDiscretePropertyType(Type propertyType)
        {
            return propertyType.IsEnum || propertyType == typeof(bool);
        }

        private static void GetEnumMinMax(Type propertyType, out int min, out int max)
        {
            Debug.Assert(IsDiscretePropertyType(propertyType));

            if (propertyType == typeof(bool))
            {
                max = 1;
                min = 0;
            }
            else
            {
                Array Values = propertyType.GetEnumValues();

                max = int.MinValue;
                min = int.MaxValue;
                foreach (object? Value in Values)
                {
                    int ValueInt = (int)Value!;

                    if (max < ValueInt)
                        max = ValueInt;
                    if (min > ValueInt)
                        min = ValueInt;
                }
            }
        }

        private static bool IsValueProperty(Node node, string propertyName, Type type)
        {
            Type NodeType = node.GetType();
            return IsValueProperty(NodeType, propertyName, type);
        }

        private static bool IsValueProperty(Type nodeType, string propertyName, Type type)
        {
            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);
            Type PropertyType = Property.PropertyType;

            if (PropertyType != type)
                return false;

            return true;
        }

        private static void SetValueProperty<T>(Node node, string propertyName, T value)
        {
            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);
            Type PropertyType = Property.PropertyType;

            if (PropertyType != typeof(T))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of type {typeof(T)}");

            Property.SetValue(node, value);
        }

        private static void CopyValueProperty<T>(Node sourceNode, Node destinationNode, string propertyName)
        {
            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = destinationNode.GetType();

            if (SourceNodeType != DestinationNodeType)
                throw new ArgumentException($"{nameof(sourceNode)} and {nameof(destinationNode)} must be of the same type");

            PropertyInfo Property = SafeType.GetProperty(SourceNodeType, propertyName);
            Type PropertyType = Property.PropertyType;

            if (PropertyType != typeof(T))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of type {typeof(T)}");

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }
    }
}
