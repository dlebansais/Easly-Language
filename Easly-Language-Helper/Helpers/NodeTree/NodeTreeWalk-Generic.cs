namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

    /// <summary>
    /// Provides methods to walk through a tree of nodes.
    /// </summary>
    /// <typeparam name="TContext">The type of context to use.</typeparam>
    internal static class NodeTreeWalk<TContext>
        where TContext : class
    {
        /// <summary>
        /// Walks a tree of nodes from a root.
        /// </summary>
        /// <param name="root">The root node.</param>
        /// <param name="callbacks">Callbacks to call for each child.</param>
        /// <param name="data">The call context.</param>
        /// <returns>True if none of the callbacks returned false; otherwise, false.</returns>
        public static bool Walk(Node root, WalkCallbacks<TContext> callbacks, TContext data)
        {
            if (!callbacks.HandlerRoot(root, callbacks, data))
                return false;

            return WalkAfterCallback(root, callbacks, data);
        }

        private static bool Walk(Node node, Node parentNode, string propertyName, WalkCallbacks<TContext> callbacks, TContext data)
        {
            if (!callbacks.HandlerNode(node, parentNode, propertyName, callbacks, data))
                return false;

            if (!callbacks.IsRecursive)
                return true;

            return WalkAfterCallback(node, callbacks, data);
        }

        private static bool WalkAfterCallback(Node node, WalkCallbacks<TContext> callbacks, TContext data)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);

            foreach (string NodePropertyName in PropertyNames)
                if (!WalkProperty(node, callbacks, data, NodePropertyName))
                    return false;

            return true;
        }

        private static bool WalkProperty(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(node, nodePropertyName, out _))
                return WalkChildNode(node, callbacks, data, nodePropertyName);
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, nodePropertyName, out _))
                return WalkOptionalChildNode(node, callbacks, data, nodePropertyName);
            else if (NodeTreeHelperList.IsNodeListProperty(node, nodePropertyName, out _))
                return WalkNodeList(node, callbacks, data, nodePropertyName);
            else if (NodeTreeHelperBlockList.IsBlockListProperty(node, nodePropertyName, out _))
                return WalkBlockList(node, callbacks, data, nodePropertyName);
            else
                return WalkMiscellaneousProperty(node, callbacks, data, nodePropertyName);
        }

        private static bool WalkChildNode(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName)
        {
            NodeTreeHelperChild.GetChildNode(node, nodePropertyName, out Node ChildNode);

            if (!Walk(ChildNode, node, nodePropertyName, callbacks, data))
                return false;

            return true;
        }

        private static bool WalkOptionalChildNode(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName)
        {
            NodeTreeHelperOptional.GetChildNode(node, nodePropertyName, out bool IsAssigned, out bool HasItem, out Node ChildNode);

            if (IsAssigned)
            {
                Debug.Assert(HasItem);

                if (!Walk(ChildNode, node, nodePropertyName, callbacks, data))
                    return false;
            }

            return true;
        }

        private static bool WalkNodeList(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName)
        {
            NodeTreeHelperList.GetChildNodeList(node, nodePropertyName, out IReadOnlyList<Node> ChildNodeList);

            if (!callbacks.HandlerList(node, nodePropertyName, ChildNodeList, callbacks, data))
                return false;

            if (callbacks.IsRecursive || callbacks.HandlerList == WalkCallbacks<TContext>.DefaultHandlerList)
            {
                for (int Index = 0; Index < ChildNodeList.Count; Index++)
                {
                    Node ChildNode = ChildNodeList[Index];
                    if (!Walk(ChildNode, node, nodePropertyName, callbacks, data))
                        return false;
                }
            }

            return true;
        }

        private static bool WalkBlockList(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName)
        {
            IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(node, nodePropertyName);

            if (!callbacks.HandlerBlockList(node, nodePropertyName, BlockList, callbacks, data))
                return false;

            if (callbacks.IsRecursive || callbacks.HandlerBlockList == WalkCallbacks<TContext>.DefaultHandlerBlockList)
            {
                string Key = callbacks.BlockSubstitution.Key;
                string Value = callbacks.BlockSubstitution.Value;

                if (Key.Length > 0)
                    return WalkBlockListWithSubstitution(node, callbacks, data, nodePropertyName, Key, Value);
                else
                    return WalkBlockList(node, callbacks, data, nodePropertyName, BlockList);
            }

            return true;
        }

        private static bool WalkBlockListWithSubstitution(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName, string key, string value)
        {
            Debug.Assert(key.Length > 0);

            string ListPropertyName = nodePropertyName.Replace(key, value);

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, ListPropertyName);
            IList NodeList = SafeType.GetPropertyValue<IList>(Property, node);

            for (int Index = 0; Index < NodeList.Count; Index++)
            {
                Node ChildNode = SafeType.ItemAt<Node>(NodeList, Index);

                if (!Walk(ChildNode, node, nodePropertyName, callbacks, data))
                    return false;
            }

            return true;
        }

        private static bool WalkBlockList(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName, IBlockList blockList)
        {
            for (int BlockIndex = 0; BlockIndex < blockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock Block = SafeType.ItemAt<IBlock>(blockList.NodeBlockList, BlockIndex);

                if (!callbacks.HandlerBlock(node, nodePropertyName, blockList, Block, callbacks, data))
                    return false;

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node ChildNode = SafeType.ItemAt<Node>(Block.NodeList, Index);

                    if (!Walk(ChildNode, node, nodePropertyName, callbacks, data))
                        return false;
                }
            }

            return true;
        }

        private static bool WalkMiscellaneousProperty(Node node, WalkCallbacks<TContext> callbacks, TContext data, string nodePropertyName)
        {
            if (NodeTreeHelper.IsBooleanProperty(node, nodePropertyName))
            {
                if (!callbacks.HandlerEnum(node, nodePropertyName, data))
                    return false;
            }
            else if (NodeTreeHelper.IsEnumProperty(node, nodePropertyName))
            {
                if (!callbacks.HandlerEnum(node, nodePropertyName, data))
                    return false;
            }
            else if (NodeTreeHelper.IsStringProperty(node, nodePropertyName))
            {
                if (!callbacks.HandlerString(node, nodePropertyName, data))
                    return false;
            }
            else if (NodeTreeHelper.IsGuidProperty(node, nodePropertyName))
            {
                if (!callbacks.HandlerGuid(node, nodePropertyName, data))
                    return false;
            }
            else
            {
                Debug.Assert(NodeTreeHelper.IsDocumentProperty(node, nodePropertyName));
            }

            return true;
        }
    }
}
