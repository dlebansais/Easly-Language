namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;
    using Contracts;
    using Easly;

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
            Document EmptyDocumentation = new Document();
            EmptyDocumentation.Comment = string.Empty;
            EmptyDocumentation.Uuid = Guid.NewGuid();

            return EmptyDocumentation;
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

            Document SimpleDocumentation = new Document();
            SimpleDocumentation.Comment = Comment;
            SimpleDocumentation.Uuid = uuid;

            return SimpleDocumentation;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pattern"/> with a pattern text matching everything.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Pattern CreateEmptyPattern()
        {
            Document Documentation = CreateEmptyDocumentation();
            string Text = "*";
            Pattern EmptyPattern = new Pattern(Documentation, Text);

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

            Document Documentation = CreateEmptyDocumentation();
            Pattern SimplePattern = new Pattern(Documentation, Text);

            return SimplePattern;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Identifier"/> with an empty identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Identifier CreateEmptyIdentifier()
        {
            Document Documentation = CreateEmptyDocumentation();
            string Text = string.Empty;
            Identifier EmptyIdentifier = new Identifier(Documentation, Text);

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

            Document Documentation = CreateEmptyDocumentation();
            Identifier SimpleIdentifier = new Identifier(Documentation, Text);

            return SimpleIdentifier;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Name"/> with an empty identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Name CreateEmptyName()
        {
            Document Documentation = CreateEmptyDocumentation();
            string Text = string.Empty;
            Name EmptyName = new Name(Documentation, Text);

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

            Document Documentation = CreateEmptyDocumentation();
            Name SimpleName = new Name(Documentation, Text);

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
            QualifiedName EmptyQualifiedName = CreateQualifiedName(Path);

            return EmptyQualifiedName;
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

            QualifiedName SimpleQualifiedName = CreateQualifiedName(Path);

            return SimpleQualifiedName;
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

            Document Documentation = CreateEmptyDocumentation();
            QualifiedName NewQualifiedName = new QualifiedName(Documentation, Path);

            return NewQualifiedName;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest empty expression.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static QueryExpression CreateEmptyQueryExpression()
        {
            Document Documentation = CreateEmptyDocumentation();
            QualifiedName Query = CreateEmptyQualifiedName();
            IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
            QueryExpression EmptyQueryExpression = new QueryExpression(Documentation, Query, ArgumentBlocks);

            return EmptyQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest expression from a text.
        /// </summary>
        /// <param name="text">The expression text.</param>
        /// <returns>The created instance.</returns>
        public static QueryExpression CreateSimpleQueryExpression(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            Document Documentation = CreateEmptyDocumentation();
            QualifiedName Query = CreateSimpleQualifiedName(Text);
            IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
            QueryExpression SimpleQueryExpression = new QueryExpression(Documentation, Query, ArgumentBlocks);

            return SimpleQueryExpression;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest empty instruction.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static CommandInstruction CreateEmptyCommandInstruction()
        {
            Document Documentation = CreateEmptyDocumentation();
            QualifiedName Command = CreateEmptyQualifiedName();
            IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
            CommandInstruction EmptyCommandInstruction = new CommandInstruction(Documentation, Command, ArgumentBlocks);

            return EmptyCommandInstruction;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest instruction from a text.
        /// </summary>
        /// <param name="text">The instruction text.</param>
        /// <returns>The created instance.</returns>
        public static CommandInstruction CreateSimpleCommandInstruction(string text)
        {
            Contract.RequireNotNull(text, out string Text);

            Document Documentation = CreateEmptyDocumentation();
            QualifiedName Command = CreateSimpleQualifiedName(Text);
            IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
            CommandInstruction SimpleCommandInstruction = new CommandInstruction(Documentation, Command, ArgumentBlocks);

            return SimpleCommandInstruction;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PositionalArgument"/> with an empty source.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static PositionalArgument CreateEmptyPositionalArgument()
        {
            Document Documentation = CreateEmptyDocumentation();
            Expression Source = CreateDefaultExpression();
            PositionalArgument EmptyPositionalArgument = new PositionalArgument(Documentation, Source);

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

            Document Documentation = CreateEmptyDocumentation();
            Expression Source = CreateSimpleQueryExpression(Text);
            PositionalArgument SimplePositionalArgument = new PositionalArgument(Documentation, Source);

            return SimplePositionalArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AssignmentArgument"/> with empty source and destination.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static AssignmentArgument CreateEmptyAssignmentArgument()
        {
            Identifier Parameter = CreateEmptyIdentifier();

            Document Documentation = CreateEmptyDocumentation();
            IBlockList<Identifier> ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
            Expression Source = CreateDefaultExpression();
            AssignmentArgument EmptyAssignmentArgument = new AssignmentArgument(Documentation, ParameterBlocks, Source);

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

            Document Documentation = CreateEmptyDocumentation();
            Identifier Parameter = CreateSimpleIdentifier(DestinationText);
            IBlockList<Identifier> ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
            Expression Source = CreateSimpleQueryExpression(SourceText);
            AssignmentArgument SimpleAssignmentArgument = new AssignmentArgument(Documentation, ParameterBlocks, Source);

            return SimpleAssignmentArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PositionalTypeArgument"/> with an empty source.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static PositionalTypeArgument CreateEmptyPositionalTypeArgument()
        {
            Document Documentation = CreateEmptyDocumentation();
            ObjectType Source = CreateEmptySimpleType();
            PositionalTypeArgument EmptyPositionalTypeArgument = new PositionalTypeArgument(Documentation, Source);

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

            Document Documentation = CreateEmptyDocumentation();
            ObjectType Source = CreateSimpleSimpleType(Text);
            PositionalTypeArgument SimplePositionalTypeArgument = new PositionalTypeArgument(Documentation, Source);

            return SimplePositionalTypeArgument;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SimpleType"/> with an empty class identifier.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static SimpleType CreateEmptySimpleType()
        {
            Document Documentation = CreateEmptyDocumentation();
            Identifier ClassIdentifier = CreateEmptyIdentifier();
            SimpleType EmptySimpleType = new SimpleType(Documentation, SharingType.NotShared, ClassIdentifier);

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

            Document Documentation = CreateEmptyDocumentation();
            Identifier ClassIdentifier = CreateSimpleIdentifier(Text);
            SimpleType SimpleSimpleType = new SimpleType(Documentation, SharingType.NotShared, ClassIdentifier);

            return SimpleSimpleType;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Scope"/> with no local variables and no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Scope CreateEmptyScope()
        {
            Document Documentation = CreateEmptyDocumentation();
            IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            IBlockList<Instruction> InstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
            Scope EmptyScope = new Scope(Documentation, EntityDeclarationBlocks, InstructionBlocks);

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

            Document Documentation = CreateEmptyDocumentation();
            IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            IBlockList<Instruction> InstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(Instruction);
            Scope SimpleScope = new Scope(Documentation, EntityDeclarationBlocks, InstructionBlocks);

            return SimpleScope;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Conditional"/> with a default condition and no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Conditional CreateEmptyConditional()
        {
            Document Documentation = CreateEmptyDocumentation();
            Expression BooleanExpression = CreateDefaultExpression();
            Scope Instructions = CreateEmptyScope();
            Conditional EmptyConditional = new Conditional(Documentation, BooleanExpression, Instructions);

            return EmptyConditional;
        }

        /// <summary>
        /// Creates a new instance of <see cref="QueryOverload"/> with a default return type and no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static QueryOverload CreateEmptyQueryOverload()
        {
            EntityDeclaration FirstResult = CreateEmptyEntityDeclaration();

            Document Documentation = CreateEmptyDocumentation();
            IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
            IBlockList<EntityDeclaration> ResultBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(FirstResult);
            IBlockList<Identifier> ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            IOptionalReference<Expression> Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
            Body QueryBody = CreateDefaultBody();
            QueryOverload EmptyQueryOverload = new QueryOverload(Documentation, ParameterBlocks, ParameterEnd, ResultBlocks, ModifiedQueryBlocks, Variant, QueryBody);

            return EmptyQueryOverload;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CommandOverload"/> with no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static CommandOverload CreateEmptyCommandOverload()
        {
            Document Documentation = CreateEmptyDocumentation();
            IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
            Body CommandBody = CreateDefaultBody();
            CommandOverload EmptyCommandOverload = new CommandOverload(Documentation, ParameterBlocks, ParameterEnd, CommandBody);

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

            Document Documentation = CreateEmptyDocumentation();
            IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
            Name EntityName = CreateSimpleName("Result");
            EntityDeclaration ResultEntity = CreateEntityDeclaration(EntityName, ReturnType);
            IBlockList<EntityDeclaration> ResultBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(ResultEntity);
            IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            QueryOverloadType EmptyQueryOverloadType = new QueryOverloadType(Documentation, ParameterBlocks, ParameterEnd, ResultBlocks, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

            return EmptyQueryOverloadType;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CommandOverloadType"/> with no instructions.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static CommandOverloadType CreateEmptyCommandOverloadType()
        {
            Document Documentation = CreateEmptyDocumentation();
            IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
            ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
            IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            CommandOverloadType EmptyCommandOverloadType = new CommandOverloadType(Documentation, ParameterBlocks, ParameterEnd, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

            return EmptyCommandOverloadType;
        }

        /// <summary>
        /// Creates a new instance of <see cref="EntityDeclaration"/> with an empty name and default type.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static EntityDeclaration CreateEmptyEntityDeclaration()
        {
            Document Documentation = CreateEmptyDocumentation();
            Name EntityName = CreateEmptyName();
            ObjectType EntityType = CreateDefaultObjectType();
            IOptionalReference<Expression> DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
            EntityDeclaration EmptyEntityDeclaration = new EntityDeclaration(Documentation, EntityName, EntityType, DefaultValue);

            return EmptyEntityDeclaration;
        }
    }
}
