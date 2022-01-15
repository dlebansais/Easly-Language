namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates a new instance of a <see cref="AnchoredType"/> with provided values.
    /// </summary>
    /// <param name="anchoredName">The anchor name.</param>
    /// <param name="anchorKinds">The anchor kind.</param>
    /// <returns>The created instance.</returns>
    public static AnchoredType CreateAnchoredType(QualifiedName anchoredName, AnchorKinds anchorKinds)
    {
        Contract.RequireNotNull(anchoredName, out QualifiedName AnchoredName);

        Document Documentation = CreateEmptyDocumentation();
        AnchoredType Result = new(Documentation, AnchoredName, anchorKinds);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="FunctionType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="returnType">The return type.</param>
    /// <returns>The created instance.</returns>
    public static FunctionType CreateFunctionType(ObjectType baseType, ObjectType returnType)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(returnType, out ObjectType ReturnType);

        Document Documentation = CreateEmptyDocumentation();
        QueryOverloadType FirstOverload = CreateEmptyQueryOverloadType(ReturnType);
        IBlockList<QueryOverloadType> OverloadBlocks = BlockListHelper<QueryOverloadType>.CreateSimpleBlockList(FirstOverload);
        FunctionType Result = new(Documentation, BaseType, OverloadBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="FunctionType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="overloadBlocks">The list of overloads.</param>
    /// <returns>The created instance.</returns>
    public static FunctionType CreateFunctionType(ObjectType baseType, IBlockList<QueryOverloadType> overloadBlocks)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(overloadBlocks, out IBlockList<QueryOverloadType> OverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)OverloadBlocks))
            throw new ArgumentException($"{nameof(overloadBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        FunctionType Result = new(Documentation, BaseType, OverloadBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="GenericType"/> with provided values.
    /// </summary>
    /// <param name="classIdentifier">The class identifier.</param>
    /// <param name="typeArgumentList">The list of type arguments.</param>
    /// <returns>The created instance.</returns>
    public static GenericType CreateGenericType(Identifier classIdentifier, List<TypeArgument> typeArgumentList)
    {
        Contract.RequireNotNull(classIdentifier, out Identifier ClassIdentifier);
        Contract.RequireNotNull(typeArgumentList, out List<TypeArgument> TypeArgumentList);

        if (typeArgumentList.Count == 0)
            throw new ArgumentException($"{nameof(typeArgumentList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<TypeArgument> TypeArgumentBlocks = BlockListHelper<TypeArgument>.CreateBlockListFromNodeList(TypeArgumentList);
        GenericType Result = new(Documentation, SharingType.NotShared, ClassIdentifier, TypeArgumentBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="GenericType"/> with provided values.
    /// </summary>
    /// <param name="sharing">The sharing type.</param>
    /// <param name="classIdentifier">The class identifier.</param>
    /// <param name="typeArgumentBlocks">The list of type arguments.</param>
    /// <returns>The created instance.</returns>
    public static GenericType CreateGenericType(SharingType sharing, Identifier classIdentifier, IBlockList<TypeArgument> typeArgumentBlocks)
    {
        Contract.RequireNotNull(classIdentifier, out Identifier ClassIdentifier);
        Contract.RequireNotNull(typeArgumentBlocks, out IBlockList<TypeArgument> TypeArgumentBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)TypeArgumentBlocks))
            throw new ArgumentException($"{nameof(typeArgumentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        GenericType Result = new(Documentation, sharing, ClassIdentifier, TypeArgumentBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IndexerType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The entity type.</param>
    /// <param name="parameter">The indexing parameter.</param>
    /// <returns>The created instance.</returns>
    public static IndexerType CreateIndexerType(ObjectType baseType, ObjectType entityType, EntityDeclaration parameter)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(parameter, out EntityDeclaration Parameter);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<EntityDeclaration> IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(Parameter);
        IBlockList<Assertion> GetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> GetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> GetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<Assertion> SetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> SetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> SetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IndexerType Result = new(Documentation, BaseType, EntityType, IndexParameterBlocks, ParameterEndStatus.Closed, UtilityType.ReadWrite, GetRequireBlocks, GetEnsureBlocks, GetExceptionIdentifierBlocks, SetRequireBlocks, SetEnsureBlocks, SetExceptionIdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IndexerType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The entity type.</param>
    /// <param name="indexParameterBlocks">The list of indexing parameters.</param>
    /// <param name="parameterEnd">A value indicating whether the indexer accepts extra parameters.</param>
    /// <param name="indexerKind">The kind of indexer.</param>
    /// <param name="getRequireBlocks">The getter list of requirements.</param>
    /// <param name="getEnsureBlocks">The getter list of guarantees.</param>
    /// <param name="getExceptionIdentifierBlocks">The getter list of exceptions.</param>
    /// <param name="setRequireBlocks">The setter list of requirements.</param>
    /// <param name="setEnsureBlocks">The setter list of guarantees.</param>
    /// <param name="setExceptionIdentifierBlocks">The setter list of exceptions.</param>
    /// <returns>The created instance.</returns>
    public static IndexerType CreateIndexerType(ObjectType baseType, ObjectType entityType, IBlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd, UtilityType indexerKind, IBlockList<Assertion> getRequireBlocks, IBlockList<Assertion> getEnsureBlocks, IBlockList<Identifier> getExceptionIdentifierBlocks, IBlockList<Assertion> setRequireBlocks, IBlockList<Assertion> setEnsureBlocks, IBlockList<Identifier> setExceptionIdentifierBlocks)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(indexParameterBlocks, out IBlockList<EntityDeclaration> IndexParameterBlocks);
        Contract.RequireNotNull(getRequireBlocks, out IBlockList<Assertion> GetRequireBlocks);
        Contract.RequireNotNull(getEnsureBlocks, out IBlockList<Assertion> GetEnsureBlocks);
        Contract.RequireNotNull(getExceptionIdentifierBlocks, out IBlockList<Identifier> GetExceptionIdentifierBlocks);
        Contract.RequireNotNull(setRequireBlocks, out IBlockList<Assertion> SetRequireBlocks);
        Contract.RequireNotNull(setEnsureBlocks, out IBlockList<Assertion> SetEnsureBlocks);
        Contract.RequireNotNull(setExceptionIdentifierBlocks, out IBlockList<Identifier> SetExceptionIdentifierBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)IndexParameterBlocks))
            throw new ArgumentException($"{nameof(indexParameterBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IndexerType Result = new(Documentation, BaseType, EntityType, IndexParameterBlocks, parameterEnd, indexerKind, GetRequireBlocks, GetEnsureBlocks, GetExceptionIdentifierBlocks, SetRequireBlocks, SetEnsureBlocks, SetExceptionIdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="KeywordAnchoredType"/> with the provided keyword.
    /// </summary>
    /// <param name="anchor">The anchor keyword.</param>
    /// <returns>The created instance.</returns>
    public static KeywordAnchoredType CreateKeywordAnchoredType(Keyword anchor)
    {
        Document Documentation = CreateEmptyDocumentation();
        KeywordAnchoredType Result = new(Documentation, anchor);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ProcedureType"/> with a single overload.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <returns>The created instance.</returns>
    public static ProcedureType CreateProcedureType(ObjectType baseType)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);

        Document Documentation = CreateEmptyDocumentation();
        CommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();
        IBlockList<CommandOverloadType> OverloadBlocks = BlockListHelper<CommandOverloadType>.CreateSimpleBlockList(FirstOverload);
        ProcedureType Result = new(Documentation, BaseType, OverloadBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ProcedureType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="overloadBlocks">The list of overloads.</param>
    /// <returns>The created instance.</returns>
    public static ProcedureType CreateProcedureType(ObjectType baseType, IBlockList<CommandOverloadType> overloadBlocks)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(overloadBlocks, out IBlockList<CommandOverloadType> OverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)OverloadBlocks))
            throw new ArgumentException($"{nameof(overloadBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        ProcedureType Result = new(Documentation, BaseType, OverloadBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PropertyType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The created instance.</returns>
    public static PropertyType CreatePropertyType(ObjectType baseType, ObjectType entityType)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Assertion> GetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> GetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<Assertion> SetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> SetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        PropertyType Result = new(Documentation, BaseType, EntityType, UtilityType.ReadWrite, GetEnsureBlocks, GetExceptionIdentifierBlocks, SetRequireBlocks, SetExceptionIdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PropertyType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The entity type.</param>
    /// <param name="propertyKind">The kind of property.</param>
    /// <param name="getEnsureBlocks">The getter list of guarantees.</param>
    /// <param name="getExceptionIdentifierBlocks">The getter list of exceptions.</param>
    /// <param name="setRequireBlocks">The setter list of requirements.</param>
    /// <param name="setExceptionIdentifierBlocks">The setter list of exceptions.</param>
    /// <returns>The created instance.</returns>
    public static PropertyType CreatePropertyType(ObjectType baseType, ObjectType entityType, UtilityType propertyKind, IBlockList<Assertion> getEnsureBlocks, IBlockList<Identifier> getExceptionIdentifierBlocks, IBlockList<Assertion> setRequireBlocks, IBlockList<Identifier> setExceptionIdentifierBlocks)
    {
        Contract.RequireNotNull(baseType, out ObjectType BaseType);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(getEnsureBlocks, out IBlockList<Assertion> GetEnsureBlocks);
        Contract.RequireNotNull(getExceptionIdentifierBlocks, out IBlockList<Identifier> GetExceptionIdentifierBlocks);
        Contract.RequireNotNull(setRequireBlocks, out IBlockList<Assertion> SetRequireBlocks);
        Contract.RequireNotNull(setExceptionIdentifierBlocks, out IBlockList<Identifier> SetExceptionIdentifierBlocks);

        Document Documentation = CreateEmptyDocumentation();
        PropertyType Result = new(Documentation, BaseType, EntityType, propertyKind, GetEnsureBlocks, GetExceptionIdentifierBlocks, SetRequireBlocks, SetExceptionIdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="SimpleType"/> with provided values.
    /// </summary>
    /// <param name="sharing">The sharing type.</param>
    /// <param name="classIdentifier">The class identifier.</param>
    /// <returns>The created instance.</returns>
    public static SimpleType CreateSimpleType(SharingType sharing, Identifier classIdentifier)
    {
        Contract.RequireNotNull(classIdentifier, out Identifier ClassIdentifier);

        Document Documentation = CreateEmptyDocumentation();
        SimpleType Result = new(Documentation, sharing, ClassIdentifier);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="TupleType"/> with a single field.
    /// </summary>
    /// <param name="entityDeclaration">The field.</param>
    /// <returns>The created instance.</returns>
    public static TupleType CreateTupleType(EntityDeclaration entityDeclaration)
    {
        Contract.RequireNotNull(entityDeclaration, out EntityDeclaration EntityDeclaration);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(EntityDeclaration);
        TupleType Result = new(Documentation, SharingType.NotShared, EntityDeclarationBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="TupleType"/> with provided values.
    /// </summary>
    /// <param name="sharing">The sharing type.</param>
    /// <param name="entityDeclarationBlocks">The list of fields.</param>
    /// <returns>The created instance.</returns>
    public static TupleType CreateTupleType(SharingType sharing, IBlockList<EntityDeclaration> entityDeclarationBlocks)
    {
        Contract.RequireNotNull(entityDeclarationBlocks, out IBlockList<EntityDeclaration> EntityDeclarationBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)EntityDeclarationBlocks))
            throw new ArgumentException($"{nameof(entityDeclarationBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        TupleType Result = new(Documentation, sharing, EntityDeclarationBlocks);

        return Result;
    }
}
