namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeTreeWalkCoverage
    {
        private class TestContext
        {
        }

        [Test]
        public static void TestEnumChildNodeProperties()
        {
            bool Result;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            WalkCallbacks<TestContext> NewWalkCallbacks = new WalkCallbacks<TestContext>();

            Class SimpleClass = NodeHelper.CreateSimpleClass("a");
            NodeTreeHelperOptional.AssignChildNode(SimpleClass, nameof(Class.FromIdentifier));
            List<Class> SimpleClassList = new() { SimpleClass };
            List<Library> EmptyLibraryList = new();
            GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
            List<GlobalReplicate> SimpleGlobalReplicateList = new() { SimpleGlobalReplicate };
            Root NewRoot = NodeHelper.CreateRoot(SimpleClassList, EmptyLibraryList, SimpleGlobalReplicateList);

            NewWalkCallbacks.HandlerRoot = (Node root, WalkCallbacks<TestContext> callbacks, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerBlockList = (Node node, string propertyName, IBlockList blockList, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerBlock = (Node node, string propertyName, IBlockList blockList, IBlock block, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerList = (Node node, string propertyName, IReadOnlyList<Node> nodeList, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerString = (Node node, string propertyName, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerGuid = (Node node, string propertyName, TestContext data) => { return true; };
            NewWalkCallbacks.HandlerEnum = (Node node, string propertyName, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.IsRecursive = true;

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.BlockSubstitution = new KeyValuePair<string, string>("ClassBlocks", "ClassBlocks");

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

#if !DEBUG
            Expression NullExpression = null!;
            TestContext NullTestContext = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(NullExpression, NewWalkCallbacks, new TestContext()); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(DefaultExpression, NewWalkCallbacks, NullTestContext); });
#endif
        }
    }
}
