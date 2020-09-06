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
        public static IAttributeFeature CreateEmptyAttributeFeature()
        {
            AttributeFeature Result = new AttributeFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.EntityType = CreateDefaultType();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();

            return Result;
        }

        public static IConstantFeature CreateEmptyConstantFeature()
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

        public static ICreationFeature CreateEmptyCreationFeature()
        {
            ICommandOverload FirstOverload = CreateEmptyCommandOverload();

            CreationFeature Result = new CreationFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IFunctionFeature CreateEmptyFunctionFeature()
        {
            IQueryOverload FirstOverload = CreateEmptyQueryOverload();

            FunctionFeature Result = new FunctionFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.Once = OnceChoice.Normal;
            Result.OverloadBlocks = BlockListHelper<IQueryOverload, QueryOverload>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IIndexerFeature CreateEmptyIndexerFeature()
        {
            IEntityDeclaration FirstParameter = CreateEmptyEntityDeclaration();

            IndexerFeature Result = new IndexerFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityType = CreateDefaultType();
            Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(FirstParameter);
            Result.ParameterEnd = ParameterEndStatus.Closed;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());
            Result.GetterBody.Assign();
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());

            return Result;
        }

        public static IProcedureFeature CreateEmptyProcedureFeature()
        {
            ICommandOverload FirstOverload = CreateEmptyCommandOverload();

            ProcedureFeature Result = new ProcedureFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IPropertyFeature CreateEmptyPropertyFeature()
        {
            PropertyFeature Result = new PropertyFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.EntityType = CreateDefaultType();
            Result.PropertyKind = UtilityType.ReadOnly;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());

            return Result;
        }

        public static IFeature CreateInitializedFeature(Type nodeType, IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, IBlockList<IAssertion, Assertion> ensureBlocks, IExpression constantValue, IBlockList<ICommandOverload, CommandOverload> commandOverloadBlocks, OnceChoice once, IBlockList<IQueryOverload, QueryOverload> queryOverloadBlocks, UtilityType propertyKind, IBlockList<IIdentifier, Identifier> modifiedQueryBlocks, IOptionalReference<IBody> getterBody, IOptionalReference<IBody> setterBody, IBlockList<IEntityDeclaration, EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
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

        public static IAttributeFeature CreateInitializedAttributeFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, IBlockList<IAssertion, Assertion> ensureBlocks)
        {
            AttributeFeature Result = new AttributeFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);

            return Result;
        }

        public static IConstantFeature CreateInitializedConstantFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, IExpression constantValue)
        {
            ConstantFeature Result = new ConstantFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            Result.ConstantValue = constantValue != null ? DeepCloneNode(constantValue, cloneCommentGuid: false) as IExpression : CreateDefaultExpression();

            return Result;
        }

        public static ICreationFeature CreateInitializedCreationFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IBlockList<ICommandOverload, CommandOverload> commandOverloadBlocks)
        {
            CreationFeature Result = new CreationFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            if (commandOverloadBlocks != null)
            {
                Debug.Assert(commandOverloadBlocks.NodeBlockList.Count > 0);
                Debug.Assert(commandOverloadBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateBlockListCopy(commandOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(CreateEmptyCommandOverload());

            return Result;
        }

        public static IFunctionFeature CreateInitializedFunctionFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, OnceChoice once, IBlockList<IQueryOverload, QueryOverload> queryOverloadBlocks)
        {
            FunctionFeature Result = new FunctionFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.Once = once;
            if (queryOverloadBlocks != null)
            {
                Debug.Assert(queryOverloadBlocks.NodeBlockList.Count > 0);
                Debug.Assert(queryOverloadBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.OverloadBlocks = BlockListHelper<IQueryOverload, QueryOverload>.CreateBlockListCopy(queryOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<IQueryOverload, QueryOverload>.CreateSimpleBlockList(CreateEmptyQueryOverload());

            return Result;
        }

        public static IIndexerFeature CreateInitializedIndexerFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IObjectType entityType, IBlockList<IIdentifier, Identifier> modifiedQueryBlocks, IOptionalReference<IBody> getterBody, IOptionalReference<IBody> setterBody, IBlockList<IEntityDeclaration, EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
        {
            IndexerFeature Result = new IndexerFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            if (indexParameterBlocks != null)
            {
                Debug.Assert(indexParameterBlocks.NodeBlockList.Count > 0);
                Debug.Assert(indexParameterBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateBlockListCopy(indexParameterBlocks);
            }
            else
                Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(CreateEmptyEntityDeclaration());
            Result.ParameterEnd = parameterEnd;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(setterBody);

            return Result;
        }

        public static IProcedureFeature CreateInitializedProcedureFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IBlockList<ICommandOverload, CommandOverload> commandOverloadBlocks)
        {
            ProcedureFeature Result = new ProcedureFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            if (commandOverloadBlocks != null)
            {
                Debug.Assert(commandOverloadBlocks.NodeBlockList.Count > 0);
                Debug.Assert(commandOverloadBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateBlockListCopy(commandOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(CreateEmptyCommandOverload());

            return Result;
        }

        public static IPropertyFeature CreateInitializedPropertyFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, UtilityType propertyKind, IBlockList<IIdentifier, Identifier> modifiedQueryBlocks, IOptionalReference<IBody> getterBody, IOptionalReference<IBody> setterBody)
        {
            PropertyFeature Result = new PropertyFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            Result.PropertyKind = propertyKind;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(setterBody);

            return Result;
        }
    }
}
