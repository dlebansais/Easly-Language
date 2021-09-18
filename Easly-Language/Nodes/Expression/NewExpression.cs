namespace BaseNode
{
    /// <summary>
    /// Represents the expression designating an object as new.
    /// /Doc/Nodes/Expression/NewExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class NewExpression : Expression
    {
        /// <summary>
        /// Gets or sets the name path to the new object.
        /// </summary>
        public virtual QualifiedName Object { get; set; } = null!;
    }
}
