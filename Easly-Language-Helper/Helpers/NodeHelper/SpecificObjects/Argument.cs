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
        public static IAssignmentArgument CreateAssignmentArgument(List<IIdentifier> parameterList, IExpression source)
        {
            if (parameterList == null) throw new ArgumentNullException(nameof(parameterList));
            if (source == null) throw new ArgumentNullException(nameof(source));

            Debug.Assert(parameterList.Count > 0);

            AssignmentArgument Result = new AssignmentArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockList(parameterList);
            Result.Source = source;

            return Result;
        }

        public static IAssignmentArgument CreateAssignmentArgument(IBlockList<IIdentifier, Identifier> parameterBlocks, IExpression source)
        {
            if (parameterBlocks == null) throw new ArgumentNullException(nameof(parameterBlocks));
            if (source == null) throw new ArgumentNullException(nameof(source));

            Debug.Assert(!NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)parameterBlocks));

            AssignmentArgument Result = new AssignmentArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterBlocks = parameterBlocks;
            Result.Source = source;

            return Result;
        }

        public static IPositionalArgument CreatePositionalArgument(IExpression source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            PositionalArgument Result = new PositionalArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }
    }
}
