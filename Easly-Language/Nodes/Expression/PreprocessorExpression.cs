namespace BaseNode
{
    /// <summary>
    /// Represents a preprocessor expression.
    /// /Doc/Nodes/Expression/PreprocessorExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PreprocessorExpression : Expression
    {
        /// <summary>
        /// Gets or sets the preprocessor to get the value from.
        /// </summary>
        public virtual PreprocessorMacro Value { get; set; } = PreprocessorMacro.DateAndTime;
    }
}
