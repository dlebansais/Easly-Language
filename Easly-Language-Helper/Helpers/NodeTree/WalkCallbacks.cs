namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;

    /// <summary>
    /// Represents callbacks used when walking a tree of nodes.
    /// </summary>
    /// <typeparam name="TContext">The walking context.</typeparam>
    public struct WalkCallbacks<TContext> : IEquatable<WalkCallbacks<TContext>>
        where TContext : class
    {
        /// <summary>
        /// Gets or sets the handler for child nodes.
        /// </summary>
        public Func<Node, Node?, string?, WalkCallbacks<TContext>, TContext, bool> HandlerNode { get; set; }

        /// <summary>
        /// Gets or sets the handler for block lists.
        /// </summary>
        public Func<Node, string, IBlockList, WalkCallbacks<TContext>, TContext, bool> HandlerBlockList { get; set; }

        /// <summary>
        /// Gets or sets the handler for blocks.
        /// </summary>
        public Func<Node, string, IBlockList, IBlock, WalkCallbacks<TContext>, TContext, bool> HandlerBlock { get; set; }

        /// <summary>
        /// Gets or sets the handler for list of nodes.
        /// </summary>
        public Func<Node, string, IReadOnlyList<Node>, WalkCallbacks<TContext>, TContext, bool> HandlerList { get; set; }

        /// <summary>
        /// Gets or sets the handler for strings.
        /// </summary>
        public Func<Node, string, TContext, bool> HandlerString { get; set; }

        /// <summary>
        /// Gets or sets the handler for <see cref="Guid"/> values.
        /// </summary>
        public Func<Node, string, TContext, bool> HandlerGuid { get; set; }

        /// <summary>
        /// Gets or sets the handler for enums.
        /// </summary>
        public Func<Node, string, TContext, bool> HandlerEnum { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the callback are handling parameters recursively.
        /// </summary>
        public bool IsRecursive { get; set; }

        /// <summary>
        /// Gets or sets a table of strings to substitute in blocks.
        /// </summary>
        public KeyValuePair<string, string> BlockSubstitution { get; set; }

        /// <summary>
        /// Compares two <see cref="WalkCallbacks{TContext}"/> instances.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>True if instances are equal; otherwise, false.</returns>
        public static bool operator ==(WalkCallbacks<TContext> obj1, WalkCallbacks<TContext> obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Compares two <see cref="WalkCallbacks{TContext}"/> instances.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>True if instances are different; otherwise, false.</returns>
        public static bool operator !=(WalkCallbacks<TContext> obj1, WalkCallbacks<TContext> obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Checks whether this instance is equal to another object.
        /// </summary>
        /// <param name="obj">The other instance.</param>
        /// <returns>True if instances are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Checks whether this instance is equal to another.
        /// </summary>
        /// <param name="obj">The other instance.</param>
        /// <returns>True if instances are equal; otherwise, false.</returns>
        public bool Equals(WalkCallbacks<TContext> obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
