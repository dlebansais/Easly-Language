namespace BaseNodeHelper;

using System.Collections.Generic;
using BaseNode;
using NotNullReflection;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Gets the table of property names for collections that are not allowed to be empty, for each node type.
    /// </summary>
    public static IReadOnlyDictionary<Type, string[]> NeverEmptyCollectionTable { get; } = new Dictionary<Type, string[]>()
    {
        { Type.FromTypeof<Attachment>(), new string[] { nameof(Attachment.AttachTypeBlocks) } },
        { Type.FromTypeof<ClassReplicate>(), new string[] { nameof(ClassReplicate.PatternBlocks) } },
        { Type.FromTypeof<Export>(), new string[] { nameof(Export.ClassIdentifierBlocks) } },
        { Type.FromTypeof<GlobalReplicate>(), new string[] { nameof(GlobalReplicate.Patterns) } },
        { Type.FromTypeof<QualifiedName>(), new string[] { nameof(QualifiedName.Path) } },
        { Type.FromTypeof<QueryOverload>(), new string[] { nameof(QueryOverload.ResultBlocks) } },
        { Type.FromTypeof<QueryOverloadType>(), new string[] { nameof(QueryOverloadType.ResultBlocks) } },
        { Type.FromTypeof<AssignmentArgument>(), new string[] { nameof(AssignmentArgument.ParameterBlocks) } },
        { Type.FromTypeof<With>(), new string[] { nameof(With.RangeBlocks) } },
        { Type.FromTypeof<IndexQueryExpression>(), new string[] { nameof(IndexQueryExpression.ArgumentBlocks) } },
        { Type.FromTypeof<PrecursorIndexExpression>(), new string[] { nameof(PrecursorIndexExpression.ArgumentBlocks) } },
        { Type.FromTypeof<CreationFeature>(), new string[] { nameof(CreationFeature.OverloadBlocks) } },
        { Type.FromTypeof<FunctionFeature>(), new string[] { nameof(FunctionFeature.OverloadBlocks) } },
        { Type.FromTypeof<IndexerFeature>(), new string[] { nameof(IndexerFeature.IndexParameterBlocks) } },
        { Type.FromTypeof<ProcedureFeature>(), new string[] { nameof(ProcedureFeature.OverloadBlocks) } },
        { Type.FromTypeof<AsLongAsInstruction>(), new string[] { nameof(AsLongAsInstruction.ContinuationBlocks) } },
        { Type.FromTypeof<AssignmentInstruction>(), new string[] { nameof(AssignmentInstruction.DestinationBlocks) } },
        { Type.FromTypeof<AttachmentInstruction>(), new string[] { nameof(AttachmentInstruction.EntityNameBlocks), nameof(AttachmentInstruction.AttachmentBlocks) } },
        { Type.FromTypeof<IfThenElseInstruction>(), new string[] { nameof(IfThenElseInstruction.ConditionalBlocks) } },
        { Type.FromTypeof<InspectInstruction>(), new string[] { nameof(InspectInstruction.WithBlocks) } },
        { Type.FromTypeof<OverLoopInstruction>(), new string[] { nameof(OverLoopInstruction.IndexerBlocks) } },
        { Type.FromTypeof<IndexAssignmentInstruction>(), new string[] { nameof(IndexAssignmentInstruction.ArgumentBlocks) } },
        { Type.FromTypeof<PrecursorIndexAssignmentInstruction>(), new string[] { nameof(PrecursorIndexAssignmentInstruction.ArgumentBlocks) } },
        { Type.FromTypeof<AnchoredType>(), new string[] { nameof(AnchoredType.AnchoredName) } },
        { Type.FromTypeof<FunctionType>(), new string[] { nameof(FunctionType.OverloadBlocks) } },
        { Type.FromTypeof<GenericType>(), new string[] { nameof(GenericType.TypeArgumentBlocks) } },
        { Type.FromTypeof<IndexerType>(), new string[] { nameof(IndexerType.IndexParameterBlocks) } },
        { Type.FromTypeof<ProcedureType>(), new string[] { nameof(ProcedureType.OverloadBlocks) } },
        { Type.FromTypeof<TupleType>(), new string[] { nameof(TupleType.EntityDeclarationBlocks) } },
    };

    /// <summary>
    /// Gets the table of property names for collections that can be expanded, for each node type.
    /// </summary>
    public static IReadOnlyDictionary<Type, string[]> WithExpandCollectionTable { get; } = new Dictionary<Type, string[]>()
    {
        { Type.FromTypeof<PrecursorExpression>(), new string[] { nameof(PrecursorExpression.ArgumentBlocks) } },
        { Type.FromTypeof<QueryExpression>(), new string[] { nameof(QueryExpression.ArgumentBlocks) } },
        { Type.FromTypeof<CommandInstruction>(), new string[] { nameof(CommandInstruction.ArgumentBlocks) } },
        { Type.FromTypeof<CreateInstruction>(), new string[] { nameof(CreateInstruction.ArgumentBlocks) } },
        { Type.FromTypeof<PrecursorInstruction>(), new string[] { nameof(PrecursorInstruction.ArgumentBlocks) } },
        { Type.FromTypeof<ThrowInstruction>(), new string[] { nameof(ThrowInstruction.ArgumentBlocks) } },
        { Type.FromTypeof<CommandOverload>(), new string[] { nameof(CommandOverload.ParameterBlocks) } },
    };
}
