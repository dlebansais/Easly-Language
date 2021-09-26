namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;

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
            if (parameterList == null) throw new ArgumentNullException(nameof(parameterList));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterList.Count == 0) throw new ArgumentException($"{nameof(parameterList)} must have least one identifier");

            AssignmentArgument Result = new AssignmentArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterBlocks = BlockListHelper<Identifier>.CreateBlockListFromNodeList(parameterList);
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates an instance of a <see cref="AssignmentArgument"/> from the provided values.
        /// </summary>
        /// <param name="parameterBlocks">A block list of assignment identifiers.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static AssignmentArgument CreateAssignmentArgument(IBlockList<Identifier> parameterBlocks, Expression source)
        {
            if (parameterBlocks == null) throw new ArgumentNullException(nameof(parameterBlocks));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)parameterBlocks)) throw new ArgumentNullException($"{nameof(parameterBlocks)} must not be empty");

            AssignmentArgument Result = new AssignmentArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterBlocks = parameterBlocks;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates an instance of a <see cref="PositionalArgument"/> from the provided expression.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static PositionalArgument CreatePositionalArgument(Expression source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            PositionalArgument Result = new PositionalArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }
    }
}
