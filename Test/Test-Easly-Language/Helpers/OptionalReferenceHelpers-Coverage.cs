namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;

    [TestFixture]
    public partial class OptionalReferenceHelpersCoverage
    {
        [Test]
        public static void Test()
        {
            IOptionalReference<Identifier> EmptyEmptyReference = OptionalReferenceHelper.CreateEmptyReference<Identifier>();

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            IOptionalReference<Identifier> SimpleOptionalReference = OptionalReferenceHelper.CreateReference<Identifier>(EmptyIdentifier);

            IOptionalReference<Identifier> AssignedOptionalReference = OptionalReferenceHelper.CreateReference<Identifier>(EmptyIdentifier);
            AssignedOptionalReference.Assign();

            IOptionalReference<Identifier> OptionalReferenceCopy;

            OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy<Identifier>(EmptyEmptyReference);
            OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy<Identifier>(SimpleOptionalReference);
            OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy<Identifier>(AssignedOptionalReference);
        }
    }
}
