namespace BaseNodeHelper;

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
        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        Name EntityName = CreateEmptyName();
        ObjectType EntityType = CreateDefaultObjectType();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        AttributeFeature EmptyAttributeFeature = new(Document, ExportIdentifier, Export, EntityName, EntityType, EnsureBlocks);

        return EmptyAttributeFeature;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ConstantFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ConstantFeature CreateEmptyConstantFeature()
    {
        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        Name EntityName = CreateEmptyName();
        ObjectType EntityType = CreateDefaultObjectType();
        Expression ConstantValue = CreateDefaultExpression();
        ConstantFeature EmptyConstantFeature = new(Document, ExportIdentifier, Export, EntityName, EntityType, ConstantValue);

        return EmptyConstantFeature;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="CreationFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static CreationFeature CreateEmptyCreationFeature()
    {
        CommandOverload FirstOverload = CreateEmptyCommandOverload();

        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        Name EntityName = CreateEmptyName();
        IBlockList<CommandOverload> OverloadBlocks = BlockListHelper<CommandOverload>.CreateSimpleBlockList(FirstOverload);
        CreationFeature EmptyCreationFeature = new(Document, ExportIdentifier, Export, EntityName, OverloadBlocks);

        return EmptyCreationFeature;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="FunctionFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static FunctionFeature CreateEmptyFunctionFeature()
    {
        QueryOverload FirstOverload = CreateEmptyQueryOverload();

        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        Name EntityName = CreateEmptyName();
        OnceChoice Once = OnceChoice.Normal;
        IBlockList<QueryOverload> OverloadBlocks = BlockListHelper<QueryOverload>.CreateSimpleBlockList(FirstOverload);
        FunctionFeature EmptyFunctionFeature = new(Document, ExportIdentifier, Export, EntityName, Once, OverloadBlocks);

        return EmptyFunctionFeature;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IndexerFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static IndexerFeature CreateEmptyIndexerFeature()
    {
        EntityDeclaration FirstParameter = CreateEmptyEntityDeclaration();

        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        ObjectType EntityType = CreateDefaultObjectType();
        IBlockList<EntityDeclaration> IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(FirstParameter);
        ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
        IBlockList<Identifier> ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IOptionalReference<Body> GetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());
        GetterBody.Assign();
        IOptionalReference<Body> SetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());
        IndexerFeature EmptyIndexerFeature = new(Document, ExportIdentifier, Export, EntityType, IndexParameterBlocks, ParameterEnd, ModifiedQueryBlocks, GetterBody, SetterBody);

        return EmptyIndexerFeature;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ProcedureFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ProcedureFeature CreateEmptyProcedureFeature()
    {
        CommandOverload FirstOverload = CreateEmptyCommandOverload();

        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        Name EntityName = CreateEmptyName();
        IBlockList<CommandOverload> OverloadBlocks = BlockListHelper<CommandOverload>.CreateSimpleBlockList(FirstOverload);
        ProcedureFeature EmptyProcedureFeature = new(Document, ExportIdentifier, Export, EntityName, OverloadBlocks);

        return EmptyProcedureFeature;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PropertyFeature"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static PropertyFeature CreateEmptyPropertyFeature()
    {
        Document Document = CreateEmptyDocument();
        Identifier ExportIdentifier = CreateEmptyExportIdentifier();
        ExportStatus Export = ExportStatus.Exported;
        Name EntityName = CreateEmptyName();
        ObjectType EntityType = CreateDefaultObjectType();
        UtilityType PropertyKind = UtilityType.ReadOnly;
        IBlockList<Identifier> ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IOptionalReference<Body> GetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());
        IOptionalReference<Body> SetterBody = OptionalReferenceHelper<Body>.CreateReference(CreateDefaultBody());
        PropertyFeature EmptyPropertyFeature = new(Document, ExportIdentifier, Export, EntityName, EntityType, PropertyKind, ModifiedQueryBlocks, GetterBody, SetterBody);

        return EmptyPropertyFeature;
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

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Name ClonedEntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        ObjectType ClonedEntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        IBlockList<Assertion> ClonedEnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(EnsureBlocks);
        AttributeFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityName, ClonedEntityType, ClonedEnsureBlocks);

        return Result;
    }

    private static ConstantFeature CreateInitializedConstantFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, ObjectType? entityType, Expression? constantValue)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(constantValue, out Expression ConstantValue);

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Name ClonedEntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        ObjectType ClonedEntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        Expression ClonedConstantValue = (Expression)DeepCloneNode(ConstantValue, cloneCommentGuid: false);
        ConstantFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityName, ClonedEntityType, ClonedConstantValue);

        return Result;
    }

    private static CreationFeature CreateInitializedCreationFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, IBlockList<CommandOverload>? commandOverloadBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(commandOverloadBlocks, out IBlockList<CommandOverload> CommandOverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)CommandOverloadBlocks))
            throw new ArgumentException($"{nameof(commandOverloadBlocks)} must not be empty");

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Name ClonedEntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        IBlockList<CommandOverload> ClonedOverloadBlocks = BlockListHelper<CommandOverload>.CreateBlockListCopy(CommandOverloadBlocks);
        CreationFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityName, ClonedOverloadBlocks);

        return Result;
    }

    private static FunctionFeature CreateInitializedFunctionFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, OnceChoice once, IBlockList<QueryOverload>? queryOverloadBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(queryOverloadBlocks, out IBlockList<QueryOverload> QueryOverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)QueryOverloadBlocks))
            throw new ArgumentException($"{nameof(queryOverloadBlocks)} must not be empty");

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Name ClonedEntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        IBlockList<QueryOverload> ClonedOverloadBlocks = BlockListHelper<QueryOverload>.CreateBlockListCopy(QueryOverloadBlocks);
        FunctionFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityName, once, ClonedOverloadBlocks);

        return Result;
    }

    private static ProcedureFeature CreateInitializedProcedureFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, IBlockList<CommandOverload>? commandOverloadBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(commandOverloadBlocks, out IBlockList<CommandOverload> CommandOverloadBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)CommandOverloadBlocks))
            throw new ArgumentException($"{nameof(commandOverloadBlocks)} must not be empty");

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Name ClonedEntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        IBlockList<CommandOverload> ClonedOverloadBlocks = BlockListHelper<CommandOverload>.CreateBlockListCopy(CommandOverloadBlocks);
        ProcedureFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityName, ClonedOverloadBlocks);

        return Result;
    }

    private static PropertyFeature CreateInitializedPropertyFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name? entityName, ObjectType? entityType, UtilityType propertyKind, IBlockList<Identifier>? modifiedQueryBlocks, IOptionalReference<Body>? getterBody, IOptionalReference<Body>? setterBody)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);
        Contract.RequireNotNull(modifiedQueryBlocks, out IBlockList<Identifier> ModifiedQueryBlocks);
        Contract.RequireNotNull(getterBody, out IOptionalReference<Body> GetterBody);
        Contract.RequireNotNull(setterBody, out IOptionalReference<Body> SetterBody);

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        Name ClonedEntityName = (Name)DeepCloneNode(EntityName, cloneCommentGuid: false);
        ObjectType ClonedEntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        IBlockList<Identifier> ClonedModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(ModifiedQueryBlocks);
        IOptionalReference<Body> OptionalGetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(GetterBody);
        IOptionalReference<Body> OptionalSetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(SetterBody);
        PropertyFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityName, ClonedEntityType, propertyKind, ClonedModifiedQueryBlocks, OptionalGetterBody, OptionalSetterBody);

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

        Document Documentation = CreateDocumentationCopy(documentation);
        Identifier ExportIdentifier = (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false);
        ObjectType ClonedEntityType = (ObjectType)DeepCloneNode(EntityType, cloneCommentGuid: false);
        IBlockList<EntityDeclaration> ClonedIndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateBlockListCopy(IndexParameterBlocks);
        IBlockList<Identifier> ClonedModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(ModifiedQueryBlocks);
        IOptionalReference<Body> OptionalGetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(GetterBody);
        IOptionalReference<Body> OptionalSetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(SetterBody);
        IndexerFeature Result = new(Documentation, ExportIdentifier, export, ClonedEntityType, ClonedIndexParameterBlocks, parameterEnd, ClonedModifiedQueryBlocks, OptionalGetterBody, OptionalSetterBody);

        return Result;
    }
}
