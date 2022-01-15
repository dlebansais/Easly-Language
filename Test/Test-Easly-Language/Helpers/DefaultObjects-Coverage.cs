namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using Easly;
using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public partial class DefaultObjectsCoverage
{
    [Test]
    public static void Test()
    {
        Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
        Assert.That(NodeHelper.IsDefaultNode(DefaultArgument));

        TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
        Assert.That(NodeHelper.IsDefaultNode(DefaultTypeArgument));

        Body DefaultBody = NodeHelper.CreateDefaultBody();
        Assert.That(NodeHelper.IsDefaultNode(DefaultBody));

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        Assert.That(NodeHelper.IsDefaultNode(DefaultExpression));

        Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();
        Assert.That(NodeHelper.IsDefaultNode(DefaultInstruction));

        Feature DefaultFeature = NodeHelper.CreateDefaultFeature();
        Assert.That(NodeHelper.IsDefaultNode(DefaultFeature));

        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        Assert.That(NodeHelper.IsDefaultNode(DefaultObjectType));
    }

    [Test]
    public static void TestDefaultNode()
    {
        Assert.Throws<ArgumentException>(() => { NodeHelper.CreateDefault(typeof(CoverageSet)); });

        Node DefaultBody = NodeHelper.CreateDefault(typeof(Body));
        Assert.That(NodeHelper.IsDefaultNode(DefaultBody));

        Node DefaultExpression = NodeHelper.CreateDefault(typeof(Expression));
        Assert.That(NodeHelper.IsDefaultNode(DefaultExpression));

        Node DefaultInstruction = NodeHelper.CreateDefault(typeof(Instruction));
        Assert.That(NodeHelper.IsDefaultNode(DefaultInstruction));

        Node DefaultFeature = NodeHelper.CreateDefault(typeof(Feature));
        Assert.That(NodeHelper.IsDefaultNode(DefaultFeature));

        Node DefaultObjectType = NodeHelper.CreateDefault(typeof(ObjectType));
        Assert.That(NodeHelper.IsDefaultNode(DefaultObjectType));

        Node DefaultArgument = NodeHelper.CreateDefault(typeof(Argument));
        Assert.That(NodeHelper.IsDefaultNode(DefaultArgument));

        Node DefaultTypeArgument = NodeHelper.CreateDefault(typeof(TypeArgument));
        Assert.That(NodeHelper.IsDefaultNode(DefaultTypeArgument));

        Node DefaultName = NodeHelper.CreateDefault(typeof(Name));
        Assert.That(NodeHelper.IsDefaultNode(DefaultName));

        Node DefaultIdentifier = NodeHelper.CreateDefault(typeof(Identifier));
        Assert.That(NodeHelper.IsDefaultNode(DefaultIdentifier));

        Node DefaultQualifiedName = NodeHelper.CreateDefault(typeof(QualifiedName));
        Assert.That(NodeHelper.IsDefaultNode(DefaultQualifiedName));

        Node DefaultScope = NodeHelper.CreateDefault(typeof(Scope));
        Assert.That(NodeHelper.IsDefaultNode(DefaultScope));

        Node DefaultImport = NodeHelper.CreateDefault(typeof(Import));
        Assert.That(NodeHelper.IsDefaultNode(DefaultImport));
    }

    [Test]
    public static void TestDefaultNodeType()
    {
        Type DefaultArgumentType = NodeHelper.GetDefaultItemType(typeof(Argument));
        Assert.That(DefaultArgumentType.IsSubclassOf(typeof(Argument)));

        Type DefaultTypeArgumentType = NodeHelper.GetDefaultItemType(typeof(TypeArgument));
        Assert.That(DefaultTypeArgumentType.IsSubclassOf(typeof(TypeArgument)));

        Type DefaultBodyType = NodeHelper.GetDefaultItemType(typeof(Body));
        Assert.That(DefaultBodyType.IsSubclassOf(typeof(Body)));

        Type DefaultExpressionType = NodeHelper.GetDefaultItemType(typeof(Expression));
        Assert.That(DefaultExpressionType.IsSubclassOf(typeof(Expression)));

        Type DefaultInstructionType = NodeHelper.GetDefaultItemType(typeof(Instruction));
        Assert.That(DefaultInstructionType.IsSubclassOf(typeof(Instruction)));

        Type DefaultFeatureType = NodeHelper.GetDefaultItemType(typeof(Feature));
        Assert.That(DefaultFeatureType.IsSubclassOf(typeof(Feature)));

        Type DefaultObjectTypeType = NodeHelper.GetDefaultItemType(typeof(ObjectType));
        Assert.That(DefaultObjectTypeType.IsSubclassOf(typeof(ObjectType)));

        Type DefaultOtherType = NodeHelper.GetDefaultItemType(typeof(CoverageSet));
        Assert.AreEqual(DefaultOtherType, typeof(CoverageSet));
    }

    [Test]
    public static void TestDefaultNodeFromType()
    {
        Node DefaultBody = NodeHelper.CreateDefaultFromType(typeof(Body));
        Node DefaultExpression = NodeHelper.CreateDefaultFromType(typeof(Expression));
        Node DefaultInstruction = NodeHelper.CreateDefaultFromType(typeof(Instruction));
        Node DefaultFeature = NodeHelper.CreateDefaultFromType(typeof(Feature));
        Node DefaultObjectType = NodeHelper.CreateDefaultFromType(typeof(ObjectType));

        Node DefaultArgument = NodeHelper.CreateDefaultFromType(typeof(Argument));
        Node DefaultTypeArgument = NodeHelper.CreateDefaultFromType(typeof(TypeArgument));

        Node DefaultName = NodeHelper.CreateDefaultFromType(typeof(Name));
        Node DefaultIdentifier = NodeHelper.CreateDefaultFromType(typeof(Identifier));
        Node DefaultQualifiedName = NodeHelper.CreateDefaultFromType(typeof(QualifiedName));
        Node DefaultScope = NodeHelper.CreateDefaultFromType(typeof(Scope));
        Node DefaultImport = NodeHelper.CreateDefaultFromType(typeof(Import));

        Node DefaultConstraint = NodeHelper.CreateDefaultFromType(typeof(Constraint));

        Assert.Throws<ArgumentException>(() => { NodeHelper.CreateEmptyNode(typeof(CoverageSet)); });
        Assert.Throws<ArgumentException>(() => { NodeHelper.CreateEmptyNode(typeof(Argument)); });

        Class DefaultClass = (Class)NodeHelper.CreateEmptyNode(typeof(Class));
        Assert.That(NodeHelper.IsEmptyNode(DefaultClass));

        Library DefaultLibrary = (Library)NodeHelper.CreateEmptyNode(typeof(Library));
        Assert.That(NodeHelper.IsEmptyNode(DefaultLibrary));

        GlobalReplicate DefaultGlobalReplicate = (GlobalReplicate)NodeHelper.CreateEmptyNode(typeof(GlobalReplicate));
        Assert.That(NodeHelper.IsEmptyNode(DefaultGlobalReplicate));

        InspectInstruction DefaultInspectInstruction = (InspectInstruction)NodeHelper.CreateEmptyNode(typeof(InspectInstruction));
        Assert.That(NodeHelper.IsEmptyNode(DefaultInspectInstruction));

        AttachmentInstruction DefaultAttachmentInstruction = (AttachmentInstruction)NodeHelper.CreateEmptyNode(typeof(AttachmentInstruction));
        Assert.That(NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

        Attachment Attachment0 = DefaultAttachmentInstruction.AttachmentBlocks.NodeBlockList[0].NodeList[0];
        Attachment0.Instructions = NodeHelper.CreateSimpleScope(DefaultInspectInstruction);
        Assert.That(!NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

        Attachment DefaultAttachment1 = (Attachment)NodeHelper.CreateEmptyNode(typeof(Attachment));

        DefaultAttachmentInstruction.AttachmentBlocks.NodeBlockList[0].NodeList.Add(DefaultAttachment1);
        Assert.That(!NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

        Attachment DefaultAttachment2 = (Attachment)NodeHelper.CreateEmptyNode(typeof(Attachment));
        List<Attachment> AttachmentList = new() { DefaultAttachment2 };
        IBlock<Attachment> SimpleBlock = BlockListHelper.CreateBlock<Attachment>(AttachmentList);
        DefaultAttachmentInstruction.AttachmentBlocks.NodeBlockList.Add(SimpleBlock);
        Assert.That(!NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

        Root DefaultRoot = (Root)NodeHelper.CreateEmptyNode(typeof(Root));
        Assert.That(NodeHelper.IsEmptyNode(DefaultRoot));

        DefaultRoot.Replicates.Add(DefaultGlobalReplicate);
        Assert.That(!NodeHelper.IsEmptyNode(DefaultRoot));

        //System.Diagnostics.Debug.Assert(false);
        Pattern Pattern0 = DefaultGlobalReplicate.Patterns[0];
        Pattern0.Text = "Foo0";
        Assert.That(!NodeHelper.IsEmptyNode(DefaultGlobalReplicate));

        Pattern SimplePattern1 = NodeHelper.CreateSimplePattern("Foo1");
        DefaultGlobalReplicate.Patterns.Add(SimplePattern1);
        Assert.That(!NodeHelper.IsEmptyNode(DefaultGlobalReplicate));
    }

    [Test]
    public static void TestIsNodeType()
    {
        Assert.That(NodeHelper.IsNodeType(typeof(Identifier)));
        Assert.That(!NodeHelper.IsNodeType(typeof(CoverageSet)));
    }

    [Test]
    public static void TestNonDefault()
    {
        Name NameNode = (Name)NodeHelper.CreateDefault(typeof(Name));
        Assert.That(NodeHelper.IsDefaultNode(NameNode));

        NameNode.Text = "Foo";
        Assert.That(!NodeHelper.IsDefaultNode(NameNode));

        Identifier IdentifierNode = (Identifier)NodeHelper.CreateDefault(typeof(Identifier));
        Assert.That(NodeHelper.IsDefaultNode(IdentifierNode));

        IdentifierNode.Text = "Foo";
        Assert.That(!NodeHelper.IsDefaultNode(IdentifierNode));

        Scope ScopeNode = (Scope)NodeHelper.CreateDefault(typeof(Scope));
        Assert.That(NodeHelper.IsDefaultNode(ScopeNode));

        Instruction DefaultInstruction = (Instruction)NodeHelper.CreateDefaultFromType(typeof(Instruction));
        List<Instruction> InstructionList = new() { DefaultInstruction };
        IBlock<Instruction> InstructionBlock = BlockListHelper.CreateBlock(InstructionList);

        EntityDeclaration DefaultEntityDeclaration = (EntityDeclaration)NodeHelper.CreateDefaultFromType(typeof(EntityDeclaration));
        List<EntityDeclaration> EntityDeclarationList = new() { DefaultEntityDeclaration };
        IBlock<EntityDeclaration> EntityDeclarationBlock = BlockListHelper.CreateBlock(EntityDeclarationList);

        ScopeNode.InstructionBlocks.NodeBlockList.Add(InstructionBlock);
        Assert.That(!NodeHelper.IsDefaultNode(ScopeNode));
        ScopeNode.InstructionBlocks.NodeBlockList.Remove(InstructionBlock);

        ScopeNode.EntityDeclarationBlocks.NodeBlockList.Add(EntityDeclarationBlock);
        Assert.That(!NodeHelper.IsDefaultNode(ScopeNode));
        ScopeNode.EntityDeclarationBlocks.NodeBlockList.Remove(EntityDeclarationBlock);

        QualifiedName QualifiedNameNode = (QualifiedName)NodeHelper.CreateDefault(typeof(QualifiedName));
        Assert.That(NodeHelper.IsDefaultNode(QualifiedNameNode));

        QualifiedNameNode.Path[0].Text = "Foo";
        Assert.That(!NodeHelper.IsDefaultNode(QualifiedNameNode));

        QualifiedNameNode.Path.Add(IdentifierNode);
        Assert.That(!NodeHelper.IsDefaultNode(QualifiedNameNode));

        AnchoredType AnchoredTypeNode = NodeHelper.CreateAnchoredType(QualifiedNameNode, AnchorKinds.Declaration);
        Assert.That(!NodeHelper.IsDefaultNode(AnchoredTypeNode));

        SimpleType SimpleTypeNode = (SimpleType)NodeHelper.CreateDefault(typeof(SimpleType));
        Assert.That(NodeHelper.IsDefaultNode(SimpleTypeNode));

        SimpleTypeNode.ClassIdentifier.Text = "Foo";
        Assert.That(!NodeHelper.IsDefaultNode(SimpleTypeNode));

        SimpleTypeNode.Sharing = SharingType.WriteOnly;
        Assert.That(!NodeHelper.IsDefaultNode(SimpleTypeNode));

        PrecursorBody PrecursorBodyNode = NodeHelper.CreateEmptyPrecursorBody();
        Assert.That(!NodeHelper.IsDefaultNode(PrecursorBodyNode));

        PropertyFeature PropertyFeatureNode = NodeHelper.CreateEmptyPropertyFeature();
        Assert.That(!NodeHelper.IsDefaultNode(PropertyFeatureNode));

        Feature FeatureNode = NodeHelper.CreateDefaultFeature();
        Assert.That(FeatureNode is AttributeFeature);
        AttributeFeature AttributeFeatureNode = (AttributeFeature)FeatureNode;

        Assert.That(NodeHelper.IsDefaultNode(AttributeFeatureNode));

        Assertion AssertionNode = (Assertion)NodeHelper.CreateDefaultFromType(typeof(Assertion));
        Assert.That(NodeHelper.IsDefaultNode(AssertionNode));
        List<Assertion> AssertionList = new() { AssertionNode };
        IBlock<Assertion> AssertionBlock = BlockListHelper.CreateBlock(AssertionList);

        AttributeFeatureNode.EnsureBlocks.NodeBlockList.Add(AssertionBlock);
        Assert.That(!NodeHelper.IsDefaultNode(AttributeFeatureNode));

        AttributeFeatureNode.ExportIdentifier.Text = "Foo";
        Assert.That(!NodeHelper.IsDefaultNode(AttributeFeatureNode));

        Expression ExpressionNode = (Expression)NodeHelper.CreateDefaultFromType(typeof(Expression));
        Assert.That(ExpressionNode is QueryExpression);

        QueryExpression QueryExpressionNode = (QueryExpression)ExpressionNode;
        Assert.That(NodeHelper.IsDefaultNode(QueryExpressionNode));

        Argument ArgumentNode = (Argument)NodeHelper.CreateDefaultFromType(typeof(Argument));
        Assert.That(ArgumentNode is PositionalArgument);

        PositionalArgument PositionalArgumentNode = (PositionalArgument)ArgumentNode;
        Assert.That(NodeHelper.IsDefaultNode(PositionalArgumentNode));

        List<Argument> ArgumentList = new() { ArgumentNode };
        IBlock<Argument> ArgumentBlock = BlockListHelper.CreateBlock(ArgumentList);

        QueryExpressionNode.ArgumentBlocks.NodeBlockList.Add(ArgumentBlock);
        Assert.That(!NodeHelper.IsDefaultNode(QueryExpressionNode));

        ManifestCharacterExpression ManifestCharacterExpressionNode = (ManifestCharacterExpression)NodeHelper.CreateDefaultFromType(typeof(ManifestCharacterExpression));
        Assert.That(NodeHelper.IsDefaultNode(ManifestCharacterExpressionNode));

        ManifestNumberExpression ManifestNumberExpressionNode = (ManifestNumberExpression)NodeHelper.CreateDefaultFromType(typeof(ManifestNumberExpression));
        Assert.That(NodeHelper.IsDefaultNode(ManifestNumberExpressionNode));

        ManifestStringExpression ManifestStringExpressionNode = (ManifestStringExpression)NodeHelper.CreateDefaultFromType(typeof(ManifestStringExpression));
        Assert.That(NodeHelper.IsDefaultNode(ManifestStringExpressionNode));

        PreprocessorExpression PreprocessorExpressionNode = (PreprocessorExpression)NodeHelper.CreateDefaultFromType(typeof(PreprocessorExpression));
        Assert.That(!NodeHelper.IsDefaultNode(PreprocessorExpressionNode));

        AssignmentArgument AssignmentArgumentNode = (AssignmentArgument)NodeHelper.CreateDefaultFromType(typeof(AssignmentArgument));
        Assert.That(!NodeHelper.IsDefaultNode(AssignmentArgumentNode));

        Assert.That(PositionalArgumentNode.Source is QueryExpression);
        QueryExpression Source = (QueryExpression)PositionalArgumentNode.Source;
        Assert.That(Source.Query.Path.Count == 1);

        Source.Query.Path[0].Text = "Foo";
        Assert.That(!NodeHelper.IsDefaultNode(PositionalArgumentNode));

        Source.Query.Path.Add(IdentifierNode);
        Assert.That(!NodeHelper.IsDefaultNode(PositionalArgumentNode));

        Source.ArgumentBlocks.NodeBlockList.Add(ArgumentBlock);
        Assert.That(!NodeHelper.IsDefaultNode(PositionalArgumentNode));

        PositionalArgumentNode.Source = ManifestCharacterExpressionNode;
        Assert.That(!NodeHelper.IsDefaultNode(PositionalArgumentNode));
    }

    [Test]
    public static void TestNonDefaultBody()
    {
        Body BodyNode = NodeHelper.CreateDefaultBody();
        Assert.That(BodyNode is EffectiveBody);
        EffectiveBody EffectiveBodyNode = (EffectiveBody)BodyNode;

        Assert.That(NodeHelper.IsDefaultNode(EffectiveBodyNode));

        ExceptionHandler DefaultExceptionHandler = (ExceptionHandler)NodeHelper.CreateDefaultFromType(typeof(ExceptionHandler));
        List<ExceptionHandler> ExceptionHandlerList = new() { DefaultExceptionHandler };
        IBlock<ExceptionHandler> ExceptionHandlerBlock = BlockListHelper.CreateBlock(ExceptionHandlerList);

        EffectiveBodyNode.ExceptionHandlerBlocks.NodeBlockList.Add(ExceptionHandlerBlock);
        Assert.That(!NodeHelper.IsDefaultNode(EffectiveBodyNode));

        Instruction DefaultInstruction = (Instruction)NodeHelper.CreateDefaultFromType(typeof(Instruction));
        List<Instruction> InstructionList = new() { DefaultInstruction };
        IBlock<Instruction> InstructionBlock = BlockListHelper.CreateBlock(InstructionList);

        EffectiveBodyNode.BodyInstructionBlocks.NodeBlockList.Add(InstructionBlock);
        Assert.That(!NodeHelper.IsDefaultNode(EffectiveBodyNode));

        EntityDeclaration DefaultEntityDeclaration = (EntityDeclaration)NodeHelper.CreateDefaultFromType(typeof(EntityDeclaration));
        List<EntityDeclaration> EntityDeclarationList = new() { DefaultEntityDeclaration };
        IBlock<EntityDeclaration> EntityDeclarationBlock = BlockListHelper.CreateBlock(EntityDeclarationList);

        EffectiveBodyNode.EntityDeclarationBlocks.NodeBlockList.Add(EntityDeclarationBlock);
        Assert.That(!NodeHelper.IsDefaultNode(EffectiveBodyNode));

        Identifier DefaultIdentifier = (Identifier)NodeHelper.CreateDefaultFromType(typeof(Identifier));
        List<Identifier> IdentifierList = new() { DefaultIdentifier };
        IBlock<Identifier> IdentifierBlock = BlockListHelper.CreateBlock(IdentifierList);

        EffectiveBodyNode.ExceptionIdentifierBlocks.NodeBlockList.Add(IdentifierBlock);
        Assert.That(!NodeHelper.IsDefaultNode(EffectiveBodyNode));

        Assertion DefaultAssertion = (Assertion)NodeHelper.CreateDefaultFromType(typeof(Assertion));
        List<Assertion> AssertionList = new() { DefaultAssertion };
        IBlock<Assertion> AssertionBlock = BlockListHelper.CreateBlock(AssertionList);

        EffectiveBodyNode.EnsureBlocks.NodeBlockList.Add(AssertionBlock);
        Assert.That(!NodeHelper.IsDefaultNode(EffectiveBodyNode));

        EffectiveBodyNode.RequireBlocks.NodeBlockList.Add(AssertionBlock);
        Assert.That(!NodeHelper.IsDefaultNode(EffectiveBodyNode));
    }
}
