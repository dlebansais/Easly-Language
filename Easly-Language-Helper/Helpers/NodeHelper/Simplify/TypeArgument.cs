#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using BaseNode;

    public static partial class NodeHelper
    {
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
