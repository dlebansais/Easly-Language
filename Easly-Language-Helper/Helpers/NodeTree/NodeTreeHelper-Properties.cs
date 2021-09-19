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

    public static partial class NodeTreeHelper
    {
        public static string GetString(Node node, string propertyName)
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

        public static void SetString(Node node, string propertyName, string text)
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

        public static int GetEnumValue(Node node, string propertyName)
        {
            return GetEnumValueAndRange(node, propertyName, out int Min, out int Max);
        }

        public static int GetEnumValueAndRange(Node node, string propertyName, out int min, out int max)
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

        public static void SetEnumValue(Node node, string propertyName, int value)
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

        public static Guid GetGuid(Node node, string propertyName)
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

        public static string GetCommentText(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Debug.Assert(node.Documentation != null);

            return GetCommentText(node.Documentation);
        }

        public static string GetCommentText(Document documentation)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            string Text = documentation.Comment;
            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetCommentText(Node node, string text)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Debug.Assert(node.Documentation != null);

            SetCommentText(node.Documentation, text);
        }

        public static void SetCommentText(Document documentation, string text)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            Type DocumentationType = documentation.GetType();
            PropertyInfo CommentProperty = DocumentationType.GetProperty(nameof(Document.Comment));
            CommentProperty.SetValue(documentation, text);
        }

        public static bool IsDocumentProperty(Node node, string propertyName)
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
            return PropertyType == typeof(Document);
        }

        public static void SetDocumentation(Node node, Document document)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (document == null) throw new ArgumentNullException(nameof(document));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(Node.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Property.SetValue(node, document);
        }

        public static void CopyDocumentation(Node sourceNode, Node destinationNode, bool cloneCommentGuid)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();

            if (SourceNodeType != DestinationNodeType) throw new ArgumentException($"{nameof(sourceNode)} and {nameof(destinationNode)} must be of the same type");

            PropertyInfo Property = SourceNodeType.GetProperty(nameof(Node.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? sourceNode.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceNode.Documentation.Comment, GuidCopy);
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
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? sourceBlock.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlock.Documentation.Comment, GuidCopy);
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
            Debug.Assert(PropertyType == typeof(Document));

            Guid GuidCopy = cloneCommentGuid ? sourceBlockList.Documentation.Uuid : Guid.NewGuid();
            Document DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlockList.Documentation.Comment, GuidCopy);
            Property.SetValue(destinationBlockList, DocumentCopy);
        }

        public static bool IsBooleanProperty(Node parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(bool));
        }

        public static bool IsBooleanProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(bool));
        }

        public static void SetBooleanProperty(Node parentNode, string propertyName, bool value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyBooleanProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            CopyValueProperty<bool>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsStringProperty(Node parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(string));
        }

        public static bool IsStringProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(string));
        }

        public static void SetStringProperty(Node parentNode, string propertyName, string value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyStringProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            CopyValueProperty<string>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsGuidProperty(Node parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(Guid));
        }

        public static bool IsGuidProperty(Type nodeType, string propertyName)
        {
            return IsValueProperty(nodeType, propertyName, typeof(Guid));
        }

        public static void SetGuidProperty(Node parentNode, string propertyName, Guid value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyGuidProperty(Node sourceNode, Node destinationNode, string propertyName)
        {
            CopyValueProperty<Guid>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsEnumProperty(Node node, string propertyName)
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

        public static void SetEnumProperty(Node node, string propertyName, object value)
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

        public static void CopyEnumProperty(Node sourceNode, Node destinationNode, string propertyName)
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
                foreach (int Value in Values)
                {
                    if (max < Value)
                        max = Value;
                    if (min > Value)
                        min = Value;
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

            PropertyInfo Property = GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

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
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

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

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }
    }
}
