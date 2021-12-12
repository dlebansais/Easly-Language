﻿namespace TestEaslyLanguage
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

        private class TestRoot : Root
        {
            public IList<Class> ClassList { get; set; } = new List<Class>();
            public IList<Library> LibraryList { get; set; } = new List<Library>();
        }

        private class TestLibrary : Library
        {
            public IList<Import> ImportList { get; set; } = new List<Import>();
            public IList<Identifier> ClassIdentifierList { get; set; } = new List<Identifier>();
        }

        [Test]
        public static void TestEnumChildNodeProperties()
        {
            bool Result;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            WalkCallbacks<TestContext> NewWalkCallbacks = new WalkCallbacks<TestContext>();

            Class SimpleClass = NodeHelper.CreateSimpleClass("a");
            List<Class> SimpleClassList = new() { SimpleClass };
            List<Library> EmptyLibraryList = new();
            GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
            List<GlobalReplicate> SimpleGlobalReplicateList = new() { SimpleGlobalReplicate };
            Root NewRoot = NodeHelper.CreateRoot(SimpleClassList, EmptyLibraryList, SimpleGlobalReplicateList);

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.IsRecursive = true;

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NodeTreeHelperOptional.AssignChildNode(SimpleClass, nameof(Class.FromIdentifier));

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerRoot = (Node root, WalkCallbacks<TestContext> callbacks, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerRoot = (Node root, WalkCallbacks<TestContext> callbacks, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return propertyName != "EntityName"; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return propertyName != "FromIdentifier"; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return propertyName != "ReplicateName"; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerBlockList = (Node node, string propertyName, IBlockList blockList, WalkCallbacks<TestContext> callback, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerBlockList = (Node node, string propertyName, IBlockList blockList, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.IsRecursive = false;

            NewWalkCallbacks.HandlerBlockList = (Node node, string propertyName, IBlockList blockList, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.IsRecursive = true;

            NewWalkCallbacks.HandlerBlock = (Node node, string propertyName, IBlockList blockList, IBlock block, WalkCallbacks<TestContext> callback, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerBlock = (Node node, string propertyName, IBlockList blockList, IBlock block, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerList = (Node node, string propertyName, IReadOnlyList<Node> nodeList, WalkCallbacks<TestContext> callback, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerList = (Node node, string propertyName, IReadOnlyList<Node> nodeList, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.IsRecursive = false;

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.IsRecursive = true;

            NewWalkCallbacks.HandlerString = (Node node, string propertyName, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerString = (Node node, string propertyName, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerGuid = (Node node, string propertyName, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerGuid = (Node node, string propertyName, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerEnum = (Node node, string propertyName, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

            NewWalkCallbacks.HandlerEnum = (Node node, string propertyName, TestContext data) => { return true; };

            Result = NodeTreeWalk.Walk(NewRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.BlockSubstitution = new KeyValuePair<string, string>("Blocks", "List");

            TestRoot NewTestRoot = new();
            NewTestRoot.ClassBlocks = BlockListHelper.CreateEmptyBlockList<Class>();
            NewTestRoot.LibraryBlocks = BlockListHelper.CreateEmptyBlockList<Library>();
            NewTestRoot.Replicates = new List<GlobalReplicate>();

            TestLibrary NewTestLibrary = new();
            NewTestLibrary.ImportBlocks = BlockListHelper.CreateEmptyBlockList<Import>();
            NewTestLibrary.ClassIdentifierBlocks = BlockListHelper.CreateEmptyBlockList<Identifier>();
            NewTestLibrary.EntityName = NodeHelper.CreateSimpleName("a");
            NewTestLibrary.FromIdentifier = OptionalReferenceHelper.CreateEmptyReference<Identifier>();

            NewTestRoot.LibraryList.Add(NewTestLibrary);

            Result = NodeTreeWalk.Walk(NewTestRoot, NewWalkCallbacks, new TestContext());
            Assert.True(Result);

            NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return false; };

            Result = NodeTreeWalk.Walk(NewTestRoot, NewWalkCallbacks, new TestContext());
            Assert.False(Result);

#if !DEBUG
            Expression NullExpression = null!;
            TestContext NullTestContext = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(NullExpression, NewWalkCallbacks, new TestContext()); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(DefaultExpression, NewWalkCallbacks, NullTestContext); });
#endif
        }
    }
}
