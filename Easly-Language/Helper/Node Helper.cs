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

        public static INode CreateDefault(Type objectType)
        {
            switch (objectType)
            {
                case IArgument AsArgument:
                    return CreateDefaultArgument();
                case IBody AsBody:
                    return CreateDefaultBody();
                case IExpression AsExpression:
                    return CreateDefaultExpression();
                case IInstruction AsInstruction:
                    return CreateDefaultInstruction();
                case IObjectType AsObjectType:
                    return CreateDefaultType();
                case IName AsName:
                    return CreateEmptyName();
                case IIdentifier AsIdentifier:
                    return CreateEmptyIdentifier();
                case IQualifiedName AsQualifiedName:
                    return CreateEmptyQualifiedName();
                case IScope AsScope:
                    return CreateEmptyScope();
                case IImport AsImport:
                    return CreateSimpleImport("", "", ImportType.Latest);
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType));
            }
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
        public static void InitializeDocumentation(INode node)
        {
            IDocument EmptyDocumentation = CreateEmptyDocumentation();
            ((Node)node).Documentation = EmptyDocumentation;
        }

        public static void InitializeChildNode(INode node, string propertyName, INode childNode)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            ItemProperty.SetValue(node, childNode);
        }

        public static void InitializeOptionalChildNode(INode node, string propertyName, INode childNode)
        {
            PropertyInfo ItemProperty = node.GetType().GetProperty(propertyName);
            Type ItemType = ItemProperty.PropertyType;
            Type[] Generics = ItemType.GetGenericArguments();

            Type ReferenceType = typeof(OptionalReference<>).MakeGenericType(Generics);
            IOptionalReference EmptyReference = (IOptionalReference)ReferenceType.Assembly.CreateInstance(ReferenceType.FullName);
            ReferenceType.GetProperty(nameof(IOptionalReference<Node>.Item)).SetValue(EmptyReference, childNode);
            EmptyReference.Unassign();

            ItemProperty.SetValue(node, EmptyReference);
        }

        public static void InitializeEmptyNodeList(INode node, string propertyName, Type childNodeType)
        {
            Type[] Generics = new Type[] { childNodeType };
            Type ListType = typeof(List<>).MakeGenericType(Generics);
            IList EmptyList = (IList)ListType.Assembly.CreateInstance(ListType.FullName);

            node.GetType().GetProperty(propertyName).SetValue(node, EmptyList);
        }

        public static void InitializeSimpleNodeList(INode node, string propertyName, Type childNodeType, INode firstNode)
        {
            InitializeEmptyNodeList(node, propertyName, childNodeType);

            IList NodeList = (IList)node.GetType().GetProperty(propertyName).GetValue(node);
            NodeList.Add(firstNode);
        }

        public static void InitializeEmptyBlockList(INode node, string propertyName, Type childInterfaceType, Type childNodeType)
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

        public static void InitializeSimpleBlockList(INode node, string propertyName, Type childInterfaceType, Type childNodeType, INode firstNode)
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
            if (!optional.IsAssigned || optional.AnyItem == null)
                return false;

            IList<IIdentifier> Path;

            switch (optional.AnyItem.GetType())
            {
                case IName AsName:
                    return AsName.Text.Length == 0;

                case IIdentifier AsIdentifier:
                    return AsIdentifier.Text.Length == 0;

                case IScope AsScope:
                    return AsScope.EntityDeclarationBlocks.NodeBlockList.Count == 0 && AsScope.InstructionBlocks.NodeBlockList.Count == 0;

                case IQualifiedName AsQualifiedName:
                    Path = AsQualifiedName.Path;
                    Debug.Assert(Path.Count > 0);

                    return Path.Count == 1 && Path[0].Text.Length == 0;

                case ISimpleType AsSimpleType:
                    return AsSimpleType.Sharing == SharingType.NotShared && AsSimpleType.ClassIdentifier.Text.Length == 0;

                case IObjectType AsObjectType: // Fallback for other IObjectType.
                    return false;

                case IQueryExpression AsQueryExpression:
                    Path = AsQueryExpression.Query.Path;
                    Debug.Assert(Path.Count > 0);

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

                default:
                    throw new ArgumentOutOfRangeException(nameof(optional));
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
                        IBlockList<IArgument, Argument> ArgumentBlocks = AsQueryExpression.ArgumentBlocks;
                        if (NodeTreeHelper.IsBlockListEmpty(ArgumentBlocks))
                            return true;
                    }
                }

            return false;
        }
        #endregion
    }
}
