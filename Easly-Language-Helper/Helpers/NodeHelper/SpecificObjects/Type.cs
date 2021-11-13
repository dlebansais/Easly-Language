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
    /// Creates a new instance of a <see cref="AnchoredType"/> with provided values.
    /// </summary>
    /// <param name="anchoredName">The anchor name.</param>
    /// <param name="anchorKinds">The anchor kind.</param>
    /// <returns>The created instance.</returns>
    public static AnchoredType CreateAnchoredType(QualifiedName anchoredName, AnchorKinds anchorKinds)
    {
        Contract.RequireNotNull(anchoredName, out QualifiedName AnchoredName);

        AnchoredType Result = new AnchoredType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AnchoredName = AnchoredName;
        Result.AnchorKind = anchorKinds;

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

        QueryOverloadType FirstOverload = CreateEmptyQueryOverloadType(ReturnType);

        FunctionType Result = new FunctionType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.OverloadBlocks = BlockListHelper<QueryOverloadType>.CreateSimpleBlockList(FirstOverload);

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

        FunctionType Result = new FunctionType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.OverloadBlocks = OverloadBlocks;

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

        GenericType Result = new GenericType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ClassIdentifier = ClassIdentifier;
        Result.TypeArgumentBlocks = BlockListHelper<TypeArgument>.CreateBlockListFromNodeList(TypeArgumentList);

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

        GenericType Result = new GenericType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Sharing = sharing;
        Result.ClassIdentifier = ClassIdentifier;
        Result.TypeArgumentBlocks = TypeArgumentBlocks;

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

        IndexerType Result = new IndexerType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.EntityType = EntityType;
        Result.IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(Parameter);
        Result.ParameterEnd = ParameterEndStatus.Closed;
        Result.IndexerKind = UtilityType.ReadWrite;
        Result.GetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.GetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.GetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Result.SetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.SetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.SetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

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
    /// <param name="getEnsureBlocks">The getter list of guaranties.</param>
    /// <param name="getExceptionIdentifierBlocks">The getter list of exceptions.</param>
    /// <param name="setRequireBlocks">The setter list of requirements.</param>
    /// <param name="setEnsureBlocks">The setter list of guaranties.</param>
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

        IndexerType Result = new IndexerType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.EntityType = EntityType;
        Result.IndexParameterBlocks = IndexParameterBlocks;
        Result.ParameterEnd = parameterEnd;
        Result.IndexerKind = indexerKind;
        Result.GetRequireBlocks = GetRequireBlocks;
        Result.GetEnsureBlocks = GetEnsureBlocks;
        Result.GetExceptionIdentifierBlocks = GetExceptionIdentifierBlocks;
        Result.SetRequireBlocks = SetRequireBlocks;
        Result.SetEnsureBlocks = SetEnsureBlocks;
        Result.SetExceptionIdentifierBlocks = SetExceptionIdentifierBlocks;

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="KeywordAnchoredType"/> with the provided keyword.
    /// </summary>
    /// <param name="anchor">The anchor keyword.</param>
    /// <returns>The created instance.</returns>
    public static KeywordAnchoredType CreateKeywordAnchoredType(Keyword anchor)
    {
        KeywordAnchoredType Result = new KeywordAnchoredType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Anchor = anchor;

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

        CommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

        ProcedureType Result = new ProcedureType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.OverloadBlocks = BlockListHelper<CommandOverloadType>.CreateSimpleBlockList(FirstOverload);

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

        ProcedureType Result = new ProcedureType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.OverloadBlocks = OverloadBlocks;

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

        PropertyType Result = new PropertyType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.EntityType = EntityType;
        Result.PropertyKind = UtilityType.ReadWrite;
        Result.GetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.GetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Result.SetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.SetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PropertyType"/> with provided values.
    /// </summary>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The entity type.</param>
    /// <param name="propertyKind">The kind of property.</param>
    /// <param name="getEnsureBlocks">The getter list of guaranties.</param>
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

        PropertyType Result = new PropertyType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BaseType = BaseType;
        Result.EntityType = EntityType;
        Result.PropertyKind = propertyKind;
        Result.GetEnsureBlocks = GetEnsureBlocks;
        Result.GetExceptionIdentifierBlocks = GetExceptionIdentifierBlocks;
        Result.SetRequireBlocks = SetRequireBlocks;
        Result.SetExceptionIdentifierBlocks = SetExceptionIdentifierBlocks;

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

        SimpleType SimpleSimpleType = new SimpleType();
        SimpleSimpleType.Documentation = CreateEmptyDocumentation();
        SimpleSimpleType.Sharing = sharing;
        SimpleSimpleType.ClassIdentifier = ClassIdentifier;

        return SimpleSimpleType;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="TupleType"/> with a single field.
    /// </summary>
    /// <param name="entityDeclaration">The field.</param>
    /// <returns>The created instance.</returns>
    public static TupleType CreateTupleType(EntityDeclaration entityDeclaration)
    {
        Contract.RequireNotNull(entityDeclaration, out EntityDeclaration EntityDeclaration);

        TupleType Result = new TupleType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(EntityDeclaration);

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

        TupleType Result = new TupleType();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Sharing = sharing;
        Result.EntityDeclarationBlocks = EntityDeclarationBlocks;

        return Result;
    }
}
