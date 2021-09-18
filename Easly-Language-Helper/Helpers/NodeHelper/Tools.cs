#pragma warning disable SA1600 // Elements should be documented

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

            Node Node = optional.Item as Node;
            Debug.Assert(Node != null, $"The optional item is always a {nameof(Node)}");

            return IsDefaultNode(Node);
        }

        public static bool IsDefaultNode(Node node)
        {
            IList<Identifier> Path;

            switch (node)
            {
                case Name AsName:
                    return AsName.Text.Length == 0;

                case Identifier AsIdentifier:
                    return AsIdentifier.Text.Length == 0;

                case Scope AsScope:
                    return AsScope.EntityDeclarationBlocks.NodeBlockList.Count == 0 && AsScope.InstructionBlocks.NodeBlockList.Count == 0;

                case QualifiedName AsQualifiedName:
                    Path = AsQualifiedName.Path; // Debug.Assert(Path.Count > 0);
                    return Path.Count == 1 && Path[0].Text.Length == 0;

                case SimpleType AsSimpleType:
                    return AsSimpleType.Sharing == SharingType.NotShared && AsSimpleType.ClassIdentifier.Text.Length == 0;

                case ObjectType AsObjectType: // Fallback for other IObjectType.
                    return false;

                case QueryExpression AsQueryExpression:
                    Path = AsQueryExpression.Query.Path; // Debug.Assert(Path.Count > 0);
                    return AsQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0 && Path.Count == 1 && Path[0].Text.Length == 0;

                case ManifestCharacterExpression AsManifestCharacterExpression:
                    return AsManifestCharacterExpression.Text.Length == 0;

                case ManifestNumberExpression AsManifestNumberExpression:
                    return AsManifestNumberExpression.Text.Length == 0;

                case ManifestStringExpression AsManifestStringExpression:
                    return AsManifestStringExpression.Text.Length == 0;

                case Expression AsExpression: // Fallback for other IExpression.
                    return false;

                case EffectiveBody AsEffectiveBody:
                    return AsEffectiveBody.RequireBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.EnsureBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.ExceptionIdentifierBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.EntityDeclarationBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.BodyInstructionBlocks.NodeBlockList.Count == 0 &&
                           AsEffectiveBody.ExceptionHandlerBlocks.NodeBlockList.Count == 0;

                case Body AsBody: // Fallback for other IBody.
                    return false;

                case Argument AsArgument:
                    return IsDefaultArgument(node);

                default:
                    return IsEmptyNode(node);
            }
        }

        public static bool IsDefaultArgument(Node node)
        {
            if (node is PositionalArgument AsPositional)
                if (AsPositional.Source is QueryExpression AsQueryExpression)
                {
                    IList<Identifier> Path = AsQueryExpression.Query.Path;
                    if (Path.Count == 1 && Path[0].Text.Length == 0)
                    {
                        IBlockList ArgumentBlocks = AsQueryExpression.ArgumentBlocks as IBlockList;
                        Debug.Assert(ArgumentBlocks != null, $"ArgumentBlocks is always a {nameof(IBlockList)}");

                        if (NodeTreeHelperBlockList.IsBlockListEmpty(ArgumentBlocks))
                            return true;
                    }
                }

            return false;
        }

        public static ulong NodeHash(Node node)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node);
            Type ChildNodeType;
            ulong Hash = 0;

            foreach (string PropertyName in PropertyNames)
            {
                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out Node ChildNode);
                    MergeHash(ref Hash, NodeHash(ChildNode));
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out Node ChildNode);
                    MergeHash(ref Hash, IsAssigned ? 1UL : 0);
                    if (IsAssigned)
                        MergeHash(ref Hash, NodeHash(ChildNode));
                }
                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<Node> ChildNodeList);
                    foreach (Node ChildNode in ChildNodeList)
                        MergeHash(ref Hash, NodeHash(ChildNode));
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, PropertyName, /*out Type ChildInterfaceType,*/ out ChildNodeType))
                {
                    NodeTreeHelperBlockList.GetChildBlockList(node, PropertyName, out IReadOnlyList<NodeTreeBlock> ChildBlockList);
                    for (int i = 0; i < ChildBlockList.Count; i++)
                    {
                        NodeTreeHelperBlockList.GetChildBlock(node, PropertyName, i, out IBlock ChildBlock);
                        IReadOnlyList<Node> NodeList = ChildBlockList[i].NodeList;

                        MergeHash(ref Hash, ValueHash(ChildBlock.Documentation.Comment));
                        MergeHash(ref Hash, ValueHash(ChildBlock.Documentation.Uuid));
                        MergeHash(ref Hash, ValueHash((int)ChildBlock.Replication));
                        MergeHash(ref Hash, NodeHash(ChildBlock.ReplicationPattern));
                        MergeHash(ref Hash, NodeHash(ChildBlock.SourceIdentifier));

                        foreach (Node ChildNode in NodeList)
                            MergeHash(ref Hash, NodeHash(ChildNode));
                    }
                }
                else
                {
                    Type NodeType = node.GetType();
                    PropertyInfo Info = NodeType.GetProperty(PropertyName);

                    if (Info.PropertyType == typeof(Document))
                    {
                        Document Documentation = Info.GetValue(node) as Document;
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

        public static Node DeepCloneNode(Node root, bool cloneCommentGuid)
        {
            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(root);

            // Careful, the resulting "empty" node can contain items for lists that are not allowed to be empty.
            Node ClonedRoot = CreateEmptyNode(root.GetType());

            foreach (string PropertyName in PropertyNames)
            {
                Type /*ChildInterfaceType,*/ ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(root, PropertyName, out Node ChildNode);

                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                    NodeTreeHelperChild.SetChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(root, PropertyName, out bool IsAssigned, out Node ChildNode);

                    if (ChildNode != null)
                    {
                        Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
                        NodeTreeHelperOptional.SetOptionalChildNode(ClonedRoot, PropertyName, ClonedChildNode);
                        if (!IsAssigned)
                            NodeTreeHelperOptional.UnassignChildNode(ClonedRoot, PropertyName);
                    }
                }
                else if (NodeTreeHelperList.IsNodeListProperty(root, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(root, PropertyName, out IReadOnlyList<Node> ChildNodeList);
                    IList<Node> ClonedChildNodeList = DeepCloneNodeList(ChildNodeList, cloneCommentGuid);

                    NodeTreeHelperList.ClearChildNodeList(ClonedRoot, PropertyName);
                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                        NodeTreeHelperList.InsertIntoList(ClonedRoot, PropertyName, Index, ClonedChildNodeList[Index]);
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(root, PropertyName, /*out ChildInterfaceType,*/ out ChildNodeType))
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

        public static IList<Node> DeepCloneNodeList(IEnumerable<Node> rootList, bool cloneCommentGuid)
        {
            if (rootList == null) throw new ArgumentNullException(nameof(rootList));

            List<Node> Result = new List<Node>();

            foreach (Node ChildNode in rootList)
            {
                Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);
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
                BlockType = typeof(Block<>).MakeGenericType(GenericArguments);

                Pattern ClonedPattern = (Pattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
                Identifier ClonedSource = (Identifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
                IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockType, Block.Replication, ClonedPattern, ClonedSource);
                NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node ChildNode = Block.NodeList[Index] as Node;
                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

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
            BlockListType = typeof(IBlockList<>).MakeGenericType(GenericArguments);

            IBlockList ClonedBlockList = (IBlockList)BlockListType.Assembly.CreateInstance(BlockListType.FullName);

            Type NodeListType = rootBlockList.NodeBlockList.GetType();
            IList ClonedNodeBlockList = (IList)NodeListType.Assembly.CreateInstance(NodeListType.FullName);

            PropertyInfo NodeBlockListProperty = BlockListType.GetProperty(nameof(IBlockList.NodeBlockList));
            NodeBlockListProperty.SetValue(ClonedBlockList, ClonedNodeBlockList);
            NodeTreeHelper.CopyDocumentation(rootBlockList, ClonedBlockList, cloneCommentGuid);

            for (int BlockIndex = 0; BlockIndex < rootBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock Block = rootBlockList.NodeBlockList[BlockIndex] as IBlock;

                Pattern ClonedPattern = (Pattern)DeepCloneNode(Block.ReplicationPattern, cloneCommentGuid);
                Identifier ClonedSource = (Identifier)DeepCloneNode(Block.SourceIdentifier, cloneCommentGuid);
                IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(BlockListType, Block.Replication, ClonedPattern, ClonedSource);
                ClonedNodeBlockList.Add(ClonedBlock);

                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Node ChildNode = Block.NodeList[Index] as Node;
                    Node ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                    NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
                }

                NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);
            }

            return ClonedBlockList;
        }

        public static IDictionary<Type, TValue> CreateNodeDictionary<TValue>()
        {
            IDictionary<Type, TValue> Result = new Dictionary<Type, TValue>();
            Assembly LanguageAssembly = typeof(Root).Assembly;
            Type[] LanguageTypes = LanguageAssembly.GetTypes();

            foreach (Type Item in LanguageTypes)
            {
                if (!Item.IsInterface && !Item.IsAbstract && Item.GetInterface(typeof(Node).FullName) != null)
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

        public static bool IsCollectionNeverEmpty(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return IsCollectionNeverEmpty(node.GetType(), propertyName);
        }

        public static bool IsCollectionNeverEmpty(Type nodeType, string propertyName)
        {
            if (!NodeTreeHelperList.IsNodeListProperty(nodeType, propertyName, out Type _) && !NodeTreeHelperBlockList.IsBlockListProperty(nodeType, propertyName, /*out Type _,*/ out _)) throw new ArgumentException($"{nameof(propertyName)} must be a list or block list property of {nameof(nodeType)}");

            // Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);
            Type InterfaceType = nodeType;

            if (NeverEmptyCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in NeverEmptyCollectionTable[InterfaceType])
                    if (Item == propertyName)
                        return true;
            }

            return false;
        }

        public static bool IsCollectionWithExpand(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return IsCollectionWithExpand(node.GetType(), propertyName);
        }

        public static bool IsCollectionWithExpand(Type nodeType, string propertyName)
        {
            if (!NodeTreeHelperList.IsNodeListProperty(nodeType, propertyName, out Type _) && !NodeTreeHelperBlockList.IsBlockListProperty(nodeType, propertyName, /*out Type _,*/ out _)) throw new ArgumentException($"{nameof(propertyName)} must be a list or block list property of {nameof(nodeType)}");
            if (IsCollectionNeverEmpty(nodeType, propertyName)) throw new ArgumentException($"{nameof(nodeType)} must be a list or block list that can be empty");

            // Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);
            Type InterfaceType = nodeType;

            if (WithExpandCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in WithExpandCollectionTable[InterfaceType])
                    if (Item == propertyName)
                        return true;
            }

            return false;
        }

        public static Document CreateDocumentationCopy(Document documentation)
        {
            if (documentation == null) throw new ArgumentNullException(nameof(documentation));

            return CreateSimpleDocumentation(documentation.Comment, documentation.Uuid);
        }

        public static bool GetSimplifiedNode(Node node, out Node simplifiedNode)
        {
            switch (node)
            {
                case QualifiedName AsQualifiedName:
                    return SimplifyQualifiedName(AsQualifiedName, out simplifiedNode);
                case AssignmentArgument AsAssignmentArgument:
                    return SimplifyAssignmentArgument(AsAssignmentArgument, out simplifiedNode);
                case PositionalArgument AsPositionalArgument:
                    return SimplifyPositionalArgument(AsPositionalArgument, out simplifiedNode);
                case QueryExpression AsQueryExpression:
                    return SimplifyQueryExpression(AsQueryExpression, out simplifiedNode);
                case AgentExpression AsAgentExpression:
                    return SimplifyAgentExpression(AsAgentExpression, out simplifiedNode);
                case AssertionTagExpression AsAssertionTagExpression:
                    return SimplifyAssertionTagExpression(AsAssertionTagExpression, out simplifiedNode);
                case BinaryConditionalExpression AsBinaryConditionalExpression:
                    return SimplifyBinaryConditionalExpression(AsBinaryConditionalExpression, out simplifiedNode);
                case BinaryOperatorExpression AsBinaryOperatorExpression:
                    return SimplifyBinaryOperatorExpression(AsBinaryOperatorExpression, out simplifiedNode);
                case ClassConstantExpression AsClassConstantExpression:
                    return SimplifyClassConstantExpression(AsClassConstantExpression, out simplifiedNode);
                case CloneOfExpression AsCloneOfExpression:
                    return SimplifyCloneOfExpression(AsCloneOfExpression, out simplifiedNode);
                case EntityExpression AsEntityExpression:
                    return SimplifyEntityExpression(AsEntityExpression, out simplifiedNode);
                case EqualityExpression AsEqualityExpression:
                    return SimplifyEqualityExpression(AsEqualityExpression, out simplifiedNode);
                case IndexQueryExpression AsIndexQueryExpression:
                    return SimplifyIndexQueryExpression(AsIndexQueryExpression, out simplifiedNode);
                case InitializedObjectExpression AsInitializedObjectExpression:
                    return SimplifyInitializedObjectExpression(AsInitializedObjectExpression, out simplifiedNode);
                case KeywordEntityExpression AsKeywordEntityExpression:
                    return SimplifyKeywordEntityExpression(AsKeywordEntityExpression, out simplifiedNode);
                case KeywordExpression AsKeywordExpression:
                    return SimplifyKeywordExpression(AsKeywordExpression, out simplifiedNode);
                case ManifestCharacterExpression AsManifestCharacterExpression:
                    return SimplifyManifestCharacterExpression(AsManifestCharacterExpression, out simplifiedNode);
                case ManifestNumberExpression AsManifestNumberExpression:
                    return SimplifyManifestNumberExpression(AsManifestNumberExpression, out simplifiedNode);
                case ManifestStringExpression AsManifestStringExpression:
                    return SimplifyManifestStringExpression(AsManifestStringExpression, out simplifiedNode);
                case NewExpression AsNewExpression:
                    return SimplifyNewExpression(AsNewExpression, out simplifiedNode);
                case OldExpression AsOldExpression:
                    return SimplifyOldExpression(AsOldExpression, out simplifiedNode);
                case PrecursorExpression AsPrecursorExpression:
                    return SimplifyPrecursorExpression(AsPrecursorExpression, out simplifiedNode);
                case PrecursorIndexExpression AsPrecursorIndexExpression:
                    return SimplifyPrecursorIndexExpression(AsPrecursorIndexExpression, out simplifiedNode);
                case PreprocessorExpression AsPreprocessorExpression:
                    return SimplifyPreprocessorExpression(AsPreprocessorExpression, out simplifiedNode);
                case ResultOfExpression AsResultOfExpression:
                    return SimplifyResultOfExpression(AsResultOfExpression, out simplifiedNode);
                case UnaryNotExpression AsUnaryNotExpression:
                    return SimplifyUnaryNotExpression(AsUnaryNotExpression, out simplifiedNode);
                case UnaryOperatorExpression AsUnaryOperatorExpression:
                    return SimplifyUnaryOperatorExpression(AsUnaryOperatorExpression, out simplifiedNode);
                case CommandInstruction AsCommandInstruction:
                    return SimplifyCommandInstruction(AsCommandInstruction, out simplifiedNode);
                case AsLongAsInstruction AsAsLongAsInstruction:
                    return SimplifyAsLongAsInstruction(AsAsLongAsInstruction, out simplifiedNode);
                case AssignmentInstruction AsAssignmentInstruction:
                    return SimplifyAssignmentInstruction(AsAssignmentInstruction, out simplifiedNode);
                case AttachmentInstruction AsAttachmentInstruction:
                    return SimplifyAttachmentInstruction(AsAttachmentInstruction, out simplifiedNode);
                case CheckInstruction AsCheckInstruction:
                    return SimplifyCheckInstruction(AsCheckInstruction, out simplifiedNode);
                case CreateInstruction AsCreateInstruction:
                    return SimplifyCreateInstruction(AsCreateInstruction, out simplifiedNode);
                case DebugInstruction AsDebugInstruction:
                    return SimplifyDebugInstruction(AsDebugInstruction, out simplifiedNode);
                case ForLoopInstruction AsForLoopInstruction:
                    return SimplifyForLoopInstruction(AsForLoopInstruction, out simplifiedNode);
                case IfThenElseInstruction AsIfThenElseInstruction:
                    return SimplifyIfThenElseInstruction(AsIfThenElseInstruction, out simplifiedNode);
                case IndexAssignmentInstruction AsIndexAssignmentInstruction:
                    return SimplifyIndexAssignmentInstruction(AsIndexAssignmentInstruction, out simplifiedNode);
                case InspectInstruction AsInspectInstruction:
                    return SimplifyInspectInstruction(AsInspectInstruction, out simplifiedNode);
                case KeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    return SimplifyKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, out simplifiedNode);
                case OverLoopInstruction AsOverLoopInstruction:
                    return SimplifyOverLoopInstruction(AsOverLoopInstruction, out simplifiedNode);
                case PrecursorIndexAssignmentInstruction AsPrecursorIndexAssignmentInstruction:
                    return SimplifyPrecursorIndexAssignmentInstruction(AsPrecursorIndexAssignmentInstruction, out simplifiedNode);
                case PrecursorInstruction AsPrecursorInstruction:
                    return SimplifyPrecursorInstruction(AsPrecursorInstruction, out simplifiedNode);
                case RaiseEventInstruction AsRaiseEventInstruction:
                    return SimplifyRaiseEventInstruction(AsRaiseEventInstruction, out simplifiedNode);
                case ReleaseInstruction AsReleaseInstruction:
                    return SimplifyReleaseInstruction(AsReleaseInstruction, out simplifiedNode);
                case ThrowInstruction AsThrowInstruction:
                    return SimplifyThrowInstruction(AsThrowInstruction, out simplifiedNode);
                case AnchoredType AsAnchoredType:
                    return SimplifyAnchoredType(AsAnchoredType, out simplifiedNode);
                case KeywordAnchoredType AsKeywordAnchoredType:
                    return SimplifyKeywordAnchoredType(AsKeywordAnchoredType, out simplifiedNode);
                case FunctionType AsFunctionType:
                    return SimplifyFunctionType(AsFunctionType, out simplifiedNode);
                case GenericType AsGenericType:
                    return SimplifyGenericType(AsGenericType, out simplifiedNode);
                case IndexerType AsIndexerType:
                    return SimplifyIndexerType(AsIndexerType, out simplifiedNode);
                case PropertyType AsPropertyType:
                    return SimplifyPropertyType(AsPropertyType, out simplifiedNode);
                case ProcedureType AsProcedureType:
                    return SimplifyProcedureType(AsProcedureType, out simplifiedNode);
                case TupleType AsTupleType:
                    return SimplifyTupleType(AsTupleType, out simplifiedNode);
                case AssignmentTypeArgument AsAssignmentTypeArgument:
                    return SimplifyAssignmentTypeArgument(AsAssignmentTypeArgument, out simplifiedNode);
                case PositionalTypeArgument AsPositionalTypeArgument:
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

        private static bool SimplifyQualifiedName(QualifiedName node, out Node simplifiedNode)
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

        private static bool SimplifyAssignmentArgument(AssignmentArgument node, out Node simplifiedNode)
        {
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;
            simplifiedNode = CreatePositionalArgument(Source);
            return true;
        }

        private static bool SimplifyPositionalArgument(PositionalArgument node, out Node simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null;
            return true;
        }

        private static bool SimplifyQueryExpression(QueryExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            QueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as QueryExpression;
            if (ClonedQuery.ArgumentBlocks.NodeBlockList.Count > 0)
                simplifiedNode = CreateQueryExpression(ClonedQuery.Query, new List<Argument>());

            return simplifiedNode != null;
        }

        private static bool SimplifyAgentExpression(AgentExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"agent {node.Delegated.Text}");
            return true;
        }

        private static bool SimplifyAssertionTagExpression(AssertionTagExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"tag {node.TagIdentifier.Text}");
            return true;
        }

        private static bool SimplifyBinaryConditionalExpression(BinaryConditionalExpression node, out Node simplifiedNode)
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

                Debug.Assert(Operator != null, $"All values of {nameof(ConditionalTypes)} have been handled");

                string SimplifiedText = LeftText + Operator + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyBinaryOperatorExpression(BinaryOperatorExpression node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            if (GetExpressionText(node.LeftExpression, out string LeftText) && GetExpressionText(node.RightExpression, out string RightText))
            {
                string SimplifiedText = LeftText + " " + node.Operator.Text + " " + RightText;
                simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            }

            return simplifiedNode != null;
        }

        private static bool SimplifyClassConstantExpression(ClassConstantExpression node, out Node simplifiedNode)
        {
            string MergedText = $"{{{node.ClassIdentifier.Text}}}{node.ConstantIdentifier.Text}";
            QualifiedName Query = StringToQualifiedName(MergedText);

            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyCloneOfExpression(CloneOfExpression node, out Node simplifiedNode)
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

        private static bool SimplifyEntityExpression(EntityExpression node, out Node simplifiedNode)
        {
            string SimplifiedText = $"entity {node.Query.Path[0].Text}";
            simplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            return true;
        }

        private static bool SimplifyEqualityExpression(EqualityExpression node, out Node simplifiedNode)
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

        private static bool SimplifyIndexQueryExpression(IndexQueryExpression node, out Node simplifiedNode)
        {
            if (node.IndexedExpression is QueryExpression AsQueryExpression && AsQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0)
            {
                QualifiedName Query = DeepCloneNode(AsQueryExpression.Query, cloneCommentGuid: false) as QualifiedName;
                IndexQueryExpression ClonedIndexQuery = DeepCloneNode(node, cloneCommentGuid: false) as IndexQueryExpression;
                simplifiedNode = CreateQueryExpression(Query, ClonedIndexQuery.ArgumentBlocks);
            }
            else
                simplifiedNode = DeepCloneNode(node.IndexedExpression, cloneCommentGuid: false) as Expression;

            return true;
        }

        private static bool SimplifyInitializedObjectExpression(InitializedObjectExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = StringToQualifiedName(node.ClassIdentifier.Text);

            IBlockList<AssignmentArgument> ObjectBlockList = node.AssignmentBlocks;
            BlockList<Argument> ArgumentBlocks = new BlockList<Argument>();
            ArgumentBlocks.Documentation = NodeHelper.CreateDocumentationCopy(ObjectBlockList.Documentation);
            ArgumentBlocks.NodeBlockList = new List<IBlock<Argument>>();

            for (int BlockIndex = 0; BlockIndex < ObjectBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<AssignmentArgument> Block = ObjectBlockList.NodeBlockList[BlockIndex];

                Block<Argument> NewBlock = new Block<Argument>();
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

                List<Argument> NewNodeList = new List<Argument>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    Argument Item = Block.NodeList[Index];
                    Argument NewItem = DeepCloneNode(Item, cloneCommentGuid: false) as Argument;

                    Debug.Assert(NewItem != null, $"A cloned object is always a {nameof(Argument)}");
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                ArgumentBlocks.NodeBlockList.Add(NewBlock);
            }

            simplifiedNode = CreateQueryExpression(Query, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyKeywordEntityExpression(KeywordEntityExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression($"entity {node.Value}");
            return true;
        }

        private static bool SimplifyKeywordExpression(KeywordExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Value.ToString());
            return true;
        }

        private static bool SimplifyManifestCharacterExpression(ManifestCharacterExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyManifestNumberExpression(ManifestNumberExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyManifestStringExpression(ManifestStringExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Text);
            return true;
        }

        private static bool SimplifyNewExpression(NewExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = DeepCloneNode(node.Object, cloneCommentGuid: false) as QualifiedName;

            Debug.Assert(Query.Path.Count > 0, $"A cloned query is never empty");
            string Text = Query.Path[0].Text;
            Text = "new " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyOldExpression(OldExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = DeepCloneNode(node.Query, cloneCommentGuid: false) as QualifiedName;

            Debug.Assert(Query.Path.Count > 0, $"A cloned query is never empty");
            string Text = Query.Path[0].Text;
            Text = "old " + Text;

            NodeTreeHelper.SetString(Query.Path[0], "Text", Text);
            simplifiedNode = CreateQueryExpression(Query, new List<Argument>());
            return true;
        }

        private static bool SimplifyPrecursorExpression(PrecursorExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = CreateSimpleQualifiedName("precursor");
            PrecursorExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as PrecursorExpression;
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPrecursorIndexExpression(PrecursorIndexExpression node, out Node simplifiedNode)
        {
            QualifiedName Query = CreateSimpleQualifiedName("precursor[]");
            PrecursorIndexExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as PrecursorIndexExpression;
            simplifiedNode = CreateQueryExpression(Query, ClonedQuery.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPreprocessorExpression(PreprocessorExpression node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleQueryExpression(node.Value.ToString());
            return true;
        }

        private static bool SimplifyResultOfExpression(ResultOfExpression node, out Node simplifiedNode)
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

        private static bool SimplifyUnaryNotExpression(UnaryNotExpression node, out Node simplifiedNode)
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

        private static bool SimplifyUnaryOperatorExpression(UnaryOperatorExpression node, out Node simplifiedNode)
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

        private static bool SimplifyCommandInstruction(CommandInstruction node, out Node simplifiedNode)
        {
            simplifiedNode = null;

            CommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as CommandInstruction;
            if (ClonedCommand.ArgumentBlocks.NodeBlockList.Count > 0)
                simplifiedNode = CreateCommandInstruction(ClonedCommand.Command, new List<Argument>());

            return simplifiedNode != null;
        }

        private static bool SimplifyAsLongAsInstruction(AsLongAsInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.ContinueCondition, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyAssignmentInstruction(AssignmentInstruction node, out Node simplifiedNode)
        {
            if (BlockListHelper<QualifiedName>.IsSimple(node.DestinationBlocks))
            {
                AssignmentInstruction ClonedInstruction = DeepCloneNode(node, cloneCommentGuid: false) as AssignmentInstruction;
                QualifiedName Target = ClonedInstruction.DestinationBlocks.NodeBlockList[0].NodeList[0];

                if (ClonedInstruction.Source is QueryExpression AsQueryExpression)
                {
                    List<Identifier> IdentifierList = new List<Identifier>();
                    for (int i = 0; i + 1 < Target.Path.Count; i++)
                        IdentifierList.Add(Target.Path[i]);

                    Identifier MiddleIdentifier = CreateSimpleIdentifier(Target.Path[Target.Path.Count - 1].Text + AsQueryExpression.Query.Path[0].Text);
                    IdentifierList.Add(MiddleIdentifier);

                    for (int i = 1; i < AsQueryExpression.Query.Path.Count; i++)
                        IdentifierList.Add(AsQueryExpression.Query.Path[i]);

                    QualifiedName Command = CreateQualifiedName(IdentifierList);
                    simplifiedNode = CreateCommandInstruction(Command, AsQueryExpression.ArgumentBlocks);
                }
                else
                {
                    Argument FirstArgument = CreatePositionalArgument(ClonedInstruction.Source);
                    simplifiedNode = CreateCommandInstruction(Target, new List<Argument>() { FirstArgument });
                }
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyAttachmentInstruction(AttachmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCheckInstruction(CheckInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.BooleanExpression, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyCreateInstruction(CreateInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = CreateSimpleQualifiedName(node.CreationRoutineIdentifier.Text);

            IBlockList<Argument> ArgumentCopy = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);
            simplifiedNode = CreateCommandInstruction(Command, ArgumentCopy);

            return true;
        }

        private static bool SimplifyDebugInstruction(DebugInstruction node, out Node simplifiedNode)
        {
            if (node.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                simplifiedNode = DeepCloneNode(node.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false) as Instruction;
            }
            else
                simplifiedNode = CreateEmptyCommandInstruction();

            return true;
        }

        private static bool SimplifyForLoopInstruction(ForLoopInstruction node, out Node simplifiedNode)
        {
            Instruction SelectedInstruction = null;

            if (node.InitInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.InitInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                SelectedInstruction = node.InitInstructionBlocks.NodeBlockList[0].NodeList[0];
            }
            else if (node.LoopInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.LoopInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                SelectedInstruction = node.LoopInstructionBlocks.NodeBlockList[0].NodeList[0];
            }
            else if (node.IterationInstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(node.IterationInstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                SelectedInstruction = node.IterationInstructionBlocks.NodeBlockList[0].NodeList[0];
            }

            if (SelectedInstruction != null)
                simplifiedNode = DeepCloneNode(SelectedInstruction, cloneCommentGuid: false) as Instruction;
            else
            {
                QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                Expression Source = DeepCloneNode(node.WhileCondition, cloneCommentGuid: false) as Expression;

                simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIfThenElseInstruction(IfThenElseInstruction node, out Node simplifiedNode)
        {
            Debug.Assert(node.ConditionalBlocks.NodeBlockList.Count > 0 && node.ConditionalBlocks.NodeBlockList[0].NodeList.Count > 0, "There is always at least one conditional");
            Conditional FirstConditional = node.ConditionalBlocks.NodeBlockList[0].NodeList[0];

            if (FirstConditional.Instructions.InstructionBlocks.NodeBlockList.Count > 0)
            {
                Debug.Assert(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList.Count > 0, "A block in a block list always has at least one element");
                simplifiedNode = DeepCloneNode(FirstConditional.Instructions.InstructionBlocks.NodeBlockList[0].NodeList[0], cloneCommentGuid: false) as Instruction;
            }
            else
            {
                QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
                Expression Source = DeepCloneNode(FirstConditional.BooleanExpression, cloneCommentGuid: false) as Expression;

                simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);
            }

            return true;
        }

        private static bool SimplifyIndexAssignmentInstruction(IndexAssignmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = DeepCloneNode(node.Destination, cloneCommentGuid: false) as QualifiedName;
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyInspectInstruction(InspectInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyKeywordAssignmentInstruction(KeywordAssignmentInstruction node, out Node simplifiedNode)
        {
            Identifier KeywordIdentifier = CreateSimpleIdentifier(node.Destination.ToString());

            List<Identifier> IdentifierList = new List<Identifier>();
            IdentifierList.Add(KeywordIdentifier);

            List<Argument> ArgumentList = new List<Argument>();
            IBlockList<Argument> ArgumentBlocks;

            if (node.Source is QueryExpression AsQueryExpression)
            {
                QueryExpression ClonedSource = DeepCloneNode(AsQueryExpression, cloneCommentGuid: false) as QueryExpression;

                IdentifierList.AddRange(ClonedSource.Query.Path);
                ArgumentBlocks = ClonedSource.ArgumentBlocks;
            }
            else
            {
                Expression ClonedSource = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;
                Argument FirstArgument = CreatePositionalArgument(ClonedSource);
                ArgumentBlocks = BlockListHelper<Argument>.CreateSimpleBlockList(FirstArgument);
            }

            QualifiedName Command = CreateQualifiedName(IdentifierList);

            simplifiedNode = CreateCommandInstruction(Command, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyOverLoopInstruction(OverLoopInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.OverList, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorIndexAssignmentInstruction(PrecursorIndexAssignmentInstruction node, out Node simplifiedNode)
        {
            QualifiedName AssignmentTarget = CreateEmptyQualifiedName();
            Expression Source = DeepCloneNode(node.Source, cloneCommentGuid: false) as Expression;

            simplifiedNode = CreateAssignmentInstruction(new List<QualifiedName>() { AssignmentTarget }, Source);

            return true;
        }

        private static bool SimplifyPrecursorInstruction(PrecursorInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = CreateSimpleQualifiedName("precursor");
            IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);

            simplifiedNode = CreateCommandInstruction(Command, ClonedArgumentBlocks);

            return true;
        }

        private static bool SimplifyRaiseEventInstruction(RaiseEventInstruction node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleCommandInstruction(node.QueryIdentifier.Text);

            return true;
        }

        private static bool SimplifyReleaseInstruction(ReleaseInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = DeepCloneNode(node.EntityName, cloneCommentGuid: false) as QualifiedName;
            simplifiedNode = CreateCommandInstruction(Command, new List<Argument>());

            return true;
        }

        private static bool SimplifyThrowInstruction(ThrowInstruction node, out Node simplifiedNode)
        {
            QualifiedName Command = CreateSimpleQualifiedName("precursor");
            IBlockList<Argument> ClonedArgumentBlocks = BlockListHelper<Argument>.CreateBlockListCopy(node.ArgumentBlocks);

            simplifiedNode = CreateCommandInstruction(Command, ClonedArgumentBlocks);

            return true;
        }

        private static bool SimplifyAnchoredType(AnchoredType node, out Node simplifiedNode)
        {
            Debug.Assert(node.AnchoredName.Path.Count > 0, "The path of an anchor is never empty");
            simplifiedNode = CreateSimpleSimpleType(node.AnchoredName.Path[0].Text);
            return true;
        }

        private static bool SimplifyKeywordAnchoredType(KeywordAnchoredType node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleSimpleType(node.Anchor.ToString());
            return true;
        }

        private static bool SimplifyFunctionType(FunctionType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyGenericType(GenericType node, out Node simplifiedNode)
        {
            string Text = node.ClassIdentifier.Text;

            if (BlockListHelper<TypeArgument>.IsSimple(node.TypeArgumentBlocks))
            {
                TypeArgument FirstArgument = node.TypeArgumentBlocks.NodeBlockList[0].NodeList[0];
                if (FirstArgument is PositionalTypeArgument AsPositionalTypeArgument && AsPositionalTypeArgument.Source is SimpleType AsSimpleType)
                {
                    Text += AsSimpleType.ClassIdentifier.Text;
                }
            }

            simplifiedNode = CreateSimpleSimpleType(Text);
            return true;
        }

        private static bool SimplifyIndexerType(IndexerType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyPropertyType(PropertyType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyProcedureType(ProcedureType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyTupleType(TupleType node, out Node simplifiedNode)
        {
            EntityDeclaration FirstField = node.EntityDeclarationBlocks.NodeBlockList[0].NodeList[0];
            simplifiedNode = DeepCloneNode(FirstField.EntityType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyAssignmentTypeArgument(AssignmentTypeArgument node, out Node simplifiedNode)
        {
            simplifiedNode = CreatePositionalTypeArgument(node.Source);
            return true;
        }

        private static bool SimplifyPositionalTypeArgument(PositionalTypeArgument node, out Node simplifiedNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            simplifiedNode = null;
            return true;
        }

        private static QualifiedName StringToQualifiedName(string text)
        {
            string[] StringList;
            ParseDotSeparatedIdentifiers(text, out StringList);

            List<Identifier> IdentifierList = new List<Identifier>();
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

        private static bool GetExpressionText(Expression expressionNode, out string text)
        {
            ManifestNumberExpression AsNumber;
            QueryExpression AsQuery;

            if ((AsNumber = expressionNode as ManifestNumberExpression) != null)
            {
                text = AsNumber.Text;
                return true;
            }
            else if ((AsQuery = expressionNode as QueryExpression) != null)
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
