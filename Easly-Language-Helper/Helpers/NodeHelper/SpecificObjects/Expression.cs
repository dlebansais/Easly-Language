namespace BaseNodeHelper
{
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
        /// Creates a new instance of a <see cref="AgentExpression"/> for the provided identifier.
        /// </summary>
        /// <param name="delegated">The identifier.</param>
        /// <returns>The created instance.</returns>
        public static AgentExpression CreateAgentExpression(Identifier delegated)
        {
            Contract.RequireNotNull(delegated, out Identifier Delegated);

            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = Delegated;
            Result.BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());

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
            Contract.RequireNotNull(delegated, out Identifier Delegated);
            Contract.RequireNotNull(baseType, out ObjectType BaseType);

            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = Delegated;
            Result.BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(BaseType);
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
            Contract.RequireNotNull(tagIdentifier, out Identifier TagIdentifier);

            AssertionTagExpression Result = new AssertionTagExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.TagIdentifier = TagIdentifier;

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
            Contract.RequireNotNull(leftExpression, out Expression LeftExpression);
            Contract.RequireNotNull(rightExpression, out Expression RightExpression);

            BinaryConditionalExpression Result = new BinaryConditionalExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = LeftExpression;
            Result.Conditional = conditional;
            Result.RightExpression = RightExpression;

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
            Contract.RequireNotNull(leftExpression, out Expression LeftExpression);
            Contract.RequireNotNull(operatorName, out Identifier OperatorName);
            Contract.RequireNotNull(rightExpression, out Expression RightExpression);

            BinaryOperatorExpression Result = new BinaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = LeftExpression;
            Result.Operator = OperatorName;
            Result.RightExpression = RightExpression;

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
            Contract.RequireNotNull(classIdentifier, out Identifier ClassIdentifier);
            Contract.RequireNotNull(constantIdentifier, out Identifier ConstantIdentifier);

            ClassConstantExpression Result = new ClassConstantExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = ClassIdentifier;
            Result.ConstantIdentifier = ConstantIdentifier;

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
            Contract.RequireNotNull(source, out Expression Source);

            CloneOfExpression Result = new CloneOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Type = type;
            Result.Source = Source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="EntityExpression"/> with the provided values.
        /// </summary>
        /// <param name="query">The query path.</param>
        /// <returns>The created instance.</returns>
        public static EntityExpression CreateEntityExpression(QualifiedName query)
        {
            Contract.RequireNotNull(query, out QualifiedName Query);

            EntityExpression Result = new EntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = Query;

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
            Contract.RequireNotNull(leftExpression, out Expression LeftExpression);
            Contract.RequireNotNull(rightExpression, out Expression RightExpression);

            EqualityExpression Result = new EqualityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = LeftExpression;
            Result.Comparison = comparison;
            Result.Equality = equality;
            Result.RightExpression = RightExpression;

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
            Contract.RequireNotNull(indexedExpression, out Expression IndexedExpression);
            Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

            if (ArgumentList.Count == 0)
                throw new ArgumentException($"{nameof(argumentList)} must not be empty");

            Debug.Assert(ArgumentList.Count > 0);

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = IndexedExpression;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

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
            Contract.RequireNotNull(indexedExpression, out Expression IndexedExpression);
            Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ArgumentBlocks))
                throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            Debug.Assert(ArgumentBlocks.NodeBlockList.Count > 0 && ArgumentBlocks.NodeBlockList[0].NodeList.Count > 0);

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = IndexedExpression;
            Result.ArgumentBlocks = ArgumentBlocks;

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
            Contract.RequireNotNull(classIdentifier, out Identifier ClassIdentifier);
            Contract.RequireNotNull(assignmentArgumentList, out List<AssignmentArgument> AssignmentArgumentList);

            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = ClassIdentifier;
            Result.AssignmentBlocks = BlockListHelper<AssignmentArgument>.CreateBlockListFromNodeList(AssignmentArgumentList);

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
            Contract.RequireNotNull(classIdentifier, out Identifier ClassIdentifier);
            Contract.RequireNotNull(assignmentBlocks, out IBlockList<AssignmentArgument> AssignmentBlocks);

            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = ClassIdentifier;
            Result.AssignmentBlocks = AssignmentBlocks;

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
            Contract.RequireNotNull(text, out string Text);

            ManifestCharacterExpression Result = new ManifestCharacterExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = Text;

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
            Contract.RequireNotNull(numberText, out string NumberText);

            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = NumberText;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ManifestStringExpression"/> with the provided string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>The created instance.</returns>
        public static ManifestStringExpression CreateManifestStringExpression(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            ManifestStringExpression Result = new ManifestStringExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = Text;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="NewExpression"/> with the provided values.
        /// </summary>
        /// <param name="objectName">The path to the object.</param>
        /// <returns>The created instance.</returns>
        public static NewExpression CreateNewExpression(QualifiedName objectName)
        {
            Contract.RequireNotNull(objectName, out QualifiedName ObjectName);

            NewExpression Result = new NewExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Object = ObjectName;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="OldExpression"/> with the provided values.
        /// </summary>
        /// <param name="query">The path to the object.</param>
        /// <returns>The created instance.</returns>
        public static OldExpression CreateOldExpression(QualifiedName query)
        {
            Contract.RequireNotNull(query, out QualifiedName Query);

            OldExpression Result = new OldExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = Query;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorExpression CreatePrecursorExpression(List<Argument> argumentList)
        {
            Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorExpression CreatePrecursorExpression(IBlockList<Argument> argumentBlocks)
        {
            Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
            Result.ArgumentBlocks = ArgumentBlocks;

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
            Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
            Contract.RequireNotNull(ancestorType, out ObjectType AncestorType);

            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = ArgumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexExpression CreatePrecursorIndexExpression(List<Argument> argumentList)
        {
            Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

            if (ArgumentList.Count == 0)
                throw new ArgumentException($"{nameof(argumentList)} must not be empty");

            Debug.Assert(ArgumentList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexExpression"/> with the provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<Argument> argumentBlocks)
        {
            Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ArgumentBlocks))
                throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            Debug.Assert(ArgumentBlocks.NodeBlockList.Count > 0 && ArgumentBlocks.NodeBlockList[0].NodeList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
            Result.ArgumentBlocks = ArgumentBlocks;

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
            Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
            Contract.RequireNotNull(ancestorType, out ObjectType AncestorType);

            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ArgumentBlocks))
                throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            Debug.Assert(ArgumentBlocks.NodeBlockList.Count > 0 && ArgumentBlocks.NodeBlockList[0].NodeList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = ArgumentBlocks;

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
            Contract.RequireNotNull(query, out QualifiedName Query);
            Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = Query;
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

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
            Contract.RequireNotNull(query, out QualifiedName Query);
            Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = Query;
            SimpleQueryExpression.ArgumentBlocks = ArgumentBlocks;

            return SimpleQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ResultOfExpression"/> with the provided values.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static ResultOfExpression CreateResultOfExpression(Expression source)
        {
            Contract.RequireNotNull(source, out Expression Source);

            ResultOfExpression Result = new ResultOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = Source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="UnaryNotExpression"/> with the provided values.
        /// </summary>
        /// <param name="rightExpression">The right expression.</param>
        /// <returns>The created instance.</returns>
        public static UnaryNotExpression CreateUnaryNotExpression(Expression rightExpression)
        {
            Contract.RequireNotNull(rightExpression, out Expression RightExpression);

            UnaryNotExpression Result = new UnaryNotExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RightExpression = RightExpression;

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
            Contract.RequireNotNull(operatorName, out Identifier OperatorName);
            Contract.RequireNotNull(rightExpression, out Expression RightExpression);

            UnaryOperatorExpression Result = new UnaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Operator = OperatorName;
            Result.RightExpression = RightExpression;

            return Result;
        }
    }
}
