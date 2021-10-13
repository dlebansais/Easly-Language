namespace BaseNodeHelper
{
    using System;
    using BaseNode;
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
        public static Body CreateInitializedBody(Type nodeType, Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IBlockList<EntityDeclaration> entityDeclarationBlocks, IBlockList<Instruction> bodyInstructionBlocks, IBlockList<ExceptionHandler> exceptionHandlerBlocks, IOptionalReference<ObjectType> ancestorType)
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

        private static DeferredBody CreateInitializedDeferredBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        private static EffectiveBody CreateInitializedEffectiveBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IBlockList<EntityDeclaration> entityDeclarationBlocks, IBlockList<Instruction> bodyInstructionBlocks, IBlockList<ExceptionHandler> exceptionHandlerBlocks)
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

        private static ExternBody CreateInitializedExternBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        private static PrecursorBody CreateInitializedPrecursorBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IOptionalReference<ObjectType> ancestorType)
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
