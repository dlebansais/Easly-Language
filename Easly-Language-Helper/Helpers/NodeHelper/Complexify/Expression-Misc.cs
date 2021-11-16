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
}
