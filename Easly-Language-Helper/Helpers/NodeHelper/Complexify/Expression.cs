namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
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

        Contract.Unused(out complexifiedExpressionList);
        return false;
    }

    private static bool GetComplexifiedAgentExpression(AgentExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByObjectTypeList = new();
        List<Expression> ByDelegatedList = new();

        if (node.BaseType.IsAssigned && GetComplexifiedObjectType(node.BaseType.Item, out IList<ObjectType> ComplexifiedBaseTypeList))
        {
            foreach (ObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
            {
                Identifier ClonedDelegated = (Identifier)DeepCloneNode(node.Delegated, cloneCommentGuid: false);
                AgentExpression NewAgentExpression = CreateAgentExpression(ClonedDelegated, ComplexifiedBaseType);
                ByObjectTypeList.Add(NewAgentExpression);
            }
        }
        else if (GetComplexifiedAgentExpressionDelegated(node, out string FeatureName, out string BaseTypeName))
        {
            Identifier NewDelegated = CreateSimpleIdentifier(FeatureName);
            Identifier BaseTypeIdentifier = CreateSimpleIdentifier(BaseTypeName);
            SimpleType NewBaseType = CreateSimpleType(SharingType.NotShared, BaseTypeIdentifier);

            AgentExpression NewAgentExpression = CreateAgentExpression(NewDelegated, NewBaseType);
            ByDelegatedList.Add(NewAgentExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByObjectTypeList, ByDelegatedList);
    }

    private static bool GetComplexifiedAgentExpressionDelegated(AgentExpression node, out string featureName, out string baseTypeName)
    {
        Identifier Delegated = node.Delegated;
        string Text = Delegated.Text;

        if (Text.StartsWith("{", StringComparison.InvariantCulture))
        {
            int TypeNameIndex = Text.IndexOf("}", StringComparison.InvariantCulture);

            if (TypeNameIndex > 1)
            {
                featureName = Text.Substring(TypeNameIndex + 1).Trim();
                baseTypeName = Text.Substring(1, TypeNameIndex - 1).Trim();

                return true;
            }
        }

        Contract.Unused(out featureName);
        Contract.Unused(out baseTypeName);
        return false;
    }

    private static bool GetComplexifiedBinaryConditionalExpression(BinaryConditionalExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByLeftExpressionList = new();
        List<Expression> ByRightExpressionList = new();

        if (GetComplexifiedExpression(node.LeftExpression, out IList<Expression> ComplexifiedLeftExpressionList))
        {
            foreach (Expression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
            {
                Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                BinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ComplexifiedLeftExpression, node.Conditional, ClonedRightExpression);
                ByLeftExpressionList.Add(NewBinaryConditionalExpression);
            }
        }

        if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
        {
            foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
            {
                Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                BinaryConditionalExpression NewBinaryConditionalExpression = CreateBinaryConditionalExpression(ClonedLeftExpression, node.Conditional, ComplexifiedRightExpression);
                ByRightExpressionList.Add(NewBinaryConditionalExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByLeftExpressionList, ByRightExpressionList);
    }

    private static bool GetComplexifiedBinaryOperatorExpression(BinaryOperatorExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByLeftExpressionList = new();
        List<Expression> ByRightExpressionList = new();
        List<Expression> BySymbolList = new();

        if (GetComplexifiedExpression(node.LeftExpression, out IList<Expression> ComplexifiedLeftExpressionList))
        {
            foreach (Expression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
            {
                Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                Identifier ClonedOperator = (Identifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                BinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ComplexifiedLeftExpression, ClonedOperator, ClonedRightExpression);
                ByLeftExpressionList.Add(NewBinaryOperatorExpression);
            }
        }

        if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
        {
            foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
            {
                Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                Identifier ClonedOperator = (Identifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                BinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, ClonedOperator, ComplexifiedRightExpression);
                ByRightExpressionList.Add(NewBinaryOperatorExpression);
            }
        }

        if (GetRenamedBinarySymbol(node.Operator, out Identifier RenamedOperator))
        {
            Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
            Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
            BinaryOperatorExpression NewBinaryOperatorExpression = CreateBinaryOperatorExpression(ClonedLeftExpression, RenamedOperator, ClonedRightExpression);
            BySymbolList.Add(NewBinaryOperatorExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByLeftExpressionList, ByRightExpressionList, BySymbolList);
    }

    private static bool GetComplexifiedCloneOfExpression(CloneOfExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> BySourceList = new();

        if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
        {
            foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
            {
                CloneOfExpression NewCloneOfExpression = CreateCloneOfExpression(node.Type, ComplexifiedSource);
                BySourceList.Add(NewCloneOfExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, BySourceList);
    }

    private static bool GetComplexifiedEntityExpression(EntityExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByQueryList = new();

        if (GetComplexifiedQualifiedName(node.Query, out IList<QualifiedName> ComplexifiedQueryList))
        {
            foreach (QualifiedName ComplexifiedQuery in ComplexifiedQueryList)
            {
                EntityExpression NewEntityExpression = CreateEntityExpression(ComplexifiedQuery);
                ByQueryList.Add(NewEntityExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByQueryList);
    }

    private static bool GetComplexifiedEqualityExpression(EqualityExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByLeftExpressionList = new();
        List<Expression> ByRightExpressionList = new();

        if (GetComplexifiedExpression(node.LeftExpression, out IList<Expression> ComplexifiedLeftExpressionList))
        {
            foreach (Expression ComplexifiedLeftExpression in ComplexifiedLeftExpressionList)
            {
                Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
                EqualityExpression NewEqualityExpression = CreateEqualityExpression(ComplexifiedLeftExpression, node.Comparison, node.Equality, ClonedRightExpression);
                ByLeftExpressionList.Add(NewEqualityExpression);
            }
        }

        if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
        {
            foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
            {
                Expression ClonedLeftExpression = (Expression)DeepCloneNode(node.LeftExpression, cloneCommentGuid: false);
                EqualityExpression NewEqualityExpression = CreateEqualityExpression(ClonedLeftExpression, node.Comparison, node.Equality, ComplexifiedRightExpression);
                ByRightExpressionList.Add(NewEqualityExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByLeftExpressionList, ByRightExpressionList);
    }

    private static bool GetComplexifiedIndexQueryExpression(IndexQueryExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByIndexedExpressionList = new();
        List<Expression> ByArgumentList = new();

        if (GetComplexifiedExpression(node.IndexedExpression, out IList<Expression> ComplexifiedIndexedExpressionList))
        {
            foreach (Expression ComplexifiedIndexedExpression in ComplexifiedIndexedExpressionList)
            {
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ComplexifiedIndexedExpression, ClonedArgumentBlocks);
                ByIndexedExpressionList.Add(NewIndexQueryExpression);
            }
        }

        if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
        {
            Expression ClonedIndexedExpression = (Expression)DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false);
            IndexQueryExpression NewIndexQueryExpression = CreateIndexQueryExpression(ClonedIndexedExpression, ComplexifiedArgumentBlocks);
            ByArgumentList.Add(NewIndexQueryExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByIndexedExpressionList, ByArgumentList);
    }

    private static bool GetComplexifiedInitializedObjectExpression(InitializedObjectExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByAssignmentList = new();

        if (GetComplexifiedAssignmentArgumentBlockList(node.AssignmentBlocks, out IBlockList<AssignmentArgument> ComplexifiedAssignmentBlocks))
        {
            Identifier ClonedClassIdentifier = (Identifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);
            InitializedObjectExpression NewInitializedObjectExpression = CreateInitializedObjectExpression(ClonedClassIdentifier, ComplexifiedAssignmentBlocks);
            ByAssignmentList.Add(NewInitializedObjectExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByAssignmentList);
    }

    private static bool GetComplexifiedNewExpression(NewExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByObjectList = new();

        if (GetComplexifiedNode(node.Object, out IList<Node> ComplexifiedObjectList) && ComplexifiedObjectList[0] is QualifiedName AsComplexifiedObject)
        {
            NewExpression NewNewExpression = CreateNewExpression(AsComplexifiedObject);
            ByObjectList.Add(NewNewExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByObjectList);
    }

    private static bool GetComplexifiedOldExpression(OldExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByQueryList = new();

        if (GetComplexifiedNode(node.Query, out IList<Node> ComplexifiedQueryList) && ComplexifiedQueryList[0] is QualifiedName AsComplexifiedQuery)
        {
            OldExpression NewOldExpression = CreateOldExpression(AsComplexifiedQuery);
            ByQueryList.Add(NewOldExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByQueryList);
    }

    private static bool GetComplexifiedPrecursorExpression(PrecursorExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByAncestorTypeList = new();

        if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
        {
            foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
            {
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                PrecursorExpression NewPrecursorExpression = CreatePrecursorExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                ByAncestorTypeList.Add(NewPrecursorExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByAncestorTypeList);
    }

    private static bool GetComplexifiedPrecursorIndexExpression(PrecursorIndexExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByAncestorTypeList = new();

        if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
        {
            foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
            {
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                PrecursorIndexExpression NewPrecursorIndexExpression = CreatePrecursorIndexExpression(ClonedArgumentBlocks, ComplexifiedAncestorType);
                ByAncestorTypeList.Add(NewPrecursorIndexExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByAncestorTypeList);
    }

    private static bool ComplexifyAsAgentExpression(QueryExpression node, out AgentExpression complexifiedNode)
    {
        if (IsQuerySimple(node) && ParsePattern(node, "agent ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            AfterText = AfterText.Trim();

            Identifier Delegated = CreateSimpleIdentifier(AfterText);
            complexifiedNode = CreateAgentExpression(Delegated);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsAssertionTagExpression(QueryExpression node, out AssertionTagExpression complexifiedNode)
    {
        if (IsQuerySimple(node) && ParsePattern(node, "tag ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            Identifier TagIdentifier = CreateSimpleIdentifier(AfterText);
            complexifiedNode = CreateAssertionTagExpression(TagIdentifier);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
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
        if (ParsePattern(node, pattern, out string BeforeText, out string AfterText))
        {
            CloneComplexifiedExpression(node, BeforeText, AfterText, out Expression LeftExpression, out Expression RightExpression);
            complexifiedNode = CreateBinaryConditionalExpression(LeftExpression, conditionalType, RightExpression);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsBinaryOperatorExpression(QueryExpression node, out BinaryOperatorExpression complexifiedNode)
    {
        string[] Patterns = new string[] { "+", "-", "/", "*", ">>", "<<", ">=", "<=", ">", "<" };

        foreach (string Pattern in Patterns)
        {
            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText))
            {
                CloneComplexifiedExpression(node, BeforeText, AfterText, out Expression LeftExpression, out Expression RightExpression);
                Identifier Operator = CreateSimpleIdentifier(Pattern);
                complexifiedNode = CreateBinaryOperatorExpression(LeftExpression, Operator, RightExpression);
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsClassConstantExpression(QueryExpression node, out ClassConstantExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;

            if (Text.StartsWith("{", StringComparison.InvariantCulture))
            {
                int ClassNameIndex = Text.IndexOf("}", StringComparison.InvariantCulture);

                if (ClassNameIndex >= 2)
                {
                    Identifier ClassIdentifier = CreateSimpleIdentifier(Text.Substring(1, ClassNameIndex - 1));
                    Identifier ConstantIdentifier = CreateSimpleIdentifier(Text.Substring(ClassNameIndex + 1));
                    complexifiedNode = CreateClassConstantExpression(ClassIdentifier, ConstantIdentifier);
                    return true;
                }
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsCloneOfExpression(QueryExpression node, out CloneOfExpression complexifiedNode)
    {
        if (ParsePattern(node, "clone of ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            CloneComplexifiedExpression(node, AfterText, out Expression Source);
            complexifiedNode = CreateCloneOfExpression(CloneType.Shallow, Source);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsEntityExpression(QueryExpression node, out EntityExpression complexifiedNode, out bool isKeyword, out KeywordEntityExpression complexifiedKeywordNode)
    {
        if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "entity ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            string Text = AfterText.Trim();
            QualifiedName ClonedQuery = (QualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);

            NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", Text);

            complexifiedNode = CreateEntityExpression(ClonedQuery);

            if (StringToKeyword(Text, out Keyword Value))
            {
                isKeyword = true;
                complexifiedKeywordNode = CreateKeywordEntityExpression(Value);
            }
            else
            {
                isKeyword = false;
                Contract.Unused(out complexifiedKeywordNode);
            }

            return true;
        }

        Contract.Unused(out complexifiedNode);
        isKeyword = false;
        Contract.Unused(out complexifiedKeywordNode);
        return false;
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
        if (ParsePattern(node, pattern, out string BeforeText, out string AfterText))
        {
            CloneComplexifiedExpression(node, BeforeText, AfterText, out Expression LeftExpression, out Expression RightExpression);
            complexifiedNode = CreateEqualityExpression(LeftExpression, comparisonType, EqualityType.Physical, RightExpression);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool GetComplexifiedResultOfExpression(ResultOfExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> BySourceList = new();

        if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
        {
            foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
            {
                ResultOfExpression NewResultOfExpression = CreateResultOfExpression(ComplexifiedSource);
                BySourceList.Add(NewResultOfExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, BySourceList);
    }

    private static bool GetComplexifiedUnaryNotExpression(UnaryNotExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByRightExpressionList = new();

        if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
        {
            foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
            {
                UnaryNotExpression NewUnaryNotExpression = CreateUnaryNotExpression(ComplexifiedRightExpression);
                ByRightExpressionList.Add(NewUnaryNotExpression);
            }
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByRightExpressionList);
    }

    private static bool GetComplexifiedUnaryOperatorExpression(UnaryOperatorExpression node, out IList<Expression> complexifiedExpressionList)
    {
        List<Expression> ByRightExpressionList = new();
        List<Expression> ByOperatorList = new();

        if (GetComplexifiedExpression(node.RightExpression, out IList<Expression> ComplexifiedRightExpressionList))
        {
            foreach (Expression ComplexifiedRightExpression in ComplexifiedRightExpressionList)
            {
                Identifier ClonedOperator = (Identifier)DeepCloneNode(node.Operator, cloneCommentGuid: false);
                UnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(ClonedOperator, ComplexifiedRightExpression);
                ByRightExpressionList.Add(NewUnaryOperatorExpression);
            }
        }

        if (GetRenamedUnarySymbol(node.Operator, out Identifier RenamedOperator))
        {
            Expression ClonedRightExpression = (Expression)DeepCloneNode(node.RightExpression, cloneCommentGuid: false);
            UnaryOperatorExpression NewUnaryOperatorExpression = CreateUnaryOperatorExpression(RenamedOperator, ClonedRightExpression);
            ByOperatorList.Add(NewUnaryOperatorExpression);
        }

        return ComposeExpressionLists(out complexifiedExpressionList, ByRightExpressionList, ByOperatorList);
    }

    private static bool ComposeExpressionLists(out IList<Expression> complexifiedExpressionList, params List<Expression>[] expressionLists)
    {
        bool IsComplexified = false;
        foreach (List<Expression> ExpressionList in expressionLists)
            if (ExpressionList.Count > 0)
            {
                IsComplexified = true;
                break;
            }

        if (IsComplexified)
        {
            List<Expression> FullList = new();

            foreach (List<Expression> ExpressionList in expressionLists)
                FullList.AddRange(ExpressionList);

            complexifiedExpressionList = FullList;
            return true;
        }

        Contract.Unused(out complexifiedExpressionList);
        return false;
    }
}
