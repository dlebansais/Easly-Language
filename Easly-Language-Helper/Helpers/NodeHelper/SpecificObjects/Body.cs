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
    /// Creates an instance of a <see cref="DeferredBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static DeferredBody CreateEmptyDeferredBody()
    {
        DeferredBody Result = new DeferredBody();
        Result.Documentation = CreateEmptyDocumentation();
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

        return Result;
    }

    /// <summary>
    /// Creates an instance of a <see cref="EffectiveBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static EffectiveBody CreateEmptyEffectiveBody()
    {
        EffectiveBody Result = new EffectiveBody();
        Result.Documentation = CreateEmptyDocumentation();
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        Result.BodyInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Result.ExceptionHandlerBlocks = BlockListHelper<ExceptionHandler>.CreateEmptyBlockList();

        return Result;
    }

    /// <summary>
    /// Creates an instance of a <see cref="ExternBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ExternBody CreateEmptyExternBody()
    {
        ExternBody Result = new ExternBody();
        Result.Documentation = CreateEmptyDocumentation();
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

        return Result;
    }

    /// <summary>
    /// Creates an instance of a <see cref="PrecursorBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static PrecursorBody CreateEmptyPrecursorBody()
    {
        PrecursorBody Result = new PrecursorBody();
        Result.Documentation = CreateEmptyDocumentation();
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());

        return Result;
    }

    /// <summary>
    /// Creates an instance of an object inheriting from <see cref="Body"/> with provided values.
    /// </summary>
    /// <param name="nodeType">The type of the object to create. Must inherit from <see cref="Body"/>.</param>
    /// <param name="documentation">The documentation.</param>
    /// <param name="requireBlocks">The require blocks.</param>
    /// <param name="ensureBlocks">The ensure blocks.</param>
    /// <param name="exceptionIdentifierBlocks">The exception identifiers blocks.</param>
    /// <param name="entityDeclarationBlocks">The local variables.</param>
    /// <param name="bodyInstructionBlocks">The instructions.</param>
    /// <param name="exceptionHandlerBlocks">The exception handlers.</param>
    /// <param name="ancestorType">The object type of the ancestor for a <see cref="PrecursorBody"/> object.</param>
    /// <returns>The created instance.</returns>
    public static Body CreateInitializedBody(Type nodeType, Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IBlockList<EntityDeclaration>? entityDeclarationBlocks = null, IBlockList<Instruction>? bodyInstructionBlocks = null, IBlockList<ExceptionHandler>? exceptionHandlerBlocks = null, IOptionalReference<ObjectType>? ancestorType = null)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(documentation, out Document Documentation);
        Contract.RequireNotNull(requireBlocks, out IBlockList<Assertion> RequireBlocks);
        Contract.RequireNotNull(ensureBlocks, out IBlockList<Assertion> EnsureBlocks);
        Contract.RequireNotNull(exceptionIdentifierBlocks, out IBlockList<Identifier> ExceptionIdentifierBlocks);

        if (NodeType == typeof(DeferredBody))
            return CreateInitializedDeferredBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);
        else if (NodeType == typeof(EffectiveBody))
            return CreateInitializedEffectiveBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks, entityDeclarationBlocks, bodyInstructionBlocks, exceptionHandlerBlocks);
        else if (NodeType == typeof(ExternBody))
            return CreateInitializedExternBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);
        else if (NodeType == typeof(PrecursorBody))
            return CreateInitializedPrecursorBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks, ancestorType);
        else
            throw new ArgumentException($"{nameof(nodeType)} must inherit from {typeof(Body).FullName}");
    }

    private static DeferredBody CreateInitializedDeferredBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
    {
        DeferredBody Result = new DeferredBody();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

        return Result;
    }

    private static EffectiveBody CreateInitializedEffectiveBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IBlockList<EntityDeclaration>? entityDeclarationBlocks, IBlockList<Instruction>? bodyInstructionBlocks, IBlockList<ExceptionHandler>? exceptionHandlerBlocks)
    {
        Contract.RequireNotNull(entityDeclarationBlocks, out IBlockList<EntityDeclaration> EntityDeclarationBlocks);
        Contract.RequireNotNull(bodyInstructionBlocks, out IBlockList<Instruction> BodyInstructionBlocks);
        Contract.RequireNotNull(exceptionHandlerBlocks, out IBlockList<ExceptionHandler> ExceptionHandlerBlocks);

        EffectiveBody Result = new EffectiveBody();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
        Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateBlockListCopy(EntityDeclarationBlocks);
        Result.BodyInstructionBlocks = BlockListHelper<Instruction>.CreateBlockListCopy(BodyInstructionBlocks);
        Result.ExceptionHandlerBlocks = BlockListHelper<ExceptionHandler>.CreateBlockListCopy(ExceptionHandlerBlocks);

        return Result;
    }

    private static ExternBody CreateInitializedExternBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
    {
        ExternBody Result = new ExternBody();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

        return Result;
    }

    private static PrecursorBody CreateInitializedPrecursorBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IOptionalReference<ObjectType>? ancestorType)
    {
        Contract.RequireNotNull(ancestorType, out IOptionalReference<ObjectType> AncestorType);

        PrecursorBody Result = new PrecursorBody();
        Result.Documentation = CreateDocumentationCopy(documentation);
        Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReferenceCopy(AncestorType);

        return Result;
    }
}
