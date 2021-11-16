namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        [Category("Complexify")]
        public static void TestComplexifyExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QueryExpression Expression1 = NodeHelper.CreateSimpleQueryExpression(String.Empty);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out ComplexifiedNodeList);
            Assert.False(Result);

            QueryExpression Expression2 = NodeHelper.CreateSimpleQueryExpression("a.b");

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is QueryExpression);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            QueryExpression Expression3 = NodeHelper.CreateQueryExpression(SimpleQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("a,b")});

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is QueryExpression);

            QueryExpression Complexified3 = (QueryExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified3.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified3.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 2);

            QueryExpression Expression4 = NodeHelper.CreateQueryExpression(NodeHelper.CreateSimpleQualifiedName("a(b,c)"), new List<Argument>());

            Result = NodeHelper.GetComplexifiedNode(Expression4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 2);
            Assert.That(ComplexifiedNodeList[0] is QueryExpression);
            Assert.That(ComplexifiedNodeList[1] is QueryExpression);

            QueryExpression Complexified4_0 = (QueryExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified4_0.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified4_0.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 1);

            QueryExpression Complexified4_1 = (QueryExpression)ComplexifiedNodeList[1];
            Assert.That(Complexified4_1.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified4_1.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 2);

            QueryExpression Expression5 = NodeHelper.CreateSimpleQueryExpression("entity a");

            Result = NodeHelper.GetComplexifiedNode(Expression5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is EntityExpression);

            QueryExpression Expression6 = NodeHelper.CreateSimpleQueryExpression("0");

            Result = NodeHelper.GetComplexifiedNode(Expression6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ManifestNumberExpression);

            QueryExpression Expression7 = NodeHelper.CreateSimpleQueryExpression("agent a");

            Result = NodeHelper.GetComplexifiedNode(Expression7, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AgentExpression);

            QueryExpression Expression8 = NodeHelper.CreateSimpleQueryExpression("tag a");

            Result = NodeHelper.GetComplexifiedNode(Expression8, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssertionTagExpression);

            QueryExpression Expression9 = NodeHelper.CreateSimpleQueryExpression("a and b");

            Result = NodeHelper.GetComplexifiedNode(Expression9, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified9 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified9.Conditional == ConditionalTypes.And);

            QueryExpression Expression10 = NodeHelper.CreateSimpleQueryExpression("a or b");

            Result = NodeHelper.GetComplexifiedNode(Expression10, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified10 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified10.Conditional == ConditionalTypes.Or);

            QueryExpression Expression11 = NodeHelper.CreateSimpleQueryExpression("a xor b");

            Result = NodeHelper.GetComplexifiedNode(Expression11, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified11 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified11.Conditional == ConditionalTypes.Xor);

            QueryExpression Expression12 = NodeHelper.CreateSimpleQueryExpression("a => b");

            Result = NodeHelper.GetComplexifiedNode(Expression12, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified12 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified12.Conditional == ConditionalTypes.Implies);

            QueryExpression Expression13 = NodeHelper.CreateSimpleQueryExpression("a ⇒ b");

            Result = NodeHelper.GetComplexifiedNode(Expression13, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified13 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified13.Conditional == ConditionalTypes.Implies);

            QueryExpression Expression14 = NodeHelper.CreateSimpleQueryExpression("a + b");

            Result = NodeHelper.GetComplexifiedNode(Expression14, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryOperatorExpression);

            BinaryOperatorExpression Complexified14 = (BinaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified14.Operator.Text, "+");

            QueryExpression Expression15 = NodeHelper.CreateSimpleQueryExpression("{a}");

            Result = NodeHelper.GetComplexifiedNode(Expression15, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ClassConstantExpression);

            QueryExpression Expression16 = NodeHelper.CreateSimpleQueryExpression("clone of a");

            Result = NodeHelper.GetComplexifiedNode(Expression16, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CloneOfExpression);

            QueryExpression Expression17 = NodeHelper.CreateSimpleQueryExpression("a = b");

            Result = NodeHelper.GetComplexifiedNode(Expression17, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is EqualityExpression);

            EqualityExpression Complexified17 = (EqualityExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified17.Comparison, ComparisonType.Equal);

            QueryExpression Expression18 = NodeHelper.CreateSimpleQueryExpression("a /= b");

            Result = NodeHelper.GetComplexifiedNode(Expression18, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is EqualityExpression);

            EqualityExpression Complexified18 = (EqualityExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified18.Comparison, ComparisonType.Different);

            QueryExpression Expression19 = NodeHelper.CreateQueryExpression(NodeHelper.CreateSimpleQualifiedName("a[b,c]"), new List<Argument>());

            Result = NodeHelper.GetComplexifiedNode(Expression19, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 2);
            Assert.That(ComplexifiedNodeList[0] is IndexQueryExpression);
            Assert.That(ComplexifiedNodeList[1] is IndexQueryExpression);

            IndexQueryExpression Complexified19_0 = (IndexQueryExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified19_0.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified19_0.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 1);

            IndexQueryExpression Complexified19_1 = (IndexQueryExpression)ComplexifiedNodeList[1];
            Assert.That(Complexified19_1.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified19_1.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 2);

            QueryExpression Expression20 = NodeHelper.CreateSimpleQueryExpression("a{b:=c}");

            Result = NodeHelper.GetComplexifiedNode(Expression20, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is InitializedObjectExpression);

            Assert.False(NodeHelper.GetComplexifiedNode(NodeHelper.CreateSimpleQueryExpression("a{b"), out _));
            Assert.False(NodeHelper.GetComplexifiedNode(NodeHelper.CreateSimpleQueryExpression("a{b:="), out _));

            QueryExpression Expression21 = NodeHelper.CreateSimpleQueryExpression("True");

            Result = NodeHelper.GetComplexifiedNode(Expression21, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is KeywordExpression);

            KeywordExpression Complexified21 = (KeywordExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified21.Value, Keyword.True);

            QueryExpression Expression22 = NodeHelper.CreateSimpleQueryExpression("'a'");

            Result = NodeHelper.GetComplexifiedNode(Expression22, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ManifestCharacterExpression);

            ManifestCharacterExpression Complexified22 = (ManifestCharacterExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified22.Text, "a");

            QueryExpression Expression23 = NodeHelper.CreateSimpleQueryExpression("\"a\"");

            Result = NodeHelper.GetComplexifiedNode(Expression23, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ManifestStringExpression);

            ManifestStringExpression Complexified23 = (ManifestStringExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified23.Text, "a");

            QueryExpression Expression24 = NodeHelper.CreateSimpleQueryExpression("new a");

            Result = NodeHelper.GetComplexifiedNode(Expression24, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is NewExpression);

            QueryExpression Expression25 = NodeHelper.CreateSimpleQueryExpression("entity Current");

            Result = NodeHelper.GetComplexifiedNode(Expression25, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 2);
            Assert.That(ComplexifiedNodeList[0] is EntityExpression);
            Assert.That(ComplexifiedNodeList[1] is KeywordEntityExpression);

            KeywordEntityExpression Complexified25 = (KeywordEntityExpression)ComplexifiedNodeList[1];
            Assert.AreEqual(Complexified25.Value, Keyword.Current);

            QueryExpression Expression26 = NodeHelper.CreateSimpleQueryExpression("old a");

            Result = NodeHelper.GetComplexifiedNode(Expression26, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is OldExpression);

            QueryExpression Expression27 = NodeHelper.CreateSimpleQueryExpression("precursor");

            Result = NodeHelper.GetComplexifiedNode(Expression27, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorExpression);

            QueryExpression Expression28 = NodeHelper.CreateQueryExpression(NodeHelper.CreateSimpleQualifiedName("precursor[]"), new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("a") });

            Result = NodeHelper.GetComplexifiedNode(Expression28, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexExpression);

            QueryExpression Expression29 = NodeHelper.CreateSimpleQueryExpression("DateAndTime");

            Result = NodeHelper.GetComplexifiedNode(Expression29, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PreprocessorExpression);

            QueryExpression Expression30 = NodeHelper.CreateSimpleQueryExpression("result of a");

            Result = NodeHelper.GetComplexifiedNode(Expression30, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ResultOfExpression);

            QueryExpression Expression31 = NodeHelper.CreateSimpleQueryExpression("not a");

            Result = NodeHelper.GetComplexifiedNode(Expression31, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is UnaryNotExpression);

            QueryExpression Expression32 = NodeHelper.CreateSimpleQueryExpression("-a");

            //System.Diagnostics.Debugger.Launch();
            Result = NodeHelper.GetComplexifiedNode(Expression32, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is UnaryOperatorExpression);
        }
    }
}
