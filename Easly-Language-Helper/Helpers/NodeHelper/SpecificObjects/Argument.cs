namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates an instance of a <see cref="AssignmentArgument"/> from the provided values.
    /// </summary>
    /// <param name="parameterList">The list of assignment identifiers.</param>
    /// <param name="source">The source expression.</param>
    /// <returns>The created instance.</returns>
    public static AssignmentArgument CreateAssignmentArgument(List<Identifier> parameterList, Expression source)
    {
        Contract.RequireNotNull(parameterList, out List<Identifier> ParameterList);
        Contract.RequireNotNull(source, out Expression Source);

        if (ParameterList.Count == 0)
            throw new ArgumentException($"{nameof(parameterList)} must not be empty");

        Document Document = CreateEmptyDocument();
        IBlockList<Identifier> ParameterBlocks = BlockListHelper<Identifier>.CreateBlockListFromNodeList(ParameterList);
        AssignmentArgument NewAssignmentArgument = new(Document, ParameterBlocks, Source);

        return NewAssignmentArgument;
    }

    /// <summary>
    /// Creates an instance of a <see cref="AssignmentArgument"/> from the provided values.
    /// </summary>
    /// <param name="parameterBlocks">A block list of assignment identifiers.</param>
    /// <param name="source">The source expression.</param>
    /// <returns>The created instance.</returns>
    public static AssignmentArgument CreateAssignmentArgument(IBlockList<Identifier> parameterBlocks, Expression source)
    {
        Contract.RequireNotNull(parameterBlocks, out IBlockList<Identifier> ParameterBlocks);
        Contract.RequireNotNull(source, out Expression Source);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ParameterBlocks))
            throw new ArgumentException($"{nameof(parameterBlocks)} must not be empty");

        Document Document = CreateEmptyDocument();
        AssignmentArgument NewAssignmentArgument = new(Document, ParameterBlocks, Source);

        return NewAssignmentArgument;
    }

    /// <summary>
    /// Creates an instance of a <see cref="PositionalArgument"/> from the provided expression.
    /// </summary>
    /// <param name="source">The source expression.</param>
    /// <returns>The created instance.</returns>
    public static PositionalArgument CreatePositionalArgument(Expression source)
    {
        Contract.RequireNotNull(source, out Expression Source);

        Document Document = CreateEmptyDocument();
        PositionalArgument NewPositionalArgument = new(Document, Source);

        return NewPositionalArgument;
    }
}
