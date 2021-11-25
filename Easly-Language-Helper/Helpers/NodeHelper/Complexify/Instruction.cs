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

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedAsLongAsInstruction(AsLongAsInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedAssignmentInstruction(AssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (GetComplexifiedQualifiedNameBlockList(node.DestinationBlocks, out IBlockList<QualifiedName> ComplexifiedDestinationBlocks))
            {
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                AssignmentInstruction NewAssignmentInstruction = CreateAssignmentInstruction(ComplexifiedDestinationBlocks, ClonedSource);
                complexifiedInstructionList = new List<Instruction>() { NewAssignmentInstruction };
                return true;
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

                return true;
            }
            else if (ComplexifyAsKeywordAssignmentInstruction(node, out KeywordAssignmentInstruction ComplexifiedKeywordAssignmentInstruction))
            {
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedKeywordAssignmentInstruction };
                return true;
            }
            else if (ComplexifyAsIndexAssignmentInstruction(node, out IndexAssignmentInstruction ComplexifiedIndexAssignmentInstruction))
            {
                complexifiedInstructionList = new List<Instruction>() { ComplexifiedIndexAssignmentInstruction };
                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool ComplexifyAsKeywordAssignmentInstruction(AssignmentInstruction node, out KeywordAssignmentInstruction complexifiedNode)
        {
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
                            Expression Source = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                            complexifiedNode = CreateKeywordAssignmentInstruction(Keyword, Source);
                            return true;
                        }
                    }
                }
            }

            Contract.Unused(out complexifiedNode);
            return false;
        }

        private static bool ComplexifyAsIndexAssignmentInstruction(AssignmentInstruction node, out IndexAssignmentInstruction complexifiedNode)
        {
            if (BlockListHelper<QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                QualifiedName AssignmentTarget = node.DestinationBlocks.NodeBlockList[0].NodeList[0];
                if (ComplexifyWithArguments(AssignmentTarget, '[', ']', out QualifiedName NewQuery, out List<Argument> ArgumentList))
                {
                    Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                    complexifiedNode = CreateIndexAssignmentInstruction(NewQuery, ArgumentList, ClonedSource);
                    return true;
                }
            }

            Contract.Unused(out complexifiedNode);
            return false;
        }

        private static bool GetComplexifiedAttachmentInstruction(AttachmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
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
                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedCheckInstruction(CheckInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (GetComplexifiedExpression(node.BooleanExpression, out IList<Expression> ComplexifiedBooleanExpressionList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedBooleanExpression in ComplexifiedBooleanExpressionList)
                {
                    CheckInstruction NewCheckInstruction = CreateCheckInstruction(ComplexifiedBooleanExpression);
                    complexifiedInstructionList.Add(NewCheckInstruction);
                }

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedCreateInstruction(CreateInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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
                return true;
            }
            else if (node.Processor.IsAssigned && ComplexifyQualifiedName(node.Processor.Item, out QualifiedName ComplexifiedPropcessor))
            {
                Identifier ClonedEntityIdentifier = (Identifier)DeepCloneNode(node.EntityIdentifier, cloneCommentGuid: false);
                Identifier ClonedCreationRoutineIdentifier = (Identifier)DeepCloneNode(node.CreationRoutineIdentifier, cloneCommentGuid: false);
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                CreateInstruction NewCreateInstruction = CreateCreateInstruction(ClonedEntityIdentifier, ClonedCreationRoutineIdentifier, ClonedArgumentBlocks, ComplexifiedPropcessor);
                complexifiedInstructionList = new List<Instruction>() { NewCreateInstruction };
                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedIfThenElseInstruction(IfThenElseInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedIndexAssignmentInstruction(IndexAssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (ComplexifyQualifiedName(node.Destination, out QualifiedName ComplexifiedDestination))
            {
                IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ComplexifiedDestination, ClonedArgumentBlocks, ClonedSource);
                complexifiedInstructionList = new List<Instruction>() { NewIndexAssignmentInstruction };
                return true;
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                QualifiedName ClonedDestination = (QualifiedName)DeepCloneNode(node.Destination, cloneCommentGuid: false);
                Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IndexAssignmentInstruction NewIndexAssignmentInstruction = CreateIndexAssignmentInstruction(ClonedDestination, ComplexifiedArgumentBlocks, ClonedSource);
                complexifiedInstructionList = new List<Instruction>() { NewIndexAssignmentInstruction };
                return true;
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

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedInspectInstruction(InspectInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedKeywordAssignmentInstruction(KeywordAssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
                {
                    KeywordAssignmentInstruction NewKeywordAssignmentInstruction = CreateKeywordAssignmentInstruction(node.Destination, ComplexifiedSource);
                    complexifiedInstructionList.Add(NewKeywordAssignmentInstruction);
                }

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedOverLoopInstruction(OverLoopInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedPrecursorIndexAssignmentInstruction(PrecursorIndexAssignmentInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
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
                return true;
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

                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedPrecursorInstruction(PrecursorInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (node.AncestorType.IsAssigned && GetComplexifiedObjectType(node.AncestorType.Item, out IList<ObjectType> ComplexifiedAncestorTypeList))
            {
                complexifiedInstructionList = new List<Instruction>();

                foreach (ObjectType ComplexifiedAncestorType in ComplexifiedAncestorTypeList)
                {
                    IBlockList<Argument> ClonedArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)node.ArgumentBlocks, cloneCommentGuid: false);

                    PrecursorInstruction NewPrecursorInstruction = CreatePrecursorInstruction(ComplexifiedAncestorType, ClonedArgumentBlocks);
                    complexifiedInstructionList.Add(NewPrecursorInstruction);
                }

                return true;
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
                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedReleaseInstruction(ReleaseInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
            if (ComplexifyQualifiedName(node.EntityName, out QualifiedName ComplexifiedEntityName))
            {
                ReleaseInstruction NewReleaseInstruction = CreateReleaseInstruction(ComplexifiedEntityName);
                complexifiedInstructionList = new List<Instruction>() { NewReleaseInstruction };
                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }

        private static bool GetComplexifiedThrowInstruction(ThrowInstruction node, out IList<Instruction> complexifiedInstructionList)
        {
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

                return true;
            }
            else if (GetComplexifiedArgumentBlockList(node.ArgumentBlocks, out IBlockList<Argument> ComplexifiedArgumentBlocks))
            {
                ObjectType ClonedExceptionType = (ObjectType)DeepCloneNode(node.ExceptionType, cloneCommentGuid: false);
                Identifier ClonedCreationRoutine = (Identifier)DeepCloneNode(node.CreationRoutine, cloneCommentGuid: false);

                ThrowInstruction NewThrowInstruction = CreateThrowInstruction(ClonedExceptionType, ClonedCreationRoutine, ComplexifiedArgumentBlocks);
                complexifiedInstructionList = new List<Instruction>() { NewThrowInstruction };
                return true;
            }

            Contract.Unused(out complexifiedInstructionList);
            return false;
        }
    }
}
