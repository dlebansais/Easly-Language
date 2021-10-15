namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);
            Debug.Assert(Property.PropertyType == typeof(string));

            string Text = SafeType.GetPropertyValue<string>(Property, node);

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);
            Debug.Assert(Property.PropertyType == typeof(string));

            Property.SetValue(node, text);
        }

        /// <summary>
        /// Gets the content of an enum property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The enum content.</returns>
        public static int GetEnumValue(Node node, string propertyName)
        {
            return GetEnumValueAndRange(node, propertyName, out int Min, out int Max);
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out min, out max);
            int? Result;

            if (PropertyType == typeof(bool))
            {
                bool? BoolValue = Property.GetValue(node) as bool?;
                Debug.Assert(BoolValue != null);

                if (BoolValue == null)
                    return 0;

                Result = BoolValue.Value ? 1 : 0;
            }
            else
                Result = Property.GetValue(node) as int?;

            if (Result == null)
            {
                min = 0;
                max = 0;
                return 0;
            }

            Debug.Assert(min <= Result && Result <= max);

            return Result.Value;
        }

        /// <summary>
        /// Sets the content of an enum property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The enum content.</param>
        public static void SetEnumValue(Node node, string propertyName, int value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out int Min, out int Max);
            Debug.Assert(Min <= value && value <= Max);

            if (PropertyType == typeof(bool))
                Property.SetValue(node, value == 1 ? true : false);
            else
                Property.SetValue(node, value);
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
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);
            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out min, out max);
        }

        /// <summary>
        /// Gets the value of a <see cref="Guid"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property value.</returns>
        public static Guid GetGuid(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Guid));

            Guid? Result = Property.GetValue(node) as Guid?;
            Debug.Assert(Result != null);

            if (Result == null)
                return Guid.Empty;

            return Result.Value;
        }

        /// <summary>
        /// Gets the comment text of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The comment text.</returns>
        public static string GetCommentText(Node node)
        {
            return GetCommentText(node.Documentation);
        }

        /// <summary>
        /// Gets the comment text of a node.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <returns>The comment text.</returns>
        public static string GetCommentText(Document documentation)
        {
            return documentation.Comment;
        }

        /// <summary>
        /// Sets the comment text of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="text">The comment text.</param>
        public static void SetCommentText(Node node, string text)
        {
            SetCommentText(node.Documentation, text);
        }

        /// <summary>
        /// Sets the comment text of a node.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="text">The comment text.</param>
        public static void SetCommentText(Document documentation, string text)
        {
            Type DocumentationType = documentation.GetType();
            PropertyInfo CommentProperty = SafeType.GetProperty(DocumentationType, nameof(Document.Comment));

            CommentProperty.SetValue(documentation, text);
        }

        /// <summary>
        /// Checks whether the given property of a node is a documentation object.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the given property of a node is a documentation object; otherwise, false.</returns>
        public static bool IsDocumentProperty(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return IsDocumentProperty(NodeType, propertyName);
        }

        /// <summary>
        /// Checks whether the given property of a node type is a documentation object.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the given property of a node type is a documentation object; otherwise, false.</returns>
        public static bool IsDocumentProperty(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (document == null) throw new ArgumentNullException(nameof(document));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, nameof(Node.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Property.SetValue(node, document);
        }

        /// <summary>
        /// Copy the documentation from a node to another.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="cloneCommentGuid">True if Uuid of the destination documentation is to be the same as the source, or should be a new Uuid.</param>
        public static void CopyDocumentation(Node sourceNode, Node destinationNode, bool cloneCommentGuid)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            if (SourceNodeType != DestinationNodeType) throw new ArgumentException($"{nameof(sourceNode)} and {nameof(destinationNode)} must be of the same type");

            PropertyInfo Property = SafeType.GetProperty(SourceNodeType, nameof(Node.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? sourceNode.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceNode.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationNode, DocumentCopy);
        }

        /// <summary>
        /// Copy a documentation from a block to another.
        /// </summary>
        /// <param name="sourceBlock">The source block.</param>
        /// <param name="destinationBlock">The destination block.</param>
        /// <param name="cloneCommentGuid">True if Uuid of the destination documentation is to be the same as the source, or should be a new Uuid.</param>
        public static void CopyDocumentation(IBlock sourceBlock, IBlock destinationBlock, bool cloneCommentGuid)
        {
            if (sourceBlock == null) throw new ArgumentNullException(nameof(sourceBlock));
            if (destinationBlock == null) throw new ArgumentNullException(nameof(destinationBlock));

            Type SourceBlockType = sourceBlock.GetType();
            Type DestinationBlockType = sourceBlock.GetType();
            Debug.Assert(SourceBlockType == DestinationBlockType);

            PropertyInfo Property = SafeType.GetProperty(SourceBlockType, nameof(IBlock.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? sourceBlock.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlock.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationBlock, DocumentCopy);
        }

        /// <summary>
        /// Copy a documentation from a block list to another.
        /// </summary>
        /// <param name="sourceBlockList">The source block list.</param>
        /// <param name="destinationBlockList">The destination block list.</param>
        /// <param name="cloneCommentGuid">True if Uuid of the destination documentation is to be the same as the source, or should be a new Uuid.</param>
        public static void CopyDocumentation(IBlockList sourceBlockList, IBlockList destinationBlockList, bool cloneCommentGuid)
        {
            if (sourceBlockList == null) throw new ArgumentNullException(nameof(sourceBlockList));
            if (destinationBlockList == null) throw new ArgumentNullException(nameof(destinationBlockList));

            Type SourceBlockType = sourceBlockList.GetType();
            Type DestinationBlockType = sourceBlockList.GetType();
            Debug.Assert(SourceBlockType == DestinationBlockType);

            PropertyInfo Property = SafeType.GetProperty(SourceBlockType, nameof(IBlock.Documentation));

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? sourceBlockList.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlockList.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationBlockList, DocumentCopy);
        }

        /// <summary>
        /// Checks whether the property of a node is a <see cref="bool"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is a <see cref="bool"/>; otherwise, false.</returns>
        public static bool IsBooleanProperty(Node node, string propertyName)
        {
            return IsValueProperty(node, propertyName, typeof(bool));
        }

        /// <summary>
        /// Checks whether the property of a node type is a <see cref="bool"/>.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is a <see cref="bool"/>; otherwise, false.</returns>
        public static bool IsBooleanProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(bool));
        }

        /// <summary>
        /// Sets the value of a <see cref="bool"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetBooleanProperty(Node node, string propertyName, bool value)
        {
            SetValueProperty(node, propertyName, value);
        }

        /// <summary>
        /// Copy the <see cref="bool"/> value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyBooleanProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            CopyValueProperty<bool>(sourceNode, destinationNode, propertyName);
        }

        /// <summary>
        /// Checks whether the property of a node is a <see cref="string"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is a <see cref="string"/>; otherwise, false.</returns>
        public static bool IsStringProperty(Node node, string propertyName)
        {
            return IsValueProperty(node, propertyName, typeof(string));
        }

        /// <summary>
        /// Checks whether the property of a node type is a <see cref="string"/>.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is a <see cref="string"/>; otherwise, false.</returns>
        public static bool IsStringProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(string));
        }

        /// <summary>
        /// Sets the value of a <see cref="string"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetStringProperty(Node node, string propertyName, string value)
        {
            SetValueProperty(node, propertyName, value);
        }

        /// <summary>
        /// Copy the <see cref="string"/> value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyStringProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            CopyValueProperty<string>(sourceNode, destinationNode, propertyName);
        }

        /// <summary>
        /// Checks whether the property of a node is a <see cref="Guid"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is a <see cref="Guid"/>; otherwise, false.</returns>
        public static bool IsGuidProperty(Node node, string propertyName)
        {
            return IsValueProperty(node, propertyName, typeof(Guid));
        }

        /// <summary>
        /// Checks whether the property of a node type is a <see cref="Guid"/>.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is a <see cref="Guid"/>; otherwise, false.</returns>
        public static bool IsGuidProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(Guid));
        }

        /// <summary>
        /// Sets the value of a <see cref="Guid"/> property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetGuidProperty(Node node, string propertyName, Guid value)
        {
            SetValueProperty(node, propertyName, value);
        }

        /// <summary>
        /// Copy the <see cref="Guid"/> value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyGuidProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            CopyValueProperty<Guid>(sourceNode, destinationNode, propertyName);
        }

        /// <summary>
        /// Checks whether the property of a node is an enum.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node is an enum; otherwise, false.</returns>
        public static bool IsEnumProperty(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return IsEnumProperty(NodeType, propertyName);
        }

        /// <summary>
        /// Checks whether the property of a node type is an enum.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the property of a node type is an enum; otherwise, false.</returns>
        public static bool IsEnumProperty(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            return PropertyType.IsEnum;
        }

        /// <summary>
        /// Sets the value of an enum property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The new value.</param>
        public static void SetEnumProperty(Node node, string propertyName, object value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum);

            Property.SetValue(node, value);
        }

        /// <summary>
        /// Copy the enum value of from a node to another for a given property.
        /// </summary>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destinationNode">The destination node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void CopyEnumProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SafeType.GetProperty(SourceNodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsEnum) throw new ArgumentException($"{nameof(propertyName)} must designate an enum property");

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }

        private static void GetEnumMinMax(PropertyInfo property, out int min, out int max)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            Type PropertyType = property.PropertyType;
            if (!PropertyType.IsEnum && PropertyType != typeof(bool)) throw new ArgumentException($"{nameof(property)} must designate an enum or bool property");

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

            PropertyInfo Property = SafeType.GetProperty(nodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            if (PropertyType != type)
                return false;

            return true;
        }

        private static void SetValueProperty<T>(Node node, string propertyName, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (value == null) throw new ArgumentNullException(nameof(value));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(node, value);
        }

        private static void CopyValueProperty<T>(Node sourceNode, Node destinationNode, string propertyName)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SafeType.GetProperty(SourceNodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }
    }
}
