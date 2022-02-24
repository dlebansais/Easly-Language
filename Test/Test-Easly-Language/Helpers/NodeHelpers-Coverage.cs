namespace TestEaslyLanguage;

using System.Collections.Generic;
using ArgumentException = System.ArgumentException;
using Guid = System.Guid;
using BaseNode;
using BaseNodeHelper;
using NUnit.Framework;
using NotNullReflection;

[TestFixture]
public partial class NodeHelperCoverage
{
    [Test]
    public static void TestBuilding()
    {
        Document Document = NodeHelper.CreateEmptyDocument();
        Document = NodeHelper.CreateSimpleDocument(string.Empty, Guid.NewGuid());

        Assert.Throws<ArgumentException>(() => { NodeHelper.CreateSimpleDocument(string.Empty, Guid.Empty); });

        Pattern Pattern = NodeHelper.CreateEmptyPattern();
        Pattern = NodeHelper.CreateSimplePattern(string.Empty);

        Identifier Identifier = NodeHelper.CreateEmptyIdentifier();
        Identifier = NodeHelper.CreateSimpleIdentifier(string.Empty);

        Name Name = NodeHelper.CreateEmptyName();
        Name = NodeHelper.CreateSimpleName(string.Empty);

        QualifiedName QualifiedName = NodeHelper.CreateEmptyQualifiedName();
        QualifiedName = NodeHelper.CreateSimpleQualifiedName(string.Empty);
        QualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier> { Identifier });

        Assert.Throws<ArgumentException>(() => { NodeHelper.CreateQualifiedName(new List<Identifier>()); });

        Expression Expression = NodeHelper.CreateEmptyQueryExpression();
        Expression = NodeHelper.CreateSimpleQueryExpression(string.Empty);

        Instruction Instruction = NodeHelper.CreateEmptyCommandInstruction();
        Instruction = NodeHelper.CreateSimpleCommandInstruction(string.Empty);

        PositionalArgument PositionalArgument = NodeHelper.CreateEmptyPositionalArgument();
        PositionalArgument = NodeHelper.CreateSimplePositionalArgument(string.Empty);

        AssignmentArgument AssignmentArgument = NodeHelper.CreateEmptyAssignmentArgument();
        AssignmentArgument = NodeHelper.CreateSimpleAssignmentArgument(string.Empty, string.Empty);

        PositionalTypeArgument PositionalTypeArgument = NodeHelper.CreateEmptyPositionalTypeArgument();
        PositionalTypeArgument = NodeHelper.CreateSimplePositionalTypeArgument(string.Empty);

        SimpleType SimpleType = NodeHelper.CreateEmptySimpleType();
        SimpleType = NodeHelper.CreateSimpleSimpleType(string.Empty);

        Scope Scope = NodeHelper.CreateEmptyScope();
        Scope = NodeHelper.CreateSimpleScope(Instruction);

        Conditional Conditional = NodeHelper.CreateEmptyConditional();

        QueryOverload QueryOverload = NodeHelper.CreateEmptyQueryOverload();

        CommandOverload CommandOverload = NodeHelper.CreateEmptyCommandOverload();

        QueryOverloadType QueryOverloadType = NodeHelper.CreateEmptyQueryOverloadType(SimpleType);

        CommandOverloadType CommandOverloadType = NodeHelper.CreateEmptyCommandOverloadType();

        EntityDeclaration EntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();
    }

    [Test]
    public static void TestDefault()
    {
        Argument Argument = NodeHelper.CreateDefaultArgument();
        TypeArgument TypeArgument = NodeHelper.CreateDefaultTypeArgument();
        Body Body = NodeHelper.CreateDefaultBody();
        Expression Expression = NodeHelper.CreateDefaultExpression();
        Instruction Instruction = NodeHelper.CreateDefaultInstruction();
        Feature Feature = NodeHelper.CreateDefaultFeature();
        ObjectType ObjectType = NodeHelper.CreateDefaultObjectType();

        Node Default = NodeHelper.CreateDefault(Type.FromTypeof<PositionalArgument>());
        Default = NodeHelper.CreateDefaultFromType(Type.FromTypeof<PositionalArgument>());

        Type DefaultType = NodeHelper.GetDefaultItemType(Type.FromTypeof<Argument>());
    }
}
