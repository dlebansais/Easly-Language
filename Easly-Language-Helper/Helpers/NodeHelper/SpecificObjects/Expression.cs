namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Creates a new instance of a <see cref="AgentExpression"/> for the provided identifier.
        /// </summary>
        /// <param name="delegated">The identifier.</param>
        /// <returns>The created instance.</returns>
        public static AgentExpression CreateAgentExpression(Identifier delegated)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AgentExpression"/> for the provided identifier and base type.
        /// </summary>
        /// <param name="delegated">The identifier.</param>
        /// <param name="baseType">The base type.</param>
        /// <returns>The created instance.</returns>
        public static AgentExpression CreateAgentExpression(Identifier delegated, ObjectType baseType)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(baseType);
            Result.BaseType.Assign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AssertionTagExpression"/> for the provided identifier.
        /// </summary>
        /// <param name="tagIdentifier">Thye tag identifier.</param>
        /// <returns>The created instance.</returns>
        public static AssertionTagExpression CreateAssertionTagExpression(Identifier tagIdentifier)
        {
            AssertionTagExpression Result = new AssertionTagExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.TagIdentifier = tagIdentifier;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="BinaryConditionalExpression"/> with the provided values.
        /// </summary>
        /// <param name="leftExpression">The left expression.</param>
        /// <param name="conditional">The type of condition.</param>
        /// <param name="rightExpression">The right expression.</param>
        /// <returns>The created instance.</returns>
        public static BinaryConditionalExpression CreateBinaryConditionalExpression(Expression leftExpression, ConditionalTypes conditional, Expression rightExpression)
        {
            BinaryConditionalExpression Result = new BinaryConditionalExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Conditional = conditional;
            Result.RightExpression = rightExpression;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="BinaryOperatorExpression"/> with the provided values.
        /// </summary>
        /// <param name="leftExpression">The left expression.</param>
        /// <param name="operatorName">The operator name.</param>
        /// <param name="rightExpression">The right expression.</param>
        /// <returns>The created instance.</returns>
        public static BinaryOperatorExpression CreateBinaryOperatorExpression(Expression leftExpression, Identifier operatorName, Expression rightExpression)
        {
            BinaryOperatorExpression Result = new BinaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ClassConstantExpression"/> with the provided values.
        /// </summary>
        /// <param name="classIdentifier">The class identifier.</param>
        /// <param name="constantIdentifier">The constant identifier.</param>
        /// <returns>The created instance.</returns>
        public static ClassConstantExpression CreateClassConstantExpression(Identifier classIdentifier, Identifier constantIdentifier)
        {
            ClassConstantExpression Result = new ClassConstantExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.ConstantIdentifier = constantIdentifier;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CloneOfExpression"/> with the provided values.
        /// </summary>
        /// <param name="type">The clone type.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static CloneOfExpression CreateCloneOfExpression(CloneType type, Expression source)
        {
            CloneOfExpression Result = new CloneOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Type = type;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="EntityExpression"/> with the provided values.
        /// </summary>
        /// <param name="query">The query path.</param>
        /// <returns>The created instance.</returns>
        public static EntityExpression CreateEntityExpression(QualifiedName query)
        {
            EntityExpression Result = new EntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="EqualityExpression"/> with the provided values.
        /// </summary>
        /// <param name="leftExpression">The left expression.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="equality">The equality type.</param>
        /// <param name="rightExpression">The right expression.</param>
        /// <returns>The created instance.</returns>
        public static EqualityExpression CreateEqualityExpression(Expression leftExpression, ComparisonType comparison, EqualityType equality, Expression rightExpression)
        {
            EqualityExpression Result = new EqualityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Comparison = comparison;
            Result.Equality = equality;
            Result.RightExpression = rightExpression;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IndexQueryExpression "/> with the provided values.
        /// </summary>
        /// <param name="indexedExpression">The indexed expression.</param>
        /// <param name="argumentList">The argument list.</param>
        /// <returns>The created instance.</returns>
        public static IndexQueryExpression CreateIndexQueryExpression(Expression indexedExpression, List<Argument> argumentList)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IndexQueryExpression "/> with the provided values.
        /// </summary>
        /// <param name="indexedExpression">The indexed expression.</param>
        /// <param name="argumentBlocks">The argument list.</param>
        /// <returns>The created instance.</returns>
        public static IndexQueryExpression CreateIndexQueryExpression(Expression indexedExpression, IBlockList<Argument> argumentBlocks)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="InitializedObjectExpression"/> with the provided values.
        /// </summary>
        /// <param name="classIdentifier">The class identifier.</param>
        /// <param name="assignmentArgumentList">The argument list.</param>
        /// <returns>The created instance.</returns>
        public static InitializedObjectExpression CreateInitializedObjectExpression(Identifier classIdentifier, List<AssignmentArgument> assignmentArgumentList)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = BlockListHelper<AssignmentArgument>.CreateBlockList(assignmentArgumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="InitializedObjectExpression"/> with the provided values.
        /// </summary>
        /// <param name="classIdentifier">The class identifier.</param>
        /// <param name="assignmentBlocks">The argument list.</param>
        /// <returns>The created instance.</returns>
        public static InitializedObjectExpression CreateInitializedObjectExpression(Identifier classIdentifier, IBlockList<AssignmentArgument> assignmentBlocks)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = assignmentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="KeywordEntityExpression"/> with the provided keyword.
        /// </summary>
        /// <param name="value">The keyword.</param>
        /// <returns>The created instance.</returns>
        public static KeywordEntityExpression CreateKeywordEntityExpression(Keyword value)
        {
            KeywordEntityExpression Result = new KeywordEntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="KeywordExpression"/> with the provided keyword.
        /// </summary>
        /// <param name="value">The keyword.</param>
        /// <returns>The created instance.</returns>
        public static KeywordExpression CreateKeywordExpression(Keyword value)
        {
            KeywordExpression Result = new KeywordExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ManifestCharacterExpression"/> with the provided character.
        /// </summary>
        /// <param name="text">The character.</param>
        /// <returns>The created instance.</returns>
        public static ManifestCharacterExpression CreateManifestCharacterExpression(string text)
        {
            ManifestCharacterExpression Result = new ManifestCharacterExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ManifestNumberExpression"/> with a default number.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static ManifestNumberExpression CreateDefaultManifestNumberExpression()
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = "0";

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ManifestNumberExpression"/> with the provided number.
        /// </summary>
        /// <param name="numberText">The number.</param>
        /// <returns>The created instance.</returns>
        public static ManifestNumberExpression CreateSimpleManifestNumberExpression(string numberText)
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = numberText;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ManifestStringExpression"/> with the provided string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>The created instance.</returns>
        public static ManifestStringExpression CreateManifestStringExpression(string text)
        {
            ManifestStringExpression Result = new ManifestStringExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="NewExpression"/> with the provided values.
        /// </summary>
        /// <param name="objectName">The path to the object.</param>
        /// <returns>The created instance.</returns>
        public static NewExpression CreateNewExpression(QualifiedName objectName)
        {
            NewExpression Result = new NewExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Object = objectName;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="OldExpression"/> with the provided values.
        /// </summary>
        /// <param name="query">The path to the object.</param>
        /// <returns>The created instance.</returns>
        public static OldExpression CreateOldExpression(QualifiedName query)
        {
            OldExpression Result = new OldExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorExpression CreatePrecursorExpression(List<Argument> argumentList)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorExpression CreatePrecursorExpression(IBlockList<Argument> argumentBlocks)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <param name="ancestorType">The ancestor type.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorExpression CreatePrecursorExpression(IBlockList<Argument> argumentBlocks, ObjectType ancestorType)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexExpression CreatePrecursorIndexExpression(List<Argument> argumentList)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<Argument> argumentBlocks)
        {
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <param name="ancestorType">The ancestor type.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<Argument> argumentBlocks, ObjectType ancestorType)
        {
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PreprocessorExpression"/> with the provided preprocessor macro.
        /// </summary>
        /// <param name="value">The preprocessor macro.</param>
        /// <returns>The created instance.</returns>
        public static PreprocessorExpression CreatePreprocessorExpression(PreprocessorMacro value)
        {
            PreprocessorExpression Result = new PreprocessorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="QueryExpression"/> with the provided values.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static QueryExpression CreateQueryExpression(QualifiedName query, List<Argument> argumentList)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return SimpleQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="QueryExpression"/> with the provided values.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static QueryExpression CreateQueryExpression(QualifiedName query, IBlockList<Argument> argumentBlocks)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = argumentBlocks;

            return SimpleQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ResultOfExpression"/> with the provided values.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static ResultOfExpression CreateResultOfExpression(Expression source)
        {
            ResultOfExpression Result = new ResultOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UnaryNotExpression"/> with the provided values.
        /// </summary>
        /// <param name="rightExpression">The right expression.</param>
        /// <returns>The created instance.</returns>
        public static UnaryNotExpression CreateUnaryNotExpression(Expression rightExpression)
        {
            UnaryNotExpression Result = new UnaryNotExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RightExpression = rightExpression;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UnaryNotExpression"/> with the provided values.
        /// </summary>
        /// <param name="operatorName">The operator nam.</param>
        /// <param name="rightExpression">The right expression.</param>
        /// <returns>The created instance.</returns>
        public static UnaryOperatorExpression CreateUnaryOperatorExpression(Identifier operatorName, Expression rightExpression)
        {
            UnaryOperatorExpression Result = new UnaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }
    }
}
