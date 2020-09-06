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
        public static IAnchoredType CreateAnchoredType(IQualifiedName anchoredName, AnchorKinds anchorKinds)
        {
            AnchoredType Result = new AnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AnchoredName = anchoredName;
            Result.AnchorKind = anchorKinds;

            return Result;
        }

        public static IFunctionType CreateFunctionType(IObjectType baseType, IObjectType returnType)
        {
            IQueryOverloadType FirstOverload = CreateEmptyQueryOverloadType(returnType);

            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = BlockListHelper<IQueryOverloadType, QueryOverloadType>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IFunctionType CreateFunctionType(IObjectType baseType, IBlockList<IQueryOverloadType, QueryOverloadType> overloadBlocks)
        {
            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = overloadBlocks;

            return Result;
        }

        public static IGenericType CreateGenericType(IIdentifier classIdentifier, List<ITypeArgument> typeArgumentList)
        {
            if (typeArgumentList == null) throw new ArgumentNullException(nameof(typeArgumentList));
            if (typeArgumentList.Count == 0) throw new ArgumentException($"{nameof(typeArgumentList)} must have at least one type argument");

            GenericType Result = new GenericType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.TypeArgumentBlocks = BlockListHelper<ITypeArgument, TypeArgument>.CreateBlockList(typeArgumentList);

            return Result;
        }

        public static IGenericType CreateGenericType(SharingType sharing, IIdentifier classIdentifier, IBlockList<ITypeArgument, TypeArgument> typeArgumentBlocks)
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

        public static IIndexerType CreateIndexerType(IObjectType baseType, IObjectType entityType, IEntityDeclaration parameter)
        {
            IndexerType Result = new IndexerType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(parameter);
            Result.ParameterEnd = ParameterEndStatus.Closed;
            Result.IndexerKind = UtilityType.ReadWrite;
            Result.GetRequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.GetEnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.GetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.SetRequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.SetEnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.SetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IIndexerType CreateIndexerType(IObjectType baseType, IObjectType entityType, IBlockList<IEntityDeclaration, EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd, UtilityType indexerKind, IBlockList<IAssertion, Assertion> getRequireBlocks, IBlockList<IAssertion, Assertion> getEnsureBlocks, IBlockList<IIdentifier, Identifier> getExceptionIdentifierBlocks, IBlockList<IAssertion, Assertion> setRequireBlocks, IBlockList<IAssertion, Assertion> setEnsureBlocks, IBlockList<IIdentifier, Identifier> setExceptionIdentifierBlocks)
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

        public static IKeywordAnchoredType CreateKeywordAnchoredType(Keyword anchor)
        {
            KeywordAnchoredType Result = new KeywordAnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Anchor = anchor;

            return Result;
        }

        public static IProcedureType CreateProcedureType(IObjectType baseType)
        {
            ICommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = BlockListHelper<ICommandOverloadType, CommandOverloadType>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IProcedureType CreateProcedureType(IObjectType baseType, IBlockList<ICommandOverloadType, CommandOverloadType> overloadBlocks)
        {
            ICommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = overloadBlocks;

            return Result;
        }

        public static IPropertyType CreatePropertyType(IObjectType baseType, IObjectType entityType)
        {
            PropertyType Result = new PropertyType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.PropertyKind = UtilityType.ReadWrite;
            Result.GetEnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.GetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.SetRequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.SetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IPropertyType CreatePropertyType(IObjectType baseType, IObjectType entityType, UtilityType propertyKind, IBlockList<IAssertion, Assertion> getEnsureBlocks, IBlockList<IIdentifier, Identifier> getExceptionIdentifierBlocks, IBlockList<IAssertion, Assertion> setRequireBlocks, IBlockList<IIdentifier, Identifier> setExceptionIdentifierBlocks)
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

        public static ISimpleType CreateSimpleType(SharingType sharing, IIdentifier classIdentifier)
        {
            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.Sharing = sharing;
            SimpleSimpleType.ClassIdentifier = classIdentifier;

            return SimpleSimpleType;
        }

        public static ITupleType CreateTupleType(IEntityDeclaration firstEntityDeclaration)
        {
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(firstEntityDeclaration);

            return Result;
        }

        public static ITupleType CreateTupleType(SharingType sharing, IBlockList<IEntityDeclaration, EntityDeclaration> entityDeclarationBlocks)
        {
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Sharing = sharing;
            Result.EntityDeclarationBlocks = entityDeclarationBlocks;

            return Result;
        }
    }
}
