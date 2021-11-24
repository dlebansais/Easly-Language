namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        private static void CloneComplexifiedExpression(QueryExpression node, string afterText, out Expression rightExpression)
        {
            QueryExpression ClonedQuery = (QueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedExpression(QueryExpression node, string beforeText, string afterText, out Expression leftExpression, out Expression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            QueryExpression ClonedQuery = (QueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedCommand(CommandInstruction node, string afterText, out Expression rightExpression)
        {
            CommandInstruction ClonedCommand = (CommandInstruction)DeepCloneNode(node, cloneCommentGuid: false);
            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(CommandInstruction node, string pattern, out CommandInstruction clonedCommand)
        {
            clonedCommand = (CommandInstruction)DeepCloneNode(node, cloneCommentGuid: false);
            Identifier FirstIdentifier = clonedCommand.Command.Path[0];
            string Text = FirstIdentifier.Text;

            if (Text.Length > pattern.Length || clonedCommand.Command.Path.Count == 1)
                NodeTreeHelper.SetString(FirstIdentifier, nameof(Identifier.Text), Text.Substring(pattern.Length));
            else
                clonedCommand.Command.Path.RemoveAt(0);
        }
    }
}
