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

            AssignmentTypeArgument TypeArgument1 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(TypeArgument1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            AssignmentTypeArgument TypeArgument2 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, AnchorType);

            Result = NodeHelper.GetComplexifiedNode(TypeArgument2, out ComplexifiedNodeList);
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

            PositionalTypeArgument TypeArgument1 = NodeHelper.CreatePositionalTypeArgument(DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(TypeArgument1, out ComplexifiedNodeList);
            Assert.False(Result);

            SimpleType SplittableSimpleType = NodeHelper.CreateSimpleSimpleType("a:=b");

            PositionalTypeArgument TypeArgument2 = NodeHelper.CreatePositionalTypeArgument(SplittableSimpleType);

            Result = NodeHelper.GetComplexifiedNode(TypeArgument2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentTypeArgument);

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a:=b");
            TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            GenericType SplittableGenericType = NodeHelper.CreateGenericType(SimpleIdentifier, new List<TypeArgument>() { DefaultTypeArgument });

            PositionalTypeArgument TypeArgument3 = NodeHelper.CreatePositionalTypeArgument(SplittableGenericType);

            Result = NodeHelper.GetComplexifiedNode(TypeArgument3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentTypeArgument);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType AnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            PositionalTypeArgument TypeArgument4 = NodeHelper.CreatePositionalTypeArgument(AnchoredType);

            Result = NodeHelper.GetComplexifiedNode(TypeArgument4, out _);
            Assert.False(Result);
        }
    }
}
