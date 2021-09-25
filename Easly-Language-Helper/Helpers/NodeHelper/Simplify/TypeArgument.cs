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
        /// Creates an instance of a simplified version of a type argument.
        /// </summary>
        /// <param name="nodeTypeArgument">The type argument to simplify.</param>
        /// <param name="simplifiedNode">The simplified type argument.</param>
        /// <returns>True if the type argument could be simplified; otherwise, false.</returns>
        public static bool GetSimplifiedTypeArgument(TypeArgument nodeTypeArgument, out Node simplifiedNode)
        {
            switch (nodeTypeArgument)
            {
                case AssignmentTypeArgument AsAssignmentTypeArgument:
                    return SimplifyAssignmentTypeArgument(AsAssignmentTypeArgument, out simplifiedNode);
                case PositionalTypeArgument AsPositionalTypeArgument:
                    return SimplifyPositionalTypeArgument(AsPositionalTypeArgument, out simplifiedNode);
                default:
                    simplifiedNode = null!;
                    return false;
            }
        }

        private static bool SimplifyAssignmentTypeArgument(AssignmentTypeArgument node, out Node simplifiedNode)
        {
            simplifiedNode = CreatePositionalTypeArgument(node.Source);
            return true;
        }

        private static bool SimplifyPositionalTypeArgument(PositionalTypeArgument node, out Node simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null!;
            return true;
        }
    }
}
