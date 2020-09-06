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
            { typeof(IAttachment), new string[] { nameof(IAttachment.AttachTypeBlocks) } },
            { typeof(IClassReplicate), new string[] { nameof(IClassReplicate.PatternBlocks) } },
            { typeof(IExport), new string[] { nameof(IExport.ClassIdentifierBlocks) } },
            { typeof(IGlobalReplicate), new string[] { nameof(IGlobalReplicate.Patterns) } },
            { typeof(IQualifiedName), new string[] { nameof(IQualifiedName.Path) } },
            { typeof(IQueryOverload), new string[] { nameof(IQueryOverload.ResultBlocks) } },
            { typeof(IQueryOverloadType), new string[] { nameof(IQueryOverloadType.ResultBlocks) } },
            { typeof(IAssignmentArgument), new string[] { nameof(IAssignmentArgument.ParameterBlocks) } },
            { typeof(IWith), new string[] { nameof(IWith.RangeBlocks) } },
            { typeof(IIndexQueryExpression), new string[] { nameof(IIndexQueryExpression.ArgumentBlocks) } },
            { typeof(IPrecursorIndexExpression), new string[] { nameof(IPrecursorIndexExpression.ArgumentBlocks) } },
            { typeof(ICreationFeature), new string[] { nameof(ICreationFeature.OverloadBlocks) } },
            { typeof(IFunctionFeature), new string[] { nameof(IFunctionFeature.OverloadBlocks) } },
            { typeof(IIndexerFeature), new string[] { nameof(IIndexerFeature.IndexParameterBlocks) } },
            { typeof(IProcedureFeature), new string[] { nameof(IProcedureFeature.OverloadBlocks) } },
            { typeof(IAsLongAsInstruction), new string[] { nameof(IAsLongAsInstruction.ContinuationBlocks) } },
            { typeof(IAssignmentInstruction), new string[] { nameof(IAssignmentInstruction.DestinationBlocks) } },
            { typeof(IAttachmentInstruction), new string[] { nameof(IAttachmentInstruction.EntityNameBlocks), nameof(IAttachmentInstruction.AttachmentBlocks) } },
            { typeof(IIfThenElseInstruction), new string[] { nameof(IIfThenElseInstruction.ConditionalBlocks) } },
            { typeof(IInspectInstruction), new string[] { nameof(IInspectInstruction.WithBlocks) } },
            { typeof(IOverLoopInstruction), new string[] { nameof(IOverLoopInstruction.IndexerBlocks) } },
            { typeof(IIndexAssignmentInstruction), new string[] { nameof(IIndexAssignmentInstruction.ArgumentBlocks) } },
            { typeof(IPrecursorIndexAssignmentInstruction), new string[] { nameof(IPrecursorIndexAssignmentInstruction.ArgumentBlocks) } },
            { typeof(IAnchoredType), new string[] { nameof(IAnchoredType.AnchoredName) } },
            { typeof(IFunctionType), new string[] { nameof(IFunctionType.OverloadBlocks) } },
            { typeof(IGenericType), new string[] { nameof(IGenericType.TypeArgumentBlocks) } },
            { typeof(IIndexerType), new string[] { nameof(IIndexerType.IndexParameterBlocks) } },
            { typeof(IProcedureType), new string[] { nameof(IProcedureType.OverloadBlocks) } },
            { typeof(ITupleType), new string[] { nameof(ITupleType.EntityDeclarationBlocks) } },
        };

        public static IReadOnlyDictionary<Type, string[]> WithExpandCollectionTable { get; } = new Dictionary<Type, string[]>()
        {
            { typeof(IPrecursorExpression), new string[] { nameof(IPrecursorExpression.ArgumentBlocks) } },
            { typeof(IQueryExpression), new string[] { nameof(IQueryExpression.ArgumentBlocks) } },
            { typeof(ICommandInstruction), new string[] { nameof(ICommandInstruction.ArgumentBlocks) } },
            { typeof(ICreateInstruction), new string[] { nameof(ICreateInstruction.ArgumentBlocks) } },
            { typeof(IPrecursorInstruction), new string[] { nameof(IPrecursorInstruction.ArgumentBlocks) } },
            { typeof(IThrowInstruction), new string[] { nameof(IThrowInstruction.ArgumentBlocks) } },
            { typeof(ICommandOverload), new string[] { nameof(ICommandOverload.ParameterBlocks) } },
        };
    }
}
