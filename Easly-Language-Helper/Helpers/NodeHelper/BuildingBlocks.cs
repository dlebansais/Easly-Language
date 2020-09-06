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
        public static IDocument CreateEmptyDocumentation()
        {
            Document Documentation = new Document();
            Documentation.Comment = string.Empty;
            Documentation.Uuid = Guid.NewGuid();

            return Documentation;
        }

        public static IDocument CreateSimpleDocumentation(string comment, Guid uuid)
        {
            Document Documentation = new Document();
            Documentation.Comment = comment;
            Documentation.Uuid = uuid;

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
            EmptyIdentifier.Text = string.Empty;

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
            EmptyName.Text = string.Empty;

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
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Count == 0) throw new ArgumentException($"{nameof(path)} must be have at least one element");

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

        public static IScope CreateSimpleScope(IInstruction instruction)
        {
            Scope EmptyScope = new Scope();
            EmptyScope.Documentation = CreateEmptyDocumentation();
            EmptyScope.EntityDeclarationBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyScope.InstructionBlocks = BlockListHelper<IInstruction, Instruction>.CreateSimpleBlockList(instruction);

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

        public static IQueryOverloadType CreateEmptyQueryOverloadType(IObjectType returnType)
        {
            IName EntityName = CreateSimpleName("Result");
            IEntityDeclaration ResultEntity = CreateEntityDeclaration(EntityName, returnType);
            QueryOverloadType EmptyQueryOverloadType = new QueryOverloadType();
            EmptyQueryOverloadType.Documentation = CreateEmptyDocumentation();
            EmptyQueryOverloadType.ParameterBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateEmptyBlockList();
            EmptyQueryOverloadType.ParameterEnd = ParameterEndStatus.Closed;
            EmptyQueryOverloadType.ResultBlocks = BlockListHelper<IEntityDeclaration, EntityDeclaration>.CreateSimpleBlockList(ResultEntity);
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
    }
}
