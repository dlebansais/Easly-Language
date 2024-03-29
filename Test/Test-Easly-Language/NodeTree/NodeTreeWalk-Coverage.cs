﻿namespace TestEaslyLanguage;

using System;
using System.Collections.Generic;
using BaseNode;
using BaseNodeHelper;
using NUnit.Framework;

[TestFixture]
public partial class NodeTreeWalkCoverage
{
    private class TestContext
    {
    }

    private class TestRoot : Root
    {
        public TestRoot()
            : base(NodeHelper.CreateEmptyDocument(), BlockListHelper.CreateEmptyBlockList<Class>(), BlockListHelper.CreateEmptyBlockList<Library>(), new List<GlobalReplicate>())
        {
        }

        public IList<Class> ClassList { get; set; } = new List<Class>();
        public IList<Library> LibraryList { get; set; } = new List<Library>();
    }

    private class TestLibrary : Library
    {
        public TestLibrary()
            : base(NodeHelper.CreateEmptyDocument(), NodeHelper.CreateEmptyName(), OptionalReferenceHelper.CreateReference(NodeHelper.CreateEmptyIdentifier()), BlockListHelper.CreateEmptyBlockList<Import>(), BlockListHelper.CreateEmptyBlockList<Identifier>())
        {
        }

        public IList<Import> ImportList { get; set; } = new List<Import>();
        public IList<Identifier> ClassIdentifierList { get; set; } = new List<Identifier>();
    }

    [Test]
    public static void TestWalkCallbacks()
    {
        WalkCallbacks<TestContext> WalkCallback1 = new();
        WalkCallbacks<TestContext> WalkCallback2 = new();

        Assert.AreEqual(WalkCallback1, WalkCallback1);
        Assert.True(WalkCallback1 == WalkCallback2);
        Assert.False(WalkCallback1 != WalkCallback2);
        Assert.True(WalkCallback1.Equals(WalkCallback2));
        Assert.False(WalkCallback1.Equals(string.Empty));

        int HashCode1 = WalkCallback1.GetHashCode();
        int HashCode2 = WalkCallback2.GetHashCode();
        Assert.AreEqual(HashCode1, HashCode2);
    }

    [Test]
    public static void TestEnumChildNodeProperties()
    {
        bool Result;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        WalkCallbacks<TestContext> NewWalkCallbacks = new();

        Class SimpleClass = NodeHelper.CreateSimpleClass("a");
        List<Class> SimpleClassList = new() { SimpleClass };
        List<Library> EmptyLibraryList = new();
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("b");
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
        NewTestLibrary.FromIdentifier = OptionalReferenceHelper.CreateReference(NodeHelper.CreateEmptyIdentifier());

        NewTestRoot.LibraryList.Add(NewTestLibrary);

        Result = NodeTreeWalk.Walk(NewTestRoot, NewWalkCallbacks, new TestContext());
        Assert.True(Result);

        NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return false; };

        Result = NodeTreeWalk.Walk(NewTestRoot, NewWalkCallbacks, new TestContext());
        Assert.False(Result);

        NewWalkCallbacks.HandlerNode = (Node node, Node parentNode, string propertyName, WalkCallbacks<TestContext> callback, TestContext data) => { return true; };

        Inheritance SimpleInheritance = NodeHelper.CreateSimpleInheritance("c");

        NewWalkCallbacks.BlockSubstitution = new KeyValuePair<string, string>(string.Empty, string.Empty);
        NewWalkCallbacks.HandlerEnum = (Node node, string propertyName, TestContext data) => { return true; };

        Result = NodeTreeWalk.Walk(SimpleInheritance, NewWalkCallbacks, new TestContext());
        Assert.True(Result);

        NewWalkCallbacks.HandlerEnum = (Node node, string propertyName, TestContext data) => { return propertyName != "ForgetIndexer"; };

        Result = NodeTreeWalk.Walk(SimpleInheritance, NewWalkCallbacks, new TestContext());
        Assert.False(Result);

#if !DEBUG
        Expression NullExpression = null!;
        TestContext NullTestContext = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(NullExpression, NewWalkCallbacks, new TestContext()); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(DefaultExpression, NewWalkCallbacks, NullTestContext); });
#endif
    }
}
