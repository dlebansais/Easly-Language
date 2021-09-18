namespace BaseNode
{
    /// <summary>
    /// Represents the expression designating the value of an object before it was modified.
    /// /Doc/Nodes/Expression/OldExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class OldExpression : Expression
    {
        /// <summary>
        /// Gets or sets the name path to the object.
        /// </summary>
        public virtual QualifiedName Query { get; set; } = null!;
    }
}
