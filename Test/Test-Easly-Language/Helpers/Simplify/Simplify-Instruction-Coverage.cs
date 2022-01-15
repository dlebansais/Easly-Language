namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using Easly;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

[TestFixture]
public partial class SimplifyInstructionCoverage
{
    [Test]
    [Category("Simplify")]
    public static void TestCommandInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        CommandInstruction Instruction1 = NodeHelper.CreateSimpleCommandInstruction(string.Empty);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out _);
        Assert.False(Result);

        QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
        CommandInstruction Instruction2 = NodeHelper.CreateCommandInstruction(SimpleQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("b") });

        Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        CommandInstruction Simplified2 = (CommandInstruction)SimplifiedNode;
        Assert.That(Simplified2.ArgumentBlocks.NodeBlockList.Count == 0);
    }

    [Test]
    [Category("Simplify")]
    public static void TestAsLongAsInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        Continuation EmptyContinuation = NodeHelper.CreateEmptyContinuation();

        AsLongAsInstruction Instruction1 = NodeHelper.CreateAsLongAsInstruction(DefaultExpression, EmptyContinuation);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestAssignmentInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
        List<QualifiedName> SimpleQualifiedNameList = new() { EmptyQualifiedName };
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        AssignmentInstruction Instruction1 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
        QualifiedName FirstQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier });
        IBlock<QualifiedName> FirstBlock = BlockListHelper.CreateBlock(new List<QualifiedName>() { FirstQualifiedName });
        Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");
        QualifiedName SecondQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { SecondIdentifier });
        IBlock<QualifiedName> SecondBlock = BlockListHelper.CreateBlock(new List<QualifiedName>() { SecondQualifiedName });
        IBlockList<QualifiedName> NotSimpleBlockList = BlockListHelper.CreateBlockList(new List<IBlock<QualifiedName>>() { FirstBlock, SecondBlock });

        AssignmentInstruction Instruction2= NodeHelper.CreateAssignmentInstruction(NotSimpleBlockList, DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        ManifestNumberExpression NumberExpression = NodeHelper.CreateDefaultManifestNumberExpression();

        AssignmentInstruction Instruction3 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, NumberExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction3, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        QualifiedName NotSimpleQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, SecondIdentifier });
        AssignmentInstruction Instruction4 = NodeHelper.CreateAssignmentInstruction(new List<QualifiedName>() { NotSimpleQualifiedName }, DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction4, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        QueryExpression NotSimpleQuery = NodeHelper.CreateQueryExpression(NotSimpleQualifiedName, new List<Argument>());
        AssignmentInstruction Instruction5 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, NotSimpleQuery);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction5, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestAttachmentInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Name EmptyName = NodeHelper.CreateEmptyName();
        List<Name> SimpleNameList = new() { EmptyName };
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        AttachmentInstruction Instruction1 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameList);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestCheckInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        CheckInstruction Instruction1 = NodeHelper.CreateCheckInstruction(DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestCreateInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Identifier EmptyClassIdentifier = NodeHelper.CreateEmptyIdentifier();
        Identifier EmptyRoutineIdentifier = NodeHelper.CreateEmptyIdentifier();
        List<Argument> EmptyArgumentList = new();

        CreateInstruction Instruction1 = NodeHelper.CreateCreateInstruction(EmptyClassIdentifier, EmptyRoutineIdentifier, EmptyArgumentList);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestDebugInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();

        DebugInstruction Instruction1 = NodeHelper.CreateSimpleDebugInstruction(DefaultInstruction);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        DebugInstruction Instruction2 = NodeHelper.CreateEmptyDebugInstruction();

        Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestForLoopInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        ForLoopInstruction Instruction1 = NodeHelper.CreateEmptyForLoopInstruction();

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);

        Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();

        ForLoopInstruction Instruction2 = NodeHelper.CreateSimpleForLoopInstruction(DefaultInstruction);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        IBlockList<Instruction> EmptyInstructionBlocks = BlockListHelper.CreateEmptyBlockList<Instruction>();
        IBlockList<EntityDeclaration> EntityDeclarationBlocks = BlockListHelper.CreateEmptyBlockList<EntityDeclaration>();
        Expression DefaultCondition = NodeHelper.CreateDefaultExpression();
        IBlockList<Assertion> InvariantBlocks = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IOptionalReference<Expression> Variant = OptionalReferenceHelper.CreateReference<Expression>(NodeHelper.CreateDefaultExpression());

        IBlockList<Instruction> InitInstructionBlocks = BlockListHelper.CreateSimpleBlockList(DefaultInstruction);

        ForLoopInstruction Instruction3 = NodeHelper.CreateForLoopInstruction(EntityDeclarationBlocks, InitInstructionBlocks, DefaultCondition, EmptyInstructionBlocks, EmptyInstructionBlocks, InvariantBlocks, Variant);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction3, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        IBlockList<Instruction> LoopInstructionBlocks = BlockListHelper.CreateSimpleBlockList(DefaultInstruction);

        ForLoopInstruction Instruction4 = NodeHelper.CreateForLoopInstruction(EntityDeclarationBlocks, EmptyInstructionBlocks, DefaultCondition, LoopInstructionBlocks, EmptyInstructionBlocks, InvariantBlocks, Variant);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction4, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        IBlockList<Instruction> IterationInstructionBlocks = BlockListHelper.CreateSimpleBlockList(DefaultInstruction);

        ForLoopInstruction Instruction5 = NodeHelper.CreateForLoopInstruction(EntityDeclarationBlocks, EmptyInstructionBlocks, DefaultCondition, EmptyInstructionBlocks, IterationInstructionBlocks, InvariantBlocks, Variant);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction5, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestIfThenElseInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Conditional EmptyConditional = NodeHelper.CreateEmptyConditional();

        IfThenElseInstruction Instruction1 = NodeHelper.CreateIfThenElseInstruction(EmptyConditional);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);

        Expression DefaultCondition = NodeHelper.CreateDefaultExpression();
        Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();
        Conditional SimpleConditional = NodeHelper.CreateConditional(DefaultCondition, DefaultInstruction);

        IfThenElseInstruction Instruction2 = NodeHelper.CreateIfThenElseInstruction(SimpleConditional);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestIndexAssignmentInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
        Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
        IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultArgument);
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        IndexAssignmentInstruction Instruction1 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentBlockList, DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestInspectInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        InspectInstruction Instruction1 = NodeHelper.CreateInspectInstruction(DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestKeywordAssignmentInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        KeywordAssignmentInstruction Instruction1 = NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);

        ManifestNumberExpression NumberExpression = NodeHelper.CreateDefaultManifestNumberExpression();

        KeywordAssignmentInstruction Instruction2 = NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, NumberExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestOverLoopInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Name EmptyName = NodeHelper.CreateEmptyName();
        List<Name> SimpleNameList = new() { EmptyName };
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        OverLoopInstruction Instruction1 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameList);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestPrecursorIndexAssignmentInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
        IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultArgument);
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        PrecursorIndexAssignmentInstruction Instruction1 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, SimpleArgumentBlockList, DefaultExpression);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is AssignmentInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestPrecursorInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();

        PrecursorInstruction Instruction1 = NodeHelper.CreatePrecursorInstruction(DefaultObjectType, EmptyArgumentBlockList);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestRaiseEventInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();

        RaiseEventInstruction Instruction1 = NodeHelper.CreateRaiseEventInstruction(EmptyIdentifier);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestReleaseInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();

        ReleaseInstruction Instruction1 = NodeHelper.CreateReleaseInstruction(EmptyQualifiedName);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }

    [Test]
    [Category("Simplify")]
    public static void TestThrowInstruction()
    {
        bool Result;
        Node SimplifiedNode;

        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        List<Argument> EmptyArgumentList = new();

        ThrowInstruction Instruction1 = NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, EmptyArgumentList);

        Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is CommandInstruction);
    }
}
