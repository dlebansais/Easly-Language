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
        public static Document CreateEmptyDocumentation()
        {
            Document Documentation = new Document();
            Documentation.Comment = string.Empty;
            Documentation.Uuid = Guid.NewGuid();

            return Documentation;
        }

        public static Document CreateSimpleDocumentation(string comment, Guid uuid)
        {
            Document Documentation = new Document();
            Documentation.Comment = comment;
            Documentation.Uuid = uuid;

            return Documentation;
        }

        public static Pattern CreateEmptyPattern()
        {
            Pattern EmptyPattern = new Pattern();
            EmptyPattern.Documentation = CreateEmptyDocumentation();
            EmptyPattern.Text = "*";

            return EmptyPattern;
        }

        public static Pattern CreateSimplePattern(string patternText)
        {
            Pattern SimplePattern = new Pattern();
            SimplePattern.Documentation = CreateEmptyDocumentation();
            SimplePattern.Text = patternText;

            return SimplePattern;
        }

        public static Identifier CreateEmptyIdentifier()
        {
            Identifier EmptyIdentifier = new Identifier();
            EmptyIdentifier.Documentation = CreateEmptyDocumentation();
            EmptyIdentifier.Text = string.Empty;

            return EmptyIdentifier;
        }

        public static Identifier CreateSimpleIdentifier(string identifierText)
        {
            Identifier SimpleIdentifier = new Identifier();
            SimpleIdentifier.Documentation = CreateEmptyDocumentation();
            SimpleIdentifier.Text = identifierText;

            return SimpleIdentifier;
        }

        public static Name CreateEmptyName()
        {
            Name EmptyName = new Name();
            EmptyName.Documentation = CreateEmptyDocumentation();
            EmptyName.Text = string.Empty;

            return EmptyName;
        }

        public static Name CreateSimpleName(string nameText)
        {
            Name SimpleName = new Name();
            SimpleName.Documentation = CreateEmptyDocumentation();
            SimpleName.Text = nameText;

            return SimpleName;
        }

        public static QualifiedName CreateEmptyQualifiedName()
        {
            List<Identifier> Path = new List<Identifier>();
            Path.Add(CreateEmptyIdentifier());
            return CreateQualifiedName(Path);
        }

        public static QualifiedName CreateSimpleQualifiedName(string identifierText)
        {
            List<Identifier> Path = new List<Identifier>();
            Path.Add(CreateSimpleIdentifier(identifierText));
            return CreateQualifiedName(Path);
        }

        public static QualifiedName CreateQualifiedName(IList<Identifier> path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Count == 0) throw new ArgumentException($"{nameof(path)} must be have at least one element");

            QualifiedName DefaultQualifiedName = new QualifiedName();
            DefaultQualifiedName.Documentation = CreateEmptyDocumentation();
            DefaultQualifiedName.Path = path;

            return DefaultQualifiedName;
        }

        public static Expression CreateEmptyQueryExpression()
        {
            QueryExpression EmptyQueryExpression = new QueryExpression();
            EmptyQueryExpression.Documentation = CreateEmptyDocumentation();
            EmptyQueryExpression.Query = CreateEmptyQualifiedName();
            EmptyQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return EmptyQueryExpression;
        }

        public static Expression CreateSimpleQueryExpression(string queryText)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = CreateSimpleQualifiedName(queryText);
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return SimpleQueryExpression;
        }

        public static Instruction CreateEmptyCommandInstruction()
        {
            CommandInstruction EmptyCommandInstruction = new CommandInstruction();
            EmptyCommandInstruction.Documentation = CreateEmptyDocumentation();
            EmptyCommandInstruction.Command = CreateEmptyQualifiedName();
            EmptyCommandInstruction.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return EmptyCommandInstruction;
        }

        public static Instruction CreateSimpleCommandInstruction(string commandText)
        {
            CommandInstruction SimpleCommandInstruction = new CommandInstruction();
            SimpleCommandInstruction.Documentation = CreateEmptyDocumentation();
            SimpleCommandInstruction.Command = CreateSimpleQualifiedName(commandText);
            SimpleCommandInstruction.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return SimpleCommandInstruction;
        }

        public static PositionalArgument CreateEmptyPositionalArgument()
        {
            PositionalArgument EmptyPositionalArgument = new PositionalArgument();
            EmptyPositionalArgument.Documentation = CreateEmptyDocumentation();
            EmptyPositionalArgument.Source = CreateDefaultExpression();

            return EmptyPositionalArgument;
        }

        public static PositionalArgument CreateSimplePositionalArgument(string queryText)
        {
            PositionalArgument SimplePositionalArgument = new PositionalArgument();
            SimplePositionalArgument.Documentation = CreateEmptyDocumentation();
            SimplePositionalArgument.Source = CreateSimpleQueryExpression(queryText);

            return SimplePositionalArgument;
        }

        public static AssignmentArgument CreateEmptyAssignmentArgument()
        {
            Identifier Parameter = CreateEmptyIdentifier();

            AssignmentArgument EmptyAssignmentArgument = new AssignmentArgument();
            EmptyAssignmentArgument.Documentation = CreateEmptyDocumentation();
            EmptyAssignmentArgument.ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
            EmptyAssignmentArgument.Source = CreateDefaultExpression();

            return EmptyAssignmentArgument;
        }

        public static AssignmentArgument CreateSimpleAssignmentArgument(string identifierText, string queryText)
        {
            Identifier Parameter = CreateSimpleIdentifier(identifierText);

            AssignmentArgument SimpleAssignmentArgument = new AssignmentArgument();
            SimpleAssignmentArgument.Documentation = CreateEmptyDocumentation();
            SimpleAssignmentArgument.ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
            SimpleAssignmentArgument.Source = CreateSimpleQueryExpression(queryText);

            return SimpleAssignmentArgument;
        }

        public static PositionalTypeArgument CreateEmptyPositionalTypeArgument()
        {
            PositionalTypeArgument EmptyPositionalTypeArgument = new PositionalTypeArgument();
            EmptyPositionalTypeArgument.Documentation = CreateEmptyDocumentation();
            EmptyPositionalTypeArgument.Source = CreateEmptySimpleType();

            return EmptyPositionalTypeArgument;
        }

        public static PositionalTypeArgument CreateSimplePositionalTypeArgument(string typeText)
        {
            PositionalTypeArgument SimplePositionalTypeArgument = new PositionalTypeArgument();
            SimplePositionalTypeArgument.Documentation = CreateEmptyDocumentation();
            SimplePositionalTypeArgument.Source = CreateSimpleSimpleType(typeText);

            return SimplePositionalTypeArgument;
        }

        public static SimpleType CreateEmptySimpleType()
        {
            SimpleType EmptySimpleType = new SimpleType();
            EmptySimpleType.Documentation = CreateEmptyDocumentation();
            EmptySimpleType.ClassIdentifier = CreateEmptyIdentifier();

            return EmptySimpleType;
        }

        public static SimpleType CreateSimpleSimpleType(string identifierText)
        {
            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.ClassIdentifier = CreateSimpleIdentifier(identifierText);

            return SimpleSimpleType;
        }

        public static Scope CreateEmptyScope()
        {
            Scope EmptyScope = new Scope();
            EmptyScope.Documentation = CreateEmptyDocumentation();
            EmptyScope.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyScope.InstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();

            return EmptyScope;
        }

        public static Scope CreateSimpleScope(Instruction instruction)
        {
            Scope EmptyScope = new Scope();
            EmptyScope.Documentation = CreateEmptyDocumentation();
            EmptyScope.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyScope.InstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(instruction);

            return EmptyScope;
        }

        public static Conditional CreateEmptyConditional()
        {
            Conditional EmptyConditional = new Conditional();
            EmptyConditional.Documentation = CreateEmptyDocumentation();
            EmptyConditional.BooleanExpression = CreateDefaultExpression();
            EmptyConditional.Instructions = CreateEmptyScope();

            return EmptyConditional;
        }

        public static QueryOverload CreateEmptyQueryOverload()
        {
            EntityDeclaration FirstResult = CreateEmptyEntityDeclaration();

            QueryOverload EmptyQueryOverload = new QueryOverload();
            EmptyQueryOverload.Documentation = CreateEmptyDocumentation();
            EmptyQueryOverload.ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyQueryOverload.ParameterEnd = ParameterEndStatus.Closed;
            EmptyQueryOverload.ResultBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(FirstResult);
            EmptyQueryOverload.ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            EmptyQueryOverload.Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
            EmptyQueryOverload.QueryBody = CreateDefaultBody();

            return EmptyQueryOverload;
        }

        public static QueryOverloadType CreateEmptyQueryOverloadType(ObjectType returnType)
        {
            Name EntityName = CreateSimpleName("Result");
            EntityDeclaration ResultEntity = CreateEntityDeclaration(EntityName, returnType);
            QueryOverloadType EmptyQueryOverloadType = new QueryOverloadType();
            EmptyQueryOverloadType.Documentation = CreateEmptyDocumentation();
            EmptyQueryOverloadType.ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyQueryOverloadType.ParameterEnd = ParameterEndStatus.Closed;
            EmptyQueryOverloadType.ResultBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(ResultEntity);
            EmptyQueryOverloadType.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            EmptyQueryOverloadType.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            EmptyQueryOverloadType.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return EmptyQueryOverloadType;
        }

        public static CommandOverloadType CreateEmptyCommandOverloadType()
        {
            CommandOverloadType EmptyCommandOverloadType = new CommandOverloadType();
            EmptyCommandOverloadType.Documentation = CreateEmptyDocumentation();
            EmptyCommandOverloadType.ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyCommandOverloadType.ParameterEnd = ParameterEndStatus.Closed;
            EmptyCommandOverloadType.RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            EmptyCommandOverloadType.EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            EmptyCommandOverloadType.ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return EmptyCommandOverloadType;
        }

        public static EntityDeclaration CreateEmptyEntityDeclaration()
        {
            EntityDeclaration EmptyEntityDeclaration = new EntityDeclaration();
            EmptyEntityDeclaration.Documentation = CreateEmptyDocumentation();
            EmptyEntityDeclaration.EntityName = CreateEmptyName();
            EmptyEntityDeclaration.EntityType = CreateDefaultType();
            EmptyEntityDeclaration.DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

            return EmptyEntityDeclaration;
        }

        public static CommandOverload CreateEmptyCommandOverload()
        {
            CommandOverload EmptyCommandOverload = new CommandOverload();
            EmptyCommandOverload.Documentation = CreateEmptyDocumentation();
            EmptyCommandOverload.ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyCommandOverload.ParameterEnd = ParameterEndStatus.Closed;
            EmptyCommandOverload.CommandBody = CreateDefaultBody();

            return EmptyCommandOverload;
        }
    }
}
