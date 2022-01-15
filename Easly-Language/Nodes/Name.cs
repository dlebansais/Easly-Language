namespace BaseNode
{
    /// <summary>
    /// Represents the definition of a name.
    /// /Doc/Nodes/Name.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Name : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Name"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="text">The name.</param>
        internal Name(Document documentation, string text)
            : base(documentation)
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Text { get; set; }
    }
}
