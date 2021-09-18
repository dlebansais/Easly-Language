#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeHelper
    {
        public static AssignmentArgument CreateAssignmentArgument(List<Identifier> parameterList, Expression source)
        {
            if (parameterList == null) throw new ArgumentNullException(nameof(parameterList));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterList.Count == 0) throw new ArgumentException($"{nameof(parameterList)} must have least one identifier");

            AssignmentArgument Result = new AssignmentArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterBlocks = BlockListHelper<Identifier>.CreateBlockList(parameterList);
            Result.Source = source;

            return Result;
        }

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
