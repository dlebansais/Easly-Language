#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using System.Collections.Generic;

    [System.Serializable]
    public class QualifiedName : Node
    {
        public virtual IList<Identifier> Path { get; set; }
    }
}
