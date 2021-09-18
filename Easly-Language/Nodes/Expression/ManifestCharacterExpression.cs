namespace BaseNode
{
    /// <summary>
    /// Represents a character constant expression.
    /// /Doc/Nodes/Expression/ManifestCharacterExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ManifestCharacterExpression : Expression
    {
        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
    }
}
