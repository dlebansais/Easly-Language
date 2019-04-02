using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;

namespace BaseNodeHelper
{
    public static class NodeTreeWalk<TContext>
        where TContext : class
    {
        public static bool Walk(INode root, IWalkCallbacks<TContext> callbacks, TContext data)
        {
            return Walk(root, null, null, callbacks, data);
        }

        private static bool Walk(INode node, INode parentNode, string propertyName, IWalkCallbacks<TContext> callbacks, TContext data)
        {
            Debug.Assert((parentNode == null && propertyName == null) || (parentNode != null && propertyName != null));

            if (callbacks.HandlerNode != null && !callbacks.HandlerNode(node, parentNode, propertyName, data))
                return false;

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out INode ChildNode);
                    if (!Walk(ChildNode, node, PropertyName, callbacks, data))
                        return false;
                }

                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out INode ChildNode);

                    if (ChildNode != null && !Walk(ChildNode, node, PropertyName, callbacks, data))
                        return false;
                }

                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<INode> ChildNodeList);
                    Debug.Assert(ChildNodeList != null);

                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                    {
                        INode ChildNode = ChildNodeList[Index];
                        if (!Walk(ChildNode, node, PropertyName, callbacks, data))
                            return false;
                    }
                }

                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, PropertyName, out ChildInterfaceType, out ChildNodeType))
                {
                    IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(node, PropertyName);
                    Debug.Assert(BlockList.NodeBlockList != null);

                    for (int BlockIndex = 0; BlockIndex < BlockList.NodeBlockList.Count; BlockIndex++)
                    {
                        IBlock Block = BlockList.NodeBlockList[BlockIndex] as IBlock;
                        Debug.Assert(Block.NodeList != null);

                        for (int Index = 0; Index < Block.NodeList.Count; Index++)
                        {
                            INode ChildNode = Block.NodeList[Index] as INode;
                            if (!Walk(ChildNode, node, PropertyName, callbacks, data))
                                return false;
                        }
                    }
                }

                else if (NodeTreeHelper.IsBooleanProperty(node, PropertyName))
                {
                    if (callbacks.HandlerEnum != null && !callbacks.HandlerEnum(node, PropertyName, data))
                        return false;
                }

                else if (NodeTreeHelper.IsEnumProperty(node, PropertyName))
                {
                    if (callbacks.HandlerEnum != null && !callbacks.HandlerEnum(node, PropertyName, data))
                        return false;
                }

                else if (NodeTreeHelper.IsStringProperty(node, PropertyName))
                {
                    if (callbacks.HandlerString != null && !callbacks.HandlerString(node, PropertyName, data))
                        return false;
                }

                else if (NodeTreeHelper.IsGuidProperty(node, PropertyName))
                {
                    if (callbacks.HandlerGuid != null && !callbacks.HandlerGuid(node, PropertyName, data))
                        return false;
                }

                else if (NodeTreeHelper.IsDocumentProperty(node, PropertyName))
                {
                }
            }

            return true;
        }
    }
}
