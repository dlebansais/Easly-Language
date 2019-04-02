using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;

namespace BaseNodeHelper
{
    public static class NodeTreeWalk
    {
        public static bool Walk(INode root, Func<INode, INode, string, IClass, bool> handlerNode, Func<INode, string, Type, bool> handlerProperty)
        {
            return Walk(root, null, null, null, handlerNode, handlerProperty);
        }

        private static bool Walk(INode node, INode parentNode, string propertyName, IClass parentClass, Func<INode, INode, string, IClass, bool> handlerNode, Func<INode, string, Type, bool> handlerProperty)
        {
            Debug.Assert((parentNode == null && propertyName == null) || (parentNode != null && propertyName != null));

            if (!handlerNode(node, parentNode, propertyName, parentClass))
                return false;

            if (node is IClass AsClass)
            {
                Debug.Assert(parentClass == null);
                parentClass = AsClass;
            }

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out INode ChildNode);
                    if (!Walk(ChildNode, node, PropertyName, parentClass, handlerNode, handlerProperty))
                        return false;
                }

                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out INode ChildNode);

                    if (ChildNode != null && !Walk(ChildNode, node, PropertyName, parentClass, handlerNode, handlerProperty))
                        return false;
                }

                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<INode> ChildNodeList);
                    Debug.Assert(ChildNodeList != null);

                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                    {
                        INode ChildNode = ChildNodeList[Index];
                        if (!Walk(ChildNode, node, PropertyName, parentClass, handlerNode, handlerProperty))
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
                            if (!Walk(ChildNode, node, PropertyName, parentClass, handlerNode, handlerProperty))
                                return false;
                        }
                    }
                }

                else if (NodeTreeHelper.IsBooleanProperty(node, PropertyName))
                {
                    if (!handlerProperty(node, PropertyName, typeof(bool)))
                        return false;
                }

                else if (NodeTreeHelper.IsEnumProperty(node, PropertyName))
                {
                    if (!handlerProperty(node, PropertyName, typeof(Enum)))
                        return false;
                }

                else if (NodeTreeHelper.IsStringProperty(node, PropertyName))
                {
                    if (!handlerProperty(node, PropertyName, typeof(string)))
                        return false;
                }

                else if (NodeTreeHelper.IsGuidProperty(node, PropertyName))
                {
                    if (!handlerProperty(node, PropertyName, typeof(Guid)))
                        return false;
                }

                else if (NodeTreeHelper.IsDocumentProperty(node, PropertyName))
                {
                    if (!handlerProperty(node, PropertyName, typeof(Document)))
                        return false;
                }
            }

            return true;
        }
    }
}
