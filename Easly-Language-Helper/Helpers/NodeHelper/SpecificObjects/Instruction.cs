namespace BaseNodeHelper
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
        /// Creates a new instance of a <see cref="AsLongAsInstruction"/> with a single continuation.
        /// </summary>
        /// <param name="continueCondition">The continue condition.</param>
        /// <param name="continuation">The continuation.</param>
        /// <returns>The created instance.</returns>
        public static AsLongAsInstruction CreateAsLongAsInstruction(Expression continueCondition, Continuation continuation)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = BlockListHelper<Continuation>.CreateSimpleBlockList(continuation);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AsLongAsInstruction"/> with provided values.
        /// </summary>
        /// <param name="continueCondition">The continue condition.</param>
        /// <param name="continuationBlocks">The list of continuations.</param>
        /// <returns>The created instance.</returns>
        public static AsLongAsInstruction CreateAsLongAsInstruction(Expression continueCondition, IBlockList<Continuation> continuationBlocks)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = continuationBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AsLongAsInstruction"/> with provided values.
        /// </summary>
        /// <param name="continueCondition">The continue condition.</param>
        /// <param name="continuationBlocks">The list of continuations.</param>
        /// <param name="elseInstructions">The instructions for the else case.</param>
        /// <returns>The created instance.</returns>
        public static AsLongAsInstruction CreateAsLongAsInstruction(Expression continueCondition, IBlockList<Continuation> continuationBlocks, Scope elseInstructions)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = continuationBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="Continuation"/> with no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Continuation CreateEmptyContinuation()
        {
            Continuation Result = new Continuation();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();
            Result.CleanupBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AssignmentInstruction"/> with the provided values.
        /// </summary>
        /// <param name="assignmentList">The list of assigned destinations.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static AssignmentInstruction CreateAssignmentInstruction(List<QualifiedName> assignmentList, Expression source)
        {
            if (assignmentList == null) throw new ArgumentNullException(nameof(assignmentList));
            if (assignmentList.Count == 0) throw new ArgumentException($"{nameof(assignmentList)} must have at least one assignee");

            Debug.Assert(assignmentList.Count > 0);

            AssignmentInstruction Result = new AssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.DestinationBlocks = BlockListHelper<QualifiedName>.CreateBlockList(assignmentList);
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AssignmentInstruction"/> with the provided values.
        /// </summary>
        /// <param name="assignmentBlocks">The list of assigned destinations.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static AssignmentInstruction CreateAssignmentInstruction(IBlockList<QualifiedName> assignmentBlocks, Expression source)
        {
            if (assignmentBlocks == null) throw new ArgumentNullException(nameof(assignmentBlocks));
            if (assignmentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(assignmentBlocks)} must not be empty");
            Debug.Assert(assignmentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            AssignmentInstruction Result = new AssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.DestinationBlocks = assignmentBlocks;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AttachmentInstruction"/> with a single attachment.
        /// </summary>
        /// <param name="source">The source exporession.</param>
        /// <param name="nameList">The list of attached names.</param>
        /// <returns>The created instance.</returns>
        public static AttachmentInstruction CreateAttachmentInstruction(Expression source, List<Name> nameList)
        {
            ObjectType AttachType = CreateDefaultType();
            Attachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = BlockListHelper<Name>.CreateBlockList(nameList);
            Result.AttachmentBlocks = BlockListHelper<Attachment>.CreateSimpleBlockList(FirstAttachment);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AttachmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="source">The source exporession.</param>
        /// <param name="entityNameBlocks">The list of attached names.</param>
        /// <param name="attachmentBlocks">The list of attachments.</param>
        /// <returns>The created instance.</returns>
        public static AttachmentInstruction CreateAttachmentInstruction(Expression source, IBlockList<Name> entityNameBlocks, IBlockList<Attachment> attachmentBlocks)
        {
            ObjectType AttachType = CreateDefaultType();
            Attachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = entityNameBlocks;
            Result.AttachmentBlocks = attachmentBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AttachmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="source">The source exporession.</param>
        /// <param name="entityNameBlocks">The list of attached names.</param>
        /// <param name="attachmentBlocks">The list of attachments.</param>
        /// <param name="elseInstructions">The instructions for the else case.</param>
        /// <returns>The created instance.</returns>
        public static AttachmentInstruction CreateAttachmentInstruction(Expression source, IBlockList<Name> entityNameBlocks, IBlockList<Attachment> attachmentBlocks, Scope elseInstructions)
        {
            ObjectType AttachType = CreateDefaultType();
            Attachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = entityNameBlocks;
            Result.AttachmentBlocks = attachmentBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CheckInstruction"/> with provided values.
        /// </summary>
        /// <param name="booleanExpression">The checked expression.</param>
        /// <returns>The created instance.</returns>
        public static CheckInstruction CreateCheckInstruction(Expression booleanExpression)
        {
            CheckInstruction Result = new CheckInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BooleanExpression = booleanExpression;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CommandInstruction"/> with provided values.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static CommandInstruction CreateCommandInstruction(QualifiedName command, List<Argument> argumentList)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CommandInstruction"/> with provided values.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static CommandInstruction CreateCommandInstruction(QualifiedName command, IBlockList<Argument> argumentBlocks)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CreateInstruction"/> with provided values.
        /// </summary>
        /// <param name="entityIdentifier">The created object identifier.</param>
        /// <param name="creationRoutineIdentifier">The creation routine identifier.</param>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static CreateInstruction CreateCreateInstruction(Identifier entityIdentifier, Identifier creationRoutineIdentifier, List<Argument> argumentList)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);
            Result.Processor = OptionalReferenceHelper<QualifiedName>.CreateReference(CreateEmptyQualifiedName());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CreateInstruction"/> with provided values.
        /// </summary>
        /// <param name="entityIdentifier">The created object identifier.</param>
        /// <param name="creationRoutineIdentifier">The creation routine identifier.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static CreateInstruction CreateCreateInstruction(Identifier entityIdentifier, Identifier creationRoutineIdentifier, IBlockList<Argument> argumentBlocks)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = argumentBlocks;
            Result.Processor = OptionalReferenceHelper<QualifiedName>.CreateReference(CreateEmptyQualifiedName());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="CreateInstruction"/> with provided values.
        /// </summary>
        /// <param name="entityIdentifier">The created object identifier.</param>
        /// <param name="creationRoutineIdentifier">The creation routine identifier.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <param name="processor">The processor on which the object is created.</param>
        /// <returns>The created instance.</returns>
        public static CreateInstruction CreateCreateInstruction(Identifier entityIdentifier, Identifier creationRoutineIdentifier, IBlockList<Argument> argumentBlocks, QualifiedName processor)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = argumentBlocks;
            Result.Processor = OptionalReferenceHelper<QualifiedName>.CreateReference(processor);
            Result.Processor.Assign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="DebugInstruction"/> with no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static DebugInstruction CreateEmptyDebugInstruction()
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="DebugInstruction"/> with a single instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        /// <returns>The created instance.</returns>
        public static DebugInstruction CreateSimpleDebugInstruction(Instruction instruction)
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateSimpleScope(instruction);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ForLoopInstruction"/> with empty content.
        /// </summary>
        /// <param name="whileCondition">The condition.</param>
        /// <returns>The created instance.</returns>
        public static ForLoopInstruction CreateForLoopInstruction(Expression whileCondition)
        {
            ForLoopInstruction Result = new ForLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            Result.InitInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
            Result.WhileCondition = whileCondition;
            Result.LoopInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
            Result.IterationInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
            Result.InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ForLoopInstruction"/> with a single instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        /// <returns>The created instance.</returns>
        public static ForLoopInstruction CreateForLoopInstruction(Instruction instruction)
        {
            ForLoopInstruction Result = new ForLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            Result.InitInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
            Result.WhileCondition = CreateEmptyQueryExpression();
            Result.LoopInstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(instruction);
            Result.IterationInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
            Result.InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            Result.Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IfThenElseInstruction"/> with a single conditional.
        /// </summary>
        /// <param name="conditional">The conditional.</param>
        /// <returns>The created instance.</returns>
        public static IfThenElseInstruction CreateIfThenElseInstruction(Conditional conditional)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = BlockListHelper<Conditional>.CreateSimpleBlockList(conditional);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IfThenElseInstruction"/> with provided values.
        /// </summary>
        /// <param name="conditionalBlocks">The list of conditionals.</param>
        /// <returns>The created instance.</returns>
        public static IfThenElseInstruction CreateIfThenElseInstruction(IBlockList<Conditional> conditionalBlocks)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = conditionalBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IfThenElseInstruction"/> with provided values.
        /// </summary>
        /// <param name="conditionalBlocks">The list of conditionals.</param>
        /// <param name="elseInstructions">The instructions for the else case.</param>
        /// <returns>The created instance.</returns>
        public static IfThenElseInstruction CreateIfThenElseInstruction(IBlockList<Conditional> conditionalBlocks, Scope elseInstructions)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = conditionalBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IndexAssignmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="argumentList">The list of arguments.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static IndexAssignmentInstruction CreateIndexAssignmentInstruction(QualifiedName destination, List<Argument> argumentList, Expression source)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            IndexAssignmentInstruction Result = new IndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IndexAssignmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static IndexAssignmentInstruction CreateIndexAssignmentInstruction(QualifiedName destination, IBlockList<Argument> argumentBlocks, Expression source)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (argumentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");
            Debug.Assert(argumentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            IndexAssignmentInstruction Result = new IndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.ArgumentBlocks = argumentBlocks;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="InspectInstruction"/> with empty content.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static InspectInstruction CreateInspectInstruction(Expression source)
        {
            Expression FirstExpression = CreateDefaultManifestNumberExpression();
            With FirstWith = CreateWith(FirstExpression);

            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = BlockListHelper<With>.CreateSimpleBlockList(FirstWith);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="InspectInstruction"/> with a single case.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <param name="with">The with case.</param>
        /// <returns>The created instance.</returns>
        public static InspectInstruction CreateInspectInstruction(Expression source, With with)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = BlockListHelper<With>.CreateSimpleBlockList(with);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="InspectInstruction"/> with provided values.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <param name="withBlocks">The list of with cases.</param>
        /// <returns>The created instance.</returns>
        public static InspectInstruction CreateInspectInstruction(Expression source, IBlockList<With> withBlocks)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = withBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="InspectInstruction"/> with provided values.
        /// </summary>
        /// <param name="source">The source expression.</param>
        /// <param name="withBlocks">The list of with cases.</param>
        /// <param name="elseInstructions">The instructions for the else case.</param>
        /// <returns>The created instance.</returns>
        public static InspectInstruction CreateInspectInstruction(Expression source, IBlockList<With> withBlocks, Scope elseInstructions)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = withBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="KeywordAssignmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="destination">The destination keyword.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static KeywordAssignmentInstruction CreateKeywordAssignmentInstruction(Keyword destination, Expression source)
        {
            KeywordAssignmentInstruction Result = new KeywordAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="OverLoopInstruction"/> with provided values.
        /// </summary>
        /// <param name="overList">The list of collections.</param>
        /// <param name="nameList">The list of indexers.</param>
        /// <returns>The created instance.</returns>
        public static OverLoopInstruction CreateOverLoopInstruction(Expression overList, List<Name> nameList)
        {
            if (nameList == null) throw new ArgumentNullException(nameof(nameList));
            if (nameList.Count == 0) throw new ArgumentException($"{nameof(nameList)} must have at least one name");

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = BlockListHelper<Name>.CreateBlockList(nameList);
            Result.Iteration = IterationType.Single;
            Result.LoopInstructions = CreateEmptyScope();
            Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="OverLoopInstruction"/> with a single instruction.
        /// </summary>
        /// <param name="overList">The list of collections.</param>
        /// <param name="nameList">The list of indexers.</param>
        /// <param name="instruction">The instruction.</param>
        /// <returns>The created instance.</returns>
        public static OverLoopInstruction CreateOverLoopInstruction(Expression overList, List<Name> nameList, Instruction instruction)
        {
            if (nameList == null) throw new ArgumentNullException(nameof(nameList));
            if (nameList.Count == 0) throw new ArgumentException($"{nameof(nameList)} must have at least one name");

            Debug.Assert(nameList.Count > 0);

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = BlockListHelper<Name>.CreateBlockList(nameList);
            Result.Iteration = IterationType.Single;
            Result.LoopInstructions = CreateSimpleScope(instruction);
            Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="OverLoopInstruction"/> with provided values.
        /// </summary>
        /// <param name="overList">The list of collections.</param>
        /// <param name="indexerBlocks">The list of indexers.</param>
        /// <param name="iteration">The iteration type.</param>
        /// <param name="loopInstructions">List of instructions in the loop.</param>
        /// <param name="invariantBlocks">The list of invariants.</param>
        /// <returns>The created instance.</returns>
        public static OverLoopInstruction CreateOverLoopInstruction(Expression overList, IBlockList<Name> indexerBlocks, IterationType iteration, Scope loopInstructions, IBlockList<Assertion> invariantBlocks)
        {
            if (indexerBlocks == null) throw new ArgumentNullException(nameof(indexerBlocks));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)indexerBlocks)) throw new ArgumentException($"{nameof(indexerBlocks)} must not be empty");

            Debug.Assert(!NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)indexerBlocks));

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = indexerBlocks;
            Result.Iteration = iteration;
            Result.LoopInstructions = loopInstructions;
            Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = invariantBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="OverLoopInstruction"/> with provided values.
        /// </summary>
        /// <param name="overList">The list of collections.</param>
        /// <param name="indexerBlocks">The list of indexers.</param>
        /// <param name="iteration">The iteration type.</param>
        /// <param name="loopInstructions">List of instructions in the loop.</param>
        /// <param name="exitEntityName">The exit entoty name.</param>
        /// <param name="invariantBlocks">The list of invariants.</param>
        /// <returns>The created instance.</returns>
        public static OverLoopInstruction CreateOverLoopInstruction(Expression overList, IBlockList<Name> indexerBlocks, IterationType iteration, Scope loopInstructions, Identifier exitEntityName, IBlockList<Assertion> invariantBlocks)
        {
            if (indexerBlocks == null) throw new ArgumentNullException(nameof(indexerBlocks));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)indexerBlocks)) throw new ArgumentException($"{nameof(indexerBlocks)} must not be empty");

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = indexerBlocks;
            Result.Iteration = iteration;
            Result.LoopInstructions = loopInstructions;
            Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(exitEntityName);
            Result.ExitEntityName.Assign();
            Result.InvariantBlocks = invariantBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexAssignmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="argumentList">The list of arguments.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(List<Argument> argumentList, Expression source)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexAssignmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(IBlockList<Argument> argumentBlocks, Expression source)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (argumentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");
            Debug.Assert(argumentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorIndexAssignmentInstruction"/> with provided values.
        /// </summary>
        /// <param name="ancestorType">The ancestor type.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <param name="source">The source expression.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(ObjectType ancestorType, IBlockList<Argument> argumentBlocks, Expression source)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (argumentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");
            Debug.Assert(argumentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;
            Result.Source = source;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorInstruction"/> with provided values.
        /// </summary>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorInstruction CreatePrecursorInstruction(List<Argument> argumentList)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorInstruction"/> with provided values.
        /// </summary>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorInstruction CreatePrecursorInstruction(IBlockList<Argument> argumentBlocks)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="PrecursorInstruction"/> with provided values.
        /// </summary>
        /// <param name="ancestorType">The ancestor type.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static PrecursorInstruction CreatePrecursorInstruction(ObjectType ancestorType, IBlockList<Argument> argumentBlocks)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="RaiseEventInstruction"/> with provided values.
        /// </summary>
        /// <param name="queryIdentifier">The query identifier.</param>
        /// <returns>The created instance.</returns>
        public static RaiseEventInstruction CreateRaiseEventInstruction(Identifier queryIdentifier)
        {
            RaiseEventInstruction Result = new RaiseEventInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.QueryIdentifier = queryIdentifier;
            Result.Event = EventType.Single;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ReleaseInstruction"/> with provided values.
        /// </summary>
        /// <param name="entityName">The released entity name.</param>
        /// <returns>The created instance.</returns>
        public static ReleaseInstruction CreateReleaseInstruction(QualifiedName entityName)
        {
            ReleaseInstruction Result = new ReleaseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityName = entityName;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ThrowInstruction"/> with provided values.
        /// </summary>
        /// <param name="exceptionType">The exception type.</param>
        /// <param name="creationRoutineIdentifier">The creation routine identifier.</param>
        /// <param name="argumentList">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static ThrowInstruction CreateThrowInstruction(ObjectType exceptionType, Identifier creationRoutineIdentifier, List<Argument> argumentList)
        {
            ThrowInstruction Result = new ThrowInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExceptionType = exceptionType;
            Result.CreationRoutine = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="ThrowInstruction"/> with provided values.
        /// </summary>
        /// <param name="exceptionType">The exception type.</param>
        /// <param name="creationRoutineIdentifier">The creation routine identifier.</param>
        /// <param name="argumentBlocks">The list of arguments.</param>
        /// <returns>The created instance.</returns>
        public static ThrowInstruction CreateThrowInstruction(ObjectType exceptionType, Identifier creationRoutineIdentifier, IBlockList<Argument> argumentBlocks)
        {
            ThrowInstruction Result = new ThrowInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExceptionType = exceptionType;
            Result.CreationRoutine = creationRoutineIdentifier;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }
    }
}
