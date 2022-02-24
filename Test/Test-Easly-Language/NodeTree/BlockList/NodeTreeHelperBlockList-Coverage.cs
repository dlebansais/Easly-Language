namespace TestEaslyLanguage;

using System.Collections;
using System.Collections.Generic;
using ArgumentException = System.ArgumentException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using BaseNode;
using BaseNodeHelper;
using NotNullReflection;
using NUnit.Framework;

[TestFixture]
public partial class NodeTreeHelperBlockListCoverage
{
    private class TestBlockList : IBlockList
    {
        public Document Documentation { get; } = NodeHelper.CreateEmptyDocument();
        public IList NodeBlockList { get; } = new List<int>();
    }

    [Test]
    public static void TestIsBlockListProperty()
    {
        bool Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Type ChildNodeType;

        Result = NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, nameof(Export.ClassIdentifierBlocks), out ChildNodeType);
        Assert.True(Result);
        Assert.AreEqual(ChildNodeType, Type.FromTypeof<Identifier>());

        Result = NodeTreeHelperBlockList.IsBlockListProperty(Type.FromTypeof<Export>(), nameof(Export.ClassIdentifierBlocks), out ChildNodeType);
        Assert.True(Result);
        Assert.AreEqual(ChildNodeType, Type.FromTypeof<Identifier>());

        Result = NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, nameof(Identifier.Text), out ChildNodeType);
        Assert.False(Result);

        Result = NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, nameof(Export.EntityName), out ChildNodeType);
        Assert.False(Result);

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Type NullType = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(NullExport, nameof(Export.ClassIdentifierBlocks), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, NullString, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(NullType, nameof(Export.ClassIdentifierBlocks), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(Type.FromTypeof<Export>(), NullString, out _); });
#endif
    }

    [Test]
    public static void TestGetBlockItemType()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Type ChildNodeType;

        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 1);
        IBlock Block = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];

        NodeTreeHelperBlockList.GetBlockItemType(Block, out ChildNodeType);
        Assert.AreEqual(ChildNodeType, Type.FromTypeof<Identifier>());

#if !DEBUG
        IBlock NullBlock = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetBlockItemType(NullBlock, out _); });
#endif
    }

    [Test]
    public static void TestGetBlockList()
    {
        IBlockList Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        Result = NodeTreeHelperBlockList.GetBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks));
        Assert.AreEqual(Result, SimpleExport.ClassIdentifierBlocks);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetBlockList(SimpleExport, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetBlockList(SimpleExport, nameof(Export.EntityName)); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetBlockList(NullExport, nameof(Export.ClassIdentifierBlocks)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetBlockList(SimpleExport, NullString); });
#endif
    }

    [Test]
    public static void TestGetChildBlockList()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        NodeTreeHelperBlockList.GetChildBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), out IList<NodeTreeBlock> ChildBlockList);
        Assert.AreEqual(ChildBlockList.Count, 1);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetChildBlockList(SimpleExport, nameof(Identifier.Text), out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetChildBlockList(SimpleExport, nameof(Export.EntityName), out _); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlockList(NullExport, nameof(Export.ClassIdentifierBlocks), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlockList(SimpleExport, NullString, out _); });
#endif
    }

    [Test]
    public static void TestBlockListItemType()
    {
        Type Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        Result = NodeTreeHelperBlockList.BlockListItemType(SimpleExport, nameof(Export.ClassIdentifierBlocks));
        Assert.AreEqual(Result, Type.FromTypeof<Identifier>());

        Result = NodeTreeHelperBlockList.BlockListItemType(Type.FromTypeof<Export>(), nameof(Export.ClassIdentifierBlocks));
        Assert.AreEqual(Result, Type.FromTypeof<Identifier>());

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.BlockListItemType(SimpleExport, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.BlockListItemType(SimpleExport, nameof(Export.EntityName)); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.BlockListItemType(NullExport, nameof(Export.ClassIdentifierBlocks)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.BlockListItemType(SimpleExport, NullString); });
#endif
    }

    [Test]
    public static void TestBlockListBlockType()
    {
        Type Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        Result = NodeTreeHelperBlockList.BlockListBlockType(SimpleExport, nameof(Export.ClassIdentifierBlocks));
        Assert.AreEqual(Result, Type.FromTypeof<IBlock<Identifier>>());

        Result = NodeTreeHelperBlockList.BlockListBlockType(Type.FromTypeof<Export>(), nameof(Export.ClassIdentifierBlocks));
        Assert.AreEqual(Result, Type.FromTypeof<IBlock<Identifier>>());

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.BlockListBlockType(SimpleExport, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.BlockListBlockType(SimpleExport, nameof(Export.EntityName)); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.BlockListBlockType(NullExport, nameof(Export.ClassIdentifierBlocks)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.BlockListBlockType(SimpleExport, NullString); });
#endif
    }

    [Test]
    public static void TestGetLastBlockIndex()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        NodeTreeHelperBlockList.GetLastBlockIndex(SimpleExport, nameof(Export.ClassIdentifierBlocks), out int Index);
        Assert.AreEqual(Index, 1);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetLastBlockIndex(SimpleExport, nameof(Identifier.Text), out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetLastBlockIndex(SimpleExport, nameof(Export.EntityName), out _); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetLastBlockIndex(NullExport, nameof(Export.ClassIdentifierBlocks), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetLastBlockIndex(SimpleExport, NullString, out _); });
#endif
    }

    [Test]
    public static void TestGetLastBlockChildIndex()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        NodeTreeHelperBlockList.GetLastBlockChildIndex(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, out int Index);
        Assert.AreEqual(Index, 1);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetLastBlockChildIndex(SimpleExport, nameof(Identifier.Text), 0, out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetLastBlockChildIndex(SimpleExport, nameof(Export.EntityName), 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetLastBlockChildIndex(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetLastBlockChildIndex(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, out _); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetLastBlockChildIndex(NullExport, nameof(Export.ClassIdentifierBlocks), 0, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetLastBlockChildIndex(SimpleExport, NullString, 0, out _); });
#endif
    }

    [Test]
    public static void TestIsBlockChildNode()
    {
        bool Result;
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Identifier FirstIdentifier = SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].NodeList[0];
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("b");

        Result = NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 0, FirstIdentifier);
        Assert.True(Result);

        Result = NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 0, SimpleIdentifier);
        Assert.False(Result);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Identifier.Text), 0, 0, FirstIdentifier); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.EntityName), 0, 0, FirstIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, 0, FirstIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 0, FirstIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, -1, FirstIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].NodeList.Count, FirstIdentifier); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(NullExport, nameof(Export.ClassIdentifierBlocks), 0, 0, FirstIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, NullString, 0, 0, FirstIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 0, NullIdentifier); });
#endif
    }

    [Test]
    public static void IsChildNode()
    {
        bool Result;
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Identifier FirstIdentifier = SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].NodeList[0];
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("b");
        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];

        Result = NodeTreeHelperBlockList.IsChildNode(FirstBlock, 0, FirstIdentifier);
        Assert.True(Result);

        Result = NodeTreeHelperBlockList.IsChildNode(FirstBlock, 0, SimpleIdentifier);
        Assert.False(Result);

        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsChildNode(FirstBlock, -1, FirstIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsChildNode(FirstBlock, FirstBlock.NodeList.Count, FirstIdentifier); });

#if !DEBUG
        IBlock NullBlock = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsChildNode(NullBlock, 0, FirstIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsChildNode(FirstBlock, 0, NullIdentifier); });
#endif
    }

    [Test]
    public static void TestGetChildBlock()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");

        NodeTreeHelperBlockList.GetChildBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, out IBlock ChildBlock);
        Assert.AreEqual(ChildBlock, SimpleExport.ClassIdentifierBlocks.NodeBlockList[0]);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetChildBlock(SimpleExport, nameof(Identifier.Text), 0, out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetChildBlock(SimpleExport, nameof(Export.EntityName), 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, out _); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlock(NullExport, nameof(Export.ClassIdentifierBlocks), 0, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlock(SimpleExport, NullString, 0, out _); });
#endif
    }

    [Test]
    public static void TestGetChildNode()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Identifier FirstIdentifier = SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].NodeList[0];
        Node ChildNode;

        NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 0, out ChildNode);
        Assert.AreEqual(ChildNode, FirstIdentifier);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Identifier.Text), 0, 0, out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Export.EntityName), 0, 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, -1, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].NodeList.Count, out _); });

        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];

        NodeTreeHelperBlockList.GetChildNode(FirstBlock, 0, out ChildNode);
        Assert.AreEqual(ChildNode, FirstIdentifier);

        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildNode(FirstBlock, -1, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.GetChildNode(FirstBlock, FirstBlock.NodeList.Count, out _); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        IBlock NullBlock = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildNode(NullExport, nameof(Export.ClassIdentifierBlocks), 0, 0, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildNode(SimpleExport, NullString, 0, 0, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildNode(NullBlock, 0, out _); });
#endif
    }

    [Test]
    public static void TestSetBlockList()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("b");
        IBlockList SimpleIdentifierBlockList = (IBlockList)BlockListHelper.CreateSimpleBlockList(SimpleIdentifier);

        NodeTreeHelperBlockList.SetBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleIdentifierBlockList);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks, SimpleIdentifierBlockList);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SetBlockList(SimpleExport, nameof(Identifier.Text), SimpleIdentifierBlockList); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SetBlockList(SimpleExport, nameof(Export.EntityName), SimpleIdentifierBlockList); });

        Name SimpleName = NodeHelper.CreateSimpleName("c");
        IBlockList SimpleNameBlockList = (IBlockList)BlockListHelper.CreateSimpleBlockList(SimpleName);
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SetBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleNameBlockList); });

        IBlockList EmptyIdentifierBlockList = (IBlockList)BlockListHelper.CreateEmptyBlockList<Identifier>();

        NeverEmptyException? Exception = Assert.Throws<NeverEmptyException>(() => { NodeTreeHelperBlockList.SetBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), EmptyIdentifierBlockList); });
        Assert.NotNull(Exception);
        Assert.AreEqual(Exception?.Node, SimpleExport);
        Assert.AreEqual(Exception?.PropertyName, nameof(Export.ClassIdentifierBlocks));

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        IBlockList NullBlockList = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetBlockList(NullExport, nameof(Export.ClassIdentifierBlocks), SimpleIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetBlockList(SimpleExport, NullString, SimpleIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), NullBlockList); });
#endif
    }

    [Test]
    public static void TestIsBlockListEmptySingle()
    {
        bool Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        IBlockList ClassIdentifierBlocks = (IBlockList)SimpleExport.ClassIdentifierBlocks;

        Result = NodeTreeHelperBlockList.IsBlockListEmpty(ClassIdentifierBlocks);
        Assert.False(Result);

        Result = NodeTreeHelperBlockList.IsBlockListSingle(ClassIdentifierBlocks);
        Assert.True(Result);

        Class SimpleClass = NodeHelper.CreateSimpleClass("b");
        IBlockList ExportBlocks = (IBlockList)SimpleClass.ExportBlocks;

        Result = NodeTreeHelperBlockList.IsBlockListSingle(ExportBlocks);
        Assert.False(Result);

#if !DEBUG
        IBlockList NullBlockList = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListEmpty(NullBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListSingle(NullBlockList); });
#endif
    }

    [Test]
    public static void TestCreateBlock()
    {
        IBlock Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("a");
        Pattern SimplePattern = NodeHelper.CreateSimplePattern("b");
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");

        Result = NodeTreeHelperBlockList.CreateBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), ReplicationStatus.Normal, SimplePattern, SimpleIdentifier);
        Assert.AreEqual(Type.FromGetType(Result), Type.FromTypeof<Block<Identifier>>());

        IBlockList ClassIdentifierBlocks = (IBlockList)SimpleExport.ClassIdentifierBlocks;

        Result = NodeTreeHelperBlockList.CreateBlock(ClassIdentifierBlocks, ReplicationStatus.Normal, SimplePattern, SimpleIdentifier);
        Assert.AreEqual(Type.FromGetType(Result), Type.FromTypeof<Block<Identifier>>());

        Result = NodeTreeHelperBlockList.CreateBlock(Type.FromTypeof<IBlockList<Identifier>>(), ReplicationStatus.Normal, SimplePattern, SimpleIdentifier);
        Assert.AreEqual(Type.FromGetType(Result), Type.FromTypeof<Block<Identifier>>());

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.CreateBlock(new TestBlockList(), ReplicationStatus.Normal, SimplePattern, SimpleIdentifier); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.CreateBlock(Type.FromTypeof<TestBlockList>(), ReplicationStatus.Normal, SimplePattern, SimpleIdentifier); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Pattern NullPattern = null!;
        Identifier NullIdentifier = null!;
        IBlockList NullBlockList = null!;
        Type NullType = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(NullExport, nameof(Export.ClassIdentifierBlocks), ReplicationStatus.Normal, SimplePattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(SimpleExport, NullString, ReplicationStatus.Normal, SimplePattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), ReplicationStatus.Normal, NullPattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), ReplicationStatus.Normal, SimplePattern, NullIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(NullBlockList, ReplicationStatus.Normal, SimplePattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(ClassIdentifierBlocks, ReplicationStatus.Normal, NullPattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(ClassIdentifierBlocks, ReplicationStatus.Normal, SimplePattern, NullIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(NullType, ReplicationStatus.Normal, SimplePattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(Type.FromTypeof<IBlockList<Identifier>>(), ReplicationStatus.Normal, NullPattern, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.CreateBlock(Type.FromTypeof<IBlockList<Identifier>>(), ReplicationStatus.Normal, SimplePattern, NullIdentifier); });
#endif
    }

    [Test]
    public static void TestClearChildBlockList()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("a");
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");

        Export FirstExport = NodeHelper.CreateSimpleExport("c");
        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { FirstExport });
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleBlock);

        NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, SimpleExport);

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 2);

        NodeTreeHelperBlockList.ClearChildBlockList(SimpleClass, nameof(Class.ExportBlocks));
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 0);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.ClearChildBlockList(SimpleClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.ClearChildBlockList(SimpleClass, nameof(Class.EntityName)); });

        NeverEmptyException? Exception = Assert.Throws<NeverEmptyException>(() => { NodeTreeHelperBlockList.ClearChildBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks)); });
        Assert.NotNull(Exception);
        Assert.AreEqual(Exception?.Node, SimpleExport);
        Assert.AreEqual(Exception?.PropertyName, nameof(Export.ClassIdentifierBlocks));

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ClearChildBlockList(NullClass, nameof(Class.ExportBlocks)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ClearChildBlockList(SimpleClass, NullString); });
#endif
    }

    [Test]
    public static void TestInsertIntoBlock()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("a");
        Export FirstExport = NodeHelper.CreateSimpleExport("b");
        Export SecondExport = NodeHelper.CreateSimpleExport("c");

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 0);

        Export SimpleExport = NodeHelper.CreateSimpleExport("c");
        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { SimpleExport });
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleBlock);

        NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 1, FirstExport);

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 2);

        IBlockList<Identifier> ClassIdentifierBlocks = FirstExport.ClassIdentifierBlocks;
        IBlock FirstBlock = (IBlock)ClassIdentifierBlocks.NodeBlockList[0];
        Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("d");
        Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("e");

        NodeTreeHelperBlockList.InsertIntoBlock(FirstBlock, 0, FirstIdentifier);
        NodeTreeHelperBlockList.InsertIntoBlock(FirstBlock, FirstBlock.NodeList.Count, SecondIdentifier);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Identifier.Text), 0, 0, FirstExport); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.EntityName), 0, 0, FirstExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), -1, 0, FirstExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), SimpleClass.ExportBlocks.NodeBlockList.Count, 0, FirstExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, -1, FirstExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count + 1, FirstExport); });

        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(FirstBlock, -1, FirstIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(FirstBlock, FirstBlock.NodeList.Count + 1, FirstIdentifier); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Export NullExport = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(NullClass, nameof(Class.ExportBlocks), 0, 0, FirstExport); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, NullString, 0, 0, FirstExport); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, NullExport); });

        IBlock NullBlock = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(NullBlock, 0, FirstIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlock(FirstBlock, 0, NullIdentifier); });
#endif
    }

    [Test]
    public static void TestRemoveFromBlock()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("a");
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        bool IsBlockRemoved;

        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { SimpleExport });
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleBlock);

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 1);

        NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, out IsBlockRemoved);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 0);
        Assert.True(IsBlockRemoved);

        SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { SimpleExport });
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleBlock);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 1);

        Export OtherExport = NodeHelper.CreateSimpleExport("b");
        NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, OtherExport);

        NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, out IsBlockRemoved);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.False(IsBlockRemoved);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Identifier.Text), 0, 0, out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.EntityName), 0, 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.ExportBlocks), -1, 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.ExportBlocks), SimpleClass.ExportBlocks.NodeBlockList.Count, 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.ExportBlocks), 0, -1, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, out _); });

        NeverEmptyException? Exception = Assert.Throws<NeverEmptyException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 0, out _); });
        Assert.NotNull(Exception);
        Assert.AreEqual(Exception?.Node, SimpleExport);
        Assert.AreEqual(Exception?.PropertyName, nameof(Export.ClassIdentifierBlocks));

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(NullClass, nameof(Class.ExportBlocks), 0, 0, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.RemoveFromBlock(SimpleClass, NullString, 0, 0, out _); });
#endif
    }

    [Test]
    public static void TestReplaceNode()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("a");
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        Export OtherExport = NodeHelper.CreateSimpleExport("c");

        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { SimpleExport });
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleBlock);

        NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.ExportBlocks), 0, 0, OtherExport);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList[0], OtherExport);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Identifier.Text), 0, 0, OtherExport); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.EntityName), 0, 0, OtherExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.ExportBlocks), -1, 0, OtherExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.ExportBlocks), SimpleClass.ExportBlocks.NodeBlockList.Count, 0, OtherExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.ExportBlocks), 0, -1, OtherExport); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, OtherExport); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Export NullExport = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ReplaceNode(NullClass, nameof(Class.ExportBlocks), 0, 0, OtherExport); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, NullString, 0, 0, OtherExport); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ReplaceNode(SimpleClass, nameof(Class.ExportBlocks), 0, 0, NullExport); });
#endif
    }

    [Test]
    public static void TestReplaceInBlock()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");

        IBlockList<Identifier> ClassIdentifierBlocks = SimpleExport.ClassIdentifierBlocks;
        IBlock FirstBlock = (IBlock)ClassIdentifierBlocks.NodeBlockList[0];
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");

        NodeTreeHelperBlockList.ReplaceInBlock(FirstBlock, 0, SimpleIdentifier);
        Assert.AreEqual(FirstBlock.NodeList.Count, 1);
        Assert.AreEqual(FirstBlock.NodeList[0], SimpleIdentifier);

        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.ReplaceInBlock(FirstBlock, -1, SimpleIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.ReplaceInBlock(FirstBlock, FirstBlock.NodeList.Count, SimpleIdentifier); });

#if !DEBUG
        IBlock NullBlock = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ReplaceInBlock(NullBlock, 0, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.ReplaceInBlock(FirstBlock, 0, NullIdentifier); });
#endif
    }

    [Test]
    public static void TestInsertIntoBlockList()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");

        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");
        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Identifier>() { SimpleIdentifier });

        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 1);
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, SimpleBlock);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 2);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList[SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count - 1], SimpleBlock);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Identifier.Text), 0, SimpleBlock); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Export.EntityName), 0, SimpleBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, SimpleBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count + 1, SimpleBlock); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        IBlock NullBlock = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(NullExport, nameof(Export.ClassIdentifierBlocks), 0, SimpleBlock); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, NullString, 0, SimpleBlock); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, NullBlock); });
#endif
    }

    [Test]
    public static void TestRemoveFromBlockList()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");

        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");
        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Identifier>() { SimpleIdentifier });

        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, SimpleBlock);

        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 2);
        NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 1);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, nameof(Identifier.Text), 0); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, nameof(Export.EntityName), 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count); });

        NeverEmptyException? Exception = Assert.Throws<NeverEmptyException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0); });
        Assert.NotNull(Exception);
        Assert.AreEqual(Exception?.Node, SimpleExport);
        Assert.AreEqual(Exception?.PropertyName, nameof(Export.ClassIdentifierBlocks));

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(NullExport, nameof(Export.ClassIdentifierBlocks), 0); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.RemoveFromBlockList(SimpleExport, NullString, 0); });
#endif
    }

    [Test]
    public static void TestSplitMerge()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("a");
        Export FirstExport = NodeHelper.CreateSimpleExport("b");
        Export SecondExport = NodeHelper.CreateSimpleExport("c");
        IBlock MergedBlock;

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 0);

        IBlock SimpleBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { FirstExport, SecondExport });
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleBlock);

        IBlock NewBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { FirstExport });
        NewBlock.NodeList.Clear();

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(NewBlock.NodeList.Count, 0);
        NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 1, NewBlock);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 2);
        Assert.AreEqual(NewBlock.NodeList.Count, 1);

        NodeTreeHelperBlockList.MergeBlocks(SimpleClass, nameof(Class.ExportBlocks), 1, out MergedBlock);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Identifier.Text), 0, 0, NewBlock); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.EntityName), 0, 0, NewBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), -1, 0, NewBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), SimpleClass.ExportBlocks.NodeBlockList.Count, 0, NewBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, NewBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, NewBlock); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MergeBlocks(SimpleClass, nameof(Class.ExportBlocks), 0, out _); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MergeBlocks(SimpleClass, nameof(Class.ExportBlocks), SimpleClass.ExportBlocks.NodeBlockList.Count, out _); });

        IBlock AnotherBlock = (IBlock)BlockListHelper.CreateBlock(new List<Export>() { FirstExport });
        AnotherBlock.NodeList.Clear();

        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 1);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 2);

        NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 1, AnotherBlock);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList.Count, 2);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 1);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, AnotherBlock); });

        NodeTreeHelperBlockList.InsertIntoBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, SecondExport);
        Assert.AreEqual(SimpleClass.ExportBlocks.NodeBlockList[0].NodeList.Count, 2);

        Assert.AreEqual(AnotherBlock.NodeList.Count, 2);
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 1, AnotherBlock); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        IBlock NullBlock = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SplitBlock(NullClass, nameof(Class.ExportBlocks), 0, 0, NewBlock); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, NullString, 0, 0, NewBlock); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SplitBlock(SimpleClass, nameof(Class.ExportBlocks), 0, 0, NullBlock); });
#endif
    }

    [Test]
    public static void TestMoveNode()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");

        NodeTreeHelperBlockList.InsertIntoBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 1, SimpleIdentifier);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 1);

        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];
        Assert.AreEqual(FirstBlock.NodeList.Count, 2);
        Assert.AreEqual(FirstBlock.NodeList[1], SimpleIdentifier);

        NodeTreeHelperBlockList.MoveNode(FirstBlock, 1, -1);
        Assert.AreEqual(FirstBlock.NodeList[0], SimpleIdentifier);

        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveNode(FirstBlock, -1, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveNode(FirstBlock, FirstBlock.NodeList.Count, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveNode(FirstBlock, 0, -1); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveNode(FirstBlock, FirstBlock.NodeList.Count - 1, +1); });

#if !DEBUG
        IBlock NullBlock = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.MoveNode(NullBlock , 0, 0); });
#endif
    }

    [Test]
    public static void TestMoveBlock()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("c");

        NodeTreeHelperBlockList.InsertIntoBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 1, SimpleIdentifier);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 1);

        IBlock NewBlock = (IBlock)BlockListHelper.CreateBlock(new List<Identifier>() { SimpleIdentifier });
        NewBlock.NodeList.Clear();

        NodeTreeHelperBlockList.SplitBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 1, NewBlock);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 2);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].NodeList.Count, 1);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList[1].NodeList.Count, 1);
        Assert.AreEqual(NewBlock.NodeList.Count, 1);
        Assert.AreNotEqual(NewBlock.NodeList[0], SimpleIdentifier);
        Assert.AreEqual(SimpleExport.ClassIdentifierBlocks.NodeBlockList[1].NodeList[0], SimpleIdentifier);

        NodeTreeHelperBlockList.MoveBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, 1);

        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];
        Assert.AreEqual(FirstBlock.NodeList.Count, 1);
        Assert.AreEqual(FirstBlock.NodeList[0], SimpleIdentifier);

        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, 0); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, -1); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.MoveBlock(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count - 1, +1); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.MoveBlock(NullExport, nameof(Export.ClassIdentifierBlocks), 0, 0); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.MoveBlock(SimpleExport, NullString, 0, 0); });
#endif
    }

    [Test]
    public static void TestIsBlockPatternNode()
    {
        bool Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        Pattern ReplicationPattern = SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].ReplicationPattern;

        Result = NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, ReplicationPattern);
        Assert.True(Result);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, nameof(Identifier.Text), 0, ReplicationPattern); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, nameof(Class.EntityName), 0, ReplicationPattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, ReplicationPattern); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, ReplicationPattern); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Pattern NullPattern = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(NullExport, nameof(Export.ClassIdentifierBlocks), 0, ReplicationPattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, NullString, 0, ReplicationPattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockPatternNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, NullPattern); });
#endif
    }

    [Test]
    public static void TestIsPatternNode()
    {
        bool Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];
        Pattern ReplicationPattern = FirstBlock.ReplicationPattern;

        Result = NodeTreeHelperBlockList.IsPatternNode(FirstBlock, ReplicationPattern);
        Assert.True(Result);

#if !DEBUG
        IBlock NullBlock = null!;
        Pattern NullPattern = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsPatternNode(NullBlock , ReplicationPattern); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsPatternNode(FirstBlock, NullPattern); });
#endif
    }

    [Test]
    public static void TestSetGetPattern()
    {
        string Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];

        Result = NodeTreeHelperBlockList.GetPattern(FirstBlock);
        Assert.AreEqual(Result, "*");

        NodeTreeHelperBlockList.SetPattern(FirstBlock, "a");

        Result = NodeTreeHelperBlockList.GetPattern(FirstBlock);
        Assert.AreEqual(Result, "a");

#if !DEBUG
        IBlock NullBlock = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetPattern(NullBlock); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetPattern(NullBlock, "a"); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetPattern(FirstBlock, NullString); });
#endif
    }

    [Test]
    public static void TestIsBlockSourceNode()
    {
        bool Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        Identifier SourceIdentifier = SimpleExport.ClassIdentifierBlocks.NodeBlockList[0].SourceIdentifier;

        Result = NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, SourceIdentifier);
        Assert.True(Result);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, nameof(Identifier.Text), 0, SourceIdentifier); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, nameof(Class.EntityName), 0, SourceIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), -1, SourceIdentifier); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), SimpleExport.ClassIdentifierBlocks.NodeBlockList.Count, SourceIdentifier); });

#if !DEBUG
        Export NullExport = null!;
        string NullString = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(NullExport, nameof(Export.ClassIdentifierBlocks), 0, SourceIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, NullString, 0, SourceIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockSourceNode(SimpleExport, nameof(Export.ClassIdentifierBlocks), 0, NullIdentifier); });
#endif
    }

    [Test]
    public static void TestIsSourceNode()
    {
        bool Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];
        Identifier SourceIdentifier = FirstBlock.SourceIdentifier;

        Result = NodeTreeHelperBlockList.IsSourceNode(FirstBlock, SourceIdentifier);
        Assert.True(Result);

#if !DEBUG
        IBlock NullBlock = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsSourceNode(NullBlock , SourceIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsSourceNode(FirstBlock, NullIdentifier); });
#endif
    }

    [Test]
    public static void TestSetGetSource()
    {
        string Result;

        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];

        Result = NodeTreeHelperBlockList.GetSource(FirstBlock);
        Assert.AreEqual(Result, string.Empty);

        NodeTreeHelperBlockList.SetSource(FirstBlock, "a");

        Result = NodeTreeHelperBlockList.GetSource(FirstBlock);
        Assert.AreEqual(Result, "a");

#if !DEBUG
        IBlock NullBlock = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetSource(NullBlock); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetSource(NullBlock, "a"); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetSource(FirstBlock, NullString); });
#endif
    }

    [Test]
    public static void TestSetReplication()
    {
        Export SimpleExport = NodeHelper.CreateSimpleExport("b");
        IBlock FirstBlock = (IBlock)SimpleExport.ClassIdentifierBlocks.NodeBlockList[0];

        Assert.AreEqual(FirstBlock.Replication, ReplicationStatus.Normal);
        NodeTreeHelperBlockList.SetReplication(FirstBlock, ReplicationStatus.Replicated);
        Assert.AreEqual(FirstBlock.Replication, ReplicationStatus.Replicated);

#if !DEBUG
        IBlock NullBlock = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.SetReplication(NullBlock, ReplicationStatus.Replicated); });
#endif
    }
}
