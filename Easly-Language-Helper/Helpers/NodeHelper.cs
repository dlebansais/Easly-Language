using BaseNode;
using Easly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace BaseNodeHelper
{
    public static class NodeHelper
    {
        #region Building Blocks
        public static IDocument CreateEmptyDocumentation()
        {
            Document Documentation = new Document();
            Documentation.Comment = "";
            Documentation.Uuid = Guid.NewGuid();

            return Documentation;
        }

        public static IDocument CreateSimpleDocumentation(string comment, Guid guid)
        {
            Document Documentation = new Document();
            Documentation.Comment = comment;
            Documentation.Uuid = guid;

            return Documentation;
        }

        public static IPattern CreateEmptyPattern()
        {
            Pattern EmptyPattern = new Pattern();
            EmptyPattern.Documentation = CreateEmptyDocumentation();
            EmptyPattern.Text = "*";

            return EmptyPattern;
        }

        public static IPattern CreateSimplePattern(string patternText)
        {
            Pattern SimplePattern = new Pattern();
            SimplePattern.Documentation = CreateEmptyDocumentation();
            SimplePattern.Text = patternText;

            return SimplePattern;
        }

        public static IIdentifier CreateEmptyIdentifier()
        {
            Identifier EmptyIdentifier = new Identifier();
            EmptyIdentifier.Documentation = CreateEmptyDocumentation();
            EmptyIdentifier.Text = "";

            return EmptyIdentifier;
        }

        public static IIdentifier CreateSimpleIdentifier(string identifierText)
        {
            Identifier SimpleIdentifier = new Identifier();
            SimpleIdentifier.Documentation = CreateEmptyDocumentation();
            SimpleIdentifier.Text = identifierText;

            return SimpleIdentifier;
        }

        public static IName CreateEmptyName()
        {
            Name EmptyName = new Name();
            EmptyName.Documentation = CreateEmptyDocumentation();
            EmptyName.Text = "";

            return EmptyName;
        }

        public static IName CreateSimpleName(string nameText)
        {
            Name SimpleName = new Name();
            SimpleName.Documentation = CreateEmptyDocumentation();
            SimpleName.Text = nameText;

            return SimpleName;
        }

        public static IQualifiedName CreateEmptyQualifiedName()
        {
            List<IIdentifier> Path = new List<IIdentifier>();
            Path.Add(CreateEmptyIdentifier());
            return CreateQualifiedName(Path);
        }

        public static IQualifiedName CreateSimpleQualifiedName(string identifierText)
        {
            List<IIdentifier> Path = new List<IIdentifier>();
            Path.Add(CreateSimpleIdentifier(identifierText));
            return CreateQualifiedName(Path);
        }

        public static IQualifiedName CreateQualifiedName(IList<IIdentifier> path)
        {
            Debug.Assert(path.Count > 0);

            QualifiedName DefaultQualifiedName = new QualifiedName();
            DefaultQualifiedName.Documentation = CreateEmptyDocumentation();
            DefaultQualifiedName.Path = path;

            return DefaultQualifiedName;
        }

        public static IExpression CreateEmptyQueryExpression()
        {
            QueryExpression EmptyQueryExpression = new QueryExpression();
            EmptyQueryExpression.Documentation = CreateEmptyDocumentation();
            EmptyQueryExpression.Query = CreateEmptyQualifiedName();
            EmptyQueryExpression.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateEmptyBlockList();

            return EmptyQueryExpression;
        }

        public static IExpression CreateSimpleQueryExpression(string queryText)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = CreateSimpleQualifiedName(queryText);
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateEmptyBlockList();

            return SimpleQueryExpression;
        }

        public static IInstruction CreateEmptyCommandInstruction()
        {
            CommandInstruction EmptyCommandInstruction = new CommandInstruction();
            EmptyCommandInstruction.Documentation = CreateEmptyDocumentation();
            EmptyCommandInstruction.Command = CreateEmptyQualifiedName();
            EmptyCommandInstruction.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateEmptyBlockList();

            return EmptyCommandInstruction;
        }

        public static IInstruction CreateSimpleCommandInstruction(string commandText)
        {
            CommandInstruction SimpleCommandInstruction = new CommandInstruction();
            SimpleCommandInstruction.Documentation = CreateEmptyDocumentation();
            SimpleCommandInstruction.Command = CreateSimpleQualifiedName(commandText);
            SimpleCommandInstruction.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateEmptyBlockList();

            return SimpleCommandInstruction;
        }

        public static IPositionalArgument CreateEmptyPositionalArgument()
        {
            PositionalArgument EmptyPositionalArgument = new PositionalArgument();
            EmptyPositionalArgument.Documentation = CreateEmptyDocumentation();
            EmptyPositionalArgument.Source = CreateDefaultExpression();

            return EmptyPositionalArgument;
        }

        public static IPositionalArgument CreateSimplePositionalArgument(string queryText)
        {
            PositionalArgument SimplePositionalArgument = new PositionalArgument();
            SimplePositionalArgument.Documentation = CreateEmptyDocumentation();
            SimplePositionalArgument.Source = CreateSimpleQueryExpression(queryText);

            return SimplePositionalArgument;
        }

        public static IAssignmentArgument CreateEmptyAssignmentArgument()
        {
            IIdentifier Parameter = CreateEmptyIdentifier();

            AssignmentArgument EmptyAssignmentArgument = new AssignmentArgument();
            EmptyAssignmentArgument.Documentation = CreateEmptyDocumentation();
            EmptyAssignmentArgument.ParameterBlocks = BlockListHelper<IIdentifier, Identifier>.CreateSimpleBlockList(Parameter);
            EmptyAssignmentArgument.Source = CreateDefaultExpression();

            return EmptyAssignmentArgument;
        }

        public static IAssignmentArgument CreateSimpleAssignmentArgument(string identifierText, string queryText)
        {
            IIdentifier Parameter = CreateSimpleIdentifier(identifierText);

            AssignmentArgument SimpleAssignmentArgument = new AssignmentArgument();
            SimpleAssignmentArgument.Documentation = CreateEmptyDocumentation();
            SimpleAssignmentArgument.ParameterBlocks = BlockListHelper<IIdentifier, Identifier>.CreateSimpleBlockList(Parameter);
            SimpleAssignmentArgument.Source = CreateSimpleQueryExpression(queryText);

            return SimpleAssignmentArgument;
        }

        public static IPositionalTypeArgument CreateEmptyPositionalTypeArgument()
        {
            PositionalTypeArgument EmptyPositionalTypeArgument = new PositionalTypeArgument();
            EmptyPositionalTypeArgument.Documentation = CreateEmptyDocumentation();
            EmptyPositionalTypeArgument.Source = CreateEmptySimpleType();

            return EmptyPositionalTypeArgument;
        }

        public static IPositionalTypeArgument CreateSimplePositionalTypeArgument(string typeText)
        {
            PositionalTypeArgument SimplePositionalTypeArgument = new PositionalTypeArgument();
            SimplePositionalTypeArgument.Documentation = CreateEmptyDocumentation();
            SimplePositionalTypeArgument.Source = CreateSimpleSimpleType(typeText);

            return SimplePositionalTypeArgument;
        }

        public static ISimpleType CreateEmptySimpleType()
        {
            SimpleType EmptySimpleType = new SimpleType();
            EmptySimpleType.Documentation = CreateEmptyDocumentation();
            EmptySimpleType.ClassIdentifier = CreateEmptyIdentifier();

            return EmptySimpleType;
        }

        public static ISimpleType CreateSimpleSimpleType(string identifierText)
        {
            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.ClassIdentifier = CreateSimpleIdentifier(identifierText);

            return SimpleSimpleType;
        }

        public static IScope CreateEmptyScope()
        {
            Scope EmptyScope = new Scope();
            EmptyScope.Documentation = CreateEmptyDocumentation();
            EmptyScope.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyScope.InstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();

            return EmptyScope;
        }

        public static IConditional CreateEmptyConditional()
        {
            Conditional EmptyConditional = new Conditional();
            EmptyConditional.Documentation = CreateEmptyDocumentation();
            EmptyConditional.BooleanExpression = CreateDefaultExpression();
            EmptyConditional.Instructions = CreateEmptyScope();

            return EmptyConditional;
        }

        public static IQueryOverload CreateEmptyQueryOverload()
        {
            IEntityDeclaration FirstResult = CreateEmptyEntityDeclaration();

            QueryOverload EmptyQueryOverload = new QueryOverload();
            EmptyQueryOverload.Documentation = CreateEmptyDocumentation();
            EmptyQueryOverload.ParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyQueryOverload.ParameterEnd = ParameterEndStatus.Closed;
            EmptyQueryOverload.ResultBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(FirstResult);
            EmptyQueryOverload.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            EmptyQueryOverload.Variant = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());
            EmptyQueryOverload.QueryBody = CreateDefaultBody();

            return EmptyQueryOverload;
        }

        public static IQueryOverloadType CreateEmptyQueryOverloadType()
        {
            QueryOverloadType EmptyQueryOverloadType = new QueryOverloadType();
            EmptyQueryOverloadType.Documentation = CreateEmptyDocumentation();
            EmptyQueryOverloadType.ParameterBlocks  = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyQueryOverloadType.ParameterEnd = ParameterEndStatus.Closed;
            EmptyQueryOverloadType.ResultBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyQueryOverloadType.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            EmptyQueryOverloadType.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            EmptyQueryOverloadType.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return EmptyQueryOverloadType;
        }

        public static ICommandOverloadType CreateEmptyCommandOverloadType()
        {
            CommandOverloadType EmptyCommandOverloadType = new CommandOverloadType();
            EmptyCommandOverloadType.Documentation = CreateEmptyDocumentation();
            EmptyCommandOverloadType.ParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyCommandOverloadType.ParameterEnd = ParameterEndStatus.Closed;
            EmptyCommandOverloadType.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            EmptyCommandOverloadType.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            EmptyCommandOverloadType.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return EmptyCommandOverloadType;
        }

        public static IEntityDeclaration CreateEmptyEntityDeclaration()
        {
            EntityDeclaration EmptyEntityDeclaration = new EntityDeclaration();
            EmptyEntityDeclaration.Documentation = CreateEmptyDocumentation();
            EmptyEntityDeclaration.EntityName = CreateEmptyName();
            EmptyEntityDeclaration.EntityType = CreateDefaultType();
            EmptyEntityDeclaration.DefaultValue = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());

            return EmptyEntityDeclaration;
        }

        public static ICommandOverload CreateEmptyCommandOverload()
        {
            CommandOverload EmptyCommandOverload = new CommandOverload();
            EmptyCommandOverload.Documentation = CreateEmptyDocumentation();
            EmptyCommandOverload.ParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyCommandOverload.ParameterEnd = ParameterEndStatus.Closed;
            EmptyCommandOverload.CommandBody = CreateDefaultBody();

            return EmptyCommandOverload;
        }
        #endregion

        #region Default Objects
        public static IArgument CreateDefaultArgument()
        {
            return CreateEmptyPositionalArgument();
        }

        public static ITypeArgument CreateDefaultTypeArgument()
        {
            return CreateEmptyPositionalTypeArgument();
        }

        public static IBody CreateDefaultBody()
        {
            return CreateEmptyEffectiveBody();
        }

        public static IExpression CreateDefaultExpression()
        {
            return CreateEmptyQueryExpression();
        }

        public static IInstruction CreateDefaultInstruction()
        {
            return CreateEmptyCommandInstruction();
        }

        public static IObjectType CreateDefaultType()
        {
            return CreateEmptySimpleType();
        }

        public static INode CreateDefault(Type interfaceType)
        {
            INode Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
                return Result;
            else
                throw new ArgumentOutOfRangeException($"{nameof(interfaceType)}: {interfaceType.FullName}");
        }

        private static INode CreateDefaultNoCheck(Type interfaceType)
        {
            if (interfaceType == typeof(IArgument))
                return CreateDefaultArgument();
            else if (interfaceType == typeof(ITypeArgument))
                return CreateDefaultTypeArgument();
            else if (interfaceType == typeof(IBody))
                return CreateDefaultBody();
            else if (interfaceType == typeof(IExpression))
                return CreateDefaultExpression();
            else if (interfaceType == typeof(IInstruction))
                return CreateDefaultInstruction();
            else if (interfaceType == typeof(IObjectType))
                return CreateDefaultType();
            else if (interfaceType == typeof(IName))
                return CreateEmptyName();
            else if (interfaceType == typeof(IIdentifier))
                return CreateEmptyIdentifier();
            else if (interfaceType == typeof(IQualifiedName))
                return CreateEmptyQualifiedName();
            else if (interfaceType == typeof(IScope))
                return CreateEmptyScope();
            else if (interfaceType == typeof(IImport))
                return CreateSimpleImport("", "", ImportType.Latest);
            else
                return null;
        }

        public static INode CreateDefaultFromInterface(Type interfaceType)
        {
            INode Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
                return Result;

            string NamePrefix = interfaceType.AssemblyQualifiedName;
            NamePrefix = NamePrefix.Substring(0, NamePrefix.IndexOf('.') + 1);

            string NodeTypeName = interfaceType.AssemblyQualifiedName;
            NodeTypeName = NodeTypeName.Replace(NamePrefix + "I", NamePrefix);

            Type NodeType = Type.GetType(NodeTypeName);

            Result = CreateEmptyNode(NodeType);
            Debug.Assert(Result != null);

            return Result;
        }

        public static bool IsNodeType(Type type)
        {
            while (type != null && type != typeof(Node))
                type = type.BaseType;

            return type != null;
        }

        public static INode CreateEmptyNode(Type objectType)
        {
            Debug.Assert(IsNodeType(objectType));

            INode EmptyNode = objectType.Assembly.CreateInstance(objectType.FullName) as INode;
            Debug.Assert(EmptyNode != null);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(EmptyNode);

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(EmptyNode, PropertyName, out ChildNodeType))
                    InitializeChildNode(EmptyNode, PropertyName, CreateDefaultFromInterface(ChildNodeType));

                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(EmptyNode, PropertyName, out ChildNodeType))
                    InitializeUnassignedOptionalChildNode(EmptyNode, PropertyName);

                else if (NodeTreeHelperList.IsNodeListProperty(EmptyNode, PropertyName, out ChildNodeType))
                    if (IsCollectionNeverEmpty(EmptyNode, PropertyName))
                    {
                        Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildNodeType);

                        INode FirstNode;
                        if (NodeType.IsAbstract)
                            FirstNode = CreateDefault(ChildNodeType);
                        else
                            FirstNode = CreateEmptyNode(NodeType);

                        InitializeSimpleNodeList(EmptyNode, PropertyName, ChildNodeType, FirstNode);
                    }
                    else
                        InitializeEmptyNodeList(EmptyNode, PropertyName, ChildNodeType);

                else if (NodeTreeHelperBlockList.IsBlockListProperty(EmptyNode, PropertyName, out ChildInterfaceType, out ChildNodeType))
                    if (IsCollectionNeverEmpty(EmptyNode, PropertyName))
                    {
                        Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildInterfaceType);

                        INode FirstNode;
                        if (NodeType.IsAbstract)
                            FirstNode = CreateDefault(ChildInterfaceType);
                        else
                            FirstNode = CreateEmptyNode(NodeType);

                        InitializeSimpleBlockList(EmptyNode, PropertyName, ChildInterfaceType, ChildNodeType, FirstNode);
                    }
                    else
                        InitializeEmptyBlockList(EmptyNode, PropertyName, ChildInterfaceType, ChildNodeType);

                else if (NodeTreeHelper.IsStringProperty(EmptyNode, PropertyName))
                    NodeTreeHelper.SetStringProperty(EmptyNode, PropertyName, "");
            }

            InitializeDocumentation(EmptyNode);

            return EmptyNode;
        }

        public static bool IsEmptyNode(INode node)
        {
            Debug.Assert(node != null);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node.GetType());

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out INode ChildNode);
                    if (!IsEmptyNode(ChildNode))
                        return false;
                }

                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out INode ChildNode);
                    if (IsAssigned)
                        return false;
                }

                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<INode> ChildNodeList);

                    if (IsCollectionNeverEmpty(node, PropertyName))
                    {
                        Debug.Assert(ChildNodeList.Count > 0);

                        if (ChildNodeList.Count != 1)
                            return false;

                        INode ChildNode = ChildNodeList[0];
                        if (!IsEmptyNode(ChildNode))
                            return false;
                    }
                    else if (ChildNodeList.Count > 0)
                        return false;
                }

                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, PropertyName, out ChildInterfaceType, out ChildNodeType))
                {
                    NodeTreeHelperBlockList.GetChildBlockList(node, PropertyName, out IReadOnlyList<INodeTreeBlock> ChildBlockList);

                    if (IsCollectionNeverEmpty(node, PropertyName))
                    {
                        Debug.Assert(ChildBlockList.Count > 0);

                        if (ChildBlockList.Count != 1)
                            return false;

                        INodeTreeBlock FirstBlock = ChildBlockList[0];
                        Debug.Assert(FirstBlock.NodeList.Count > 0);

                        if (FirstBlock.NodeList.Count != 1)
                            return false;

                        INode ChildNode = FirstBlock.NodeList[0];
                        if (!IsEmptyNode(ChildNode))
                            return false;
                    }
                    else if (ChildBlockList.Count > 0)
                        return false;
                }

                else if (NodeTreeHelper.IsStringProperty(node, PropertyName))
                {
                    string Text = NodeTreeHelper.GetString(node, PropertyName);
                    Debug.Assert(Text != null);

                    if (Text.Length > 0)
                        return false;
                }

                else if (NodeTreeHelper.IsBooleanProperty(node, PropertyName) || NodeTreeHelper.IsEnumProperty(node, PropertyName))
                {
                    int Value = NodeTreeHelper.GetEnumValue(node, PropertyName);
                    NodeTreeHelper.GetEnumRange(node.GetType(), PropertyName, out int Min, out int Max);

                    if (Value != Min)
                        return false;
                }
            }

            return true;
        }
        #endregion

        #region Specific Objects
        #region Argument
        public static IAssignmentArgument CreateAssignmentArgument(List<IIdentifier> parameterList, IExpression source)
        {
            Debug.Assert(parameterList.Count > 0);

            AssignmentArgument Result = new AssignmentArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockList(parameterList);
            Result.Source = source;

            return Result;
        }

        public static IPositionalArgument CreatePositionalArgument(IExpression source)
        {
            PositionalArgument Result = new PositionalArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }
        #endregion
        #region Body
        public static IDeferredBody CreateEmptyDeferredBody()
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IEffectiveBody CreateEmptyEffectiveBody()
        {
            EffectiveBody Result = new EffectiveBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            Result.BodyInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.ExceptionHandlerBlocks = BlockListHelper<IExceptionHandler, ExceptionHandler>.CreateEmptyBlockList();

            return Result;
        }

        public static IExternBody CreateEmptyExternBody()
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IPrecursorBody CreateEmptyPrecursorBody()
        {
            PrecursorBody Result = new PrecursorBody();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        public static IBody CreateInitializedBody(Type nodeType, IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks, IBlockList<IEntityDeclaration, EntityDeclaration> entityDeclarationBlocks, IBlockList<IInstruction, Instruction> bodyInstructionBlocks, IBlockList<IExceptionHandler, ExceptionHandler> exceptionHandlerBlocks, IOptionalReference<IObjectType> ancestorType)
        {
            if (nodeType == typeof(DeferredBody))
                return CreateInitializedDeferredBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks);
            else if (nodeType == typeof(EffectiveBody))
                return CreateInitializedEffectiveBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks, entityDeclarationBlocks, bodyInstructionBlocks, exceptionHandlerBlocks);
            else if (nodeType == typeof(ExternBody))
                return CreateInitializedExternBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks);
            else if (nodeType == typeof(PrecursorBody))
                return CreateInitializedPrecursorBody(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks, ancestorType);
            else
                throw new ArgumentOutOfRangeException($"{nameof(nodeType)}: {nodeType.FullName}");
        }

        public static IDeferredBody CreateInitializedDeferredBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks)
        {
            DeferredBody Result = new DeferredBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        public static IEffectiveBody CreateInitializedEffectiveBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks, IBlockList<IEntityDeclaration, EntityDeclaration> entityDeclarationBlocks, IBlockList<IInstruction, Instruction> bodyInstructionBlocks, IBlockList<IExceptionHandler, ExceptionHandler> exceptionHandlerBlocks)
        {
            EffectiveBody Result = new EffectiveBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateBlockListCopy(entityDeclarationBlocks);
            Result.BodyInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateBlockListCopy(bodyInstructionBlocks);
            Result.ExceptionHandlerBlocks = BlockListHelper<IExceptionHandler, ExceptionHandler>.CreateBlockListCopy(exceptionHandlerBlocks);

            return Result;
        }

        public static IExternBody CreateInitializedExternBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks)
        {
            ExternBody Result = new ExternBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);

            return Result;
        }

        public static IPrecursorBody CreateInitializedPrecursorBody(IDocument documentation, IBlockList<IAssertion, Assertion> requireBlocks, IBlockList<IAssertion, Assertion> ensureBlocks, IBlockList<IIdentifier, Identifier> exceptionIdentifierBlocks, IOptionalReference<IObjectType> ancestorType)
        {
            PrecursorBody Result = new PrecursorBody();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.RequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(requireBlocks);
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);
            Result.ExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(exceptionIdentifierBlocks);
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReferenceCopy(ancestorType);

            return Result;
        }
        #endregion
        #region Expression
        public static IAgentExpression CreateAgentExpression(IIdentifier delegated)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        public static IAssertionTagExpression CreateAssertionTagExpression(IIdentifier tagIdentifier)
        {
            AssertionTagExpression Result = new AssertionTagExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.TagIdentifier = tagIdentifier;

            return Result;
        }

        public static IBinaryConditionalExpression CreateBinaryConditionalExpression(IExpression leftExpression, ConditionalTypes conditional, IExpression rightExpression)
        {
            BinaryConditionalExpression Result = new BinaryConditionalExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Conditional = conditional;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IBinaryOperatorExpression CreateBinaryOperatorExpression(IExpression leftExpression, IIdentifier operatorName, IExpression rightExpression)
        {
            BinaryOperatorExpression Result = new BinaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IClassConstantExpression CreateClassConstantExpression(IIdentifier classIdentifier, IIdentifier constantIdentifier)
        {
            ClassConstantExpression Result = new ClassConstantExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.ConstantIdentifier = constantIdentifier;

            return Result;
        }

        public static ICloneOfExpression CreateCloneOfExpression(IExpression source)
        {
            CloneOfExpression Result = new CloneOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Type = CloneType.Shallow;
            Result.Source = source;

            return Result;
        }

        public static IEntityExpression CreateEntityExpression(IQualifiedName query)
        {
            EntityExpression Result = new EntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        public static IEqualityExpression CreateEqualityExpression(IExpression leftExpression, ComparisonType comparison, IExpression rightExpression)
        {
            EqualityExpression Result = new EqualityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Comparison = comparison;
            Result.Equality = EqualityType.Physical;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IIndexQueryExpression CreateIndexQueryExpression(IExpression indexedExpression, List<IArgument> argumentList)
        {
            Debug.Assert(argumentList.Count > 0);

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IInitializedObjectExpression CreateInitializedObjectExpression(IIdentifier classIdentifier, List<IAssignmentArgument> assignmentArgumentList)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = BlockListHelper<IAssignmentArgument, AssignmentArgument>.CreateBlockList(assignmentArgumentList);

            return Result;
        }

        public static IKeywordExpression CreateKeywordExpression(Keyword value)
        {
            KeywordExpression Result = new KeywordExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static IManifestCharacterExpression CreateManifestCharacterExpression(string text)
        {
            ManifestCharacterExpression Result = new ManifestCharacterExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        public static IManifestNumberExpression CreateDefaultManifestNumberExpression()
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = "0";

            return Result;
        }

        public static IManifestNumberExpression CreateSimpleManifestNumberExpression(string numberText)
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = numberText;

            return Result;
        }

        public static IManifestStringExpression CreateManifestStringExpression(string text)
        {
            ManifestStringExpression Result = new ManifestStringExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        public static INewExpression CreateNewExpression(IQualifiedName objectName)
        {
            NewExpression Result = new NewExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Object = objectName;

            return Result;
        }

        public static IOldExpression CreateOldExpression(IQualifiedName query)
        {
            OldExpression Result = new OldExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        public static IPrecursorExpression CreatePrecursorExpression(List<IArgument> argumentList)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IPrecursorIndexExpression CreatePrecursorIndexExpression(List<IArgument> argumentList)
        {
            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IPreprocessorExpression CreatePreprocessorExpression(PreprocessorMacro value)
        {
            PreprocessorExpression Result = new PreprocessorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static IQueryExpression CreateQueryExpression(IQualifiedName query, List<IArgument> argumentList)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return SimpleQueryExpression;
        }

        public static IQueryExpression CreateQueryExpression(IQualifiedName query, IBlockList<IArgument, Argument> argumentBlocks)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = argumentBlocks;

            return SimpleQueryExpression;
        }

        public static IResultOfExpression CreateResultOfExpression(IExpression source)
        {
            ResultOfExpression Result = new ResultOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }

        public static IUnaryNotExpression CreateUnaryNotExpression(IExpression rightExpression)
        {
            UnaryNotExpression Result = new UnaryNotExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IUnaryOperatorExpression CreateUnaryOperatorExpression(IIdentifier operatorName, IExpression rightExpression)
        {
            UnaryOperatorExpression Result = new UnaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }
        #endregion
        #region Feature
        public static IAttributeFeature CreateEmptyAttributeFeature()
        {
            AttributeFeature Result = new AttributeFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.EntityType = CreateDefaultType();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();

            return Result;
        }

        public static IConstantFeature CreateEmptyConstantFeature()
        {
            ConstantFeature Result = new ConstantFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.EntityType = CreateDefaultType();
            Result.ConstantValue = CreateDefaultExpression();

            return Result;
        }

        public static ICreationFeature CreateEmptyCreationFeature()
        {
            ICommandOverload FirstOverload = CreateEmptyCommandOverload();

            CreationFeature Result = new CreationFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IFunctionFeature CreateEmptyFunctionFeature()
        {
            IQueryOverload FirstOverload = CreateEmptyQueryOverload();

            FunctionFeature Result = new FunctionFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.Once = OnceChoice.Normal;
            Result.OverloadBlocks = BlockListHelper<IQueryOverload, QueryOverload>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IIndexerFeature CreateEmptyIndexerFeature()
        {
            IEntityDeclaration FirstParameter = CreateEmptyEntityDeclaration();

            IndexerFeature Result = new IndexerFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityType = CreateDefaultType();
            Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(FirstParameter);
            Result.ParameterEnd = ParameterEndStatus.Closed;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());
            Result.GetterBody.Assign();
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());

            return Result;
        }

        public static IProcedureFeature CreateEmptyProcedureFeature()
        {
            ICommandOverload FirstOverload = CreateEmptyCommandOverload();

            ProcedureFeature Result = new ProcedureFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IPropertyFeature CreateEmptyPropertyFeature()
        {
            PropertyFeature Result = new PropertyFeature();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExportIdentifier = CreateEmptyExportIdentifier();
            Result.Export = ExportStatus.Exported;
            Result.EntityName = CreateEmptyName();
            Result.EntityType = CreateDefaultType();
            Result.PropertyKind = UtilityType.ReadOnly;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReference(CreateDefaultBody());

            return Result;
        }

        public static IFeature CreateInitializedFeature(Type nodeType, IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, IBlockList<IAssertion, Assertion> ensureBlocks, IExpression constantValue, IBlockList<ICommandOverload, CommandOverload> commandOverloadBlocks, OnceChoice once, IBlockList<IQueryOverload, QueryOverload> queryOverloadBlocks, UtilityType propertyKind, IBlockList<IIdentifier, Identifier> modifiedQueryBlocks, IOptionalReference<IBody> getterBody, IOptionalReference<IBody> setterBody, IBlockList<IEntityDeclaration, EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
        {
            if (nodeType == typeof(AttributeFeature))
                return CreateInitializedAttributeFeature(documentation, exportIdentifier, export, entityName, entityType, ensureBlocks);
            else if (nodeType == typeof(ConstantFeature))
                return CreateInitializedConstantFeature(documentation, exportIdentifier, export, entityName, entityType, constantValue);
            else if (nodeType == typeof(CreationFeature))
                return CreateInitializedCreationFeature(documentation, exportIdentifier, export, entityName, commandOverloadBlocks);
            else if (nodeType == typeof(FunctionFeature))
                return CreateInitializedFunctionFeature(documentation, exportIdentifier, export, entityName, once, queryOverloadBlocks);
            else if (nodeType == typeof(ProcedureFeature))
                return CreateInitializedProcedureFeature(documentation, exportIdentifier, export, entityName, commandOverloadBlocks);
            else if (nodeType == typeof(PropertyFeature))
                return CreateInitializedPropertyFeature(documentation, exportIdentifier, export, entityName, entityType, propertyKind, modifiedQueryBlocks, getterBody, setterBody);
            else if (nodeType == typeof(IndexerFeature))
                return CreateInitializedIndexerFeature(documentation, exportIdentifier, export, entityType, modifiedQueryBlocks, getterBody, setterBody, indexParameterBlocks, parameterEnd);
            else
                throw new ArgumentOutOfRangeException($"{nameof(nodeType)}: {nodeType.FullName}");
        }

        public static IAttributeFeature CreateInitializedAttributeFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, IBlockList<IAssertion, Assertion> ensureBlocks)
        {
            AttributeFeature Result = new AttributeFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            Result.EnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateBlockListCopy(ensureBlocks);

            return Result;
        }

        public static IConstantFeature CreateInitializedConstantFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, IExpression constantValue)
        {
            ConstantFeature Result = new ConstantFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            Result.ConstantValue = constantValue != null ? DeepCloneNode(constantValue, cloneCommentGuid: false) as IExpression : CreateDefaultExpression();

            return Result;
        }

        public static ICreationFeature CreateInitializedCreationFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IBlockList<ICommandOverload, CommandOverload> commandOverloadBlocks)
        {
            CreationFeature Result = new CreationFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            if (commandOverloadBlocks != null)
            {
                Debug.Assert(commandOverloadBlocks.NodeBlockList.Count > 0);
                Debug.Assert(commandOverloadBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateBlockListCopy(commandOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(CreateEmptyCommandOverload());

            return Result;
        }

        public static IFunctionFeature CreateInitializedFunctionFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, OnceChoice once, IBlockList<IQueryOverload, QueryOverload> queryOverloadBlocks)
        {
            FunctionFeature Result = new FunctionFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.Once = once;
            if (queryOverloadBlocks != null)
            {
                Debug.Assert(queryOverloadBlocks.NodeBlockList.Count > 0);
                Debug.Assert(queryOverloadBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.OverloadBlocks = BlockListHelper<IQueryOverload, QueryOverload>.CreateBlockListCopy(queryOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<IQueryOverload, QueryOverload>.CreateSimpleBlockList(CreateEmptyQueryOverload());

            return Result;
        }

        public static IIndexerFeature CreateInitializedIndexerFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IObjectType entityType, IBlockList<IIdentifier, Identifier> modifiedQueryBlocks, IOptionalReference<IBody> getterBody, IOptionalReference<IBody> setterBody, IBlockList<IEntityDeclaration, EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd)
        {
            IndexerFeature Result = new IndexerFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            if (indexParameterBlocks != null)
            {
                Debug.Assert(indexParameterBlocks.NodeBlockList.Count > 0);
                Debug.Assert(indexParameterBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateBlockListCopy(indexParameterBlocks);
            }
            else
                Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(CreateEmptyEntityDeclaration());
            Result.ParameterEnd = parameterEnd;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(setterBody);

            return Result;
        }

        public static IProcedureFeature CreateInitializedProcedureFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IBlockList<ICommandOverload, CommandOverload> commandOverloadBlocks)
        {
            ProcedureFeature Result = new ProcedureFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            if (commandOverloadBlocks != null)
            {
                Debug.Assert(commandOverloadBlocks.NodeBlockList.Count > 0);
                Debug.Assert(commandOverloadBlocks.NodeBlockList[0].NodeList.Count > 0);
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateBlockListCopy(commandOverloadBlocks);
            }
            else
                Result.OverloadBlocks = BlockListHelper<ICommandOverload, CommandOverload>.CreateSimpleBlockList(CreateEmptyCommandOverload());

            return Result;
        }

        public static IPropertyFeature CreateInitializedPropertyFeature(IDocument documentation, IIdentifier exportIdentifier, ExportStatus export, IName entityName, IObjectType entityType, UtilityType propertyKind, IBlockList<IIdentifier, Identifier> modifiedQueryBlocks, IOptionalReference<IBody> getterBody, IOptionalReference<IBody> setterBody)
        {
            PropertyFeature Result = new PropertyFeature();
            Result.Documentation = CreateDocumentationCopy(documentation);
            Result.ExportIdentifier = exportIdentifier != null ? DeepCloneNode(exportIdentifier, cloneCommentGuid: false) as IIdentifier : CreateEmptyExportIdentifier();
            Result.Export = export;
            Result.EntityName = entityName != null ? DeepCloneNode(entityName, cloneCommentGuid: false) as IName : CreateEmptyName();
            Result.EntityType = entityType != null ? DeepCloneNode(entityType, cloneCommentGuid: false) as IObjectType : CreateDefaultType();
            Result.PropertyKind = propertyKind;
            Result.ModifiedQueryBlocks = BlockListHelper<IIdentifier, Identifier>.CreateBlockListCopy(modifiedQueryBlocks);
            Result.GetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(getterBody);
            Result.SetterBody = OptionalReferenceHelper<IBody>.CreateReferenceCopy(setterBody);

            return Result;
        }
        #endregion
        #region Instruction
        public static IAsLongAsInstruction CreateAsLongAsInstruction(IExpression continueCondition)
        {
            AsLongAsInstruction Result = new AsLongAsInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ContinueCondition = continueCondition;
            Result.ContinuationBlocks = BlockListHelper<IContinuation, Continuation>.CreateEmptyBlockList();
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IAssignmentInstruction CreateAssignmentInstruction(List<IQualifiedName> assignmentList, IExpression source)
        {
            Debug.Assert(assignmentList.Count > 0);

            AssignmentInstruction Result = new AssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.DestinationBlocks = BlockListHelper<IQualifiedName, QualifiedName>.CreateBlockList(assignmentList);
            Result.Source = source;

            return Result;
        }

        public static IAttachmentInstruction CreateAttachmentInstruction(IExpression source, List<IName> nameList)
        {
            IObjectType AttachType = CreateDefaultType();
            IAttachment FirstAttachment = CreateAttachment(AttachType);

            AttachmentInstruction Result = new AttachmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.EntityNameBlocks = BlockListHelper<IName, Name>.CreateBlockList(nameList);
            Result.AttachmentBlocks = BlockListHelper<IAttachment, Attachment>.CreateSimpleBlockList(FirstAttachment);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static ICheckInstruction CreateCheckInstruction(IExpression booleanExpression)
        {
            CheckInstruction Result = new CheckInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BooleanExpression = booleanExpression;

            return Result;
        }

        public static ICommandInstruction CreateCommandInstruction(IQualifiedName command, List<IArgument> argumentList)
        {
            CommandInstruction Result = new CommandInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Command = command;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static ICreateInstruction CreateCreateInstruction(IIdentifier entityIdentifier, IIdentifier creationRoutineIdentifier, List<IArgument> argumentList)
        {
            CreateInstruction Result = new CreateInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityIdentifier = entityIdentifier;
            Result.CreationRoutineIdentifier = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);
            Result.Processor = OptionalReferenceHelper<IQualifiedName>.CreateReference(CreateEmptyQualifiedName());

            return Result;
        }

        public static IDebugInstruction CreateDebugInstruction()
        {
            DebugInstruction Result = new DebugInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static IForLoopInstruction CreateForLoopInstruction(IExpression whileCondition)
        {
            ForLoopInstruction Result = new ForLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            Result.InitInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.WhileCondition = whileCondition;
            Result.LoopInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.IterationInstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateEmptyBlockList();
            Result.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.Variant = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        public static IIfThenElseInstruction CreateIfThenElseInstruction(IConditional firstConditional)
        {
            IfThenElseInstruction Result = new IfThenElseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ConditionalBlocks = BlockListHelper<IConditional, Conditional>.CreateSimpleBlockList(firstConditional);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IIndexAssignmentInstruction CreateIndexAssignmentInstruction(IQualifiedName destination, List<IArgument> argumentList, IExpression source)
        {
            Debug.Assert(argumentList.Count > 0);

            IndexAssignmentInstruction Result = new IndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);
            Result.Source = source;

            return Result;
        }

        public static IInspectInstruction CreateInspectInstruction(IExpression source)
        {
            IExpression FirstExpression = CreateDefaultManifestNumberExpression();
            IWith FirstWith = CreateWith(FirstExpression);

            InspectInstruction Result = new InspectInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;
            Result.WithBlocks = BlockListHelper<IWith, With>.CreateSimpleBlockList(FirstWith);
            Result.ElseInstructions = OptionalReferenceHelper<IScope>.CreateReference(CreateEmptyScope());

            return Result;
        }

        public static IKeywordAssignmentInstruction CreateKeywordAssignmentInstruction(Keyword destination, IExpression source)
        {
            KeywordAssignmentInstruction Result = new KeywordAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Destination = destination;
            Result.Source = source;

            return Result;
        }

        public static IOverLoopInstruction CreateOverLoopInstruction(IExpression overList, List<IName> nameList)
        {
            Debug.Assert(nameList.Count > 0);

            OverLoopInstruction Result = new OverLoopInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.OverList = overList;
            Result.IndexerBlocks = BlockListHelper<IName, Name>.CreateBlockList(nameList);
            Result.Iteration =  IterationType.Single;
            Result.LoopInstructions = CreateEmptyScope();
            Result.ExitEntityName = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateEmptyIdentifier());
            Result.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();

            return Result;
        }

        public static IPrecursorIndexAssignmentInstruction CreatePrecursorIndexAssignmentInstruction(List<IArgument> argumentList, IExpression source)
        {
            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexAssignmentInstruction Result = new PrecursorIndexAssignmentInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);
            Result.Source = source;

            return Result;
        }

        public static IPrecursorInstruction CreatePrecursorInstruction(List<IArgument> argumentList)
        {
            PrecursorInstruction Result = new PrecursorInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IRaiseEventInstruction CreateRaiseEventInstruction(IIdentifier queryIdentifier)
        {
            RaiseEventInstruction Result = new RaiseEventInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.QueryIdentifier = queryIdentifier;
            Result.Event = EventType.Single;

            return Result;
        }

        public static IReleaseInstruction CreateReleaseInstruction(IQualifiedName entityName)
        {
            ReleaseInstruction Result = new ReleaseInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityName = entityName;

            return Result;
        }

        public static IThrowInstruction CreateThrowInstruction(IObjectType exceptionType, IIdentifier creationRoutineIdentifier, List<IArgument> argumentList)
        {
            ThrowInstruction Result = new ThrowInstruction();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ExceptionType = exceptionType;
            Result.CreationRoutine = creationRoutineIdentifier;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }
        #endregion
        #region Type
        public static IAnchoredType CreateAnchoredType(IQualifiedName anchoredName)
        {
            AnchoredType Result = new AnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AnchoredName = anchoredName;
            Result.AnchorKind = AnchorKinds.Declaration;

            return Result;
        }

        public static IFunctionType CreateFunctionType(IObjectType baseType)
        {
            IQueryOverloadType FirstOverload = CreateEmptyQueryOverloadType();

            FunctionType Result = new FunctionType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = BlockListHelper<IQueryOverloadType, QueryOverloadType>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IGenericType CreateGenericType(IIdentifier classIdentifier, List<ITypeArgument> typeArgumentList)
        {
            Debug.Assert(typeArgumentList.Count > 0);

            GenericType Result = new GenericType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.TypeArgumentBlocks = BlockListHelper<ITypeArgument, TypeArgument>.CreateBlockList(typeArgumentList);

            return Result;
        }

        public static IIndexerType CreateIndexerType(IObjectType baseType, IObjectType entityType)
        {
            IndexerType Result = new IndexerType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.IndexParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            Result.ParameterEnd = ParameterEndStatus.Closed;
            Result.IndexerKind = UtilityType.ReadWrite;
            Result.GetRequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.GetEnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.GetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.SetRequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.SetEnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.SetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static IKeywordAnchoredType CreateKeywordAnchoredType(Keyword anchor)
        {
            KeywordAnchoredType Result = new KeywordAnchoredType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Anchor = anchor;

            return Result;
        }

        public static IProcedureType CreateProcedureType(IObjectType baseType)
        {
            ICommandOverloadType FirstOverload = CreateEmptyCommandOverloadType();

            ProcedureType Result = new ProcedureType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.OverloadBlocks = BlockListHelper<ICommandOverloadType, CommandOverloadType>.CreateSimpleBlockList(FirstOverload);

            return Result;
        }

        public static IPropertyType CreatePropertyType(IObjectType baseType, IObjectType entityType)
        {
            PropertyType Result = new PropertyType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.BaseType = baseType;
            Result.EntityType = entityType;
            Result.PropertyKind = UtilityType.ReadWrite;
            Result.GetEnsureBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.GetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            Result.SetRequireBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            Result.SetExceptionIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return Result;
        }

        public static ISimpleType CreateSimpleType(IIdentifier classIdentifier)
        {
            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.ClassIdentifier = classIdentifier;

            return SimpleSimpleType;
        }

        public static ITupleType CreateTupleType(IEntityDeclaration firstEntityDeclaration)
        {
            TupleType Result = new TupleType();
            Result.Documentation = CreateEmptyDocumentation();
            Result.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(firstEntityDeclaration);

            return Result;
        }
        #endregion
        #region Type Argument
        public static IAssignmentTypeArgument CreateAssignmentTypeArgument(IIdentifier parameterIdentifier, IObjectType source)
        {
            AssignmentTypeArgument Result = new AssignmentTypeArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ParameterIdentifier = parameterIdentifier;
            Result.Source = source;

            return Result;
        }

        public static IPositionalTypeArgument CreatePositionalTypeArgument(IObjectType source)
        {
            PositionalTypeArgument Result = new PositionalTypeArgument();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }
        #endregion
        #region Other
        public static IAttachment CreateAttachment(IObjectType attachType)
        {
            Attachment Result = new Attachment();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AttachTypeBlocks = BlockListHelper<IObjectType, ObjectType>.CreateSimpleBlockList(attachType);
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static IConditional CreateConditional(IExpression booleanExpression)
        {
            Conditional SimpleConditional = new Conditional();
            SimpleConditional.Documentation = CreateEmptyDocumentation();
            SimpleConditional.BooleanExpression = booleanExpression;
            SimpleConditional.Instructions = CreateEmptyScope();

            return SimpleConditional;
        }

        public static IWith CreateWith(IExpression firstExpression)
        {
            IRange FirstRange = CreateRange(firstExpression);

            With Result = new With();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RangeBlocks = BlockListHelper<IRange, Range>.CreateSimpleBlockList(FirstRange);
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static IRange CreateRange(IExpression leftExpression)
        {
            Range Result = new Range();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.RightExpression = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        public static IEntityDeclaration CreateEntityDeclaration(IName entityName, IObjectType entityType)
        {
            EntityDeclaration SimpleEntityDeclaration = new EntityDeclaration();
            SimpleEntityDeclaration.Documentation = CreateEmptyDocumentation();
            SimpleEntityDeclaration.EntityName = entityName;
            SimpleEntityDeclaration.EntityType = entityType;
            SimpleEntityDeclaration.DefaultValue = OptionalReferenceHelper<IExpression>.CreateReference(CreateDefaultExpression());

            return SimpleEntityDeclaration;
        }

        public static IIdentifier CreateEmptyExportIdentifier()
        {
            return CreateSimpleIdentifier("All");
        }

        public static IExport CreateSimpleExport(string nameText)
        {
            Export SimpleExport = new Export();
            SimpleExport.Documentation = CreateEmptyDocumentation();
            SimpleExport.EntityName = CreateSimpleName(nameText);
            SimpleExport.ClassIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return SimpleExport;
        }

        public static IClass CreateSimpleClass(string nameText)
        {
            Guid ClassGuid = Guid.NewGuid();

            Class SimpleClass = new Class();
            SimpleClass.Documentation = CreateEmptyDocumentation();
            SimpleClass.EntityName = CreateSimpleName(nameText);
            SimpleClass.FromIdentifier = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateEmptyIdentifier());
            SimpleClass.CopySpecification = CopySemantic.Reference;
            SimpleClass.Cloneable = CloneableStatus.Cloneable;
            SimpleClass.Comparable = ComparableStatus.Comparable;
            SimpleClass.IsAbstract = false;
            SimpleClass.ImportBlocks = BlockListHelper<IImport, Import>.CreateEmptyBlockList();
            SimpleClass.GenericBlocks = BlockListHelper<IGeneric, Generic>.CreateEmptyBlockList();
            SimpleClass.ExportBlocks = BlockListHelper<IExport, Export>.CreateEmptyBlockList();
            SimpleClass.TypedefBlocks = BlockListHelper<ITypedef, Typedef>.CreateEmptyBlockList();
            SimpleClass.InheritanceBlocks = BlockListHelper<IInheritance, Inheritance>.CreateEmptyBlockList();
            SimpleClass.DiscreteBlocks = BlockListHelper<IDiscrete, Discrete>.CreateEmptyBlockList();
            SimpleClass.ClassReplicateBlocks = BlockListHelper<IClassReplicate, ClassReplicate>.CreateEmptyBlockList();
            SimpleClass.FeatureBlocks = BlockListHelper<IFeature, Feature>.CreateEmptyBlockList();
            SimpleClass.ConversionBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();
            SimpleClass.InvariantBlocks = BlockListHelper<IAssertion, Assertion>.CreateEmptyBlockList();
            SimpleClass.ClassGuid = ClassGuid;
            SimpleClass.ClassPath = "";

            return SimpleClass;
        }

        public static ILibrary CreateSimpleLibrary(string nameText)
        {
            Library SimpleLibrary = new Library();
            SimpleLibrary.Documentation = CreateEmptyDocumentation();
            SimpleLibrary.EntityName = CreateSimpleName(nameText);
            SimpleLibrary.FromIdentifier = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateEmptyIdentifier());
            SimpleLibrary.ImportBlocks = BlockListHelper<IImport, Import>.CreateEmptyBlockList();
            SimpleLibrary.ClassIdentifierBlocks = BlockListHelper<IIdentifier, Identifier>.CreateEmptyBlockList();

            return SimpleLibrary;
        }

        public static IGlobalReplicate CreateSimpleGlobalReplicate(string nameText)
        {
            GlobalReplicate SimpleGlobalReplicate = new GlobalReplicate();
            SimpleGlobalReplicate.Documentation = CreateEmptyDocumentation();
            SimpleGlobalReplicate.ReplicateName = CreateSimpleName(nameText);
            SimpleGlobalReplicate.Patterns = new List<IPattern>();

            IPattern FirstPattern = CreateEmptyPattern();
            SimpleGlobalReplicate.Patterns.Add(FirstPattern);

            return SimpleGlobalReplicate;
        }

        public static IImport CreateSimpleImport(string identifierText, string fromText, ImportType type)
        {
            Import SimpleImport = new Import();
            SimpleImport.Documentation = CreateEmptyDocumentation();
            SimpleImport.LibraryIdentifier = CreateSimpleIdentifier(identifierText);
            SimpleImport.FromIdentifier = OptionalReferenceHelper<IIdentifier>.CreateReference(CreateSimpleIdentifier(fromText));
            SimpleImport.Type = type;
            SimpleImport.RenameBlocks = BlockListHelper<IRename, Rename>.CreateEmptyBlockList();

            return SimpleImport;
        }

        public static IRoot CreateRoot(IList<IClass> classList, IList<ILibrary> libraryList, IList<IGlobalReplicate> globalReplicateList)
        {
            Root EmptyRoot = new Root();
            EmptyRoot.Documentation = CreateEmptyDocumentation();
            EmptyRoot.ClassBlocks = BlockListHelper<IClass, Class>.CreateBlockList(classList);
            EmptyRoot.LibraryBlocks = BlockListHelper<ILibrary, Library>.CreateBlockList(libraryList);
            EmptyRoot.Replicates = globalReplicateList;

            return EmptyRoot;
        }
        #endregion
        #endregion

        #region Initialization
        private static void InitializeDocumentation(INode node)
        {
            IDocument EmptyDocumentation = CreateEmptyDocumentation();
            ((Node)node).Documentation = EmptyDocumentation;
        }

        private static void InitializeChildNode(INode node, string propertyName, INode childNode)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            ItemProperty.SetValue(node, childNode);
        }

        private static void InitializeUnassignedOptionalChildNode(INode node, string propertyName)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
            IOptionalReference EmptyReference = (IOptionalReference)ReferenceType.Assembly.CreateInstance(ReferenceType.FullName);

            ItemProperty.SetValue(node, EmptyReference);
        }

        private static void InitializeOptionalChildNode(INode node, string propertyName, INode childNode)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(IOptionalReference<>).MakeGenericType(Generics);
            IOptionalReference EmptyReference = (IOptionalReference)ReferenceType.Assembly.CreateInstance(ReferenceType.FullName);
            ReferenceType.GetProperty(nameof(IOptionalReference<Node>.Item)).SetValue(EmptyReference, childNode);
            EmptyReference.Unassign();

            ItemProperty.SetValue(node, EmptyReference);
        }

        private static void InitializeEmptyNodeList(INode node, string propertyName, Type childNodeType)
        {
            Type[] Generics = new Type[] { childNodeType };
            Type ListType = typeof(List<>).MakeGenericType(Generics);
            IList EmptyList = (IList)ListType.Assembly.CreateInstance(ListType.FullName);

            node.GetType().GetProperty(propertyName).SetValue(node, EmptyList);
        }

        private static void InitializeSimpleNodeList(INode node, string propertyName, Type childNodeType, INode firstNode)
        {
            InitializeEmptyNodeList(node, propertyName, childNodeType);

            IList NodeList = (IList)node.GetType().GetProperty(propertyName).GetValue(node);
            NodeList.Add(firstNode);
        }

        private static void InitializeEmptyBlockList(INode node, string propertyName, Type childInterfaceType, Type childNodeType)
        {
            Type[] Generics = new Type[] { childInterfaceType, childNodeType };
            Type BlockListType = typeof(BlockList<,>).MakeGenericType(Generics);
            Type BlockType = typeof(Block<,>).MakeGenericType(Generics);

            IBlockList EmptyBlockList = (IBlockList)BlockListType.Assembly.CreateInstance(BlockListType.FullName);

            IDocument EmptyEmptyDocumentation = CreateEmptyDocumentation();
            EmptyBlockList.GetType().GetProperty(nameof(INode.Documentation)).SetValue(EmptyBlockList, EmptyEmptyDocumentation);

            Type ListOfBlockType = typeof(List<>).MakeGenericType(new Type[] { typeof(IBlock<,>).MakeGenericType(Generics) });
            IList EmptyListOfBlock = (IList)ListOfBlockType.Assembly.CreateInstance(ListOfBlockType.FullName);
            EmptyBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).SetValue(EmptyBlockList, EmptyListOfBlock);

            node.GetType().GetProperty(propertyName).SetValue(node, EmptyBlockList);
        }

        private static void InitializeSimpleBlockList(INode node, string propertyName, Type childInterfaceType, Type childNodeType, INode firstNode)
        {
            InitializeEmptyBlockList(node, propertyName, childInterfaceType, childNodeType);

            Type[] Generics = new Type[] { childInterfaceType, childNodeType };
            Type BlockType = typeof(Block<,>).MakeGenericType(Generics);
            IBlock EmptyBlock = (IBlock)BlockType.Assembly.CreateInstance(BlockType.FullName);

            IDocument EmptyEmptyDocumentation = CreateEmptyDocumentation();
            EmptyBlock.GetType().GetProperty(nameof(INode.Documentation)).SetValue(EmptyBlock, EmptyEmptyDocumentation);

            EmptyBlock.GetType().GetProperty(nameof(IBlock.Replication)).SetValue(EmptyBlock, ReplicationStatus.Normal);

            IPattern ReplicationPattern = CreateEmptyPattern();
            EmptyBlock.GetType().GetProperty(nameof(IBlock.ReplicationPattern)).SetValue(EmptyBlock, ReplicationPattern);

            IIdentifier SourceIdentifier = CreateEmptyIdentifier();
            EmptyBlock.GetType().GetProperty(nameof(IBlock.SourceIdentifier)).SetValue(EmptyBlock, SourceIdentifier);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { Generics[0] });
            IList NodeList = (IList)NodeListType.Assembly.CreateInstance(NodeListType.FullName);
            EmptyBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).SetValue(EmptyBlock, NodeList);

            NodeList.Add(firstNode);

            IBlockList BlockList = (IBlockList)node.GetType().GetProperty(propertyName).GetValue(node);

            IList NodeBlockList = (IList)BlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(BlockList, null);
            NodeBlockList.Add(EmptyBlock);
        }
        #endregion

        #region Tools
        public static Type NodeType(string typeName)
        {
            string RootName = typeof(Root).FullName;
            int Index = RootName.LastIndexOf('.');
            string FullTypeName = RootName.Substring(0, Index + 1) + typeName;
            return typeof(Root).Assembly.GetType(FullTypeName);
        }

        public static bool IsOptionalAssignedToDefault(IOptionalReference optional)
        {
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
                    Path = AsQualifiedName.Path;
                    //Debug.Assert(Path.Count > 0);

                    return Path.Count == 1 && Path[0].Text.Length == 0;

                case ISimpleType AsSimpleType:
                    return AsSimpleType.Sharing == SharingType.NotShared && AsSimpleType.ClassIdentifier.Text.Length == 0;

                case IObjectType AsObjectType: // Fallback for other IObjectType.
                    return false;

                case IQueryExpression AsQueryExpression:
                    Path = AsQueryExpression.Query.Path;
                    //Debug.Assert(Path.Count > 0);

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
            //Debug.WriteLine($"{hash1} {hash2}");

            hash1 ^= hash2;
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
                    NodeTreeHelperList.ClearChildNodeList(ClonedRoot, PropertyName);
                    NodeTreeHelperList.GetChildNodeList(root, PropertyName, out IReadOnlyList<INode> ChildNodeList);

                    for (int Index = 0; Index < ChildNodeList.Count; Index++)
                    {
                        INode ChildNode = ChildNodeList[Index];
                        INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                        NodeTreeHelperList.InsertIntoList(ClonedRoot, PropertyName, Index, ClonedChildNode);
                    }
                }

                else if (NodeTreeHelperBlockList.IsBlockListProperty(root, PropertyName, out ChildInterfaceType, out ChildNodeType))
                {
                    NodeTreeHelperBlockList.ClearChildBlockList(ClonedRoot, PropertyName);
                    NodeTreeHelperBlockList.GetChildBlockList(root, PropertyName, out IReadOnlyList<INodeTreeBlock> ChildBlockList);

                    for (int BlockIndex = 0; BlockIndex < ChildBlockList.Count; BlockIndex++)
                    {
                        INodeTreeBlock NodeTreeBlock = ChildBlockList[BlockIndex];
                        NodeTreeHelperBlockList.GetChildBlock(root, PropertyName, BlockIndex, out IBlock Block);
                        Type InterfaceType = NodeTreeHelperBlockList.BlockListInterfaceType(root, PropertyName);

                        IPattern ClonedPattern = (IPattern)DeepCloneNode(NodeTreeBlock.ReplicationPattern, cloneCommentGuid);
                        IIdentifier ClonedSource = (IIdentifier)DeepCloneNode(NodeTreeBlock.SourceIdentifier, cloneCommentGuid);
                        IBlock ClonedBlock = NodeTreeHelperBlockList.CreateBlock(ClonedRoot, PropertyName, Block.Replication, ClonedPattern, ClonedSource);
                        NodeTreeHelperBlockList.InsertIntoBlockList(ClonedRoot, PropertyName, BlockIndex, ClonedBlock);

                        for (int Index = 0; Index < NodeTreeBlock.NodeList.Count; Index++)
                        {
                            INode ChildNode = NodeTreeBlock.NodeList[Index];
                            INode ClonedChildNode = DeepCloneNode(ChildNode, cloneCommentGuid);

                            NodeTreeHelperBlockList.InsertIntoBlock(ClonedBlock, Index, ClonedChildNode);
                        }

                        NodeTreeHelper.CopyDocumentation(Block, ClonedBlock, cloneCommentGuid);
                    }

                    IBlockList BlockList = NodeTreeHelperBlockList.GetBlockList(root, PropertyName);
                    IBlockList ClonedBlockList = NodeTreeHelperBlockList.GetBlockList(ClonedRoot, PropertyName);
                    NodeTreeHelper.CopyDocumentation(BlockList, ClonedBlockList, cloneCommentGuid);
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

        public static IDictionary<Type, TValue> CreateNodeDictionary<TValue>()
        {
            IDictionary<Type, TValue> Result = new Dictionary<Type, TValue>();
            Assembly LanguageAssembly = typeof(IRoot).Assembly;
            Type[] LanguageTypes = LanguageAssembly.GetTypes();

            foreach (Type Item in LanguageTypes)
            {
                if (!Item.IsInterface && !Item.IsAbstract && Item.GetInterface(typeof(INode).Name) != null)
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

        public static bool IsCollectionNeverEmpty(INode node, string PropertyName)
        {
            return IsCollectionNeverEmpty(node.GetType(), PropertyName);
        }

        public static bool IsCollectionNeverEmpty(Type nodeType, string PropertyName)
        {
            Debug.Assert(NodeTreeHelperList.IsNodeListProperty(nodeType, PropertyName, out Type ChildNodeType) || NodeTreeHelperBlockList.IsBlockListProperty(nodeType, PropertyName, out Type ChildInterfaceType, out ChildNodeType));

            Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);

            if (NeverEmptyCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in NeverEmptyCollectionTable[InterfaceType])
                    if (Item == PropertyName)
                        return true;
            }

            return false;
        }

        public static readonly IReadOnlyDictionary<Type, string[]> NeverEmptyCollectionTable = new Dictionary<Type, string[]>()
        {
            { typeof(IAttachment), new string[] { nameof(IAttachment.AttachTypeBlocks) } },
            { typeof(IClassReplicate), new string[] { nameof(IClassReplicate.PatternBlocks) } },
            { typeof(IExport), new string[] { nameof(IExport.ClassIdentifierBlocks) } },
            { typeof(IGlobalReplicate), new string[] { nameof(IGlobalReplicate.Patterns) } },
            { typeof(IQualifiedName), new string[] { nameof(IQualifiedName.Path) } },
            { typeof(IQueryOverload), new string[] { nameof(IQueryOverload.ResultBlocks) } },
            { typeof(IQueryOverloadType), new string[] { nameof(IQueryOverloadType.ResultBlocks) } },
            { typeof(IAssignmentArgument), new string[] { nameof(IAssignmentArgument.ParameterBlocks) } },
            { typeof(IWith), new string[] { nameof(IWith.RangeBlocks) } },
            { typeof(IIndexQueryExpression), new string[] { nameof(IIndexQueryExpression.ArgumentBlocks) } },
            { typeof(IPrecursorIndexExpression), new string[] { nameof(IPrecursorIndexExpression.ArgumentBlocks) } },
            { typeof(ICreationFeature), new string[] { nameof(ICreationFeature.OverloadBlocks) } },
            { typeof(IFunctionFeature), new string[] { nameof(IFunctionFeature.OverloadBlocks) } },
            { typeof(IIndexerFeature), new string[] { nameof(IIndexerFeature.IndexParameterBlocks) } },
            { typeof(IProcedureFeature), new string[] { nameof(IProcedureFeature.OverloadBlocks) } },
            { typeof(IAsLongAsInstruction), new string[] { nameof(IAsLongAsInstruction.ContinuationBlocks) } },
            { typeof(IAssignmentInstruction), new string[] { nameof(IAssignmentInstruction.DestinationBlocks) } },
            { typeof(IAttachmentInstruction), new string[] { nameof(IAttachmentInstruction.EntityNameBlocks), nameof(IAttachmentInstruction.AttachmentBlocks) } },
            { typeof(IIfThenElseInstruction), new string[] { nameof(IIfThenElseInstruction.ConditionalBlocks) } },
            { typeof(IInspectInstruction), new string[] { nameof(IInspectInstruction.WithBlocks) } },
            { typeof(IOverLoopInstruction), new string[] { nameof(IOverLoopInstruction.IndexerBlocks) } },
            { typeof(IIndexAssignmentInstruction), new string[] { nameof(IIndexAssignmentInstruction.ArgumentBlocks) } },
            { typeof(IPrecursorIndexAssignmentInstruction), new string[] { nameof(IPrecursorIndexAssignmentInstruction.ArgumentBlocks) } },
            { typeof(IFunctionType), new string[] { nameof(IFunctionType.OverloadBlocks) } },
            { typeof(IGenericType), new string[] { nameof(IGenericType.TypeArgumentBlocks) } },
            { typeof(IIndexerType), new string[] { nameof(IIndexerType.IndexParameterBlocks) } },
            { typeof(IProcedureType), new string[] { nameof(IProcedureType.OverloadBlocks) } },
            { typeof(ITupleType), new string[] { nameof(ITupleType.EntityDeclarationBlocks) } },
        };

        public static bool IsCollectionWithExpand(INode node, string PropertyName)
        {
            return IsCollectionWithExpand(node.GetType(), PropertyName);
        }

        public static bool IsCollectionWithExpand(Type nodeType, string PropertyName)
        {
            Debug.Assert(NodeTreeHelperList.IsNodeListProperty(nodeType, PropertyName, out Type ChildNodeType) || NodeTreeHelperBlockList.IsBlockListProperty(nodeType, PropertyName, out Type ChildInterfaceType, out ChildNodeType));
            Debug.Assert(!IsCollectionNeverEmpty(nodeType, PropertyName));

            Type InterfaceType = NodeTreeHelper.NodeTypeToInterfaceType(nodeType);

            if (WithExpandCollectionTable.ContainsKey(InterfaceType))
            {
                foreach (string Item in WithExpandCollectionTable[InterfaceType])
                    if (Item == PropertyName)
                        return true;
            }

            return false;
        }

        public static readonly IReadOnlyDictionary<Type, string[]> WithExpandCollectionTable = new Dictionary<Type, string[]>()
        {
            { typeof(IPrecursorExpression), new string[] { nameof(IPrecursorExpression.ArgumentBlocks) } },
            { typeof(IQueryExpression), new string[] { nameof(IQueryExpression.ArgumentBlocks) } },
            { typeof(ICommandInstruction), new string[] { nameof(ICommandInstruction.ArgumentBlocks) } },
            { typeof(ICreateInstruction), new string[] { nameof(ICreateInstruction.ArgumentBlocks) } },
            { typeof(IPrecursorInstruction), new string[] { nameof(IPrecursorInstruction.ArgumentBlocks) } },
            { typeof(IThrowInstruction), new string[] { nameof(IThrowInstruction.ArgumentBlocks) } },
        };

        public static IDocument CreateDocumentationCopy(IDocument documentation)
        {
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
                case IKeywordExpression AsKeywordExpression:
                    return SimplifyKeywordExpression(AsKeywordExpression, out simplifiedNode);
                case IManifestCharacterExpression AsManifestCharacterExpression:
                    return SimplifyManifestCharacterExpression(AsManifestCharacterExpression, out simplifiedNode);
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
                case IQueryExpression AsQueryExpression:
                    return SimplifyQueryExpression(AsQueryExpression, out simplifiedNode);
                case IResultOfExpression AsResultOfExpression:
                    return SimplifyResultOfExpression(AsResultOfExpression, out simplifiedNode);
                case IUnaryNotExpression AsUnaryNotExpression:
                    return SimplifyUnaryNotExpression(AsUnaryNotExpression, out simplifiedNode);
                case IUnaryOperatorExpression AsUnaryOperatorExpression:
                    return SimplifyUnaryOperatorExpression(AsUnaryOperatorExpression, out simplifiedNode);
                case IAssignmentInstruction AsAssignmentInstruction:
                    return SimplifyAssignmentInstruction(AsAssignmentInstruction, out simplifiedNode);
                case IKeywordAssignmentInstruction AsKeywordAssignmentInstruction:
                    return SimplifyKeywordAssignmentInstruction(AsKeywordAssignmentInstruction, out simplifiedNode);
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

        private static bool SimplifyQualifiedName(IQualifiedName Node, out INode SimplifiedNode)
        {
            if (Node.Path.Count > 1)
            {
                string ConcatenatedText = "";

                for (int i = 0; i < Node.Path.Count; i++)
                {
                    if (i > 0)
                        ConcatenatedText += ".";

                    ConcatenatedText += Node.Path[i].Text;
                }

                SimplifiedNode = CreateSimpleQualifiedName(ConcatenatedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return false;
            }
        }

        private static bool SimplifyAssignmentArgument(IAssignmentArgument Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreatePositionalArgument(Node.Source);
            return true;
        }

        private static bool SimplifyPositionalArgument(IPositionalArgument Node, out INode SimplifiedNode)
        {
            SimplifiedNode = null;
            return true;
        }

        private static bool SimplifyAgentExpression(IAgentExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleQueryExpression($"agent {Node.Delegated.Text}");
            return true;
        }

        private static bool SimplifyAssertionTagExpression(IAssertionTagExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleQueryExpression($"tag {Node.TagIdentifier.Text}");
            return true;
        }

        private static bool SimplifyBinaryConditionalExpression(IBinaryConditionalExpression Node, out INode SimplifiedNode)
        {
            string LeftText, RightText;

            if (GetExpressionText(Node.LeftExpression, out LeftText) && GetExpressionText(Node.RightExpression, out RightText))
            {
                string Operator;
                switch (Node.Conditional)
                {
                    case ConditionalTypes.And:
                        Operator = " and ";
                        break;

                    case ConditionalTypes.Or:
                        Operator = " or ";
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("Invalid ConditionalTypes");
                }

                string SimplifiedText = LeftText + Operator + RightText;
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyBinaryOperatorExpression(IBinaryOperatorExpression Node, out INode SimplifiedNode)
        {
            string LeftText, RightText;

            if (GetExpressionText(Node.LeftExpression, out LeftText) && GetExpressionText(Node.RightExpression, out RightText))
            {
                string SimplifiedText = LeftText + " " + Node.Operator.Text + " " + RightText;
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyClassConstantExpression(IClassConstantExpression Node, out INode SimplifiedNode)
        {
            string MergedText = $"{{{Node.ClassIdentifier.Text}}}{Node.ConstantIdentifier.Text}";
            IQualifiedName Query = StringToQualifiedName(MergedText);

            SimplifiedNode = CreateQueryExpression(Query, new List<IArgument>());
            return true;
        }

        private static bool SimplifyCloneOfExpression(ICloneOfExpression Node, out INode SimplifiedNode)
        {
            string SourceText;

            if (GetExpressionText(Node.Source, out SourceText))
            {
                string SimplifiedText = $"clone of {SourceText}";
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyEntityExpression(IEntityExpression Node, out INode SimplifiedNode)
        {
            string SimplifiedText = $"entity {Node.Query.Path[0].Text}";
            SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
            return true;
        }

        private static bool SimplifyEqualityExpression(IEqualityExpression Node, out INode SimplifiedNode)
        {
            string LeftText, RightText;

            if (GetExpressionText(Node.LeftExpression, out LeftText) && GetExpressionText(Node.RightExpression, out RightText))
            {
                string EqualityText = (Node.Comparison == ComparisonType.Equal ? "=" : "/=");
                string SimplifiedText = $"{LeftText} {EqualityText} {RightText}";
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyIndexQueryExpression(IIndexQueryExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = Node.IndexedExpression;
            return true;
        }

        private static bool SimplifyInitializedObjectExpression(IInitializedObjectExpression Node, out INode SimplifiedNode)
        {
            IQualifiedName Query = StringToQualifiedName(Node.ClassIdentifier.Text);

            IBlockList<IAssignmentArgument, AssignmentArgument> ObjectBlockList = Node.AssignmentBlocks;
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

            SimplifiedNode = CreateQueryExpression(Query, ArgumentBlocks);
            return true;
        }

        private static bool SimplifyKeywordExpression(IKeywordExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleQueryExpression(Node.Value.ToString());
            return true;
        }

        private static bool SimplifyManifestCharacterExpression(IManifestCharacterExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleQueryExpression(Node.Text);
            return true;
        }

        private static bool SimplifyManifestStringExpression(IManifestStringExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleQueryExpression(Node.Text);
            return true;
        }

        private static bool SimplifyNewExpression(INewExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateQueryExpression(Node.Object, new List<IArgument>());
            return true;
        }

        private static bool SimplifyOldExpression(IOldExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateQueryExpression(Node.Query, new List<IArgument>());
            return true;
        }

        private static bool SimplifyPrecursorExpression(IPrecursorExpression Node, out INode SimplifiedNode)
        {
            IQualifiedName Query = CreateSimpleQualifiedName("precursor");
            SimplifiedNode = CreateQueryExpression(Query, Node.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPrecursorIndexExpression(IPrecursorIndexExpression Node, out INode SimplifiedNode)
        {
            IQualifiedName Query = CreateSimpleQualifiedName("precursor[]");
            SimplifiedNode = CreateQueryExpression(Query, Node.ArgumentBlocks);
            return true;
        }

        private static bool SimplifyPreprocessorExpression(IPreprocessorExpression Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleQueryExpression(Node.Value.ToString());
            return true;
        }

        private static bool SimplifyQueryExpression(IQueryExpression Node, out INode SimplifiedNode)
        {
            IBlockList<IArgument, Argument> ArgumentBlocks = Node.ArgumentBlocks;
            if (ArgumentBlocks.NodeBlockList.Count > 0)
            {
                SimplifiedNode = CreateQueryExpression(Node.Query, new List<IArgument>());
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return false;
            }
        }

        private static bool SimplifyResultOfExpression(IResultOfExpression Node, out INode SimplifiedNode)
        {
            string SourceText;

            if (GetExpressionText(Node.Source, out SourceText))
            {
                string SimplifiedText = $"result of {SourceText}";
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyUnaryNotExpression(IUnaryNotExpression Node, out INode SimplifiedNode)
        {
            string RightText;

            if (GetExpressionText(Node.RightExpression, out RightText))
            {
                string SimplifiedText = $"not {RightText}";
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyUnaryOperatorExpression(IUnaryOperatorExpression Node, out INode SimplifiedNode)
        {
            string RightText;

            if (GetExpressionText(Node.RightExpression, out RightText))
            {
                string SimplifiedText = $"{Node.Operator.Text} {RightText}";
                SimplifiedNode = CreateSimpleQueryExpression(SimplifiedText);
                return true;
            }
            else
            {
                SimplifiedNode = null;
                return true;
            }
        }

        private static bool SimplifyAssignmentInstruction(IAssignmentInstruction Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateEmptyCommandInstruction();
            return true;
        }

        private static bool SimplifyKeywordAssignmentInstruction(IKeywordAssignmentInstruction Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateEmptyCommandInstruction();
            return true;
        }

        private static bool SimplifyFunctionType(IFunctionType Node, out INode SimplifiedNode)
        {
            SimplifiedNode = Node.BaseType;
            return true;
        }

        private static bool SimplifyGenericType(IGenericType Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateSimpleType(Node.ClassIdentifier);
            return true;
        }

        private static bool SimplifyIndexerType(IIndexerType Node, out INode SimplifiedNode)
        {
            SimplifiedNode = Node.BaseType;
            return true;
        }

        private static bool SimplifyPropertyType(IPropertyType Node, out INode SimplifiedNode)
        {
            SimplifiedNode = Node.BaseType;
            return true;
        }

        private static bool SimplifyProcedureType(IProcedureType Node, out INode SimplifiedNode)
        {
            SimplifiedNode = Node.BaseType;
            return true;
        }

        private static bool SimplifyTupleType(ITupleType Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreateEmptySimpleType();
            return true;
        }

        private static bool SimplifyAssignmentTypeArgument(IAssignmentTypeArgument Node, out INode SimplifiedNode)
        {
            SimplifiedNode = CreatePositionalTypeArgument(Node.Source);
            return true;
        }

        private static bool SimplifyPositionalTypeArgument(IPositionalTypeArgument Node, out INode SimplifiedNode)
        {
            SimplifiedNode = null;
            return true;
        }

        private static IQualifiedName StringToQualifiedName(string Text)
        {
            string[] StringList;
            ParseDotSeparatedIdentifiers(Text, out StringList);

            List<IIdentifier> IdentifierList = new List<IIdentifier>();
            foreach (string Identifier in StringList)
                IdentifierList.Add(CreateSimpleIdentifier(Identifier));

            return CreateQualifiedName(IdentifierList);
        }

        private static void ParseDotSeparatedIdentifiers(string Text, out string[] StringList)
        {
            ParseSymbolSeparatedStrings(Text, '.', out StringList);
        }

        private static void ParseSymbolSeparatedStrings(string Text, char Symbol, out string[] StringList)
        {
            string[] SplittedStrings = Text.Split(Symbol);
            StringList = new string[SplittedStrings.Length];
            for (int i = 0; i < SplittedStrings.Length; i++)
                StringList[i] = SplittedStrings[i].Trim();
        }

        private static bool GetExpressionText(IExpression ExpressionNode, out string Text)
        {
            IManifestNumberExpression AsNumber;
            IQueryExpression AsQuery;

            if ((AsNumber = ExpressionNode as IManifestNumberExpression) != null)
            {
                Text = AsNumber.Text;
                return true;
            }

            else if ((AsQuery = ExpressionNode as IQueryExpression) != null)
            {
                Text = AsQuery.Query.Path[0].Text;
                return true;
            }

            else
            {
                Text = null;
                return false;
            }
        }
        #endregion
    }
}
