namespace BaseNode
{
    /// <summary>
    /// Represents any type argument (positional or assignment).
    /// </summary>
    public abstract class TypeArgument : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeArgument"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        internal TypeArgument(Document documentation)
            : base(documentation)
        {
        }
    }
}
