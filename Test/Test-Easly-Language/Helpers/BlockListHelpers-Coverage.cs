namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using Easly;
using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public partial class BlockListHelpersCoverage
{
    [Test]
    public static void Test()
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

        bool IsSimple;

        IsSimple = BlockListHelper.IsSimple(BlockListCopy);
        Assert.That(IsSimple);

        IdentifierList.Add(NodeHelper.CreateEmptyIdentifier());
        IsSimple = BlockListHelper.IsSimple(WithBlocksBlockList);
        Assert.That(!IsSimple);

        IsSimple = BlockListHelper.IsSimple(EmptyBlockList);
        Assert.That(!IsSimple);

        Assert.Throws<ArgumentException>(() => { BlockListHelper.CreateBlock<Identifier>(new List<Identifier>()); });
        Assert.Throws<ArgumentException>(() => { BlockListHelper.CreateBlock<Identifier>(new List<Identifier>(), ReplicationStatus.Normal, ReplicationPattern, SourceIdentifier); });
    }
}
