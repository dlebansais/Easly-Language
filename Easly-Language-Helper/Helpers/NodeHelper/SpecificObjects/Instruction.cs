namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
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
    /// Creates a new instance of a <see cref="AsLongAsInstruction"/> with a single continuation.
    /// </summary>
    /// <param name="continueCondition">The continue condition.</param>
    /// <param name="continuation">The continuation.</param>
    /// <returns>The created instance.</returns>
    public static AsLongAsInstruction CreateAsLongAsInstruction(Expression continueCondition, Continuation continuation)
    {
        Contract.RequireNotNull(continueCondition, out Expression ContinueCondition);
        Contract.RequireNotNull(continuation, out Continuation Continuation);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Continuation> ContinuationBlocks = BlockListHelper<Continuation>.CreateSimpleBlockList(Continuation);
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        AsLongAsInstruction Result = new(Documentation, ContinueCondition, ContinuationBlocks, ElseInstructions);

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
        Contract.RequireNotNull(continueCondition, out Expression ContinueCondition);
        Contract.RequireNotNull(continuationBlocks, out IBlockList<Continuation> ContinuationBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ContinuationBlocks))
            throw new ArgumentException($"{nameof(continuationBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        AsLongAsInstruction Result = new(Documentation, ContinueCondition, ContinuationBlocks, ElseInstructions);

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
        Contract.RequireNotNull(continueCondition, out Expression ContinueCondition);
        Contract.RequireNotNull(continuationBlocks, out IBlockList<Continuation> ContinuationBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ContinuationBlocks))
            throw new ArgumentException($"{nameof(continuationBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> OptionalElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
        OptionalElseInstructions.Assign();
        AsLongAsInstruction Result = new(Documentation, ContinueCondition, ContinuationBlocks, OptionalElseInstructions);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Continuation"/> with no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Continuation CreateEmptyContinuation()
    {
        Document Documentation = CreateEmptyDocumentation();
        Scope Instructions = CreateEmptyScope();
        IBlockList<Instruction> CleanupBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Continuation EmptyContinuation = new(Documentation, Instructions, CleanupBlocks);

        return EmptyContinuation;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="AssignmentInstruction"/> with the provided values.
    /// </summary>
    /// <param name="assignmentList">The list of assigned destinations.</param>
    /// <param name="source">The source expression.</param>
    /// <returns>The created instance.</returns>
    public static AssignmentInstruction CreateAssignmentInstruction(List<QualifiedName> assignmentList, Expression source)
    {
        Contract.RequireNotNull(assignmentList, out List<QualifiedName> AssignmentList);
        Contract.RequireNotNull(source, out Expression Source);

        if (AssignmentList.Count == 0)
            throw new ArgumentException($"{nameof(assignmentList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<QualifiedName> DestinationBlocks = BlockListHelper<QualifiedName>.CreateBlockListFromNodeList(AssignmentList);
        AssignmentInstruction Result = new(Documentation, DestinationBlocks, Source);

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
        Contract.RequireNotNull(assignmentBlocks, out IBlockList<QualifiedName> AssignmentBlocks);
        Contract.RequireNotNull(source, out Expression Source);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AssignmentBlocks))
            throw new ArgumentException($"{nameof(assignmentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        AssignmentInstruction Result = new(Documentation, AssignmentBlocks, Source);

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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(nameList, out List<Name> NameList);

        if (NameList.Count == 0)
            throw new ArgumentException($"{nameof(nameList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Name> EntityNameBlocks = BlockListHelper<Name>.CreateBlockListFromNodeList(NameList);
        ObjectType AttachType = CreateDefaultObjectType();
        Attachment FirstAttachment = CreateAttachment(AttachType);
        IBlockList<Attachment> AttachmentBlocks = BlockListHelper<Attachment>.CreateSimpleBlockList(FirstAttachment);
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        AttachmentInstruction Result = new(Documentation, Source, EntityNameBlocks, AttachmentBlocks, ElseInstructions);

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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(entityNameBlocks, out IBlockList<Name> EntityNameBlocks);
        Contract.RequireNotNull(attachmentBlocks, out IBlockList<Attachment> AttachmentBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)EntityNameBlocks))
            throw new ArgumentException($"{nameof(entityNameBlocks)} must not be empty");

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AttachmentBlocks))
            throw new ArgumentException($"{nameof(attachmentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        AttachmentInstruction Result = new(Documentation, Source, EntityNameBlocks, AttachmentBlocks, ElseInstructions);

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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(entityNameBlocks, out IBlockList<Name> EntityNameBlocks);
        Contract.RequireNotNull(attachmentBlocks, out IBlockList<Attachment> AttachmentBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)EntityNameBlocks))
            throw new ArgumentException($"{nameof(entityNameBlocks)} must not be empty");

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AttachmentBlocks))
            throw new ArgumentException($"{nameof(attachmentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> OptionalElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
        OptionalElseInstructions.Assign();
        AttachmentInstruction Result = new(Documentation, Source, EntityNameBlocks, AttachmentBlocks, OptionalElseInstructions);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="CheckInstruction"/> with provided values.
    /// </summary>
    /// <param name="booleanExpression">The checked expression.</param>
    /// <returns>The created instance.</returns>
    public static CheckInstruction CreateCheckInstruction(Expression booleanExpression)
    {
        Contract.RequireNotNull(booleanExpression, out Expression BooleanExpression);

        Document Documentation = CreateEmptyDocumentation();
        CheckInstruction Result = new(Documentation, BooleanExpression);

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
        Contract.RequireNotNull(command, out QualifiedName Command);
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        CommandInstruction Result = new(Documentation, Command, ArgumentBlocks);

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
        Contract.RequireNotNull(command, out QualifiedName Command);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

        Document Documentation = CreateEmptyDocumentation();
        CommandInstruction Result = new(Documentation, Command, ArgumentBlocks);

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
        Contract.RequireNotNull(entityIdentifier, out Identifier EntityIdentifier);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        IOptionalReference<QualifiedName> Processor = OptionalReferenceHelper<QualifiedName>.CreateReference(CreateEmptyQualifiedName());
        CreateInstruction Result = new(Documentation, EntityIdentifier, CreationRoutineIdentifier, ArgumentBlocks, Processor);

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
        Contract.RequireNotNull(entityIdentifier, out Identifier EntityIdentifier);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<QualifiedName> Processor = OptionalReferenceHelper<QualifiedName>.CreateReference(CreateEmptyQualifiedName());
        CreateInstruction Result = new(Documentation, EntityIdentifier, CreationRoutineIdentifier, ArgumentBlocks, Processor);

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
        Contract.RequireNotNull(entityIdentifier, out Identifier EntityIdentifier);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
        Contract.RequireNotNull(processor, out QualifiedName Processor);

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<QualifiedName> OptionalProcessor = OptionalReferenceHelper<QualifiedName>.CreateReference(Processor);
        OptionalProcessor.Assign();
        CreateInstruction Result = new(Documentation, EntityIdentifier, CreationRoutineIdentifier, ArgumentBlocks, OptionalProcessor);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="DebugInstruction"/> with no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static DebugInstruction CreateEmptyDebugInstruction()
    {
        Document Documentation = CreateEmptyDocumentation();
        Scope Instructions = CreateEmptyScope();
        DebugInstruction EmptyDebugInstruction = new(Documentation, Instructions);

        return EmptyDebugInstruction;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="DebugInstruction"/> with a single instruction.
    /// </summary>
    /// <param name="instruction">The instruction.</param>
    /// <returns>The created instance.</returns>
    public static DebugInstruction CreateSimpleDebugInstruction(Instruction instruction)
    {
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        Document Documentation = CreateEmptyDocumentation();
        Scope Instructions = CreateSimpleScope(Instruction);
        DebugInstruction SimpleDebugInstruction = new(Documentation, Instructions);

        return SimpleDebugInstruction;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="DebugInstruction"/>.
    /// </summary>
    /// <param name="instructions">The instructions.</param>
    /// <returns>The created instance.</returns>
    public static DebugInstruction CreateDebugInstruction(Scope instructions)
    {
        Contract.RequireNotNull(instructions, out Scope Instructions);

        Document Documentation = CreateEmptyDocumentation();
        DebugInstruction Result = new(Documentation, Instructions);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ForLoopInstruction"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ForLoopInstruction CreateEmptyForLoopInstruction()
    {
        Document Documentation = CreateEmptyDocumentation();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        IBlockList<Instruction> InitInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Expression WhileCondition = CreateEmptyQueryExpression();
        IBlockList<Instruction> LoopInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        IBlockList<Instruction> IterationInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        IBlockList<Assertion> InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IOptionalReference<Expression> Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        ForLoopInstruction EmptyForLoopInstruction = new(Documentation, EntityDeclarationBlocks, InitInstructionBlocks, WhileCondition, LoopInstructionBlocks, IterationInstructionBlocks, InvariantBlocks, Variant);

        return EmptyForLoopInstruction;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ForLoopInstruction"/> with a single instruction.
    /// </summary>
    /// <param name="instruction">The instruction.</param>
    /// <returns>The created instance.</returns>
    public static ForLoopInstruction CreateSimpleForLoopInstruction(Instruction instruction)
    {
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        IBlockList<Instruction> InitInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Expression WhileCondition = CreateEmptyQueryExpression();
        IBlockList<Instruction> LoopInstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(Instruction);
        IBlockList<Instruction> IterationInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        IBlockList<Assertion> InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IOptionalReference<Expression> Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        ForLoopInstruction SimpleForLoopInstruction = new(Documentation, EntityDeclarationBlocks, InitInstructionBlocks, WhileCondition, LoopInstructionBlocks, IterationInstructionBlocks, InvariantBlocks, Variant);

        return SimpleForLoopInstruction;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ForLoopInstruction"/>.
    /// </summary>
    /// <param name="entityDeclarationBlocks">The local variables.</param>
    /// <param name="initInstructionBlocks">The initialization instructions.</param>
    /// <param name="whileCondition">The loop condition.</param>
    /// <param name="loopInstructionBlocks">The loop instructions.</param>
    /// <param name="iterationInstructionBlocks">The interation instructions.</param>
    /// <param name="invariantBlocks">The invariant.</param>
    /// <param name="variant">The variant.</param>
    /// <returns>The created instance.</returns>
    public static ForLoopInstruction CreateForLoopInstruction(IBlockList<EntityDeclaration> entityDeclarationBlocks, IBlockList<Instruction> initInstructionBlocks, Expression whileCondition, IBlockList<Instruction> loopInstructionBlocks, IBlockList<Instruction> iterationInstructionBlocks, IBlockList<Assertion> invariantBlocks, IOptionalReference<Expression> variant)
    {
        Contract.RequireNotNull(entityDeclarationBlocks, out IBlockList<EntityDeclaration> EntityDeclarationBlocks);
        Contract.RequireNotNull(initInstructionBlocks, out IBlockList<Instruction> InitInstructionBlocks);
        Contract.RequireNotNull(whileCondition, out Expression WhileCondition);
        Contract.RequireNotNull(loopInstructionBlocks, out IBlockList<Instruction> LoopInstructionBlocks);
        Contract.RequireNotNull(iterationInstructionBlocks, out IBlockList<Instruction> IterationInstructionBlocks);
        Contract.RequireNotNull(invariantBlocks, out IBlockList<Assertion> InvariantBlocks);
        Contract.RequireNotNull(variant, out IOptionalReference<Expression> Variant);

        Document Documentation = CreateEmptyDocumentation();
        ForLoopInstruction Result = new(Documentation, EntityDeclarationBlocks, InitInstructionBlocks, WhileCondition, LoopInstructionBlocks, IterationInstructionBlocks, InvariantBlocks, Variant);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IfThenElseInstruction"/> with a single conditional.
    /// </summary>
    /// <param name="conditional">The conditional.</param>
    /// <returns>The created instance.</returns>
    public static IfThenElseInstruction CreateIfThenElseInstruction(Conditional conditional)
    {
        Contract.RequireNotNull(conditional, out Conditional Conditional);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Conditional> ConditionalBlocks = BlockListHelper<Conditional>.CreateSimpleBlockList(Conditional);
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        IfThenElseInstruction Result = new(Documentation, ConditionalBlocks, ElseInstructions);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="IfThenElseInstruction"/> with provided values.
    /// </summary>
    /// <param name="conditionalBlocks">The list of conditionals.</param>
    /// <returns>The created instance.</returns>
    public static IfThenElseInstruction CreateIfThenElseInstruction(IBlockList<Conditional> conditionalBlocks)
    {
        Contract.RequireNotNull(conditionalBlocks, out IBlockList<Conditional> ConditionalBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ConditionalBlocks))
            throw new ArgumentException($"{nameof(conditionalBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        IfThenElseInstruction Result = new(Documentation, ConditionalBlocks, ElseInstructions);

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
        Contract.RequireNotNull(conditionalBlocks, out IBlockList<Conditional> ConditionalBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ConditionalBlocks))
            throw new ArgumentException($"{nameof(conditionalBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> OptionalElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
        OptionalElseInstructions.Assign();
        IfThenElseInstruction Result = new(Documentation, ConditionalBlocks, OptionalElseInstructions);

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
        Contract.RequireNotNull(destination, out QualifiedName Destination);
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);
        Contract.RequireNotNull(source, out Expression Source);

        if (ArgumentList.Count == 0)
            throw new ArgumentException($"{nameof(argumentList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        IndexAssignmentInstruction Result = new(Documentation, Destination, ArgumentBlocks, Source);

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
        Contract.RequireNotNull(destination, out QualifiedName Destination);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
        Contract.RequireNotNull(source, out Expression Source);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ArgumentBlocks))
            throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IndexAssignmentInstruction Result = new(Documentation, Destination, ArgumentBlocks, Source);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="InspectInstruction"/> with empty content.
    /// </summary>
    /// <param name="source">The source expression.</param>
    /// <returns>The created instance.</returns>
    public static InspectInstruction CreateInspectInstruction(Expression source)
    {
        Contract.RequireNotNull(source, out Expression Source);

        Document Documentation = CreateEmptyDocumentation();
        Expression FirstExpression = CreateDefaultManifestNumberExpression();
        With FirstWith = CreateSimpleWith(FirstExpression);
        IBlockList<With> WithBlocks = BlockListHelper<With>.CreateSimpleBlockList(FirstWith);
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        InspectInstruction Result = new(Documentation, Source, WithBlocks, ElseInstructions);

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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(with, out With With);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<With> WithBlocks = BlockListHelper<With>.CreateSimpleBlockList(With);
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        InspectInstruction Result = new(Documentation, Source, WithBlocks, ElseInstructions);

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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(withBlocks, out IBlockList<With> WithBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)WithBlocks))
            throw new ArgumentException($"{nameof(withBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(CreateEmptyScope());
        InspectInstruction Result = new(Documentation, Source, WithBlocks, ElseInstructions);

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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(withBlocks, out IBlockList<With> WithBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)WithBlocks))
            throw new ArgumentException($"{nameof(withBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Scope> OptionalElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
        OptionalElseInstructions.Assign();
        InspectInstruction Result = new(Documentation, Source, WithBlocks, OptionalElseInstructions);

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
        Contract.RequireNotNull(source, out Expression Source);

        Document Documentation = CreateEmptyDocumentation();
        KeywordAssignmentInstruction Result = new(Documentation, destination, Source);

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
        Contract.RequireNotNull(overList, out Expression OverList);
        Contract.RequireNotNull(nameList, out List<Name> NameList);

        if (NameList.Count == 0)
            throw new ArgumentException($"{nameof(nameList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Name> IndexerBlocks = BlockListHelper<Name>.CreateBlockListFromNodeList(NameList);
        Scope LoopInstructions = CreateEmptyScope();
        IOptionalReference<Identifier> ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        IBlockList<Assertion> InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        OverLoopInstruction Result = new(Documentation, OverList, IndexerBlocks, IterationType.Single, LoopInstructions, ExitEntityName, InvariantBlocks);

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
        Contract.RequireNotNull(overList, out Expression OverList);
        Contract.RequireNotNull(nameList, out List<Name> NameList);
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        if (NameList.Count == 0)
            throw new ArgumentException($"{nameof(nameList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Name> IndexerBlocks = BlockListHelper<Name>.CreateBlockListFromNodeList(NameList);
        Scope LoopInstructions = CreateSimpleScope(Instruction);
        IOptionalReference<Identifier> ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        IBlockList<Assertion> InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        OverLoopInstruction Result = new(Documentation, OverList, IndexerBlocks, IterationType.Single, LoopInstructions, ExitEntityName, InvariantBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="OverLoopInstruction"/> with provided values.
    /// </summary>
    /// <param name="overList">The list of collections.</param>
    /// <param name="indexerBlocks">The list of indexers.</param>
    /// <param name="iterationType">The iteration type.</param>
    /// <param name="loopInstructions">List of instructions in the loop.</param>
    /// <param name="invariantBlocks">The list of invariants.</param>
    /// <returns>The created instance.</returns>
    public static OverLoopInstruction CreateOverLoopInstruction(Expression overList, IBlockList<Name> indexerBlocks, IterationType iterationType, Scope loopInstructions, IBlockList<Assertion> invariantBlocks)
    {
        Contract.RequireNotNull(overList, out Expression OverList);
        Contract.RequireNotNull(indexerBlocks, out IBlockList<Name> IndexerBlocks);
        Contract.RequireNotNull(loopInstructions, out Scope LoopInstructions);
        Contract.RequireNotNull(invariantBlocks, out IBlockList<Assertion> InvariantBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)IndexerBlocks))
            throw new ArgumentException($"{nameof(indexerBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Identifier> ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        OverLoopInstruction Result = new(Documentation, OverList, IndexerBlocks, iterationType, LoopInstructions, ExitEntityName, InvariantBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="OverLoopInstruction"/> with provided values.
    /// </summary>
    /// <param name="overList">The list of collections.</param>
    /// <param name="indexerBlocks">The list of indexers.</param>
    /// <param name="iterationType">The iteration type.</param>
    /// <param name="loopInstructions">List of instructions in the loop.</param>
    /// <param name="exitEntityName">The exit entoty name.</param>
    /// <param name="invariantBlocks">The list of invariants.</param>
    /// <returns>The created instance.</returns>
    public static OverLoopInstruction CreateOverLoopInstruction(Expression overList, IBlockList<Name> indexerBlocks, IterationType iterationType, Scope loopInstructions, Identifier exitEntityName, IBlockList<Assertion> invariantBlocks)
    {
        Contract.RequireNotNull(overList, out Expression OverList);
        Contract.RequireNotNull(indexerBlocks, out IBlockList<Name> IndexerBlocks);
        Contract.RequireNotNull(loopInstructions, out Scope LoopInstructions);
        Contract.RequireNotNull(exitEntityName, out Identifier ExitEntityName);
        Contract.RequireNotNull(invariantBlocks, out IBlockList<Assertion> InvariantBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)IndexerBlocks))
            throw new ArgumentException($"{nameof(indexerBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<Identifier> OptionalExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(ExitEntityName);
        OptionalExitEntityName.Assign();
        OverLoopInstruction Result = new(Documentation, OverList, IndexerBlocks, iterationType, LoopInstructions, OptionalExitEntityName, InvariantBlocks);

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
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);
        Contract.RequireNotNull(source, out Expression Source);

        if (ArgumentList.Count == 0)
            throw new ArgumentException($"{nameof(argumentList)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        PrecursorIndexAssignmentInstruction Result = new(Documentation, AncestorType, ArgumentBlocks, Source);

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
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
        Contract.RequireNotNull(source, out Expression Source);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ArgumentBlocks))
            throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        PrecursorIndexAssignmentInstruction Result = new(Documentation, AncestorType, ArgumentBlocks, Source);

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
        Contract.RequireNotNull(ancestorType, out ObjectType AncestorType);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
        Contract.RequireNotNull(source, out Expression Source);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ArgumentBlocks))
            throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> OptionalAncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
        OptionalAncestorType.Assign();
        PrecursorIndexAssignmentInstruction Result = new(Documentation, OptionalAncestorType, ArgumentBlocks, Source);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PrecursorInstruction"/> with provided values.
    /// </summary>
    /// <param name="argumentList">The list of arguments.</param>
    /// <returns>The created instance.</returns>
    public static PrecursorInstruction CreatePrecursorInstruction(List<Argument> argumentList)
    {
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        PrecursorInstruction Result = new(Documentation, AncestorType, ArgumentBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="PrecursorInstruction"/> with provided values.
    /// </summary>
    /// <param name="argumentBlocks">The list of arguments.</param>
    /// <returns>The created instance.</returns>
    public static PrecursorInstruction CreatePrecursorInstruction(IBlockList<Argument> argumentBlocks)
    {
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        PrecursorInstruction Result = new(Documentation, AncestorType, ArgumentBlocks);

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
        Contract.RequireNotNull(ancestorType, out ObjectType AncestorType);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

        Document Documentation = CreateEmptyDocumentation();
        IOptionalReference<ObjectType> OptionalAncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
        OptionalAncestorType.Assign();
        PrecursorInstruction Result = new(Documentation, OptionalAncestorType, ArgumentBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="RaiseEventInstruction"/> with provided values.
    /// </summary>
    /// <param name="queryIdentifier">The query identifier.</param>
    /// <returns>The created instance.</returns>
    public static RaiseEventInstruction CreateRaiseEventInstruction(Identifier queryIdentifier)
    {
        Contract.RequireNotNull(queryIdentifier, out Identifier QueryIdentifier);

        Document Documentation = CreateEmptyDocumentation();
        RaiseEventInstruction Result = new(Documentation, QueryIdentifier, EventType.Single);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ReleaseInstruction"/> with provided values.
    /// </summary>
    /// <param name="entityName">The released entity name.</param>
    /// <returns>The created instance.</returns>
    public static ReleaseInstruction CreateReleaseInstruction(QualifiedName entityName)
    {
        Contract.RequireNotNull(entityName, out QualifiedName EntityName);

        Document Documentation = CreateEmptyDocumentation();
        ReleaseInstruction Result = new(Documentation, EntityName);

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
        Contract.RequireNotNull(exceptionType, out ObjectType ExceptionType);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);

        Document Documentation = CreateEmptyDocumentation();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        ThrowInstruction Result = new(Documentation, ExceptionType, CreationRoutineIdentifier, ArgumentBlocks);

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
        Contract.RequireNotNull(exceptionType, out ObjectType ExceptionType);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

        Document Documentation = CreateEmptyDocumentation();
        ThrowInstruction Result = new(Documentation, ExceptionType, CreationRoutineIdentifier, ArgumentBlocks);

        return Result;
    }
}
