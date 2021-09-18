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

    public static class NodeTreeHelper
    {
        public static IList<string> EnumChildNodeProperties(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            return EnumChildNodeProperties(NodeType);
        }

        public static IList<string> EnumChildNodeProperties(Type nodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (!IsNodeDescendantType(nodeType)) throw new ArgumentException($"{nameof(nodeType)} must inherit from {nameof(Node)}");

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

        // Exotic calls to get a property for interfaces? WTF Mikeysoft...
        public static PropertyInfo GetPropertyOf(Type type, string propertyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

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

            return type.IsInterface && type.GetInterface(typeof(Node).FullName) != null;
        }

        public static bool IsNodeDescendantType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsSubclassOf(typeof(Node));
        }

        public static Type NodeTypeToInterfaceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (type.IsInterface || type.IsAbstract) throw new ArgumentException($"{nameof(type)} must be neither an interface nor an abstract type");

            Type[] Interfaces = type.GetInterfaces();
            foreach (Type InterfaceType in Interfaces)
                if (InterfaceType.Name == $"I{type.Name}")
                    return InterfaceType;

            return null;
        }

        public static Type InterfaceTypeToNodeType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!type.IsInterface) throw new ArgumentException($"{nameof(type)} must be an interface");

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

            string NodeTypeFullName = string.Empty;
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

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsOptionalDescendantType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(OptionalReference<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            return IsNodeDescendantType(GenericType);
        }

        public static bool IsNodeListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsBlockListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlockList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsBlockType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlock<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsTextNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(Identifier.Text));

            return Property != null;
        }

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

        public static void GetOptionalNodes(Node node, out IDictionary<string, IOptionalReference> optionalNodesTable)
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

        public static void GetArgumentBlocks(Node node, out IDictionary<string, IBlockList<Argument>> argumentBlocksTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            argumentBlocksTable = new Dictionary<string, IBlockList<Argument>>();

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

                    // Debug.Assert(GenericArguments.Length == 2);
                    Debug.Assert(GenericArguments.Length == 1);

                    if (GenericArguments[0] == typeof(Argument))
                    {
                        IBlockList<Argument> ArgumentBlocks = Property.GetValue(node) as IBlockList<Argument>;
                        Debug.Assert(ArgumentBlocks != null);

                        argumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                    }
                }
            }
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

        public static bool IsAssignable(Node node, string propertyName, Node childNode)
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

            // if (IsNodeInterfaceType(PropertyType))
            if (IsNodeDescendantType(PropertyType))
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
                    // Debug.Assert(GenericArguments.Length == 2);
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }
            }

            if (AssignedType == null)
                return false;

            if (!AssignedType.IsAssignableFrom(NodeType))
                return false;

            return true;
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

        private static bool GetBaseNodeAncestor(Type nodeType, out Type ancestorType)
        {
            ancestorType = null;
            bool Result = false;

            string BaseNodeNamespace = typeof(Node).FullName.Substring(0, typeof(Node).FullName.IndexOf(".", StringComparison.InvariantCulture) + 1);
            while (nodeType != typeof(object) && !nodeType.FullName.StartsWith(BaseNodeNamespace, StringComparison.InvariantCulture))
                nodeType = nodeType.BaseType;
            Debug.Assert(nodeType != typeof(object));

            if (nodeType != typeof(Node) && nodeType != typeof(Node))
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
