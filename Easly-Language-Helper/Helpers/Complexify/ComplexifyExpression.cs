namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static void GetComplexifiedExpression(IExpression node, List<INode> complexifiedNodeList)
        {
            bool IsHandled = false;

            switch (node)
            {
                case IAgentExpression AsAgentExpression:
                    GetComplexifiedAgentExpression(AsAgentExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IAssertionTagExpression AsAssertionTagExpression:
                    IsHandled = true;
                    break;

                case IBinaryConditionalExpression AsBinaryConditionalExpression:
                    GetComplexifiedBinaryConditionalExpression(AsBinaryConditionalExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IBinaryOperatorExpression AsBinaryOperatorExpression:
                    GetComplexifiedBinaryOperatorExpression(AsBinaryOperatorExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IClassConstantExpression AsClassConstantExpression:
                    IsHandled = true;
                    break;

                case ICloneOfExpression AsCloneOfExpression:
                    GetComplexifiedCloneOfExpression(AsCloneOfExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IEntityExpression AsEntityExpression:
                    GetComplexifiedEntityExpression(AsEntityExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IEqualityExpression AsEqualityExpression:
                    GetComplexifiedEqualityExpression(AsEqualityExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IIndexQueryExpression AsIndexQueryExpression:
                    GetComplexifiedIndexQueryExpression(AsIndexQueryExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IInitializedObjectExpression AsInitializedObjectExpression:
                    GetComplexifiedInitializedObjectExpression(AsInitializedObjectExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IKeywordExpression AsKeywordExpression:
                case IManifestCharacterExpression AsManifestCharacterExpression:
                case IManifestNumberExpression AsManifestNumberExpression:
                case IManifestStringExpression AsManifestStringExpression:
                    IsHandled = true;
                    break;

                case INewExpression AsNewExpression:
                    GetComplexifiedNewExpression(AsNewExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IOldExpression AsOldExpression:
                    GetComplexifiedOldExpression(AsOldExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPrecursorExpression AsPrecursorExpression:
                    GetComplexifiedPrecursorExpression(AsPrecursorExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPrecursorIndexExpression AsPrecursorIndexExpression:
                    GetComplexifiedPrecursorIndexExpression(AsPrecursorIndexExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPreprocessorExpression AsPreprocessorExpression:
                    IsHandled = true;
                    break;

                case IQueryExpression AsQueryExpression:
                    GetComplexifiedQueryExpression(AsQueryExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IResultOfExpression AsResultOfExpression:
                    GetComplexifiedResultOfExpression(AsResultOfExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IUnaryNotExpression AsUnaryNotExpression:
                    GetComplexifiedUnaryNotExpression(AsUnaryNotExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IUnaryOperatorExpression AsUnaryOperatorExpression:
                    GetComplexifiedUnaryOperatorExpression(AsUnaryOperatorExpression, complexifiedNodeList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        private static void GetComplexifiedAgentExpression(IAgentExpression node, IList<INode> complexifiedNodeList)
        {
            if (node.BaseType.IsAssigned && GetComplexifiedNode(node.BaseType.Item, out List<INode> ComplexifiedBaseTypeList) && ComplexifiedBaseTypeList[0] is IObjectType AsComplexifiedBaseType)
            {
                IIdentifier ClonedDelegated = (IIdentifier)DeepCloneNode(node.Delegated, cloneCommentGuid: false);
                IAgentExpression NewAgentExpression = CreateAgentExpression(ClonedDelegated, AsComplexifiedBaseType);
                complexifiedNodeList.Add(NewAgentExpression);
            }
        }

        private static void GetComplexifiedBinaryConditionalExpression(IBinaryConditionalExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.LeftExpression, out List<INode> ComplexifiedLeftExpressionList) && ComplexifiedLeftExpressionList[0] is IExpression AsComplexifiedLeftExpression)
            {
                IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                IBinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(AsComplexifiedLeftExpression, node.Conditional, ClonedRightExpression);
                complexifiedNodeList.Add(NewBinaryConditionalExpression);
            }
            else if (GetComplexifiedNode(node.RightExpression, out List<INode> ComplexifiedRightExpressionList) && ComplexifiedRightExpressionList[0] is IExpression AsComplexifiedRightExpression)
            {
                IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                IBinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ClonedLeftExpression, node.Conditional, AsComplexifiedRightExpression);
                complexifiedNodeList.Add(NewBinaryConditionalExpression);
            }
        }

        private static void GetComplexifiedBinaryOperatorExpression(IBinaryOperatorExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.LeftExpression, out List<INode> ComplexifiedLeftExpressionList) && ComplexifiedLeftExpressionList[0] is IExpression AsComplexifiedLeftExpression)
            {
                IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                IIdentifier ClonedOperator = (IIdentifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                IBinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(AsComplexifiedLeftExpression, ClonedOperator, ClonedRightExpression);
                complexifiedNodeList.Add(NewBinaryOperatorExpression);
            }
            else if (GetComplexifiedNode(node.RightExpression, out List<INode> ComplexifiedRightExpressionList) && ComplexifiedRightExpressionList[0] is IExpression AsComplexifiedRightExpression)
            {
                IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                IIdentifier ClonedOperator = (IIdentifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                IBinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, ClonedOperator, AsComplexifiedRightExpression);
                complexifiedNodeList.Add(NewBinaryOperatorExpression);
            }
        }

        private static void GetComplexifiedCloneOfExpression(ICloneOfExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                ICloneOfExpression NewCloneOfExpression = CreateCloneOfExpression(node.Type, AsComplexifiedSource);
                complexifiedNodeList.Add(NewCloneOfExpression);
            }
        }

        private static void GetComplexifiedEntityExpression(IEntityExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Query, out List<INode> ComplexifiedQueryList) && ComplexifiedQueryList[0] is IQualifiedName AsComplexifiedQuery)
            {
                IEntityExpression NewEntityExpression = CreateEntityExpression(AsComplexifiedQuery);
                complexifiedNodeList.Add(NewEntityExpression);
            }
        }

        private static void GetComplexifiedEqualityExpression(IEqualityExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.LeftExpression, out List<INode> ComplexifiedLeftExpressionList) && ComplexifiedLeftExpressionList[0] is IExpression AsComplexifiedLeftExpression)
            {
                IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                IEqualityExpression NewEqualityExpression = CreateEqualityExpression(AsComplexifiedLeftExpression, node.Comparison, node.Equality, ClonedRightExpression);
                complexifiedNodeList.Add(NewEqualityExpression);
            }
            else if (GetComplexifiedNode(node.RightExpression, out List<INode> ComplexifiedRightExpressionList) && ComplexifiedRightExpressionList[0] is IExpression AsComplexifiedRightExpression)
            {
                IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                IEqualityExpression NewEqualityExpression = CreateEqualityExpression(ClonedLeftExpression, node.Comparison, node.Equality, AsComplexifiedRightExpression);
                complexifiedNodeList.Add(NewEqualityExpression);
            }
        }

        private static void GetComplexifiedIndexQueryExpression(IIndexQueryExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.IndexedExpression, out List<INode> ComplexifiedIndexedExpressionList) && ComplexifiedIndexedExpressionList[0] is IExpression AsComplexifiedIndexedExpression)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IIndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(AsComplexifiedIndexedExpression, ClonedArgumentBlocks);
                complexifiedNodeList.Add(NewIndexQueryExpression);
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IExpression ClonedIndexedExpression = (IExpression)DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false);
                IIndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ClonedIndexedExpression, ComplexifiedArgumentBlocks);
                complexifiedNodeList.Add(NewIndexQueryExpression);
            }
        }

        private static void GetComplexifiedInitializedObjectExpression(IInitializedObjectExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedAssignmentArgumentBlockList(node.AssignmentBlocks, out IBlockList<IAssignmentArgument, AssignmentArgument> ComplexifiedAssignmentBlocks))
            {
                IIdentifier ClonedClassIdentifier = (IIdentifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);
                IInitializedObjectExpression NewInitializedObjectExpression = CreateInitializedObjectExpression(ClonedClassIdentifier, ComplexifiedAssignmentBlocks);
                complexifiedNodeList.Add(NewInitializedObjectExpression);
            }
        }

        private static void GetComplexifiedNewExpression(INewExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Object, out List<INode> ComplexifiedObjectList) && ComplexifiedObjectList[0] is IQualifiedName AsComplexifiedObject)
            {
                INewExpression NewNewExpression = CreateNewExpression(AsComplexifiedObject);
                complexifiedNodeList.Add(NewNewExpression);
            }
        }

        private static void GetComplexifiedOldExpression(IOldExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Query, out List<INode> ComplexifiedQueryList) && ComplexifiedQueryList[0] is IQualifiedName AsComplexifiedQuery)
            {
                IOldExpression NewOldExpression = CreateOldExpression(AsComplexifiedQuery);
                complexifiedNodeList.Add(NewOldExpression);
            }
        }

        private static void GetComplexifiedPrecursorExpression(IPrecursorExpression node, IList<INode> complexifiedNodeList)
        {
            if (node.AncestorType.IsAssigned && GetComplexifiedNode(node.AncestorType.Item, out List<INode> ComplexifiedAncestorTypeList) && ComplexifiedAncestorTypeList[0] is IObjectType AsComplexifiedAncestorType)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IPrecursorExpression NewPrecursorExpression = CreatePrecursorExpression(ClonedArgumentBlocks, AsComplexifiedAncestorType);
                complexifiedNodeList.Add(NewPrecursorExpression);
            }
        }

        private static void GetComplexifiedPrecursorIndexExpression(IPrecursorIndexExpression node, IList<INode> complexifiedNodeList)
        {
            if (node.AncestorType.IsAssigned && GetComplexifiedNode(node.AncestorType.Item, out List<INode> ComplexifiedAncestorTypeList) && ComplexifiedAncestorTypeList[0] is IObjectType AsComplexifiedAncestorType)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IPrecursorIndexExpression NewPrecursorIndexExpression = CreatePrecursorIndexExpression(ClonedArgumentBlocks, AsComplexifiedAncestorType);
                complexifiedNodeList.Add(NewPrecursorIndexExpression);
            }
        }

        public static void GetComplexifiedQueryExpression(IQueryExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Query, out List<INode> ComplexifiedQueryList) && ComplexifiedQueryList[0] is IQualifiedName AsComplexifiedQuery)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IQueryExpression NewQueryExpression = CreateQueryExpression(AsComplexifiedQuery, ClonedArgumentBlocks);
                complexifiedNodeList.Add(NewQueryExpression);
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IQualifiedName ClonedQuery = (IQualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
                IQueryExpression NewQueryExpression = CreateQueryExpression(ClonedQuery, ComplexifiedArgumentBlocks);
                complexifiedNodeList.Add(NewQueryExpression);
            }
            else if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Query, out IQualifiedName NewQuery, out List<IArgument> ArgumentList))
            {
                IQualifiedName ClonedQuery = (IQualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
                IQueryExpression NewQueryExpression = CreateQueryExpression(ClonedQuery, ArgumentList);
                complexifiedNodeList.Add(NewQueryExpression);
            }
            else if (ComplexifyAsAgentExpression(node, out IAgentExpression ComplexifiedAgentExpression))
                complexifiedNodeList.Add(ComplexifiedAgentExpression);
            else if (ComplexifyAsAssertionTagExpression(node, out IAssertionTagExpression ComplexifiedAssertionTagExpression))
                complexifiedNodeList.Add(ComplexifiedAssertionTagExpression);
            else if (ComplexifyAsBinaryAndConditionalExpression(node, out IBinaryConditionalExpression ComplexifiedBinaryAndConditionalExpression))
                complexifiedNodeList.Add(ComplexifiedBinaryAndConditionalExpression);
            else if (ComplexifyAsBinaryOrConditionalExpression(node, out IBinaryConditionalExpression ComplexifiedBinaryOrConditionalExpression))
                complexifiedNodeList.Add(ComplexifiedBinaryOrConditionalExpression);
            else if (ComplexifyAsBinaryOperatorExpression(node, out IBinaryOperatorExpression ComplexifiedBinaryOperatorExpression))
                complexifiedNodeList.Add(ComplexifiedBinaryOperatorExpression);
            else if (ComplexifyAsClassConstantExpression(node, out IClassConstantExpression ComplexifiedClassConstantExpression))
                complexifiedNodeList.Add(ComplexifiedClassConstantExpression);
            else if (ComplexifyAsCloneOfExpression(node, out ICloneOfExpression ComplexifiedCloneOfExpression))
                complexifiedNodeList.Add(ComplexifiedCloneOfExpression);
            else if (ComplexifyAsEntityExpression(node, out IEntityExpression ComplexifiedEntityExpression))
                complexifiedNodeList.Add(ComplexifiedEntityExpression);
            else if (ComplexifyAsEqualExpression(node, out IEqualityExpression ComplexifiedEqualityExpression))
                complexifiedNodeList.Add(ComplexifiedEqualityExpression);
            else if (ComplexifyAsDifferentExpression(node, out IEqualityExpression ComplexifiedDifferentExpression))
                complexifiedNodeList.Add(ComplexifiedDifferentExpression);
            else if (ComplexifyAsIndexQueryExpression(node, out IIndexQueryExpression ComplexifiedIndexQueryExpression))
                complexifiedNodeList.Add(ComplexifiedIndexQueryExpression);
            else if (ComplexifyAsInitializedObjectExpression(node, out IInitializedObjectExpression ComplexifiedInitializedObjectExpression))
                complexifiedNodeList.Add(ComplexifiedInitializedObjectExpression);
            else if (ComplexifyAsKeywordExpression(node, out IKeywordExpression ComplexifiedKeywordExpression))
                complexifiedNodeList.Add(ComplexifiedKeywordExpression);
            else if (ComplexifyAsManifestCharacterExpression(node, out IManifestCharacterExpression ComplexifiedManifestCharacterExpression))
                complexifiedNodeList.Add(ComplexifiedManifestCharacterExpression);
            else if (ComplexifyAsManifestNumberExpression(node, out IManifestNumberExpression ComplexifiedManifestNumberExpression))
                complexifiedNodeList.Add(ComplexifiedManifestNumberExpression);
            else if (ComplexifyAsManifestStringExpression(node, out IManifestStringExpression ComplexifiedManifestStringExpression))
                complexifiedNodeList.Add(ComplexifiedManifestStringExpression);
            else if (ComplexifyAsNewExpression(node, out INewExpression ComplexifiedNewExpression))
                complexifiedNodeList.Add(ComplexifiedNewExpression);
            else if (ComplexifyAsOldExpression(node, out IOldExpression ComplexifiedOldExpression))
                complexifiedNodeList.Add(ComplexifiedOldExpression);
            else if (ComplexifyAsPrecursorExpression(node, out IPrecursorExpression ComplexifiedPrecursorExpression))
                complexifiedNodeList.Add(ComplexifiedPrecursorExpression);
            else if (ComplexifyAsPrecursorIndexExpression(node, out IPrecursorIndexExpression ComplexifiedPrecursorIndexExpression))
                complexifiedNodeList.Add(ComplexifiedPrecursorIndexExpression);
            else if (ComplexifyAsPreprocessorExpression(node, out IPreprocessorExpression ComplexifiedPreprocessorExpression))
                complexifiedNodeList.Add(ComplexifiedPreprocessorExpression);
            else if (ComplexifyAsResultOfExpression(node, out IResultOfExpression ComplexifiedResultOfExpression))
                complexifiedNodeList.Add(ComplexifiedResultOfExpression);
            else if (ComplexifyAsUnaryNotExpression(node, out IUnaryNotExpression ComplexifiedUnaryNotExpression))
                complexifiedNodeList.Add(ComplexifiedUnaryNotExpression);
            else if (ComplexifyAsUnaryOperatorExpression(node, out IUnaryOperatorExpression ComplexifiedUnaryOperatorExpression))
                complexifiedNodeList.Add(ComplexifiedUnaryOperatorExpression);
        }

        private static bool ComplexifyAsAgentExpression(IQueryExpression node, out IAgentExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node) && ParsePattern(node, "agent", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                AfterText = AfterText.Trim();
                IIdentifier Delegated = null;

                if (AfterText.StartsWith("{"))
                {
                    int TypeNameIndex = AfterText.IndexOf("}");

                    if (TypeNameIndex > 1)
                    {
                        string FeatureName = AfterText.Substring(TypeNameIndex + 1).Trim();
                        Delegated = CreateSimpleIdentifier(FeatureName);

                        string BaseTypeName = AfterText.Substring(1, TypeNameIndex - 1).Trim();
                        IIdentifier BaseTypeIdentifier = CreateSimpleIdentifier(BaseTypeName);
                        IObjectType BaseType = CreateSimpleType(SharingType.NotShared, BaseTypeIdentifier);

                        complexifiedNode = CreateAgentExpression(Delegated, BaseType);
                    }
                }

                if (complexifiedNode == null)
                {
                    Delegated = CreateSimpleIdentifier(AfterText.Trim());
                    complexifiedNode = CreateAgentExpression(Delegated);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsAssertionTagExpression(IQueryExpression node, out IAssertionTagExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node) && ParsePattern(node, "tag ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IIdentifier TagIdentifier = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateAssertionTagExpression(TagIdentifier);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsBinaryAndConditionalExpression(IQueryExpression node, out IBinaryConditionalExpression complexifiedNode)
        {
            return ComplexifyAsBinaryConditionalExpression(node, " and ", ConditionalTypes.And, out complexifiedNode);
        }

        private static bool ComplexifyAsBinaryOrConditionalExpression(IQueryExpression node, out IBinaryConditionalExpression complexifiedNode)
        {
            return ComplexifyAsBinaryConditionalExpression(node, " or ", ConditionalTypes.Or, out complexifiedNode);
        }

        private static bool ComplexifyAsBinaryConditionalExpression(IQueryExpression node, string pattern, ConditionalTypes conditionalType, out IBinaryConditionalExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, pattern, out string BeforeText, out string AfterText))
            {
                CloneComplexifiedExpression(node, BeforeText, AfterText, out IExpression LeftExpression, out IExpression RightExpression);
                complexifiedNode = CreateBinaryConditionalExpression(LeftExpression, conditionalType, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsBinaryOperatorExpression(IQueryExpression node, out IBinaryOperatorExpression complexifiedNode)
        {
            complexifiedNode = null;

            string Pattern = "+";
            if (ParsePattern(node, " " + Pattern + " ", out string BeforeText, out string AfterText))
            {
                CloneComplexifiedExpression(node, BeforeText, AfterText, out IExpression LeftExpression, out IExpression RightExpression);
                IIdentifier Operator = CreateSimpleIdentifier(Pattern);
                complexifiedNode = CreateBinaryOperatorExpression(LeftExpression, Operator, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsClassConstantExpression(IQueryExpression node, out IClassConstantExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text.StartsWith("{"))
                {
                    int ClassNameIndex = Text.IndexOf("}");

                    if (ClassNameIndex > 2)
                    {
                        IIdentifier ClassIdentifier = CreateSimpleIdentifier(Text.Substring(1, ClassNameIndex - 1));
                        IIdentifier ConstantIdentifier = CreateSimpleIdentifier(Text.Substring(ClassNameIndex + 1));
                        complexifiedNode = CreateClassConstantExpression(ClassIdentifier, ConstantIdentifier);
                    }
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsCloneOfExpression(IQueryExpression node, out ICloneOfExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "clone of ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedExpression(node, AfterText, out IExpression Source);
                complexifiedNode = CreateCloneOfExpression(CloneType.Shallow, Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsEntityExpression(IQueryExpression node, out IEntityExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "entity ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IQualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as IQualifiedName;
                Debug.Assert(ClonedQuery != null);
                Debug.Assert(ClonedQuery.Path.Count > 0);

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

                complexifiedNode = CreateEntityExpression(ClonedQuery);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsEqualExpression(IQueryExpression node, out IEqualityExpression complexifiedNode)
        {
            return ComplexifyEqualityExpression(node, " = ", ComparisonType.Equal, out complexifiedNode);
        }

        private static bool ComplexifyAsDifferentExpression(IQueryExpression node, out IEqualityExpression complexifiedNode)
        {
            return ComplexifyEqualityExpression(node, " /= ", ComparisonType.Different, out complexifiedNode);
        }

        private static bool ComplexifyEqualityExpression(IQueryExpression node, string pattern, ComparisonType comparisonType, out IEqualityExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, pattern, out string BeforeText, out string AfterText))
            {
                CloneComplexifiedExpression(node, BeforeText, AfterText, out IExpression LeftExpression, out IExpression RightExpression);
                complexifiedNode = CreateEqualityExpression(LeftExpression, comparisonType, EqualityType.Physical, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexQueryExpression(IQueryExpression node, out IIndexQueryExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Query, '[', ']', out IQualifiedName NewQuery, out List<IArgument> ArgumentList))
            {
                IExpression IndexedExpression = CreateQueryExpression(NewQuery, new List<IArgument>());
                complexifiedNode = CreateIndexQueryExpression(IndexedExpression, ArgumentList);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsInitializedObjectExpression(IQueryExpression node, out IInitializedObjectExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (ParsePattern(Text, "[", out string ClassText, out string InitText))
                {
                    if (ParsePattern(InitText, ":=", out string ParameterText, out string SourceText) && SourceText.EndsWith("]"))
                    {
                        IIdentifier ClassIdentifier = CreateSimpleIdentifier(ClassText);

                        IIdentifier ParameterIdentifier = CreateSimpleIdentifier(ParameterText);
                        IExpression Source = CreateSimpleQueryExpression(SourceText.Substring(0, SourceText.Length - 1));
                        IAssignmentArgument FirstArgument = CreateAssignmentArgument(new List<IIdentifier>() { ParameterIdentifier }, Source);

                        List<IAssignmentArgument> ArgumentList = new List<IAssignmentArgument>();
                        ArgumentList.Add(FirstArgument);

                        complexifiedNode = CreateInitializedObjectExpression(ClassIdentifier, ArgumentList);
                    }
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsKeywordExpression(IQueryExpression node, out IKeywordExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;
                if (Text.Length > 0)
                {
                    Text = Text.Substring(0, 1).ToUpper() + Text.Substring(1);

                    if (Text == "True")
                        complexifiedNode = CreateKeywordExpression(Keyword.True);
                    else if (Text == "False")
                        complexifiedNode = CreateKeywordExpression(Keyword.False);
                    else if (Text == "Current")
                        complexifiedNode = CreateKeywordExpression(Keyword.Current);
                    else if (Text == "Value")
                        complexifiedNode = CreateKeywordExpression(Keyword.Value);
                    else if (Text == "Result")
                        complexifiedNode = CreateKeywordExpression(Keyword.Result);
                    else if (Text == "Retry")
                        complexifiedNode = CreateKeywordExpression(Keyword.Retry);
                    else if (Text == "Exception")
                        complexifiedNode = CreateKeywordExpression(Keyword.Exception);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsManifestCharacterExpression(IQueryExpression node, out IManifestCharacterExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text.Length == 3 && Text[0] == '\'' && Text[2] == '\'')
                    complexifiedNode = CreateManifestCharacterExpression(Text.Substring(1, 1));
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsManifestNumberExpression(IQueryExpression node, out IManifestNumberExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text.Length >= 1)
                {
                    IFormattedNumber fn = FormattedNumber.Parse(Text, false);
                    if (fn.InvalidText.Length == 0)
                        complexifiedNode = CreateSimpleManifestNumberExpression(Text);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsManifestStringExpression(IQueryExpression node, out IManifestStringExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text.Length >= 2 && Text[0] == '"' && Text[Text.Length - 1] == '"')
                    complexifiedNode = CreateManifestStringExpression(Text.Substring(1, Text.Length - 2));
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsNewExpression(IQueryExpression node, out INewExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "new ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IQualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as IQualifiedName;
                Debug.Assert(ClonedQuery != null);
                Debug.Assert(ClonedQuery.Path.Count > 0);

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

                complexifiedNode = CreateNewExpression(ClonedQuery);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsOldExpression(IQueryExpression node, out IOldExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "old ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IQualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as IQualifiedName;
                Debug.Assert(ClonedQuery != null);
                Debug.Assert(ClonedQuery.Path.Count > 0);

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

                complexifiedNode = CreateOldExpression(ClonedQuery);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorExpression(IQueryExpression node, out IPrecursorExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Query.Path.Count == 1)
            {
                string Text = node.Query.Path[0].Text;

                if (Text == "precursor")
                {
                    IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
                    complexifiedNode = CreatePrecursorExpression(ClonedQuery.ArgumentBlocks);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorIndexExpression(IQueryExpression node, out IPrecursorIndexExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Query.Path.Count == 1 && node.ArgumentBlocks.NodeBlockList.Count > 0)
            {
                string Text = node.Query.Path[0].Text;

                if (Text == "precursor[]")
                {
                    IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
                    complexifiedNode = CreatePrecursorIndexExpression(ClonedQuery.ArgumentBlocks);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPreprocessorExpression(IQueryExpression node, out IPreprocessorExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text == "DateAndTime")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.DateAndTime);
                else if (Text == "CompilationDiscreteIdentifier")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.CompilationDiscreteIdentifier);
                else if (Text == "ClassPath")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.ClassPath);
                else if (Text == "CompilerVersion")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.CompilerVersion);
                else if (Text == "ConformanceToStandard")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.ConformanceToStandard);
                else if (Text == "DiscreteClassIdentifier")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.DiscreteClassIdentifier);
                else if (Text == "Counter")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.Counter);
                else if (Text == "Debugging")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.Debugging);
                else if (Text == "RandomInteger")
                    complexifiedNode = CreatePreprocessorExpression(PreprocessorMacro.RandomInteger);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsResultOfExpression(IQueryExpression node, out IResultOfExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "result of ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedExpression(node, AfterText, out IExpression Source);
                complexifiedNode = CreateResultOfExpression(Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsUnaryNotExpression(IQueryExpression node, out IUnaryNotExpression complexifiedNode)
        {
            complexifiedNode = null;

            Debug.Assert(node.Query.Path.Count > 0);
            string Text = node.Query.Path[0].Text;

            if (Text.StartsWith("not "))
            {
                CloneComplexifiedExpression(node, Text.Substring(4), out IExpression RightExpression);
                complexifiedNode = CreateUnaryNotExpression(RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsUnaryOperatorExpression(IQueryExpression node, out IUnaryOperatorExpression complexifiedNode)
        {
            complexifiedNode = null;

            Debug.Assert(node.Query.Path.Count > 0);
            string Text = node.Query.Path[0].Text;

            string Pattern = "-";

            if (Text.StartsWith(Pattern + " "))
            {
                IIdentifier OperatorName = CreateSimpleIdentifier(Pattern);

                CloneComplexifiedExpression(node, Text.Substring(Pattern.Length + 1), out IExpression RightExpression);
                complexifiedNode = CreateUnaryOperatorExpression(OperatorName, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static void GetComplexifiedResultOfExpression(IResultOfExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IResultOfExpression NewResultOfExpression = CreateResultOfExpression(AsComplexifiedSource);
                complexifiedNodeList.Add(NewResultOfExpression);
            }
        }

        private static void GetComplexifiedUnaryNotExpression(IUnaryNotExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.RightExpression, out List<INode> ComplexifiedRightExpressionList) && ComplexifiedRightExpressionList[0] is IExpression AsComplexifiedRightExpression)
            {
                IUnaryNotExpression NewUnaryNotExpression = CreateUnaryNotExpression(AsComplexifiedRightExpression);
                complexifiedNodeList.Add(NewUnaryNotExpression);
            }
        }

        private static void GetComplexifiedUnaryOperatorExpression(IUnaryOperatorExpression node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.RightExpression, out List<INode> ComplexifiedRightExpressionList) && ComplexifiedRightExpressionList[0] is IExpression AsComplexifiedRightExpression)
            {
                IIdentifier ClonedOperator = (IIdentifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                IUnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(ClonedOperator, AsComplexifiedRightExpression);
                complexifiedNodeList.Add(NewUnaryOperatorExpression);
            }
        }
    }
}
