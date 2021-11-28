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
        [Category("Complexify")]
        public static void TestComplexifyAssignmentTypeArgument()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            AssignmentTypeArgument Argument1 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Argument1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            AssignmentTypeArgument Argument2 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, AnchorType);

            Result = NodeHelper.GetComplexifiedNode(Argument2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentTypeArgument);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyPositionalTypeArgument()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PositionalTypeArgument Argument1 = NodeHelper.CreatePositionalTypeArgument(DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Argument1, out ComplexifiedNodeList);
            Assert.False(Result);

            SimpleType SplittableSimpleType = NodeHelper.CreateSimpleSimpleType("a:=b");

            PositionalTypeArgument Argument2 = NodeHelper.CreatePositionalTypeArgument(SplittableSimpleType);

            Result = NodeHelper.GetComplexifiedNode(Argument2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentTypeArgument);

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a:=b");
            TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            GenericType SplittableGenericType = NodeHelper.CreateGenericType(SimpleIdentifier, new List<TypeArgument>() { DefaultTypeArgument });

            PositionalTypeArgument Argument3 = NodeHelper.CreatePositionalTypeArgument(SplittableGenericType);

            Result = NodeHelper.GetComplexifiedNode(Argument3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentTypeArgument);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType AnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            PositionalTypeArgument Argument4 = NodeHelper.CreatePositionalTypeArgument(AnchoredType);

            Result = NodeHelper.GetComplexifiedNode(Argument4, out _);
            Assert.False(Result);
        }
    }
}
