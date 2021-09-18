namespace BaseNode
{
    /// <summary>
    /// Represents any argument (positional or assignment).
    /// /Doc/Nodes/Argument/PositionalArgument.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PositionalArgument : Argument
    {
        /// <summary>
        /// Gets or sets the argument source.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
