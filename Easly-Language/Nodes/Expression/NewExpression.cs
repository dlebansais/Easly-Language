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
        /// Initializes a new instance of the <see cref="NewExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="object">The name path to the new object.</param>
        internal NewExpression(Document documentation, QualifiedName @object)
            : base(documentation)
        {
            Object = @object;
        }

        /// <summary>
        /// Gets or sets the name path to the new object.
        /// </summary>
        public virtual QualifiedName Object { get; set; }
    }
}
