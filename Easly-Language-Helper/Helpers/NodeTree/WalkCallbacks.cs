namespace BaseNodeHelper;

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
    /// The default value of <see cref="HandlerRoot"/>.
    /// </summary>
    internal static Func<Node, WalkCallbacks<TContext>, TContext, bool> DefaultHandlerRoot = (Node root, WalkCallbacks<TContext> callback, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for the root node.
    /// </summary>
    public Func<Node, WalkCallbacks<TContext>, TContext, bool> HandlerRoot { get; set; } = DefaultHandlerRoot;

    /// <summary>
    /// The default value of <see cref="HandlerNode"/>.
    /// </summary>
    internal static Func<Node, Node, string, WalkCallbacks<TContext>, TContext, bool> DefaultHandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TContext> callback, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for child nodes.
    /// </summary>
    public Func<Node, Node, string, WalkCallbacks<TContext>, TContext, bool> HandlerNode { get; set; } = DefaultHandlerNode;

    /// <summary>
    /// The default value of <see cref="HandlerBlockList"/>.
    /// </summary>
    internal static Func<Node, string, IBlockList, WalkCallbacks<TContext>, TContext, bool> DefaultHandlerBlockList = (Node node, string propertyName, IBlockList blockList, WalkCallbacks<TContext> callback, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for block lists.
    /// </summary>
    public Func<Node, string, IBlockList, WalkCallbacks<TContext>, TContext, bool> HandlerBlockList { get; set; } = DefaultHandlerBlockList;

    /// <summary>
    /// The default value of <see cref="HandlerBlock"/>.
    /// </summary>
    internal static Func<Node, string, IBlockList, IBlock, WalkCallbacks<TContext>, TContext, bool> DefaultHandlerBlock = (Node node, string propertyName, IBlockList blockList, IBlock block, WalkCallbacks<TContext> callback, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for blocks.
    /// </summary>
    public Func<Node, string, IBlockList, IBlock, WalkCallbacks<TContext>, TContext, bool> HandlerBlock { get; set; } = DefaultHandlerBlock;

    /// <summary>
    /// The default value of <see cref="HandlerList"/>.
    /// </summary>
    internal static Func<Node, string, IReadOnlyList<Node>, WalkCallbacks<TContext>, TContext, bool> DefaultHandlerList = (Node node, string propertyName, IReadOnlyList<Node> nodeList, WalkCallbacks<TContext> callback, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for list of nodes.
    /// </summary>
    public Func<Node, string, IReadOnlyList<Node>, WalkCallbacks<TContext>, TContext, bool> HandlerList { get; set; } = DefaultHandlerList;

    /// <summary>
    /// The default value of <see cref="HandlerString"/>.
    /// </summary>
    internal static Func<Node, string, TContext, bool> DefaultHandlerString = (Node node, string propertyName, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for strings.
    /// </summary>
    public Func<Node, string, TContext, bool> HandlerString { get; set; } = DefaultHandlerString;

    /// <summary>
    /// The default value of <see cref="HandlerGuid"/>.
    /// </summary>
    internal static Func<Node, string, TContext, bool> DefaultHandlerGuid = (Node node, string propertyName, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for <see cref="Guid"/> values.
    /// </summary>
    public Func<Node, string, TContext, bool> HandlerGuid { get; set; } = DefaultHandlerGuid;

    /// <summary>
    /// The default value of <see cref="HandlerEnum"/>.
    /// </summary>
    internal static Func<Node, string, TContext, bool> DefaultHandlerEnum = (Node node, string propertyName, TContext data) => { return true; };

    /// <summary>
    /// Gets or sets the handler for enums.
    /// </summary>
    public Func<Node, string, TContext, bool> HandlerEnum { get; set; } = DefaultHandlerEnum;

    /// <summary>
    /// Gets or sets a value indicating whether the callback are handling parameters recursively.
    /// </summary>
    public bool IsRecursive { get; set; }

    /// <summary>
    /// Gets or sets a pair of strings to substitute in blocks.
    /// </summary>
    public KeyValuePair<string, string> BlockSubstitution { get; set; } = new(string.Empty, string.Empty);

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
