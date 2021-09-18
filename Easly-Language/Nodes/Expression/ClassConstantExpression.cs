namespace BaseNode
{
    /// <summary>
    /// Represents a class constant expression.
    /// /Doc/Nodes/Expression/ClassConstantExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ClassConstantExpression : Expression
    {
        /// <summary>
        /// Gets or sets the class name where to find the constant.
        /// </summary>
        public virtual Identifier ClassIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the constant name.
        /// </summary>
        public virtual Identifier ConstantIdentifier { get; set; } = null!;
    }
}
