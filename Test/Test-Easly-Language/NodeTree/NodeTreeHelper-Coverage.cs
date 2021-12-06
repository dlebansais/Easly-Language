namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeTreeHelperCoverage
    {
        [Test]
        public static void TestEnumChildNodeProperties()
        {
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            IList<string> ChildNodePropertyList = NodeTreeHelper.EnumChildNodeProperties(DefaultExpression);
            Assert.True(ChildNodePropertyList.Contains(nameof(Expression.Documentation)));
        }

        [Test]
        public static void TestGetString()
        {
            string Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            Result = NodeTreeHelper.GetString(SimpleIdentifier, nameof(Identifier.Text));
            Assert.AreEqual(Result, "a");

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetString(DefaultExpression, nameof(Identifier.Text)); });
        }

        [Test]
        public static void TestSetString()
        {
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            NodeTreeHelper.SetString(SimpleIdentifier, nameof(Identifier.Text), "b");

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetString(DefaultExpression, nameof(Identifier.Text), "b"); });
        }

        [Test]
        public static void TestGetEnumValue()
        {
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            NodeTreeHelper.GetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind));
        }

        [Test]
        public static void TestGetEnumValueAndRange()
        {
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            int Min, Max;
            NodeTreeHelper.GetEnumValueAndRange(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), out Min, out Max);
            Assert.AreEqual(Min, (int)AnchorKinds.Declaration);
            Assert.AreEqual(Max, (int)AnchorKinds.Creation);

            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");
            NodeTreeHelper.GetEnumValueAndRange(NewInheritance, nameof(Inheritance.ForgetIndexer), out Min, out Max);
            Assert.AreEqual(Min, 0);
            Assert.AreEqual(Max, 1);

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumValueAndRange(DefaultExpression, nameof(AnchoredType.AnchorKind), out _, out _); });
        }

        [Test]
        public static void TestSetEnumValue()
        {
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            NodeTreeHelper.SetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), AnchorKinds.Creation);
        }
    }
}
