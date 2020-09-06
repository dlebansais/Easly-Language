#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using System.Collections.Generic;

    public interface IQualifiedName : INode
    {
        IList<IIdentifier> Path { get; }
    }

    [System.Serializable]
    public class QualifiedName : Node, IQualifiedName
    {
        public virtual IList<IIdentifier> Path { get; set; }
    }
}
