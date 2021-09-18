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
        public static AnchoredType CreateAnchoredType(QualifiedName anchoredName, AnchorKinds anchorKinds)
        {
            AnchoredType Result = new AnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AnchoredName = anchoredName;
            Result.AnchorKind = anchorKinds;

            return Result;
        }

        public static FunctionType CreateFunctionType(ObjectType baseType, ObjectType returnType)
        {
            QueryOverloadType FirstOverload = CreateEmptyQueryOverloadType(returnType);

            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = BlockListHelper<QueryOverloadType>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static FunctionType CreateFunctionType(ObjectType baseType, IBlockList<QueryOverloadType> overloadBlocks)
        {
            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = overloadBlocks;

            return Result;
        }

        public static GenericType CreateGenericType(Identifier classIdentifier, List<TypeArgument> typeArgumentList)
        {
            if (typeArgumentList == null) throw new ArgumentNullException(nameof(typeArgumentList));
            if (typeArgumentList.Count == 0) throw new ArgumentException($"{nameof(typeArgumentList)} must have at least one type argument");

            GenericType Result = new GenericType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.TypeArgumentBlocks = BlockListHelper<TypeArgument>.CreateBlockList(typeArgumentList);

            return Result;
        }

        public static GenericType CreateGenericType(SharingType sharing, Identifier classIdentifier, IBlockList<TypeArgument> typeArgumentBlocks)
        {
            if (typeArgumentBlocks == null) throw new ArgumentNullException(nameof(typeArgumentBlocks));
            if (typeArgumentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(typeArgumentBlocks)} must not be empty");
            Debug.Assert(typeArgumentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            GenericType Result = new GenericType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Sharing = sharing;
            Result.ClassIdentifier = classIdentifier;
            Result.TypeArgumentBlocks = typeArgumentBlocks;

            return Result;
        }

        public static IndexerType CreateIndexerType(ObjectType baseType, ObjectType entityType, EntityDeclaration parameter)
        {
            IndexerType Result = new IndexerType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.IndexParameterBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(parameter);
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

        public static IndexerType CreateIndexerType(ObjectType baseType, ObjectType entityType, IBlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd, UtilityType indexerKind, IBlockList<Assertion> getRequireBlocks, IBlockList<Assertion> getEnsureBlocks, IBlockList<Identifier> getExceptionIdentifierBlocks, IBlockList<Assertion> setRequireBlocks, IBlockList<Assertion> setEnsureBlocks, IBlockList<Identifier> setExceptionIdentifierBlocks)
        {
            IndexerType Result = new IndexerType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.IndexParameterBlocks = indexParameterBlocks;
            Result.ParameterEnd = parameterEnd;
            Result.IndexerKind = indexerKind;
            Result.GetRequireBlocks = getRequireBlocks;
            Result.GetEnsureBlocks = getEnsureBlocks;
            Result.GetExceptionIdentifierBlocks = getExceptionIdentifierBlocks;
            Result.SetRequireBlocks = setRequireBlocks;
            Result.SetEnsureBlocks = setEnsureBlocks;
            Result.SetExceptionIdentifierBlocks = setExceptionIdentifierBlocks;

            return Result;
        }

        public static KeywordAnchoredType CreateKeywordAnchoredType(Keyword anchor)
        {
            KeywordAnchoredType Result = new KeywordAnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Anchor = anchor;

            return Result;
        }

        public static ProcedureType CreateProcedureType(ObjectType baseType)
        {
            CommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = BlockListHelper<CommandOverloadType>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static ProcedureType CreateProcedureType(ObjectType baseType, IBlockList<CommandOverloadType> overloadBlocks)
        {
            CommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = overloadBlocks;

            return Result;
        }

        public static PropertyType CreatePropertyType(ObjectType baseType, ObjectType entityType)
        {
            PropertyType Result = new PropertyType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.PropertyKind = UtilityType.ReadWrite;
            Result.GetEnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.GetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            Result.SetRequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.SetExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static PropertyType CreatePropertyType(ObjectType baseType, ObjectType entityType, UtilityType propertyKind, IBlockList<Assertion> getEnsureBlocks, IBlockList<Identifier> getExceptionIdentifierBlocks, IBlockList<Assertion> setRequireBlocks, IBlockList<Identifier> setExceptionIdentifierBlocks)
        {
            PropertyType Result = new PropertyType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.PropertyKind = propertyKind;
            Result.GetEnsureBlocks = getEnsureBlocks;
            Result.GetExceptionIdentifierBlocks = getExceptionIdentifierBlocks;
            Result.SetRequireBlocks = setRequireBlocks;
            Result.SetExceptionIdentifierBlocks = setExceptionIdentifierBlocks;

            return Result;
        }

        public static SimpleType CreateSimpleType(SharingType sharing, Identifier classIdentifier)
        {
            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.Sharing = sharing;
            SimpleSimpleType.ClassIdentifier = classIdentifier;

            return SimpleSimpleType;
        }

        public static TupleType CreateTupleType(EntityDeclaration firstEntityDeclaration)
        {
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(firstEntityDeclaration);

            return Result;
        }

        public static TupleType CreateTupleType(SharingType sharing, IBlockList<EntityDeclaration> entityDeclarationBlocks)
        {
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Sharing = sharing;
            Result.EntityDeclarationBlocks = entityDeclarationBlocks;

            return Result;
        }
    }
}
