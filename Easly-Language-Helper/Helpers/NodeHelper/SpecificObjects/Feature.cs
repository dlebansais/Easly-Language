namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;
    using BaseNode;
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
            Result.EntityType = CreateDefaultType();
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
            Result.EntityType = CreateDefaultType();
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
            Result.EntityType = CreateDefaultType();
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
            Result.EntityType = CreateDefaultType();
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
        public static Feature CreateInitializedFeature(Type nodeType, Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, IBlockList<Assertion> ensureBlocks, Expression constantValue, IBlockList<CommandOverload> commandOverloadBlocks, OnceChoice once, IBlockList<QueryOverload> queryOverloadBlocks, UtilityType propertyKind, IBlockList<Identifier> modifiedQueryBlocks, IOptionalReference<Body> getterBody, IOptionalReference<Body> setterBody, IBlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));

            if (nodeType == typeof(AttributeFeature))
                return CreateInitializedAttributeFeature(documentation, exportIdentifier, export, entityName, entityType, ensureBlocks);
            else if (nodeType == typeof(ConstantFeature))
                return CreateInitializedConstantFeature(documentation, exportIdentifier, export, entityName, entityType, constantValue);
            else if (nodeType == typeof(CreationFeature))
                return CreateInitializedCreationFeature(documentation, exportIdentifier, export, entityName, commandOverloadBlocks);
            else if (nodeType == typeof(FunctionFeature))
                return CreateInitializedFunctionFeature(documentation, exportIdentifier, export, entityName, once, queryOverloadBlocks);
            else if (nodeType == typeof(ProcedureFeature))
                return CreateInitializedProcedureFeature(documentation, exportIdentifier, export, entityName, commandOverloadBlocks);
            else if (nodeType == typeof(PropertyFeature))
                return CreateInitializedPropertyFeature(documentation, exportIdentifier, export, entityName, entityType, propertyKind, modifiedQueryBlocks, getterBody, setterBody);
            else if (nodeType == typeof(IndexerFeature))
                return CreateInitializedIndexerFeature(documentation, exportIdentifier, export, entityType, modifiedQueryBlocks, getterBody, setterBody, indexParameterBlocks, parameterEnd);
            else
                throw new ArgumentOutOfRangeException($"{nameof(nodeType)}: {nodeType.FullName}");
        }

        private static AttributeFeature CreateInitializedAttributeFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, IBlockList<Assertion> ensureBlocks)
        {
            AttributeFeature Result = new AttributeFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? (Name)DeepCloneNode(entityName, cloneCommentGuid: false) : CreateEmptyName();
            Result.EntityType = entityType != null ? (ObjectType)DeepCloneNode(entityType, cloneCommentGuid: false) : CreateDefaultType();
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);

            return Result;
        }

        private static ConstantFeature CreateInitializedConstantFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, Expression constantValue)
        {
            ConstantFeature Result = new ConstantFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? (Name)DeepCloneNode(entityName, cloneCommentGuid: false) : CreateEmptyName();
            Result.EntityType = entityType != null ? (ObjectType)DeepCloneNode(entityType, cloneCommentGuid: false) : CreateDefaultType();
            Result.ConstantValue = constantValue != null ? (Expression)DeepCloneNode(constantValue, cloneCommentGuid: false) : CreateDefaultExpression();

            return Result;
        }

        private static CreationFeature CreateInitializedCreationFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, IBlockList<CommandOverload> commandOverloadBlocks)
        {
            CreationFeature Result = new CreationFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? (Name)DeepCloneNode(entityName, cloneCommentGuid: false) : CreateEmptyName();
            if (commandOverloadBlocks != null)
            {
                if (commandOverloadBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(commandOverloadBlocks)} must not be empty");
                Debug.Assert(commandOverloadBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

                Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateBlockListCopy(commandOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateSimpleBlockList(CreateEmptyCommandOverload());

            return Result;
        }

        private static FunctionFeature CreateInitializedFunctionFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, OnceChoice once, IBlockList<QueryOverload> queryOverloadBlocks)
        {
            FunctionFeature Result = new FunctionFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? (Name)DeepCloneNode(entityName, cloneCommentGuid: false) : CreateEmptyName();
            Result.Once = once;
            if (queryOverloadBlocks != null)
            {
                if (queryOverloadBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(queryOverloadBlocks)} must not be empty");
                Debug.Assert(queryOverloadBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");
                Result.OverloadBlocks = BlockListHelper<QueryOverload>.CreateBlockListCopy(queryOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<QueryOverload>.CreateSimpleBlockList(CreateEmptyQueryOverload());

            return Result;
        }

        private static IndexerFeature CreateInitializedIndexerFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, ObjectType entityType, IBlockList<Identifier> modifiedQueryBlocks, IOptionalReference<Body> getterBody, IOptionalReference<Body> setterBody, IBlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
        {
            IndexerFeature Result = new IndexerFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityType = entityType != null ? (ObjectType)DeepCloneNode(entityType, cloneCommentGuid: false) : CreateDefaultType();
            if (indexParameterBlocks != null)
            {
                if (indexParameterBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(indexParameterBlocks)} must not be empty");
                Debug.Assert(indexParameterBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

                Result.IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateBlockListCopy(indexParameterBlocks);
            }
            else
                Result.IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(CreateEmptyEntityDeclaration());
            Result.ParameterEnd = parameterEnd;
            Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(setterBody);

            return Result;
        }

        private static ProcedureFeature CreateInitializedProcedureFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, IBlockList<CommandOverload> commandOverloadBlocks)
        {
            ProcedureFeature Result = new ProcedureFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? (Name)DeepCloneNode(entityName, cloneCommentGuid: false) : CreateEmptyName();
            if (commandOverloadBlocks != null)
            {
                if (commandOverloadBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(commandOverloadBlocks)} must not be empty");
                Debug.Assert(commandOverloadBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");
                Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateBlockListCopy(commandOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<CommandOverload>.CreateSimpleBlockList(CreateEmptyCommandOverload());

            return Result;
        }

        private static PropertyFeature CreateInitializedPropertyFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, UtilityType propertyKind, IBlockList<Identifier> modifiedQueryBlocks, IOptionalReference<Body> getterBody, IOptionalReference<Body> setterBody)
        {
            PropertyFeature Result = new PropertyFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? (Identifier)DeepCloneNode(exportIdentifier, cloneCommentGuid: false) : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? (Name)DeepCloneNode(entityName, cloneCommentGuid: false) : CreateEmptyName();
            Result.EntityType = entityType != null ? (ObjectType)DeepCloneNode(entityType, cloneCommentGuid: false) : CreateDefaultType();
            Result.PropertyKind = propertyKind;
            Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(setterBody);

            return Result;
        }
    }
}
