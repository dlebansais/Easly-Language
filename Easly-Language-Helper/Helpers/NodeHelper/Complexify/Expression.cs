#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;
    using EaslyNumber;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedExpression(Expression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;
            bool Result = false;
            bool IsHandled = false;

            switch (node)
            {
                case AgentExpression AsAgentExpression:
                    Result = GetComplexifiedAgentExpression(AsAgentExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case AssertionTagExpression AsAssertionTagExpression:
                    IsHandled = true;
                    break;

                case BinaryConditionalExpression AsBinaryConditionalExpression:
                    Result = GetComplexifiedBinaryConditionalExpression(AsBinaryConditionalExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case BinaryOperatorExpression AsBinaryOperatorExpression:
                    Result = GetComplexifiedBinaryOperatorExpression(AsBinaryOperatorExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case ClassConstantExpression AsClassConstantExpression:
                    IsHandled = true;
                    break;

                case CloneOfExpression AsCloneOfExpression:
                    Result = GetComplexifiedCloneOfExpression(AsCloneOfExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case EntityExpression AsEntityExpression:
                    Result = GetComplexifiedEntityExpression(AsEntityExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case EqualityExpression AsEqualityExpression:
                    Result = GetComplexifiedEqualityExpression(AsEqualityExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case IndexQueryExpression AsIndexQueryExpression:
                    Result = GetComplexifiedIndexQueryExpression(AsIndexQueryExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case InitializedObjectExpression AsInitializedObjectExpression:
                    Result = GetComplexifiedInitializedObjectExpression(AsInitializedObjectExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case KeywordEntityExpression AsKeywordEntityExpression:
                case KeywordExpression AsKeywordExpression:
                case ManifestCharacterExpression AsManifestCharacterExpression:
                case ManifestNumberExpression AsManifestNumberExpression:
                case ManifestStringExpression AsManifestStringExpression:
                    IsHandled = true;
                    break;

                case NewExpression AsNewExpression:
                    Result = GetComplexifiedNewExpression(AsNewExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case OldExpression AsOldExpression:
                    Result = GetComplexifiedOldExpression(AsOldExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case PrecursorExpression AsPrecursorExpression:
                    Result = GetComplexifiedPrecursorExpression(AsPrecursorExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case PrecursorIndexExpression AsPrecursorIndexExpression:
                    Result = GetComplexifiedPrecursorIndexExpression(AsPrecursorIndexExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case PreprocessorExpression AsPreprocessorExpression:
                    IsHandled = true;
                    break;

                case QueryExpression AsQueryExpression:
                    Result = GetComplexifiedQueryExpression(AsQueryExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case ResultOfExpression AsResultOfExpression:
                    Result = GetComplexifiedResultOfExpression(AsResultOfExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case UnaryNotExpression AsUnaryNotExpression:
                    Result = GetComplexifiedUnaryNotExpression(AsUnaryNotExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;

                case UnaryOperatorExpression AsUnaryOperatorExpression:
                    Result = GetComplexifiedUnaryOperatorExpression(AsUnaryOperatorExpression, out complexifiedExpressionList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(Expression)} have been handled");

            return Result;
        }

        private static bool GetComplexifiedAgentExpression(AgentExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (node.BaseType.IsAssigned && GetComplexifiedObjectType(node.BaseType.Item, out IList<ObjectType> ComplexifiedBaseTypeList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (ObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
                {
                    Identifier ClonedDelegated = (Identifier)DeepCloneNode(node.Delegated, cloneCommentGuid: false);
                    AgentExpression NewAgentExpression = CreateAgentExpression(ClonedDelegated, ComplexifiedBaseType);
                    complexifiedExpressionList.Add(NewAgentExpression);
                }
            }
            else
            {
                Identifier Delegated = node.Delegated;
                string Text = Delegated.Text;

                if (Text.StartsWith("{", StringComparison.InvariantCulture))
                {
                    int TypeNameIndex = Text.IndexOf("}", StringComparison.InvariantCulture);

                    if (TypeNameIndex > 1)
                    {
                        complexifiedExpressionList = new List<Expression>();

                        string FeatureName = Text.Substring(TypeNameIndex + 1).Trim();
                        Identifier NewDelegated = CreateSimpleIdentifier(FeatureName);

                        string BaseTypeName = Text.Substring(1, TypeNameIndex - 1).Trim();
                        Identifier BaseTypeIdentifier = CreateSimpleIdentifier(BaseTypeName);
                        ObjectType NewBaseType = CreateSimpleType(SharingType.NotShared, BaseTypeIdentifier);

                        AgentExpression NewAgentExpression = CreateAgentExpression(NewDelegated, NewBaseType);
                        complexifiedExpressionList.Add(NewAgentExpression);
                    }
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedBinaryConditionalExpression(BinaryConditionalExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.LeftExpression, out IList<Expression> ComplexifiedLeftExpressionList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
                {
                    Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                    BinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ComplexifiedLeftExpression, node.Conditional, ClonedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryConditionalExpression);
                }
            }

            if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                    BinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ClonedLeftExpression, node.Conditional, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryConditionalExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedBinaryOperatorExpression(BinaryOperatorExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.LeftExpression, out IList<Expression> ComplexifiedLeftExpressionList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
                {
                    Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                    Identifier ClonedOperator = (Identifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                    BinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ComplexifiedLeftExpression, ClonedOperator, ClonedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryOperatorExpression);
                }
            }

            if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                    Identifier ClonedOperator = (Identifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                    BinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, ClonedOperator, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewBinaryOperatorExpression);
                }
            }

            if (GetRenamedBinarySymbol(node.Operator, out Identifier RenamedOperator))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<Expression>();

                Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                BinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, RenamedOperator, ClonedRightExpression);
                complexifiedExpressionList.Add(NewBinaryOperatorExpression);
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedCloneOfExpression(CloneOfExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    CloneOfExpression NewCloneOfExpression = CreateCloneOfExpression(node.Type, ComplexifiedSource);
                    complexifiedExpressionList.Add(NewCloneOfExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedEntityExpression(EntityExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedQualifiedName(node.Query, out IList<QualifiedName> ComplexifiedQueryList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (QualifiedName ComplexifiedQuery in ComplexifiedQueryList)
                {
                    EntityExpression NewEntityExpression = CreateEntityExpression(ComplexifiedQuery);
                    complexifiedExpressionList.Add(NewEntityExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedEqualityExpression(EqualityExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.LeftExpression, out IList<Expression> ComplexifiedLeftExpressionList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
                {
                    Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                    EqualityExpression NewEqualityExpression = CreateEqualityExpression(ComplexifiedLeftExpression, node.Comparison, node.Equality, ClonedRightExpression);
                    complexifiedExpressionList.Add(NewEqualityExpression);
                }
            }

            if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                    EqualityExpression NewEqualityExpression = CreateEqualityExpression(ClonedLeftExpression, node.Comparison, node.Equality, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewEqualityExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedIndexQueryExpression(IndexQueryExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.IndexedExpression, out IList<Expression> ComplexifiedIndexedExpressionList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedIndexedExpression in ComplexifiedIndexedExpressionList)
                {
                    BlockList<Argument> ClonedArgumentBlocks = (BlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ComplexifiedIndexedExpression, ClonedArgumentBlocks);
                    complexifiedExpressionList.Add(NewIndexQueryExpression);
                }
            }

            if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out BlockList<Argument> ComplexifiedArgumentBlocks))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<Expression>();

                Expression ClonedIndexedExpression = (Expression)DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false);
                IndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ClonedIndexedExpression, ComplexifiedArgumentBlocks);
                complexifiedExpressionList.Add(NewIndexQueryExpression);
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedInitializedObjectExpression(InitializedObjectExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedAssignmentArgumentBlockList(node.AssignmentBlocks, out BlockList<AssignmentArgument> ComplexifiedAssignmentBlocks))
            {
                Identifier ClonedClassIdentifier = (Identifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);
                InitializedObjectExpression NewInitializedObjectExpression = CreateInitializedObjectExpression(ClonedClassIdentifier, ComplexifiedAssignmentBlocks);
                complexifiedExpressionList = new List<Expression>() { NewInitializedObjectExpression };
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedNewExpression(NewExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedNode(node.Object, out IList<Node> ComplexifiedObjectList) && ComplexifiedObjectList[0] is QualifiedName AsComplexifiedObject)
            {
                NewExpression NewNewExpression = CreateNewExpression(AsComplexifiedObject);
                complexifiedExpressionList = new List<Expression>() { NewNewExpression };
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedOldExpression(OldExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedNode(node.Query, out IList<Node> ComplexifiedQueryList) && ComplexifiedQueryList[0] is QualifiedName AsComplexifiedQuery)
            {
                OldExpression NewOldExpression = CreateOldExpression(AsComplexifiedQuery);
                complexifiedExpressionList = new List<Expression>() { NewOldExpression };
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedPrecursorExpression(PrecursorExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    BlockList<Argument> ClonedArgumentBlocks = (BlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    PrecursorExpression NewPrecursorExpression = CreatePrecursorExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                    complexifiedExpressionList.Add(NewPrecursorExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedPrecursorIndexExpression(PrecursorIndexExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    BlockList<Argument> ClonedArgumentBlocks = (BlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    PrecursorIndexExpression NewPrecursorIndexExpression = CreatePrecursorIndexExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                    complexifiedExpressionList.Add(NewPrecursorIndexExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedQueryExpression(QueryExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (ComplexifyAsManifestNumberExpression(node, out ManifestNumberExpression ComplexifiedManifestNumberExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedManifestNumberExpression };
            else if (GetComplexifiedNode(node.Query, out IList<Node> ComplexifiedQueryList) && ComplexifiedQueryList[0] is QualifiedName AsComplexifiedQuery)
            {
                BlockList<Argument> ClonedArgumentBlocks = (BlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                QueryExpression NewQueryExpression = CreateQueryExpression(AsComplexifiedQuery, ClonedArgumentBlocks);
                complexifiedExpressionList = new List<Expression>() { NewQueryExpression };
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out BlockList<Argument> ComplexifiedArgumentBlocks))
            {
                QualifiedName ClonedQuery = (QualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
                QueryExpression NewQueryExpression = CreateQueryExpression(ClonedQuery, ComplexifiedArgumentBlocks);
                complexifiedExpressionList = new List<Expression>() { NewQueryExpression };
            }
            else if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Query, out QualifiedName NewQuery, out List<Argument> ArgumentList))
            {
                QueryExpression NewQueryExpression = CreateQueryExpression(NewQuery, ArgumentList);
                complexifiedExpressionList = new List<Expression>() { NewQueryExpression };
            }
            else if (ComplexifyAsAgentExpression(node, out AgentExpression ComplexifiedAgentExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedAgentExpression };
            else if (ComplexifyAsAssertionTagExpression(node, out AssertionTagExpression ComplexifiedAssertionTagExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedAssertionTagExpression };
            else if (ComplexifyAsBinaryAndConditionalExpression(node, out BinaryConditionalExpression ComplexifiedBinaryAndConditionalExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedBinaryAndConditionalExpression };
            else if (ComplexifyAsBinaryOrConditionalExpression(node, out BinaryConditionalExpression ComplexifiedBinaryOrConditionalExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedBinaryOrConditionalExpression };
            else if (ComplexifyAsBinaryXorConditionalExpression(node, out BinaryConditionalExpression ComplexifiedBinaryXorConditionalExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedBinaryXorConditionalExpression };
            else if (ComplexifyAsBinaryImpliesConditionalExpression(node, out BinaryConditionalExpression ComplexifiedBinaryImpliesConditionalExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedBinaryImpliesConditionalExpression };
            else if (ComplexifyAsBinaryOperatorExpression(node, out BinaryOperatorExpression ComplexifiedBinaryOperatorExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedBinaryOperatorExpression };
            else if (ComplexifyAsClassConstantExpression(node, out ClassConstantExpression ComplexifiedClassConstantExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedClassConstantExpression };
            else if (ComplexifyAsCloneOfExpression(node, out CloneOfExpression ComplexifiedCloneOfExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedCloneOfExpression };
            else if (ComplexifyAsEntityExpression(node, out EntityExpression ComplexifiedEntityExpression, out KeywordEntityExpression ComplexifiedKeywordEntityExpression))
            {
                complexifiedExpressionList = new List<Expression>() { ComplexifiedEntityExpression };
                if (ComplexifiedKeywordEntityExpression != null)
                    complexifiedExpressionList.Add(ComplexifiedKeywordEntityExpression);
            }
            else if (ComplexifyAsEqualExpression(node, out EqualityExpression ComplexifiedEqualityExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedEqualityExpression };
            else if (ComplexifyAsDifferentExpression(node, out EqualityExpression ComplexifiedDifferentExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedDifferentExpression };
            else if (ComplexifyAsIndexQueryExpression(node, out IndexQueryExpression ComplexifiedIndexQueryExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedIndexQueryExpression };
            else if (ComplexifyAsInitializedObjectExpression(node, out InitializedObjectExpression ComplexifiedInitializedObjectExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedInitializedObjectExpression };
            else if (ComplexifyAsKeywordExpression(node, out KeywordExpression ComplexifiedKeywordExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedKeywordExpression };
            else if (ComplexifyAsManifestCharacterExpression(node, out ManifestCharacterExpression ComplexifiedManifestCharacterExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedManifestCharacterExpression };
            else if (ComplexifyAsManifestStringExpression(node, out ManifestStringExpression ComplexifiedManifestStringExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedManifestStringExpression };
            else if (ComplexifyAsNewExpression(node, out NewExpression ComplexifiedNewExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedNewExpression };
            else if (ComplexifyAsOldExpression(node, out OldExpression ComplexifiedOldExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedOldExpression };
            else if (ComplexifyAsPrecursorExpression(node, out PrecursorExpression ComplexifiedPrecursorExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedPrecursorExpression };
            else if (ComplexifyAsPrecursorIndexExpression(node, out PrecursorIndexExpression ComplexifiedPrecursorIndexExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedPrecursorIndexExpression };
            else if (ComplexifyAsPreprocessorExpression(node, out PreprocessorExpression ComplexifiedPreprocessorExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedPreprocessorExpression };
            else if (ComplexifyAsResultOfExpression(node, out ResultOfExpression ComplexifiedResultOfExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedResultOfExpression };
            else if (ComplexifyAsUnaryNotExpression(node, out UnaryNotExpression ComplexifiedUnaryNotExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedUnaryNotExpression };
            else if (ComplexifyAsUnaryOperatorExpression(node, out UnaryOperatorExpression ComplexifiedUnaryOperatorExpression))
                complexifiedExpressionList = new List<Expression>() { ComplexifiedUnaryOperatorExpression };

            return complexifiedExpressionList != null;
        }

        private static bool ComplexifyAsAgentExpression(QueryExpression node, out AgentExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node) && ParsePattern(node, "agent", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                AfterText = AfterText.Trim();

                Identifier Delegated = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateAgentExpression(Delegated);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsAssertionTagExpression(QueryExpression node, out AssertionTagExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node) && ParsePattern(node, "tag ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                Identifier TagIdentifier = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateAssertionTagExpression(TagIdentifier);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsBinaryAndConditionalExpression(QueryExpression node, out BinaryConditionalExpression complexifiedNode)
        {
            return ComplexifyAsBinaryConditionalExpression(node, " and ", ConditionalTypes.And, out complexifiedNode);
        }

        private static bool ComplexifyAsBinaryOrConditionalExpression(QueryExpression node, out BinaryConditionalExpression complexifiedNode)
        {
            return ComplexifyAsBinaryConditionalExpression(node, " or ", ConditionalTypes.Or, out complexifiedNode);
        }

        private static bool ComplexifyAsBinaryXorConditionalExpression(QueryExpression node, out BinaryConditionalExpression complexifiedNode)
        {
            return ComplexifyAsBinaryConditionalExpression(node, " xor ", ConditionalTypes.Xor, out complexifiedNode);
        }

        private static bool ComplexifyAsBinaryImpliesConditionalExpression(QueryExpression node, out BinaryConditionalExpression complexifiedNode)
        {
            return ComplexifyAsBinaryConditionalExpression(node, " => ", ConditionalTypes.Implies, out complexifiedNode) ||
                   ComplexifyAsBinaryConditionalExpression(node, " ⇒ ", ConditionalTypes.Implies, out complexifiedNode);
        }

        private static bool ComplexifyAsBinaryConditionalExpression(QueryExpression node, string pattern, ConditionalTypes conditionalType, out BinaryConditionalExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, pattern, out string BeforeText, out string AfterText))
            {
                CloneComplexifiedExpression(node, BeforeText, AfterText, out Expression LeftExpression, out Expression RightExpression);
                complexifiedNode = CreateBinaryConditionalExpression(LeftExpression, conditionalType, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsBinaryOperatorExpression(QueryExpression node, out BinaryOperatorExpression complexifiedNode)
        {
            complexifiedNode = null;

            string[] Patterns = new string[] { "+", "-", "/", "*", ">>", "<<", ">=", "<=", ">", "<" };

            foreach (string Pattern in Patterns)
            {
                if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText))
                {
                    CloneComplexifiedExpression(node, BeforeText, AfterText, out Expression LeftExpression, out Expression RightExpression);
                    Identifier Operator = CreateSimpleIdentifier(Pattern);
                    complexifiedNode = CreateBinaryOperatorExpression(LeftExpression, Operator, RightExpression);
                    break;
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsClassConstantExpression(QueryExpression node, out ClassConstantExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text.StartsWith("{", StringComparison.InvariantCulture))
                {
                    int ClassNameIndex = Text.IndexOf("}", StringComparison.InvariantCulture);

                    if (ClassNameIndex > 2)
                    {
                        Identifier ClassIdentifier = CreateSimpleIdentifier(Text.Substring(1, ClassNameIndex - 1));
                        Identifier ConstantIdentifier = CreateSimpleIdentifier(Text.Substring(ClassNameIndex + 1));
                        complexifiedNode = CreateClassConstantExpression(ClassIdentifier, ConstantIdentifier);
                    }
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsCloneOfExpression(QueryExpression node, out CloneOfExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "clone of ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedExpression(node, AfterText, out Expression Source);
                complexifiedNode = CreateCloneOfExpression(CloneType.Shallow, Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsEntityExpression(QueryExpression node, out EntityExpression complexifiedNode, out KeywordEntityExpression complexifiedKeywordNode)
        {
            complexifiedNode = null;
            complexifiedKeywordNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "entity ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                string Text = AfterText.Trim();

                QualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as QualifiedName;
                Debug.Assert(ClonedQuery != null, $"The clone is always a {nameof(QualifiedName)}");
                Debug.Assert(ClonedQuery.Path.Count > 0, $"The clone always has at least one element");

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", Text);

                complexifiedNode = CreateEntityExpression(ClonedQuery);

                if (StringToKeyword(Text, out Keyword Value))
                    complexifiedKeywordNode = CreateKeywordEntityExpression(Value);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsEqualExpression(QueryExpression node, out EqualityExpression complexifiedNode)
        {
            return ComplexifyEqualityExpression(node, " = ", ComparisonType.Equal, out complexifiedNode);
        }

        private static bool ComplexifyAsDifferentExpression(QueryExpression node, out EqualityExpression complexifiedNode)
        {
            return ComplexifyEqualityExpression(node, " /= ", ComparisonType.Different, out complexifiedNode);
        }

        private static bool ComplexifyEqualityExpression(QueryExpression node, string pattern, ComparisonType comparisonType, out EqualityExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, pattern, out string BeforeText, out string AfterText))
            {
                CloneComplexifiedExpression(node, BeforeText, AfterText, out Expression LeftExpression, out Expression RightExpression);
                complexifiedNode = CreateEqualityExpression(LeftExpression, comparisonType, EqualityType.Physical, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexQueryExpression(QueryExpression node, out IndexQueryExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Query, '[', ']', out QualifiedName NewQuery, out List<Argument> ArgumentList))
            {
                Expression IndexedExpression = CreateQueryExpression(NewQuery, new List<Argument>());
                complexifiedNode = CreateIndexQueryExpression(IndexedExpression, ArgumentList);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsInitializedObjectExpression(QueryExpression node, out InitializedObjectExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (ParsePattern(Text, "{", out string ClassText, out string InitText))
                {
                    if (ParsePattern(InitText, ":=", out string ParameterText, out string SourceText) && SourceText.EndsWith("}", StringComparison.InvariantCulture))
                    {
                        Identifier ClassIdentifier = CreateSimpleIdentifier(ClassText);

                        Identifier ParameterIdentifier = CreateSimpleIdentifier(ParameterText);
                        Expression Source = CreateSimpleQueryExpression(SourceText.Substring(0, SourceText.Length - 1));
                        AssignmentArgument FirstArgument = CreateAssignmentArgument(new List<Identifier>() { ParameterIdentifier }, Source);

                        List<AssignmentArgument> ArgumentList = new List<AssignmentArgument>();
                        ArgumentList.Add(FirstArgument);

                        complexifiedNode = CreateInitializedObjectExpression(ClassIdentifier, ArgumentList);
                    }
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsKeywordEntityExpression(QueryExpression node, out KeywordEntityExpression complexifiedNode)
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

        private static bool ComplexifyAsKeywordExpression(QueryExpression node, out KeywordExpression complexifiedNode)
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

        private static bool ComplexifyAsManifestCharacterExpression(QueryExpression node, out ManifestCharacterExpression complexifiedNode)
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

        private static bool ComplexifyAsManifestNumberExpression(QueryExpression node, out ManifestNumberExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (IsQuerySimple(node))
            {
                string Text = node.Query.Path[0].Text;

                if (Text.Length >= 1)
                {
                    FormattedNumber fn = FormattedNumber.Parse(Text);
                    if (fn.IsValid)
                        complexifiedNode = CreateSimpleManifestNumberExpression(Text);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsManifestStringExpression(QueryExpression node, out ManifestStringExpression complexifiedNode)
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

        private static bool ComplexifyAsNewExpression(QueryExpression node, out NewExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "new ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                QualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as QualifiedName;
                Debug.Assert(ClonedQuery != null, $"The clone is always a {nameof(QualifiedName)}");
                Debug.Assert(ClonedQuery.Path.Count > 0, $"The clone always has at least one element");

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

                complexifiedNode = CreateNewExpression(ClonedQuery);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsOldExpression(QueryExpression node, out OldExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "old ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                QualifiedName ClonedQuery = DeepCloneNode(node.Query, cloneCommentGuid: false) as QualifiedName;
                Debug.Assert(ClonedQuery != null, $"The clone is always a {nameof(QualifiedName)}");
                Debug.Assert(ClonedQuery.Path.Count > 0, $"The clone always has at least one element");

                NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

                complexifiedNode = CreateOldExpression(ClonedQuery);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorExpression(QueryExpression node, out PrecursorExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Query.Path.Count == 1)
            {
                string Text = node.Query.Path[0].Text;

                if (Text == "precursor")
                {
                    QueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as QueryExpression;
                    complexifiedNode = CreatePrecursorExpression(ClonedQuery.ArgumentBlocks);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorIndexExpression(QueryExpression node, out PrecursorIndexExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Query.Path.Count == 1 && node.ArgumentBlocks.NodeBlockList.Count > 0)
            {
                string Text = node.Query.Path[0].Text;

                if (Text == "precursor[]")
                {
                    QueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as QueryExpression;
                    complexifiedNode = CreatePrecursorIndexExpression(ClonedQuery.ArgumentBlocks);
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPreprocessorExpression(QueryExpression node, out PreprocessorExpression complexifiedNode)
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

        private static bool ComplexifyAsResultOfExpression(QueryExpression node, out ResultOfExpression complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "result of ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedExpression(node, AfterText, out Expression Source);
                complexifiedNode = CreateResultOfExpression(Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsUnaryNotExpression(QueryExpression node, out UnaryNotExpression complexifiedNode)
        {
            Debug.Assert(node.Query.Path.Count > 0, $"{nameof(node)} has at least one element in the query path");

            complexifiedNode = null;

            string Text = node.Query.Path[0].Text;

            if (Text.StartsWith("not ", StringComparison.InvariantCulture))
            {
                CloneComplexifiedExpression(node, Text.Substring(4), out Expression RightExpression);
                complexifiedNode = CreateUnaryNotExpression(RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsUnaryOperatorExpression(QueryExpression node, out UnaryOperatorExpression complexifiedNode)
        {
            Debug.Assert(node.Query.Path.Count > 0, $"{nameof(node)} has at least one element in the query path");

            complexifiedNode = null;

            string Text = node.Query.Path[0].Text;
            string Pattern = "-";

            if (Text.StartsWith(Pattern + " ", StringComparison.InvariantCulture))
            {
                Identifier OperatorName = CreateSimpleIdentifier(Pattern);

                CloneComplexifiedExpression(node, Text.Substring(Pattern.Length + 1), out Expression RightExpression);
                complexifiedNode = CreateUnaryOperatorExpression(OperatorName, RightExpression);
            }

            return complexifiedNode != null;
        }

        private static bool GetComplexifiedResultOfExpression(ResultOfExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    ResultOfExpression NewResultOfExpression = CreateResultOfExpression(ComplexifiedSource);
                    complexifiedExpressionList.Add(NewResultOfExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedUnaryNotExpression(UnaryNotExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    UnaryNotExpression NewUnaryNotExpression = CreateUnaryNotExpression(ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewUnaryNotExpression);
                }
            }

            return complexifiedExpressionList != null;
        }

        private static bool GetComplexifiedUnaryOperatorExpression(UnaryOperatorExpression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;

            if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
            {
                complexifiedExpressionList = new List<Expression>();

                foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
                {
                    Identifier ClonedOperator = (Identifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                    UnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(ClonedOperator, ComplexifiedRightExpression);
                    complexifiedExpressionList.Add(NewUnaryOperatorExpression);
                }
            }

            if (GetRenamedUnarySymbol(node.Operator, out Identifier RenamedOperator))
            {
                if (complexifiedExpressionList == null)
                    complexifiedExpressionList = new List<Expression>();

                Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                UnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(RenamedOperator, ClonedRightExpression);
                complexifiedExpressionList.Add(NewUnaryOperatorExpression);
            }

            return complexifiedExpressionList != null;
        }
    }
}
