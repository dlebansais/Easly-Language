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
        public static IDeferredBody CreateEmptyDeferredBody()
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IEffectiveBody CreateEmptyEffectiveBody()
        {
            EffectiveBody Result = new EffectiveBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            Result.BodyInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.ExceptionHandlerBlocks = BlockListHelper<IExceptionHandler, ExceptionHandler>.CreateEmptyBlockList();

            return Result;
        }

        public static IExternBody CreateEmptyExternBody()
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IPrecursorBody CreateEmptyPrecursorBody()
        {
            PrecursorBody Result = new PrecursorBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        public static IBody CreateInitializedBody(Type nodeType, IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks, IBlockList<IEntityDeclaration, EntityDeclaration> entityDeclarationBlocks, IBlockList<IInstruction, Instruction> bodyInstructionBlocks, IBlockList<IExceptionHandler, ExceptionHandler> exceptionHandlerBlocks, IOptionalReference<IObjectType> ancestorType)
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

        public static IDeferredBody CreateInitializedDeferredBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks)
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        public static IEffectiveBody CreateInitializedEffectiveBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks, IBlockList<IEntityDeclaration, EntityDeclaration> entityDeclarationBlocks, IBlockList<IInstruction, Instruction> bodyInstructionBlocks, IBlockList<IExceptionHandler, ExceptionHandler> exceptionHandlerBlocks)
        {
            EffectiveBody Result = new EffectiveBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateBlockListCopy(entityDeclarationBlocks);
            Result.BodyInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateBlockListCopy(bodyInstructionBlocks);
            Result.ExceptionHandlerBlocks = BlockListHelper<IExceptionHandler, ExceptionHandler>.CreateBlockListCopy(exceptionHandlerBlocks);

            return Result;
        }

        public static IExternBody CreateInitializedExternBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks)
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        public static IPrecursorBody CreateInitializedPrecursorBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks, IOptionalReference<IObjectType> ancestorType)
        {
            PrecursorBody Result = new PrecursorBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReferenceCopy(ancestorType);

            return Result;
        }
    }
}
