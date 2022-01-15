namespace TestEaslyLanguage;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BaseNode;
using BaseNodeHelper;
using Easly;
using NUnit.Framework;
using Range = BaseNode.Range;

[TestFixture]
public partial class CoverageSet
{
    [Test]
    public static void TestBlockInitializers()
    {
        IBlock<Node> TestBlock = BlockListHelper.CreateBlock(new List<Node>() { NodeHelper.CreateEmptyIdentifier() });
        IBlockList<Node> TestBlockList = BlockListHelper.CreateEmptyBlockList<Node>();

        IList<Node> NodeList = TestBlock.NodeList;
        IList NonGenericNodeList = ((IBlock)TestBlock).NodeList;

        IList<IBlock<Node>> BlockList = TestBlockList.NodeBlockList;
        IList NonGenericBlockList = ((IBlockList)TestBlockList).NodeBlockList;
        Document TestDocument = TestBlockList.Documentation;
        TestDocument = ((IBlockList)TestBlockList).Documentation;

        string Comment = TestDocument.Comment;
        Guid Uuid = TestDocument.Uuid;
    }

    [Test]
    public static void TestMiscNodeInitializers()
    {
        Discrete NewDiscrete1 = NodeHelper.CreateSimpleDiscrete(string.Empty);

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        Discrete NewDiscrete2 = NodeHelper.CreateDiscrete(string.Empty, DefaultExpression);

        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        IBlockList<Identifier> EmptyIdentifierBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
        ExportChange NewExportChange = NodeHelper.CreateExportChange(EmptyIdentifier, EmptyIdentifierBlockList);

        Generic NewGeneric1 = NodeHelper.CreateSimpleGeneric(string.Empty);

        Name SimpleName = NodeHelper.CreateSimpleName(string.Empty);
        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        IBlockList<Constraint> EmptyConstraintBlockList = BlockListHelper.CreateEmptyBlockList<Constraint>();
        Generic NewGeneric2 = NodeHelper.CreateGeneric(SimpleName, DefaultObjectType, EmptyConstraintBlockList);

        Rename NewRename = NodeHelper.CreateRename(string.Empty, string.Empty);

        Typedef NewTypedef = NodeHelper.CreateTypedef(SimpleName, DefaultObjectType);
    }
}
