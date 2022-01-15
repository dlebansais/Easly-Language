namespace BaseNodeHelper;

using System.Diagnostics;
using BaseNode;
using Contracts;

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
        Contract.Unused(out simplifiedNode);

        bool Result = false;
        bool IsHandled = false;

        switch (nodeArgument)
        {
            case AssignmentArgument AsAssignmentArgument:
                Result = SimplifyAssignmentArgument(AsAssignmentArgument, out simplifiedNode);
                IsHandled = true;
                break;

            case PositionalArgument AsPositionalArgument:
                Result = SimplifyPositionalArgument(AsPositionalArgument, out simplifiedNode);
                IsHandled = true;
                break;
        }

        Debug.Assert(IsHandled, $"All descendants of {nameof(Argument)} have been handled");

        return Result;
    }

    private static bool SimplifyAssignmentArgument(AssignmentArgument node, out Node simplifiedNode)
    {
        Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
        simplifiedNode = CreatePositionalArgument(Source);
        return true;
    }

    private static bool SimplifyPositionalArgument(PositionalArgument node, out Node simplifiedNode)
    {
        Contract.Unused(out simplifiedNode);
        return false;
    }
}
