namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Creates a new instance of <see cref="Document"/> with empty text.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Document CreateEmptyDocumentation()
        {
            Document Documentation = new Document();
            Documentation.Comment = string.Empty;
            Documentation.Uuid = Guid.NewGuid();

            return Documentation;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Document"/> with the provided parameters.
        /// </summary>
        /// <param name="comment">The text comment.</param>
        /// <param name="uuid">The comment id.</param>
        /// <returns>The created instance.</returns>
        public static Document CreateSimpleDocumentation(string comment, Guid uuid)
        {
            Contract.RequireNotNull(comment, out string Comment);

            Document Documentation = new Document();
            Documentation.Comment = Comment;
            Documentation.Uuid = uuid;

            return Documentation;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pattern"/> with a pattern text matching everything.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Pattern CreateEmptyPattern()
        {
            Pattern EmptyPattern = new Pattern();
            EmptyPattern.Documentation = CreateEmptyDocumentation();
            EmptyPattern.Text = "*";

            return EmptyPattern;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pattern"/> with the provided parameters.
        /// </summary>
        /// <param name="text">The pattern text.</param>
        /// <returns>The created instance.</returns>
        public static Pattern CreateSimplePattern(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            Pattern SimplePattern = new Pattern();
            SimplePattern.Documentation = CreateEmptyDocumentation();
            SimplePattern.Text = Text;

            return SimplePattern;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Identifier"/> with an empty identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Identifier CreateEmptyIdentifier()
        {
            Identifier EmptyIdentifier = new Identifier();
            EmptyIdentifier.Documentation = CreateEmptyDocumentation();
            EmptyIdentifier.Text = string.Empty;

            return EmptyIdentifier;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pattern"/> with the provided parameters.
        /// </summary>
        /// <param name="text">The identifier text.</param>
        /// <returns>The created instance.</returns>
        public static Identifier CreateSimpleIdentifier(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            Identifier SimpleIdentifier = new Identifier();
            SimpleIdentifier.Documentation = CreateEmptyDocumentation();
            SimpleIdentifier.Text = Text;

            return SimpleIdentifier;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Name"/> with an empty identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Name CreateEmptyName()
        {
            Name EmptyName = new Name();
            EmptyName.Documentation = CreateEmptyDocumentation();
            EmptyName.Text = string.Empty;

            return EmptyName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Name"/> with the provided parameters.
        /// </summary>
        /// <param name="text">The name text.</param>
        /// <returns>The created instance.</returns>
        public static Name CreateSimpleName(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            Name SimpleName = new Name();
            SimpleName.Documentation = CreateEmptyDocumentation();
            SimpleName.Text = Text;

            return SimpleName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="QualifiedName "/> with a path made of a single empty identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static QualifiedName CreateEmptyQualifiedName()
        {
            List<Identifier> Path = new List<Identifier>();
            Path.Add(CreateEmptyIdentifier());
            return CreateQualifiedName(Path);
        }

        /// <summary>
        /// Creates a new instance of <see cref="QualifiedName "/> with a path made of a single identifier.
        /// </summary>
        /// <param name="text">The identifier text.</param>
        /// <returns>The created instance.</returns>
        public static QualifiedName CreateSimpleQualifiedName(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            List<Identifier> Path = new List<Identifier>();
            Path.Add(CreateSimpleIdentifier(Text));

            return CreateQualifiedName(Path);
        }

        /// <summary>
        /// Creates a new instance of <see cref="QualifiedName "/> with a path made from a list of identifier.
        /// The list must contain at least one element.
        /// </summary>
        /// <param name="path">The list of identifiers.</param>
        /// <returns>The created instance.</returns>
        public static QualifiedName CreateQualifiedName(IList<Identifier> path)
        {
            Contract.RequireNotNull(path, out IList<Identifier> Path);

            if (Path.Count == 0)
                throw new ArgumentException($"{nameof(path)} must be have at least one element");

            foreach (Identifier? Item in Path)
                if (Item == null)
                    throw new ArgumentException($"{nameof(path)} must not contain null");

            QualifiedName DefaultQualifiedName = new QualifiedName();
            DefaultQualifiedName.Documentation = CreateEmptyDocumentation();
            DefaultQualifiedName.Path = Path;

            return DefaultQualifiedName;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest empty expression.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Expression CreateEmptyQueryExpression()
        {
            QueryExpression EmptyQueryExpression = new QueryExpression();
            EmptyQueryExpression.Documentation = CreateEmptyDocumentation();
            EmptyQueryExpression.Query = CreateEmptyQualifiedName();
            EmptyQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return EmptyQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest expression from a text.
        /// </summary>
        /// <param name="text">The expression text.</param>
        /// <returns>The created instance.</returns>
        public static Expression CreateSimpleQueryExpression(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = CreateSimpleQualifiedName(Text);
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return SimpleQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest empty instruction.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Instruction CreateEmptyCommandInstruction()
        {
            CommandInstruction EmptyCommandInstruction = new CommandInstruction();
            EmptyCommandInstruction.Documentation = CreateEmptyDocumentation();
            EmptyCommandInstruction.Command = CreateEmptyQualifiedName();
            EmptyCommandInstruction.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return EmptyCommandInstruction;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest instruction from a text.
        /// </summary>
        /// <param name="text">The instruction text.</param>
        /// <returns>The created instance.</returns>
        public static Instruction CreateSimpleCommandInstruction(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            CommandInstruction SimpleCommandInstruction = new CommandInstruction();
            SimpleCommandInstruction.Documentation = CreateEmptyDocumentation();
            SimpleCommandInstruction.Command = CreateSimpleQualifiedName(Text);
            SimpleCommandInstruction.ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();

            return SimpleCommandInstruction;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PositionalArgument"/> with an empty source.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static PositionalArgument CreateEmptyPositionalArgument()
        {
            PositionalArgument EmptyPositionalArgument = new PositionalArgument();
            EmptyPositionalArgument.Documentation = CreateEmptyDocumentation();
            EmptyPositionalArgument.Source = CreateDefaultExpression();

            return EmptyPositionalArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PositionalArgument"/> with the source expression from a text.
        /// </summary>
        /// <param name="text">The source expression text.</param>
        /// <returns>The created instance.</returns>
        public static PositionalArgument CreateSimplePositionalArgument(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            PositionalArgument SimplePositionalArgument = new PositionalArgument();
            SimplePositionalArgument.Documentation = CreateEmptyDocumentation();
            SimplePositionalArgument.Source = CreateSimpleQueryExpression(Text);

            return SimplePositionalArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AssignmentArgument"/> with empty source and destination.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static AssignmentArgument CreateEmptyAssignmentArgument()
        {
            Identifier Parameter = CreateEmptyIdentifier();

            AssignmentArgument EmptyAssignmentArgument = new AssignmentArgument();
            EmptyAssignmentArgument.Documentation = CreateEmptyDocumentation();
            EmptyAssignmentArgument.ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
            EmptyAssignmentArgument.Source = CreateDefaultExpression();

            return EmptyAssignmentArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AssignmentArgument"/> with provided parameters for source and destination.
        /// </summary>
        /// <param name="destinationText">The destination text.</param>
        /// <param name="sourceText">The source text.</param>
        /// <returns>The created instance.</returns>
        public static AssignmentArgument CreateSimpleAssignmentArgument(string destinationText, string sourceText)
        {
            Contract.RequireNotNull(destinationText, out string DestinationText);
            Contract.RequireNotNull(sourceText, out string SourceText);

            Identifier Parameter = CreateSimpleIdentifier(DestinationText);

            AssignmentArgument SimpleAssignmentArgument = new AssignmentArgument();
            SimpleAssignmentArgument.Documentation = CreateEmptyDocumentation();
            SimpleAssignmentArgument.ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
            SimpleAssignmentArgument.Source = CreateSimpleQueryExpression(SourceText);

            return SimpleAssignmentArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PositionalTypeArgument"/> with an empty source.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static PositionalTypeArgument CreateEmptyPositionalTypeArgument()
        {
            PositionalTypeArgument EmptyPositionalTypeArgument = new PositionalTypeArgument();
            EmptyPositionalTypeArgument.Documentation = CreateEmptyDocumentation();
            EmptyPositionalTypeArgument.Source = CreateEmptySimpleType();

            return EmptyPositionalTypeArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PositionalTypeArgument"/> with the source type from a text.
        /// </summary>
        /// <param name="text">The source type text.</param>
        /// <returns>The created instance.</returns>
        public static PositionalTypeArgument CreateSimplePositionalTypeArgument(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            PositionalTypeArgument SimplePositionalTypeArgument = new PositionalTypeArgument();
            SimplePositionalTypeArgument.Documentation = CreateEmptyDocumentation();
            SimplePositionalTypeArgument.Source = CreateSimpleSimpleType(Text);

            return SimplePositionalTypeArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SimpleType"/> with an empty class identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static SimpleType CreateEmptySimpleType()
        {
            SimpleType EmptySimpleType = new SimpleType();
            EmptySimpleType.Documentation = CreateEmptyDocumentation();
            EmptySimpleType.ClassIdentifier = CreateEmptyIdentifier();

            return EmptySimpleType;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SimpleType"/> with the provided class identifier.
        /// </summary>
        /// <param name="text">The class identifier.</param>
        /// <returns>The created instance.</returns>
        public static SimpleType CreateSimpleSimpleType(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            SimpleType SimpleSimpleType = new SimpleType();
            SimpleSimpleType.Documentation = CreateEmptyDocumentation();
            SimpleSimpleType.ClassIdentifier = CreateSimpleIdentifier(Text);

            return SimpleSimpleType;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Scope"/> with no local variables and no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Scope CreateEmptyScope()
        {
            Scope EmptyScope = new Scope();
            EmptyScope.Documentation = CreateEmptyDocumentation();
            EmptyScope.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyScope.InstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();

            return EmptyScope;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Scope"/> with no local variables and the provided instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        /// <returns>The created instance.</returns>
        public static Scope CreateSimpleScope(Instruction instruction)
        {
            Contract.RequireNotNull(instruction, out Instruction Instruction);

            Scope EmptyScope = new Scope();
            EmptyScope.Documentation = CreateEmptyDocumentation();
            EmptyScope.EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyScope.InstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(Instruction);

            return EmptyScope;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Conditional"/> with a default condition and no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Conditional CreateEmptyConditional()
        {
            Conditional EmptyConditional = new Conditional();
            EmptyConditional.Documentation = CreateEmptyDocumentation();
            EmptyConditional.BooleanExpression = CreateDefaultExpression();
            EmptyConditional.Instructions = CreateEmptyScope();

            return EmptyConditional;
        }

        /// <summary>
        /// Creates a new instance of <see cref="QueryOverload"/> with a default return type and no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
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

        /// <summary>
        /// Creates a new instance of <see cref="CommandOverload"/> with no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static CommandOverload CreateEmptyCommandOverload()
        {
            CommandOverload EmptyCommandOverload = new CommandOverload();
            EmptyCommandOverload.Documentation = CreateEmptyDocumentation();
            EmptyCommandOverload.ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            EmptyCommandOverload.ParameterEnd = ParameterEndStatus.Closed;
            EmptyCommandOverload.CommandBody = CreateDefaultBody();

            return EmptyCommandOverload;
        }

        /// <summary>
        /// Creates a new instance of <see cref="QueryOverloadType"/> with the provided return type and no instructions.
        /// </summary>
        /// <param name="returnType">The type for which to create a new overload.</param>
        /// <returns>The created instance.</returns>
        public static QueryOverloadType CreateEmptyQueryOverloadType(ObjectType returnType)
        {
            Contract.RequireNotNull(returnType, out ObjectType ReturnType);

            Name EntityName = CreateSimpleName("Result");
            EntityDeclaration ResultEntity = CreateEntityDeclaration(EntityName, ReturnType);
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

        /// <summary>
        /// Creates a new instance of <see cref="CommandOverloadType"/> with no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
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

        /// <summary>
        /// Creates a new instance of <see cref="EntityDeclaration"/> with an empty name and default type.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static EntityDeclaration CreateEmptyEntityDeclaration()
        {
            EntityDeclaration EmptyEntityDeclaration = new EntityDeclaration();
            EmptyEntityDeclaration.Documentation = CreateEmptyDocumentation();
            EmptyEntityDeclaration.EntityName = CreateEmptyName();
            EmptyEntityDeclaration.EntityType = CreateDefaultType();
            EmptyEntityDeclaration.DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

            return EmptyEntityDeclaration;
        }
    }
}
