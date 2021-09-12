#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;

    public struct WalkCallbacks<TContext> : IEquatable<WalkCallbacks<TContext>>
        where TContext : class
    {
        public Func<Node, Node, string, WalkCallbacks<TContext>, TContext, bool> HandlerNode { get; set; }
        public Func<Node, string, IBlockList, WalkCallbacks<TContext>, TContext, bool> HandlerBlockList { get; set; }
        public Func<Node, string, IBlockList, IBlock, WalkCallbacks<TContext>, TContext, bool> HandlerBlock { get; set; }
        public Func<Node, string, IReadOnlyList<Node>, WalkCallbacks<TContext>, TContext, bool> HandlerList { get; set; }
        public Func<Node, string, TContext, bool> HandlerString { get; set; }
        public Func<Node, string, TContext, bool> HandlerGuid { get; set; }
        public Func<Node, string, TContext, bool> HandlerEnum { get; set; }
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
