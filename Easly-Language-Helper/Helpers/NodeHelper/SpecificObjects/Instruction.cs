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
        public static AsLongAsInstruction CreateAsLongAsInstruction(Expression continueCondition, Continuation continuation)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = BlockListHelper<Continuation>.CreateSimpleBlockList(continuation);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static AsLongAsInstruction CreateAsLongAsInstruction(Expression continueCondition, IBlockList<Continuation> continuationBlocks)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = continuationBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

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

        public static Continuation CreateEmptyContinuation()
        {
            Continuation Result = new Continuation();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();
            Result.CleanupBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();

            return Result;
        }

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

        public static AssignmentInstruction CreateAssignmentInstruction(IBlockList<QualifiedName> destinationBlocks, Expression source)
        {
            if (destinationBlocks == null) throw new ArgumentNullException(nameof(destinationBlocks));
            if (destinationBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(destinationBlocks)} must not be empty");
            Debug.Assert(destinationBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            AssignmentInstruction Result = new AssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.DestinationBlocks = destinationBlocks;
            Result.Source = source;

            return Result;
        }

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

        public static CheckInstruction CreateCheckInstruction(Expression booleanExpression)
        {
            CheckInstruction Result = new CheckInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BooleanExpression = booleanExpression;

            return Result;
        }

        public static CommandInstruction CreateCommandInstruction(QualifiedName command, List<Argument> argumentList)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static CommandInstruction CreateCommandInstruction(QualifiedName command, IBlockList<Argument> argumentBlocks)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

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

        public static DebugInstruction CreateEmptyDebugInstruction()
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static DebugInstruction CreateSimpleDebugInstruction(Instruction instruction)
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateSimpleScope(instruction);

            return Result;
        }

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

        public static IfThenElseInstruction CreateIfThenElseInstruction(Conditional firstConditional)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = BlockListHelper<Conditional>.CreateSimpleBlockList(firstConditional);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IfThenElseInstruction CreateIfThenElseInstruction(IBlockList<Conditional> conditionalBlocks)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = conditionalBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IfThenElseInstruction CreateIfThenElseInstruction(IBlockList<Conditional> conditionalBlocks, Scope elseInstructions)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = conditionalBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

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

        public static InspectInstruction CreateInspectInstruction(Expression source, With with)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = BlockListHelper<With>.CreateSimpleBlockList(with);
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static InspectInstruction CreateInspectInstruction(Expression source, IBlockList<With> withBlocks)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = withBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());

            return Result;
        }

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

        public static KeywordAssignmentInstruction CreateKeywordAssignmentInstruction(Keyword destination, Expression source)
        {
            KeywordAssignmentInstruction Result = new KeywordAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.Source = source;

            return Result;
        }

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

        public static PrecursorInstruction CreatePrecursorInstruction(List<Argument> argumentList)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static PrecursorInstruction CreatePrecursorInstruction(IBlockList<Argument> argumentBlocks)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static PrecursorInstruction CreatePrecursorInstruction(ObjectType ancestorType, IBlockList<Argument> argumentBlocks)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static RaiseEventInstruction CreateRaiseEventInstruction(Identifier queryIdentifier)
        {
            RaiseEventInstruction Result = new RaiseEventInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.QueryIdentifier = queryIdentifier;
            Result.Event = EventType.Single;

            return Result;
        }

        public static ReleaseInstruction CreateReleaseInstruction(QualifiedName entityName)
        {
            ReleaseInstruction Result = new ReleaseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityName = entityName;

            return Result;
        }

        public static ThrowInstruction CreateThrowInstruction(ObjectType exceptionType, Identifier creationRoutineIdentifier, List<Argument> argumentList)
        {
            ThrowInstruction Result = new ThrowInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExceptionType = exceptionType;
            Result.CreationRoutine = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

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
