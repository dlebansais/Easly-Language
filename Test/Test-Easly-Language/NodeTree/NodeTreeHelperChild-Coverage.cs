namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public partial class NodeTreeHelperChildCoverage
    {
        [Test]
        public static void TestIsChildNodeProperty()
        {
            bool Result;

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();
            Type ChildNodeType;

            Result = NodeTreeHelperChild.IsChildNodeProperty(DefaultExpression, nameof(QueryExpression.Query), out ChildNodeType);
            Assert.True(Result);
            Assert.AreEqual(ChildNodeType, typeof(QualifiedName));

            Result = NodeTreeHelperChild.IsChildNodeProperty(typeof(QueryExpression), nameof(QueryExpression.Query), out ChildNodeType);
            Assert.True(Result);
            Assert.AreEqual(ChildNodeType, typeof(QualifiedName));

            Result = NodeTreeHelperChild.IsChildNodeProperty(typeof(Identifier), nameof(QueryExpression.Query), out _);
            Assert.False(Result);

            Result = NodeTreeHelperChild.IsChildNodeProperty(typeof(QueryExpression), nameof(QueryExpression.ArgumentBlocks), out _);
            Assert.False(Result);

#if !DEBUG
            Expression NullExpression = null!;
            string NullString = null!;
            Type NullType = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNodeProperty(NullExpression, nameof(QueryExpression.Query), out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNodeProperty(DefaultExpression, NullString, out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNodeProperty(NullType, nameof(QueryExpression.Query), out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNodeProperty(typeof(QueryExpression), NullString, out _); });
#endif
        }

        [Test]
        public static void TestIsChildNode()
        {
            bool Result;

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Result = NodeTreeHelperChild.IsChildNode(DefaultExpression, nameof(QueryExpression.Query), DefaultExpression.Query);
            Assert.True(Result);

            Result = NodeTreeHelperChild.IsChildNode(DefaultExpression, nameof(Identifier.Text), DefaultExpression.Query);
            Assert.False(Result);

            Result = NodeTreeHelperChild.IsChildNode(DefaultExpression, nameof(QueryExpression.ArgumentBlocks), DefaultExpression.Query);
            Assert.False(Result);

            Result = NodeTreeHelperChild.IsChildNode(DefaultExpression, nameof(QueryExpression.Query), DefaultExpression);
            Assert.False(Result);

#if !DEBUG
            Expression NullExpression = null!;
            string NullString = null!;
            QualifiedName NullQualifiedName = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNode(NullExpression, nameof(QueryExpression.Query), DefaultExpression.Query); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNode(DefaultExpression, NullString, DefaultExpression.Query); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.IsChildNode(DefaultExpression, nameof(QueryExpression.Query), NullQualifiedName); });
#endif
        }

        [Test]
        public static void TestChildInterfaceType()
        {
            Type Result;

            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            Result = NodeTreeHelperChild.ChildInterfaceType(DefaultExpression, nameof(QueryExpression.Query));
            Assert.AreEqual(Result, typeof(QualifiedName));

            Assert.Throws<ArgumentException>(() => { NodeTreeHelperChild.ChildInterfaceType(DefaultExpression, nameof(Identifier.Text)); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelperChild.ChildInterfaceType(DefaultExpression, nameof(QueryExpression.ArgumentBlocks)); });

#if !DEBUG
            Expression NullExpression = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.ChildInterfaceType(NullExpression, nameof(QueryExpression.Query)); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.ChildInterfaceType(DefaultExpression, nameof(NullString)); });
#endif
        }

        [Test]
        public static void TestGetChildNode()
        {
            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();

            NodeTreeHelperChild.GetChildNode(DefaultExpression, nameof(QueryExpression.Query), out Node ChildNode);
            Assert.AreEqual(ChildNode, DefaultExpression.Query);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelperChild.GetChildNode(DefaultExpression, nameof(Identifier.Text), out _); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelperChild.GetChildNode(DefaultExpression, nameof(QueryExpression.ArgumentBlocks), out _); });

#if !DEBUG
            Expression NullExpression = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.GetChildNode(NullExpression, nameof(QueryExpression.Query), out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.GetChildNode(DefaultExpression, NullString, out _); });
#endif
        }

        [Test]
        public static void TestSetChildNode()
        {
            QueryExpression DefaultExpression = (QueryExpression)NodeHelper.CreateDefaultExpression();
            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            NodeTreeHelperChild.SetChildNode(DefaultExpression, nameof(QueryExpression.Query), SimpleQualifiedName);

            Assert.Throws<ArgumentException>(() => { NodeTreeHelperChild.SetChildNode(DefaultExpression, nameof(Identifier.Text), SimpleQualifiedName); });
            Assert.Throws<ArgumentException>(() => { NodeTreeHelperChild.SetChildNode(DefaultExpression, nameof(QueryExpression.ArgumentBlocks), SimpleQualifiedName); });

#if !DEBUG
            Expression NullExpression = null!;
            string NullString = null!;
            QualifiedName NullQualifiedName = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.SetChildNode(NullExpression, nameof(QueryExpression.Query), SimpleQualifiedName); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.SetChildNode(DefaultExpression, NullString, SimpleQualifiedName); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperChild.SetChildNode(DefaultExpression, nameof(QueryExpression.Query), NullQualifiedName); });
#endif
        }
    }
}
