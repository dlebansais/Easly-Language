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
        public static DeferredBody CreateEmptyDeferredBody()
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return Result;
        }

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

        public static ExternBody CreateEmptyExternBody()
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static PrecursorBody CreateEmptyPrecursorBody()
        {
            PrecursorBody Result = new PrecursorBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        public static Body CreateInitializedBody(Type nodeType, Document documentation, BlockList<Assertion> requireBlocks, BlockList<Assertion> ensureBlocks, BlockList<Identifier> exceptionIdentifierBlocks, BlockList<EntityDeclaration> entityDeclarationBlocks, BlockList<Instruction> bodyInstructionBlocks, BlockList<ExceptionHandler> exceptionHandlerBlocks, OptionalReference<ObjectType> ancestorType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));

            if (nodeType == typeof(DeferredBody))
                return CreateInitializedDeferredBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks);
            else if (nodeType == typeof(EffectiveBody))
                return CreateInitializedEffectiveBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks, entityDeclarationBlocks, bodyInstructionBlocks, exceptionHandlerBlocks);
            else if (nodeType == typeof(ExternBody))
                return CreateInitializedExternBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks);
            else if (nodeType == typeof(PrecursorBody))
                return CreateInitializedPrecursorBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks, ancestorType);
            else
                throw new ArgumentOutOfRangeException($"{nameof(nodeType)}: {nodeType.FullName}");
        }

        public static DeferredBody CreateInitializedDeferredBody(Document documentation, BlockList<Assertion> requireBlocks, BlockList<Assertion> ensureBlocks, BlockList<Identifier> exceptionIdentifierBlocks)
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        public static EffectiveBody CreateInitializedEffectiveBody(Document documentation, BlockList<Assertion> requireBlocks, BlockList<Assertion> ensureBlocks, BlockList<Identifier> exceptionIdentifierBlocks, BlockList<EntityDeclaration> entityDeclarationBlocks, BlockList<Instruction> bodyInstructionBlocks, BlockList<ExceptionHandler> exceptionHandlerBlocks)
        {
            EffectiveBody Result = new EffectiveBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
            Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateBlockListCopy(entityDeclarationBlocks);
            Result.BodyInstructionBlocks = BlockListHelper<Instruction>.CreateBlockListCopy(bodyInstructionBlocks);
            Result.ExceptionHandlerBlocks = BlockListHelper<ExceptionHandler>.CreateBlockListCopy(exceptionHandlerBlocks);

            return Result;
        }

        public static ExternBody CreateInitializedExternBody(Document documentation, BlockList<Assertion> requireBlocks, BlockList<Assertion> ensureBlocks, BlockList<Identifier> exceptionIdentifierBlocks)
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        public static PrecursorBody CreateInitializedPrecursorBody(Document documentation, BlockList<Assertion> requireBlocks, BlockList<Assertion> ensureBlocks, BlockList<Identifier> exceptionIdentifierBlocks, OptionalReference<ObjectType> ancestorType)
        {
            PrecursorBody Result = new PrecursorBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReferenceCopy(ancestorType);

            return Result;
        }
    }
}
