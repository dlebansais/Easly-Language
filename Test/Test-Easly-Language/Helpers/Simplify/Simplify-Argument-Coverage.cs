namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public partial class SimplifyArgumentCoverage
{
    [Test]
    [Category("Simplify")]
    public static void TestAssignmentArgument()
    {
        bool Result;
        Node SimplifiedNode;

        //System.Diagnostics.Debugger.Launch();
        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        List<Identifier> SimpleParameterList = new() { EmptyIdentifier };
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        AssignmentArgument Argument1 = NodeHelper.CreateAssignmentArgument(SimpleParameterList, DefaultExpression);

        Result = NodeHelper.GetSimplifiedArgument(Argument1, out SimplifiedNode);
        Assert.True(Result);
        Assert.That(SimplifiedNode is PositionalArgument);
    }

    [Test]
    [Category("Simplify")]
    public static void TestPositionalArgument()
    {
        bool Result;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

        PositionalArgument Argument1 = NodeHelper.CreatePositionalArgument(DefaultExpression);

        Result = NodeHelper.GetSimplifiedArgument(Argument1, out _);
        Assert.False(Result);
    }
}
