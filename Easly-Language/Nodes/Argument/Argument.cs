namespace BaseNode
{
    /// <summary>
    /// Represents any argument (positional or assignment).
    /// </summary>
    public abstract class Argument : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Argument"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        internal Argument(Document documentation)
            : base(documentation)
        {
        }
    }
}
