﻿namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

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
            AnchoredType Result = new AnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AnchoredName = anchoredName;
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
            QueryOverloadType FirstOverload = CreateEmptyQueryOverloadType(returnType);

            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
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
            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = overloadBlocks;

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
            if (typeArgumentList == null) throw new ArgumentNullException(nameof(typeArgumentList));
            if (typeArgumentList.Count == 0) throw new ArgumentException($"{nameof(typeArgumentList)} must have at least one type argument");

            GenericType Result = new GenericType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.TypeArgumentBlocks = BlockListHelper<TypeArgument>.CreateBlockListFromNodeList(typeArgumentList);

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

        /// <summary>
        /// Creates a new instance of a <see cref="IndexerType"/> with provided values.
        /// </summary>
        /// <param name="baseType">The base type.</param>
        /// <param name="entityType">The entity type.</param>
        /// <param name="parameter">The indexing parameter.</param>
        /// <returns>The created instance.</returns>
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
            CommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
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
            CommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = overloadBlocks;

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

        /// <summary>
        /// Creates a new instance of a <see cref="SimpleType"/> with provided values.
        /// </summary>
        /// <param name="sharing">The sharing type.</param>
        /// <param name="classIdentifier">The class identifier.</param>
        /// <returns>The created instance.</returns>
        public static SimpleType CreateSimpleType(SharingType sharing, Identifier classIdentifier)
        {
            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.Sharing = sharing;
            SimpleSimpleType.ClassIdentifier = classIdentifier;

            return SimpleSimpleType;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="TupleType"/> with a single field.
        /// </summary>
        /// <param name="entityDeclaration">The field.</param>
        /// <returns>The created instance.</returns>
        public static TupleType CreateTupleType(EntityDeclaration entityDeclaration)
        {
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(entityDeclaration);

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
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Sharing = sharing;
            Result.EntityDeclarationBlocks = entityDeclarationBlocks;

            return Result;
        }
    }
}
