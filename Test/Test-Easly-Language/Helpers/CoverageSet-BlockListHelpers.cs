namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        public static void TestBlockListHelpers()
        {
            IBlockList<Identifier> EmptyBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
            
            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            IBlockList<Identifier> SimpleBlockList = BlockListHelper.CreateSimpleBlockList<Identifier>(EmptyIdentifier);

            List<Identifier> IdentifierList = new() { EmptyIdentifier };
            IBlockList<Identifier> WithListBlockList = BlockListHelper.CreateBlockList<Identifier>(IdentifierList);

            IBlock<Identifier> IdentifierBlock = BlockListHelper.CreateBlock<Identifier>(IdentifierList);

            Pattern ReplicationPattern = NodeHelper.CreateEmptyPattern();
            ReplicationPattern = NodeHelper.CreateSimplePattern(string.Empty);
            Identifier SourceIdentifier = NodeHelper.CreateEmptyIdentifier();
            SourceIdentifier = NodeHelper.CreateSimpleIdentifier(string.Empty);
            IdentifierBlock = BlockListHelper.CreateBlock<Identifier>(IdentifierList, ReplicationStatus.Normal, ReplicationPattern, SourceIdentifier);

            List<IBlock<Identifier>> IdentifierBlockList = new() { IdentifierBlock };
            IBlockList<Identifier> WithBlocksBlockList = BlockListHelper.CreateBlockList<Identifier>(IdentifierBlockList);

            IBlockList<Identifier> BlockListCopy = BlockListHelper.CreateBlockListCopy<Identifier>(WithBlocksBlockList);
            bool IsSimple = BlockListHelper.IsSimple(BlockListCopy);

            Assert.Throws<ArgumentException>(() => { BlockListHelper.CreateBlock<Identifier>(new List<Identifier>()); });
            Assert.Throws<ArgumentException>(() => { BlockListHelper.CreateBlock<Identifier>(new List<Identifier>(), ReplicationStatus.Normal, ReplicationPattern, SourceIdentifier); });
        }

        [Test]
        public static void TestOptionalReferenceHelpers()
        {
            IOptionalReference<Identifier> EmptyEmptyReference = OptionalReferenceHelper.CreateEmptyReference<Identifier>();

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            IOptionalReference<Identifier> SimpleOptionalReference = OptionalReferenceHelper.CreateReference<Identifier>(EmptyIdentifier);

            IOptionalReference<Identifier> OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy<Identifier>(SimpleOptionalReference);
        }
    }
}
