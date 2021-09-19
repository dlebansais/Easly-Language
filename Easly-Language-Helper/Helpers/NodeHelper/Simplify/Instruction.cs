#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
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
                    return GetSimplifiedInstructionSingle(nodeInstruction, out simplifiedNode);
            }
        }

        public static bool GetSimplifiedInstructionSingle(Instruction nodeInstruction, out Node simplifiedNode)
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
                case ThrowInstruction AsThrowInstruction:
                    return SimplifyThrowInstruction(AsThrowInstruction, out simplifiedNode);
                default:
                    simplifiedNode = null;
                    return false;
            }
        }

        private static bool SimplifyCommandInstruction(CommandInstruction node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            CommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as CommandInstruction;
            if (ClonedCommand.ArgumentBlocks.NodeBlockList.Count > 0)
                simplifiedNode = CreateCommandInstruction(ClonedCommand.Command, new List<Argument>());

            return simplifiedNode != null;
        }

        private static bool SimplifyAsLongAsInstruction(AsLongAsInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.ContinueCondition, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyAssignmentInstruction(AssignmentInstruction node, out Node simplifiedNode)
        {
            if (BlockListHelper<QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                AssignmentInstruction ClonedInstruction = DeepCloneNode(node, cloneCommentGuid: false) as AssignmentInstruction;
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
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCheckInstruction(CheckInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.BooleanExpression, cloneCommentGuid: false) as Expression;

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
                simplifiedNode = DeepCloneNode(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false) as Instruction;
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyForLoopInstruction(ForLoopInstruction node, out Node simplifiedNode)
        {
            Instruction SelectedInstruction = null;

            if (node.InitInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.InitInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                SelectedInstruction = node.InitInstructionBlocks.NodeBlockList[0].NodeList[0];
            }
            else if (node.LoopInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.LoopInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                SelectedInstruction = node.LoopInstructionBlocks.NodeBlockList[0].NodeList[0];
            }
            else if (node.IterationInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.IterationInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                SelectedInstruction = node.IterationInstructionBlocks.NodeBlockList[0].NodeList[0];
            }

            if (SelectedInstruction != null)
                simplifiedNode = DeepCloneNode(SelectedInstruction, cloneCommentGuid: false) as Instruction;
            else
            {
                QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                Expression Source = DeepCloneNode(node.WhileCondition, cloneCommentGuid: false) as Expression;

                simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIfThenElseInstruction(IfThenElseInstruction node, out Node simplifiedNode)
        {
            Debug.Assert(node.ConditionalBlocks.NodeBlockList.Count > 0 && node.ConditionalBlocks.NodeBlockList[0].NodeList.Count > 0, "There is always at least one conditional");
            Conditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];

            if (FirstConditional.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                simplifiedNode = DeepCloneNode(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false) as Instruction;
            }
            else
            {
                QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                Expression Source = DeepCloneNode(FirstConditional.BooleanExpression, cloneCommentGuid: false) as Expression;

                simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIndexAssignmentInstruction(IndexAssignmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = DeepCloneNode(node.Destination, cloneCommentGuid: false) as QualifiedName;
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyInspectInstruction(InspectInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyKeywordAssignmentInstruction(KeywordAssignmentInstruction node, out Node simplifiedNode)
        {
            Identifier KeywordIdentifier = CreateSimpleIdentifier(node.Destination.ToString());

            List<Identifier> IdentifierList = new List<Identifier>();
            IdentifierList.Add(KeywordIdentifier);

            List<Argument> ArgumentList = new List<Argument>();
            IBlockList<Argument> ArgumentBlocks;

            if (node.Source is QueryExpression AsQueryExpression)
            {
                QueryExpression ClonedSource = DeepCloneNode(AsQueryExpression, cloneCommentGuid: false) as QueryExpression;

                IdentifierList.AddRange(ClonedSource.Query.Path);
                ArgumentBlocks = ClonedSource.ArgumentBlocks;
            }
            else
            {
                Expression ClonedSource = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;
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
            Expression Source = DeepCloneNode(node.OverList, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorIndexAssignmentInstruction(PrecursorIndexAssignmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

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
            QualifiedName Command = DeepCloneNode(node.EntityName, cloneCommentGuid: false) as QualifiedName;
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
