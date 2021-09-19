#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using BaseNode;

    public static partial class NodeHelper
    {
        public static bool GetSimplifiedArgument(Argument nodeArgument, out Node simplifiedNode)
        {
            switch (nodeArgument)
            {
                case AssignmentArgument AsAssignmentArgument:
                    return SimplifyAssignmentArgument(AsAssignmentArgument, out simplifiedNode);
                case PositionalArgument AsPositionalArgument:
                    return SimplifyPositionalArgument(AsPositionalArgument, out simplifiedNode);
                default:
                    simplifiedNode = null;
                    return false;
            }
        }

        private static bool SimplifyAssignmentArgument(AssignmentArgument node, out Node simplifiedNode)
        {
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;
            simplifiedNode = CreatePositionalArgument(Source);
            return true;
        }

        private static bool SimplifyPositionalArgument(PositionalArgument node, out Node simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null;
            return true;
        }
    }
}
