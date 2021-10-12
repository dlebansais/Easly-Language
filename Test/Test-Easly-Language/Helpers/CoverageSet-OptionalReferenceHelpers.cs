namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        public static void TestOptionalReferenceHelpers()
        {
            IOptionalReference<Identifier> EmptyEmptyReference = OptionalReferenceHelper.CreateEmptyReference<Identifier>();

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            IOptionalReference<Identifier> SimpleOptionalReference = OptionalReferenceHelper.CreateReference<Identifier>(EmptyIdentifier);

            IOptionalReference<Identifier> OptionalReferenceCopy;

            OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy<Identifier>(SimpleOptionalReference);
            OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy<Identifier>(EmptyEmptyReference);
        }
    }
}
