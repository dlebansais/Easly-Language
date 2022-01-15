namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Creates an instance of a simplified version of an expression.
        /// </summary>
        /// <param name="nodeExpression">The expression to simplify.</param>
        /// <param name="simplifiedNode">The simplified expression.</param>
        /// <returns>True if the expression could be simplified; otherwise, false.</returns>
        public static bool GetSimplifiedExpression(Expression nodeExpression, out Node simplifiedNode)
        {
            switch (nodeExpression)
            {
                case QueryExpression AsQueryExpression:
                    return SimplifyQueryExpression(AsQueryExpression, out simplifiedNode);
                case BinaryConditionalExpression AsBinaryConditionalExpression:
                    return SimplifyBinaryConditionalExpression(AsBinaryConditionalExpression, out simplifiedNode);
                case BinaryOperatorExpression AsBinaryOperatorExpression:
                    return SimplifyBinaryOperatorExpression(AsBinaryOperatorExpression, out simplifiedNode);
                case EqualityExpression AsEqualityExpression:
                    return SimplifyEqualityExpression(AsEqualityExpression, out simplifiedNode);
                case IndexQueryExpression AsIndexQueryExpression:
                    return SimplifyIndexQueryExpression(AsIndexQueryExpression, out simplifiedNode);
                case InitializedObjectExpression AsInitializedObjectExpression:
                    return SimplifyInitializedObjectExpression(AsInitializedObjectExpression, out simplifiedNode);
                case PrecursorExpression AsPrecursorExpression:
                    return SimplifyPrecursorExpression(AsPrecursorExpression, out simplifiedNode);
                case PrecursorIndexExpression AsPrecursorIndexExpression:
                    return SimplifyPrecursorIndexExpression(AsPrecursorIndexExpression, out simplifiedNode);
                case ResultOfExpression AsResultOfExpression:
                    return SimplifyResultOfExpression(AsResultOfExpression, out simplifiedNode);
                case UnaryNotExpression AsUnaryNotExpression:
                    return SimplifyUnaryNotExpression(AsUnaryNotExpression, out simplifiedNode);
                case UnaryOperatorExpression AsUnaryOperatorExpression:
                    return SimplifyUnaryOperatorExpression(AsUnaryOperatorExpression, out simplifiedNode);
                default:
                    return GetSimplifiedExpressionSingle1(nodeExpression, out simplifiedNode);
            }
        }

        private static bool GetSimplifiedExpressionSingle1(Expression nodeExpression, out Node simplifiedNode)
        {
            switch (nodeExpression)
            {
                case AgentExpression AsAgentExpression:
                    return SimplifyAgentExpression(AsAgentExpression, out simplifiedNode);
                case AssertionTagExpression AsAssertionTagExpression:
                    return SimplifyAssertionTagExpression(AsAssertionTagExpression, out simplifiedNode);
                case ClassConstantExpression AsClassConstantExpression:
                    return SimplifyClassConstantExpression(AsClassConstantExpression, out simplifiedNode);
                case CloneOfExpression AsCloneOfExpression:
                    return SimplifyCloneOfExpression(AsCloneOfExpression, out simplifiedNode);
                case EntityExpression AsEntityExpression:
                    return SimplifyEntityExpression(AsEntityExpression, out simplifiedNode);
                case KeywordEntityExpression AsKeywordEntityExpression:
                    return SimplifyKeywordEntityExpression(AsKeywordEntityExpression, out simplifiedNode);
                case KeywordExpression AsKeywordExpression:
                    return SimplifyKeywordExpression(AsKeywordExpression, out simplifiedNode);
                case ManifestCharacterExpression AsManifestCharacterExpression:
                    return SimplifyManifestCharacterExpression(AsManifestCharacterExpression, out simplifiedNode);
                case ManifestNumberExpression AsManifestNumberExpression:
                    return SimplifyManifestNumberExpression(AsManifestNumberExpression, out simplifiedNode);
                case ManifestStringExpression AsManifestStringExpression:
                    return SimplifyManifestStringExpression(AsManifestStringExpression, out simplifiedNode);
                case NewExpression AsNewExpression:
                    return SimplifyNewExpression(AsNewExpression, out simplifiedNode);
                case OldExpression AsOldExpression:
                    return SimplifyOldExpression(AsOldExpression, out simplifiedNode);
                default:
                    return GetSimplifiedExpressionSingle2(nodeExpression, out simplifiedNode);
            }
        }

        private static bool GetSimplifiedExpressionSingle2(Expression nodeExpression, out Node simplifiedNode)
        {
            Contract.Unused(out simplifiedNode);

            bool Result = false;
            bool IsHandled = false;

            switch (nodeExpression)
            {
                case PreprocessorExpression AsPreprocessorExpression:
                    Result = SimplifyPreprocessorExpression(AsPreprocessorExpression, out simplifiedNode);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(Expression)} have been handled");

            return Result;
        }

        private static bool SimplifyQueryExpression(QueryExpression node, out Node simplifiedNode)
        {
            if (node.ArgumentBlocks.NodeBlockList.Count > 0)
            {
                QueryExpression ClonedQuery = (QueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
                simplifiedNode = CreateQueryExpression(ClonedQuery.Query, new List<Argument>());
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyAgentExpression(AgentExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"agent {node.Delegated.Text}");
            return true;
        }

        private static bool SimplifyAssertionTagExpression(AssertionTagExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"tag {node.TagIdentifier.Text}");
            return true;
        }

        private static bool SimplifyBinaryConditionalExpression(BinaryConditionalExpression node, out Node simplifiedNode)
        {
            string LeftText, RightText;

            if (GetExpressionText(node.LeftExpression, out LeftText) && GetExpressionText(node.RightExpression, out RightText))
            {
                Dictionary<ConditionalTypes, string> SimplifyOperatorTable = new()
                {
                    { ConditionalTypes.And, " and " },
                    { ConditionalTypes.Or, " or " },
                    { ConditionalTypes.Xor, " xor " },
                    { ConditionalTypes.Implies, " ⇒ " },
                };

                Debug.Assert(SimplifyOperatorTable.ContainsKey(node.Conditional), $"All values of {nameof(ConditionalTypes)} have been handled");

                string Operator = SimplifyOperatorTable[node.Conditional];
                string SimplifiedText = LeftText + Operator + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyBinaryOperatorExpression(BinaryOperatorExpression node, out Node simplifiedNode)
        {
            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string SimplifiedText = LeftText + " " + node.Operator.Text + " " + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyClassConstantExpression(ClassConstantExpression node, out Node simplifiedNode)
        {
            string MergedText = $"{{{node.ClassIdentifier.Text}}}{node.ConstantIdentifier.Text}";
            QualifiedName Query = StringToQualifiedName(MergedText);

            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyCloneOfExpression(CloneOfExpression node, out Node simplifiedNode)
        {
            string SourceText;

            if (GetExpressionText(node.Source, out SourceText))
            {
                string SimplifiedText = $"clone of {SourceText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyEntityExpression(EntityExpression node, out Node simplifiedNode)
        {
            string SimplifiedText = $"entity {node.Query.Path[0].Text}";
            simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            return true;
        }

        private static bool SimplifyEqualityExpression(EqualityExpression node, out Node simplifiedNode)
        {
            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string EqualityText = (node.Comparison == ComparisonType.Equal) ? "=" : "/=";
                string SimplifiedText = $"{LeftText} {EqualityText} {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyIndexQueryExpression(IndexQueryExpression node, out Node simplifiedNode)
        {
            if (node.IndexedExpression is QueryExpression AsQueryExpression && AsQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0)
            {
                QualifiedName Query = (QualifiedName)DeepCloneNode(AsQueryExpression.Query, cloneCommentGuid: false);
                IndexQueryExpression ClonedIndexQuery = (IndexQueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
                simplifiedNode = CreateQueryExpression(Query, ClonedIndexQuery.ArgumentBlocks);
            }
            else
                simplifiedNode = (Expression)DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false);

            return true;
        }

        private static bool SimplifyInitializedObjectExpression(InitializedObjectExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = StringToQualifiedName(node.ClassIdentifier.Text);

            IBlockList<AssignmentArgument> ObjectBlockList = node.AssignmentBlocks;

            Document Documentation = NodeHelper.CreateDocumentationCopy(ObjectBlockList.Documentation);
            List<IBlock<Argument>> NodeBlockList = new List<IBlock<Argument>>();

            for (int BlockIndex = 0; BlockIndex < ObjectBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<AssignmentArgument> Block = ObjectBlockList.NodeBlockList[BlockIndex];

                Document BlockDocumentation = CreateDocumentationCopy(Block.Documentation);

                List<Argument> NewNodeList = new List<Argument>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Argument Item = Block.NodeList[Index];
                    Argument NewItem = (Argument)DeepCloneNode(Item, cloneCommentGuid: false);

                    NewNodeList.Add(NewItem);
                }

                Pattern NewReplicationPattern = new Pattern(CreateDocumentationCopy(Block.ReplicationPattern.Documentation), Block.ReplicationPattern.Text);
                Identifier NewSourceIdentifier = new Identifier(CreateDocumentationCopy(Block.SourceIdentifier.Documentation), Block.SourceIdentifier.Text);

                Block<Argument> NewBlock = new Block<Argument>(BlockDocumentation, NewNodeList, Block.Replication, NewReplicationPattern, NewSourceIdentifier);

                NodeBlockList.Add(NewBlock);
            }

            BlockList<Argument> ArgumentBlocks = new BlockList<Argument>(Documentation, NodeBlockList);

            simplifiedNode = CreateQueryExpression(Query, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyKeywordEntityExpression(KeywordEntityExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"entity {node.Value}");
            return true;
        }

        private static bool SimplifyKeywordExpression(KeywordExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Value.ToString());
            return true;
        }

        private static bool SimplifyManifestCharacterExpression(ManifestCharacterExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyManifestNumberExpression(ManifestNumberExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyManifestStringExpression(ManifestStringExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyNewExpression(NewExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = (QualifiedName)DeepCloneNode(node.Object, cloneCommentGuid: false);

            Debug.Assert(Query.Path.Count > 0, $"A cloned query is never empty");
            string Text = Query.Path[0].Text;
            Text = "new " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyOldExpression(OldExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = (QualifiedName)DeepCloneNode(node.Query, cloneCommentGuid: false);

            Debug.Assert(Query.Path.Count > 0, $"A cloned query is never empty");
            string Text = Query.Path[0].Text;
            Text = "old " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyPrecursorExpression(PrecursorExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = CreateSimpleQualifiedName("precursor");
            PrecursorExpression ClonedQuery = (PrecursorExpression)DeepCloneNode(node, cloneCommentGuid: false);
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPrecursorIndexExpression(PrecursorIndexExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = CreateSimpleQualifiedName("precursor[]");
            PrecursorIndexExpression ClonedQuery = (PrecursorIndexExpression)DeepCloneNode(node, cloneCommentGuid: false);
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPreprocessorExpression(PreprocessorExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Value.ToString());
            return true;
        }

        private static bool SimplifyResultOfExpression(ResultOfExpression node, out Node simplifiedNode)
        {
            string SourceText;

            if (GetExpressionText(node.Source, out SourceText))
            {
                string SimplifiedText = $"result of {SourceText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyUnaryNotExpression(UnaryNotExpression node, out Node simplifiedNode)
        {
            string RightText;

            if (GetExpressionText(node.RightExpression, out RightText))
            {
                string SimplifiedText = $"not {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyUnaryOperatorExpression(UnaryOperatorExpression node, out Node simplifiedNode)
        {
            string RightText;

            if (GetExpressionText(node.RightExpression, out RightText))
            {
                string SimplifiedText = $"{node.Operator.Text} {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }
    }
}
