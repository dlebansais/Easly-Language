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

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetString(DefaultExpression, nameof(Identifier.Text)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetString(DefaultExpression, nameof(QueryExpression.Query)); });
        }

        [Test]
        public static void TestSetString()
        {
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            NodeTreeHelper.SetString(SimpleIdentifier, nameof(Identifier.Text), "b");

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetString(DefaultExpression, nameof(Identifier.Text), "b"); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetString(DefaultExpression, nameof(QueryExpression.Query), "b"); });
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
            int Result;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            int Min, Max;

            Result = NodeTreeHelper.GetEnumValueAndRange(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), out Min, out Max);
            Assert.AreEqual(Result, (int)AnchorKinds.Declaration);
            Assert.AreEqual(Min, (int)AnchorKinds.Declaration);
            Assert.AreEqual(Max, (int)AnchorKinds.Creation);

            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");
            Result = NodeTreeHelper.GetEnumValueAndRange(NewInheritance, nameof(Inheritance.ForgetIndexer), out Min, out Max);
            Assert.AreEqual(Result, 0);
            Assert.AreEqual(Min, 0);
            Assert.AreEqual(Max, 1);

            NewInheritance.ForgetIndexer = true;
            Result = NodeTreeHelper.GetEnumValueAndRange(NewInheritance, nameof(Inheritance.ForgetIndexer), out Min, out Max);
            Assert.AreEqual(Result, 1);
            Assert.AreEqual(Min, 0);
            Assert.AreEqual(Max, 1);

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumValueAndRange(DefaultExpression, nameof(AnchoredType.AnchorKind), out _, out _); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumValueAndRange(DefaultExpression, nameof(QueryExpression.Query), out _, out _); });
        }

        private enum TestSetEnum: byte
        {
            TestValue,
        };

        [Test]
        public static void TestSetEnumValue()
        {
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            NodeTreeHelper.SetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), AnchorKinds.Creation);
            NodeTreeHelper.SetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), (int)AnchorKinds.Creation);

            Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelper.SetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), (int)(AnchorKinds.Creation) + 1); });

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind), TestSetEnum.TestValue); });

            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");
            NodeTreeHelper.SetEnumValue(NewInheritance, nameof(Inheritance.ForgetIndexer), true);
            NodeTreeHelper.SetEnumValue(NewInheritance, nameof(Inheritance.ForgetIndexer), false);
            NodeTreeHelper.SetEnumValue(NewInheritance, nameof(Inheritance.ForgetIndexer), 1);
            NodeTreeHelper.SetEnumValue(NewInheritance, nameof(Inheritance.ForgetIndexer), 0);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetEnumValue(DefaultExpression, nameof(AnchoredType.AnchorKind), AnchorKinds.Creation); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetEnumValue(DefaultExpression, nameof(QueryExpression.Query), AnchorKinds.Creation); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelper.SetEnumValue(NewInheritance, nameof(Inheritance.ForgetIndexer), -1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelper.SetEnumValue(NewInheritance, nameof(Inheritance.ForgetIndexer), 2); });
        }

        [Test]
        public static void TestGetEnumRange()
        {
            NodeTreeHelper.GetEnumRange(typeof(AnchoredType), nameof(AnchoredType.AnchorKind), out int Min, out int Max);
            Assert.AreEqual(Min, (int)AnchorKinds.Declaration);
            Assert.AreEqual(Max, (int)AnchorKinds.Creation);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumRange(typeof(Inheritance), nameof(Inheritance.DiscontinueBlocks), out _, out _); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumRange(typeof(Inheritance), nameof(QueryExpression.Query), out _, out _); });
        }

        [Test]
        public static void TestGetGuid()
        {
            Class NewClass = NodeHelper.CreateSimpleClass("a");

            Guid Guid = NodeTreeHelper.GetGuid(NewClass, nameof(Class.ClassGuid));
            Assert.AreEqual(Guid, NewClass.ClassGuid);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetGuid(NewClass, nameof(Class.ClassPath)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetGuid(NewClass, nameof(QueryExpression.Query)); });
        }

        [Test]
        public static void TestGetComment()
        {
            string Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            Result = NodeTreeHelper.GetCommentText(SimpleIdentifier);
            Assert.AreEqual(Result, string.Empty);
        }

        [Test]
        public static void TestSetComment()
        {
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            NodeTreeHelper.SetCommentText(SimpleIdentifier, "a");
            Assert.AreEqual(SimpleIdentifier.Documentation.Comment, "a");
        }

        [Test]
        public static void TestIsDocumentProperty()
        {
            bool Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            Result = NodeTreeHelper.IsDocumentProperty(SimpleIdentifier, nameof(Identifier.Documentation));
            Assert.True(Result);

            Result = NodeTreeHelper.IsDocumentProperty(SimpleIdentifier, nameof(Identifier.Text));
            Assert.False(Result);

            Result = NodeTreeHelper.IsDocumentProperty(SimpleIdentifier, nameof(QueryExpression.Query));
            Assert.False(Result);
        }

        [Test]
        public static void TestSetDocumentation()
        {
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Guid NewGuid = new Guid();
            Document SimpleDocumentation = NodeHelper.CreateSimpleDocumentation("a", NewGuid);

            NodeTreeHelper.SetDocumentation(SimpleIdentifier, SimpleDocumentation);
            Assert.AreEqual(SimpleIdentifier.Documentation.Comment, "a");
            Assert.AreEqual(SimpleIdentifier.Documentation.Uuid, NewGuid);
        }

        [Test]
        public static void TestCopyDocumentationNode()
        {
            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");

            NodeTreeHelper.CopyDocumentation(FirstIdentifier, SecondIdentifier, cloneCommentGuid: false);
            Assert.AreEqual(FirstIdentifier.Documentation.Comment, SecondIdentifier.Documentation.Comment);
            Assert.AreNotEqual(FirstIdentifier.Documentation.Uuid, SecondIdentifier.Documentation.Uuid);

            NodeTreeHelper.CopyDocumentation(FirstIdentifier, SecondIdentifier, cloneCommentGuid: true);
            Assert.AreEqual(FirstIdentifier.Documentation.Comment, SecondIdentifier.Documentation.Comment);
            Assert.AreEqual(FirstIdentifier.Documentation.Uuid, SecondIdentifier.Documentation.Uuid);

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyDocumentation(FirstIdentifier, DefaultExpression, cloneCommentGuid: false); });
        }

        [Test]
        public static void TestCopyDocumentationBlock()
        {
            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");

            IBlockList<Identifier> FirstIdentifierBlockList = BlockListHelper.CreateSimpleBlockList(FirstIdentifier);
            IBlockList<Identifier> SecondIdentifierBlockList = BlockListHelper.CreateSimpleBlockList(SecondIdentifier);

            IBlock FirstIdentifierBlock = (IBlock)FirstIdentifierBlockList.NodeBlockList[0];
            IBlock SecondIdentifierBlock = (IBlock)SecondIdentifierBlockList.NodeBlockList[0];

            NodeTreeHelper.CopyDocumentation(FirstIdentifierBlock, SecondIdentifierBlock, cloneCommentGuid: false);
            Assert.AreEqual(FirstIdentifierBlock.Documentation.Comment, SecondIdentifierBlock.Documentation.Comment);
            Assert.AreNotEqual(FirstIdentifierBlock.Documentation.Uuid, SecondIdentifierBlock.Documentation.Uuid);

            NodeTreeHelper.CopyDocumentation(FirstIdentifierBlock, SecondIdentifierBlock, cloneCommentGuid: true);
            Assert.AreEqual(FirstIdentifierBlock.Documentation.Comment, SecondIdentifierBlock.Documentation.Comment);
            Assert.AreEqual(FirstIdentifierBlock.Documentation.Uuid, SecondIdentifierBlock.Documentation.Uuid);

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            IBlockList<Expression> SimpleExpressionBlockList = BlockListHelper.CreateSimpleBlockList(DefaultExpression);
            IBlock SimpleExpressionBlock = (IBlock)SimpleExpressionBlockList.NodeBlockList[0];

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyDocumentation(FirstIdentifierBlock, SimpleExpressionBlock, cloneCommentGuid: false); });
        }

        [Test]
        public static void TestCopyDocumentationBlockList()
        {
            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");

            IBlockList FirstIdentifierBlockList = (IBlockList)BlockListHelper.CreateSimpleBlockList(FirstIdentifier);
            IBlockList SecondIdentifierBlockList = (IBlockList)BlockListHelper.CreateSimpleBlockList(SecondIdentifier);

            NodeTreeHelper.CopyDocumentation(FirstIdentifierBlockList, SecondIdentifierBlockList, cloneCommentGuid: false);
            Assert.AreEqual(FirstIdentifierBlockList.Documentation.Comment, SecondIdentifierBlockList.Documentation.Comment);
            Assert.AreNotEqual(FirstIdentifierBlockList.Documentation.Uuid, SecondIdentifierBlockList.Documentation.Uuid);

            NodeTreeHelper.CopyDocumentation(FirstIdentifierBlockList, SecondIdentifierBlockList, cloneCommentGuid: true);
            Assert.AreEqual(FirstIdentifierBlockList.Documentation.Comment, SecondIdentifierBlockList.Documentation.Comment);
            Assert.AreEqual(FirstIdentifierBlockList.Documentation.Uuid, SecondIdentifierBlockList.Documentation.Uuid);

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            IBlockList SimpleExpressionBlockList = (IBlockList)BlockListHelper.CreateSimpleBlockList(DefaultExpression);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyDocumentation(FirstIdentifierBlockList, SimpleExpressionBlockList, cloneCommentGuid: false); });
        }

        [Test]
        public static void TestIsBooleanProperty()
        {
            bool Result;

            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");

            Result = NodeTreeHelper.IsBooleanProperty(NewInheritance, nameof(Inheritance.ForgetIndexer));
            Assert.True(Result);

            Result = NodeTreeHelper.IsBooleanProperty(NewInheritance, nameof(Inheritance.DiscontinueBlocks));
            Assert.False(Result);

            Result = NodeTreeHelper.IsBooleanProperty(NewInheritance, nameof(QueryExpression.Query));
            Assert.False(Result);
        }

        [Test]
        public static void TestSetBooleanProperty()
        {
            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");

            NodeTreeHelper.SetBooleanProperty(NewInheritance, nameof(Inheritance.ForgetIndexer), true);
            Assert.True(NewInheritance.ForgetIndexer);
        }

        [Test]
        public static void TestIsStringProperty()
        {
            bool Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            Result = NodeTreeHelper.IsStringProperty(SimpleIdentifier, nameof(Identifier.Text));
            Assert.True(Result);

            Result = NodeTreeHelper.IsStringProperty(SimpleIdentifier, nameof(Identifier.Documentation));
            Assert.False(Result);

            Result = NodeTreeHelper.IsStringProperty(SimpleIdentifier, nameof(QueryExpression.Query));
            Assert.False(Result);
        }

        [Test]
        public static void TestIsGuidProperty()
        {
            bool Result;

            Class NewClass = NodeHelper.CreateSimpleClass("a");

            Result = NodeTreeHelper.IsGuidProperty(NewClass, nameof(Class.ClassGuid));
            Assert.True(Result);

            Result = NodeTreeHelper.IsGuidProperty(NewClass, nameof(Class.EntityName));
            Assert.False(Result);

            Result = NodeTreeHelper.IsGuidProperty(NewClass, nameof(QueryExpression.Query));
            Assert.False(Result);
        }
    }
}
