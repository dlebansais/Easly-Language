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

        public static Feature CreateInitializedFeature(Type nodeType, Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, BlockList<Assertion> ensureBlocks, Expression constantValue, BlockList<CommandOverload> commandOverloadBlocks, OnceChoice once, BlockList<QueryOverload> queryOverloadBlocks, UtilityType propertyKind, BlockList<Identifier> modifiedQueryBlocks, OptionalReference<Body> getterBody, OptionalReference<Body> setterBody, BlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
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

        public static AttributeFeature CreateInitializedAttributeFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, BlockList<Assertion> ensureBlocks)
        {
            AttributeFeature Result = new AttributeFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as Name : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as ObjectType : CreateDefaultType();
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);

            return Result;
        }

        public static ConstantFeature CreateInitializedConstantFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, Expression constantValue)
        {
            ConstantFeature Result = new ConstantFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as Name : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as ObjectType : CreateDefaultType();
            Result.ConstantValue = constantValue != null ? DeepCloneNode(constantValue, cloneCommentGuid: false) as Expression : CreateDefaultExpression();

            return Result;
        }

        public static CreationFeature CreateInitializedCreationFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, BlockList<CommandOverload> commandOverloadBlocks)
        {
            CreationFeature Result = new CreationFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as Name : CreateEmptyName();
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

        public static FunctionFeature CreateInitializedFunctionFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, OnceChoice once, BlockList<QueryOverload> queryOverloadBlocks)
        {
            FunctionFeature Result = new FunctionFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as Name : CreateEmptyName();
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

        public static IndexerFeature CreateInitializedIndexerFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, ObjectType entityType, BlockList<Identifier> modifiedQueryBlocks, OptionalReference<Body> getterBody, OptionalReference<Body> setterBody, BlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
        {
            IndexerFeature Result = new IndexerFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as ObjectType : CreateDefaultType();
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

        public static ProcedureFeature CreateInitializedProcedureFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, BlockList<CommandOverload> commandOverloadBlocks)
        {
            ProcedureFeature Result = new ProcedureFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as Name : CreateEmptyName();
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

        public static PropertyFeature CreateInitializedPropertyFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, UtilityType propertyKind, BlockList<Identifier> modifiedQueryBlocks, OptionalReference<Body> getterBody, OptionalReference<Body> setterBody)
        {
            PropertyFeature Result = new PropertyFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as Identifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as Name : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as ObjectType : CreateDefaultType();
            Result.PropertyKind = propertyKind;
            Result.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<Body>.CreateReferenceCopy(setterBody);

            return Result;
        }
    }
}
