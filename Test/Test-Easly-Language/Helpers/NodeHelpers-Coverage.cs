namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeHelperCoverage
    {
        [Test]
        public static void TestBuilding()
        {
            Document Documentation = NodeHelper.CreateEmptyDocumentation();
            Documentation = NodeHelper.CreateSimpleDocumentation(string.Empty, Guid.Empty);

            Pattern Pattern = NodeHelper.CreateEmptyPattern();
            Pattern = NodeHelper.CreateSimplePattern(string.Empty);

            Identifier Identifier = NodeHelper.CreateEmptyIdentifier();
            Identifier = NodeHelper.CreateSimpleIdentifier(string.Empty);

            Name Name = NodeHelper.CreateEmptyName();
            Name = NodeHelper.CreateSimpleName(string.Empty);

            QualifiedName QualifiedName = NodeHelper.CreateEmptyQualifiedName();
            QualifiedName = NodeHelper.CreateSimpleQualifiedName(string.Empty);
            QualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier> { Identifier });

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateQualifiedName(new List<Identifier>()); });

            Expression Expression = NodeHelper.CreateEmptyQueryExpression();
            Expression = NodeHelper.CreateSimpleQueryExpression(string.Empty);

            Instruction Instruction = NodeHelper.CreateEmptyCommandInstruction();
            Instruction = NodeHelper.CreateSimpleCommandInstruction(string.Empty);

            PositionalArgument PositionalArgument = NodeHelper.CreateEmptyPositionalArgument();
            PositionalArgument = NodeHelper.CreateSimplePositionalArgument(string.Empty);

            AssignmentArgument AssignmentArgument = NodeHelper.CreateEmptyAssignmentArgument();
            AssignmentArgument = NodeHelper.CreateSimpleAssignmentArgument(string.Empty, string.Empty);

            PositionalTypeArgument PositionalTypeArgument = NodeHelper.CreateEmptyPositionalTypeArgument();
            PositionalTypeArgument = NodeHelper.CreateSimplePositionalTypeArgument(string.Empty);

            SimpleType SimpleType = NodeHelper.CreateEmptySimpleType();
            SimpleType = NodeHelper.CreateSimpleSimpleType(string.Empty);

            Scope Scope = NodeHelper.CreateEmptyScope();
            Scope = NodeHelper.CreateSimpleScope(Instruction);

            Conditional Conditional = NodeHelper.CreateEmptyConditional();

            QueryOverload QueryOverload = NodeHelper.CreateEmptyQueryOverload();

            CommandOverload CommandOverload = NodeHelper.CreateEmptyCommandOverload();

            QueryOverloadType QueryOverloadType = NodeHelper.CreateEmptyQueryOverloadType(SimpleType);

            CommandOverloadType CommandOverloadType = NodeHelper.CreateEmptyCommandOverloadType();

            EntityDeclaration EntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();
        }

        [Test]
        public static void TestDefault()
        {
            Argument Argument = NodeHelper.CreateDefaultArgument();
            TypeArgument TypeArgument = NodeHelper.CreateDefaultTypeArgument();
            Body Body = NodeHelper.CreateDefaultBody();
            Expression Expression = NodeHelper.CreateDefaultExpression();
            Instruction Instruction = NodeHelper.CreateDefaultInstruction();
            Feature Feature = NodeHelper.CreateDefaultFeature();
            ObjectType ObjectType = NodeHelper.CreateDefaultObjectType();

            Node Default = NodeHelper.CreateDefault(typeof(PositionalArgument));
            Default = NodeHelper.CreateDefaultFromType(typeof(PositionalArgument));

            Type DefaultType = NodeHelper.GetDefaultItemType(typeof(Argument));
        }
    }
}
