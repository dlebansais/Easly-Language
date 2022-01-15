namespace BaseNodeHelper;

using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates an instance of a simplified version of a node.
    /// </summary>
    /// <param name="node">The node to simplify.</param>
    /// <param name="simplifiedNode">The simplified node.</param>
    /// <returns>True if the node could be simplified; otherwise, false.</returns>
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
                Contract.Unused(out simplifiedNode);
                return false;
        }
    }

    private static bool SimplifyQualifiedName(QualifiedName node, out Node simplifiedNode)
    {
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
            return true;
        }

        Contract.Unused(out simplifiedNode);
        return false;
    }
}
