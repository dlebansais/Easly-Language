namespace TestEaslyLanguage
{
    using System;
    using System.Collections.Generic;
    using BaseNode;
    using BaseNodeHelper;
    using NUnit.Framework;

    [TestFixture]
    public partial class NodeTreeHelperBlockListCoverage
    {
        [Test]
        public static void TestIsBlockListProperty()
        {
            bool Result;

            Export SimpleExport = NodeHelper.CreateSimpleExport("a");
            Type ChildNodeType;

            Result = NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, nameof(Export.ClassIdentifierBlocks), out ChildNodeType);
            Assert.True(Result);
            Assert.AreEqual(ChildNodeType, typeof(Identifier));

            Result = NodeTreeHelperBlockList.IsBlockListProperty(typeof(Export), nameof(Export.ClassIdentifierBlocks), out ChildNodeType);
            Assert.True(Result);
            Assert.AreEqual(ChildNodeType, typeof(Identifier));

            Result = NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, nameof(Identifier.Text), out ChildNodeType);
            Assert.False(Result);

            Result = NodeTreeHelperBlockList.IsBlockListProperty(SimpleExport, nameof(Export.EntityName), out ChildNodeType);
            Assert.False(Result);

#if !DEBUG
            Export NullExport = null!;
            string NullString = null!;
            Type NullType = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(NullExport, nameof(Export.ClassIdentifierBlocks), out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(SimpleClass, NullString, out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(NullType, nameof(Export.ClassIdentifierBlocks), out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.IsBlockListProperty(typeof(Export), NullString, out _); });
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
            Assert.AreEqual(ChildNodeType, typeof(Identifier));

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
            Assert.AreEqual(Result, typeof(Identifier));

            Result = NodeTreeHelperBlockList.BlockListItemType(typeof(Export), nameof(Export.ClassIdentifierBlocks));
            Assert.AreEqual(Result, typeof(Identifier));

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
            Assert.AreEqual(Result, typeof(IBlock<Identifier>));

            Result = NodeTreeHelperBlockList.BlockListBlockType(typeof(Export), nameof(Export.ClassIdentifierBlocks));
            Assert.AreEqual(Result, typeof(IBlock<Identifier>));

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
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlock(NullExport, nameof(Export.ClassIdentifierBlocks), 0, _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlock(SimpleExport, NullString, 0, _); });
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
    }
}
