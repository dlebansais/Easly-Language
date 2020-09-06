namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedInstruction(IInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;
            bool Result = false;
            bool IsHandled = false;

            switch (node)
            {
                case IAsLongAsInstruction AsAsLongAsInstruction:
                    Result = GetComplexifiedAsLongAsInstruction(AsAsLongAsInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IAssignmentInstruction AsAssignmentInstruction:
                    Result = GetComplexifiedAssignmentInstruction(AsAssignmentInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IAttachmentInstruction AsAttachmentInstruction:
                    Result = GetComplexifiedAttachmentInstruction(AsAttachmentInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case ICheckInstruction AsCheckInstruction:
                    Result = GetComplexifiedCheckInstruction(AsCheckInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case ICommandInstruction AsCommandInstruction:
                    Result = GetComplexifiedCommandInstruction(AsCommandInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case ICreateInstruction AsCreateInstruction:
                    Result = GetComplexifiedCreateInstruction(AsCreateInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IDebugInstruction AsDebugInstruction:
                case IForLoopInstruction AsForLoopInstruction:
                    IsHandled = true;
                    break;

                case IIfThenElseInstruction AsIfThenElseInstruction:
                    Result = GetComplexifiedIfThenElseInstruction(AsIfThenElseInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IIndexAssignmentInstruction AsIndexAssignmentInstruction:
                    Result = GetComplexifiedIndexAssignmentInstruction(AsIndexAssignmentInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IInspectInstruction AsInspectInstruction:
                    Result = GetComplexifiedInspectInstruction(AsInspectInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IKeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    Result = GetComplexifiedKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IOverLoopInstruction AsOverLoopInstruction:
                    Result = GetComplexifiedOverLoopInstruction(AsOverLoopInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IPrecursorIndexAssignmentInstruction AsPrecursorIndexAssignmentInstruction:
                    Result = GetComplexifiedPrecursorIndexAssignmentInstruction(AsPrecursorIndexAssignmentInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IPrecursorInstruction AsPrecursorInstruction:
                    Result = GetComplexifiedPrecursorInstruction(AsPrecursorInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IRaiseEventInstruction AsRaiseEventInstruction:
                    IsHandled = true;
                    break;

                case IReleaseInstruction AsReleaseInstruction:
                    Result = GetComplexifiedReleaseInstruction(AsReleaseInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;

                case IThrowInstruction AsThrowInstruction:
                    Result = GetComplexifiedThrowInstruction(AsThrowInstruction, out complexifiedInstructionList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(IInstruction)} have been handled");

            return Result;
        }

        private static bool GetComplexifiedAsLongAsInstruction(IAsLongAsInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.ContinueCondition, out IList<IExpression> ComplexifiedContinueConditionList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedContinueCondition in ComplexifiedContinueConditionList)
                {
                    IBlockList<IContinuation, Continuation> ClonedContinuationBlocks = (IBlockList<IContinuation, Continuation>)DeepCloneBlockList((IBlockList)node.ContinuationBlocks, cloneCommentGuid: false);

                    IAsLongAsInstruction NewAsLongAsInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewAsLongAsInstruction = CreateAsLongAsInstruction(ComplexifiedContinueCondition, ClonedContinuationBlocks, ClonedElseInstructions);
                    }
                    else
                        NewAsLongAsInstruction = CreateAsLongAsInstruction(ComplexifiedContinueCondition, ClonedContinuationBlocks);

                    complexifiedInstructionList.Add(NewAsLongAsInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedAssignmentInstruction(IAssignmentInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedQualifiedNameBlockList(node.DestinationBlocks, out IBlockList<IQualifiedName, QualifiedName> ComplexifiedDestinationBlocks))
            {
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IAssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ComplexifiedDestinationBlocks, ClonedSource);
                complexifiedInstructionList = new List<IInstruction>() { NewAssignmentInstruction };
            }
            else if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<IQualifiedName, QualifiedName> ClonedDestinationBlocks = (IBlockList<IQualifiedName, QualifiedName>)DeepCloneBlockList((IBlockList)node.DestinationBlocks, cloneCommentGuid: false);
                    IAssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ClonedDestinationBlocks, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewAssignmentInstruction);
                }
            }
            else if (ComplexifyAsKeywordAssignmentInstruction(node, out IKeywordAssignmentInstruction ComplexifiedKeywordAssignmentInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedKeywordAssignmentInstruction };
            else if (ComplexifyAsIndexAssignmentInstruction(node, out IIndexAssignmentInstruction ComplexifiedIndexAssignmentInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedIndexAssignmentInstruction };

            return complexifiedInstructionList != null;
        }

        private static bool ComplexifyAsKeywordAssignmentInstruction(IAssignmentInstruction node, out IKeywordAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (BlockListHelper<IQualifiedName, QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                IQualifiedName AssignmentTarget = node.DestinationBlocks.NodeBlockList[0].NodeList[0];
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
                            IExpression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;
                            complexifiedNode = CreateKeywordAssignmentInstruction(Keyword, Source);
                        }
                    }
                }
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexAssignmentInstruction(IAssignmentInstruction node, out IIndexAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (BlockListHelper<IQualifiedName, QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                IQualifiedName AssignmentTarget = node.DestinationBlocks.NodeBlockList[0].NodeList[0];
                if (ComplexifyWithArguments(AssignmentTarget, '[', ']', out IQualifiedName NewQuery, out List<IArgument> ArgumentList))
                {
                    IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                    complexifiedNode = CreateIndexAssignmentInstruction(NewQuery, ArgumentList, ClonedSource);
                }
            }

            return complexifiedNode != null;
        }

        private static bool GetComplexifiedAttachmentInstruction(IAttachmentInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<IName, Name> ClonedEntityNameBlocks = (IBlockList<IName, Name>)DeepCloneBlockList((IBlockList)node.EntityNameBlocks, cloneCommentGuid: false);
                    IBlockList<IAttachment, Attachment> ClonedAttachmentBlocks = (IBlockList<IAttachment, Attachment>)DeepCloneBlockList((IBlockList)node.AttachmentBlocks, cloneCommentGuid: false);

                    IAttachmentInstruction NewAttachmentInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewAttachmentInstruction = CreateAttachmentInstruction(ComplexifiedSource, ClonedEntityNameBlocks, ClonedAttachmentBlocks, ClonedElseInstructions);
                    }
                    else
                        NewAttachmentInstruction = CreateAttachmentInstruction(ComplexifiedSource, ClonedEntityNameBlocks, ClonedAttachmentBlocks);

                    complexifiedInstructionList.Add(NewAttachmentInstruction);
                }
            }
            else if (GetComplexifiedNameBlockList(node.EntityNameBlocks, out IBlockList<IName, Name> ComplexifiedEntityNameBlocks))
            {
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IBlockList<IAttachment, Attachment> ClonedAttachmentBlocks = (IBlockList<IAttachment, Attachment>)DeepCloneBlockList((IBlockList)node.AttachmentBlocks, cloneCommentGuid: false);

                IAttachmentInstruction NewAttachmentInstruction;

                if (node.ElseInstructions.IsAssigned)
                {
                    IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                    NewAttachmentInstruction = CreateAttachmentInstruction(ClonedSource, ComplexifiedEntityNameBlocks, ClonedAttachmentBlocks, ClonedElseInstructions);
                }
                else
                    NewAttachmentInstruction = CreateAttachmentInstruction(ClonedSource, ComplexifiedEntityNameBlocks, ClonedAttachmentBlocks);

                complexifiedInstructionList = new List<IInstruction>() { NewAttachmentInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedCheckInstruction(ICheckInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.BooleanExpression, out IList<IExpression> ComplexifiedBooleanExpressionList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedBooleanExpression in ComplexifiedBooleanExpressionList)
                {
                    ICheckInstruction NewCheckInstruction = CreateCheckInstruction(ComplexifiedBooleanExpression);
                    complexifiedInstructionList.Add(NewCheckInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedCommandInstruction(ICommandInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Command, out IQualifiedName NewCommand, out List<IArgument> ArgumentList))
            {
                ICommandInstruction NewCommandInstruction = CreateCommandInstruction(NewCommand, ArgumentList);
                complexifiedInstructionList = new List<IInstruction>() { NewCommandInstruction };
            }
            else if (ComplexifyQualifiedName(node.Command, out IQualifiedName ComplexifiedCommand))
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                ICommandInstruction NewCommandInstruction = CreateCommandInstruction(ComplexifiedCommand, ClonedArgumentBlocks);
                complexifiedInstructionList = new List<IInstruction>() { NewCommandInstruction };
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IQualifiedName ClonedCommand = (IQualifiedName)DeepCloneNode(node.Command, cloneCommentGuid: false);
                ICommandInstruction NewCommandInstruction = CreateCommandInstruction(ClonedCommand, ComplexifiedArgumentBlocks);
                complexifiedInstructionList = new List<IInstruction>() { NewCommandInstruction };
            }
            else if (ComplexifyAsAsLongAsInstruction(node, out IAsLongAsInstruction ComplexifiedAsLongAsInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedAsLongAsInstruction };
            else if (ComplexifyAsAssignmentInstruction(node, out IAssignmentInstruction ComplexifiedAssignmentInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedAssignmentInstruction };
            else if (ComplexifyAsAttachmentInstruction(node, out IAttachmentInstruction ComplexifiedAttachmentInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedAttachmentInstruction };
            else if (ComplexifyAsCheckInstruction(node, out ICheckInstruction ComplexifiedCheckInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedCheckInstruction };
            else if (ComplexifyAsCreateInstruction(node, out ICreateInstruction ComplexifiedCreateInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedCreateInstruction };
            else if (ComplexifyAsDebugInstruction(node, out IDebugInstruction ComplexifieDebugInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifieDebugInstruction };
            else if (ComplexifyAsForLoopInstruction(node, out IForLoopInstruction ComplexifiedForLoopInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedForLoopInstruction };
            else if (ComplexifyAsIfThenElseInstruction(node, out IIfThenElseInstruction ComplexifiedIfThenElseInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedIfThenElseInstruction };
            else if (ComplexifyAsIndexAssignmentInstruction(node, out IIndexAssignmentInstruction ComplexifiedIndexAssignmentInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedIndexAssignmentInstruction };
            else if (ComplexifyAsInspectInstruction(node, out IInspectInstruction ComplexifiedInspectInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedInspectInstruction };
            else if (ComplexifyAsOverLoopInstruction(node, out IOverLoopInstruction ComplexifiedOverLoopInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedOverLoopInstruction };
            else if (ComplexifyAsPrecursorIndexAssignmentInstruction(node, out IPrecursorIndexAssignmentInstruction ComplexifiedPrecursorIndexAssignmentInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedPrecursorIndexAssignmentInstruction };
            else if (ComplexifyAsPrecursorInstruction(node, out IPrecursorInstruction ComplexifiedPrecursorInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedPrecursorInstruction };
            else if (ComplexifyAsRaiseEventInstruction(node, out IRaiseEventInstruction ComplexifiedRaiseEventInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedRaiseEventInstruction };
            else if (ComplexifyAsReleaseInstruction(node, out IReleaseInstruction ComplexifiedReleaseInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedReleaseInstruction };
            else if (ComplexifyAsThrowInstruction(node, out IThrowInstruction ComplexifiedThrowInstruction))
                complexifiedInstructionList = new List<IInstruction>() { ComplexifiedThrowInstruction };

            return complexifiedInstructionList != null;
        }

        private static bool ComplexifyAsAsLongAsInstruction(ICommandInstruction node, out IAsLongAsInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "as long as ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, AfterText, out IExpression Source);
                IContinuation FirstContinuation = CreateEmptyContinuation();
                complexifiedNode = CreateAsLongAsInstruction(Source, FirstContinuation);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsAssignmentInstruction(ICommandInstruction node, out IAssignmentInstruction complexifiedNode)
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

                List<IIdentifier> TargetIdentifierList = new List<IIdentifier>();
                for (int i = 0; i < BreakIndex; i++)
                    TargetIdentifierList.Add(CreateSimpleIdentifier(node.Command.Path[i].Text));
                TargetIdentifierList.Add(CreateSimpleIdentifier(BeforeText));

                string AfterText = node.Command.Path[BreakIndex].Text.Substring(BreakTextIndex + 2).Trim();

                List<IIdentifier> SourceIdentifierList = new List<IIdentifier>();
                SourceIdentifierList.Add(CreateSimpleIdentifier(AfterText));
                for (int i = BreakIndex + 1; i < node.Command.Path.Count; i++)
                    SourceIdentifierList.Add(CreateSimpleIdentifier(node.Command.Path[i].Text));

                IQualifiedName AssignmentTarget = CreateQualifiedName(TargetIdentifierList);

                ICommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
                IQualifiedName AssignmentSource = CreateQualifiedName(SourceIdentifierList);
                IExpression Source = CreateQueryExpression(AssignmentSource, ClonedCommand.ArgumentBlocks);

                complexifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsAttachmentInstruction(ICommandInstruction node, out IAttachmentInstruction complexifiedNode)
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

                IExpression Source = CreateSimpleQueryExpression(ExpressionText);
                IName Name = CreateSimpleName(NameText);

                complexifiedNode = CreateAttachmentInstruction(Source, new List<IName>() { Name });
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsCheckInstruction(ICommandInstruction node, out ICheckInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "check ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, AfterText, out IExpression Source);
                complexifiedNode = CreateCheckInstruction(Source);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsCreateInstruction(ICommandInstruction node, out ICreateInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "create ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IIdentifier EntityIdentifier = CreateEmptyIdentifier();
                IIdentifier CreationRoutineIdentifier = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateCreateInstruction(EntityIdentifier, CreationRoutineIdentifier, new List<IArgument>());
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsDebugInstruction(ICommandInstruction node, out IDebugInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "debug ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out ICommandInstruction ClonedCommand);
                complexifiedNode = CreateSimpleDebugInstruction(ClonedCommand);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsForLoopInstruction(ICommandInstruction node, out IForLoopInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "for ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out ICommandInstruction ClonedCommand);
                complexifiedNode = CreateForLoopInstruction(ClonedCommand);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIfThenElseInstruction(ICommandInstruction node, out IIfThenElseInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "if ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out ICommandInstruction ClonedCommand);
                IConditional FirstConditional = CreateConditional(CreateEmptyQueryExpression(), ClonedCommand);
                complexifiedNode = CreateIfThenElseInstruction(FirstConditional);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexAssignmentInstruction(ICommandInstruction node, out IIndexAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Command.Path.Count > 1 && node.Command.Path[node.Command.Path.Count - 1].Text == "[]:=")
            {
                IQualifiedName ClonedDestination = DeepCloneNode(node.Command, cloneCommentGuid: false) as IQualifiedName;
                ClonedDestination.Path.RemoveAt(ClonedDestination.Path.Count - 1);

                IBlockList<IArgument, Argument> ClonedArgumentBlocks;
                if (node.ArgumentBlocks.NodeBlockList.Count > 0)
                    ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);
                else
                    ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateSimpleBlockList(CreatePositionalArgument(CreateSimpleManifestNumberExpression("0")));

                complexifiedNode = CreateIndexAssignmentInstruction(ClonedDestination, ClonedArgumentBlocks, CreateEmptyQueryExpression());
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsInspectInstruction(ICommandInstruction node, out IInspectInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "inspect ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out ICommandInstruction ClonedCommand);
                IExpression FirstExpression = CreateDefaultManifestNumberExpression();
                IWith FirstWith = CreateWith(FirstExpression, ClonedCommand);

                IExpression Source = CreateEmptyQueryExpression();
                complexifiedNode = CreateInspectInstruction(Source, FirstWith);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsOverLoopInstruction(ICommandInstruction node, out IOverLoopInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "over ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, Pattern, out ICommandInstruction ClonedCommand);

                IExpression OverList = CreateDefaultManifestNumberExpression();
                complexifiedNode = CreateOverLoopInstruction(OverList, new List<IName>() { CreateEmptyName() }, ClonedCommand);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorIndexAssignmentInstruction(ICommandInstruction node, out IPrecursorIndexAssignmentInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Command.Path.Count == 1 && node.Command.Path[0].Text == "precursor[]:=" && node.ArgumentBlocks.NodeBlockList.Count > 0)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);
                complexifiedNode = CreatePrecursorIndexAssignmentInstruction(ClonedArgumentBlocks, CreateEmptyQueryExpression());
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPrecursorInstruction(ICommandInstruction node, out IPrecursorInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Command.Path.Count == 1 && node.Command.Path[0].Text == "precursor")
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);
                complexifiedNode = CreatePrecursorInstruction(ClonedArgumentBlocks);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsRaiseEventInstruction(ICommandInstruction node, out IRaiseEventInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "raise ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IIdentifier QueryIdentifier = CreateSimpleIdentifier(AfterText);
                complexifiedNode = CreateRaiseEventInstruction(QueryIdentifier);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsReleaseInstruction(ICommandInstruction node, out IReleaseInstruction complexifiedNode)
        {
            complexifiedNode = null;
            string Pattern = "release ";

            if (ParsePattern(node, Pattern, out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IQualifiedName entityName = CreateSimpleQualifiedName(AfterText);
                complexifiedNode = CreateReleaseInstruction(entityName);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsThrowInstruction(ICommandInstruction node, out IThrowInstruction complexifiedNode)
        {
            complexifiedNode = null;

            if (ParsePattern(node, "throw ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                IObjectType ExceptionType = CreateEmptySimpleType();
                IIdentifier CreationRoutineIdentifier = CreateSimpleIdentifier(AfterText);
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);

                complexifiedNode = CreateThrowInstruction(ExceptionType, CreationRoutineIdentifier, ClonedArgumentBlocks);
            }

            return complexifiedNode != null;
        }

        private static bool GetComplexifiedCreateInstruction(ICreateInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IIdentifier ClonedEntityIdentifier = (IIdentifier)DeepCloneNode(node.EntityIdentifier, cloneCommentGuid: false);
                IIdentifier ClonedCreationRoutineIdentifier = (IIdentifier)DeepCloneNode(node.CreationRoutineIdentifier, cloneCommentGuid: false);

                ICreateInstruction NewCreateInstruction;

                if (node.Processor.IsAssigned)
                {
                    IQualifiedName ClonedProcessor = (IQualifiedName)DeepCloneNode(node.Processor.Item, cloneCommentGuid: false);
                    NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ComplexifiedArgumentBlocks, ClonedProcessor);
                }
                else
                    NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ComplexifiedArgumentBlocks);

                complexifiedInstructionList = new List<IInstruction>() { NewCreateInstruction };
            }
            else if (node.Processor.IsAssigned && ComplexifyQualifiedName(node.Processor.Item, out IQualifiedName ComplexifiedPropcessor))
            {
                IIdentifier ClonedEntityIdentifier = (IIdentifier)DeepCloneNode(node.EntityIdentifier, cloneCommentGuid: false);
                IIdentifier ClonedCreationRoutineIdentifier = (IIdentifier)DeepCloneNode(node.CreationRoutineIdentifier, cloneCommentGuid: false);
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                ICreateInstruction NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ClonedArgumentBlocks, ComplexifiedPropcessor);
                complexifiedInstructionList = new List<IInstruction>() { NewCreateInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedIfThenElseInstruction(IIfThenElseInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            Debug.Assert(!NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)node.ConditionalBlocks));

            IConditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];
            if (GetComplexifiedConditional(FirstConditional, out IList<IConditional> ComplexifiedConditionalList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IConditional ComplexifiedConditional in ComplexifiedConditionalList)
                {
                    IBlockList<IConditional, Conditional> ClonedConditionalBlocks = (IBlockList<IConditional, Conditional>)DeepCloneBlockList((IBlockList)node.ConditionalBlocks, cloneCommentGuid: false);
                    ClonedConditionalBlocks.NodeBlockList[0].NodeList[0] = ComplexifiedConditional;

                    IIfThenElseInstruction NewIfThenElseInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewIfThenElseInstruction = CreateIfThenElseInstruction(ClonedConditionalBlocks, ClonedElseInstructions);
                    }
                    else
                        NewIfThenElseInstruction = CreateIfThenElseInstruction(ClonedConditionalBlocks);

                    complexifiedInstructionList.Add(NewIfThenElseInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedIndexAssignmentInstruction(IIndexAssignmentInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (ComplexifyQualifiedName(node.Destination, out IQualifiedName ComplexifiedDestination))
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IIndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ComplexifiedDestination, ClonedArgumentBlocks, ClonedSource);
                complexifiedInstructionList = new List<IInstruction>() { NewIndexAssignmentInstruction };
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IQualifiedName ClonedDestination = (IQualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IIndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ComplexifiedArgumentBlocks, ClonedSource);
                complexifiedInstructionList = new List<IInstruction>() { NewIndexAssignmentInstruction };
            }
            else if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IQualifiedName ClonedDestination = (IQualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IIndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ClonedArgumentBlocks, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewIndexAssignmentInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedInspectInstruction(IInspectInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<IWith, With> ClonedWithBlocks = (IBlockList<IWith, With>)DeepCloneBlockList((IBlockList)node.WithBlocks, cloneCommentGuid: false);
                    IInspectInstruction NewInspectInstruction;

                    if (node.ElseInstructions.IsAssigned)
                    {
                        IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                        NewInspectInstruction = CreateInspectInstruction(ComplexifiedSource, ClonedWithBlocks, ClonedElseInstructions);
                    }
                    else
                        NewInspectInstruction = CreateInspectInstruction(ComplexifiedSource, ClonedWithBlocks);

                    complexifiedInstructionList.Add(NewInspectInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedKeywordAssignmentInstruction(IKeywordAssignmentInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IKeywordAssignmentInstruction NewKeywordAssignmentInstruction = CreateKeywordAssignmentInstruction(node.Destination, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewKeywordAssignmentInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedOverLoopInstruction(IOverLoopInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedExpression(node.OverList, out IList<IExpression> ComplexifiedOverListList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedOverList in ComplexifiedOverListList)
                {
                    IBlockList<IName, Name> ClonedIndexerBlocks = (IBlockList<IName, Name>)DeepCloneBlockList((IBlockList)node.IndexerBlocks, cloneCommentGuid: false);
                    IScope ClonedLoopInstructions = (IScope)DeepCloneNode(node.LoopInstructions, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedInvariantBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.InvariantBlocks, cloneCommentGuid: false);

                    IOverLoopInstruction NewOverLoopInstruction;

                    if (node.ExitEntityName.IsAssigned)
                    {
                        IIdentifier ClonedExitEntityName = (IIdentifier)DeepCloneNode(node.ExitEntityName.Item, cloneCommentGuid: false);
                        NewOverLoopInstruction = CreateOverLoopInstruction(ComplexifiedOverList, ClonedIndexerBlocks, node.Iteration, ClonedLoopInstructions, ClonedExitEntityName, ClonedInvariantBlocks);
                    }
                    else
                        NewOverLoopInstruction = CreateOverLoopInstruction(ComplexifiedOverList, ClonedIndexerBlocks, node.Iteration, ClonedLoopInstructions, ClonedInvariantBlocks);

                    complexifiedInstructionList.Add(NewOverLoopInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedPrecursorIndexAssignmentInstruction(IPrecursorIndexAssignmentInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<IObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                    IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);

                    IPrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ComplexifiedAncestorType, ClonedArgumentBlocks, ClonedSource);
                    complexifiedInstructionList.Add(NewPrecursorIndexAssignmentInstruction);
                }
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);

                IPrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction;

                if (node.AncestorType.IsAssigned)
                {
                    IObjectType ClonedAncestorType = (IObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                    NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedAncestorType, ComplexifiedArgumentBlocks, ClonedSource);
                }
                else
                    NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ComplexifiedArgumentBlocks, ClonedSource);

                complexifiedInstructionList = new List<IInstruction>() { NewPrecursorIndexAssignmentInstruction };
            }
            else if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    IPrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction;

                    if (node.AncestorType.IsAssigned)
                    {
                        IObjectType ClonedAncestorType = (IObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                        NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedAncestorType, ClonedArgumentBlocks, ComplexifiedSource);
                    }
                    else
                        NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedArgumentBlocks, ComplexifiedSource);

                    complexifiedInstructionList.Add(NewPrecursorIndexAssignmentInstruction);
                }
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedPrecursorInstruction(IPrecursorInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<IObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    IPrecursorInstruction NewPrecursorInstruction = CreatePrecursorInstruction(ComplexifiedAncestorType, ClonedArgumentBlocks);
                    complexifiedInstructionList.Add(NewPrecursorInstruction);
                }
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IPrecursorInstruction NewPrecursorInstruction;

                if (node.AncestorType.IsAssigned)
                {
                    IObjectType ClonedAncestorType = (IObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                    NewPrecursorInstruction = CreatePrecursorInstruction(ClonedAncestorType, ComplexifiedArgumentBlocks);
                }
                else
                    NewPrecursorInstruction = CreatePrecursorInstruction(ComplexifiedArgumentBlocks);

                complexifiedInstructionList = new List<IInstruction>() { NewPrecursorInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedReleaseInstruction(IReleaseInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (ComplexifyQualifiedName(node.EntityName, out IQualifiedName ComplexifiedEntityName))
            {
                IReleaseInstruction NewReleaseInstruction = CreateReleaseInstruction(ComplexifiedEntityName);
                complexifiedInstructionList = new List<IInstruction>() { NewReleaseInstruction };
            }

            return complexifiedInstructionList != null;
        }

        private static bool GetComplexifiedThrowInstruction(IThrowInstruction node, out IList<IInstruction> complexifiedInstructionList)
        {
            complexifiedInstructionList = null;

            if (GetComplexifiedObjectType(node.ExceptionType, out IList<IObjectType> ComplexifiedExceptionTypeList))
            {
                complexifiedInstructionList = new List<IInstruction>();

                foreach (IObjectType ComplexifiedExceptionType in ComplexifiedExceptionTypeList)
                {
                    IIdentifier ClonedCreationRoutine = (IIdentifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);
                    IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    IThrowInstruction NewThrowInstruction = CreateThrowInstruction(ComplexifiedExceptionType, ClonedCreationRoutine, ClonedArgumentBlocks);
                    complexifiedInstructionList.Add(NewThrowInstruction);
                }
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IObjectType ClonedExceptionType = (IObjectType)DeepCloneNode(node.ExceptionType, cloneCommentGuid: false);
                IIdentifier ClonedCreationRoutine = (IIdentifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);

                IThrowInstruction NewThrowInstruction = CreateThrowInstruction(ClonedExceptionType, ClonedCreationRoutine, ComplexifiedArgumentBlocks);
                complexifiedInstructionList = new List<IInstruction>() { NewThrowInstruction };
            }

            return complexifiedInstructionList != null;
        }
    }
}
