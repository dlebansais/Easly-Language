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
        public static AssignmentTypeArgument CreateAssignmentTypeArgument(Identifier parameterIdentifier, ObjectType source)
        {
            AssignmentTypeArgument Result = new AssignmentTypeArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterIdentifier = parameterIdentifier;
            Result.Source = source;

            return Result;
        }

        public static PositionalTypeArgument CreatePositionalTypeArgument(ObjectType source)
        {
            PositionalTypeArgument Result = new PositionalTypeArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }
    }
}
