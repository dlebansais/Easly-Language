﻿namespace BaseNodeHelper;

using System;
using System.Diagnostics;
using BaseNode;
using Contracts;
using Easly;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates a new instance of a <see cref="AttributeFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static AttributeFeature CreateEmptyAttributeFeature()
    {
        AttributeFeature Result = new AttributeFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityName = CreateEmptyName();
        Result.EntityType = CreateDefaultObjectType();
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ConstantFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ConstantFeature CreateEmptyConstantFeature()
    {
        ConstantFeature Result = new ConstantFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityName = CreateEmptyName();
        Result.EntityType = CreateDefaultObjectType();
        Result.ConstantValue = CreateDefaultExpression();

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="CreationFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static CreationFeature CreateEmptyCreationFeature()
    {
        CommandOverload FirstOverload = CreateEmptyCommandOverload();

        CreationFeature Result = new CreationFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityName = CreateEmptyName();
        Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateSimpleBlockList(FirstOverload);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="FunctionFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static FunctionFeature CreateEmptyFunctionFeature()
    {
        QueryOverload FirstOverload = CreateEmptyQueryOverload();

        FunctionFeature Result = new FunctionFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityName = CreateEmptyName();
        Result.Once = OnceChoice.Normal;
        Result.OverloadBlocks = BlockListHelper<QueryOverload>.CreateSimpleBlockList(FirstOverload);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IndexerFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static IndexerFeature CreateEmptyIndexerFeature()
    {
        EntityDeclaration FirstParameter = CreateEmptyEntityDeclaration();

        IndexerFeature Result = new IndexerFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityType = CreateDefaultObjectType();
        Result.IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(FirstParameter);
        Result.ParameterEnd = ParameterEndStatus.Closed;
        Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Result.GetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());
        Result.GetterBody.Assign();
        Result.SetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ProcedureFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ProcedureFeature CreateEmptyProcedureFeature()
    {
        CommandOverload FirstOverload = CreateEmptyCommandOverload();

        ProcedureFeature Result = new ProcedureFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityName = CreateEmptyName();
        Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateSimpleBlockList(FirstOverload);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PropertyFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static PropertyFeature CreateEmptyPropertyFeature()
    {
        PropertyFeature Result = new PropertyFeature();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExportIdentifier = CreateEmptyExportIdentifier();
        Result.Export = ExportStatus.Exported;
        Result.EntityName = CreateEmptyName();
        Result.EntityType = CreateDefaultObjectType();
        Result.PropertyKind = UtilityType.ReadOnly;
        Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Result.GetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());
        Result.SetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());

        return Result;
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Feature"/> with provided values.
    /// </summary>
    /// <param name="nodeType">The type of the object to create. It must inherit from <see cref="Feature"/>.</param>
    /// <param name="documentation">The documentation.</param>
    /// <param name="exportIdentifier">The export identifier.</param>
    /// <param name="export">The export status.</param>
    /// <param name="entityName">The feature name, if applicable.</param>
    /// <param name="entityType">The entity type, if applicable.</param>
    /// <param name="ensureBlocks">The ensure block, if applicable.</param>
    /// <param name="constantValue">The constant value, if applicable.</param>
    /// <param name="commandOverloadBlocks">The list of command overloads, if applicable.</param>
    /// <param name="once">The once specification, if applicable.</param>
    /// <param name="queryOverloadBlocks">The list of query overloads, if applicable.</param>
    /// <param name="propertyKind">The property kind, if applicable.</param>
    /// <param name="modifiedQueryBlocks">The list of modified queries, if applicable.</param>
    /// <param name="getterBody">The getter body, if applicable.</param>
    /// <param name="setterBody">The setter body, if applicable.</param>
    /// <param name="indexParameterBlocks">The list of index parameters, if applicable.</param>
    /// <param name="parameterEnd">Whether the feature accepts extra parameters, if applicable.</param>
    /// <returns>The created instance.</returns>
    public static Feature CreateInitializedFeature(Type nodeType, Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, ObjectType? entityType, IBlockList<Assertion>? ensureBlocks, Expression? constantValue, IBlockList<CommandOverload>? commandOverloadBlocks, OnceChoice once, IBlockList<QueryOverload>? queryOverloadBlocks, UtilityType propertyKind, IBlockList<Identifier>? modifiedQueryBlocks, IOptionalReference<Body>? getterBody, IOptionalReference<Body>? setterBody, IBlockList<EntityDeclaration>? indexParameterBlocks, ParameterEndStatus parameterEnd)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(documentation, out Document Documentation);
        Contract.RequireNotNull(exportIdentifier, out Identifier ExportIdentifier);

        if (NodeType == typeof(AttributeFeature))
            return CreateInitializedAttributeFeature(Documentation, ExportIdentifier, export, entityName, entityType, ensureBlocks);
        else if (NodeType == typeof(ConstantFeature))
            return CreateInitializedConstantFeature(Documentation, ExportIdentifier, export, entityName, entityType, constantValue);
        else if (NodeType == typeof(CreationFeature))
            return CreateInitializedCreationFeature(Documentation, ExportIdentifier, export, entityName, commandOverloadBlocks);
        else if (NodeType == typeof(FunctionFeature))
            return CreateInitializedFunctionFeature(Documentation, ExportIdentifier, export, entityName, once, queryOverloadBlocks);
        else if (NodeType == typeof(ProcedureFeature))
            return CreateInitializedProcedureFeature(Documentation, ExportIdentifier, export, entityName, commandOverloadBlocks);
        else if (NodeType == typeof(PropertyFeature))
            return CreateInitializedPropertyFeature(Documentation, ExportIdentifier, export, entityName, entityType, propertyKind, modifiedQueryBlocks, getterBody, setterBody);
        else if (NodeType == typeof(IndexerFeature))
            return CreateInitializedIndexerFeature(Documentation, ExportIdentifier, export, entityType, modifiedQueryBlocks, getterBody, setterBody, indexParameterBlocks, parameterEnd);
        else
            throw new ArgumentException($"{nameof(nodeType)} must inherit from {typeof(Feature).FullName}");
    }

    private static AttributeFeature CreateInitializedAttributeFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, ObjectType? entityType, IBlockList<Assertion>? ensureBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(ensureBlocks, out IBlockList<Assertion> EnsureBlocks);

        AttributeFeature Result = new AttributeFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        Result.EntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(EnsureBlocks);

        return Result;
    }

    private static ConstantFeature CreateInitializedConstantFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, ObjectType? entityType, Expression? constantValue)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(constantValue, out Expression ConstantValue);

        ConstantFeature Result = new ConstantFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        Result.EntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        Result.ConstantValue = (Expression)DeepCloneNode(ConstantValue, cloneCommentGuid: false);

        return Result;
    }

    private static CreationFeature CreateInitializedCreationFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, IBlockList<CommandOverload>? commandOverloadBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(commandOverloadBlocks, out IBlockList<CommandOverload> CommandOverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)CommandOverloadBlocks))
            throw new ArgumentException($"{nameof(commandOverloadBlocks)} must not be empty");

        CreationFeature Result = new CreationFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateBlockListCopy(CommandOverloadBlocks);

        return Result;
    }

    private static FunctionFeature CreateInitializedFunctionFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, OnceChoice once, IBlockList<QueryOverload>? queryOverloadBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(queryOverloadBlocks, out IBlockList<QueryOverload> QueryOverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)QueryOverloadBlocks))
            throw new ArgumentException($"{nameof(queryOverloadBlocks)} must not be empty");

        FunctionFeature Result = new FunctionFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        Result.Once = once;
        Result.OverloadBlocks = BlockListHelper<QueryOverload>.CreateBlockListCopy(QueryOverloadBlocks);

        return Result;
    }

    private static IndexerFeature CreateInitializedIndexerFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, ObjectType? entityType, IBlockList<Identifier>? modifiedQueryBlocks, IOptionalReference<Body>? getterBody, IOptionalReference<Body>? setterBody, IBlockList<EntityDeclaration>? indexParameterBlocks, ParameterEndStatus parameterEnd)
    {
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(modifiedQueryBlocks, out IBlockList<Identifier> ModifiedQueryBlocks);
        Contract.RequireNotNull(getterBody, out IOptionalReference<Body> GetterBody);
        Contract.RequireNotNull(setterBody, out IOptionalReference<Body> SetterBody);
        Contract.RequireNotNull(indexParameterBlocks, out IBlockList<EntityDeclaration> IndexParameterBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)IndexParameterBlocks))
            throw new ArgumentException($"{nameof(indexParameterBlocks)} must not be empty");

        IndexerFeature Result = new IndexerFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        Result.IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateBlockListCopy(IndexParameterBlocks);
        Result.ParameterEnd = parameterEnd;
        Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(ModifiedQueryBlocks);
        Result.GetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(GetterBody);
        Result.SetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(SetterBody);

        return Result;
    }

    private static ProcedureFeature CreateInitializedProcedureFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, IBlockList<CommandOverload>? commandOverloadBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(commandOverloadBlocks, out IBlockList<CommandOverload> CommandOverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)CommandOverloadBlocks))
            throw new ArgumentException($"{nameof(commandOverloadBlocks)} must not be empty");

        ProcedureFeature Result = new ProcedureFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateBlockListCopy(CommandOverloadBlocks);

        return Result;
    }

    private static PropertyFeature CreateInitializedPropertyFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, ObjectType? entityType, UtilityType propertyKind, IBlockList<Identifier>? modifiedQueryBlocks, IOptionalReference<Body>? getterBody, IOptionalReference<Body>? setterBody)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(modifiedQueryBlocks, out IBlockList<Identifier> ModifiedQueryBlocks);
        Contract.RequireNotNull(getterBody, out IOptionalReference<Body> GetterBody);
        Contract.RequireNotNull(setterBody, out IOptionalReference<Body> SetterBody);

        PropertyFeature Result = new PropertyFeature();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Result.Export = export;
        Result.EntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        Result.EntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        Result.PropertyKind = propertyKind;
        Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(ModifiedQueryBlocks);
        Result.GetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(GetterBody);
        Result.SetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(SetterBody);

        return Result;
    }
}
