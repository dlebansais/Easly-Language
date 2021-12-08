namespace BaseNodeHelper
{
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Creates a new instance of a <see cref="AssignmentTypeArgument"/> with provided values.
        /// </summary>
        /// <param name="parameterIdentifier">The parameter identifier.</param>
        /// <param name="source">The source type.</param>
        /// <returns>The created instance.</returns>
        public static AssignmentTypeArgument CreateAssignmentTypeArgument(Identifier parameterIdentifier, ObjectType source)
        {
            Contract.RequireNotNull(parameterIdentifier, out Identifier ParameterIdentifier);
            Contract.RequireNotNull(source, out ObjectType Source);

            AssignmentTypeArgument Result = new AssignmentTypeArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterIdentifier = ParameterIdentifier;
            Result.Source = Source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PositionalTypeArgument"/> with provided values.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <returns>The created instance.</returns>
        public static PositionalTypeArgument CreatePositionalTypeArgument(ObjectType source)
        {
            Contract.RequireNotNull(source, out ObjectType Source);

            PositionalTypeArgument Result = new PositionalTypeArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = Source;

            return Result;
        }
    }
}
