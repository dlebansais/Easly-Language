namespace TestEaslyLanguage;

using System.Collections.Generic;
using ArgumentException = System.ArgumentException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using BaseNode;
using BaseNodeHelper;
using NotNullReflection;
using NUnit.Framework;

[TestFixture]
public partial class NodeTreeHelperListCoverage
{
    [Test]
    public static void TestIsNodeListProperty()
    {
        bool Result;

        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        Type ChildNodeType;

        Result = NodeTreeHelperList.IsNodeListProperty(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), out ChildNodeType);
        Assert.True(Result);
        Assert.AreEqual(ChildNodeType, Type.FromTypeof<Pattern>());

        Result = NodeTreeHelperList.IsNodeListProperty(Type.FromTypeof<GlobalReplicate>(), nameof(GlobalReplicate.Patterns), out ChildNodeType);
        Assert.True(Result);
        Assert.AreEqual(ChildNodeType, Type.FromTypeof<Pattern>());

        Result = NodeTreeHelperList.IsNodeListProperty(SimpleGlobalReplicate, nameof(Identifier.Text), out ChildNodeType);
        Assert.False(Result);

        Result = NodeTreeHelperList.IsNodeListProperty(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), out ChildNodeType);
        Assert.False(Result);

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Type NullType = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsNodeListProperty(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsNodeListProperty(SimpleGlobalReplicate, NullString, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsNodeListProperty(NullType, nameof(GlobalReplicate.Patterns), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsNodeListProperty(Type.FromTypeof<GlobalReplicate>(), NullString, out _); });
#endif
    }

    [Test]
    public static void TestIsListChildNode()
    {
        bool Result;

        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");

        for (int i = 0; i < SimpleGlobalReplicate.Patterns.Count; i++)
        {
            Pattern Item = SimpleGlobalReplicate.Patterns[i];

            Result = NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), i, Item);
            Assert.True(Result);
        }

        Pattern FirstPattern = SimpleGlobalReplicate.Patterns[0];

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, nameof(Identifier.Text), 0, FirstPattern); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), 0, FirstPattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), -1, FirstPattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimpleGlobalReplicate.Patterns.Count, FirstPattern); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Pattern NullPattern = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsListChildNode(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, FirstPattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, NullString, 0, FirstPattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.IsListChildNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, NullPattern); });
#endif
    }

    [Test]
    public static void TestListInterfaceType()
    {
        Type Result;

        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");

        Result = NodeTreeHelperList.ListItemType(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns));
        Assert.AreEqual(Result, Type.FromTypeof<Pattern>());

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ListItemType(SimpleGlobalReplicate, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ListItemType(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName)); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ListItemType(NullGlobalReplicate, nameof(GlobalReplicate.Patterns)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ListItemType(SimpleGlobalReplicate, NullString); });
#endif
    }

    [Test]
    public static void TestGetChildNodeList()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        IReadOnlyList<Node> ChildNodeList;

        NodeTreeHelperList.GetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), out ChildNodeList);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns, ChildNodeList);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.GetChildNodeList(SimpleGlobalReplicate, nameof(Identifier.Text), out ChildNodeList); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.GetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), out ChildNodeList); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.GetChildNodeList(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.GetChildNodeList(SimpleGlobalReplicate, NullString, out _); });
#endif
    }

    [Test]
    public static void TestClearChildNodeList()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");

        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 1);
        NodeTreeHelperList.ClearChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns));
        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 0);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ClearChildNodeList(SimpleGlobalReplicate, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ClearChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName)); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ClearChildNodeList(NullGlobalReplicate, nameof(GlobalReplicate.Patterns)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ClearChildNodeList(SimpleGlobalReplicate, NullString); });
#endif
    }

    [Test]
    public static void TestGetLastListIndex()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        int Index;

        NodeTreeHelperList.GetLastListIndex(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), out Index);
        Assert.AreEqual(Index, 1);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.GetLastListIndex(SimpleGlobalReplicate, nameof(Identifier.Text), out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.GetLastListIndex(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), out _); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.GetLastListIndex(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.GetLastListIndex(SimpleGlobalReplicate, NullString, out _); });
#endif
    }

    [Test]
    public static void TestSetChildNodeList()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        Pattern SimplePattern = NodeHelper.CreateSimplePattern("b");
        List<Pattern> SimplePatternList = new() { SimplePattern };

        NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimplePatternList);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns, SimplePatternList);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, nameof(Identifier.Text), SimplePatternList); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), SimplePatternList); });

        List<int> IntList = new();
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), IntList); });

        List<Pattern> EmptyPatternList = new();
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), EmptyPatternList); });

        Root NewRoot = NodeHelper.CreateRoot(new List<Class>(), new List<Library>(), new List<GlobalReplicate>());
        List<GlobalReplicate> EmptyGlobalReplicateList = new();
        NodeTreeHelperList.SetChildNodeList(NewRoot, nameof(Root.Replicates), EmptyGlobalReplicateList);

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        List<Pattern> NullPatternList = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.SetChildNodeList(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), SimplePatternList); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, NullString, SimplePatternList); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.SetChildNodeList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), NullPatternList); });
#endif
    }

    [Test]
    public static void TestInsertIntoList()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        Pattern SimplePattern = NodeHelper.CreateSimplePattern("b");

        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 1);
        NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimplePattern);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 2);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns[0], SimplePattern);

        Pattern OtherPattern = NodeHelper.CreateSimplePattern("c");

        NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimpleGlobalReplicate.Patterns.Count, OtherPattern);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 3);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns[SimpleGlobalReplicate.Patterns.Count - 1], OtherPattern);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(Identifier.Text), 0, SimplePattern); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), 0, SimplePattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), -1, SimplePattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimpleGlobalReplicate.Patterns.Count + 1, SimplePattern); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Pattern NullPattern = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.InsertIntoList(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimplePattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, NullString, 0, SimplePattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, NullPattern); });
#endif
    }

    [Test]
    public static void TestRemoveFromList()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        Pattern SimplePattern = NodeHelper.CreateSimplePattern("b");

        NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimplePattern);

        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 2);
        NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 1);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 1);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns[0], SimplePattern);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, nameof(Identifier.Text), 0); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), -1); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimpleGlobalReplicate.Patterns.Count); });

        NeverEmptyException? Exception = Assert.Throws<NeverEmptyException>(() => { NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0); });
        Assert.NotNull(Exception);
        Assert.AreEqual(Exception?.Node, SimpleGlobalReplicate);
        Assert.AreEqual(Exception?.PropertyName, nameof(GlobalReplicate.Patterns));

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.RemoveFromList(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), 0); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.RemoveFromList(SimpleGlobalReplicate, NullString, 0); });
#endif
    }

    [Test]
    public static void TestReplaceNode()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        Pattern SimplePattern = NodeHelper.CreateSimplePattern("b");

        NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimplePattern);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 1);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns[0], SimplePattern);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(Identifier.Text), 0, SimplePattern); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), 0, SimplePattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), -1, SimplePattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimpleGlobalReplicate.Patterns.Count, SimplePattern); });

        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimpleIdentifier); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Pattern NullPattern = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ReplaceNode(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimplePattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, NullString, 0, SimplePattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.ReplaceNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, NullPattern); });
#endif
    }

    [Test]
    public static void TestMoveNode()
    {
        GlobalReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleGlobalReplicate("a");
        Pattern SimplePattern = NodeHelper.CreateSimplePattern("b");

        NodeTreeHelperList.InsertIntoList(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimplePattern);

        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 2);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns[0], SimplePattern);
        NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, 1);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns.Count, 2);
        Assert.AreEqual(SimpleGlobalReplicate.Patterns[1], SimplePattern);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(Identifier.Text), 0, 0); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(GlobalReplicate.ReplicateName), 0, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), -1, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), SimpleGlobalReplicate.Patterns.Count, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, -1); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, SimpleGlobalReplicate.Patterns.Count); });

#if !DEBUG
        GlobalReplicate NullGlobalReplicate = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.MoveNode(NullGlobalReplicate, nameof(GlobalReplicate.Patterns), 0, 0); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperList.MoveNode(SimpleGlobalReplicate, NullString, 0, 0); });
#endif
    }
}
