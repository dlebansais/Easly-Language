﻿namespace TestEaslyLanguage;

using System.Collections.Generic;
using BaseNode;
using BaseNodeHelper;
using NotNullReflection;
using NUnit.Framework;

[TestFixture]
public partial class ToolsCoverage
{
    [Test]
    public static void TestGetNodeType()
    {
        bool Result;

        Result = NodeHelper.GetNodeType("Identifier", out _);
        Assert.True(Result);

        Result = NodeHelper.GetNodeType("Foo", out _);
        Assert.False(Result);
    }

    [Test]
    public static void TestCreateNodeDictionary()
    {
        IList<Type> Table = NodeHelper.GetNodeKeys();
        Assert.That(Table.Count > 0);
    }

    [Test]
    public static void TestIsCollectionNeverEmpty()
    {
        bool Result;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        Name EmptyName = NodeHelper.CreateEmptyName();

        OverLoopInstruction Instruction = NodeHelper.CreateOverLoopInstruction(DefaultExpression, new List<Name>() { EmptyName });

        Result = NodeHelper.IsCollectionNeverEmpty(Instruction, nameof(OverLoopInstruction.IndexerBlocks));
        Assert.True(Result);

        Result = NodeHelper.IsCollectionNeverEmpty(Instruction, nameof(OverLoopInstruction.InvariantBlocks));
        Assert.False(Result);
    }

    [Test]
    public static void TestIsCollectionWithExpand()
    {
        bool Result;

        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        Name EmptyName = NodeHelper.CreateEmptyName();

        OverLoopInstruction Instruction = NodeHelper.CreateOverLoopInstruction(DefaultExpression, new List<Name>() { EmptyName });

        Result = NodeHelper.IsCollectionWithExpand(Instruction, nameof(OverLoopInstruction.InvariantBlocks));
        Assert.False(Result);

        Result = NodeHelper.IsCollectionWithExpand(DefaultExpression, nameof(QueryExpression.ArgumentBlocks));
        Assert.True(Result);
    }
}
