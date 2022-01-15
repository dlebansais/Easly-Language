namespace BaseNodeHelper;

using System;
using System.Diagnostics;
using BaseNode;
using Contracts;

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
        Contract.Unused(out simplifiedNode);

        bool Result = false;
        bool IsHandled = false;

        switch (nodeTypeArgument)
        {
            case AssignmentTypeArgument AsAssignmentTypeArgument:
                Result = SimplifyAssignmentTypeArgument(AsAssignmentTypeArgument, out simplifiedNode);
                IsHandled = true;
                break;

            case PositionalTypeArgument AsPositionalTypeArgument:
                Result = SimplifyPositionalTypeArgument(AsPositionalTypeArgument, out simplifiedNode);
                IsHandled = true;
                break;
        }

        Debug.Assert(IsHandled, $"All descendants of {nameof(TypeArgument)} have been handled");

        return Result;
    }

    private static bool SimplifyAssignmentTypeArgument(AssignmentTypeArgument node, out Node simplifiedNode)
    {
        simplifiedNode = CreatePositionalTypeArgument(node.Source);
        return true;
    }

    private static bool SimplifyPositionalTypeArgument(PositionalTypeArgument node, out Node simplifiedNode)
    {
        Contract.Unused(out simplifiedNode);
        return false;
    }
}
