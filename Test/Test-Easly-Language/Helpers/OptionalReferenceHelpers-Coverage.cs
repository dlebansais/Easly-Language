namespace TestEaslyLanguage;

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
        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        IOptionalReference<Identifier> EmptyOptionalReference = OptionalReferenceHelper.CreateReference(EmptyIdentifier);

        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("test");
        IOptionalReference<Identifier> AssignedOptionalReference = OptionalReferenceHelper.CreateReference(SimpleIdentifier);
        AssignedOptionalReference.Assign();

        IOptionalReference<Identifier> OptionalReferenceCopy;

        OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy(EmptyOptionalReference);
        Assert.IsFalse(OptionalReferenceCopy.IsAssigned);

        OptionalReferenceCopy = OptionalReferenceHelper.CreateReferenceCopy(AssignedOptionalReference);
        Assert.IsTrue(OptionalReferenceCopy.IsAssigned);
        Assert.AreEqual(OptionalReferenceCopy.Item.Text, AssignedOptionalReference.Item.Text);
    }
}
