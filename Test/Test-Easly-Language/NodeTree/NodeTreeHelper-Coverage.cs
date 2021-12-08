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
        private class DescendantExpression : QueryExpression
        {
        };

        [Test]
        public static void TestEnumChildNodeProperties()
        {
            IList<string> ChildNodePropertyList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            ChildNodePropertyList = NodeTreeHelper.EnumChildNodeProperties(DefaultExpression);
            Assert.True(ChildNodePropertyList.Contains(nameof(Expression.Documentation)));

            ChildNodePropertyList = NodeTreeHelper.EnumChildNodeProperties(typeof(QueryExpression));
            Assert.True(ChildNodePropertyList.Contains(nameof(Expression.Documentation)));

            DescendantExpression NewDescendantExpression = new DescendantExpression();

            ChildNodePropertyList = NodeTreeHelper.EnumChildNodeProperties(NewDescendantExpression);
            Assert.True(ChildNodePropertyList.Contains(nameof(Expression.Documentation)));

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.EnumChildNodeProperties(typeof(string)); });

#if !DEBUG
            Expression NullExpression = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.EnumChildNodeProperties(NullExpression); });
#endif
        }

        [Test]
        public static void TestIsBlockType()
        {
            bool Result;

            Result = NodeTreeHelper.IsBlockType(typeof(IBlock<Identifier>));
            Assert.True(Result);

            Result = NodeTreeHelper.IsBlockType(typeof(string));
            Assert.False(Result);

            Result = NodeTreeHelper.IsBlockType(typeof(IDisposable));
            Assert.False(Result);

            Result = NodeTreeHelper.IsBlockType(typeof(IEnumerable<int>));
            Assert.False(Result);

#if !DEBUG
            Type NullType = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsBlockType(NullType); });
#endif
        }

        [Test]
        public static void TestIsTextNode()
        {
            bool Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Result = NodeTreeHelper.IsTextNode(SimpleIdentifier);
            Assert.True(Result);

            Result = NodeTreeHelper.IsTextNode(DefaultExpression);
            Assert.False(Result);

#if !DEBUG
            Identifier NullIdentifier = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsTextNode(NullIdentifier); });
#endif
        }

        [Test]
        public static void TestIsAssignable()
        {
            bool Result;

            QueryExpression SimpleQueryExpression = NodeHelper.CreateSimpleQueryExpression("b");
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            Result = NodeTreeHelper.IsAssignable(SimpleQueryExpression, nameof(QueryExpression.Query), SimpleQualifiedName);
            Assert.True(Result);

#if !DEBUG
            QueryExpression NullQueryExpression = null!;
            QualifiedName NullQualifiedName = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsAssignable(NullQueryExpression, nameof(QueryExpression.Query), SimpleQualifiedName); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsAssignable(SimpleQueryExpression, NullQualifiedName, SimpleQualifiedName); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsAssignable(SimpleQueryExpression, nameof(QueryExpression.Query), NullString); });
#endif
        }

        [Test]
        public static void TestGetOptionalNodes()
        {
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            PrecursorExpression NewPrecursorExpression = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList);

            NodeTreeHelper.GetOptionalNodes(NewPrecursorExpression, out IDictionary<string, IOptionalReference> OptionalNodesTable);
            Assert.True(OptionalNodesTable.ContainsKey(nameof(PrecursorExpression.AncestorType)));
            Assert.AreEqual(OptionalNodesTable.Count, 1);

#if !DEBUG
            PrecursorExpression NullPrecursorExpression = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetOptionalNodes(NullPrecursorExpression, out _); });
#endif
        }

        [Test]
        public static void TestGetArgumentBlocks()
        {
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            PrecursorExpression NewPrecursorExpression = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList);

            NodeTreeHelper.GetArgumentBlocks(NewPrecursorExpression, out IDictionary<string, IBlockList<Argument>> ArgumentBlocksTable);
            Assert.True(ArgumentBlocksTable.ContainsKey(nameof(PrecursorExpression.ArgumentBlocks)));
            Assert.AreEqual(ArgumentBlocksTable.Count, 1);

#if !DEBUG
            PrecursorExpression NullPrecursorExpression = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetArgumentBlocks(NullPrecursorExpression, out _); });
#endif
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

#if !DEBUG
            Identifier NullIdentifier = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetString(NullIdentifier, nameof(Identifier.Text)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetString(SimpleIdentifier, NullString); });
#endif
        }

        [Test]
        public static void TestSetString()
        {
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            NodeTreeHelper.SetString(SimpleIdentifier, nameof(Identifier.Text), "b");

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetString(DefaultExpression, nameof(Identifier.Text), "b"); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetString(DefaultExpression, nameof(QueryExpression.Query), "b"); });

#if !DEBUG
            Identifier NullIdentifier = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetString(NullIdentifier, nameof(Identifier.Text), "b"); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetString(SimpleIdentifier, NullString, "b"); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetString(SimpleIdentifier, nameof(Identifier.Text), NullString); });
#endif
        }

        [Test]
        public static void TestGetEnumValue()
        {
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            NodeTreeHelper.GetEnumValue(SimpleAnchoredType, nameof(AnchoredType.AnchorKind));

#if !DEBUG
            AnchoredType NullAnchoredType = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetEnumValue(NullAnchoredType, nameof(AnchoredType.AnchorKind)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetEnumValue(SimpleAnchoredType, NullString); });
#endif
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

#if !DEBUG
            AnchoredType NullAnchoredType = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetEnumValueAndRange(NullAnchoredType, nameof(AnchoredType.AnchorKind), out _, out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetEnumValueAndRange(SimpleAnchoredType, NullString, out _, out _); });
#endif
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

#if !DEBUG
            AnchoredType NullAnchoredType = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetEnumValue(NullAnchoredType, nameof(AnchoredType.AnchorKind), AnchorKinds.Creation); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetEnumValue(SimpleAnchoredType, NullString, AnchorKinds.Creation); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetEnumValue(NullAnchoredType, nameof(AnchoredType.AnchorKind), (int)AnchorKinds.Creation); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetEnumValue(SimpleAnchoredType, NullString, (int)AnchorKinds.Creation); });
#endif
        }

        [Test]
        public static void TestCopyEnumProperty()
        {
            QualifiedName FirstQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType FirstAnchoredType = NodeHelper.CreateAnchoredType(FirstQualifiedName, AnchorKinds.Declaration);
            QualifiedName SecondQualifiedName = NodeHelper.CreateSimpleQualifiedName("b");
            AnchoredType SecondAnchoredType = NodeHelper.CreateAnchoredType(SecondQualifiedName, AnchorKinds.Creation);

            NodeTreeHelper.CopyEnumProperty(FirstAnchoredType, SecondAnchoredType, nameof(AnchoredType.AnchorKind));

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyEnumProperty(FirstAnchoredType, DefaultExpression, nameof(AnchoredType.AnchorKind)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyEnumProperty(FirstAnchoredType, SecondAnchoredType, nameof(QueryExpression.Query)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyEnumProperty(FirstAnchoredType, SecondAnchoredType, nameof(AnchoredType.AnchoredName)); });

#if !DEBUG
            AnchoredType NullAnchoredType = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyEnumProperty(NullAnchoredType, SecondAnchoredType, nameof(AnchoredType.AnchorKind)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyEnumProperty(FirstAnchoredType, NullAnchoredType, nameof(AnchoredType.AnchorKind)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyEnumProperty(FirstAnchoredType, SecondAnchoredType, NullString); });
#endif
        }

        [Test]
        public static void TestGetEnumRange()
        {
            NodeTreeHelper.GetEnumRange(typeof(AnchoredType), nameof(AnchoredType.AnchorKind), out int Min, out int Max);
            Assert.AreEqual(Min, (int)AnchorKinds.Declaration);
            Assert.AreEqual(Max, (int)AnchorKinds.Creation);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumRange(typeof(Inheritance), nameof(Inheritance.DiscontinueBlocks), out _, out _); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetEnumRange(typeof(Inheritance), nameof(QueryExpression.Query), out _, out _); });

#if !DEBUG
            Type NullType = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetEnumRange(NullType, nameof(AnchoredType.AnchorKind), out _, out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetEnumRange(typeof(AnchoredType), NullString, out _, out _); });
#endif
        }

        [Test]
        public static void TestGetGuid()
        {
            Class NewClass = NodeHelper.CreateSimpleClass("a");

            Guid Guid = NodeTreeHelper.GetGuid(NewClass, nameof(Class.ClassGuid));
            Assert.AreEqual(Guid, NewClass.ClassGuid);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetGuid(NewClass, nameof(Class.ClassPath)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.GetGuid(NewClass, nameof(QueryExpression.Query)); });

#if !DEBUG
            Class NullClass = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetGuid(NullClass, nameof(Class.ClassGuid)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetGuid(NewClass, NullString); });
#endif
        }

        [Test]
        public static void TestGetComment()
        {
            string Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            Result = NodeTreeHelper.GetCommentText(SimpleIdentifier);
            Assert.AreEqual(Result, string.Empty);

#if !DEBUG
            Identifier NullIdentifier = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.GetCommentText(NullIdentifier); });
#endif
        }

        [Test]
        public static void TestSetComment()
        {
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            NodeTreeHelper.SetCommentText(SimpleIdentifier, "a");
            Assert.AreEqual(SimpleIdentifier.Documentation.Comment, "a");

#if !DEBUG
            Identifier NullIdentifier = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetCommentText(NullIdentifier, "a"); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetCommentText(SimpleIdentifier, NullString); });
#endif
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

#if !DEBUG
            Identifier NullIdentifier = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsDocumentProperty(NullIdentifier, nameof(Identifier.Documentation)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsDocumentProperty(SimpleIdentifier, NullString); });
#endif
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

#if !DEBUG
            Identifier NullIdentifier = null!;
            Document NullDocumentation = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetDocumentation(NullIdentifier, SimpleDocumentation); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetDocumentation(SimpleIdentifier, NullDocumentation); });
#endif
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

#if !DEBUG
            Identifier NullIdentifier = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyDocumentation(NullIdentifier, SecondIdentifier, cloneCommentGuid: false); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyDocumentation(FirstIdentifier, NullIdentifier, cloneCommentGuid: false); });
#endif
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

#if !DEBUG
            IBlock NullBlock = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyDocumentation(NullBlock, SecondIdentifierBlock, cloneCommentGuid: false); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyDocumentation(FirstIdentifierBlock, NullBlock, cloneCommentGuid: false); });
#endif
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

#if !DEBUG
            IBlockList NullBlockList = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyDocumentation(NullBlockList, SecondIdentifierBlockList, cloneCommentGuid: false); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyDocumentation(FirstIdentifierBlockList, NullBlockList, cloneCommentGuid: false); });
#endif
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

            Result = NodeTreeHelper.IsBooleanProperty(typeof(Inheritance), nameof(Inheritance.ForgetIndexer));
            Assert.True(Result);

#if !DEBUG
            Inheritance NullInheritance = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsBooleanProperty(NullInheritance, nameof(Inheritance.ForgetIndexer)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsBooleanProperty(NewInheritance, NullString); });
#endif
        }

        [Test]
        public static void TestSetBooleanProperty()
        {
            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");

            NodeTreeHelper.SetBooleanProperty(NewInheritance, nameof(Inheritance.ForgetIndexer), true);
            Assert.True(NewInheritance.ForgetIndexer);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetBooleanProperty(NewInheritance, nameof(QueryExpression.Query), true); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.SetBooleanProperty(NewInheritance, nameof(Inheritance.ForgetBlocks), true); });

#if !DEBUG
            Inheritance NullInheritance = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetBooleanProperty(NullInheritance, nameof(Inheritance.ForgetIndexer), true); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.SetBooleanProperty(NewInheritance, NullString, true); });
#endif
        }

        [Test]
        public static void TestCopyBooleanProperty()
        {
            Inheritance FirstInheritance = NodeHelper.CreateSimpleInheritance("a");
            Inheritance SecondInheritance = NodeHelper.CreateSimpleInheritance("b");

            FirstInheritance.ForgetIndexer = true;

            NodeTreeHelper.CopyBooleanProperty(FirstInheritance, SecondInheritance, nameof(Inheritance.ForgetIndexer));
            Assert.True(SecondInheritance.ForgetIndexer);

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyBooleanProperty(FirstInheritance, DefaultExpression, nameof(Inheritance.ForgetIndexer)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyBooleanProperty(FirstInheritance, SecondInheritance, nameof(Inheritance.ForgetBlocks)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelper.CopyBooleanProperty(FirstInheritance, SecondInheritance, nameof(QueryExpression.Query)); });

#if !DEBUG
            Inheritance NullInheritance = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyBooleanProperty(NullInheritance, SecondInheritance, nameof(Inheritance.ForgetIndexer)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyBooleanProperty(FirstInheritance, NullInheritance, nameof(Inheritance.ForgetIndexer)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.CopyBooleanProperty(FirstInheritance, SecondInheritance, NullString); });
#endif
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

            Result = NodeTreeHelper.IsStringProperty(typeof(Identifier), nameof(Identifier.Text));
            Assert.True(Result);

#if !DEBUG
            Identifier NullIdentifier = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsStringProperty(NullIdentifier, nameof(Identifier.Text)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsStringProperty(SimpleIdentifier, NullString); });
#endif
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

            Result = NodeTreeHelper.IsGuidProperty(typeof(Class), nameof(Class.ClassGuid));
            Assert.True(Result);

#if !DEBUG
            Class NullClass = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsGuidProperty(NullClass, nameof(Class.ClassGuid)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsGuidProperty(NewClass, NullString); });
#endif
        }

        [Test]
        public static void TestIsEnumProperty()
        {
            bool Result;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType SimpleAnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            Result = NodeTreeHelper.IsEnumProperty(SimpleAnchoredType, nameof(AnchoredType.AnchorKind));
            Assert.True(Result);

            Result = NodeTreeHelper.IsEnumProperty(SimpleAnchoredType, nameof(AnchoredType.AnchoredName));
            Assert.False(Result);

            Result = NodeTreeHelper.IsEnumProperty(SimpleAnchoredType, nameof(QueryExpression.Query));
            Assert.False(Result);

            Result = NodeTreeHelper.IsEnumProperty(typeof(AnchoredType), nameof(AnchoredType.AnchorKind));
            Assert.True(Result);

#if !DEBUG
            AnchoredType NullAnchoredType = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsEnumProperty(NullAnchoredType, nameof(AnchoredType.AnchorKind)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelper.IsEnumProperty(SimpleAnchoredType, NullString); });
#endif
        }
    }
}
