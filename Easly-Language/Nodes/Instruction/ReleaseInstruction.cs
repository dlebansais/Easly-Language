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
