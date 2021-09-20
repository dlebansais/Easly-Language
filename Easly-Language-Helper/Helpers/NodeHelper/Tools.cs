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

    public static partial class NodeHelper
    {
        public static Type NodeType(string typeName)
        {
            string? RootName = typeof(Root).FullName;
            Debug.Assert(RootName != null);

            if (RootName == null)
                return null!;

            int Index = RootName.LastIndexOf('.');
            string FullTypeName = RootName.Substring(0, Index + 1) + typeName;

            Assembly RootAssembly = typeof(Root).Assembly;

            Type? FullType = RootAssembly.GetType(FullTypeName);
            Debug.Assert(FullType != null);

            if (FullType == null)
                return null!;

            return FullType;
        }

        public static IDictionary<Type, TValue?> CreateNodeDictionary<TValue>()
        {
            IDictionary<Type, TValue?> Result = new Dictionary<Type, TValue?>();
            Assembly LanguageAssembly = typeof(Root).Assembly;
            Type[] LanguageTypes = LanguageAssembly.GetTypes();

            foreach (Type Item in LanguageTypes)
            {
                if (!Item.IsInterface && !Item.IsAbstract)
                {
                    string? FullName = typeof(Node).FullName;
                    Debug.Assert(FullName != null);

                    if (FullName == null)
                        return null!;

                    if (Item.GetInterface(FullName) != null)
                    {
                        Type[] Interfaces = Item.GetInterfaces();
                        foreach (Type InterfaceType in Interfaces)
                            if (InterfaceType.Name == $"I{Item.Name}")
                            {
                                Result.Add(InterfaceType, default(TValue));
                                break;
                            }
                    }
                }
            }

            return Result;
        }

        public static bool IsCollectionNeverEmpty(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return IsCollectionNeverEmpty(node.GetType(), propertyName);
        }

        public static bool IsCollectionNeverEmpty(Type nodeType, string propertyName)
        {
            if (!NodeTreeHelperList.IsNodeListProperty(nodeType, propertyName, out Type _) && !NodeTreeHelperBlockList.IsBlockListProperty(nodeType, propertyName, /*out Type _,*/ out _)) throw new ArgumentException($"{nameof(propertyName)} must be a list or block list property of {nameof(nodeType)}");

            // Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);
            Type InterfaceType = nodeType;

            if (NeverEmptyCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in NeverEmptyCollectionTable[InterfaceType])
                    if (Item == propertyName)
                        return true;
            }

            return false;
        }

        public static bool IsCollectionWithExpand(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return IsCollectionWithExpand(node.GetType(), propertyName);
        }

        public static bool IsCollectionWithExpand(Type nodeType, string propertyName)
        {
            if (!NodeTreeHelperList.IsNodeListProperty(nodeType, propertyName, out Type _) && !NodeTreeHelperBlockList.IsBlockListProperty(nodeType, propertyName, /*out Type _,*/ out _)) throw new ArgumentException($"{nameof(propertyName)} must be a list or block list property of {nameof(nodeType)}");
            if (IsCollectionNeverEmpty(nodeType, propertyName)) throw new ArgumentException($"{nameof(nodeType)} must be a list or block list that can be empty");

            // Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);
            Type InterfaceType = nodeType;

            if (WithExpandCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in WithExpandCollectionTable[InterfaceType])
                    if (Item == propertyName)
                        return true;
            }

            return false;
        }

        public static Document CreateDocumentationCopy(Document documentation)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            return CreateSimpleDocumentation(documentation.Comment, documentation.Uuid);
        }

        private static QualifiedName StringToQualifiedName(string text)
        {
            string[] StringList;
            ParseDotSeparatedIdentifiers(text, out StringList);

            List<Identifier> IdentifierList = new List<Identifier>();
            foreach (string Identifier in StringList)
                IdentifierList.Add(CreateSimpleIdentifier(Identifier));

            return CreateQualifiedName(IdentifierList);
        }

        private static void ParseDotSeparatedIdentifiers(string text, out string[] stringList)
        {
            ParseSymbolSeparatedStrings(text, '.', out stringList);
        }

        private static void ParseSymbolSeparatedStrings(string text, char symbol, out string[] stringList)
        {
            string[] SplittedStrings = text.Split(symbol);
            stringList = new string[SplittedStrings.Length];
            for (int i = 0; i < SplittedStrings.Length; i++)
                stringList[i] = SplittedStrings[i].Trim();
        }

        private static bool GetExpressionText(Expression expressionNode, out string text)
        {
            if (expressionNode is ManifestNumberExpression AsNumber)
            {
                text = AsNumber.Text;
                return true;
            }
            else if (expressionNode is QueryExpression AsQuery)
            {
                text = AsQuery.Query.Path[0].Text;
                return true;
            }
            else
            {
                text = null!;
                return false;
            }
        }
    }
}
