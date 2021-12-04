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
    public partial class ComplexifyExpressionCoverage
    {
        [Test]
        [Category("Complexify")]
        public static void TestQueryExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QueryExpression Expression1 = NodeHelper.CreateSimpleQueryExpression(string.Empty);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
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

            Assert.False(NodeHelper.GetComplexifiedNode(NodeHelper.CreateSimpleQueryExpression("{}"), out _));

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

            Result = NodeHelper.GetComplexifiedNode(Expression32, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is UnaryOperatorExpression);

            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier MiddleIdentifier1 = NodeHelper.CreateSimpleIdentifier("b[c");
            Identifier MiddleIdentifier2 = NodeHelper.CreateSimpleIdentifier("d");
            Identifier LastIdentifier = NodeHelper.CreateSimpleIdentifier("e]");
            QualifiedName Path = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, MiddleIdentifier1, MiddleIdentifier2, LastIdentifier });
            QueryExpression Expression33 = NodeHelper.CreateQueryExpression(Path, new List<Argument>());

            Result = NodeHelper.GetComplexifiedNode(Expression33, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexQueryExpression);

            QueryExpression Expression34 = NodeHelper.CreateQueryExpression(NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, MiddleIdentifier1 }), new List<Argument>());

            //System.Diagnostics.Debugger.Launch();
            Result = NodeHelper.GetComplexifiedNode(Expression34, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Complexify")]
        public static void TestAgentExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            AgentExpression Expression1 = NodeHelper.CreateAgentExpression(SimpleIdentifier, DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like b");
            AgentExpression Expression2 = NodeHelper.CreateAgentExpression(SimpleIdentifier, AnchorType);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AgentExpression);

            AgentExpression Complexified2 = (AgentExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.BaseType.IsAssigned);
            Assert.That(Complexified2.BaseType.Item is AnchoredType);

            Identifier TypeIdentifier = NodeHelper.CreateSimpleIdentifier("{b}a");
            AgentExpression Expression3 = NodeHelper.CreateAgentExpression(TypeIdentifier, DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AgentExpression);

            AgentExpression Complexified3 = (AgentExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified3.BaseType.IsAssigned);
            Assert.That(Complexified3.BaseType.Item is SimpleType);

            Assert.False(NodeHelper.GetComplexifiedNode(NodeHelper.CreateAgentExpression(NodeHelper.CreateSimpleIdentifier("{b"), DefaultObjectType), out _));
        }

        [Test]
        [Category("Complexify")]
        public static void TestBinaryConditionalExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            BinaryConditionalExpression Expression1 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.And, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            BinaryConditionalExpression Expression2 = NodeHelper.CreateBinaryConditionalExpression(NumberExpression, ConditionalTypes.And, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified2 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified2.Conditional, ConditionalTypes.And);
            Assert.That(Complexified2.LeftExpression is ManifestNumberExpression);

            BinaryConditionalExpression Expression3 = NodeHelper.CreateBinaryConditionalExpression(RightExpression, ConditionalTypes.Or, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryConditionalExpression);

            BinaryConditionalExpression Complexified3 = (BinaryConditionalExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified3.Conditional, ConditionalTypes.Or);
            Assert.That(Complexified3.RightExpression is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestBinaryOperatorExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;
            string InverseRenamedSymbol;

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("+");

            BinaryOperatorExpression Expression1 = NodeHelper.CreateBinaryOperatorExpression(LeftExpression, SimpleIdentifier, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            BinaryOperatorExpression Expression2 = NodeHelper.CreateBinaryOperatorExpression(NumberExpression, SimpleIdentifier, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryOperatorExpression);

            BinaryOperatorExpression Complexified2 = (BinaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.LeftExpression is ManifestNumberExpression);

            BinaryOperatorExpression Expression3 = NodeHelper.CreateBinaryOperatorExpression(RightExpression, SimpleIdentifier, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryOperatorExpression);

            BinaryOperatorExpression Complexified3 = (BinaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified3.RightExpression is ManifestNumberExpression);

            Identifier GreaterEqualSymbolIdentifier = NodeHelper.CreateSimpleIdentifier(">=");

            BinaryOperatorExpression Expression4 = NodeHelper.CreateBinaryOperatorExpression(RightExpression, GreaterEqualSymbolIdentifier, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryOperatorExpression);

            BinaryOperatorExpression Complexified4 = (BinaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified4.LeftExpression is QueryExpression);
            Assert.That(Complexified4.RightExpression is QueryExpression);
            Assert.AreEqual(Complexified4.Operator.Text, "≥");

            Result = NodeHelper.GetInverseRenamedBinarySymbol(Complexified4.Operator.Text, out InverseRenamedSymbol);
            Assert.True(Result);
            Assert.AreEqual(InverseRenamedSymbol, ">=");

            Identifier LesserEqualSymbolIdentifier = NodeHelper.CreateSimpleIdentifier("<=");

            BinaryOperatorExpression Expression5 = NodeHelper.CreateBinaryOperatorExpression(RightExpression, LesserEqualSymbolIdentifier, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryOperatorExpression);

            BinaryOperatorExpression Complexified5 = (BinaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified5.LeftExpression is QueryExpression);
            Assert.That(Complexified5.RightExpression is QueryExpression);
            Assert.AreEqual(Complexified5.Operator.Text, "≤");

            Result = NodeHelper.GetInverseRenamedBinarySymbol(Complexified5.Operator.Text, out InverseRenamedSymbol);
            Assert.True(Result);
            Assert.AreEqual(InverseRenamedSymbol, "<=");

            Identifier ImplySymbolIdentifier = NodeHelper.CreateSimpleIdentifier("=>");

            BinaryOperatorExpression Expression6 = NodeHelper.CreateBinaryOperatorExpression(RightExpression, ImplySymbolIdentifier, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is BinaryOperatorExpression);

            BinaryOperatorExpression Complexified6 = (BinaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified6.LeftExpression is QueryExpression);
            Assert.That(Complexified6.RightExpression is QueryExpression);
            Assert.AreEqual(Complexified6.Operator.Text, "⇒");

            Result = NodeHelper.GetInverseRenamedBinarySymbol(Complexified6.Operator.Text, out InverseRenamedSymbol);
            Assert.True(Result);
            Assert.AreEqual(InverseRenamedSymbol, "=>");

            Result = NodeHelper.GetInverseRenamedBinarySymbol("*", out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Complexify")]
        public static void TestCloneOfExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            CloneOfExpression Expression1 = NodeHelper.CreateCloneOfExpression(CloneType.Deep, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            CloneOfExpression Expression2 = NodeHelper.CreateCloneOfExpression(CloneType.Shallow, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CloneOfExpression);

            CloneOfExpression Complexified2 = (CloneOfExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified2.Type, CloneType.Shallow);
            Assert.That(Complexified2.Source is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestEntityExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            EntityExpression Expression1 = NodeHelper.CreateEntityExpression(SimpleQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            QualifiedName OtherQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            EntityExpression Expression2 = NodeHelper.CreateEntityExpression(OtherQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is EntityExpression);

            EntityExpression Complexified2 = (EntityExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.Query.Path.Count == 2);
        }

        [Test]
        [Category("Complexify")]
        public static void TestEqualityExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            EqualityExpression Expression1 = NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Equal, EqualityType.Physical , RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            EqualityExpression Expression2 = NodeHelper.CreateEqualityExpression(NumberExpression, ComparisonType.Equal, EqualityType.Physical, RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is EqualityExpression);

            EqualityExpression Complexified2 = (EqualityExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified2.Comparison, ComparisonType.Equal);
            Assert.AreEqual(Complexified2.Equality, EqualityType.Physical);
            Assert.That(Complexified2.LeftExpression is ManifestNumberExpression);

            EqualityExpression Expression3 = NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Different, EqualityType.Deep, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is EqualityExpression);

            EqualityExpression Complexified3 = (EqualityExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified3.Comparison, ComparisonType.Different);
            Assert.AreEqual(Complexified3.Equality, EqualityType.Deep);
            Assert.That(Complexified3.RightExpression is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestIndexQueryExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            List<Argument> DefaultArgumentList = new() { DefaultArgument };

            IndexQueryExpression Expression1 = NodeHelper.CreateIndexQueryExpression(DefaultExpression, DefaultArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            IndexQueryExpression Expression2 = NodeHelper.CreateIndexQueryExpression(NumberExpression, DefaultArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexQueryExpression);

            IndexQueryExpression Complexified2 = (IndexQueryExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.IndexedExpression is ManifestNumberExpression);

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            IBlockList<Identifier> SimpleIdentifierBlockList = BlockListHelper.CreateSimpleBlockList(SimpleIdentifier);
            Argument SplittableArgument = NodeHelper.CreateAssignmentArgument(SimpleIdentifierBlockList, NumberExpression);
            IBlockList<Argument> SplittableArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SplittableArgument);

            IndexQueryExpression Expression3 = NodeHelper.CreateIndexQueryExpression(DefaultExpression, SplittableArgumentBlockList);

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexQueryExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestInitializedObjectExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("+");
            List<AssignmentArgument> EmptyAssignmentArgumentList = new();

            InitializedObjectExpression Expression1 = NodeHelper.CreateInitializedObjectExpression(SimpleIdentifier, EmptyAssignmentArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            AssignmentArgument SimpleAssignmentArgument = NodeHelper.CreateSimpleAssignmentArgument("a", "b");
            List<AssignmentArgument> SimpleAssignmentArgumentList = new() { SimpleAssignmentArgument };

            InitializedObjectExpression Expression2 = NodeHelper.CreateInitializedObjectExpression(SimpleIdentifier, SimpleAssignmentArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");
            AssignmentArgument NumberAssignmentArgument = NodeHelper.CreateAssignmentArgument(new List<Identifier>() { SimpleIdentifier }, NumberExpression);

            InitializedObjectExpression Expression3 = NodeHelper.CreateInitializedObjectExpression(SimpleIdentifier, new List<AssignmentArgument>() { NumberAssignmentArgument });

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is InitializedObjectExpression);

            InitializedObjectExpression Complexified3 = (InitializedObjectExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified3.AssignmentBlocks.NodeBlockList.Count == 1 && Complexified3.AssignmentBlocks.NodeBlockList[0].NodeList.Count == 1);

            AssignmentArgument ComplexifiedArgument3 = Complexified3.AssignmentBlocks.NodeBlockList[0].NodeList[0];
            Assert.That(ComplexifiedArgument3.Source is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestNewExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            NewExpression Expression1 = NodeHelper.CreateNewExpression(SimpleQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            QualifiedName OtherQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            NewExpression Expression2 = NodeHelper.CreateNewExpression(OtherQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is NewExpression);

            NewExpression Complexified2 = (NewExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.Object.Path.Count == 2);
        }

        [Test]
        [Category("Complexify")]
        public static void TestOldExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            OldExpression Expression1 = NodeHelper.CreateOldExpression(SimpleQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            QualifiedName OtherQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            OldExpression Expression2 = NodeHelper.CreateOldExpression(OtherQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is OldExpression);

            OldExpression Complexified2 = (OldExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.Query.Path.Count == 2);
        }

        [Test]
        [Category("Complexify")]
        public static void TestPrecursorExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PrecursorExpression Expression1 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            PrecursorExpression Expression2 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, AnchorType);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorExpression);

            PrecursorExpression Complexified2 = (PrecursorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.AncestorType.IsAssigned);
            Assert.That(Complexified2.AncestorType.Item is AnchoredType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestPrecursorIndexExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Argument SimpleArgument = NodeHelper.CreateSimplePositionalArgument("a");
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SimpleArgument);
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PrecursorIndexExpression Expression1 = NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentBlockList, DefaultObjectType);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like b");

            PrecursorIndexExpression Expression2 = NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentBlockList, AnchorType);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexExpression);

            PrecursorIndexExpression Complexified2 = (PrecursorIndexExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.AncestorType.IsAssigned);
            Assert.That(Complexified2.AncestorType.Item is AnchoredType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestResultOfExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            ResultOfExpression Expression1 = NodeHelper.CreateResultOfExpression(DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            ResultOfExpression Expression2 = NodeHelper.CreateResultOfExpression(NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ResultOfExpression);

            ResultOfExpression Complexified2 = (ResultOfExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.Source is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestUnaryNotExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            UnaryNotExpression Expression1 = NodeHelper.CreateUnaryNotExpression(RightExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            UnaryNotExpression Expression2 = NodeHelper.CreateUnaryNotExpression(NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is UnaryNotExpression);

            UnaryNotExpression Complexified2 = (UnaryNotExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.RightExpression is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestUnaryOperatorExpression()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;
            string InverseRenamedSymbol;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("-");
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            UnaryOperatorExpression Expression1 = NodeHelper.CreateUnaryOperatorExpression(SimpleIdentifier, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            UnaryOperatorExpression Expression2 = NodeHelper.CreateUnaryOperatorExpression(SimpleIdentifier, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is UnaryOperatorExpression);

            UnaryOperatorExpression Complexified2 = (UnaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.That(Complexified2.RightExpression is ManifestNumberExpression);

            Identifier SymbolIdentifier = NodeHelper.CreateSimpleIdentifier("sqrt");

            UnaryOperatorExpression Expression3 = NodeHelper.CreateUnaryOperatorExpression(SymbolIdentifier, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Expression3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is UnaryOperatorExpression);

            UnaryOperatorExpression Complexified3 = (UnaryOperatorExpression)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified3.Operator.Text, "√");

            Result = NodeHelper.GetInverseRenamedUnarySymbol(Complexified3.Operator.Text, out InverseRenamedSymbol);
            Assert.True(Result);
            Assert.AreEqual(InverseRenamedSymbol, "sqrt");

            Result = NodeHelper.GetInverseRenamedUnarySymbol("*", out _);
            Assert.False(Result);
        }
    }
}
