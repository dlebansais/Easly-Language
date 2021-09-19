﻿#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
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
                    return GetSimplifiedExpressionSingle(nodeExpression, out simplifiedNode);
            }
        }

        public static bool GetSimplifiedExpressionSingle(Expression nodeExpression, out Node simplifiedNode)
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
                case PreprocessorExpression AsPreprocessorExpression:
                    return SimplifyPreprocessorExpression(AsPreprocessorExpression, out simplifiedNode);
                default:
                    simplifiedNode = null;
                    return false;
            }
        }

        private static bool SimplifyQueryExpression(QueryExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            QueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as QueryExpression;
            if (ClonedQuery.ArgumentBlocks.NodeBlockList.Count > 0)
                simplifiedNode = CreateQueryExpression(ClonedQuery.Query, new List<Argument>());

            return simplifiedNode != null;
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
            simplifiedNode = null;

            string LeftText, RightText;

            if (GetExpressionText(node.LeftExpression, out LeftText) && GetExpressionText(node.RightExpression, out RightText))
            {
                string Operator = null;

                switch (node.Conditional)
                {
                    case ConditionalTypes.And:
                        Operator = " and ";
                        break;

                    case ConditionalTypes.Or:
                        Operator = " or ";
                        break;

                    case ConditionalTypes.Xor:
                        Operator = " xor ";
                        break;

                    case ConditionalTypes.Implies:
                        Operator = " ⇒ ";
                        break;
                }

                Debug.Assert(Operator != null, $"All values of {nameof(ConditionalTypes)} have been handled");

                string SimplifiedText = LeftText + Operator + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyBinaryOperatorExpression(BinaryOperatorExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string SimplifiedText = LeftText + " " + node.Operator.Text + " " + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
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
            simplifiedNode = null;

            string SourceText;

            if (GetExpressionText(node.Source, out SourceText))
            {
                string SimplifiedText = $"clone of {SourceText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyEntityExpression(EntityExpression node, out Node simplifiedNode)
        {
            string SimplifiedText = $"entity {node.Query.Path[0].Text}";
            simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            return true;
        }

        private static bool SimplifyEqualityExpression(EqualityExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string EqualityText = (node.Comparison == ComparisonType.Equal) ? "=" : "/=";
                string SimplifiedText = $"{LeftText} {EqualityText} {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyIndexQueryExpression(IndexQueryExpression node, out Node simplifiedNode)
        {
            if (node.IndexedExpression is QueryExpression AsQueryExpression && AsQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0)
            {
                QualifiedName Query = DeepCloneNode(AsQueryExpression.Query, cloneCommentGuid: false) as QualifiedName;
                IndexQueryExpression ClonedIndexQuery = DeepCloneNode(node, cloneCommentGuid: false) as IndexQueryExpression;
                simplifiedNode = CreateQueryExpression(Query, ClonedIndexQuery.ArgumentBlocks);
            }
            else
                simplifiedNode = DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false) as Expression;

            return true;
        }

        private static bool SimplifyInitializedObjectExpression(InitializedObjectExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = StringToQualifiedName(node.ClassIdentifier.Text);

            IBlockList<AssignmentArgument> ObjectBlockList = node.AssignmentBlocks;
            BlockList<Argument> ArgumentBlocks = new BlockList<Argument>();
            ArgumentBlocks.Documentation = NodeHelper.CreateDocumentationCopy(ObjectBlockList.Documentation);
            ArgumentBlocks.NodeBlockList = new List<IBlock<Argument>>();

            for (int BlockIndex = 0; BlockIndex < ObjectBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<AssignmentArgument> Block = ObjectBlockList.NodeBlockList[BlockIndex];

                Block<Argument> NewBlock = new Block<Argument>();
                NewBlock.Documentation = CreateDocumentationCopy(Block.Documentation);
                NewBlock.Replication = Block.Replication;

                Pattern NewReplicationPattern = new Pattern();
                NewReplicationPattern.Documentation = CreateDocumentationCopy(Block.ReplicationPattern.Documentation);
                NewReplicationPattern.Text = Block.ReplicationPattern.Text;
                NewBlock.ReplicationPattern = NewReplicationPattern;

                Identifier NewSourceIdentifier = new Identifier();
                NewSourceIdentifier.Documentation = CreateDocumentationCopy(Block.SourceIdentifier.Documentation);
                NewSourceIdentifier.Text = Block.SourceIdentifier.Text;
                NewBlock.SourceIdentifier = NewSourceIdentifier;

                List<Argument> NewNodeList = new List<Argument>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Argument Item = Block.NodeList[Index];
                    Argument NewItem = DeepCloneNode(Item, cloneCommentGuid: false) as Argument;

                    Debug.Assert(NewItem != null, $"A cloned object is always a {nameof(Argument)}");
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                ArgumentBlocks.NodeBlockList.Add(NewBlock);
            }

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
            QualifiedName Query = DeepCloneNode(node.Object, cloneCommentGuid: false) as QualifiedName;

            Debug.Assert(Query.Path.Count > 0, $"A cloned query is never empty");
            string Text = Query.Path[0].Text;
            Text = "new " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyOldExpression(OldExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = DeepCloneNode(node.Query, cloneCommentGuid: false) as QualifiedName;

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
            PrecursorExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as PrecursorExpression;
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPrecursorIndexExpression(PrecursorIndexExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = CreateSimpleQualifiedName("precursor[]");
            PrecursorIndexExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as PrecursorIndexExpression;
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
            simplifiedNode = null;

            string SourceText;

            if (GetExpressionText(node.Source, out SourceText))
            {
                string SimplifiedText = $"result of {SourceText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyUnaryNotExpression(UnaryNotExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            string RightText;

            if (GetExpressionText(node.RightExpression, out RightText))
            {
                string SimplifiedText = $"not {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyUnaryOperatorExpression(UnaryOperatorExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            string RightText;

            if (GetExpressionText(node.RightExpression, out RightText))
            {
                string SimplifiedText = $"{node.Operator.Text} {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }
    }
}