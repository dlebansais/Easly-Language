namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;
using EaslyNumber;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    private static bool GetComplexifiedQueryExpression(QueryExpression node, out IList<Expression> complexifiedExpressionList)
    {
        if (GetComplexifiedNode(node.Query, out IList<Node> ComplexifiedQueryList) && ComplexifiedQueryList[0] is QualifiedName AsComplexifiedQuery)
        {
            IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
            QueryExpression NewQueryExpression = CreateQueryExpression(AsComplexifiedQuery, ClonedArgumentBlocks);
            complexifiedExpressionList = new List<Expression>() { NewQueryExpression };
        }
        else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
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
        else if (ComplexifyAsEntityExpression(node, out EntityExpression ComplexifiedEntityExpression, out bool IsKeyword, out KeywordEntityExpression ComplexifiedKeywordEntityExpression))
        {
            complexifiedExpressionList = new List<Expression>() { ComplexifiedEntityExpression };
            if (IsKeyword)
                complexifiedExpressionList.Add(ComplexifiedKeywordEntityExpression);
        }
        else
            return GetComplexifiedQueryExpressionSingle1(node, out complexifiedExpressionList);

        return true;
    }

    private static bool GetComplexifiedQueryExpressionSingle1(QueryExpression node, out IList<Expression> complexifiedExpressionList)
    {
        if (ComplexifyAsManifestNumberExpression(node, out ManifestNumberExpression ComplexifiedManifestNumberExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedManifestNumberExpression };
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
        else if (ComplexifyAsEqualExpression(node, out EqualityExpression ComplexifiedEqualityExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedEqualityExpression };
        else if (ComplexifyAsDifferentExpression(node, out EqualityExpression ComplexifiedDifferentExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedDifferentExpression };
        else
            return GetComplexifiedQueryExpressionSingle2(node, out complexifiedExpressionList);

        return true;
    }

    private static bool GetComplexifiedQueryExpressionSingle2(QueryExpression node, out IList<Expression> complexifiedExpressionList)
    {
        if (ComplexifyAsCloneOfExpression(node, out CloneOfExpression ComplexifiedCloneOfExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedCloneOfExpression };
        else if (ComplexifyAsUnaryOperatorExpression(node, out UnaryOperatorExpression ComplexifiedUnaryOperatorExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedUnaryOperatorExpression };
        else if (ComplexifyAsBinaryOperatorExpression(node, out BinaryOperatorExpression ComplexifiedBinaryOperatorExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedBinaryOperatorExpression };
        else if (ComplexifyAsClassConstantExpression(node, out ClassConstantExpression ComplexifiedClassConstantExpression))
            complexifiedExpressionList = new List<Expression>() { ComplexifiedClassConstantExpression };
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
        else
            return GetComplexifiedQueryExpressionSingle3(node, out complexifiedExpressionList);

        return true;
    }

    private static bool GetComplexifiedQueryExpressionSingle3(QueryExpression node, out IList<Expression> complexifiedExpressionList)
    {
        if (ComplexifyAsNewExpression(node, out NewExpression ComplexifiedNewExpression))
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
        else
        {
            Contract.Unused(out complexifiedExpressionList);
            return false;
        }

        return true;
    }

    private static bool ComplexifyAsIndexQueryExpression(QueryExpression node, out IndexQueryExpression complexifiedNode)
    {
        if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Query, '[', ']', out QualifiedName NewQuery, out List<Argument> ArgumentList))
        {
            Expression IndexedExpression = CreateQueryExpression(NewQuery, new List<Argument>());
            complexifiedNode = CreateIndexQueryExpression(IndexedExpression, ArgumentList);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsInitializedObjectExpression(QueryExpression node, out InitializedObjectExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;

            if (ParsePattern(Text, "{", out string ClassText, out string InitText))
            {
                if (ParsePattern(InitText, ":=", out string ParameterText, out string SourceText))
                {
                    if (SourceText.EndsWith("}", StringComparison.InvariantCulture))
                    {
                        Identifier ClassIdentifier = CreateSimpleIdentifier(ClassText);

                        Identifier ParameterIdentifier = CreateSimpleIdentifier(ParameterText);
                        Expression Source = CreateSimpleQueryExpression(SourceText.Substring(0, SourceText.Length - 1));
                        AssignmentArgument FirstArgument = CreateAssignmentArgument(new List<Identifier>() { ParameterIdentifier }, Source);

                        List<AssignmentArgument> ArgumentList = new();
                        ArgumentList.Add(FirstArgument);

                        complexifiedNode = CreateInitializedObjectExpression(ClassIdentifier, ArgumentList);
                        return true;
                    }
                }
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsKeywordExpression(QueryExpression node, out KeywordExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;

            if (StringToKeyword(Text, out Keyword Value))
            {
                complexifiedNode = CreateKeywordExpression(Value);
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsManifestCharacterExpression(QueryExpression node, out ManifestCharacterExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;

            if (Text.Length == 3 && Text[0] == '\'' && Text[2] == '\'')
            {
                complexifiedNode = CreateManifestCharacterExpression(Text.Substring(1, 1));
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsManifestNumberExpression(QueryExpression node, out ManifestNumberExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;

            if (Text.Length >= 1)
            {
                FormattedNumber fn = FormattedNumber.Parse(Text);
                if (fn.IsValid)
                {
                    complexifiedNode = CreateSimpleManifestNumberExpression(Text);
                    return true;
                }
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsManifestStringExpression(QueryExpression node, out ManifestStringExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;

            if (Text.Length >= 2 && Text[0] == '"' && Text[Text.Length - 1] == '"')
            {
                complexifiedNode = CreateManifestStringExpression(Text.Substring(1, Text.Length - 2));
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsNewExpression(QueryExpression node, out NewExpression complexifiedNode)
    {
        if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "new ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            QualifiedName ClonedQuery = (QualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
            NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

            complexifiedNode = CreateNewExpression(ClonedQuery);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsOldExpression(QueryExpression node, out OldExpression complexifiedNode)
    {
        if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ParsePattern(node, "old ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            QualifiedName ClonedQuery = (QualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);
            NodeTreeHelper.SetString(ClonedQuery.Path[0], "Text", AfterText);

            complexifiedNode = CreateOldExpression(ClonedQuery);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsPrecursorExpression(QueryExpression node, out PrecursorExpression complexifiedNode)
    {
        if (node.Query.Path.Count == 1)
        {
            string Text = node.Query.Path[0].Text;

            if (Text == "precursor")
            {
                QueryExpression ClonedQuery = (QueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
                complexifiedNode = CreatePrecursorExpression(ClonedQuery.ArgumentBlocks);
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsPrecursorIndexExpression(QueryExpression node, out PrecursorIndexExpression complexifiedNode)
    {
        if (node.Query.Path.Count == 1 && node.ArgumentBlocks.NodeBlockList.Count > 0)
        {
            string Text = node.Query.Path[0].Text;

            if (Text == "precursor[]")
            {
                QueryExpression ClonedQuery = (QueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
                complexifiedNode = CreatePrecursorIndexExpression(ClonedQuery.ArgumentBlocks);
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsPreprocessorExpression(QueryExpression node, out PreprocessorExpression complexifiedNode)
    {
        if (IsQuerySimple(node))
        {
            string Text = node.Query.Path[0].Text;
            Dictionary<string, PreprocessorMacro> PreprocessorTable = new()
            {
                { "DateAndTime", PreprocessorMacro.DateAndTime },
                { "CompilationDiscreteIdentifier", PreprocessorMacro.CompilationDiscreteIdentifier },
                { "ClassPath", PreprocessorMacro.ClassPath },
                { "CompilerVersion", PreprocessorMacro.CompilerVersion },
                { "ConformanceToStandard", PreprocessorMacro.ConformanceToStandard },
                { "DiscreteClassIdentifier", PreprocessorMacro.DiscreteClassIdentifier },
                { "Counter", PreprocessorMacro.Counter },
                { "Debugging", PreprocessorMacro.Debugging },
                { "RandomInteger", PreprocessorMacro.RandomInteger },
            };

            foreach (KeyValuePair<string, PreprocessorMacro> Entry in PreprocessorTable)
                if (Text == Entry.Key)
                {
                    complexifiedNode = CreatePreprocessorExpression(Entry.Value);
                    return true;
                }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsResultOfExpression(QueryExpression node, out ResultOfExpression complexifiedNode)
    {
        if (ParsePattern(node, "result of ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
        {
            CloneComplexifiedExpression(node, AfterText, out Expression Source);
            complexifiedNode = CreateResultOfExpression(Source);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsUnaryNotExpression(QueryExpression node, out UnaryNotExpression complexifiedNode)
    {
        Debug.Assert(node.Query.Path.Count > 0, $"{nameof(node)} has at least one element in the query path");

        string Text = node.Query.Path[0].Text;

        if (Text.StartsWith("not ", StringComparison.InvariantCulture))
        {
            CloneComplexifiedExpression(node, Text.Substring(4), out Expression RightExpression);
            complexifiedNode = CreateUnaryNotExpression(RightExpression);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsUnaryOperatorExpression(QueryExpression node, out UnaryOperatorExpression complexifiedNode)
    {
        Debug.Assert(node.Query.Path.Count > 0, $"{nameof(node)} has at least one element in the query path");

        string Text = node.Query.Path[0].Text;
        string Pattern = "-";

        if (Text.StartsWith(Pattern, StringComparison.InvariantCulture))
        {
            Identifier OperatorName = CreateSimpleIdentifier(Pattern);

            CloneComplexifiedExpression(node, Text.Substring(Pattern.Length), out Expression RightExpression);
            complexifiedNode = CreateUnaryOperatorExpression(OperatorName, RightExpression);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }
}
