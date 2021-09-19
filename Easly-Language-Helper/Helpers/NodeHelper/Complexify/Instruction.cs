#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedInstruction(Instruction node, out IList<Instruction> complexifiedInstructionList)
        {
            switch (node)
            {
                case AsLongAsInstruction AsAsLongAsInstruction:
                    return GetComplexifiedAsLongAsInstruction(AsAsLongAsInstruction, out complexifiedInstructionList);

                case AssignmentInstruction AsAssignmentInstruction:
                    return GetComplexifiedAssignmentInstruction(AsAssignmentInstruction, out complexifiedInstructionList);

                case AttachmentInstruction AsAttachmentInstruction:
                    return GetComplexifiedAttachmentInstruction(AsAttachmentInstruction, out complexifiedInstructionList);

                case CommandInstruction AsCommandInstruction:
                    return GetComplexifiedCommandInstruction(AsCommandInstruction, out complexifiedInstructionList);

                case CreateInstruction AsCreateInstruction:
                    return GetComplexifiedCreateInstruction(AsCreateInstruction, out complexifiedInstructionList);

                case IfThenElseInstruction AsIfThenElseInstruction:
                    return GetComplexifiedIfThenElseInstruction(AsIfThenElseInstruction, out complexifiedInstructionList);

                case IndexAssignmentInstruction AsIndexAssignmentInstruction:
                    return GetComplexifiedIndexAssignmentInstruction(AsIndexAssignmentInstruction, out complexifiedInstructionList);

                case InspectInstruction AsInspectInstruction:
                    return GetComplexifiedInspectInstruction(AsInspectInstruction, out complexifiedInstructionList);

                case OverLoopInstruction AsOverLoopInstruction:
                    return GetComplexifiedOverLoopInstruction(AsOverLoopInstruction, out complexifiedInstructionList);

                case PrecursorIndexAssignmentInstruction AsPrecursorIndexAssignmentInstruction:
                    return GetComplexifiedPrecursorIndexAssignmentInstruction(AsPrecursorIndexAssignmentInstruction, out complexifiedInstructionList);

                default:
                    return GetComplexifiedInstructionSingle(node, out complexifiedInstructionList);
            }
        }

        private static bool GetComplexifiedInstructionSingle(Instruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;
            bool IsHandled = false;

            switch (node)
            {
                case CheckInstruction AsCheckInstruction:
                    return GetComplexifiedCheckInstruction(AsCheckInstruction, out complexifiedInstructionList);

                case KeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    return GetComplexifiedKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, out complexifiedInstructionList);

                case PrecursorInstruction AsPrecursorInstruction:
                    return GetComplexifiedPrecursorInstruction(AsPrecursorInstruction, out complexifiedInstructionList);

                case ReleaseInstruction AsReleaseInstruction:
                    return GetComplexifiedReleaseInstruction(AsReleaseInstruction, out complexifiedInstructionList);

                case ThrowInstruction AsThrowInstruction:
                    return GetComplexifiedThrowInstruction(AsThrowInstruction, out complexifiedInstructionList);

                case DebugInstruction AsDebugInstruction:
                case ForLoopInstruction AsForLoopInstruction:
                case RaiseEventInstruction AsRaiseEventInstruction:
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(Instruction)} have been handled");

            return false;
        }

        private static bool GetComplexifiedAsLongAsInstruction(AsLongAsInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.ContinueCondition, out IList<Expression> ComplexifiedContinueConditionList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedContinueCondition in ComplexifiedContinueConditionList)
                {
                    IBlockList<Continuation> ClonedContinuationBlocks = (IBlockList<Continuation>)DeepCloneBlockList((IBlockList)node.ContinuationBlocks, cloneCommentGuid: false);

                    AsLongAsInstruction NewAsLongAsInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        Scope ClonedElseInstructions = (Scope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewAsLongAsInstruction = CreateAsLongAsInstruction(ComplexifiedContinueCondition, ClonedContinuationBlocks, ClonedElseInstructions);
                    }
                    else
                        NewAsLongAsInstruction = CreateAsLongAsInstruction(ComplexifiedContinueCondition, ClonedContinuationBlocks);

                    complexifiedInstructionList.Add(NewAsLongAsInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedAssignmentInstruction(AssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedQualifiedNameBlockList(node.DestinationBlocks, out IBlockList<QualifiedName> ComplexifiedDestinationBlocks))
            {
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                AssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ComplexifiedDestinationBlocks, ClonedSource);
                complexifiedInstructionList = new List<Instruction>() { NewAssignmentInstruction };
            }
            else if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<QualifiedName> ClonedDestinationBlocks = (IBlockList<QualifiedName>)DeepCloneBlockList((IBlockList)node.DestinationBlocks, cloneCommentGuid: false);
                    AssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ClonedDestinationBlocks, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewAssignmentInstruction);
                }
            }
            else if (ComplexifyAsKeywordAssignmentInstruction(node, out KeywordAssignmentInstruction ComplexifiedKeywordAssignmentInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedKeywordAssignmentInstruction };
            else if (ComplexifyAsIndexAssignmentInstruction(node, out IndexAssignmentInstruction ComplexifiedIndexAssignmentInstruction))
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedIndexAssignmentInstruction };

            return complexifiedInstructionList != null;
        }

        private static bool ComplexifyAsKeywordAssignmentInstruction(AssignmentInstruction node, out KeywordAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (BlockListHelper<QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                QualifiedName AssignmentTarget = node.DestinationBlocks.NodeBlockList[0].NodeList[0];
                if (AssignmentTarget.Path.Count == 1)
                {
                    string Text = AssignmentTarget.Path[0].Text;
                    if (Text.Length > 0)
                    {
                        Text = Text.Substring(0, 1).ToUpperInvariant() + Text.Substring(1);

                        Keyword Keyword = Keyword.Current;

                        if (Text == "Result")
                            Keyword = Keyword.Result;
                        else if (Text == "Retry")
                            Keyword = Keyword.Retry;

                        if (Keyword != Keyword.Current)
                        {
                            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;
                            complexifiedNode = CreateKeywordAssignmentInstruction(Keyword, Source);
                        }
                    }
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexAssignmentInstruction(AssignmentInstruction node, out IndexAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (BlockListHelper<QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                QualifiedName AssignmentTarget = node.DestinationBlocks.NodeBlockList[0].NodeList[0];
                if (ComplexifyWithArguments(AssignmentTarget, '[', ']', out QualifiedName NewQuery, out List<Argument> ArgumentList))
                {
                    Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                    complexifiedNode = CreateIndexAssignmentInstruction(NewQuery, ArgumentList, ClonedSource);
                }
            }

            return complexifiedNode != null;
        }

        private static bool GetComplexifiedAttachmentInstruction(AttachmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<Name> ClonedEntityNameBlocks = (IBlockList<Name>)DeepCloneBlockList((IBlockList)node.EntityNameBlocks, cloneCommentGuid: false);
                    IBlockList<Attachment> ClonedAttachmentBlocks = (IBlockList<Attachment>)DeepCloneBlockList((IBlockList)node.AttachmentBlocks, cloneCommentGuid: false);

                    AttachmentInstruction NewAttachmentInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        Scope ClonedElseInstructions = (Scope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewAttachmentInstruction = CreateAttachmentInstruction(ComplexifiedSource, ClonedEntityNameBlocks, ClonedAttachmentBlocks, ClonedElseInstructions);
                    }
                    else
                        NewAttachmentInstruction = CreateAttachmentInstruction(ComplexifiedSource, ClonedEntityNameBlocks, ClonedAttachmentBlocks);

                    complexifiedInstructionList.Add(NewAttachmentInstruction);
                }
            }
            else if (GetComplexifiedNameBlockList(node.EntityNameBlocks, out IBlockList<Name> ComplexifiedEntityNameBlocks))
            {
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IBlockList<Attachment> ClonedAttachmentBlocks = (IBlockList<Attachment>)DeepCloneBlockList((IBlockList)node.AttachmentBlocks, cloneCommentGuid: false);

                AttachmentInstruction NewAttachmentInstruction;

                if (node.ElseInstructions.IsAssigned)
                {
                    Scope ClonedElseInstructions = (Scope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                    NewAttachmentInstruction = CreateAttachmentInstruction(ClonedSource, ComplexifiedEntityNameBlocks, ClonedAttachmentBlocks, ClonedElseInstructions);
                }
                else
                    NewAttachmentInstruction = CreateAttachmentInstruction(ClonedSource, ComplexifiedEntityNameBlocks, ClonedAttachmentBlocks);

                complexifiedInstructionList = new List<Instruction>() { NewAttachmentInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedCheckInstruction(CheckInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.BooleanExpression, out IList<Expression> ComplexifiedBooleanExpressionList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedBooleanExpression in ComplexifiedBooleanExpressionList)
                {
                    CheckInstruction NewCheckInstruction = CreateCheckInstruction(ComplexifiedBooleanExpression);
                    complexifiedInstructionList.Add(NewCheckInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

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

        private static bool GetComplexifiedCreateInstruction(CreateInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                Identifier ClonedEntityIdentifier = (Identifier)DeepCloneNode(node.EntityIdentifier, cloneCommentGuid: false);
                Identifier ClonedCreationRoutineIdentifier = (Identifier)DeepCloneNode(node.CreationRoutineIdentifier, cloneCommentGuid: false);

                CreateInstruction NewCreateInstruction;

                if (node.Processor.IsAssigned)
                {
                    QualifiedName ClonedProcessor = (QualifiedName)DeepCloneNode(node.Processor.Item, cloneCommentGuid: false);
                    NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ComplexifiedArgumentBlocks, ClonedProcessor);
                }
                else
                    NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ComplexifiedArgumentBlocks);

                complexifiedInstructionList = new List<Instruction>() { NewCreateInstruction };
            }
            else if (node.Processor.IsAssigned && ComplexifyQualifiedName(node.Processor.Item, out QualifiedName ComplexifiedPropcessor))
            {
                Identifier ClonedEntityIdentifier = (Identifier)DeepCloneNode(node.EntityIdentifier, cloneCommentGuid: false);
                Identifier ClonedCreationRoutineIdentifier = (Identifier)DeepCloneNode(node.CreationRoutineIdentifier, cloneCommentGuid: false);
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                CreateInstruction NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ClonedArgumentBlocks, ComplexifiedPropcessor);
                complexifiedInstructionList = new List<Instruction>() { NewCreateInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedIfThenElseInstruction(IfThenElseInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            Debug.Assert(!NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)node.ConditionalBlocks), $"The conditional of {nameof(node)} is never empty");

            Conditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];
            if (GetComplexifiedConditional(FirstConditional, out IList<Conditional> ComplexifiedConditionalList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Conditional ComplexifiedConditional in ComplexifiedConditionalList)
                {
                    IBlockList<Conditional> ClonedConditionalBlocks = (IBlockList<Conditional>)DeepCloneBlockList((IBlockList)node.ConditionalBlocks, cloneCommentGuid: false);
                    ClonedConditionalBlocks.NodeBlockList[0].NodeList[0] = ComplexifiedConditional;

                    IfThenElseInstruction NewIfThenElseInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        Scope ClonedElseInstructions = (Scope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewIfThenElseInstruction = CreateIfThenElseInstruction(ClonedConditionalBlocks, ClonedElseInstructions);
                    }
                    else
                        NewIfThenElseInstruction = CreateIfThenElseInstruction(ClonedConditionalBlocks);

                    complexifiedInstructionList.Add(NewIfThenElseInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedIndexAssignmentInstruction(IndexAssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (ComplexifyQualifiedName(node.Destination, out QualifiedName ComplexifiedDestination))
            {
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ComplexifiedDestination, ClonedArgumentBlocks, ClonedSource);
                complexifiedInstructionList = new List<Instruction>() { NewIndexAssignmentInstruction };
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                QualifiedName ClonedDestination = (QualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ComplexifiedArgumentBlocks, ClonedSource);
                complexifiedInstructionList = new List<Instruction>() { NewIndexAssignmentInstruction };
            }
            else if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    QualifiedName ClonedDestination = (QualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ClonedArgumentBlocks, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewIndexAssignmentInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedInspectInstruction(InspectInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<With> ClonedWithBlocks = (IBlockList<With>)DeepCloneBlockList((IBlockList)node.WithBlocks, cloneCommentGuid: false);
                    InspectInstruction NewInspectInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        Scope ClonedElseInstructions = (Scope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewInspectInstruction = CreateInspectInstruction(ComplexifiedSource, ClonedWithBlocks, ClonedElseInstructions);
                    }
                    else
                        NewInspectInstruction = CreateInspectInstruction(ComplexifiedSource, ClonedWithBlocks);

                    complexifiedInstructionList.Add(NewInspectInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedKeywordAssignmentInstruction(KeywordAssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    KeywordAssignmentInstruction NewKeywordAssignmentInstruction = CreateKeywordAssignmentInstruction(node.Destination, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewKeywordAssignmentInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedOverLoopInstruction(OverLoopInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.OverList, out IList<Expression> ComplexifiedOverListList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedOverList in ComplexifiedOverListList)
                {
                    IBlockList<Name> ClonedIndexerBlocks = (IBlockList<Name>)DeepCloneBlockList((IBlockList)node.IndexerBlocks, cloneCommentGuid: false);
                    Scope ClonedLoopInstructions = (Scope)DeepCloneNode(node.LoopInstructions, cloneCommentGuid: false);
                    IBlockList<Assertion> ClonedInvariantBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.InvariantBlocks, cloneCommentGuid: false);

                    OverLoopInstruction NewOverLoopInstruction;

                    if (node.ExitEntityName.IsAssigned)
                    {
                        Identifier ClonedExitEntityName = (Identifier)DeepCloneNode(node.ExitEntityName.Item, cloneCommentGuid: false);
                        NewOverLoopInstruction = CreateOverLoopInstruction(ComplexifiedOverList, ClonedIndexerBlocks, node.Iteration, ClonedLoopInstructions, ClonedExitEntityName, ClonedInvariantBlocks);
                    }
                    else
                        NewOverLoopInstruction = CreateOverLoopInstruction(ComplexifiedOverList, ClonedIndexerBlocks, node.Iteration, ClonedLoopInstructions, ClonedInvariantBlocks);

                    complexifiedInstructionList.Add(NewOverLoopInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedPrecursorIndexAssignmentInstruction(PrecursorIndexAssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);

                    PrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ComplexifiedAncestorType, ClonedArgumentBlocks, ClonedSource);
                    complexifiedInstructionList.Add(NewPrecursorIndexAssignmentInstruction);
                }
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);

                PrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction;

                if (node.AncestorType.IsAssigned)
                {
                    ObjectType ClonedAncestorType = (ObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                    NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedAncestorType, ComplexifiedArgumentBlocks, ClonedSource);
                }
                else
                    NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ComplexifiedArgumentBlocks, ClonedSource);

                complexifiedInstructionList = new List<Instruction>() { NewPrecursorIndexAssignmentInstruction };
            }
            else if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    PrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction;

                    if (node.AncestorType.IsAssigned)
                    {
                        ObjectType ClonedAncestorType = (ObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                        NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedAncestorType, ClonedArgumentBlocks, ComplexifiedSource);
                    }
                    else
                        NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedArgumentBlocks, ComplexifiedSource);

                    complexifiedInstructionList.Add(NewPrecursorIndexAssignmentInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedPrecursorInstruction(PrecursorInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    PrecursorInstruction NewPrecursorInstruction = CreatePrecursorInstruction(ComplexifiedAncestorType, ClonedArgumentBlocks);
                    complexifiedInstructionList.Add(NewPrecursorInstruction);
                }
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                PrecursorInstruction NewPrecursorInstruction;

                if (node.AncestorType.IsAssigned)
                {
                    ObjectType ClonedAncestorType = (ObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                    NewPrecursorInstruction = CreatePrecursorInstruction(ClonedAncestorType, ComplexifiedArgumentBlocks);
                }
                else
                    NewPrecursorInstruction = CreatePrecursorInstruction(ComplexifiedArgumentBlocks);

                complexifiedInstructionList = new List<Instruction>() { NewPrecursorInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedReleaseInstruction(ReleaseInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (ComplexifyQualifiedName(node.EntityName, out QualifiedName ComplexifiedEntityName))
            {
                ReleaseInstruction NewReleaseInstruction = CreateReleaseInstruction(ComplexifiedEntityName);
                complexifiedInstructionList = new List<Instruction>() { NewReleaseInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedThrowInstruction(ThrowInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedObjectType(node.ExceptionType, out IList<ObjectType> ComplexifiedExceptionTypeList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (ObjectType ComplexifiedExceptionType in ComplexifiedExceptionTypeList)
                {
                    Identifier ClonedCreationRoutine = (Identifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    ThrowInstruction NewThrowInstruction = CreateThrowInstruction(ComplexifiedExceptionType, ClonedCreationRoutine, ClonedArgumentBlocks);
                    complexifiedInstructionList.Add(NewThrowInstruction);
                }
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                ObjectType ClonedExceptionType = (ObjectType)DeepCloneNode(node.ExceptionType, cloneCommentGuid: false);
                Identifier ClonedCreationRoutine = (Identifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);

                ThrowInstruction NewThrowInstruction = CreateThrowInstruction(ClonedExceptionType, ClonedCreationRoutine, ComplexifiedArgumentBlocks);
                complexifiedInstructionList = new List<Instruction>() { NewThrowInstruction };
            }

            return complexifiedInstructionList != null;
        }
    }
}
