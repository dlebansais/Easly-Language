namespace TestEaslyLanguage
{
    using System;
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
    }
}
