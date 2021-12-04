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
    public partial class SimplifyOtherCoverage
    {
        [Test]
        [Category("Simplify")]
        public static void TestQualifiedName()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            Result = NodeHelper.GetSimplifiedNode(SimpleQualifiedName, out _);
            Assert.False(Result);

            Identifier FirstIdentifier = NodeHelper.CreateEmptyIdentifier();
            Identifier SecondIdentifier = NodeHelper.CreateEmptyIdentifier();
            QualifiedName NotSimpleQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, SecondIdentifier });

            Result = NodeHelper.GetSimplifiedNode(NotSimpleQualifiedName, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QualifiedName);
        }

        [Test]
        [Category("Simplify")]
        public static void TestNode()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Identifier> SimpleParameterList = new() { EmptyIdentifier };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AssignmentArgument Argument = NodeHelper.CreateAssignmentArgument(SimpleParameterList, DefaultExpression);

            Result = NodeHelper.GetSimplifiedNode(Argument, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is PositionalArgument);

            QueryExpression Expression = NodeHelper.CreateSimpleQueryExpression(string.Empty);

            Result = NodeHelper.GetSimplifiedNode(Expression, out _);
            Assert.False(Result);

            CommandInstruction Instruction = NodeHelper.CreateSimpleCommandInstruction(string.Empty);

            Result = NodeHelper.GetSimplifiedNode(Instruction, out _);
            Assert.False(Result);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            AnchoredType ObjectType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            Result = NodeHelper.GetSimplifiedNode(ObjectType, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            AssignmentTypeArgument TypeArgument1 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);

            Result = NodeHelper.GetSimplifiedNode(TypeArgument1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is PositionalTypeArgument);

            Result = NodeHelper.GetSimplifiedNode(EmptyIdentifier, out _);
            Assert.False(Result);
        }
    }
}
