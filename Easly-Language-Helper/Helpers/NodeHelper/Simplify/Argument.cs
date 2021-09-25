namespace BaseNodeHelper
{
    using System;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Creates an instance of a simplified version of an argument.
        /// </summary>
        /// <param name="nodeArgument">The argument to simplify.</param>
        /// <param name="simplifiedNode">The simplified argument.</param>
        /// <returns>True if the argument could be simplified; otherwise, false.</returns>
        public static bool GetSimplifiedArgument(Argument nodeArgument, out Node simplifiedNode)
        {
            switch (nodeArgument)
            {
                case AssignmentArgument AsAssignmentArgument:
                    return SimplifyAssignmentArgument(AsAssignmentArgument, out simplifiedNode);
                case PositionalArgument AsPositionalArgument:
                    return SimplifyPositionalArgument(AsPositionalArgument, out simplifiedNode);
                default:
                    simplifiedNode = null!;
                    return false;
            }
        }

        private static bool SimplifyAssignmentArgument(AssignmentArgument node, out Node simplifiedNode)
        {
            Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
            simplifiedNode = CreatePositionalArgument(Source);
            return true;
        }

        private static bool SimplifyPositionalArgument(PositionalArgument node, out Node simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null!;
            return true;
        }
    }
}
