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
        public static void TestComplexifyAttachment()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            Attachment Attachment1 = NodeHelper.CreateAttachment(DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Attachment1, out ComplexifiedNodeList);
            Assert.False(Result);

            ObjectType SplittableSimpleType = NodeHelper.CreateSimpleSimpleType("a,b");
            IBlockList<ObjectType> SimpleObjectTypeBlockList = BlockListHelper.CreateSimpleBlockList(SplittableSimpleType);
            Scope EmptyScope = NodeHelper.CreateEmptyScope();

            Attachment Attachment2 = NodeHelper.CreateAttachment(SimpleObjectTypeBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Attachment2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is Attachment);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");
            IBlockList<ObjectType> AnchorObjectTypeBlockList = BlockListHelper.CreateSimpleBlockList(AnchorType);

            Attachment Attachment3 = NodeHelper.CreateAttachment(AnchorObjectTypeBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Attachment3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is Attachment);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyConditional()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Conditional Conditional1 = NodeHelper.CreateConditional(DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Conditional1, out ComplexifiedNodeList);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            Conditional Conditional2 = NodeHelper.CreateConditional(NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Conditional2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is Conditional);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyIdentifier()
        {
            bool Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            Result = NodeHelper.GetComplexifiedNode(SimpleIdentifier, out _);
            Assert.False(Result);
        }
    }
}
