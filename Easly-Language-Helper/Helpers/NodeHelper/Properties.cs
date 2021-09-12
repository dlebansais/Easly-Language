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
        public static IReadOnlyDictionary<Type, string[]> NeverEmptyCollectionTable { get; } = new Dictionary<Type, string[]>()
        {
            { typeof(Attachment), new string[] { nameof(Attachment.AttachTypeBlocks) } },
            { typeof(ClassReplicate), new string[] { nameof(ClassReplicate.PatternBlocks) } },
            { typeof(Export), new string[] { nameof(Export.ClassIdentifierBlocks) } },
            { typeof(GlobalReplicate), new string[] { nameof(GlobalReplicate.Patterns) } },
            { typeof(QualifiedName), new string[] { nameof(QualifiedName.Path) } },
            { typeof(QueryOverload), new string[] { nameof(QueryOverload.ResultBlocks) } },
            { typeof(QueryOverloadType), new string[] { nameof(QueryOverloadType.ResultBlocks) } },
            { typeof(AssignmentArgument), new string[] { nameof(AssignmentArgument.ParameterBlocks) } },
            { typeof(With), new string[] { nameof(With.RangeBlocks) } },
            { typeof(IndexQueryExpression), new string[] { nameof(IndexQueryExpression.ArgumentBlocks) } },
            { typeof(PrecursorIndexExpression), new string[] { nameof(PrecursorIndexExpression.ArgumentBlocks) } },
            { typeof(CreationFeature), new string[] { nameof(CreationFeature.OverloadBlocks) } },
            { typeof(FunctionFeature), new string[] { nameof(FunctionFeature.OverloadBlocks) } },
            { typeof(IndexerFeature), new string[] { nameof(IndexerFeature.IndexParameterBlocks) } },
            { typeof(ProcedureFeature), new string[] { nameof(ProcedureFeature.OverloadBlocks) } },
            { typeof(AsLongAsInstruction), new string[] { nameof(AsLongAsInstruction.ContinuationBlocks) } },
            { typeof(AssignmentInstruction), new string[] { nameof(AssignmentInstruction.DestinationBlocks) } },
            { typeof(AttachmentInstruction), new string[] { nameof(AttachmentInstruction.EntityNameBlocks), nameof(AttachmentInstruction.AttachmentBlocks) } },
            { typeof(IfThenElseInstruction), new string[] { nameof(IfThenElseInstruction.ConditionalBlocks) } },
            { typeof(InspectInstruction), new string[] { nameof(InspectInstruction.WithBlocks) } },
            { typeof(OverLoopInstruction), new string[] { nameof(OverLoopInstruction.IndexerBlocks) } },
            { typeof(IndexAssignmentInstruction), new string[] { nameof(IndexAssignmentInstruction.ArgumentBlocks) } },
            { typeof(PrecursorIndexAssignmentInstruction), new string[] { nameof(PrecursorIndexAssignmentInstruction.ArgumentBlocks) } },
            { typeof(AnchoredType), new string[] { nameof(AnchoredType.AnchoredName) } },
            { typeof(FunctionType), new string[] { nameof(FunctionType.OverloadBlocks) } },
            { typeof(GenericType), new string[] { nameof(GenericType.TypeArgumentBlocks) } },
            { typeof(IndexerType), new string[] { nameof(IndexerType.IndexParameterBlocks) } },
            { typeof(ProcedureType), new string[] { nameof(ProcedureType.OverloadBlocks) } },
            { typeof(TupleType), new string[] { nameof(TupleType.EntityDeclarationBlocks) } },
        };

        public static IReadOnlyDictionary<Type, string[]> WithExpandCollectionTable { get; } = new Dictionary<Type, string[]>()
        {
            { typeof(PrecursorExpression), new string[] { nameof(PrecursorExpression.ArgumentBlocks) } },
            { typeof(QueryExpression), new string[] { nameof(QueryExpression.ArgumentBlocks) } },
            { typeof(CommandInstruction), new string[] { nameof(CommandInstruction.ArgumentBlocks) } },
            { typeof(CreateInstruction), new string[] { nameof(CreateInstruction.ArgumentBlocks) } },
            { typeof(PrecursorInstruction), new string[] { nameof(PrecursorInstruction.ArgumentBlocks) } },
            { typeof(ThrowInstruction), new string[] { nameof(ThrowInstruction.ArgumentBlocks) } },
            { typeof(CommandOverload), new string[] { nameof(CommandOverload.ParameterBlocks) } },
        };
    }
}
