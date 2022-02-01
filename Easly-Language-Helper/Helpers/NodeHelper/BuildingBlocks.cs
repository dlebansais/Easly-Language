namespace BaseNodeHelper;

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
    public static Document CreateEmptyDocument()
    {
        Document EmptyDocument = new()
        {
            Comment = string.Empty,
            Uuid = Guid.NewGuid(),
        };

        return EmptyDocument;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Document"/> with the provided parameters.
    /// </summary>
    /// <param name="comment">The text comment.</param>
    /// <param name="uuid">The comment id.</param>
    /// <returns>The created instance.</returns>
    public static Document CreateSimpleDocument(string comment, Guid uuid)
    {
        Contract.RequireNotNull(comment, out string Comment);

        if (uuid == Guid.Empty)
            throw new ArgumentException($"{nameof(uuid)} must not be {Guid.Empty}");

        Document SimpleDocument = new()
        {
            Comment = Comment,
            Uuid = uuid,
        };

        return SimpleDocument;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Pattern"/> with a pattern text matching everything.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Pattern CreateEmptyPattern()
    {
        Document Document = CreateEmptyDocument();
        string Text = "*";
        Pattern EmptyPattern = new(Document, Text);

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

        Document Document = CreateEmptyDocument();
        Pattern SimplePattern = new(Document, Text);

        return SimplePattern;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Identifier"/> with an empty identifier.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Identifier CreateEmptyIdentifier()
    {
        Document Document = CreateEmptyDocument();
        string Text = string.Empty;
        Identifier EmptyIdentifier = new(Document, Text);

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

        Document Document = CreateEmptyDocument();
        Identifier SimpleIdentifier = new(Document, Text);

        return SimpleIdentifier;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Name"/> with an empty identifier.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Name CreateEmptyName()
    {
        Document Document = CreateEmptyDocument();
        string Text = string.Empty;
        Name EmptyName = new(Document, Text);

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

        Document Document = CreateEmptyDocument();
        Name SimpleName = new(Document, Text);

        return SimpleName;
    }

    /// <summary>
    /// Creates a new instance of <see cref="QualifiedName "/> with a path made of a single empty identifier.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static QualifiedName CreateEmptyQualifiedName()
    {
        List<Identifier> Path = new();
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

        List<Identifier> Path = new();
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

        Document Document = CreateEmptyDocument();
        QualifiedName NewQualifiedName = new(Document, Path);

        return NewQualifiedName;
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest empty expression.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static QueryExpression CreateEmptyQueryExpression()
    {
        Document Document = CreateEmptyDocument();
        QualifiedName Query = CreateEmptyQualifiedName();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
        QueryExpression EmptyQueryExpression = new(Document, Query, ArgumentBlocks);

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

        Document Document = CreateEmptyDocument();
        QualifiedName Query = CreateSimpleQualifiedName(Text);
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
        QueryExpression SimpleQueryExpression = new(Document, Query, ArgumentBlocks);

        return SimpleQueryExpression;
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest empty instruction.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static CommandInstruction CreateEmptyCommandInstruction()
    {
        Document Document = CreateEmptyDocument();
        QualifiedName Command = CreateEmptyQualifiedName();
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
        CommandInstruction EmptyCommandInstruction = new(Document, Command, ArgumentBlocks);

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

        Document Document = CreateEmptyDocument();
        QualifiedName Command = CreateSimpleQualifiedName(Text);
        IBlockList<Argument> ArgumentBlocks = BlockListHelper<Argument>.CreateEmptyBlockList();
        CommandInstruction SimpleCommandInstruction = new(Document, Command, ArgumentBlocks);

        return SimpleCommandInstruction;
    }

    /// <summary>
    /// Creates a new instance of <see cref="PositionalArgument"/> with an empty source.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static PositionalArgument CreateEmptyPositionalArgument()
    {
        Document Document = CreateEmptyDocument();
        Expression Source = CreateDefaultExpression();
        PositionalArgument EmptyPositionalArgument = new(Document, Source);

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

        Document Document = CreateEmptyDocument();
        Expression Source = CreateSimpleQueryExpression(Text);
        PositionalArgument SimplePositionalArgument = new(Document, Source);

        return SimplePositionalArgument;
    }

    /// <summary>
    /// Creates a new instance of <see cref="AssignmentArgument"/> with empty source and destination.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static AssignmentArgument CreateEmptyAssignmentArgument()
    {
        Identifier Parameter = CreateEmptyIdentifier();

        Document Document = CreateEmptyDocument();
        IBlockList<Identifier> ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
        Expression Source = CreateDefaultExpression();
        AssignmentArgument EmptyAssignmentArgument = new(Document, ParameterBlocks, Source);

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

        Document Document = CreateEmptyDocument();
        Identifier Parameter = CreateSimpleIdentifier(DestinationText);
        IBlockList<Identifier> ParameterBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(Parameter);
        Expression Source = CreateSimpleQueryExpression(SourceText);
        AssignmentArgument SimpleAssignmentArgument = new(Document, ParameterBlocks, Source);

        return SimpleAssignmentArgument;
    }

    /// <summary>
    /// Creates a new instance of <see cref="PositionalTypeArgument"/> with an empty source.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static PositionalTypeArgument CreateEmptyPositionalTypeArgument()
    {
        Document Document = CreateEmptyDocument();
        ObjectType Source = CreateEmptySimpleType();
        PositionalTypeArgument EmptyPositionalTypeArgument = new(Document, Source);

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

        Document Document = CreateEmptyDocument();
        ObjectType Source = CreateSimpleSimpleType(Text);
        PositionalTypeArgument SimplePositionalTypeArgument = new(Document, Source);

        return SimplePositionalTypeArgument;
    }

    /// <summary>
    /// Creates a new instance of <see cref="SimpleType"/> with an empty class identifier.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static SimpleType CreateEmptySimpleType()
    {
        Document Document = CreateEmptyDocument();
        Identifier ClassIdentifier = CreateEmptyIdentifier();
        SimpleType EmptySimpleType = new(Document, SharingType.NotShared, ClassIdentifier);

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

        Document Document = CreateEmptyDocument();
        Identifier ClassIdentifier = CreateSimpleIdentifier(Text);
        SimpleType SimpleSimpleType = new(Document, SharingType.NotShared, ClassIdentifier);

        return SimpleSimpleType;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Scope"/> with no local variables and no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Scope CreateEmptyScope()
    {
        Document Document = CreateEmptyDocument();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        IBlockList<Instruction> InstructionBlocks = BlockListHelper<Instruction>.CreateEmptyBlockList();
        Scope EmptyScope = new(Document, EntityDeclarationBlocks, InstructionBlocks);

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

        Document Document = CreateEmptyDocument();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        IBlockList<Instruction> InstructionBlocks = BlockListHelper<Instruction>.CreateSimpleBlockList(Instruction);
        Scope SimpleScope = new(Document, EntityDeclarationBlocks, InstructionBlocks);

        return SimpleScope;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Conditional"/> with a default condition and no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Conditional CreateEmptyConditional()
    {
        Document Document = CreateEmptyDocument();
        Expression BooleanExpression = CreateDefaultExpression();
        Scope Instructions = CreateEmptyScope();
        Conditional EmptyConditional = new(Document, BooleanExpression, Instructions);

        return EmptyConditional;
    }

    /// <summary>
    /// Creates a new instance of <see cref="QueryOverload"/> with a default return type and no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static QueryOverload CreateEmptyQueryOverload()
    {
        EntityDeclaration FirstResult = CreateEmptyEntityDeclaration();

        Document Document = CreateEmptyDocument();
        IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
        IBlockList<EntityDeclaration> ResultBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(FirstResult);
        IBlockList<Identifier> ModifiedQueryBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IOptionalReference<Expression> Variant = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        Body QueryBody = CreateDefaultBody();
        QueryOverload EmptyQueryOverload = new(Document, ParameterBlocks, ParameterEnd, ResultBlocks, ModifiedQueryBlocks, Variant, QueryBody);

        return EmptyQueryOverload;
    }

    /// <summary>
    /// Creates a new instance of <see cref="CommandOverload"/> with no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static CommandOverload CreateEmptyCommandOverload()
    {
        Document Document = CreateEmptyDocument();
        IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
        Body CommandBody = CreateDefaultBody();
        CommandOverload EmptyCommandOverload = new(Document, ParameterBlocks, ParameterEnd, CommandBody);

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

        Document Document = CreateEmptyDocument();
        IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
        Name EntityName = CreateSimpleName("Result");
        EntityDeclaration ResultEntity = CreateEntityDeclaration(EntityName, ReturnType);
        IBlockList<EntityDeclaration> ResultBlocks = BlockListHelper<EntityDeclaration>.CreateSimpleBlockList(ResultEntity);
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        QueryOverloadType EmptyQueryOverloadType = new(Document, ParameterBlocks, ParameterEnd, ResultBlocks, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

        return EmptyQueryOverloadType;
    }

    /// <summary>
    /// Creates a new instance of <see cref="CommandOverloadType"/> with no instructions.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static CommandOverloadType CreateEmptyCommandOverloadType()
    {
        Document Document = CreateEmptyDocument();
        IBlockList<EntityDeclaration> ParameterBlocks = BlockListHelper<EntityDeclaration>.CreateEmptyBlockList();
        ParameterEndStatus ParameterEnd = ParameterEndStatus.Closed;
        IBlockList<Assertion> RequireBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Assertion> EnsureBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        IBlockList<Identifier> ExceptionIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        CommandOverloadType EmptyCommandOverloadType = new(Document, ParameterBlocks, ParameterEnd, RequireBlocks, EnsureBlocks, ExceptionIdentifierBlocks);

        return EmptyCommandOverloadType;
    }

    /// <summary>
    /// Creates a new instance of <see cref="EntityDeclaration"/> with an empty name and default type.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static EntityDeclaration CreateEmptyEntityDeclaration()
    {
        Document Document = CreateEmptyDocument();
        Name EntityName = CreateEmptyName();
        ObjectType EntityType = CreateDefaultObjectType();
        IOptionalReference<Expression> DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        EntityDeclaration EmptyEntityDeclaration = new(Document, EntityName, EntityType, DefaultValue);

        return EmptyEntityDeclaration;
    }
}
