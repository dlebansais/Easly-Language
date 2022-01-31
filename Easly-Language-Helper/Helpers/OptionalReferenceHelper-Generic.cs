namespace BaseNodeHelper;

using System.Diagnostics;
using BaseNode;
using Contracts;
using Easly;

/// <summary>
/// Provides methods to manipulate optional nodes of a given type in a program tree.
/// </summary>
/// <typeparam name="TNode">The node type.</typeparam>
public static class OptionalReferenceHelper<TNode>
    where TNode : Node
{
    /// <summary>
    /// Creates a new instance of a <see cref="IOptionalReference{TNode}"/> with the provided child node.
    /// </summary>
    /// <param name="childNode">The child node.</param>
    /// <returns>The created instance.</returns>
    internal static IOptionalReference<TNode> CreateReference(TNode childNode)
    {
        OptionalReference<TNode> Result = new(childNode);
        Debug.Assert(!Result.IsAssigned);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IOptionalReference{TNode}"/> as a copy of the provided reference.
    /// </summary>
    /// <param name="optional">The optional reference to copy.</param>
    /// <returns>The created instance.</returns>
    internal static IOptionalReference<TNode> CreateReferenceCopy(IOptionalReference<TNode> optional)
    {
        OptionalReference<TNode> Result;

        TNode Item = Contract.NullSupressed(optional.Item);
        TNode ClonedItem = (TNode)NodeHelper.DeepCloneNode(Item, cloneCommentGuid: false);

        Result = new(ClonedItem);

        if (optional.IsAssigned)
            Result.Assign();
        else
            Debug.Assert(!Result.IsAssigned);

        return Result;
    }
}
