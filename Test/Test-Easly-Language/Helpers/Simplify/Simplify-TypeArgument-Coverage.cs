namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using NUnit.Framework;

[TestFixture]
public partial class SimplifyTypeArgumentCoverage
{
    [Test]
    [Category("Simplify")]
    public static void TestAssignmentTypeArgument()
    {
        bool Result;
        Node SimplifiedNode;

        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

        AssignmentTypeArgument TypeArgument1 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);

        Result = NodeHelper.GetSimplifiedTypeArgument(TypeArgument1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is PositionalTypeArgument);
    }

    [Test]
    [Category("Simplify")]
    public static void TestPositionalTypeArgument()
    {
        bool Result;
        Node SimplifiedNode;

        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

        PositionalTypeArgument TypeArgument1 = NodeHelper.CreatePositionalTypeArgument(DefaultObjectType);

        Result = NodeHelper.GetSimplifiedTypeArgument(TypeArgument1, out SimplifiedNode);
        Assert.False(Result);
    }
}
