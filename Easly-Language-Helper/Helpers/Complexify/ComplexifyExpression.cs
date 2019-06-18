namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedExpression(IExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;
            bool Result = false;
            bool IsHandled = false;

            switch (node)
            {
                case IAgentExpression AsAgentExpression:
                    Result = GetComplexifiedAgentExpression(AsAgentExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IAssertionTagExpression AsAssertionTagExpression:
                    IsHandled = true;
                    break;

                case IBinaryConditionalExpression AsBinaryConditionalExpression:
                    Result = GetComplexifiedBinaryConditionalExpression(AsBinaryConditionalExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IBinaryOperatorExpression AsBinaryOperatorExpression:
                    Result = GetComplexifiedBinaryOperatorExpression(AsBinaryOperatorExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IClassConstantExpression AsClassConstantExpression:
                    IsHandled = true;
                    break;

                case ICloneOfExpression AsCloneOfExpression:
                    Result = GetComplexifiedCloneOfExpression(AsCloneOfExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IEntityExpression AsEntityExpression:
                    Result = GetComplexifiedEntityExpression(AsEntityExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IEqualityExpression AsEqualityExpression:
                    Result = GetComplexifiedEqualityExpression(AsEqualityExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IIndexQueryExpression AsIndexQueryExpression:
                    Result = GetComplexifiedIndexQueryExpression(AsIndexQueryExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IInitializedObjectExpression AsInitializedObjectExpression:
                    Result = GetComplexifiedInitializedObjectExpression(AsInitializedObjectExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IKeywordEntityExpression AsKeywordEntityExpression:
                case IKeywordExpression AsKeywordExpression:
                case IManifestCharacterExpression AsManifestCharacterExpression:
                case IManifestNumberExpression AsManifestNumberExpression:
                case IManifestStringExpression AsManifestStringExpression:
                    IsHandled = true;
                    break;

                case INewExpression AsNewExpression:
                    Result = GetComplexifiedNewExpression(AsNewExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IOldExpression AsOldExpression:
                    Result = GetComplexifiedOldExpression(AsOldExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IPrecursorExpression AsPrecursorExpression:
                    Result = GetComplexifiedPrecursorExpression(AsPrecursorExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IPrecursorIndexExpression AsPrecursorIndexExpression:
                    Result = GetComplexifiedPrecursorIndexExpression(AsPrecursorIndexExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IPreprocessorExpression AsPreprocessorExpression:
                    IsHandled = true;
                    break;

                case IQueryExpression AsQueryExpression:
                    Result = GetComplexifiedQueryExpression(AsQueryExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IResultOfExpression AsResultOfExpression:
                    Result = GetComplexifiedResultOfExpression(AsResultOfExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IUnaryNotExpression AsUnaryNotExpression:
                    Result = GetComplexifiedUnaryNotExpression(AsUnaryNotExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IUnaryOperatorExpression AsUnaryOperatorExpression:
                    Result = GetComplexifiedUnaryOperatorExpression(AsUnaryOperatorExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);

            return Result;
        }

        private static bool GetComplexifiedAgentExpression(IAgentExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (node.BaseType.IsAssigned && GetComplexifiedObjectType(node.BaseType.Item, out IList<IObjectType> ComplexifiedBaseTypeList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
                {
                    IIdentifier ClonedDelegated = (IIdentifier)DeepCloneNode(node.Delegated, cloneCommentGuid: false);
                    IAgentExpression NewAgentExpression = CreateAgentExpression(ClonedDelegated, ComplexifiedBaseType);
                    complexifiedExpressionList.Add(NewAgentExpression);
                }
            }
            else
            {
                IIdentifier Delegated = node.Delegated;
                string Text = Delegated.Text;

                if (Text.StartsWith("{"))
                {
                    int TypeNameIndex = Text.IndexOf("}");

                    if (TypeNameIndex > 1)
                    {
                        complexifiedExpressionList = new List<IExpression>();

                        string FeatureName = Text.Substring(TypeNameIndex + 1).Trim();
                        IIdentifier NewDelegated = CreateSimpleIdentifier(FeatureName);

                        string BaseTypeName = Text.Substring(1, TypeNameIndex - 1).Trim();
                        IIdentifier BaseTypeIdentifier = CreateSimpleIdentifier(BaseTypeName);
                        IObjectType NewBaseType = CreateSimpleType(SharingType.NotShared, BaseTypeIdentifier);

                        IAgentExpression NewAgentExpression = CreateAgentExpression(NewDelegated, NewBaseType);
                        complexifiedExpressionList.Add(NewAgentExpression);
                    }
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedBinaryConditionalExpression(IBinaryConditionalExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.LeftExpression, out IList<IExpression> ComplexifiedLeftExpressionList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
                {
                    IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                    IBinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ComplexifiedLeftExpression, node.Conditional, ClonedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryConditionalExpression);
                }
            }

            if (GetComplexifiedExpression(node.RightExpression, out IList<IExpression> ComplexifiedRightExpressionList))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                    IBinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ClonedLeftExpression, node.Conditional, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryConditionalExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedBinaryOperatorExpression(IBinaryOperatorExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.LeftExpression, out IList<IExpression> ComplexifiedLeftExpressionList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
                {
                    IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                    IIdentifier ClonedOperator = (IIdentifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                    IBinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ComplexifiedLeftExpression, ClonedOperator, ClonedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryOperatorExpression);
                }
            }

            if (GetComplexifiedExpression(node.RightExpression, out IList<IExpression> ComplexifiedRightExpressionList))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                    IIdentifier ClonedOperator = (IIdentifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                    IBinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, ClonedOperator, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryOperatorExpression);
                }
            }

            if (GetRenamedBinarySymbol(node.Operator, out IIdentifier RenamedOperator))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<IExpression>();

                IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                IBinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, RenamedOperator, ClonedRightExpression);
                complexifiedExpressionList.Add(NewBinaryOperatorExpression);
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedCloneOfExpression(ICloneOfExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    ICloneOfExpression NewCloneOfExpression = CreateCloneOfExpression(node.Type, ComplexifiedSource);
                    complexifiedExpressionList.Add(NewCloneOfExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedEntityExpression(IEntityExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedQualifiedName(node.Query, out IList<IQualifiedName> ComplexifiedQueryList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IQualifiedName ComplexifiedQuery in ComplexifiedQueryList)
                {
                    IEntityExpression NewEntityExpression = CreateEntityExpression(ComplexifiedQuery);
                    complexifiedExpressionList.Add(NewEntityExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedEqualityExpression(IEqualityExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.LeftExpression, out IList<IExpression> ComplexifiedLeftExpressionList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
                {
                    IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                    IEqualityExpression NewEqualityExpression = CreateEqualityExpression(ComplexifiedLeftExpression, node.Comparison, node.Equality, ClonedRightExpression);
                    complexifiedExpressionList.Add(NewEqualityExpression);
                }
            }

            if (GetComplexifiedExpression(node.RightExpression, out IList<IExpression> ComplexifiedRightExpressionList))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    IExpression ClonedLeftExpression = (IExpression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                    IEqualityExpression NewEqualityExpression = CreateEqualityExpression(ClonedLeftExpression, node.Comparison, node.Equality, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewEqualityExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedIndexQueryExpression(IIndexQueryExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.IndexedExpression, out IList<IExpression> ComplexifiedIndexedExpressionList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedIndexedExpression in ComplexifiedIndexedExpressionList)
                {
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IIndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ComplexifiedIndexedExpression, ClonedArgumentBlocks);
                    complexifiedExpressionList.Add(NewIndexQueryExpression);
                }
            }

            if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<IExpression>();

                IExpression ClonedIndexedExpression = (IExpression)DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false);
                IIndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ClonedIndexedExpression, ComplexifiedArgumentBlocks);
                complexifiedExpressionList.Add(NewIndexQueryExpression);
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedInitializedObjectExpression(IInitializedObjectExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedAssignmentArgumentBlockList(node.AssignmentBlocks, out IBlockList<IAssignmentArgument, AssignmentArgument> ComplexifiedAssignmentBlocks))
            {
                IIdentifier ClonedClassIdentifier = (IIdentifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);
                IInitializedObjectExpression NewInitializedObjectExpression = CreateInitializedObjectExpression(ClonedClassIdentifier, ComplexifiedAssignmentBlocks);
                complexifiedExpressionList = new List<IExpression>() { NewInitializedObjectExpression };
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedNewExpression(INewExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedNode(node.Object, out IList<INode> ComplexifiedObjectList) && ComplexifiedObjectList[0] is IQualifiedName AsComplexifiedObject)
            {
                INewExpression NewNewExpression = CreateNewExpression(AsComplexifiedObject);
                complexifiedExpressionList = new List<IExpression>() { NewNewExpression };
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedOldExpression(IOldExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedNode(node.Query, out IList<INode> ComplexifiedQueryList) && ComplexifiedQueryList[0] is IQualifiedName AsComplexifiedQuery)
            {
                IOldExpression NewOldExpression = CreateOldExpression(AsComplexifiedQuery);
                complexifiedExpressionList = new List<IExpression>() { NewOldExpression };
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedPrecursorExpression(IPrecursorExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<IObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IPrecursorExpression NewPrecursorExpression = CreatePrecursorExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                    complexifiedExpressionList.Add(NewPrecursorExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedPrecursorIndexExpression(IPrecursorIndexExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<IObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IPrecursorIndexExpression NewPrecursorIndexExpression = CreatePrecursorIndexExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                    complexifiedExpressionList.Add(NewPrecursorIndexExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedQueryExpression(IQueryExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedNode(node.Query, out IList<INode> ComplexifiedQueryList) && ComplexifiedQueryList[0] is IQualifiedName AsComplexifiedQuery)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IQueryExpression NewQueryExpression = CreateQueryExpression(AsComplexifiedQuery, ClonedArgumentBlocks);
                complexifiedExpressionList = new List<IExpression>() { NewQueryExpression };
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IQualifiedName ClonedQuery = (IQualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
                IQueryExpression NewQueryExpression = CreateQueryExpression(ClonedQuery, ComplexifiedArgumentBlocks);
                complexifiedExpressionList = new List<IExpression>() { NewQueryExpression };
            }
            else if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Query, out IQualifiedName NewQuery, out List<IArgument> ArgumentList))
            {
                IQualifiedName ClonedQuery = (IQualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
                IQueryExpression NewQueryExpression = CreateQueryExpression(ClonedQuery, ArgumentList);
                complexifiedExpressionList = new List<IExpression>() { NewQueryExpression };
            }
            else if (ComplexifyAsAgentExpression(node, out IAgentExpression ComplexifiedAgentExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedAgentExpression };
            else if (ComplexifyAsAssertionTagExpression(node, out IAssertionTagExpression ComplexifiedAssertionTagExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedAssertionTagExpression };
            else if (ComplexifyAsBinaryAndConditionalExpression(node, out IBinaryConditionalExpression ComplexifiedBinaryAndConditionalExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedBinaryAndConditionalExpression };
            else if (ComplexifyAsBinaryOrConditionalExpression(node, out IBinaryConditionalExpression ComplexifiedBinaryOrConditionalExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedBinaryOrConditionalExpression };
            else if (ComplexifyAsBinaryOperatorExpression(node, out IBinaryOperatorExpression ComplexifiedBinaryOperatorExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedBinaryOperatorExpression };
            else if (ComplexifyAsClassConstantExpression(node, out IClassConstantExpression ComplexifiedClassConstantExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedClassConstantExpression };
            else if (ComplexifyAsCloneOfExpression(node, out ICloneOfExpression ComplexifiedCloneOfExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedCloneOfExpression };
            else if (ComplexifyAsEntityExpression(node, out IEntityExpression ComplexifiedEntityExpression, out IKeywordEntityExpression ComplexifiedKeywordEntityExpression))
            {
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedEntityExpression };
                if (ComplexifiedKeywordEntityExpression != null)
                    complexifiedExpressionList.Add(ComplexifiedKeywordEntityExpression);
            }
            else if (ComplexifyAsEqualExpression(node, out IEqualityExpression ComplexifiedEqualityExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedEqualityExpression };
            else if (ComplexifyAsDifferentExpression(node, out IEqualityExpression ComplexifiedDifferentExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedDifferentExpression };
            else if (ComplexifyAsIndexQueryExpression(node, out IIndexQueryExpression ComplexifiedIndexQueryExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedIndexQueryExpression };
            else if (ComplexifyAsInitializedObjectExpression(node, out IInitializedObjectExpression ComplexifiedInitializedObjectExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedInitializedObjectExpression };
            else if (ComplexifyAsKeywordExpression(node, out IKeywordExpression ComplexifiedKeywordExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedKeywordExpression };
            else if (ComplexifyAsManifestCharacterExpression(node, out IManifestCharacterExpression ComplexifiedManifestCharacterExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedManifestCharacterExpression };
            else if (ComplexifyAsManifestNumberExpression(node, out IManifestNumberExpression ComplexifiedManifestNumberExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedManifestNumberExpression };
            else if (ComplexifyAsManifestStringExpression(node, out IManifestStringExpression ComplexifiedManifestStringExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedManifestStringExpression };
            else if (ComplexifyAsNewExpression(node, out INewExpression ComplexifiedNewExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedNewExpression };
            else if (ComplexifyAsOldExpression(node, out IOldExpression ComplexifiedOldExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedOldExpression };
            else if (ComplexifyAsPrecursorExpression(node, out IPrecursorExpression ComplexifiedPrecursorExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedPrecursorExpression };
            else if (ComplexifyAsPrecursorIndexExpression(node, out IPrecursorIndexExpression ComplexifiedPrecursorIndexExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedPrecursorIndexExpression };
            else if (ComplexifyAsPreprocessorExpression(node, out IPreprocessorExpression ComplexifiedPreprocessorExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedPreprocessorExpression };
            else if (ComplexifyAsResultOfExpression(node, out IResultOfExpression ComplexifiedResultOfExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedResultOfExpression };
            else if (ComplexifyAsUnaryNotExpression(node, out IUnaryNotExpression ComplexifiedUnaryNotExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedUnaryNotExpression };
            else if (ComplexifyAsUnaryOperatorExpression(node, out IUnaryOperatorExpression ComplexifiedUnaryOperatorExpression))
                complexifiedExpressionList = new List<IExpression>() { ComplexifiedUnaryOperatorExpression };

            return complexifiedExpressionList != null;
        }

        private static bool ComplexifyAsAgentExpression(IQueryExpression node, out IAgentExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node) && ParsePattern(node, "agent", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                AfterText = AfterText.Trim();

                IIdentifier Delegated = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateAgentExpression(Delegated);
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

            string[] Patterns = new string[] { "+", "-", "/", "*", ">", "<", ">=", "<=" };

            foreach (string Pattern in Patterns)
            {
                if (ParsePattern(node, $" {Pattern} ", out string BeforeText, out string AfterText))
                {
                    CloneComplexifiedExpression(node, BeforeText, AfterText, out IExpression LeftExpression, out IExpression RightExpression);
                    IIdentifier Operator = CreateSimpleIdentifier(Pattern);
                    complexifiedNode = CreateBinaryOperatorExpression(LeftExpression, Operator, RightExpression);
                    break;
                }
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

        private static bool ComplexifyAsEntityExpression(IQueryExpression node, out IEntityExpression complexifiedNode, out IKeywordEntityExpression complexifiedKeywordNode)
        {
            complexifiedNode = null;
            complexifiedKeywordNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "entity ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                string Text = AfterText.Trim();

                IQualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as IQualifiedName;
                Debug.Assert(ClonedQuery != null);
                Debug.Assert(ClonedQuery.Path.Count > 0);

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", Text);

                complexifiedNode = CreateEntityExpression(ClonedQuery);

                if (StringToKeyword(Text, out Keyword Value))
                    complexifiedKeywordNode = CreateKeywordEntityExpression(Value);
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

        private static bool ComplexifyAsKeywordEntityExpression(IQueryExpression node, out IKeywordEntityExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "entity ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                string Text = AfterText.Trim();

                if (StringToKeyword(Text, out Keyword Value) && Value == Keyword.Indexer)
                    complexifiedNode = CreateKeywordEntityExpression(Value);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsKeywordExpression(IQueryExpression node, out IKeywordExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (StringToKeyword(Text, out Keyword Value))
                    complexifiedNode = CreateKeywordExpression(Value);
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

        private static bool GetComplexifiedResultOfExpression(IResultOfExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IResultOfExpression NewResultOfExpression = CreateResultOfExpression(ComplexifiedSource);
                    complexifiedExpressionList.Add(NewResultOfExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedUnaryNotExpression(IUnaryNotExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.RightExpression, out IList<IExpression> ComplexifiedRightExpressionList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    IUnaryNotExpression NewUnaryNotExpression = CreateUnaryNotExpression(ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewUnaryNotExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedUnaryOperatorExpression(IUnaryOperatorExpression node, out IList<IExpression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.RightExpression, out IList<IExpression> ComplexifiedRightExpressionList))
            {
                complexifiedExpressionList = new List<IExpression>();

                foreach (IExpression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    IIdentifier ClonedOperator = (IIdentifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                    IUnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(ClonedOperator, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewUnaryOperatorExpression);
                }
            }

            if (GetRenamedUnarySymbol(node.Operator, out IIdentifier RenamedOperator))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<IExpression>();

                IExpression ClonedRightExpression = (IExpression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                IUnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(RenamedOperator, ClonedRightExpression);
                complexifiedExpressionList.Add(NewUnaryOperatorExpression);
            }

            return complexifiedExpressionList != null;
        }
    }
}
