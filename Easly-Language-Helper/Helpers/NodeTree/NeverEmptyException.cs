namespace BaseNodeHelper
{
    using System;
    using BaseNode;

    /// <summary>
    /// Defines an exception thrown when attempting to empty a list that must not be empty.
    /// </summary>
    public class NeverEmptyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NeverEmptyException"/> class.
        /// </summary>
        /// <param name="nodeType">The type with the list property.</param>
        /// <param name="propertyName">The property name.</param>
        internal NeverEmptyException(Type nodeType, string propertyName)
            : base($"Collection '{propertyName}' in type '{nodeType}' must not be empty")
        {
            NodeType = nodeType;
            PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the type with the list property.
        /// </summary>
        public Type NodeType { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string PropertyName { get; }
    }
}
