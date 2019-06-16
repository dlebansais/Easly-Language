namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static void GetComplexifiedInstruction(IInstruction node, List<INode> complexifiedNodeList)
        {
            bool IsHandled = false;

            switch (node)
            {
                case IAsLongAsInstruction AsAsLongAsInstruction:
                    GetComplexifiedAsLongAsInstruction(AsAsLongAsInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IAssignmentInstruction AsAssignmentInstruction:
                    GetComplexifiedAssignmentInstruction(AsAssignmentInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IAttachmentInstruction AsAttachmentInstruction:
                    GetComplexifiedAttachmentInstruction(AsAttachmentInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case ICheckInstruction AsCheckInstruction:
                    GetComplexifiedCheckInstruction(AsCheckInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case ICommandInstruction AsCommandInstruction:
                    GetComplexifiedCommandInstruction(AsCommandInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case ICreateInstruction AsCreateInstruction:
                    GetComplexifiedCreateInstruction(AsCreateInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IDebugInstruction AsDebugInstruction:
                case IForLoopInstruction AsForLoopInstruction:
                    IsHandled = true;
                    break;

                case IIfThenElseInstruction AsIfThenElseInstruction:
                    GetComplexifiedIfThenElseInstruction(AsIfThenElseInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IIndexAssignmentInstruction AsIndexAssignmentInstruction:
                    GetComplexifiedIndexAssignmentInstruction(AsIndexAssignmentInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IInspectInstruction AsInspectInstruction:
                    GetComplexifiedInspectInstruction(AsInspectInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IKeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    GetComplexifiedKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IOverLoopInstruction AsOverLoopInstruction:
                    GetComplexifiedOverLoopInstruction(AsOverLoopInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPrecursorIndexAssignmentInstruction AsPrecursorIndexAssignmentInstruction:
                    GetComplexifiedPrecursorIndexAssignmentInstruction(AsPrecursorIndexAssignmentInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPrecursorInstruction AsPrecursorInstruction:
                    GetComplexifiedPrecursorInstruction(AsPrecursorInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IRaiseEventInstruction AsRaiseEventInstruction:
                    IsHandled = true;
                    break;

                case IReleaseInstruction AsReleaseInstruction:
                    GetComplexifiedReleaseInstruction(AsReleaseInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IThrowInstruction AsThrowInstruction:
                    GetComplexifiedThrowInstruction(AsThrowInstruction, complexifiedNodeList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        public static void GetComplexifiedAsLongAsInstruction(IAsLongAsInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.ContinueCondition, out List<INode> ComplexifiedContinueConditionList) && ComplexifiedContinueConditionList[0] is IExpression AsComplexifiedContinueCondition)
            {
                IBlockList<IContinuation, Continuation> ClonedContinuationBlocks = (IBlockList<IContinuation, Continuation>)DeepCloneBlockList((IBlockList)node.ContinuationBlocks, cloneCommentGuid: false);

                IAsLongAsInstruction NewAsLongAsInstruction;

                if (node.ElseInstructions.IsAssigned)
                {
                    IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                    NewAsLongAsInstruction = CreateAsLongAsInstruction(AsComplexifiedContinueCondition, ClonedContinuationBlocks, ClonedElseInstructions);
                }
                else
                    NewAsLongAsInstruction = CreateAsLongAsInstruction(AsComplexifiedContinueCondition, ClonedContinuationBlocks);

                complexifiedNodeList.Add(NewAsLongAsInstruction);
            }
        }

        public static void GetComplexifiedAssignmentInstruction(IAssignmentInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedQualifiedNameBlockList(node.DestinationBlocks, out IBlockList<IQualifiedName, QualifiedName> ComplexifiedDestinationBlocks))
            {
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IAssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ComplexifiedDestinationBlocks, ClonedSource);
                complexifiedNodeList.Add(NewAssignmentInstruction);
            }
            else if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && IsNodeListSameType(ComplexifiedSourceList, out IList<IExpression> ComplexifiedList))
            {
                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<IQualifiedName, QualifiedName> ClonedDestinationBlocks = (IBlockList<IQualifiedName, QualifiedName>)DeepCloneBlockList((IBlockList)node.DestinationBlocks, cloneCommentGuid: false);
                    IAssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ClonedDestinationBlocks, ComplexifiedSource);
                    complexifiedNodeList.Add(NewAssignmentInstruction);
                }
            }
            else if (ComplexifyAsKeywordAssignmentInstruction(node, out IKeywordAssignmentInstruction ComplexifiedKeywordAssignmentInstruction))
                complexifiedNodeList.Add(ComplexifiedKeywordAssignmentInstruction);
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
                        Text = Text.Substring(0, 1).ToUpper() + Text.Substring(1);

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

        public static void GetComplexifiedAttachmentInstruction(IAttachmentInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IBlockList<IName, Name> ClonedEntityNameBlocks = (IBlockList<IName, Name>)DeepCloneBlockList((IBlockList)node.EntityNameBlocks, cloneCommentGuid: false);
                IBlockList<IAttachment, Attachment> ClonedAttachmentBlocks = (IBlockList<IAttachment, Attachment>)DeepCloneBlockList((IBlockList)node.AttachmentBlocks, cloneCommentGuid: false);

                IAttachmentInstruction NewAttachmentInstruction;

                if (node.ElseInstructions.IsAssigned)
                {
                    IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                    NewAttachmentInstruction = CreateAttachmentInstruction(AsComplexifiedSource, ClonedEntityNameBlocks, ClonedAttachmentBlocks, ClonedElseInstructions);
                }
                else
                    NewAttachmentInstruction = CreateAttachmentInstruction(AsComplexifiedSource, ClonedEntityNameBlocks, ClonedAttachmentBlocks);

                complexifiedNodeList.Add(NewAttachmentInstruction);
            }
        }

        public static void GetComplexifiedCheckInstruction(ICheckInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.BooleanExpression, out List<INode> ComplexifiedBooleanExpressionList) && ComplexifiedBooleanExpressionList[0] is IExpression AsComplexifiedBooleanExpression)
            {
                ICheckInstruction NewCheckInstruction = CreateCheckInstruction(AsComplexifiedBooleanExpression);
                complexifiedNodeList.Add(NewCheckInstruction);
            }
        }

        public static void GetComplexifiedCommandInstruction(ICommandInstruction node, IList<INode> complexifiedNodeList)
        {
            if (node.ArgumentBlocks.NodeBlockList.Count == 0 && ComplexifyWithArguments(node.Command, out IQualifiedName NewCommand, out List<IArgument> ArgumentList))
            {
                ICommandInstruction NewCommandInstruction = CreateCommandInstruction(NewCommand, ArgumentList);
                complexifiedNodeList.Add(NewCommandInstruction);
            }
            else if (ComplexifyQualifiedName(node.Command, out IQualifiedName ComplexifiedCommand))
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                ICommandInstruction NewCommandInstruction = CreateCommandInstruction(ComplexifiedCommand, ClonedArgumentBlocks);
                complexifiedNodeList.Add(NewCommandInstruction);
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IQualifiedName ClonedCommand = (IQualifiedName)DeepCloneNode(node.Command, cloneCommentGuid: false);
                ICommandInstruction NewCommandInstruction = CreateCommandInstruction(ClonedCommand, ComplexifiedArgumentBlocks);
            }
            else if (ComplexifyAsAsLongAsInstruction(node, out IAsLongAsInstruction ComplexifiedAsLongAsInstruction))
                complexifiedNodeList.Add(ComplexifiedAsLongAsInstruction);
            else if (ComplexifyAsAssignmentInstruction(node, out IAssignmentInstruction ComplexifiedAssignmentInstruction))
                complexifiedNodeList.Add(ComplexifiedAssignmentInstruction);
            else if (ComplexifyAsAttachmentInstruction(node, out IAttachmentInstruction ComplexifiedAttachmentInstruction))
                complexifiedNodeList.Add(ComplexifiedAttachmentInstruction);
            else if (ComplexifyAsCheckInstruction(node, out ICheckInstruction ComplexifiedCheckInstruction))
                complexifiedNodeList.Add(ComplexifiedCheckInstruction);
            else if (ComplexifyAsCreateInstruction(node, out ICreateInstruction ComplexifiedCreateInstruction))
                complexifiedNodeList.Add(ComplexifiedCreateInstruction);
            else if (ComplexifyAsDebugInstruction(node, out IDebugInstruction ComplexifieDebugInstruction))
                complexifiedNodeList.Add(ComplexifieDebugInstruction);
            else if (ComplexifyAsForLoopInstruction(node, out IForLoopInstruction ComplexifiedForLoopInstruction))
                complexifiedNodeList.Add(ComplexifiedForLoopInstruction);
            else if (ComplexifyAsIfThenElseInstruction(node, out IIfThenElseInstruction ComplexifiedIfThenElseInstruction))
                complexifiedNodeList.Add(ComplexifiedIfThenElseInstruction);
            else if (ComplexifyAsIndexAssignmentInstruction(node, out IIndexAssignmentInstruction ComplexifiedIndexAssignmentInstruction))
                complexifiedNodeList.Add(ComplexifiedIndexAssignmentInstruction);
            else if (ComplexifyAsInspectInstruction(node, out IInspectInstruction ComplexifiedInspectInstruction))
                complexifiedNodeList.Add(ComplexifiedInspectInstruction);
            else if (ComplexifyAsOverLoopInstruction(node, out IOverLoopInstruction ComplexifiedOverLoopInstruction))
                complexifiedNodeList.Add(ComplexifiedOverLoopInstruction);
            else if (ComplexifyAsPrecursorIndexAssignmentInstruction(node, out IPrecursorIndexAssignmentInstruction ComplexifiedPrecursorIndexAssignmentInstruction))
                complexifiedNodeList.Add(ComplexifiedPrecursorIndexAssignmentInstruction);
            else if (ComplexifyAsPrecursorInstruction(node, out IPrecursorInstruction ComplexifiedPrecursorInstruction))
                complexifiedNodeList.Add(ComplexifiedPrecursorInstruction);
            else if (ComplexifyAsRaiseEventInstruction(node, out IRaiseEventInstruction ComplexifiedRaiseEventInstruction))
                complexifiedNodeList.Add(ComplexifiedRaiseEventInstruction);
            else if (ComplexifyAsReleaseInstruction(node, out IReleaseInstruction ComplexifiedReleaseInstruction))
                complexifiedNodeList.Add(ComplexifiedReleaseInstruction);
            else if (ComplexifyAsThrowInstruction(node, out IThrowInstruction ComplexifiedThrowInstruction))
                complexifiedNodeList.Add(ComplexifiedThrowInstruction);
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
                BreakTextIndex = node.Command.Path[i].Text.IndexOf(":=");
                if (BreakTextIndex >= 0)
                {
                    BreakIndex = i;
                    break;
                }
            }

            if (BreakIndex >= 0)
            {
                string BeforeText = node.Command.Path[BreakIndex].Text.Substring(0, BreakTextIndex);

                List<IIdentifier> TargetIdentifierList = new List<IIdentifier>();
                for (int i = 0; i < BreakIndex; i++)
                    TargetIdentifierList.Add(CreateSimpleIdentifier(node.Command.Path[i].Text));
                TargetIdentifierList.Add(CreateSimpleIdentifier(BeforeText));

                string AfterText = node.Command.Path[BreakIndex].Text.Substring(BreakTextIndex + 2);

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

            if (ParsePattern(node, "attach ", out string BeforeText, out string AfterText) && BeforeText.Length == 0)
            {
                CloneComplexifiedCommand(node, AfterText, out IExpression Source);
                complexifiedNode = CreateAttachmentInstruction(Source, new List<IName>() { CreateEmptyName() });
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

        public static void GetComplexifiedCreateInstruction(ICreateInstruction node, List<INode> complexifiedNodeList)
        {
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

                complexifiedNodeList.Add(NewCreateInstruction);
            }
            else if (node.Processor.IsAssigned && ComplexifyQualifiedName(node.Processor.Item, out IQualifiedName ComplexifiedPropcessor))
            {
                IIdentifier ClonedEntityIdentifier = (IIdentifier)DeepCloneNode(node.EntityIdentifier, cloneCommentGuid: false);
                IIdentifier ClonedCreationRoutineIdentifier = (IIdentifier)DeepCloneNode(node.CreationRoutineIdentifier, cloneCommentGuid: false);
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                ICreateInstruction NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ClonedArgumentBlocks, ComplexifiedPropcessor);
            }
        }

        public static void GetComplexifiedIfThenElseInstruction(IIfThenElseInstruction node, List<INode> complexifiedNodeList)
        {
            Debug.Assert(!NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)node.ConditionalBlocks));

            IConditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];
            if (ComplexifyConditional(FirstConditional, out IList<IConditional> ComplexifiedConditionalList))
            {
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

                    complexifiedNodeList.Add(NewIfThenElseInstruction);
                }
            }
        }

        public static void GetComplexifiedIndexAssignmentInstruction(IIndexAssignmentInstruction node, List<INode> complexifiedNodeList)
        {
            if (ComplexifyQualifiedName(node.Destination, out IQualifiedName ComplexifiedDestination))
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IIndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ComplexifiedDestination, ClonedArgumentBlocks, ClonedSource);
                complexifiedNodeList.Add(NewIndexAssignmentInstruction);
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IQualifiedName ClonedDestination = (IQualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IIndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ComplexifiedArgumentBlocks, ClonedSource);
                complexifiedNodeList.Add(NewIndexAssignmentInstruction);
            }
            else if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IQualifiedName ClonedDestination = (IQualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IIndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ClonedArgumentBlocks, AsComplexifiedSource);
                complexifiedNodeList.Add(NewIndexAssignmentInstruction);
            }
        }

        public static void GetComplexifiedInspectInstruction(IInspectInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IBlockList<IWith, With> ClonedWithBlocks = (IBlockList<IWith, With>)DeepCloneBlockList((IBlockList)node.WithBlocks, cloneCommentGuid: false);

                IInspectInstruction NewInspectInstruction;

                if (node.ElseInstructions.IsAssigned)
                {
                    IScope ClonedElseInstructions = (IScope)DeepCloneNode(node.ElseInstructions.Item, cloneCommentGuid: false);
                    NewInspectInstruction = CreateInspectInstruction(AsComplexifiedSource, ClonedWithBlocks, ClonedElseInstructions);
                }
                else
                    NewInspectInstruction = CreateInspectInstruction(AsComplexifiedSource, ClonedWithBlocks);

                complexifiedNodeList.Add(NewInspectInstruction);
            }
        }

        public static void GetComplexifiedKeywordAssignmentInstruction(IKeywordAssignmentInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IKeywordAssignmentInstruction NewKeywordAssignmentInstruction = CreateKeywordAssignmentInstruction(node.Destination, AsComplexifiedSource);
                complexifiedNodeList.Add(NewKeywordAssignmentInstruction);
            }
        }

        public static void GetComplexifiedOverLoopInstruction(IOverLoopInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.OverList, out List<INode> ComplexifiedOverListList) && ComplexifiedOverListList[0] is IExpression AsComplexifiedOverList)
            {
                IBlockList<IName, Name> ClonedIndexerBlocks = (IBlockList<IName, Name>)DeepCloneBlockList((IBlockList)node.IndexerBlocks, cloneCommentGuid: false);
                IScope ClonedLoopInstructions = (IScope)DeepCloneNode(node.LoopInstructions, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedInvariantBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.InvariantBlocks, cloneCommentGuid: false);

                IOverLoopInstruction NewOverLoopInstruction;

                if (node.ExitEntityName.IsAssigned)
                {
                    IIdentifier ClonedExitEntityName = (IIdentifier)DeepCloneNode(node.ExitEntityName.Item, cloneCommentGuid: false);
                    NewOverLoopInstruction = CreateOverLoopInstruction(AsComplexifiedOverList, ClonedIndexerBlocks, node.Iteration, ClonedLoopInstructions, ClonedExitEntityName, ClonedInvariantBlocks);
                }
                else
                    NewOverLoopInstruction = CreateOverLoopInstruction(AsComplexifiedOverList, ClonedIndexerBlocks, node.Iteration, ClonedLoopInstructions, ClonedInvariantBlocks);

                complexifiedNodeList.Add(NewOverLoopInstruction);
            }
        }

        public static void GetComplexifiedPrecursorIndexAssignmentInstruction(IPrecursorIndexAssignmentInstruction node, List<INode> complexifiedNodeList)
        {
            if (node.AncestorType.IsAssigned && GetComplexifiedNode(node.AncestorType.Item, out List<INode> ComplexifiedAncestorTypeList) && ComplexifiedAncestorTypeList[0] is IObjectType AsComplexifiedAncestorType)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);

                IPrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(AsComplexifiedAncestorType, ClonedArgumentBlocks, ClonedSource);
                complexifiedNodeList.Add(NewPrecursorIndexAssignmentInstruction);
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

                complexifiedNodeList.Add(NewPrecursorIndexAssignmentInstruction);
            }
            else if (GetComplexifiedNode(node.Source, out List<INode> ComplexifiedSourceList) && ComplexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                IPrecursorIndexAssignmentInstruction NewPrecursorIndexAssignmentInstruction;

                if (node.AncestorType.IsAssigned)
                {
                    IObjectType ClonedAncestorType = (IObjectType)DeepCloneNode(node.AncestorType.Item, cloneCommentGuid: false);
                    NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedAncestorType, ClonedArgumentBlocks, AsComplexifiedSource);
                }
                else
                    NewPrecursorIndexAssignmentInstruction = CreatePrecursorIndexAssignmentInstruction(ClonedArgumentBlocks, AsComplexifiedSource);

                complexifiedNodeList.Add(NewPrecursorIndexAssignmentInstruction);
            }
        }

        public static void GetComplexifiedPrecursorInstruction(IPrecursorInstruction node, List<INode> complexifiedNodeList)
        {
            if (node.AncestorType.IsAssigned && GetComplexifiedNode(node.AncestorType.Item, out List<INode> ComplexifiedAncestorTypeList) && ComplexifiedAncestorTypeList[0] is IObjectType AsComplexifiedAncestorType)
            {
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                IPrecursorInstruction NewPrecursorInstruction = CreatePrecursorInstruction(AsComplexifiedAncestorType, ClonedArgumentBlocks);
                complexifiedNodeList.Add(NewPrecursorInstruction);
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

                complexifiedNodeList.Add(NewPrecursorInstruction);
            }
        }

        public static void GetComplexifiedReleaseInstruction(IReleaseInstruction node, List<INode> complexifiedNodeList)
        {
            if (ComplexifyQualifiedName(node.EntityName, out IQualifiedName ComplexifiedEntityName))
            {
                IReleaseInstruction NewReleaseInstruction = CreateReleaseInstruction(ComplexifiedEntityName);
                complexifiedNodeList.Add(NewReleaseInstruction);
            }
        }

        public static void GetComplexifiedThrowInstruction(IThrowInstruction node, List<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.ExceptionType, out List<INode> ComplexifiedExceptionTypeList) && ComplexifiedExceptionTypeList[0] is IObjectType AsComplexifiedExceptionType)
            {
                IIdentifier ClonedCreationRoutine = (IIdentifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);
                IBlockList<IArgument, Argument> ClonedArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                IThrowInstruction NewThrowInstruction = CreateThrowInstruction(AsComplexifiedExceptionType, ClonedCreationRoutine, ClonedArgumentBlocks);
                complexifiedNodeList.Add(NewThrowInstruction);
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<IArgument, Argument> ComplexifiedArgumentBlocks))
            {
                IObjectType ClonedExceptionType = (IObjectType)DeepCloneNode(node.ExceptionType, cloneCommentGuid: false);
                IIdentifier ClonedCreationRoutine = (IIdentifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);

                IThrowInstruction NewThrowInstruction = CreateThrowInstruction(ClonedExceptionType, ClonedCreationRoutine, ComplexifiedArgumentBlocks);
                complexifiedNodeList.Add(NewThrowInstruction);
            }
        }
    }
}
