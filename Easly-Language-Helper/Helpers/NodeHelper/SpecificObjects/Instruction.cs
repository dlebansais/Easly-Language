namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;

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

        AsLongAsInstruction Result = new AsLongAsInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ContinueCondition = ContinueCondition;
        Result.ContinuationBlocks = BlockListHelper<Continuation>.CreateSimpleBlockList(Continuation);
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
        Contract.RequireNotNull(continueCondition, out Expression ContinueCondition);
        Contract.RequireNotNull(continuationBlocks, out IBlockList<Continuation> ContinuationBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ContinuationBlocks))
            throw new ArgumentException($"{nameof(continuationBlocks)} must not be empty");

        AsLongAsInstruction Result = new AsLongAsInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ContinueCondition = ContinueCondition;
        Result.ContinuationBlocks = ContinuationBlocks;
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
        Contract.RequireNotNull(continueCondition, out Expression ContinueCondition);
        Contract.RequireNotNull(continuationBlocks, out IBlockList<Continuation> ContinuationBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ContinuationBlocks))
            throw new ArgumentException($"{nameof(continuationBlocks)} must not be empty");

        AsLongAsInstruction Result = new AsLongAsInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ContinueCondition = ContinueCondition;
        Result.ContinuationBlocks = ContinuationBlocks;
        Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
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
        Contract.RequireNotNull(assignmentList, out List<QualifiedName> AssignmentList);
        Contract.RequireNotNull(source, out Expression Source);

        if (AssignmentList.Count == 0)
            throw new ArgumentException($"{nameof(assignmentList)} must not be empty");

        AssignmentInstruction Result = new AssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.DestinationBlocks = BlockListHelper<QualifiedName>.CreateBlockListFromNodeList(AssignmentList);
        Result.Source = Source;

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

        AssignmentInstruction Result = new AssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.DestinationBlocks = AssignmentBlocks;
        Result.Source = Source;

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

        ObjectType AttachType = CreateDefaultObjectType();
        Attachment FirstAttachment = CreateAttachment(AttachType);

        AttachmentInstruction Result = new AttachmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
        Result.EntityNameBlocks = BlockListHelper<Name>.CreateBlockListFromNodeList(NameList);
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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(entityNameBlocks, out IBlockList<Name> EntityNameBlocks);
        Contract.RequireNotNull(attachmentBlocks, out IBlockList<Attachment> AttachmentBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)EntityNameBlocks))
            throw new ArgumentException($"{nameof(entityNameBlocks)} must not be empty");

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AttachmentBlocks))
            throw new ArgumentException($"{nameof(attachmentBlocks)} must not be empty");

        AttachmentInstruction Result = new AttachmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
        Result.EntityNameBlocks = EntityNameBlocks;
        Result.AttachmentBlocks = AttachmentBlocks;
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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(entityNameBlocks, out IBlockList<Name> EntityNameBlocks);
        Contract.RequireNotNull(attachmentBlocks, out IBlockList<Attachment> AttachmentBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)EntityNameBlocks))
            throw new ArgumentException($"{nameof(entityNameBlocks)} must not be empty");

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AttachmentBlocks))
            throw new ArgumentException($"{nameof(attachmentBlocks)} must not be empty");

        AttachmentInstruction Result = new AttachmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
        Result.EntityNameBlocks = EntityNameBlocks;
        Result.AttachmentBlocks = AttachmentBlocks;
        Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
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
        Contract.RequireNotNull(booleanExpression, out Expression BooleanExpression);

        CheckInstruction Result = new CheckInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.BooleanExpression = BooleanExpression;

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

        CommandInstruction Result = new CommandInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Command = Command;
        Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

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

        CommandInstruction Result = new CommandInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Command = Command;
        Result.ArgumentBlocks = ArgumentBlocks;

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

        CreateInstruction Result = new CreateInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityIdentifier = EntityIdentifier;
        Result.CreationRoutineIdentifier = CreationRoutineIdentifier;
        Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
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
        Contract.RequireNotNull(entityIdentifier, out Identifier EntityIdentifier);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);

        CreateInstruction Result = new CreateInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityIdentifier = EntityIdentifier;
        Result.CreationRoutineIdentifier = CreationRoutineIdentifier;
        Result.ArgumentBlocks = ArgumentBlocks;
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
        Contract.RequireNotNull(entityIdentifier, out Identifier EntityIdentifier);
        Contract.RequireNotNull(creationRoutineIdentifier, out Identifier CreationRoutineIdentifier);
        Contract.RequireNotNull(argumentBlocks, out IBlockList<Argument> ArgumentBlocks);
        Contract.RequireNotNull(processor, out QualifiedName Processor);

        CreateInstruction Result = new CreateInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityIdentifier = EntityIdentifier;
        Result.CreationRoutineIdentifier = CreationRoutineIdentifier;
        Result.ArgumentBlocks = ArgumentBlocks;
        Result.Processor = OptionalReferenceHelper<QualifiedName>.CreateReference(Processor);
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
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        DebugInstruction Result = new DebugInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Instructions = CreateSimpleScope(Instruction);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="DebugInstruction"/>.
    /// </summary>
    /// <param name="instructions">The instructions.</param>
    /// <returns>The created instance.</returns>
    public static DebugInstruction CreateDebugInstruction(Scope instructions)
    {
        Contract.RequireNotNull(instructions, out Scope Instructions);

        DebugInstruction Result = new DebugInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Instructions = Instructions;

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ForLoopInstruction"/> with empty content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ForLoopInstruction CreateEmptyForLoopInstruction()
    {
        ForLoopInstruction Result = new ForLoopInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        Result.InitInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Result.WhileCondition = CreateEmptyQueryExpression();
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
    public static ForLoopInstruction CreateSimpleForLoopInstruction(Instruction instruction)
    {
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        ForLoopInstruction Result = new ForLoopInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        Result.InitInstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Result.WhileCondition = CreateEmptyQueryExpression();
        Result.LoopInstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(Instruction);
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
        Contract.RequireNotNull(conditional, out Conditional Conditional);

        IfThenElseInstruction Result = new IfThenElseInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ConditionalBlocks = BlockListHelper<Conditional>.CreateSimpleBlockList(Conditional);
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
        Contract.RequireNotNull(conditionalBlocks, out IBlockList<Conditional> ConditionalBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ConditionalBlocks))
            throw new ArgumentException($"{nameof(conditionalBlocks)} must not be empty");

        IfThenElseInstruction Result = new IfThenElseInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ConditionalBlocks = ConditionalBlocks;
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
        Contract.RequireNotNull(conditionalBlocks, out IBlockList<Conditional> ConditionalBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)ConditionalBlocks))
            throw new ArgumentException($"{nameof(conditionalBlocks)} must not be empty");

        IfThenElseInstruction Result = new IfThenElseInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ConditionalBlocks = conditionalBlocks;
        Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
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
        Contract.RequireNotNull(destination, out QualifiedName Destination);
        Contract.RequireNotNull(argumentList, out List<Argument> ArgumentList);
        Contract.RequireNotNull(source, out Expression Source);

        if (ArgumentList.Count == 0)
            throw new ArgumentException($"{nameof(argumentList)} must not be empty");

        IndexAssignmentInstruction Result = new IndexAssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Destination = Destination;
        Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        Result.Source = Source;

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

        IndexAssignmentInstruction Result = new IndexAssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Destination = Destination;
        Result.ArgumentBlocks = ArgumentBlocks;
        Result.Source = Source;

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

        Expression FirstExpression = CreateDefaultManifestNumberExpression();
        With FirstWith = CreateSimpleWith(FirstExpression);

        InspectInstruction Result = new InspectInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(with, out With With);

        InspectInstruction Result = new InspectInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
        Result.WithBlocks = BlockListHelper<With>.CreateSimpleBlockList(With);
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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(withBlocks, out IBlockList<With> WithBlocks);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)WithBlocks))
            throw new ArgumentException($"{nameof(withBlocks)} must not be empty");

        InspectInstruction Result = new InspectInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
        Result.WithBlocks = WithBlocks;
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
        Contract.RequireNotNull(source, out Expression Source);
        Contract.RequireNotNull(withBlocks, out IBlockList<With> WithBlocks);
        Contract.RequireNotNull(elseInstructions, out Scope ElseInstructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)WithBlocks))
            throw new ArgumentException($"{nameof(withBlocks)} must not be empty");

        InspectInstruction Result = new InspectInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Source = Source;
        Result.WithBlocks = WithBlocks;
        Result.ElseInstructions = OptionalReferenceHelper<Scope>.CreateReference(ElseInstructions);
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
        Contract.RequireNotNull(source, out Expression Source);

        KeywordAssignmentInstruction Result = new KeywordAssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.Destination = destination;
        Result.Source = Source;

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

        OverLoopInstruction Result = new OverLoopInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.OverList = OverList;
        Result.IndexerBlocks = BlockListHelper<Name>.CreateBlockListFromNodeList(NameList);
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
        Contract.RequireNotNull(overList, out Expression OverList);
        Contract.RequireNotNull(nameList, out List<Name> NameList);
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        if (NameList.Count == 0)
            throw new ArgumentException($"{nameof(nameList)} must not be empty");

        OverLoopInstruction Result = new OverLoopInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.OverList = OverList;
        Result.IndexerBlocks = BlockListHelper<Name>.CreateBlockListFromNodeList(NameList);
        Result.Iteration = IterationType.Single;
        Result.LoopInstructions = CreateSimpleScope(Instruction);
        Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        Result.InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();

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

        OverLoopInstruction Result = new OverLoopInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.OverList = OverList;
        Result.IndexerBlocks = IndexerBlocks;
        Result.Iteration = iterationType;
        Result.LoopInstructions = LoopInstructions;
        Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        Result.InvariantBlocks = InvariantBlocks;

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

        OverLoopInstruction Result = new OverLoopInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.OverList = OverList;
        Result.IndexerBlocks = IndexerBlocks;
        Result.Iteration = iterationType;
        Result.LoopInstructions = LoopInstructions;
        Result.ExitEntityName = OptionalReferenceHelper<Identifier>.CreateReference(ExitEntityName);
        Result.ExitEntityName.Assign();
        Result.InvariantBlocks = InvariantBlocks;

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

        PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);
        Result.Source = Source;

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

        PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        Result.ArgumentBlocks = ArgumentBlocks;
        Result.Source = Source;

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

        PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
        Result.AncestorType.Assign();
        Result.ArgumentBlocks = ArgumentBlocks;
        Result.Source = Source;

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

        PrecursorInstruction Result = new PrecursorInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

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

        PrecursorInstruction Result = new PrecursorInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        Result.ArgumentBlocks = ArgumentBlocks;

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

        PrecursorInstruction Result = new PrecursorInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(AncestorType);
        Result.AncestorType.Assign();
        Result.ArgumentBlocks = ArgumentBlocks;

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

        RaiseEventInstruction Result = new RaiseEventInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.QueryIdentifier = QueryIdentifier;
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
        Contract.RequireNotNull(entityName, out QualifiedName EntityName);

        ReleaseInstruction Result = new ReleaseInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.EntityName = EntityName;

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

        ThrowInstruction Result = new ThrowInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExceptionType = ExceptionType;
        Result.CreationRoutine = CreationRoutineIdentifier;
        Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockListFromNodeList(ArgumentList);

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

        ThrowInstruction Result = new ThrowInstruction();
        Result.Documentation = CreateEmptyDocumentation();
        Result.ExceptionType = ExceptionType;
        Result.CreationRoutine = CreationRoutineIdentifier;
        Result.ArgumentBlocks = ArgumentBlocks;

        return Result;
    }
}
