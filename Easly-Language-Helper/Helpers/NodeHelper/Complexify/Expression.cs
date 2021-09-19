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
            switch (node)
            {
                case BinaryConditionalExpression AsBinaryConditionalExpression:
                    return GetComplexifiedBinaryConditionalExpression(AsBinaryConditionalExpression, out complexifiedExpressionList);

                case BinaryOperatorExpression AsBinaryOperatorExpression:
                    return GetComplexifiedBinaryOperatorExpression(AsBinaryOperatorExpression, out complexifiedExpressionList);

                case EqualityExpression AsEqualityExpression:
                    return GetComplexifiedEqualityExpression(AsEqualityExpression, out complexifiedExpressionList);

                case IndexQueryExpression AsIndexQueryExpression:
                    return GetComplexifiedIndexQueryExpression(AsIndexQueryExpression, out complexifiedExpressionList);

                case InitializedObjectExpression AsInitializedObjectExpression:
                    return GetComplexifiedInitializedObjectExpression(AsInitializedObjectExpression, out complexifiedExpressionList);

                case PrecursorIndexExpression AsPrecursorIndexExpression:
                    return GetComplexifiedPrecursorIndexExpression(AsPrecursorIndexExpression, out complexifiedExpressionList);

                case QueryExpression AsQueryExpression:
                    return GetComplexifiedQueryExpression(AsQueryExpression, out complexifiedExpressionList);

                case UnaryNotExpression AsUnaryNotExpression:
                    return GetComplexifiedUnaryNotExpression(AsUnaryNotExpression, out complexifiedExpressionList);

                case UnaryOperatorExpression AsUnaryOperatorExpression:
                    return GetComplexifiedUnaryOperatorExpression(AsUnaryOperatorExpression, out complexifiedExpressionList);

                default:
                    return GetComplexifiedExpressionSingle1(node, out complexifiedExpressionList);
            }
        }

        private static bool GetComplexifiedExpressionSingle1(Expression node, out IList<Expression> complexifiedExpressionList)
        {
            switch (node)
            {
                case AgentExpression AsAgentExpression:
                    return GetComplexifiedAgentExpression(AsAgentExpression, out complexifiedExpressionList);

                case CloneOfExpression AsCloneOfExpression:
                    return GetComplexifiedCloneOfExpression(AsCloneOfExpression, out complexifiedExpressionList);

                case EntityExpression AsEntityExpression:
                    return GetComplexifiedEntityExpression(AsEntityExpression, out complexifiedExpressionList);

                case NewExpression AsNewExpression:
                    return GetComplexifiedNewExpression(AsNewExpression, out complexifiedExpressionList);

                case OldExpression AsOldExpression:
                    return GetComplexifiedOldExpression(AsOldExpression, out complexifiedExpressionList);

                case PrecursorExpression AsPrecursorExpression:
                    return GetComplexifiedPrecursorExpression(AsPrecursorExpression, out complexifiedExpressionList);

                case ResultOfExpression AsResultOfExpression:
                    return GetComplexifiedResultOfExpression(AsResultOfExpression, out complexifiedExpressionList);

                default:
                    return GetComplexifiedExpressionSingle2(node, out complexifiedExpressionList);
            }
        }

        private static bool GetComplexifiedExpressionSingle2(Expression node, out IList<Expression> complexifiedExpressionList)
        {
            complexifiedExpressionList = null;
            bool IsHandled = false;

            switch (node)
            {
                case AssertionTagExpression AsAssertionTagExpression:
                case ClassConstantExpression AsClassConstantExpression:
                case KeywordEntityExpression AsKeywordEntityExpression:
                case KeywordExpression AsKeywordExpression:
                case ManifestCharacterExpression AsManifestCharacterExpression:
                case ManifestNumberExpression AsManifestNumberExpression:
                case ManifestStringExpression AsManifestStringExpression:
                case PreprocessorExpression AsPreprocessorExpression:
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(Expression)} have been handled");

            return false;
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
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ComplexifiedIndexedExpression, ClonedArgumentBlocks);
                    complexifiedExpressionList.Add(NewIndexQueryExpression);
                }
            }

            if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
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

            if (GetComplexifiedAssignmentArgumentBlockList(node.AssignmentBlocks, out IBlockList<AssignmentArgument> ComplexifiedAssignmentBlocks))
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
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
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
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    PrecursorIndexExpression NewPrecursorIndexExpression = CreatePrecursorIndexExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                    complexifiedExpressionList.Add(NewPrecursorIndexExpression);
                }
            }

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
