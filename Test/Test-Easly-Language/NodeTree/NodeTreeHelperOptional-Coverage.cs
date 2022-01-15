namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using Easly;
using NUnit.Framework;
using System;

[TestFixture]
public partial class NodeTreeHelperOptionalCoverage
{
    [Test]
    public static void TestIsOptionalChildNodeProperty()
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

    [Test]
    public static void TestIsOptionalChildNode()
    {
        bool Result;

        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("b");

        Result = NodeTreeHelperOptional.IsOptionalChildNode(NewClass, nameof(Class.FromIdentifier), SimpleIdentifier);
        Assert.False(Result);

        NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Class.FromIdentifier));

        Result = NodeTreeHelperOptional.IsOptionalChildNode(NewClass, nameof(Class.FromIdentifier), NewClass.FromIdentifier.Item);
        Assert.True(Result);

        Result = NodeTreeHelperOptional.IsOptionalChildNode(NewClass, nameof(Class.FromIdentifier), SimpleIdentifier);
        Assert.False(Result);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.IsOptionalChildNode(NewClass, nameof(Identifier.Text), SimpleIdentifier); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.IsOptionalChildNode(NewClass, nameof(Class.EntityName), SimpleIdentifier); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsOptionalChildNode(NullClass, nameof(Class.FromIdentifier), SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsOptionalChildNode(NewClass, NullString, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsOptionalChildNode(NewClass, nameof(Class.FromIdentifier), NullIdentifier); });
#endif
    }

    [Test]
    public static void TestGetChildNode()
    {
        Class NewClass = NodeHelper.CreateSimpleClass("a");
        bool IsAssigned;
        bool HasItem;
        Node ChildNode;

        NodeTreeHelperOptional.GetChildNode(NewClass, nameof(Class.FromIdentifier), out IsAssigned, out HasItem, out _);
        Assert.False(IsAssigned);
        Assert.True(HasItem);

        NodeTreeHelperOptional.GetChildNode((IOptionalReference)NewClass.FromIdentifier, out IsAssigned, out _);
        Assert.False(IsAssigned);

        NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Class.FromIdentifier));

        NodeTreeHelperOptional.GetChildNode(NewClass, nameof(Class.FromIdentifier), out IsAssigned, out HasItem, out ChildNode);
        Assert.True(IsAssigned);
        Assert.True(HasItem);
        Assert.AreEqual(ChildNode, NewClass.FromIdentifier.Item);

        NodeTreeHelperOptional.GetChildNode((IOptionalReference)NewClass.FromIdentifier, out IsAssigned, out ChildNode);
        Assert.True(IsAssigned);
        Assert.AreEqual(ChildNode, NewClass.FromIdentifier.Item);

        NodeTreeHelperOptional.ClearOptionalChildNode(NewClass, nameof(Class.FromIdentifier));

        NodeTreeHelperOptional.GetChildNode(NewClass, nameof(Class.FromIdentifier), out IsAssigned, out HasItem, out _);
        Assert.False(IsAssigned);
        Assert.False(HasItem);

        NodeTreeHelperOptional.GetChildNode((IOptionalReference)NewClass.FromIdentifier, out IsAssigned, out _);
        Assert.False(IsAssigned);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.GetChildNode(NewClass, nameof(Identifier.Text), out _, out _, out _); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.GetChildNode(NewClass, nameof(Class.EntityName), out _, out _, out _); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.GetChildNode(NullClass, nameof(Class.FromIdentifier), out _, out _, out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.GetChildNode(NewClass, NullString, out _, out _, out _); });
#endif
    }

    [Test]
    public static void TestOptionalItemType()
    {
        Type Result;

        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Result = NodeTreeHelperOptional.OptionalItemType(NewClass, nameof(Class.FromIdentifier));
        Assert.AreEqual(Result, typeof(Identifier));

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.OptionalItemType(NewClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.OptionalItemType(NewClass, nameof(Class.EntityName)); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.OptionalItemType(NullClass, nameof(Class.FromIdentifier)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.OptionalItemType(NewClass, NullString); });
#endif
    }

    [Test]
    public static void TestGetOptionalReference()
    {
        IOptionalReference Result;

        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Result = NodeTreeHelperOptional.GetOptionalReference(NewClass, nameof(Class.FromIdentifier));
        Assert.AreEqual(Result, NewClass.FromIdentifier);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.GetOptionalReference(NewClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.GetOptionalReference(NewClass, nameof(Class.EntityName)); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.GetOptionalReference(NullClass, nameof(Class.FromIdentifier)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.GetOptionalReference(NewClass, NullString); });
#endif
    }

    [Test]
    public static void TestSetOptionalReference()
    {
        Class NewClass = NodeHelper.CreateSimpleClass("a");
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("b");
        IOptionalReference NewFromIdentifier = (IOptionalReference)OptionalReferenceHelper.CreateReference(SimpleIdentifier);

        NodeTreeHelperOptional.SetOptionalReference(NewClass, nameof(Class.FromIdentifier), NewFromIdentifier);
        Assert.AreEqual(NewClass.FromIdentifier, NewFromIdentifier);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.SetOptionalReference(NewClass, nameof(Identifier.Text), NewFromIdentifier); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.SetOptionalReference(NewClass, nameof(Class.EntityName), NewFromIdentifier); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        IOptionalReference NullOptionalReference = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.SetOptionalReference(NullClass, nameof(Class.FromIdentifier), NewFromIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.SetOptionalReference(NewClass, NullString, NewFromIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.SetOptionalReference(NewClass, nameof(Class.FromIdentifier), NullOptionalReference); });
#endif
    }

    [Test]
    public static void TestSetOptionalChildNode()
    {
        Class NewClass = NodeHelper.CreateSimpleClass("a");
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("b");

        NodeTreeHelperOptional.SetOptionalChildNode(NewClass, nameof(Class.FromIdentifier), SimpleIdentifier);
        Assert.True(NewClass.FromIdentifier.HasItem);
        Assert.True(NewClass.FromIdentifier.IsAssigned);
        Assert.AreEqual(NewClass.FromIdentifier.Item, SimpleIdentifier);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.SetOptionalChildNode(NewClass, nameof(Identifier.Text), SimpleIdentifier); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.SetOptionalChildNode(NewClass, nameof(Class.EntityName), SimpleIdentifier); });

        Name SimpleName = NodeHelper.CreateSimpleName("c");

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.SetOptionalChildNode(NewClass, nameof(Class.FromIdentifier), SimpleName); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Identifier NullIdentifier = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.SetOptionalChildNode(NullClass, nameof(Class.FromIdentifier), SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.SetOptionalChildNode(NewClass, NullString, SimpleIdentifier); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.SetOptionalChildNode(NewClass, nameof(Class.FromIdentifier), NullIdentifier); });
#endif
    }

    [Test]
    public static void TestIsChildNodeAssigned()
    {
        bool Result;

        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Result = NodeTreeHelperOptional.IsChildNodeAssigned(NewClass, nameof(Class.FromIdentifier));
        Assert.False(Result);

        NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Class.FromIdentifier));

        Result = NodeTreeHelperOptional.IsChildNodeAssigned(NewClass, nameof(Class.FromIdentifier));
        Assert.True(Result);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.IsChildNodeAssigned(NewClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.IsChildNodeAssigned(NewClass, nameof(Class.EntityName)); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsChildNodeAssigned(NullClass, nameof(Class.FromIdentifier)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.IsChildNodeAssigned(NewClass, NullString); });
#endif
    }

    [Test]
    public static void TestAssignChildNode()
    {
        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Assert.False(NewClass.FromIdentifier.IsAssigned);

        NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Class.FromIdentifier));
        Assert.True(NewClass.FromIdentifier.IsAssigned);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Class.EntityName)); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.AssignChildNode(NullClass, nameof(Class.FromIdentifier)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.AssignChildNode(NewClass, NullString); });
#endif
    }

    [Test]
    public static void TestUnassignChildNode()
    {
        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Assert.False(NewClass.FromIdentifier.IsAssigned);

        NodeTreeHelperOptional.AssignChildNode(NewClass, nameof(Class.FromIdentifier));
        Assert.True(NewClass.FromIdentifier.IsAssigned);

        NodeTreeHelperOptional.UnassignChildNode(NewClass, nameof(Class.FromIdentifier));
        Assert.False(NewClass.FromIdentifier.IsAssigned);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.UnassignChildNode(NewClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.UnassignChildNode(NewClass, nameof(Class.EntityName)); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.UnassignChildNode(NullClass, nameof(Class.FromIdentifier)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.UnassignChildNode(NewClass, NullString); });
#endif
    }

    [Test]
    public static void TestClearOptionalChildNode()
    {
        Class NewClass = NodeHelper.CreateSimpleClass("a");

        Assert.True(NewClass.FromIdentifier.HasItem);
        Assert.False(NewClass.FromIdentifier.IsAssigned);

        NodeTreeHelperOptional.ClearOptionalChildNode(NewClass, nameof(Class.FromIdentifier));
        Assert.False(NewClass.FromIdentifier.HasItem);

        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.ClearOptionalChildNode(NewClass, nameof(Identifier.Text)); });
        Assert.Throws<ArgumentException>(() => { NodeTreeHelperOptional.ClearOptionalChildNode(NewClass, nameof(Class.EntityName)); });

#if !DEBUG
        Class NullClass = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.ClearOptionalChildNode(NullClass, nameof(Class.FromIdentifier)); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperOptional.ClearOptionalChildNode(NewClass, NullString); });
#endif
    }
}
