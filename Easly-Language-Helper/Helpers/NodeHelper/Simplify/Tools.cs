#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using BaseNode;

    public static partial class NodeHelper
    {
        public static bool GetSimplifiedNode(Node node, out Node simplifiedNode)
        {
            switch (node)
            {
                case QualifiedName AsQualifiedName:
                    return SimplifyQualifiedName(AsQualifiedName, out simplifiedNode);
                case Argument AsArgument:
                    return GetSimplifiedArgument(AsArgument, out simplifiedNode);
                case Expression AsExpression:
                    return GetSimplifiedExpression(AsExpression, out simplifiedNode);
                case Instruction AsInstruction:
                    return GetSimplifiedInstruction(AsInstruction, out simplifiedNode);
                case ObjectType AsObjectType:
                    return GetSimplifiedObjectType(AsObjectType, out simplifiedNode);
                case TypeArgument AsTypeArgument:
                    return GetSimplifiedTypeArgument(AsTypeArgument, out simplifiedNode);
                default:
                    simplifiedNode = null!;
                    return false;
            }
        }

        private static bool SimplifyQualifiedName(QualifiedName node, out Node simplifiedNode)
        {
            simplifiedNode = null!;

            if (node.Path.Count > 1)
            {
                string ConcatenatedText = string.Empty;

                for (int i = 0; i < node.Path.Count; i++)
                {
                    if (i > 0)
                        ConcatenatedText += ".";

                    ConcatenatedText += node.Path[i].Text;
                }

                simplifiedNode = CreateSimpleQualifiedName(ConcatenatedText);
            }

            return simplifiedNode != null;
        }
    }
}
