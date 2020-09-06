#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IReleaseInstruction : IInstruction
    {
        IQualifiedName EntityName { get; }
    }

    [System.Serializable]
    public class ReleaseInstruction : Instruction, IReleaseInstruction
    {
        public virtual IQualifiedName EntityName { get; set; }
    }
}
