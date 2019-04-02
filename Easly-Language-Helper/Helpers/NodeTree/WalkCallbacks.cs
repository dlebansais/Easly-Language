using System;
using BaseNode;

namespace BaseNodeHelper
{
    public interface IWalkCallbacks<TContext>
        where TContext : class
    {
        Func<INode, INode, string, TContext, bool> HandlerNode { get; }
        Func<INode, string, TContext, bool> HandlerString { get; }
        Func<INode, string, TContext, bool> HandlerGuid { get; }
        Func<INode, string, TContext, bool> HandlerEnum { get; }
    }

    public struct WalkCallbacks<TContext> : IWalkCallbacks<TContext>
        where TContext : class
    {
        public Func<INode, INode, string, TContext, bool> HandlerNode { get; set; }
        public Func<INode, string, TContext, bool> HandlerString { get; set; }
        public Func<INode, string, TContext, bool> HandlerGuid { get; set; }
        public Func<INode, string, TContext, bool> HandlerEnum { get; set; }
    }
}
