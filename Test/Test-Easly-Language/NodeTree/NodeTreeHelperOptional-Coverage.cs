namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public partial class NodeTreeHelperOptionalCoverage
    {
        [Test]
        public static void TestIsChildNodeProperty()
        {
            bool Result;

            Class NewClass = NodeHelper.CreateSimpleClass("a");
            Type ChildNodeType;

            Result = NodeTreeHelperOptional.IsOptionalChildNodeProperty(NewClass, nameof(Class.FromIdentifier), out ChildNodeType);
            Assert.True(Result);
            Assert.AreEqual(ChildNodeType, typeof(Identifier));

            Result = NodeTreeHelperOptional.IsOptionalChildNodeProperty(typeof(Class), nameof(Class.FromIdentifier), out ChildNodeType);
            Assert.True(Result);
            Assert.AreEqual(ChildNodeType, typeof(Identifier));

            Result = NodeTreeHelperOptional.IsOptionalChildNodeProperty(typeof(Identifier), nameof(Class.FromIdentifier), out _);
            Assert.False(Result);

            Result = NodeTreeHelperOptional.IsOptionalChildNodeProperty(typeof(Class), nameof(Class.EntityName), out _);
            Assert.False(Result);

#if !DEBUG
            Class NullClass = null!;
            string NullString = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsOptionalChildNodeProperty(NullClass, nameof(Class.FromIdentifier), out _); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsOptionalChildNodeProperty(NewClass, NullString, out _); });
#endif
        }
    }
}
