#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

    public static class NodeTreeWalk
    {
        public static bool Walk<TContext>(Node root, WalkCallbacks<TContext> callbacks, TContext data)
        where TContext : class
        {
            return NodeTreeWalk<TContext>.Walk(root, callbacks, data);
        }
    }
}
