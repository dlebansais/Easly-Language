namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using BaseNode;
using Contracts;
using Easly;

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        AgentExpression Result = new(Documentation, Delegated, BaseType);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> OptionalBaseType = OptionalReferenceHelper<ObjectType>.CreateReference(BaseType);
        OptionalBaseType.Assign();
        AgentExpression Result = new(Documentation, Delegated, OptionalBaseType);

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

        Document Documentation = CreateEmptyDocumentation();
        AssertionTagExpression Result = new(Documentation, TagIdentifier);

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

        Document Documentation = CreateEmptyDocumentation();
        BinaryConditionalExpression Result = new(Documentation, LeftExpression, conditional, RightExpression);

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

        Document Documentation = CreateEmptyDocumentation();
        BinaryOperatorExpression Result = new(Documentation, LeftExpression, OperatorName, RightExpression);

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

        Document Documentation = CreateEmptyDocumentation();
        ClassConstantExpression Result = new(Documentation, ClassIdentifier, ConstantIdentifier);

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

        Document Documentation = CreateEmptyDocumentation();
        CloneOfExpression Result = new(Documentation, type, Source);

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

        Document Documentation = CreateEmptyDocumentation();
        EntityExpression Result = new(Documentation, Query);

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

        Document Documentation = CreateEmptyDocumentation();
        EqualityExpression Result = new(Documentation, LeftExpression, comparison, equality, RightExpression);

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

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        IndexQueryExpression Result = new(Documentation, IndexedExpression, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IndexQueryExpression Result = new(Documentation, IndexedExpression, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<AssignmentArgument> AssignmentBlocks = BlockListHelper<AssignmentArgument>.CreateBlockListFromNodeList(AssignmentArgumentList);
        InitializedObjectExpression Result = new(Documentation, ClassIdentifier, AssignmentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        InitializedObjectExpression Result = new(Documentation, ClassIdentifier, AssignmentBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="KeywordEntityExpression"/> with the provided keyword.
    /// </summary>
    /// <param name="value">The keyword.</param>
    /// <returns>The created instance.</returns>
    public static KeywordEntityExpression CreateKeywordEntityExpression(Keyword value)
    {
        Document Documentation = CreateEmptyDocumentation();
        KeywordEntityExpression Result = new(Documentation, value);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="KeywordExpression"/> with the provided keyword.
    /// </summary>
    /// <param name="value">The keyword.</param>
    /// <returns>The created instance.</returns>
    public static KeywordExpression CreateKeywordExpression(Keyword value)
    {
        Document Documentation = CreateEmptyDocumentation();
        KeywordExpression Result = new(Documentation, value);

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

        Document Documentation = CreateEmptyDocumentation();
        ManifestCharacterExpression Result = new(Documentation, Text);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ManifestNumberExpression"/> with a default number.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ManifestNumberExpression CreateDefaultManifestNumberExpression()
    {
        Document Documentation = CreateEmptyDocumentation();
        string Text = "0";
        ManifestNumberExpression Result = new(Documentation, Text);

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

        Document Documentation = CreateEmptyDocumentation();
        ManifestNumberExpression Result = new(Documentation, NumberText);

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

        Document Documentation = CreateEmptyDocumentation();
        ManifestStringExpression Result = new(Documentation, Text);

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

        Document Documentation = CreateEmptyDocumentation();
        NewExpression Result = new(Documentation, ObjectName);

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

        Document Documentation = CreateEmptyDocumentation();
        OldExpression Result = new(Documentation, Query);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        PrecursorExpression Result = new(Documentation, AncestorType, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        PrecursorExpression Result = new(Documentation, AncestorType, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> OptionalAncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
        OptionalAncestorType.Assign();
        PrecursorExpression Result = new(Documentation, OptionalAncestorType, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        PrecursorIndexExpression Result = new(Documentation, AncestorType, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        PrecursorIndexExpression Result = new(Documentation, AncestorType, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> OptionalAncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
        OptionalAncestorType.Assign();
        PrecursorIndexExpression Result = new(Documentation, OptionalAncestorType, ArgumentBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PreprocessorExpression"/> with the provided preprocessor macro.
    /// </summary>
    /// <param name="value">The preprocessor macro.</param>
    /// <returns>The created instance.</returns>
    public static PreprocessorExpression CreatePreprocessorExpression(PreprocessorMacro value)
    {
        Document Documentation = CreateEmptyDocumentation();
        PreprocessorExpression Result = new(Documentation, value);

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

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        QueryExpression SimpleQueryExpression = new(Documentation, Query, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        QueryExpression SimpleQueryExpression = new(Documentation, Query, ArgumentBlocks);

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

        Document Documentation = CreateEmptyDocumentation();
        ResultOfExpression Result = new(Documentation, Source);

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

        Document Documentation = CreateEmptyDocumentation();
        UnaryNotExpression Result = new(Documentation, RightExpression);

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

        Document Documentation = CreateEmptyDocumentation();
        UnaryOperatorExpression Result = new(Documentation, OperatorName, RightExpression);

        return Result;
    }
}
