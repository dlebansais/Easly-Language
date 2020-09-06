namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeHelper
    {
        public static Type NodeType(string typeName)
        {
            string RootName = typeof(Root).FullName;
            int Index = RootName.LastIndexOf('.');
            string FullTypeName = RootName.Substring(0, Index + 1) + typeName;
            return typeof(Root).Assembly.GetType(FullTypeName);
        }

        public static bool IsOptionalAssignedToDefault(IOptionalReference optional)
        {
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            if (!optional.IsAssigned)
                return false;

            INode Node = optional.Item as INode;
            Debug.Assert(Node != null);

            return IsDefaultNode(Node);
        }

        public static bool IsDefaultNode(INode node)
        {
            IList<IIdentifier> Path;

            switch (node)
            {
                case IName AsName:
                    return AsName.Text.Length == 0;

                case IIdentifier AsIdentifier:
                    return AsIdentifier.Text.Length == 0;

                case IScope AsScope:
                    return AsScope.EntityDeclarationBlocks.NodeBlockList.Count == 0 && AsScope.InstructionBlocks.NodeBlockList.Count == 0;

                case IQualifiedName AsQualifiedName:
                    Path = AsQualifiedName.Path; // Debug.Assert(Path.Count > 0);
                    return Path.Count == 1 && Path[0].Text.Length == 0;

                case ISimpleType AsSimpleType:
                    return AsSimpleType.Sharing == SharingType.NotShared && AsSimpleType.ClassIdentifier.Text.Length == 0;

                case IObjectType AsObjectType: // Fallback for other IObjectType.
                    return false;

                case IQueryExpression AsQueryExpression:
                    Path = AsQueryExpression.Query.Path; // Debug.Assert(Path.Count > 0);
                    return AsQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0 && Path.Count == 1 && Path[0].Text.Length == 0;

                case IManifestCharacterExpression AsManifestCharacterExpression:
                    return AsManifestCharacterExpression.Text.Length == 0;

                case IManifestNumberExpression AsManifestNumberExpression:
                    return AsManifestNumberExpression.Text.Length == 0;

                case IManifestStringExpression AsManifestStringExpression:
                    return AsManifestStringExpression.Text.Length == 0;

                case IExpression AsExpression: // Fallback for other IExpression.
                    return false;

                case IEffectiveBody AsEffectiveBody:
                    return AsEffectiveBody.RequireBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.EnsureBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.ExceptionIdentifierBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.EntityDeclarationBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.BodyInstructionBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.ExceptionHandlerBlocks.NodeBlockList.Count == 0;

                case IBody AsBody: // Fallback for other IBody.
                    return false;

                case IArgument AsArgument:
                    return IsDefaultArgument(node);

                default:
                    return IsEmptyNode(node);
            }
        }

        public static bool IsDefaultArgument(INode node)
        {
            if (node is IPositionalArgument AsPositional)
                if (AsPositional.Source is IQueryExpression AsQueryExpression)
                {
                    IList<IIdentifier> Path = AsQueryExpression.Query.Path;
                    if (Path.Count == 1 && Path[0].Text.Length == 0)
                    {
                        IBlockList ArgumentBlocks = AsQueryExpression.ArgumentBlocks as IBlockList;
                        Debug.Assert(ArgumentBlocks != null);

                        if (NodeTreeHelperBlockList.IsBlockListEmpty(ArgumentBlocks))
                            return true;
                    }
                }

            return false;
        }

        public static ulong NodeHash(INode node)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);
            Type ChildNodeType;
            ulong Hash = 0;

            foreach (string PropertyName in PropertyNames)
            {
                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out INode ChildNode);
                    MergeHash(ref Hash, NodeHash(ChildNode));
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out INode ChildNode);
                    MergeHash(ref Hash, IsAssigned ? 1UL : 0);
                    if (IsAssigned)
                        MergeHash(ref Hash, NodeHash(ChildNode));
                }
                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<INode> ChildNodeList);
                    foreach (INode ChildNode in ChildNodeList)
                        MergeHash(ref Hash, NodeHash(ChildNode));
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, PropertyName, out Type ChildInterfaceType, out ChildNodeType))
                {
                    NodeTreeHelperBlockList.GetChildBlockList(node, PropertyName, out IReadOnlyList<INodeTreeBlock> ChildBlockList);
                    for (int i = 0; i < ChildBlockList.Count; i++)
                    {
                        NodeTreeHelperBlockList.GetChildBlock(node, PropertyName, i, out IBlock ChildBlock);
                        IReadOnlyList<INode> NodeList = ChildBlockList[i].NodeList;

                        MergeHash(ref Hash, ValueHash(ChildBlock.Documentation.Comment));
                        MergeHash(ref Hash, ValueHash(ChildBlock.Documentation.Uuid));
                        MergeHash(ref Hash, ValueHash((int)ChildBlock.Replication));
                        MergeHash(ref Hash, NodeHash(ChildBlock.ReplicationPattern));
                        MergeHash(ref Hash, NodeHash(ChildBlock.SourceIdentifier));

                        foreach (INode ChildNode in NodeList)
                            MergeHash(ref Hash, NodeHash(ChildNode));
                    }
                }
                else
                {
                    Type NodeType = node.GetType();
                    PropertyInfo Info = NodeType.GetProperty(PropertyName);

                    if (Info.PropertyType == typeof(IDocument))
                    {
                        IDocument Documentation = Info.GetValue(node) as IDocument;
                        MergeHash(ref Hash, ValueHash(Documentation.Comment));
                        MergeHash(ref Hash, ValueHash(Documentation.Uuid));
                    }
                    else if (Info.PropertyType == typeof(bool))
                        MergeHash(ref Hash, ValueHash((bool)Info.GetValue(node)));
                    else if (Info.PropertyType.IsEnum)
                        MergeHash(ref Hash, ValueHash((int)Info.GetValue(node)));
                    else if (Info.PropertyType == typeof(string))
                        MergeHash(ref Hash, ValueHash((string)Info.GetValue(node)));
                    else if (Info.PropertyType == typeof(Guid))
                        MergeHash(ref Hash, ValueHash((Guid)Info.GetValue(node)));
                    else
                        throw new ArgumentOutOfRangeException($"{nameof(NodeType)}: {NodeType.FullName}");
                }
            }

            return Hash;
        }

        public static INode DeepCloneNode(INode root, bool cloneCommentGuid)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(root);

            // Careful, the resulting "empty" node can contain items for lists that are not allowed to be empty.
            INode ClonedRoot = CreateEmptyNode(root.GetType());

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(root, PropertyName, out INode ChildNode);

                    INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                    NodeTreeHelperChild.SetChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(root, PropertyName, out bool IsAssigned, out INode ChildNode);

                    if (ChildNode != null)
                    {
                        INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                        NodeTreeHelperOptional.SetOptionalChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                        if (!IsAssigned)
                            NodeTreeHelperOptional.UnassignChildNode(ClonedRoot, PropertyName);
                    }
                }
                else if (NodeTreeHelperList.IsNodeListProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(root, PropertyName, out IReadOnlyList<INode> ChildNodeList);
                    IList<INode> ClonedChildNodeList = DeepCloneNodeList(ChildNodeList, cloneCommentGuid);

                    NodeTreeHelperList.ClearChildNodeList(ClonedRoot, PropertyName);
                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                        NodeTreeHelperList.InsertIntoList(ClonedRoot, PropertyName, Index, ClonedChildNodeList[Index]);
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(root, PropertyName, out ChildInterfaceType, out ChildNodeType))
                {
                    IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(root, PropertyName);
                    IBlockList ClonedBlockList = DeepCloneBlockList(BlockList, cloneCommentGuid);
                    NodeTreeHelperBlockList.SetBlockList(ClonedRoot, PropertyName, ClonedBlockList);
                }
                else if (NodeTreeHelper.IsBooleanProperty(root, PropertyName))
                    NodeTreeHelper.CopyBooleanProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsEnumProperty(root, PropertyName))
                    NodeTreeHelper.CopyEnumProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsStringProperty(root, PropertyName))
                    NodeTreeHelper.CopyStringProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsGuidProperty(root, PropertyName))
                    NodeTreeHelper.CopyGuidProperty(root, ClonedRoot, PropertyName);
                else if (NodeTreeHelper.IsDocumentProperty(root, PropertyName))
                    NodeTreeHelper.CopyDocumentation(root, ClonedRoot, cloneCommentGuid);
                else
                    throw new ArgumentOutOfRangeException($"{nameof(PropertyName)}: {PropertyName}");
            }

            return ClonedRoot;
        }

        public static IList<INode> DeepCloneNodeList(IEnumerable<INode> rootList, bool cloneCommentGuid)
        {
            if (rootList == null) throw new ArgumentNullException(nameof(rootList));

            List<INode> Result = new List<INode>();

            foreach (INode ChildNode in rootList)
            {
                INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                Result.Add(ClonedChildNode);
            }

            return Result;
        }

        public static IList<IBlock> DeepCloneBlockList(IEnumerable<IBlock> rootBlockList, bool cloneCommentGuid)
        {
            if (rootBlockList == null) throw new ArgumentNullException(nameof(rootBlockList));

            IList<IBlock> ClonedNodeBlockList = new List<IBlock>();

            foreach (IBlock Block in rootBlockList)
            {
                Type BlockType = Block.GetType();
                Type[] GenericArguments = BlockType.GetGenericArguments();
                BlockType = typeof(Block<,>).MakeGenericType(GenericArguments);

                IPattern ClonedPattern = (IPattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
                IIdentifier ClonedSource = (IIdentifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
                IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockType, Block.Replication, ClonedPattern, ClonedSource);
                NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    INode ChildNode = Block.NodeList[Index] as INode;
                    INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                    NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
                }

                ClonedNodeBlockList.Add(ClonedBlock);
            }

            return ClonedNodeBlockList;
        }

        public static IBlockList DeepCloneBlockList(IBlockList rootBlockList, bool cloneCommentGuid)
        {
            if (rootBlockList == null) throw new ArgumentNullException(nameof(rootBlockList));

            Type BlockListType = rootBlockList.GetType();
            Type[] GenericArguments = BlockListType.GetGenericArguments();
            BlockListType = typeof(BlockList<,>).MakeGenericType(GenericArguments);

            IBlockList ClonedBlockList = (IBlockList)BlockListType.Assembly.CreateInstance(BlockListType.FullName);

            Type NodeListType = rootBlockList.NodeBlockList.GetType();
            IList ClonedNodeBlockList = (IList)NodeListType.Assembly.CreateInstance(NodeListType.FullName);

            PropertyInfo NodeBlockListProperty = BlockListType.GetProperty(nameof(IBlockList.NodeBlockList));
            NodeBlockListProperty.SetValue(ClonedBlockList, ClonedNodeBlockList);
            NodeTreeHelper.CopyDocumentation(rootBlockList, ClonedBlockList, cloneCommentGuid);

            for (int BlockIndex = 0; BlockIndex < rootBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock Block = rootBlockList.NodeBlockList[BlockIndex] as IBlock;

                IPattern ClonedPattern = (IPattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
                IIdentifier ClonedSource = (IIdentifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
                IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockListType, Block.Replication, ClonedPattern, ClonedSource);
                ClonedNodeBlockList.Add(ClonedBlock);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    INode ChildNode = Block.NodeList[Index] as INode;
                    INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                    NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
                }

                NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);
            }

            return ClonedBlockList;
        }

        public static IDictionary<Type, TValue> CreateNodeDictionary<TValue>()
        {
            IDictionary<Type, TValue> Result = new Dictionary<Type, TValue>();
            Assembly LanguageAssembly = typeof(IRoot).Assembly;
            Type[] LanguageTypes = LanguageAssembly.GetTypes();

            foreach (Type Item in LanguageTypes)
            {
                if (!Item.IsInterface && !Item.IsAbstract && Item.GetInterface(typeof(INode).FullName) != null)
                {
                    Type[] Interfaces = Item.GetInterfaces();
                    foreach (Type InterfaceType in Interfaces)
                        if (InterfaceType.Name == $"I{Item.Name}")
                        {
                            Result.Add(InterfaceType, default(TValue));
                            break;
                        }
                }
            }

            return Result;
        }

        public static bool IsCollectionNeverEmpty(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return IsCollectionNeverEmpty(node.GetType(), propertyName);
        }

        public static bool IsCollectionNeverEmpty(Type nodeType, string propertyName)
        {
            Debug.Assert(NodeTreeHelperList.IsNodeListProperty(nodeType, propertyName, out Type ChildNodeType) || NodeTreeHelperBlockList.IsBlockListProperty(nodeType, propertyName, out Type ChildInterfaceType, out ChildNodeType));

            Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);

            if (NeverEmptyCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in NeverEmptyCollectionTable[InterfaceType])
                    if (Item == propertyName)
                        return true;
            }

            return false;
        }

        public static bool IsCollectionWithExpand(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return IsCollectionWithExpand(node.GetType(), propertyName);
        }

        public static bool IsCollectionWithExpand(Type nodeType, string propertyName)
        {
            Debug.Assert(NodeTreeHelperList.IsNodeListProperty(nodeType, propertyName, out Type ChildNodeType) || NodeTreeHelperBlockList.IsBlockListProperty(nodeType, propertyName, out Type ChildInterfaceType, out ChildNodeType));
            Debug.Assert(!IsCollectionNeverEmpty(nodeType, propertyName));

            Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);

            if (WithExpandCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in WithExpandCollectionTable[InterfaceType])
                    if (Item == propertyName)
                        return true;
            }

            return false;
        }

        public static IDocument CreateDocumentationCopy(IDocument documentation)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            return CreateSimpleDocumentation(documentation.Comment, documentation.Uuid);
        }

        public static bool GetSimplifiedNode(INode node, out INode simplifiedNode)
        {
            switch (node)
            {
                case IQualifiedName AsQualifiedName:
                    return SimplifyQualifiedName(AsQualifiedName, out simplifiedNode);
                case IAssignmentArgument AsAssignmentArgument:
                    return SimplifyAssignmentArgument(AsAssignmentArgument, out simplifiedNode);
                case IPositionalArgument AsPositionalArgument:
                    return SimplifyPositionalArgument(AsPositionalArgument, out simplifiedNode);
                case IQueryExpression AsQueryExpression:
                    return SimplifyQueryExpression(AsQueryExpression, out simplifiedNode);
                case IAgentExpression AsAgentExpression:
                    return SimplifyAgentExpression(AsAgentExpression, out simplifiedNode);
                case IAssertionTagExpression AsAssertionTagExpression:
                    return SimplifyAssertionTagExpression(AsAssertionTagExpression, out simplifiedNode);
                case IBinaryConditionalExpression AsBinaryConditionalExpression:
                    return SimplifyBinaryConditionalExpression(AsBinaryConditionalExpression, out simplifiedNode);
                case IBinaryOperatorExpression AsBinaryOperatorExpression:
                    return SimplifyBinaryOperatorExpression(AsBinaryOperatorExpression, out simplifiedNode);
                case IClassConstantExpression AsClassConstantExpression:
                    return SimplifyClassConstantExpression(AsClassConstantExpression, out simplifiedNode);
                case ICloneOfExpression AsCloneOfExpression:
                    return SimplifyCloneOfExpression(AsCloneOfExpression, out simplifiedNode);
                case IEntityExpression AsEntityExpression:
                    return SimplifyEntityExpression(AsEntityExpression, out simplifiedNode);
                case IEqualityExpression AsEqualityExpression:
                    return SimplifyEqualityExpression(AsEqualityExpression, out simplifiedNode);
                case IIndexQueryExpression AsIndexQueryExpression:
                    return SimplifyIndexQueryExpression(AsIndexQueryExpression, out simplifiedNode);
                case IInitializedObjectExpression AsInitializedObjectExpression:
                    return SimplifyInitializedObjectExpression(AsInitializedObjectExpression, out simplifiedNode);
                case IKeywordEntityExpression AsKeywordEntityExpression:
                    return SimplifyKeywordEntityExpression(AsKeywordEntityExpression, out simplifiedNode);
                case IKeywordExpression AsKeywordExpression:
                    return SimplifyKeywordExpression(AsKeywordExpression, out simplifiedNode);
                case IManifestCharacterExpression AsManifestCharacterExpression:
                    return SimplifyManifestCharacterExpression(AsManifestCharacterExpression, out simplifiedNode);
                case IManifestNumberExpression AsManifestNumberExpression:
                    return SimplifyManifestNumberExpression(AsManifestNumberExpression, out simplifiedNode);
                case IManifestStringExpression AsManifestStringExpression:
                    return SimplifyManifestStringExpression(AsManifestStringExpression, out simplifiedNode);
                case INewExpression AsNewExpression:
                    return SimplifyNewExpression(AsNewExpression, out simplifiedNode);
                case IOldExpression AsOldExpression:
                    return SimplifyOldExpression(AsOldExpression, out simplifiedNode);
                case IPrecursorExpression AsPrecursorExpression:
                    return SimplifyPrecursorExpression(AsPrecursorExpression, out simplifiedNode);
                case IPrecursorIndexExpression AsPrecursorIndexExpression:
                    return SimplifyPrecursorIndexExpression(AsPrecursorIndexExpression, out simplifiedNode);
                case IPreprocessorExpression AsPreprocessorExpression:
                    return SimplifyPreprocessorExpression(AsPreprocessorExpression, out simplifiedNode);
                case IResultOfExpression AsResultOfExpression:
                    return SimplifyResultOfExpression(AsResultOfExpression, out simplifiedNode);
                case IUnaryNotExpression AsUnaryNotExpression:
                    return SimplifyUnaryNotExpression(AsUnaryNotExpression, out simplifiedNode);
                case IUnaryOperatorExpression AsUnaryOperatorExpression:
                    return SimplifyUnaryOperatorExpression(AsUnaryOperatorExpression, out simplifiedNode);
                case ICommandInstruction AsCommandInstruction:
                    return SimplifyCommandInstruction(AsCommandInstruction, out simplifiedNode);
                case IAsLongAsInstruction AsAsLongAsInstruction:
                    return SimplifyAsLongAsInstruction(AsAsLongAsInstruction, out simplifiedNode);
                case IAssignmentInstruction AsAssignmentInstruction:
                    return SimplifyAssignmentInstruction(AsAssignmentInstruction, out simplifiedNode);
                case IAttachmentInstruction AsAttachmentInstruction:
                    return SimplifyAttachmentInstruction(AsAttachmentInstruction, out simplifiedNode);
                case ICheckInstruction AsCheckInstruction:
                    return SimplifyCheckInstruction(AsCheckInstruction, out simplifiedNode);
                case ICreateInstruction AsCreateInstruction:
                    return SimplifyCreateInstruction(AsCreateInstruction, out simplifiedNode);
                case IDebugInstruction AsDebugInstruction:
                    return SimplifyDebugInstruction(AsDebugInstruction, out simplifiedNode);
                case IForLoopInstruction AsForLoopInstruction:
                    return SimplifyForLoopInstruction(AsForLoopInstruction, out simplifiedNode);
                case IIfThenElseInstruction AsIfThenElseInstruction:
                    return SimplifyIfThenElseInstruction(AsIfThenElseInstruction, out simplifiedNode);
                case IIndexAssignmentInstruction AsIndexAssignmentInstruction:
                    return SimplifyIndexAssignmentInstruction(AsIndexAssignmentInstruction, out simplifiedNode);
                case IInspectInstruction AsInspectInstruction:
                    return SimplifyInspectInstruction(AsInspectInstruction, out simplifiedNode);
                case IKeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    return SimplifyKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, out simplifiedNode);
                case IOverLoopInstruction AsOverLoopInstruction:
                    return SimplifyOverLoopInstruction(AsOverLoopInstruction, out simplifiedNode);
                case IPrecursorIndexAssignmentInstruction AsPrecursorIndexAssignmentInstruction:
                    return SimplifyPrecursorIndexAssignmentInstruction(AsPrecursorIndexAssignmentInstruction, out simplifiedNode);
                case IPrecursorInstruction AsPrecursorInstruction:
                    return SimplifyPrecursorInstruction(AsPrecursorInstruction, out simplifiedNode);
                case IRaiseEventInstruction AsRaiseEventInstruction:
                    return SimplifyRaiseEventInstruction(AsRaiseEventInstruction, out simplifiedNode);
                case IReleaseInstruction AsReleaseInstruction:
                    return SimplifyReleaseInstruction(AsReleaseInstruction, out simplifiedNode);
                case IThrowInstruction AsThrowInstruction:
                    return SimplifyThrowInstruction(AsThrowInstruction, out simplifiedNode);
                case IAnchoredType AsAnchoredType:
                    return SimplifyAnchoredType(AsAnchoredType, out simplifiedNode);
                case IKeywordAnchoredType AsKeywordAnchoredType:
                    return SimplifyKeywordAnchoredType(AsKeywordAnchoredType, out simplifiedNode);
                case IFunctionType AsFunctionType:
                    return SimplifyFunctionType(AsFunctionType, out simplifiedNode);
                case IGenericType AsGenericType:
                    return SimplifyGenericType(AsGenericType, out simplifiedNode);
                case IIndexerType AsIndexerType:
                    return SimplifyIndexerType(AsIndexerType, out simplifiedNode);
                case IPropertyType AsPropertyType:
                    return SimplifyPropertyType(AsPropertyType, out simplifiedNode);
                case IProcedureType AsProcedureType:
                    return SimplifyProcedureType(AsProcedureType, out simplifiedNode);
                case ITupleType AsTupleType:
                    return SimplifyTupleType(AsTupleType, out simplifiedNode);
                case IAssignmentTypeArgument AsAssignmentTypeArgument:
                    return SimplifyAssignmentTypeArgument(AsAssignmentTypeArgument, out simplifiedNode);
                case IPositionalTypeArgument AsPositionalTypeArgument:
                    return SimplifyPositionalTypeArgument(AsPositionalTypeArgument, out simplifiedNode);
                default:
                    simplifiedNode = null;
                    return false;
            }
        }

        private static ulong ValueHash(bool value)
        {
            return (ulong)value.GetHashCode();
        }

        private static ulong ValueHash(int value)
        {
            return (ulong)value.GetHashCode();
        }

        private static ulong ValueHash(string value)
        {
            return (ulong)value.GetHashCode();
        }

        private static ulong ValueHash(Guid value)
        {
            return (ulong)value.GetHashCode();
        }

        private static void MergeHash(ref ulong hash1, ulong hash2)
        {
            hash1 ^= hash2;
        }

        private static bool SimplifyQualifiedName(IQualifiedName node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            if (node.Path.Count > 1)
            {
                string ConcatenatedText = string.Empty;

                for (int i = 0; i < node.Path.Count; i++)
                {
                    if (i > 0)
                        ConcatenatedText += ".";

                    ConcatenatedText += node.Path[i].Text;
                }

                simplifiedNode = CreateSimpleQualifiedName(ConcatenatedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyAssignmentArgument(IAssignmentArgument node, out INode simplifiedNode)
        {
            IExpression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;
            simplifiedNode = CreatePositionalArgument(Source);
            return true;
        }

        private static bool SimplifyPositionalArgument(IPositionalArgument node, out INode simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null;
            return true;
        }

        private static bool SimplifyQueryExpression(IQueryExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
            if (ClonedQuery.ArgumentBlocks.NodeBlockList.Count > 0)
                simplifiedNode = CreateQueryExpression(ClonedQuery.Query, new List<IArgument>());

            return simplifiedNode != null;
        }

        private static bool SimplifyAgentExpression(IAgentExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"agent {node.Delegated.Text}");
            return true;
        }

        private static bool SimplifyAssertionTagExpression(IAssertionTagExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"tag {node.TagIdentifier.Text}");
            return true;
        }

        private static bool SimplifyBinaryConditionalExpression(IBinaryConditionalExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            string LeftText, RightText;

            if (GetExpressionText(node.LeftExpression, out LeftText) && GetExpressionText(node.RightExpression, out RightText))
            {
                string Operator = null;

                switch (node.Conditional)
                {
                    case ConditionalTypes.And:
                        Operator = " and ";
                        break;

                    case ConditionalTypes.Or:
                        Operator = " or ";
                        break;

                    case ConditionalTypes.Xor:
                        Operator = " xor ";
                        break;

                    case ConditionalTypes.Implies:
                        Operator = " ⇒ ";
                        break;
                }

                Debug.Assert(Operator != null);

                string SimplifiedText = LeftText + Operator + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyBinaryOperatorExpression(IBinaryOperatorExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string SimplifiedText = LeftText + " " + node.Operator.Text + " " + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyClassConstantExpression(IClassConstantExpression node, out INode simplifiedNode)
        {
            string MergedText = $"{{{node.ClassIdentifier.Text}}}{node.ConstantIdentifier.Text}";
            IQualifiedName Query = StringToQualifiedName(MergedText);

            simplifiedNode = CreateQueryExpression(Query, new List<IArgument>());
            return true;
        }

        private static bool SimplifyCloneOfExpression(ICloneOfExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            string SourceText;

            if (GetExpressionText(node.Source, out SourceText))
            {
                string SimplifiedText = $"clone of {SourceText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyEntityExpression(IEntityExpression node, out INode simplifiedNode)
        {
            string SimplifiedText = $"entity {node.Query.Path[0].Text}";
            simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            return true;
        }

        private static bool SimplifyEqualityExpression(IEqualityExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string EqualityText = (node.Comparison == ComparisonType.Equal) ? "=" : "/=";
                string SimplifiedText = $"{LeftText} {EqualityText} {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyIndexQueryExpression(IIndexQueryExpression node, out INode simplifiedNode)
        {
            if (node.IndexedExpression is IQueryExpression AsQueryExpression && AsQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0)
            {
                IQualifiedName Query = DeepCloneNode(AsQueryExpression.Query, cloneCommentGuid: false) as IQualifiedName;
                IIndexQueryExpression ClonedIndexQuery = DeepCloneNode(node, cloneCommentGuid: false) as IIndexQueryExpression;
                simplifiedNode = CreateQueryExpression(Query, ClonedIndexQuery.ArgumentBlocks);
            }
            else
                simplifiedNode = DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false) as IExpression;

            return true;
        }

        private static bool SimplifyInitializedObjectExpression(IInitializedObjectExpression node, out INode simplifiedNode)
        {
            IQualifiedName Query = StringToQualifiedName(node.ClassIdentifier.Text);

            IBlockList<IAssignmentArgument, AssignmentArgument> ObjectBlockList = node.AssignmentBlocks;
            BlockList<IArgument, Argument> ArgumentBlocks = new BlockList<IArgument, Argument>();
            ArgumentBlocks.Documentation = NodeHelper.CreateDocumentationCopy(ObjectBlockList.Documentation);
            ArgumentBlocks.NodeBlockList = new List<IBlock<IArgument, Argument>>();

            for (int BlockIndex = 0; BlockIndex < ObjectBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IAssignmentArgument, AssignmentArgument> Block = ObjectBlockList.NodeBlockList[BlockIndex];

                Block<IArgument, Argument> NewBlock = new Block<IArgument, Argument>();
                NewBlock.Documentation = CreateDocumentationCopy(Block.Documentation);
                NewBlock.Replication = Block.Replication;

                Pattern NewReplicationPattern = new Pattern();
                NewReplicationPattern.Documentation = CreateDocumentationCopy(Block.ReplicationPattern.Documentation);
                NewReplicationPattern.Text = Block.ReplicationPattern.Text;
                NewBlock.ReplicationPattern = NewReplicationPattern;

                Identifier NewSourceIdentifier = new Identifier();
                NewSourceIdentifier.Documentation = CreateDocumentationCopy(Block.SourceIdentifier.Documentation);
                NewSourceIdentifier.Text = Block.SourceIdentifier.Text;
                NewBlock.SourceIdentifier = NewSourceIdentifier;

                List<IArgument> NewNodeList = new List<IArgument>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    IArgument Item = Block.NodeList[Index];
                    IArgument NewItem = DeepCloneNode(Item, cloneCommentGuid: false) as IArgument;

                    Debug.Assert(NewItem != null);
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                ArgumentBlocks.NodeBlockList.Add(NewBlock);
            }

            simplifiedNode = CreateQueryExpression(Query, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyKeywordEntityExpression(IKeywordEntityExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"entity {node.Value}");
            return true;
        }

        private static bool SimplifyKeywordExpression(IKeywordExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Value.ToString());
            return true;
        }

        private static bool SimplifyManifestCharacterExpression(IManifestCharacterExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyManifestNumberExpression(IManifestNumberExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyManifestStringExpression(IManifestStringExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyNewExpression(INewExpression node, out INode simplifiedNode)
        {
            IQualifiedName Query = DeepCloneNode(node.Object, cloneCommentGuid: false) as IQualifiedName;

            Debug.Assert(Query.Path.Count > 0);
            string Text = Query.Path[0].Text;
            Text = "new " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<IArgument>());
            return true;
        }

        private static bool SimplifyOldExpression(IOldExpression node, out INode simplifiedNode)
        {
            IQualifiedName Query = DeepCloneNode(node.Query, cloneCommentGuid: false) as IQualifiedName;

            Debug.Assert(Query.Path.Count > 0);
            string Text = Query.Path[0].Text;
            Text = "old " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<IArgument>());
            return true;
        }

        private static bool SimplifyPrecursorExpression(IPrecursorExpression node, out INode simplifiedNode)
        {
            IQualifiedName Query = CreateSimpleQualifiedName("precursor");
            IPrecursorExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IPrecursorExpression;
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPrecursorIndexExpression(IPrecursorIndexExpression node, out INode simplifiedNode)
        {
            IQualifiedName Query = CreateSimpleQualifiedName("precursor[]");
            IPrecursorIndexExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IPrecursorIndexExpression;
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPreprocessorExpression(IPreprocessorExpression node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Value.ToString());
            return true;
        }

        private static bool SimplifyResultOfExpression(IResultOfExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            string SourceText;

            if (GetExpressionText(node.Source, out SourceText))
            {
                string SimplifiedText = $"result of {SourceText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyUnaryNotExpression(IUnaryNotExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            string RightText;

            if (GetExpressionText(node.RightExpression, out RightText))
            {
                string SimplifiedText = $"not {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyUnaryOperatorExpression(IUnaryOperatorExpression node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            string RightText;

            if (GetExpressionText(node.RightExpression, out RightText))
            {
                string SimplifiedText = $"{node.Operator.Text} {RightText}";
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyCommandInstruction(ICommandInstruction node, out INode simplifiedNode)
        {
            simplifiedNode = null;

            ICommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            if (ClonedCommand.ArgumentBlocks.NodeBlockList.Count > 0)
                simplifiedNode = CreateCommandInstruction(ClonedCommand.Command, new List<IArgument>());

            return simplifiedNode != null;
        }

        private static bool SimplifyAsLongAsInstruction(IAsLongAsInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            IExpression Source = DeepCloneNode(node.ContinueCondition, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyAssignmentInstruction(IAssignmentInstruction node, out INode simplifiedNode)
        {
            if (BlockListHelper<IQualifiedName, QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                IAssignmentInstruction ClonedInstruction = DeepCloneNode(node, cloneCommentGuid: false) as IAssignmentInstruction;
                IQualifiedName Target = ClonedInstruction.DestinationBlocks.NodeBlockList[0].NodeList[0];

                if (ClonedInstruction.Source is IQueryExpression AsQueryExpression)
                {
                    List<IIdentifier> IdentifierList = new List<IIdentifier>();
                    for (int i = 0; i + 1 < Target.Path.Count; i++)
                        IdentifierList.Add(Target.Path[i]);

                    IIdentifier MiddleIdentifier = CreateSimpleIdentifier(Target.Path[Target.Path.Count - 1].Text + AsQueryExpression.Query.Path[0].Text);
                    IdentifierList.Add(MiddleIdentifier);

                    for (int i = 1; i < AsQueryExpression.Query.Path.Count; i++)
                        IdentifierList.Add(AsQueryExpression.Query.Path[i]);

                    IQualifiedName Command = CreateQualifiedName(IdentifierList);
                    simplifiedNode = CreateCommandInstruction(Command, AsQueryExpression.ArgumentBlocks);
                }
                else
                {
                    IArgument FirstArgument = CreatePositionalArgument(ClonedInstruction.Source);
                    simplifiedNode = CreateCommandInstruction(Target, new List<IArgument>() { FirstArgument });
                }
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyAttachmentInstruction(IAttachmentInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            IExpression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCheckInstruction(ICheckInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            IExpression Source = DeepCloneNode(node.BooleanExpression, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCreateInstruction(ICreateInstruction node, out INode simplifiedNode)
        {
            IQualifiedName Command = CreateSimpleQualifiedName(node.CreationRoutineIdentifier.Text);

            IBlockList<IArgument, Argument> ArgumentCopy = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);
            simplifiedNode = CreateCommandInstruction(Command, ArgumentCopy);

            return true;
        }

        private static bool SimplifyDebugInstruction(IDebugInstruction node, out INode simplifiedNode)
        {
            if (node.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0);
                simplifiedNode = DeepCloneNode(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false) as IInstruction;
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyForLoopInstruction(IForLoopInstruction node, out INode simplifiedNode)
        {
            IInstruction SelectedInstruction = null;

            if (node.InitInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.InitInstructionBlocks.NodeBlockList[0].NodeList.Count > 0);
                SelectedInstruction = node.InitInstructionBlocks.NodeBlockList[0].NodeList[0];
            }
            else if (node.LoopInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.LoopInstructionBlocks.NodeBlockList[0].NodeList.Count > 0);
                SelectedInstruction = node.LoopInstructionBlocks.NodeBlockList[0].NodeList[0];
            }
            else if (node.IterationInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.IterationInstructionBlocks.NodeBlockList[0].NodeList.Count > 0);
                SelectedInstruction = node.IterationInstructionBlocks.NodeBlockList[0].NodeList[0];
            }

            if (SelectedInstruction != null)
                simplifiedNode = DeepCloneNode(SelectedInstruction, cloneCommentGuid: false) as IInstruction;
            else
            {
                IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                IExpression Source = DeepCloneNode(node.WhileCondition, cloneCommentGuid: false) as IExpression;

                simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIfThenElseInstruction(IIfThenElseInstruction node, out INode simplifiedNode)
        {
            Debug.Assert(node.ConditionalBlocks.NodeBlockList.Count > 0 && node.ConditionalBlocks.NodeBlockList[0].NodeList.Count > 0);
            IConditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];

            if (FirstConditional.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0);
                simplifiedNode = DeepCloneNode(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false) as IInstruction;
            }
            else
            {
                IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                IExpression Source = DeepCloneNode(FirstConditional.BooleanExpression, cloneCommentGuid: false) as IExpression;

                simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIndexAssignmentInstruction(IIndexAssignmentInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = DeepCloneNode(node.Destination, cloneCommentGuid: false) as IQualifiedName;
            IExpression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyInspectInstruction(IInspectInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            IExpression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyKeywordAssignmentInstruction(IKeywordAssignmentInstruction node, out INode simplifiedNode)
        {
            IIdentifier KeywordIdentifier = CreateSimpleIdentifier(node.Destination.ToString());

            List<IIdentifier> IdentifierList = new List<IIdentifier>();
            IdentifierList.Add(KeywordIdentifier);

            List<IArgument> ArgumentList = new List<IArgument>();
            IBlockList<IArgument, Argument> ArgumentBlocks;

            if (node.Source is IQueryExpression AsQueryExpression)
            {
                IQueryExpression ClonedSource = DeepCloneNode(AsQueryExpression, cloneCommentGuid: false) as IQueryExpression;

                IdentifierList.AddRange(ClonedSource.Query.Path);
                ArgumentBlocks = ClonedSource.ArgumentBlocks;
            }
            else
            {
                IExpression ClonedSource = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;
                IArgument FirstArgument = CreatePositionalArgument(ClonedSource);
                ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateSimpleBlockList(FirstArgument);
            }

            IQualifiedName Command = CreateQualifiedName(IdentifierList);

            simplifiedNode = CreateCommandInstruction(Command, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyOverLoopInstruction(IOverLoopInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            IExpression Source = DeepCloneNode(node.OverList, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorIndexAssignmentInstruction(IPrecursorIndexAssignmentInstruction node, out INode simplifiedNode)
        {
            IQualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            IExpression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as IExpression;

            simplifiedNode = CreateAssignmentInstruction(new List<IQualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorInstruction(IPrecursorInstruction node, out INode simplifiedNode)
        {
            IQualifiedName Command = CreateSimpleQualifiedName("precursor");
            IBlockList<IArgument, Argument> ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);

            simplifiedNode = CreateCommandInstruction(Command, ClonedArgumentBlocks);

            return true;
        }

        private static bool SimplifyRaiseEventInstruction(IRaiseEventInstruction node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleCommandInstruction(node.QueryIdentifier.Text);

            return true;
        }

        private static bool SimplifyReleaseInstruction(IReleaseInstruction node, out INode simplifiedNode)
        {
            IQualifiedName Command = DeepCloneNode(node.EntityName, cloneCommentGuid: false) as IQualifiedName;
            simplifiedNode = CreateCommandInstruction(Command, new List<IArgument>());

            return true;
        }

        private static bool SimplifyThrowInstruction(IThrowInstruction node, out INode simplifiedNode)
        {
            IQualifiedName Command = CreateSimpleQualifiedName("precursor");
            IBlockList<IArgument, Argument> ClonedArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockListCopy(node.ArgumentBlocks);

            simplifiedNode = CreateCommandInstruction(Command, ClonedArgumentBlocks);

            return true;
        }

        private static bool SimplifyAnchoredType(IAnchoredType node, out INode simplifiedNode)
        {
            Debug.Assert(node.AnchoredName.Path.Count > 0);
            simplifiedNode = CreateSimpleSimpleType(node.AnchoredName.Path[0].Text);
            return true;
        }

        private static bool SimplifyKeywordAnchoredType(IKeywordAnchoredType node, out INode simplifiedNode)
        {
            simplifiedNode = CreateSimpleSimpleType(node.Anchor.ToString());
            return true;
        }

        private static bool SimplifyFunctionType(IFunctionType node, out INode simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as IObjectType;
            return true;
        }

        private static bool SimplifyGenericType(IGenericType node, out INode simplifiedNode)
        {
            string Text = node.ClassIdentifier.Text;

            if (BlockListHelper<ITypeArgument, TypeArgument>.IsSimple(node.TypeArgumentBlocks))
            {
                ITypeArgument FirstArgument = node.TypeArgumentBlocks.NodeBlockList[0].NodeList[0];
                if (FirstArgument is IPositionalTypeArgument AsPositionalTypeArgument && AsPositionalTypeArgument.Source is ISimpleType AsSimpleType)
                {
                    Text += AsSimpleType.ClassIdentifier.Text;
                }
            }

            simplifiedNode = CreateSimpleSimpleType(Text);
            return true;
        }

        private static bool SimplifyIndexerType(IIndexerType node, out INode simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as IObjectType;
            return true;
        }

        private static bool SimplifyPropertyType(IPropertyType node, out INode simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as IObjectType;
            return true;
        }

        private static bool SimplifyProcedureType(IProcedureType node, out INode simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as IObjectType;
            return true;
        }

        private static bool SimplifyTupleType(ITupleType node, out INode simplifiedNode)
        {
            IEntityDeclaration FirstField = node.EntityDeclarationBlocks.NodeBlockList[0].NodeList[0];
            simplifiedNode = DeepCloneNode(FirstField.EntityType, cloneCommentGuid: false) as IObjectType;
            return true;
        }

        private static bool SimplifyAssignmentTypeArgument(IAssignmentTypeArgument node, out INode simplifiedNode)
        {
            simplifiedNode = CreatePositionalTypeArgument(node.Source);
            return true;
        }

        private static bool SimplifyPositionalTypeArgument(IPositionalTypeArgument node, out INode simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null;
            return true;
        }

        private static IQualifiedName StringToQualifiedName(string text)
        {
            string[] StringList;
            ParseDotSeparatedIdentifiers(text, out StringList);

            List<IIdentifier> IdentifierList = new List<IIdentifier>();
            foreach (string Identifier in StringList)
                IdentifierList.Add(CreateSimpleIdentifier(Identifier));

            return CreateQualifiedName(IdentifierList);
        }

        private static void ParseDotSeparatedIdentifiers(string text, out string[] stringList)
        {
            ParseSymbolSeparatedStrings(text, '.', out stringList);
        }

        private static void ParseSymbolSeparatedStrings(string text, char symbol, out string[] stringList)
        {
            string[] SplittedStrings = text.Split(symbol);
            stringList = new string[SplittedStrings.Length];
            for (int i = 0; i < SplittedStrings.Length; i++)
                stringList[i] = SplittedStrings[i].Trim();
        }

        private static bool GetExpressionText(IExpression expressionNode, out string text)
        {
            IManifestNumberExpression AsNumber;
            IQueryExpression AsQuery;

            if ((AsNumber = expressionNode as IManifestNumberExpression) != null)
            {
                text = AsNumber.Text;
                return true;
            }
            else if ((AsQuery = expressionNode as IQueryExpression) != null)
            {
                text = AsQuery.Query.Path[0].Text;
                return true;
            }
            else
            {
                text = null;
                return false;
            }
        }
    }
}
