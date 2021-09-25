namespace BaseNodeHelper
{
    using BaseNode;

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
            return NodeTreeWalk<TContext>.Walk(root, callbacks, data);
        }
    }
}
