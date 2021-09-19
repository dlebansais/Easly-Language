﻿#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedCommandInstruction(CommandInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Command, out QualifiedName NewCommand, out List<Argument> ArgumentList))
            {
                CommandInstruction NewCommandInstruction = CreateCommandInstruction(NewCommand, ArgumentList);
                complexifiedInstructionList = new List<Instruction>() { NewCommandInstruction };
            }
            else if (ComplexifyQualifiedName(node.Command, out QualifiedName ComplexifiedCommand))
            {
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                CommandInstruction NewCommandInstruction = CreateCommandInstruction(ComplexifiedCommand, ClonedArgumentBlocks);
                complexifiedInstructionList = new List<Instruction>() { NewCommandInstruction };
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                QualifiedName ClonedCommand = (QualifiedName)DeepCloneNode(node.Command, cloneCommentGuid: false);
                CommandInstruction NewCommandInstruction = CreateCommandInstruction(ClonedCommand, ComplexifiedArgumentBlocks);
                complexifiedInstructionList = new List<Instruction>() { NewCommandInstruction };
            }
            else
                return GetComplexifiedCommandInstructionSingle1(node, out complexifiedInstructionList);

            return true;
        }

        private static bool GetComplexifiedCommandInstructionSingle1(CommandInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (ComplexifyAsAsLongAsInstruction(node, out AsLongAsInstruction ComplexifiedAsLongAsInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedAsLongAsInstruction };
            else if (ComplexifyAsAssignmentInstruction(node, out AssignmentInstruction ComplexifiedAssignmentInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedAssignmentInstruction };
            else if (ComplexifyAsAttachmentInstruction(node, out AttachmentInstruction ComplexifiedAttachmentInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedAttachmentInstruction };
            else if (ComplexifyAsCheckInstruction(node, out CheckInstruction ComplexifiedCheckInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedCheckInstruction };
            else if (ComplexifyAsCreateInstruction(node, out CreateInstruction ComplexifiedCreateInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedCreateInstruction };
            else if (ComplexifyAsDebugInstruction(node, out DebugInstruction ComplexifieDebugInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifieDebugInstruction };
            else if (ComplexifyAsForLoopInstruction(node, out ForLoopInstruction ComplexifiedForLoopInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedForLoopInstruction };
            else
                return GetComplexifiedCommandInstructionSingle2(node, out complexifiedInstructionList);

            return true;
        }

        private static bool GetComplexifiedCommandInstructionSingle2(CommandInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (ComplexifyAsIfThenElseInstruction(node, out IfThenElseInstruction ComplexifiedIfThenElseInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedIfThenElseInstruction };
            else if (ComplexifyAsIndexAssignmentInstruction(node, out IndexAssignmentInstruction ComplexifiedIndexAssignmentInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedIndexAssignmentInstruction };
            else if (ComplexifyAsInspectInstruction(node, out InspectInstruction ComplexifiedInspectInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedInspectInstruction };
            else if (ComplexifyAsOverLoopInstruction(node, out OverLoopInstruction ComplexifiedOverLoopInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedOverLoopInstruction };
            else if (ComplexifyAsPrecursorIndexAssignmentInstruction(node, out PrecursorIndexAssignmentInstruction ComplexifiedPrecursorIndexAssignmentInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedPrecursorIndexAssignmentInstruction };
            else if (ComplexifyAsPrecursorInstruction(node, out PrecursorInstruction ComplexifiedPrecursorInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedPrecursorInstruction };
            else if (ComplexifyAsRaiseEventInstruction(node, out RaiseEventInstruction ComplexifiedRaiseEventInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedRaiseEventInstruction };
            else if (ComplexifyAsReleaseInstruction(node, out ReleaseInstruction ComplexifiedReleaseInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedReleaseInstruction };
            else if (ComplexifyAsThrowInstruction(node, out ThrowInstruction ComplexifiedThrowInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedThrowInstruction };

            return complexifiedInstructionList != null;
        }

        private static bool ComplexifyAsAsLongAsInstruction(CommandInstruction node, out AsLongAsInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "as long as ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, AfterText, out Expression Source);
                Continuation FirstContinuation = CreateEmptyContinuation();
                complexifiedNode = CreateAsLongAsInstruction(Source, FirstContinuation);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsAssignmentInstruction(CommandInstruction node, out AssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            int BreakIndex = -1;
            int BreakTextIndex = -1;

            for (int i = 0; i < node.Command.Path.Count; i++)
            {
                BreakTextIndex = node.Command.Path[i].Text.IndexOf(":=", StringComparison.InvariantCulture);
                if (BreakTextIndex >= 0)
                {
                    BreakIndex = i;
                    break;
                }
            }

            if (BreakIndex >= 0)
            {
                string BeforeText = node.Command.Path[BreakIndex].Text.Substring(0, BreakTextIndex).Trim();

                List<Identifier> TargetIdentifierList = new List<Identifier>();
                for (int i = 0; i < BreakIndex; i++)
                    TargetIdentifierList.Add(CreateSimpleIdentifier(node.Command.Path[i].Text));
                TargetIdentifierList.Add(CreateSimpleIdentifier(BeforeText));

                string AfterText = node.Command.Path[BreakIndex].Text.Substring(BreakTextIndex + 2).Trim();

                List<Identifier> SourceIdentifierList = new List<Identifier>();
                SourceIdentifierList.Add(CreateSimpleIdentifier(AfterText));
                for (int i = BreakIndex + 1; i < node.Command.Path.Count; i++)
                    SourceIdentifierList.Add(CreateSimpleIdentifier(node.Command.Path[i].Text));

                QualifiedName AssignmentTarget = CreateQualifiedName(TargetIdentifierList);

                CommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as CommandInstruction;
                QualifiedName AssignmentSource = CreateQualifiedName(SourceIdentifierList);
                Expression Source = CreateQueryExpression(AssignmentSource, ClonedCommand.ArgumentBlocks);

                complexifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsAttachmentInstruction(CommandInstruction node, out AttachmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "attach", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                string ExpressionText;
                string NameText;

                int ToIndex = AfterText.LastIndexOf("to", StringComparison.InvariantCulture);
                if (ToIndex >= 1)
                {
                    ExpressionText = AfterText.Substring(0, ToIndex).Trim();
                    NameText = AfterText.Substring(ToIndex + 2).Trim();
                }
                else
                {
                    ExpressionText = AfterText.Trim();
                    NameText = string.Empty;
                }

                Expression Source = CreateSimpleQueryExpression(ExpressionText);
                Name Name = CreateSimpleName(NameText);

                complexifiedNode = CreateAttachmentInstruction(Source, new List<Name>() { Name });
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsCheckInstruction(CommandInstruction node, out CheckInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "check ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, AfterText, out Expression Source);
                complexifiedNode = CreateCheckInstruction(Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsCreateInstruction(CommandInstruction node, out CreateInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "create ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                Identifier EntityIdentifier = CreateEmptyIdentifier();
                Identifier CreationRoutineIdentifier = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateCreateInstruction(EntityIdentifier, CreationRoutineIdentifier, new List<Argument>());
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsDebugInstruction(CommandInstruction node, out DebugInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "debug ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out CommandInstruction ClonedCommand);
                complexifiedNode = CreateSimpleDebugInstruction(ClonedCommand);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsForLoopInstruction(CommandInstruction node, out ForLoopInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "for ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out CommandInstruction ClonedCommand);
                complexifiedNode = CreateForLoopInstruction(ClonedCommand);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIfThenElseInstruction(CommandInstruction node, out IfThenElseInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "if ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out CommandInstruction ClonedCommand);
                Conditional FirstConditional = CreateConditional(CreateEmptyQueryExpression(), ClonedCommand);
                complexifiedNode = CreateIfThenElseInstruction(FirstConditional);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexAssignmentInstruction(CommandInstruction node, out IndexAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Command.Path.Count > 1 && node.Command.Path[node.Command.Path.Count - 1].Text == "[]:=")
            {
                QualifiedName ClonedDestination = DeepCloneNode(node.Command, cloneCommentGuid: false) as QualifiedName;
                ClonedDestination.Path.RemoveAt(ClonedDestination.Path.Count - 1);

                IBlockList<Argument> ClonedArgumentBlocks;
                if (node.ArgumentBlocks.NodeBlockList.Count > 0)
                    ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);
                else
                    ClonedArgumentBlocks = BlockListHelper<Argument>.CreateSimpleBlockList(CreatePositionalArgument(CreateSimpleManifestNumberExpression("0")));

                complexifiedNode = CreateIndexAssignmentInstruction(ClonedDestination, ClonedArgumentBlocks, CreateEmptyQueryExpression());
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsInspectInstruction(CommandInstruction node, out InspectInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "inspect ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out CommandInstruction ClonedCommand);
                Expression FirstExpression = CreateDefaultManifestNumberExpression();
                With FirstWith = CreateWith(FirstExpression, ClonedCommand);

                Expression Source = CreateEmptyQueryExpression();
                complexifiedNode = CreateInspectInstruction(Source, FirstWith);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsOverLoopInstruction(CommandInstruction node, out OverLoopInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "over ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out CommandInstruction ClonedCommand);

                Expression OverList = CreateDefaultManifestNumberExpression();
                complexifiedNode = CreateOverLoopInstruction(OverList, new List<Name>() { CreateEmptyName() }, ClonedCommand);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorIndexAssignmentInstruction(CommandInstruction node, out PrecursorIndexAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Command.Path.Count == 1 && node.Command.Path[0].Text == "precursor[]:=" && node.ArgumentBlocks.NodeBlockList.Count > 0)
            {
                IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);
                complexifiedNode = CreatePrecursorIndexAssignmentInstruction(ClonedArgumentBlocks, CreateEmptyQueryExpression());
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorInstruction(CommandInstruction node, out PrecursorInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Command.Path.Count == 1 && node.Command.Path[0].Text == "precursor")
            {
                IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);
                complexifiedNode = CreatePrecursorInstruction(ClonedArgumentBlocks);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsRaiseEventInstruction(CommandInstruction node, out RaiseEventInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "raise ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                Identifier QueryIdentifier = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateRaiseEventInstruction(QueryIdentifier);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsReleaseInstruction(CommandInstruction node, out ReleaseInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "release ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                QualifiedName entityName = CreateSimpleQualifiedName(AfterText);
                complexifiedNode = CreateReleaseInstruction(entityName);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsThrowInstruction(CommandInstruction node, out ThrowInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "throw ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                ObjectType ExceptionType = CreateEmptySimpleType();
                Identifier CreationRoutineIdentifier = CreateSimpleIdentifier(AfterText);
                IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);

                complexifiedNode = CreateThrowInstruction(ExceptionType, CreationRoutineIdentifier, ClonedArgumentBlocks);
            }

            return complexifiedNode != null;
        }
    }
}
