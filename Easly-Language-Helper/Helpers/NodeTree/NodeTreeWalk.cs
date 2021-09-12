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

#pragma warning disable SA1402 // File may only contain a single type
    public static class NodeTreeWalk<TContext>
#pragma warning restore SA1402 // File may only contain a single type
        where TContext : class
    {
        internal static bool Walk(Node root, WalkCallbacks<TContext> callbacks, TContext data)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));

            return Walk(root, null, null, callbacks, data);
        }

        private static bool Walk(Node node, Node parentNode, string propertyName, WalkCallbacks<TContext> callbacks, TContext data)
        {
            Debug.Assert((parentNode == null && propertyName == null) || (parentNode != null && propertyName != null));

            if (callbacks.HandlerNode != null && !callbacks.HandlerNode(node, parentNode, propertyName, callbacks, data))
                return false;

            if (!callbacks.IsRecursive && parentNode != null)
                return true;

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);

            foreach (string NodePropertyName in PropertyNames)
            {
                Type /*ChildInterfaceType,*/ ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(node, NodePropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, NodePropertyName, out Node ChildNode);
                    if (!Walk(ChildNode, node, NodePropertyName, callbacks, data))
                        return false;
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, NodePropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, NodePropertyName, out bool IsAssigned, out Node ChildNode);
                    if (IsAssigned)
                    {
                        Debug.Assert(ChildNode != null);

                        if (!Walk(ChildNode, node, NodePropertyName, callbacks, data))
                            return false;
                    }
                }
                else if (NodeTreeHelperList.IsNodeListProperty(node, NodePropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, NodePropertyName, out IReadOnlyList<Node> ChildNodeList);
                    Debug.Assert(ChildNodeList != null);

                    if (callbacks.HandlerList != null && !callbacks.HandlerList(node, NodePropertyName, ChildNodeList, callbacks, data))
                        return false;

                    if (callbacks.IsRecursive || callbacks.HandlerList == null)
                    {
                        for (int Index = 0; Index < ChildNodeList.Count; Index++)
                        {
                            Node ChildNode = ChildNodeList[Index];
                            if (!Walk(ChildNode, node, NodePropertyName, callbacks, data))
                                return false;
                        }
                    }
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, NodePropertyName, /*out ChildInterfaceType,*/ out ChildNodeType))
                {
                    IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(node, NodePropertyName);
                    Debug.Assert(BlockList.NodeBlockList != null);

                    if (callbacks.HandlerBlockList != null && !callbacks.HandlerBlockList(node, NodePropertyName, BlockList, callbacks, data))
                        return false;

                    if (callbacks.IsRecursive || callbacks.HandlerBlockList == null)
                    {
                        string Key = callbacks.BlockSubstitution.Key;

                        if (Key != null)
                        {
                            string Value = callbacks.BlockSubstitution.Value;
                            Debug.Assert(Value != null);

                            string ListPropertyName = NodePropertyName.Replace(Key, Value);
                            Debug.Assert(ListPropertyName != NodePropertyName);

                            PropertyInfo Property = node.GetType().GetProperty(ListPropertyName);
                            Debug.Assert(Property != null);

                            IList NodeList = Property.GetValue(node) as IList;
                            Debug.Assert(NodeList != null);

                            for (int Index = 0; Index < NodeList.Count; Index++)
                            {
                                Node ChildNode = NodeList[Index] as Node;
                                Debug.Assert(ChildNode != null);

                                if (!Walk(ChildNode, node, NodePropertyName, callbacks, data))
                                    return false;
                            }
                        }
                        else
                        {
                            for (int BlockIndex = 0; BlockIndex < BlockList.NodeBlockList.Count; BlockIndex++)
                            {
                                IBlock Block = BlockList.NodeBlockList[BlockIndex] as IBlock;
                                Debug.Assert(Block.NodeList != null);

                                if (callbacks.HandlerBlock != null && !callbacks.HandlerBlock(node, NodePropertyName, BlockList, Block, callbacks, data))
                                    return false;

                                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                                {
                                    Node ChildNode = Block.NodeList[Index] as Node;
                                    if (!Walk(ChildNode, node, NodePropertyName, callbacks, data))
                                        return false;
                                }
                            }
                        }
                    }
                }
                else if (NodeTreeHelper.IsBooleanProperty(node, NodePropertyName))
                {
                    if (callbacks.HandlerEnum != null && !callbacks.HandlerEnum(node, NodePropertyName, data))
                        return false;
                }
                else if (NodeTreeHelper.IsEnumProperty(node, NodePropertyName))
                {
                    if (callbacks.HandlerEnum != null && !callbacks.HandlerEnum(node, NodePropertyName, data))
                        return false;
                }
                else if (NodeTreeHelper.IsStringProperty(node, NodePropertyName))
                {
                    if (callbacks.HandlerString != null && !callbacks.HandlerString(node, NodePropertyName, data))
                        return false;
                }
                else if (NodeTreeHelper.IsGuidProperty(node, NodePropertyName))
                {
                    if (callbacks.HandlerGuid != null && !callbacks.HandlerGuid(node, NodePropertyName, data))
                        return false;
                }
                else if (NodeTreeHelper.IsDocumentProperty(node, NodePropertyName))
                {
                }
            }

            return true;
        }
    }
}
