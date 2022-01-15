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
        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        DeferredBody Result = new DeferredBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates an instance of a <see cref="EffectiveBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static EffectiveBody CreateEmptyEffectiveBody()
    {
        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        IBlockList<Instruction> BodyInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        IBlockList<ExceptionHandler> ExceptionHandlerBlocks = BlockListHelper<ExceptionHandler>.CreateEmptyBlockList();
        EffectiveBody Result = new EffectiveBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks, EntityDeclarationBlocks, BodyInstructionBlocks, ExceptionHandlerBlocks);

        return Result;
    }

    /// <summary>
    /// Creates an instance of a <see cref="ExternBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ExternBody CreateEmptyExternBody()
    {
        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        ExternBody Result = new ExternBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates an instance of a <see cref="PrecursorBody"/> with empty values.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static PrecursorBody CreateEmptyPrecursorBody()
    {
        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        PrecursorBody Result = new PrecursorBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks, AncestorType);

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
        Document Documentation = CreateDocumentationCopy(documentation);
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
        DeferredBody Result = new DeferredBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

        return Result;
    }

    private static EffectiveBody CreateInitializedEffectiveBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IBlockList<EntityDeclaration>? entityDeclarationBlocks, IBlockList<Instruction>? bodyInstructionBlocks, IBlockList<ExceptionHandler>? exceptionHandlerBlocks)
    {
        Contract.RequireNotNull(entityDeclarationBlocks, out IBlockList<EntityDeclaration> EntityDeclarationBlocks);
        Contract.RequireNotNull(bodyInstructionBlocks, out IBlockList<Instruction> BodyInstructionBlocks);
        Contract.RequireNotNull(exceptionHandlerBlocks, out IBlockList<ExceptionHandler> ExceptionHandlerBlocks);

        Document Documentation = CreateDocumentationCopy(documentation);
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
        IBlockList<EntityDeclaration> ClonedEntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateBlockListCopy(EntityDeclarationBlocks);
        IBlockList<Instruction> ClonedBodyInstructionBlocks = BlockListHelper<Instruction>.CreateBlockListCopy(BodyInstructionBlocks);
        IBlockList<ExceptionHandler> ClonedExceptionHandlerBlocks = BlockListHelper<ExceptionHandler>.CreateBlockListCopy(ExceptionHandlerBlocks);
        EffectiveBody Result = new EffectiveBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks, ClonedEntityDeclarationBlocks, ClonedBodyInstructionBlocks, ClonedExceptionHandlerBlocks);

        return Result;
    }

    private static ExternBody CreateInitializedExternBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
    {
        Document Documentation = CreateDocumentationCopy(documentation);
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
        ExternBody Result = new ExternBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

        return Result;
    }

    private static PrecursorBody CreateInitializedPrecursorBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IOptionalReference<ObjectType>? ancestorType)
    {
        Contract.RequireNotNull(ancestorType, out IOptionalReference<ObjectType> AncestorType);

        Document Documentation = CreateDocumentationCopy(documentation);
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
        IOptionalReference<ObjectType> ClonedAncestorType = OptionalReferenceHelper<ObjectType>.CreateReferenceCopy(AncestorType);
        PrecursorBody Result = new PrecursorBody(Documentation, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks, ClonedAncestorType);

        return Result;
    }
}
