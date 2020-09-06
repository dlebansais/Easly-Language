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
        public static IAsLongAsInstruction CreateAsLongAsInstruction(IExpression continueCondition, IContinuation continuation)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = BlockListHelper<IContinuation, Continuation>.CreateSimpleBlockList(continuation);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IAsLongAsInstruction CreateAsLongAsInstruction(IExpression continueCondition, IBlockList<IContinuation, Continuation> continuationBlocks)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = continuationBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IAsLongAsInstruction CreateAsLongAsInstruction(IExpression continueCondition, IBlockList<IContinuation, Continuation> continuationBlocks, IScope elseInstructions)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = continuationBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        public static IContinuation CreateEmptyContinuation()
        {
            Continuation Result = new Continuation();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();
            Result.CleanupBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();

            return Result;
        }

        public static IAssignmentInstruction CreateAssignmentInstruction(List<IQualifiedName> assignmentList, IExpression source)
        {
            if (assignmentList == null) throw new ArgumentNullException(nameof(assignmentList));
            if (assignmentList.Count == 0) throw new ArgumentException($"{nameof(assignmentList)} must have at least one assignee");

            Debug.Assert(assignmentList.Count > 0);

            AssignmentInstruction Result = new AssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.DestinationBlocks = BlockListHelper<IQualifiedName, QualifiedName>.CreateBlockList(assignmentList);
            Result.Source = source;

            return Result;
        }

        public static IAssignmentInstruction CreateAssignmentInstruction(IBlockList<IQualifiedName, QualifiedName> destinationBlocks, IExpression source)
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

        public static IAttachmentInstruction CreateAttachmentInstruction(IExpression source, List<IName> nameList)
        {
            IObjectType AttachType = CreateDefaultType();
            IAttachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = BlockListHelper<IName, Name>.CreateBlockList(nameList);
            Result.AttachmentBlocks = BlockListHelper<IAttachment, Attachment>.CreateSimpleBlockList(FirstAttachment);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IAttachmentInstruction CreateAttachmentInstruction(IExpression source, IBlockList<IName, Name> entityNameBlocks, IBlockList<IAttachment, Attachment> attachmentBlocks)
        {
            IObjectType AttachType = CreateDefaultType();
            IAttachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = entityNameBlocks;
            Result.AttachmentBlocks = attachmentBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IAttachmentInstruction CreateAttachmentInstruction(IExpression source, IBlockList<IName, Name> entityNameBlocks, IBlockList<IAttachment, Attachment> attachmentBlocks, IScope elseInstructions)
        {
            IObjectType AttachType = CreateDefaultType();
            IAttachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = entityNameBlocks;
            Result.AttachmentBlocks = attachmentBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        public static ICheckInstruction CreateCheckInstruction(IExpression booleanExpression)
        {
            CheckInstruction Result = new CheckInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BooleanExpression = booleanExpression;

            return Result;
        }

        public static ICommandInstruction CreateCommandInstruction(IQualifiedName command, List<IArgument> argumentList)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static ICommandInstruction CreateCommandInstruction(IQualifiedName command, IBlockList<IArgument, Argument> argumentBlocks)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static ICreateInstruction CreateCreateInstruction(IIdentifier entityIdentifier, IIdentifier creationRoutineIdentifier, List<IArgument> argumentList)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);
            Result.Processor = OptionalReferenceHelper<IQualifiedName>.CreateReference(CreateEmptyQualifiedName());

            return Result;
        }

        public static ICreateInstruction CreateCreateInstruction(IIdentifier entityIdentifier, IIdentifier creationRoutineIdentifier, IBlockList<IArgument, Argument> argumentBlocks)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = argumentBlocks;
            Result.Processor = OptionalReferenceHelper<IQualifiedName>.CreateReference(CreateEmptyQualifiedName());

            return Result;
        }

        public static ICreateInstruction CreateCreateInstruction(IIdentifier entityIdentifier, IIdentifier creationRoutineIdentifier, IBlockList<IArgument, Argument> argumentBlocks, IQualifiedName processor)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = argumentBlocks;
            Result.Processor = OptionalReferenceHelper<IQualifiedName>.CreateReference(processor);
            Result.Processor.Assign();

            return Result;
        }

        public static IDebugInstruction CreateEmptyDebugInstruction()
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static IDebugInstruction CreateSimpleDebugInstruction(IInstruction instruction)
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateSimpleScope(instruction);

            return Result;
        }

        public static IForLoopInstruction CreateForLoopInstruction(IExpression whileCondition)
        {
            ForLoopInstruction Result = new ForLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            Result.InitInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.WhileCondition = whileCondition;
            Result.LoopInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.IterationInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.Variant = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        public static IForLoopInstruction CreateForLoopInstruction(IInstruction instruction)
        {
            ForLoopInstruction Result = new ForLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            Result.InitInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.WhileCondition = CreateEmptyQueryExpression();
            Result.LoopInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateSimpleBlockList(instruction);
            Result.IterationInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.Variant = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        public static IIfThenElseInstruction CreateIfThenElseInstruction(IConditional firstConditional)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = BlockListHelper<IConditional, Conditional>.CreateSimpleBlockList(firstConditional);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IIfThenElseInstruction CreateIfThenElseInstruction(IBlockList<IConditional, Conditional> conditionalBlocks)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = conditionalBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IIfThenElseInstruction CreateIfThenElseInstruction(IBlockList<IConditional, Conditional> conditionalBlocks, IScope elseInstructions)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = conditionalBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        public static IIndexAssignmentInstruction CreateIndexAssignmentInstruction(IQualifiedName destination, List<IArgument> argumentList, IExpression source)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            IndexAssignmentInstruction Result = new IndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);
            Result.Source = source;

            return Result;
        }

        public static IIndexAssignmentInstruction CreateIndexAssignmentInstruction(IQualifiedName destination, IBlockList<IArgument, Argument> argumentBlocks, IExpression source)
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

        public static IInspectInstruction CreateInspectInstruction(IExpression source)
        {
            IExpression FirstExpression = CreateDefaultManifestNumberExpression();
            IWith FirstWith = CreateWith(FirstExpression);

            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = BlockListHelper<IWith, With>.CreateSimpleBlockList(FirstWith);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IInspectInstruction CreateInspectInstruction(IExpression source, IWith with)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = BlockListHelper<IWith, With>.CreateSimpleBlockList(with);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IInspectInstruction CreateInspectInstruction(IExpression source, IBlockList<IWith, With> withBlocks)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = withBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IInspectInstruction CreateInspectInstruction(IExpression source, IBlockList<IWith, With> withBlocks, IScope elseInstructions)
        {
            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = withBlocks;
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(elseInstructions);
            Result.ElseInstructions.Assign();

            return Result;
        }

        public static IKeywordAssignmentInstruction CreateKeywordAssignmentInstruction(Keyword destination, IExpression source)
        {
            KeywordAssignmentInstruction Result = new KeywordAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.Source = source;

            return Result;
        }

        public static IOverLoopInstruction CreateOverLoopInstruction(IExpression overList, List<IName> nameList)
        {
            if (nameList == null) throw new ArgumentNullException(nameof(nameList));
            if (nameList.Count == 0) throw new ArgumentException($"{nameof(nameList)} must have at least one name");

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = BlockListHelper<IName, Name>.CreateBlockList(nameList);
            Result.Iteration = IterationType.Single;
            Result.LoopInstructions = CreateEmptyScope();
            Result.ExitEntityName = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();

            return Result;
        }

        public static IOverLoopInstruction CreateOverLoopInstruction(IExpression overList, List<IName> nameList, IInstruction instruction)
        {
            if (nameList == null) throw new ArgumentNullException(nameof(nameList));
            if (nameList.Count == 0) throw new ArgumentException($"{nameof(nameList)} must have at least one name");

            Debug.Assert(nameList.Count > 0);

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = BlockListHelper<IName, Name>.CreateBlockList(nameList);
            Result.Iteration = IterationType.Single;
            Result.LoopInstructions = CreateSimpleScope(instruction);
            Result.ExitEntityName = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();

            return Result;
        }

        public static IOverLoopInstruction CreateOverLoopInstruction(IExpression overList, IBlockList<IName, Name> indexerBlocks, IterationType iteration, IScope loopInstructions, IBlockList<IAssertion, Assertion> invariantBlocks)
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
            Result.ExitEntityName = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = invariantBlocks;

            return Result;
        }

        public static IOverLoopInstruction CreateOverLoopInstruction(IExpression overList, IBlockList<IName, Name> indexerBlocks, IterationType iteration, IScope loopInstructions, IIdentifier exitEntityName, IBlockList<IAssertion, Assertion> invariantBlocks)
        {
            if (indexerBlocks == null) throw new ArgumentNullException(nameof(indexerBlocks));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)indexerBlocks)) throw new ArgumentException($"{nameof(indexerBlocks)} must not be empty");

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = indexerBlocks;
            Result.Iteration = iteration;
            Result.LoopInstructions = loopInstructions;
            Result.ExitEntityName = OptionalReferenceHelper<IIdentifier>.CreateReference(exitEntityName);
            Result.ExitEntityName.Assign();
            Result.InvariantBlocks = invariantBlocks;

            return Result;
        }

        public static IPrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(List<IArgument> argumentList, IExpression source)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);
            Result.Source = source;

            return Result;
        }

        public static IPrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(IBlockList<IArgument, Argument> argumentBlocks, IExpression source)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (argumentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");
            Debug.Assert(argumentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;
            Result.Source = source;

            return Result;
        }

        public static IPrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(IObjectType ancestorType, IBlockList<IArgument, Argument> argumentBlocks, IExpression source)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (argumentBlocks.NodeBlockList.Count == 0) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");
            Debug.Assert(argumentBlocks.NodeBlockList[0].NodeList.Count > 0, $"A block in a block list always has at least one node");

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;
            Result.Source = source;

            return Result;
        }

        public static IPrecursorInstruction CreatePrecursorInstruction(List<IArgument> argumentList)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IPrecursorInstruction CreatePrecursorInstruction(IBlockList<IArgument, Argument> argumentBlocks)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IPrecursorInstruction CreatePrecursorInstruction(IObjectType ancestorType, IBlockList<IArgument, Argument> argumentBlocks)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IRaiseEventInstruction CreateRaiseEventInstruction(IIdentifier queryIdentifier)
        {
            RaiseEventInstruction Result = new RaiseEventInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.QueryIdentifier = queryIdentifier;
            Result.Event = EventType.Single;

            return Result;
        }

        public static IReleaseInstruction CreateReleaseInstruction(IQualifiedName entityName)
        {
            ReleaseInstruction Result = new ReleaseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityName = entityName;

            return Result;
        }

        public static IThrowInstruction CreateThrowInstruction(IObjectType exceptionType, IIdentifier creationRoutineIdentifier, List<IArgument> argumentList)
        {
            ThrowInstruction Result = new ThrowInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExceptionType = exceptionType;
            Result.CreationRoutine = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IThrowInstruction CreateThrowInstruction(IObjectType exceptionType, IIdentifier creationRoutineIdentifier, IBlockList<IArgument, Argument> argumentBlocks)
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
