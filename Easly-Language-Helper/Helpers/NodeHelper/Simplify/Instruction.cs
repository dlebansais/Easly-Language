namespace BaseNodeHelper
{
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
        /// Creates an instance of a simplified version of an instruction.
        /// </summary>
        /// <param name="nodeInstruction">The instruction to simplify.</param>
        /// <param name="simplifiedNode">The simplified instruction.</param>
        /// <returns>True if the instruction could be simplified; otherwise, false.</returns>
        public static bool GetSimplifiedInstruction(Instruction nodeInstruction, out Node simplifiedNode)
        {
            switch (nodeInstruction)
            {
                case CommandInstruction AsCommandInstruction:
                    return SimplifyCommandInstruction(AsCommandInstruction, out simplifiedNode);
                case AsLongAsInstruction AsAsLongAsInstruction:
                    return SimplifyAsLongAsInstruction(AsAsLongAsInstruction, out simplifiedNode);
                case AssignmentInstruction AsAssignmentInstruction:
                    return SimplifyAssignmentInstruction(AsAssignmentInstruction, out simplifiedNode);
                case AttachmentInstruction AsAttachmentInstruction:
                    return SimplifyAttachmentInstruction(AsAttachmentInstruction, out simplifiedNode);
                case CreateInstruction AsCreateInstruction:
                    return SimplifyCreateInstruction(AsCreateInstruction, out simplifiedNode);
                case ForLoopInstruction AsForLoopInstruction:
                    return SimplifyForLoopInstruction(AsForLoopInstruction, out simplifiedNode);
                case IfThenElseInstruction AsIfThenElseInstruction:
                    return SimplifyIfThenElseInstruction(AsIfThenElseInstruction, out simplifiedNode);
                case IndexAssignmentInstruction AsIndexAssignmentInstruction:
                    return SimplifyIndexAssignmentInstruction(AsIndexAssignmentInstruction, out simplifiedNode);
                case InspectInstruction AsInspectInstruction:
                    return SimplifyInspectInstruction(AsInspectInstruction, out simplifiedNode);
                case KeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    return SimplifyKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, out simplifiedNode);
                case OverLoopInstruction AsOverLoopInstruction:
                    return SimplifyOverLoopInstruction(AsOverLoopInstruction, out simplifiedNode);
                case PrecursorIndexAssignmentInstruction AsPrecursorIndexAssignmentInstruction:
                    return SimplifyPrecursorIndexAssignmentInstruction(AsPrecursorIndexAssignmentInstruction, out simplifiedNode);
                default:
                    return GetSimplifiedInstructionSingle1(nodeInstruction, out simplifiedNode);
            }
        }

        private static bool GetSimplifiedInstructionSingle1(Instruction nodeInstruction, out Node simplifiedNode)
        {
            switch (nodeInstruction)
            {
                case CheckInstruction AsCheckInstruction:
                    return SimplifyCheckInstruction(AsCheckInstruction, out simplifiedNode);
                case DebugInstruction AsDebugInstruction:
                    return SimplifyDebugInstruction(AsDebugInstruction, out simplifiedNode);
                case PrecursorInstruction AsPrecursorInstruction:
                    return SimplifyPrecursorInstruction(AsPrecursorInstruction, out simplifiedNode);
                case RaiseEventInstruction AsRaiseEventInstruction:
                    return SimplifyRaiseEventInstruction(AsRaiseEventInstruction, out simplifiedNode);
                case ReleaseInstruction AsReleaseInstruction:
                    return SimplifyReleaseInstruction(AsReleaseInstruction, out simplifiedNode);
                default:
                    return GetSimplifiedInstructionSingle2(nodeInstruction, out simplifiedNode);
            }
        }

        private static bool GetSimplifiedInstructionSingle2(Instruction nodeInstruction, out Node simplifiedNode)
        {
            Contract.Unused(out simplifiedNode);

            bool Result = false;
            bool IsHandled = false;

            switch (nodeInstruction)
            {
                case ThrowInstruction AsThrowInstruction:
                    Result = SimplifyThrowInstruction(AsThrowInstruction, out simplifiedNode);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(Instruction)} have been handled");

            return Result;
        }

        private static bool SimplifyCommandInstruction(CommandInstruction node, out Node simplifiedNode)
        {
            CommandInstruction ClonedCommand = (CommandInstruction)DeepCloneNode(node, cloneCommentGuid: false);
            if (ClonedCommand.ArgumentBlocks.NodeBlockList.Count > 0)
            {
                simplifiedNode = CreateCommandInstruction(ClonedCommand.Command, new List<Argument>());
                return true;
            }

            Contract.Unused(out simplifiedNode);
            return false;
        }

        private static bool SimplifyAsLongAsInstruction(AsLongAsInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = (Expression)DeepCloneNode(node.ContinueCondition, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyAssignmentInstruction(AssignmentInstruction node, out Node simplifiedNode)
        {
            if (BlockListHelper<QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                AssignmentInstruction ClonedInstruction = (AssignmentInstruction)DeepCloneNode(node, cloneCommentGuid: false);
                QualifiedName Target = ClonedInstruction.DestinationBlocks.NodeBlockList[0].NodeList[0];

                if (ClonedInstruction.Source is QueryExpression AsQueryExpression)
                {
                    List<Identifier> IdentifierList = new List<Identifier>();
                    for (int i = 0; i + 1 < Target.Path.Count; i++)
                        IdentifierList.Add(Target.Path[i]);

                    Identifier MiddleIdentifier = CreateSimpleIdentifier(Target.Path[Target.Path.Count - 1].Text + AsQueryExpression.Query.Path[0].Text);
                    IdentifierList.Add(MiddleIdentifier);

                    for (int i = 1; i < AsQueryExpression.Query.Path.Count; i++)
                        IdentifierList.Add(AsQueryExpression.Query.Path[i]);

                    QualifiedName Command = CreateQualifiedName(IdentifierList);
                    simplifiedNode = CreateCommandInstruction(Command, AsQueryExpression.ArgumentBlocks);
                }
                else
                {
                    Argument FirstArgument = CreatePositionalArgument(ClonedInstruction.Source);
                    simplifiedNode = CreateCommandInstruction(Target, new List<Argument>() { FirstArgument });
                }
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyAttachmentInstruction(AttachmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCheckInstruction(CheckInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = (Expression)DeepCloneNode(node.BooleanExpression, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCreateInstruction(CreateInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = CreateSimpleQualifiedName(node.CreationRoutineIdentifier.Text);

            IBlockList<Argument> ArgumentCopy = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);
            simplifiedNode = CreateCommandInstruction(Command, ArgumentCopy);

            return true;
        }

        private static bool SimplifyDebugInstruction(DebugInstruction node, out Node simplifiedNode)
        {
            if (node.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                simplifiedNode = (Instruction)DeepCloneNode(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false);
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyForLoopInstruction(ForLoopInstruction node, out Node simplifiedNode)
        {
            if (node.InitInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.InitInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                Instruction SelectedInstruction = node.InitInstructionBlocks.NodeBlockList[0].NodeList[0];
                simplifiedNode = (Instruction)DeepCloneNode(SelectedInstruction, cloneCommentGuid: false);
            }
            else if (node.LoopInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.LoopInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                Instruction SelectedInstruction = node.LoopInstructionBlocks.NodeBlockList[0].NodeList[0];
                simplifiedNode = (Instruction)DeepCloneNode(SelectedInstruction, cloneCommentGuid: false);
            }
            else if (node.IterationInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.IterationInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                Instruction SelectedInstruction = node.IterationInstructionBlocks.NodeBlockList[0].NodeList[0];
                simplifiedNode = (Instruction)DeepCloneNode(SelectedInstruction, cloneCommentGuid: false);
            }
            else
            {
                QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                Expression Source = (Expression)DeepCloneNode(node.WhileCondition, cloneCommentGuid: false);

                simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIfThenElseInstruction(IfThenElseInstruction node, out Node simplifiedNode)
        {
            Debug.Assert(node.ConditionalBlocks.NodeBlockList.Count > 0, "There is always at least one conditional");
            Debug.Assert(node.ConditionalBlocks.NodeBlockList[0].NodeList.Count > 0, "There is always at least one conditional");

            Conditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];

            if (FirstConditional.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                simplifiedNode = (Instruction)DeepCloneNode(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false);
            }
            else
            {
                QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                Expression Source = (Expression)DeepCloneNode(FirstConditional.BooleanExpression, cloneCommentGuid: false);

                simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIndexAssignmentInstruction(IndexAssignmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = (QualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
            Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyInspectInstruction(InspectInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyKeywordAssignmentInstruction(KeywordAssignmentInstruction node, out Node simplifiedNode)
        {
            Identifier KeywordIdentifier = CreateSimpleIdentifier(node.Destination.ToString());

            List<Identifier> IdentifierList = new List<Identifier>();
            IdentifierList.Add(KeywordIdentifier);

            IBlockList<Argument> ArgumentBlocks;

            if (node.Source is QueryExpression AsQueryExpression)
            {
                QueryExpression ClonedSource = (QueryExpression)DeepCloneNode(AsQueryExpression, cloneCommentGuid: false);

                IdentifierList.AddRange(ClonedSource.Query.Path);
                ArgumentBlocks = ClonedSource.ArgumentBlocks;
            }
            else
            {
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                Argument FirstArgument = CreatePositionalArgument(ClonedSource);
                ArgumentBlocks = BlockListHelper<Argument>.CreateSimpleBlockList(FirstArgument);
            }

            QualifiedName Command = CreateQualifiedName(IdentifierList);

            simplifiedNode = CreateCommandInstruction(Command, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyOverLoopInstruction(OverLoopInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = (Expression)DeepCloneNode(node.OverList, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorIndexAssignmentInstruction(PrecursorIndexAssignmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorInstruction(PrecursorInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = CreateSimpleQualifiedName("precursor");
            IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);

            simplifiedNode = CreateCommandInstruction(Command, ClonedArgumentBlocks);

            return true;
        }

        private static bool SimplifyRaiseEventInstruction(RaiseEventInstruction node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleCommandInstruction(node.QueryIdentifier.Text);
            return true;
        }

        private static bool SimplifyReleaseInstruction(ReleaseInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = (QualifiedName)DeepCloneNode(node.EntityName, cloneCommentGuid: false);
            simplifiedNode = CreateCommandInstruction(Command, new List<Argument>());

            return true;
        }

        private static bool SimplifyThrowInstruction(ThrowInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = CreateSimpleQualifiedName("precursor");
            IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);

            simplifiedNode = CreateCommandInstruction(Command, ClonedArgumentBlocks);

            return true;
        }
    }
}
