namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;

    public interface IWalkCallbacks<TContext>
        where TContext : class
    {
        Func<INode, INode, string, IWalkCallbacks<TContext>, TContext, bool> HandlerNode { get; }
        Func<INode, string, IBlockList, IWalkCallbacks<TContext>, TContext, bool> HandlerBlockList { get; }
        Func<INode, string, IBlockList, IBlock, IWalkCallbacks<TContext>, TContext, bool> HandlerBlock { get; }
        Func<INode, string, IReadOnlyList<INode>, IWalkCallbacks<TContext>, TContext, bool> HandlerList { get; }
        Func<INode, string, TContext, bool> HandlerString { get; }
        Func<INode, string, TContext, bool> HandlerGuid { get; }
        Func<INode, string, TContext, bool> HandlerEnum { get; }
        bool IsRecursive { get; }
        KeyValuePair<string, string> BlockSubstitution { get; }
    }

    public struct WalkCallbacks<TContext> : IWalkCallbacks<TContext>, IEquatable<WalkCallbacks<TContext>>
        where TContext : class
    {
        public Func<INode, INode, string, IWalkCallbacks<TContext>, TContext, bool> HandlerNode { get; set; }
        public Func<INode, string, IBlockList, IWalkCallbacks<TContext>, TContext, bool> HandlerBlockList { get; set; }
        public Func<INode, string, IBlockList, IBlock, IWalkCallbacks<TContext>, TContext, bool> HandlerBlock { get; set; }
        public Func<INode, string, IReadOnlyList<INode>, IWalkCallbacks<TContext>, TContext, bool> HandlerList { get; set; }
        public Func<INode, string, TContext, bool> HandlerString { get; set; }
        public Func<INode, string, TContext, bool> HandlerGuid { get; set; }
        public Func<INode, string, TContext, bool> HandlerEnum { get; set; }
        public bool IsRecursive { get; set; }
        public KeyValuePair<string, string> BlockSubstitution { get; set; }

        public static bool operator ==(WalkCallbacks<TContext> obj1, WalkCallbacks<TContext> obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(WalkCallbacks<TContext> obj1, WalkCallbacks<TContext> obj2)
        {
            return !obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(WalkCallbacks<TContext> obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
