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
            Debug.Assert(ClonedQuery.Query != null, $"The clone always contains a {nameof(QueryExpression.Query)}");
            Debug.Assert(ClonedQuery.Query != null && ClonedQuery.Query.Path.Count > 0, "The clone query path is always valid");

            rightExpression = null!;
            if (ClonedQuery.Query == null)
                return;

            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedExpression(QueryExpression node, string beforeText, string afterText, out Expression leftExpression, out Expression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            QueryExpression ClonedQuery = (QueryExpression)DeepCloneNode(node, cloneCommentGuid: false);
            Debug.Assert(ClonedQuery.Query != null, $"The clone always contains a {nameof(QueryExpression.Query)}");
            Debug.Assert(ClonedQuery.Query != null && ClonedQuery.Query.Path.Count > 0, "The clone query path is always valid");

            rightExpression = null!;
            if (ClonedQuery.Query == null)
                return;

            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedCommand(CommandInstruction node, string afterText, out Expression rightExpression)
        {
            CommandInstruction ClonedCommand = (CommandInstruction)DeepCloneNode(node, cloneCommentGuid: false);
            Debug.Assert(ClonedCommand.Command != null, $"The clone always contains a {nameof(CommandInstruction.Command)}");
            Debug.Assert(ClonedCommand.Command != null && ClonedCommand.Command.Path.Count > 0, "The clone command path is always valid");

            rightExpression = null!;
            if (ClonedCommand.Command == null)
                return;

            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(CommandInstruction node, string beforeText, string afterText, out Expression leftExpression, out Expression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            CommandInstruction ClonedCommand = (CommandInstruction)DeepCloneNode(node, cloneCommentGuid: false);
            Debug.Assert(ClonedCommand.Command != null, $"The clone always contains a {nameof(CommandInstruction.Command)}");
            Debug.Assert(ClonedCommand.Command != null && ClonedCommand.Command.Path.Count > 0, "The clone command path is always valid");

            rightExpression = null!;
            if (ClonedCommand.Command == null)
                return;

            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(CommandInstruction node, string pattern, out CommandInstruction clonedCommand)
        {
            clonedCommand = (CommandInstruction)DeepCloneNode(node, cloneCommentGuid: false);
            Debug.Assert(clonedCommand.Command.Path.Count > 0, "The clone command path is always valid");
            Identifier FirstIdentifier = clonedCommand.Command.Path[0];
            string Text = FirstIdentifier.Text;
            Debug.Assert(Text.StartsWith(pattern, StringComparison.InvariantCulture), "The first element in the clone command path is always unchanged");

            if (Text.Length > pattern.Length || clonedCommand.Command.Path.Count == 1)
                NodeTreeHelper.SetString(FirstIdentifier, nameof(Identifier.Text), Text.Substring(pattern.Length));
            else
                clonedCommand.Command.Path.RemoveAt(0);
        }
    }
}
