namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        [Category("Simplify")]
        public static void TestSimplifyAssignmentTypeArgument()
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
        public static void TestSimplifyPositionalTypeArgument()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PositionalTypeArgument TypeArgument1 = NodeHelper.CreatePositionalTypeArgument(DefaultObjectType);

            Result = NodeHelper.GetSimplifiedTypeArgument(TypeArgument1, out SimplifiedNode);
            Assert.False(Result);
        }
    }
}
