namespace BaseNodeHelper;

using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to walk through a tree of nodes.
/// </summary>
public static class NodeTreeWalk
{
    /// <inheritdoc cref="NodeTreeWalk{TContext}.Walk(Node, WalkCallbacks{TContext}, TContext)"/>
    /// <typeparam name="TContext">The node type.</typeparam>
    public static bool Walk<TContext>(Node root, WalkCallbacks<TContext> callbacks, TContext data)
        where TContext : class
    {
        Contract.RequireNotNull(root, out Node Root);
        Contract.RequireNotNull(data, out TContext Data);

        return NodeTreeWalk<TContext>.Walk(Root, callbacks, Data);
    }
}
