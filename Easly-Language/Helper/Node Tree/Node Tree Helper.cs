using BaseNode;
using Easly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace BaseNodeHelper
{
    public static class NodeTreeHelper
    {
        public static readonly string ReplicationPatternPropertyName = "ReplicationPattern";
        public static readonly string SourceIdentifierPropertyName = "SourceIdentifier";

        public static bool IsChildNodeProperty(INode ParentNode, string PropertyName)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (PropertyType.GetInterface(typeof(INode).Name) == null)
                return false;

            return true;
        }

        public static bool IsChildNode(INode ParentNode, string PropertyName, INode Node)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(Node != null);

            Type ParentNodeType = ParentNode.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(PropertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (PropertyType.GetInterface(typeof(INode).Name) == null)
                return false;

            if (Property.GetValue(ParentNode) != Node)
                return false;

            return true;
        }

        public static bool IsChildNode(IBlock ChildBlock, int Index, INode ChildNode)
        {
            Debug.Assert(ChildBlock != null);
            Debug.Assert(ChildNode != null);

            IList NodeList = (IList)ChildBlock.GetType().GetProperty("NodeList").GetValue(ChildBlock);
            if (NodeList == null)
                return false;

            if (Index < 0 || Index >= NodeList.Count)
                return false;

            if (NodeList[Index] != ChildNode)
                return false;

            return true;
        }

        public static bool IsOptionalChildNodeProperty(INode ParentNode, string PropertyName)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;

            if (PropertyType.GetGenericTypeDefinition() != typeof(OptionalReference<>))
                return false;

            return true;
        }

        public static bool IsOptionalChildNode(INode ParentNode, string PropertyName, INode Node)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(Node != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;

            if (PropertyType.GetGenericTypeDefinition() != typeof(OptionalReference<>))
                return false;

            IOptionalReference Optional = Property.GetValue(ParentNode) as IOptionalReference;
            if (Optional == null)
                return false;

            INode NodeItem = Optional.AnyItem as INode;
            if (NodeItem != Node)
                return false;

            return true;
        }

        public static void GetChildNode(INode Node, string PropertyName, out bool IsAssigned, out INode ChildNode)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            if (Property == null)
            {
            }
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            if (PropertyType.IsGenericType)
            {
                Debug.Assert(PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

                IOptionalReference Optional = Property.GetValue(Node) as IOptionalReference;

                Debug.Assert(Optional != null);

                IsAssigned = Optional.IsAssigned;

                if (IsAssigned)
                {
                    ChildNode = Optional.AnyItem as INode;
                    Debug.Assert(ChildNode != null);
                }
                else
                {
                    Type[] GenericArguments = PropertyType.GetGenericArguments();
                    Debug.Assert(GenericArguments != null && GenericArguments.Length == 1);
                    Type GenericArgument = GenericArguments[0];
                    Debug.Assert(GenericArgument != null);

                    ChildNode = NodeHelper.CreateDefault(GenericArgument);
                    Optional.Hack(ChildNode);
                    Optional.Unassign();
                }
            }
            else
            {
                IsAssigned = true;
                ChildNode = Property.GetValue(Node) as INode;
                Debug.Assert(ChildNode != null);
            }
        }

        public static void GetChildNode(IBlock Block, string PropertyName, out INode ChildNode)
        {
            Debug.Assert(Block != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Block.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            ChildNode = Property.GetValue(Block) as INode;
            Debug.Assert(ChildNode != null);
        }

        public static void SetOptionalChildNode(INode Node, string PropertyName, INode ChildNode)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ChildNode != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            IOptionalReference Optional = Property.GetValue(Node) as IOptionalReference;

            Debug.Assert(Optional != null);

            PropertyInfo ItemProperty = Optional.GetType().GetProperty("Item");
            ItemProperty.SetValue(Optional, ChildNode);
            Optional.Assign();
        }

        public static bool IsChildNodeAssigned(INode Node, string PropertyName)
        {
            IOptionalReference Optional;
            GetOptionalChildNode(Node, PropertyName, out Optional);

            return Optional.IsAssigned;
        }

        public static void AssignChildNode(INode Node, string PropertyName)
        {
            IOptionalReference Optional;
            GetOptionalChildNode(Node, PropertyName, out Optional);
            
            Optional.Assign();
        }

        public static void UnassignChildNode(INode Node, string PropertyName)
        {
            IOptionalReference Optional;
            GetOptionalChildNode(Node, PropertyName, out Optional);

            Optional.Unassign();
        }

        private static void GetOptionalChildNode(INode Node, string PropertyName, out IOptionalReference Optional)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            Optional = Property.GetValue(Node) as IOptionalReference;

            Debug.Assert(Optional != null);

            PropertyInfo ItemProperty = Optional.GetType().GetProperty("Item");
            INode ChildNode = (INode)ItemProperty.GetValue(Optional);

            Debug.Assert(ChildNode != null);
        }

        public static void ReplaceChildNode(INode Node, string PropertyName, INode ChildNode)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ChildNode != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type ChildNodeType = ChildNode.GetType();
            Debug.Assert(ChildNodeType.GetInterface(Property.PropertyType.FullName) != null);

            Property.SetValue(Node, ChildNode);
        }

        public static bool IsChildNodeList(INode Node, string PropertyName, out Type ChildNodeType)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            if (PropertyType.IsGenericType)
            {
                Type CollectionType = PropertyType.GetGenericTypeDefinition();

                if (CollectionType == typeof(IList<>))
                {
                    Type[] Generics = PropertyType.GetGenericArguments();

                    Debug.Assert(Generics.Length == 1);

                    ChildNodeType = Generics[0];
                    return true;
                }
            }

            ChildNodeType = null;
            return false;
        }

        public static bool GetChildNodeList(INode Node, string PropertyName, out IReadOnlyList<INode> ChildNodeList)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(PropertyType.IsGenericType);

            Type ListType = PropertyType.GetGenericTypeDefinition();

            Debug.Assert(ListType == typeof(IList<>) || ListType == typeof(IBlockList<,>));

            object Child = Property.GetValue(Node);

            Debug.Assert(Child != null);

            IList AsList;
            if ((AsList = Child as IList) != null)
            {
                List<INode> NodeList = new List<INode>();

                foreach (object Item in AsList)
                {
                    INode ChildNode = Item as INode;

                    Debug.Assert(ChildNode != null);

                    NodeList.Add(ChildNode);
                }

                ChildNodeList = NodeList.AsReadOnly();
                return true;
            }

            ChildNodeList = null;
            return false;
        }

        public static bool IsListChildNode(INode ParentNode, string PropertyName, int Index, INode ChildNode)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ChildNode != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;

            if (!PropertyType.IsGenericType)
                return false;

            Type ListType = PropertyType.GetGenericTypeDefinition();
            if (ListType != typeof(IList<>))
                return false;

            IList AsList = (IList)Property.GetValue(ParentNode);
            if (AsList == null)
                return false;

            if (Index < 0 || Index >= AsList.Count)
                return false;

            if (AsList[Index] != ChildNode)
                return false;

            return true;
        }

        public static bool IsChildBlockList(INode Node, string PropertyName, out Type ChildInterfaceType, out Type ChildNodeType)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            if (PropertyType.IsGenericType)
            {
                Type CollectionType = PropertyType.GetGenericTypeDefinition();

                if (CollectionType == typeof(IBlockList<,>))
                {
                    Type[] Generics = PropertyType.GetGenericArguments();

                    Debug.Assert(Generics.Length == 2);

                    ChildInterfaceType = Generics[0];
                    ChildNodeType = Generics[1];
                    return true;
                }
            }

            ChildInterfaceType = null;
            ChildNodeType = null;
            return false;
        }

        public static bool GetChildBlockList(INode Node, string PropertyName, out IReadOnlyList<INodeTreeBlock> ChildBlockList)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type ListType = Property.PropertyType.GetGenericTypeDefinition();

            Debug.Assert(ListType == typeof(IList<>) || ListType == typeof(IBlockList<,>));

            object Child = Property.GetValue(Node);

            Debug.Assert(Child != null);

            IBlockList AsBlockList;
            if ((AsBlockList = Child as IBlockList) != null)
            {
                IList NodeBlockList = AsBlockList.GetType().GetProperty("NodeBlockList").GetValue(AsBlockList) as IList;

                Debug.Assert(NodeBlockList != null);

                List<INodeTreeBlock> BlockList = new List<INodeTreeBlock>();
                List<IBlock> ToRemove = new List<IBlock>();

                foreach (IBlock ChildBlock in NodeBlockList)
                {
                    List<INode> ChildNodeList = new List<INode>();

                    IPattern ReplicationPattern = (IPattern)ChildBlock.GetType().GetProperty("ReplicationPattern").GetValue(ChildBlock, null);
                    IIdentifier SourceIdentifier = (IIdentifier)ChildBlock.GetType().GetProperty("SourceIdentifier").GetValue(ChildBlock, null);
                    IList NodeList = (IList)ChildBlock.GetType().GetProperty("NodeList").GetValue(ChildBlock);

                    Debug.Assert(ReplicationPattern != null);
                    Debug.Assert(SourceIdentifier != null);
                    Debug.Assert(NodeList != null);

                    if (NodeList.Count >= 0)
                    {
                        Debug.Assert(NodeList.Count > 0);

                        foreach (INode ChildNode in NodeList)
                            ChildNodeList.Add(ChildNode);

                        BlockList.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ChildNodeList));
                    }
                    else
                        ToRemove.Add(ChildBlock);
                }

                foreach (IBlock ChildBlock in ToRemove)
                    NodeBlockList.Remove(ChildBlock);

                ChildBlockList = BlockList.AsReadOnly();
                return true;
            }

            ChildBlockList = null;
            return false;
        }

        public static bool IsTextNode(INode Node)
        {
            Debug.Assert(Node != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty("Text");

            return (Property != null);
        }

        public static string GetText(INode Node)
        {
            Debug.Assert(Node != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty("Text");

            Debug.Assert(Property != null);

            string Text = Property.GetValue(Node) as string;

            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetText(INode Node, string Text)
        {
            Debug.Assert(Node != null);
            Debug.Assert(Text != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty("Text");

            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType == typeof(string));

            Property.SetValue(Node, Text);
        }

        private static void GetEnumMinMax(PropertyInfo Property, out int Min, out int Max)
        {
            Array Values = Property.PropertyType.GetEnumValues();

            Max = int.MinValue;
            Min = int.MaxValue;
            foreach (int Value in Values)
            {
                if (Max < Value)
                    Max = Value;
                if (Min > Value)
                    Min = Value;
            }
        }

        public static int GetEnumValue(INode Node, string PropertyName)
        {
            Debug.Assert(Node != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            int Min, Max;
            int Result;

            if (PropertyType.IsEnum)
            {
                GetEnumMinMax(Property, out Min, out Max);
                Result = (int)Property.GetValue(Node);
            }
            else
            {
                Min = 0;
                Max = 1;
                bool BoolValue = (bool)Property.GetValue(Node);
                Result = BoolValue ? 1 : 0;
            }

            Debug.Assert(Min <= Result && Result <= Max);

            return Result;
        }

        public static void SetEnumValue(INode Node, string PropertyName, int Value)
        {
            Debug.Assert(Node != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            int Min, Max;

            if (PropertyType.IsEnum)
            {
                GetEnumMinMax(Property, out Min, out Max);

                Debug.Assert(Min <= Value && Value <= Max);

                Property.SetValue(Node, Value);
            }
            else
            {
                Debug.Assert(Value == 0 || Value == 1);

                Property.SetValue(Node, Value == 1 ? true : false);
            }
        }

        public static string GetCommentText(INode Node)
        {
            Debug.Assert(Node != null);

            string Text = Node.Documentation.Comment;

            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetCommentText(INode Node, string Text)
        {
            Debug.Assert(Node != null);
            Debug.Assert(Text != null);

            Node.Documentation.Comment = Text;
        }

        public static void InsertIntoList(INode Node, string PropertyName, int Index, INode ChildNode)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ChildNode != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type GenericType = Property.PropertyType.GetGenericTypeDefinition();
            Debug.Assert(GenericType == typeof(IList<>));

            IList NodeList = Property.GetValue(Node) as IList;

            Debug.Assert(NodeList != null);
            Debug.Assert(Index >= 0 && Index <= NodeList.Count);

            NodeList.Insert(Index, ChildNode);
        }

        public static void RemoveFromList(INode Node, string PropertyName, int Index)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type GenericType = Property.PropertyType.GetGenericTypeDefinition();
            Debug.Assert(GenericType == typeof(IList<>));

            IList NodeList = Property.GetValue(Node) as IList;

            Debug.Assert(NodeList != null);
            Debug.Assert(Index >= 0 && Index < NodeList.Count);

            NodeList.RemoveAt(Index);
        }

        public static Type ChildInterfaceType(INode Node, string PropertyName)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            Debug.Assert(Property != null);

            Type InterfaceType = Property.PropertyType;
            Debug.Assert(!InterfaceType.IsGenericType);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type OptionalChildInterfaceType(INode Node, string PropertyName)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            Type[] OptionalGenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(OptionalGenericArguments != null);
            Debug.Assert(OptionalGenericArguments.Length == 1);

            Type InterfaceType = OptionalGenericArguments[0];
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type BlockListInterfaceType(INode Node, string PropertyName)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;
            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);
            Debug.Assert(NodeBlockList != null);

            Type BlockListType = NodeBlockList.GetType();
            Debug.Assert(BlockListType.IsGenericType);

            Type[] BlockListGenericArguments = BlockListType.GetGenericArguments();
            Debug.Assert(BlockListGenericArguments != null);
            Debug.Assert(BlockListGenericArguments.Length == 1);

            Type BlockGenericArgument = BlockListGenericArguments[0];
            Debug.Assert(BlockGenericArgument.IsGenericType);

            Type[] GenericArguments = BlockGenericArgument.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type ListInterfaceType(INode Node, string PropertyName)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);

            Type GenericType = Property.PropertyType.GetGenericTypeDefinition();
            Debug.Assert(GenericType == typeof(IList<>));

            Type[] GenericArguments = Property.PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static bool IsBlockChildNode(INode ParentNode, string PropertyName, int BlockIndex, int Index, INode ChildNode)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ChildNode != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(ParentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (BlockIndex < 0 || BlockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[BlockIndex];
            if (ChildBlock == null)
                return false;

            return IsChildNode(ChildBlock, Index, ChildNode);
        }

        public static void GetChildBlock(INode Node, string PropertyName, int BlockIndex, out IBlock ChildBlock)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex < NodeBlockList.Count);

            ChildBlock = (IBlock)NodeBlockList[BlockIndex];
        }

        public static bool IsBlockPatternNode(INode ParentNode, string PropertyName, int BlockIndex, IPattern ReplicationPattern)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ReplicationPattern != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(ParentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (BlockIndex < 0 || BlockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[BlockIndex];
            if (ChildBlock == null)
                return false;

            return IsPatternNode(ChildBlock, ReplicationPattern);
        }

        public static bool IsPatternNode(IBlock Block, IPattern ReplicationPattern)
        {
            Debug.Assert(Block != null);
            Debug.Assert(ReplicationPattern != null);

            Type BlockType = Block.GetType();
            PropertyInfo Property = BlockType.GetProperty("ReplicationPattern");

            Debug.Assert(Property != null);
            return ReplicationPattern == Property.GetValue(Block) as IPattern;
        }

        public static string GetPattern(IBlock Block)
        {
            Debug.Assert(Block != null);

            Type BlockType = Block.GetType();
            PropertyInfo Property = BlockType.GetProperty("ReplicationPattern");

            Debug.Assert(Property != null);

            IPattern ReplicationPattern = Property.GetValue(Block) as IPattern;

            Debug.Assert(ReplicationPattern != null);

            return GetText(ReplicationPattern);
        }

        public static void SetPattern(IBlock Block, string Text)
        {
            Debug.Assert(Block != null);
            Debug.Assert(Text != null);

            Type BlockType = Block.GetType();
            PropertyInfo Property = BlockType.GetProperty("ReplicationPattern");

            Debug.Assert(Property != null);

            IPattern ReplicationPattern = Property.GetValue(Block) as IPattern;

            Debug.Assert(ReplicationPattern != null);

            SetText(ReplicationPattern, Text);
        }

        public static bool IsBlockSourceNode(INode ParentNode, string PropertyName, int BlockIndex, IIdentifier SourceIdentifier)
        {
            Debug.Assert(ParentNode != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(SourceIdentifier != null);

            Type NodeType = ParentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(ParentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (BlockIndex < 0 || BlockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[BlockIndex];
            if (ChildBlock == null)
                return false;

            return IsSourceNode(ChildBlock, SourceIdentifier);
        }

        public static bool IsSourceNode(IBlock Block, IIdentifier SourceIdentifier)
        {
            Debug.Assert(Block != null);
            Debug.Assert(SourceIdentifier != null);

            Type BlockType = Block.GetType();
            PropertyInfo Property = BlockType.GetProperty("SourceIdentifier");

            Debug.Assert(Property != null);
            return SourceIdentifier == Property.GetValue(Block) as IIdentifier;
        }

        public static string GetSource(IBlock Block)
        {
            Debug.Assert(Block != null);

            Type BlockType = Block.GetType();
            PropertyInfo Property = BlockType.GetProperty("SourceIdentifier");

            Debug.Assert(Property != null);

            IIdentifier SourceIdentifier = Property.GetValue(Block) as IIdentifier;

            Debug.Assert(SourceIdentifier != null);

            return GetText(SourceIdentifier);
        }

        public static void SetSource(IBlock Block, string Text)
        {
            Debug.Assert(Block != null);
            Debug.Assert(Text != null);

            Type BlockType = Block.GetType();
            PropertyInfo Property = BlockType.GetProperty("SourceIdentifier");

            Debug.Assert(Property != null);

            IIdentifier SourceIdentifier = Property.GetValue(Block) as IIdentifier;

            Debug.Assert(SourceIdentifier != null);

            SetText(SourceIdentifier, Text);
        }

        public static void SetReplication(IBlock Block, ReplicationStatus Replication)
        {
            Block.GetType().GetProperty("Replication").SetValue(Block, Replication);
        }

        public static void InsertIntoBlock(INode Node, string PropertyName, int BlockIndex, int Index, INode ChildNode)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ChildNode != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex < NodeBlockList.Count);

            IBlock ChildBlock = (IBlock)NodeBlockList[BlockIndex];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty("NodeList").GetValue(ChildBlock);

            Debug.Assert(Index >= 0 && Index <= NodeList.Count);

            NodeList.Insert(Index, ChildNode);
        }

        public static void RemoveFromBlock(INode Node, string PropertyName, int BlockIndex, int Index, out bool IsBlockRemoved)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex < NodeBlockList.Count);

            IBlock ChildBlock = (IBlock)NodeBlockList[BlockIndex];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty("NodeList").GetValue(ChildBlock);

            Debug.Assert(Index >= 0 && Index < NodeList.Count);

            NodeList.RemoveAt(Index);

            if (NodeList.Count == 0)
            {
                NodeBlockList.RemoveAt(BlockIndex);
                IsBlockRemoved = true;
            }
            else
                IsBlockRemoved = false;
        }

        public static void ReplaceInBlock(INode Node, string PropertyName, int BlockIndex, int Index, INode NewChildNode)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex < NodeBlockList.Count);

            IBlock ChildBlock = (IBlock)NodeBlockList[BlockIndex];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty("NodeList").GetValue(ChildBlock);

            Debug.Assert(Index >= 0 && Index < NodeList.Count);

            NodeList[Index] = NewChildNode;
        }

        public static bool IsBlockListEmpty(IBlockList BlockList)
        {
            Debug.Assert(BlockList != null);

            IList NodeBlockList = (IList)BlockList.GetType().GetProperty("NodeBlockList").GetValue(BlockList);

            Debug.Assert(NodeBlockList != null);

            return (NodeBlockList.Count == 0);
        }

        public static bool IsBlockListSingle(IBlockList BlockList)
        {
            Debug.Assert(BlockList != null);

            IList NodeBlockList = (IList)BlockList.GetType().GetProperty("NodeBlockList").GetValue(BlockList);

            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList.Count == 0)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[0];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty("NodeList").GetValue(ChildBlock);

            Debug.Assert(NodeList.Count > 0);

            return NodeList.Count == 1;
        }

        public static void InsertIntoBlockList(INode Node, string PropertyName, int BlockIndex, ReplicationStatus Replication, IPattern ReplicationPattern, IIdentifier SourceIdentifier, out IBlock ChildBlock)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);
            Debug.Assert(ReplicationPattern != null);
            Debug.Assert(SourceIdentifier != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex <= NodeBlockList.Count);

            ChildBlock = CreateBlock(Property.PropertyType, Replication, ReplicationPattern, SourceIdentifier);
            NodeBlockList.Insert(BlockIndex, ChildBlock);
        }

        private static IBlock CreateBlock(Type PropertyType, ReplicationStatus Replication, IPattern ReplicationPattern, IIdentifier SourceIdentifier)
        {
            Type[] TypeArguments = PropertyType.GetGenericArguments();

            Type BlockType = typeof(Block<,>).MakeGenericType(TypeArguments);
            IBlock NewBlock = (IBlock)BlockType.Assembly.CreateInstance(BlockType.FullName);

            Document EmptyComment = new Document();
            EmptyComment.Comment = "";
            NewBlock.GetType().GetProperty("Documentation").SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });
            IList NewNodeList = (IList)NodeListType.Assembly.CreateInstance(NodeListType.FullName);
            NewBlock.GetType().GetProperty("NodeList").SetValue(NewBlock, NewNodeList);

            NewBlock.GetType().GetProperty("Replication").SetValue(NewBlock, Replication);
            NewBlock.GetType().GetProperty("ReplicationPattern").SetValue(NewBlock, ReplicationPattern);
            NewBlock.GetType().GetProperty("SourceIdentifier").SetValue(NewBlock, SourceIdentifier);

            return NewBlock;
        }

        public static void RemoveFromBlockList(INode Node, string PropertyName, int BlockIndex)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex < NodeBlockList.Count);

            NodeBlockList.RemoveAt(BlockIndex);
        }

        public static void SplitBlock(INode Node, string PropertyName, int BlockIndex, int Index, ReplicationStatus Replication, IPattern ReplicationPattern, IIdentifier SourceIdentifier)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex >= 0 && BlockIndex < NodeBlockList.Count);

            IBlock CurrentBlock = (IBlock)NodeBlockList[BlockIndex];
            IList CurrentNodeList = (IList)CurrentBlock.GetType().GetProperty("NodeList").GetValue(CurrentBlock);

            IBlock NewChildBlock = CreateBlock(Property.PropertyType, Replication, ReplicationPattern, SourceIdentifier);
            IList NewNodeList = (IList)NewChildBlock.GetType().GetProperty("NodeList").GetValue(NewChildBlock);
            NodeBlockList.Insert(BlockIndex, NewChildBlock);

            Debug.Assert(CurrentNodeList.Count > 1);
            Debug.Assert(Index > 0 && Index < CurrentNodeList.Count);

            for (int i = 0; i < Index; i++)
            {
                INode ChildNode = (INode)CurrentNodeList[0];

                CurrentNodeList.RemoveAt(0);
                NewNodeList.Insert(i, ChildNode);
            }

            Debug.Assert(CurrentNodeList.Count > 0);
            Debug.Assert(NewNodeList.Count > 0);
        }

        public static void JoinBlocks(INode Node, string PropertyName, int BlockIndex)
        {
            Debug.Assert(Node != null);
            Debug.Assert(PropertyName != null);

            Type NodeType = Node.GetType();
            PropertyInfo Property = NodeType.GetProperty(PropertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(Node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty("NodeBlockList").GetValue(ItemBlockList);

            Debug.Assert(BlockIndex > 0 && BlockIndex < NodeBlockList.Count);

            IBlock MergedBlock = (IBlock)NodeBlockList[BlockIndex - 1];
            IBlock CurrentBlock = (IBlock)NodeBlockList[BlockIndex];
            IList MergedNodeList = (IList)MergedBlock.GetType().GetProperty("NodeList").GetValue(MergedBlock);
            IList CurrentNodeList = (IList)CurrentBlock.GetType().GetProperty("NodeList").GetValue(CurrentBlock);

            for (int i = 0; i < MergedNodeList.Count; i++)
            {
                INode ChildNode = (INode)MergedNodeList[i];
                CurrentNodeList.Insert(i, ChildNode);
            }

            NodeBlockList.RemoveAt(BlockIndex - 1);
        }

        public static void GetOptionalNodes(INode Node, out Dictionary<string, IOptionalReference> OptionalNodesTable)
        {
            Debug.Assert(Node != null);

            OptionalNodesTable = new Dictionary<string, IOptionalReference>();

            Type NodeType = Node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>))
                {
                    IOptionalReference Optional = Property.GetValue(Node) as IOptionalReference;

                    Debug.Assert(Optional != null);

                    OptionalNodesTable.Add(PropertyName, Optional);
                }
            }
        }

        public static void GetArgumentBlocks(INode Node, out Dictionary<string, IBlockList<IArgument, Argument>> ArgumentBlocksTable)
        {
            Debug.Assert(Node != null);

            ArgumentBlocksTable = new Dictionary<string, IBlockList<IArgument, Argument>>();

            Type NodeType = Node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(IBlockList<,>))
                {
                    Type[] GenericArguments = PropertyType.GetGenericArguments();

                    if (GenericArguments.Length > 1 && GenericArguments[0] == typeof(IArgument))
                    {
                        IBlockList<IArgument, Argument> ArgumentBlocks = Property.GetValue(Node) as IBlockList<IArgument, Argument>;

                        Debug.Assert(ArgumentBlocks != null);

                        ArgumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                    }
                }
            }
        }

        public static void MoveNode(IBlock Block, int Index, int Direction)
        {
            Debug.Assert(Block != null);

            IList NodeList = (IList)Block.GetType().GetProperty("NodeList").GetValue(Block);
            Debug.Assert(NodeList != null);

            Debug.Assert(Index >= 0 && Index < NodeList.Count);
            Debug.Assert(Index + Direction >= 0 && Index + Direction < NodeList.Count);

            INode Node1 = (INode)NodeList[Index];
            INode Node2 = (INode)NodeList[Index + Direction];

            NodeList[Index] = Node2;
            NodeList[Index + Direction] = Node1;
        }
    }
}
