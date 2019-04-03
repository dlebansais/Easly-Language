using System;
using System.Collections.Generic;
using BaseNode;

namespace BaseNodeHelper
{
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

    public struct WalkCallbacks<TContext> : IWalkCallbacks<TContext>
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
    }
}
