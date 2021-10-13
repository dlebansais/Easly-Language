namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        public static void TestDefaultObjects()
        {
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            Body DefaultBody = NodeHelper.CreateDefaultBody();
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();
            Feature DefaultFeature = NodeHelper.CreateDefaultFeature();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        }

        [Test]
        public static void TestDefaultNode()
        {
            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateDefault(typeof(CoverageSet)); });

            Node DefaultBody = NodeHelper.CreateDefault(typeof(Body));
            Node DefaultExpression = NodeHelper.CreateDefault(typeof(Expression));
            Node DefaultInstruction = NodeHelper.CreateDefault(typeof(Instruction));
            Node DefaultFeature = NodeHelper.CreateDefault(typeof(Feature));
            Node DefaultObjectType = NodeHelper.CreateDefault(typeof(ObjectType));

            Node DefaultArgument = NodeHelper.CreateDefault(typeof(Argument));
            Node DefaultTypeArgument = NodeHelper.CreateDefault(typeof(TypeArgument));

            Node DefaultName = NodeHelper.CreateDefault(typeof(Name));
            Node DefaultIdentifier = NodeHelper.CreateDefault(typeof(Identifier));
            Node DefaultQualifiedName = NodeHelper.CreateDefault(typeof(QualifiedName));
            Node DefaultScope = NodeHelper.CreateDefault(typeof(Scope));
            Node DefaultImport = NodeHelper.CreateDefault(typeof(Import));
        }
    }
}
