#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    [System.Serializable]
    public class QueryOverloadType : Node
    {
        public virtual BlockList<EntityDeclaration> ParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual BlockList<EntityDeclaration> ResultBlocks { get; set; }
        public virtual BlockList<Assertion> RequireBlocks { get; set; }
        public virtual BlockList<Assertion> EnsureBlocks { get; set; }
        public virtual BlockList<Identifier> ExceptionIdentifierBlocks { get; set; }
    }
}
