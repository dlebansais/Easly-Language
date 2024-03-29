﻿namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using Easly;
using NotNullReflection;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using InvalidOperationException = System.InvalidOperationException;
using TypeInitializationException = System.TypeInitializationException;
using ArgumentNullException = System.ArgumentNullException;

[TestFixture]
public partial class CoverageSet
{
    [Test]
    public static void TestLanguageInitializers()
    {
        MethodInfo FunctionInfo = (MethodInfo)Type.FromTypeof<IList>().GetMember("IndexOf")[0];
        PropertyInfo IndexerInfo = Type.FromTypeof<IList>().GetProperty("Item");
        MethodInfo ProcedureInfo = (MethodInfo)Type.FromTypeof<IList>().GetMember("Clear")[0];
        PropertyInfo PropertyInfo = Type.FromTypeof<Name>().GetProperty("Text");

        FunctionEntity TestFunctionEntity = new(FunctionInfo);
        IndexerEntity TestIndexerEntity = new(IndexerInfo);
        ProcedureEntity TestProcedureEntity = new(ProcedureInfo);
        PropertyEntity TestPropertyEntity = new(PropertyInfo);

        TypeEntity ReturnType = TestIndexerEntity.Type;

        MethodInfo IndexerInfoAsMethod = (MethodInfo)Type.FromTypeof<IList>().GetMember("get_Item")[0];
        TestIndexerEntity = new(IndexerInfoAsMethod);
        ReturnType = TestIndexerEntity.Type;
            
        SpecializedTypeEntity<Class> TestSpecializedTypeEntityClass = SpecializedTypeEntity<Class>.Singleton;
        TestSpecializedTypeEntityClass = SpecializedTypeEntity<Class>.Singleton; // Class twice to cover different branches in the code.

        SpecializedTypeEntity<string> TestSpecializedTypeEntityString = SpecializedTypeEntity<string>.Singleton;
        TestSpecializedTypeEntityString.Procedure("CopyTo");
        TestSpecializedTypeEntityString.Function("CompareTo");
        TestSpecializedTypeEntityString.Property("Length");

        PropertyFeature TestFeature = NodeHelper.CreateEmptyPropertyFeature();
        Entity TestEntity = Entity.FromThis(TestFeature);
        Entity TestStaticEntity = Entity.FromStaticConstructor();

        //System.Diagnostics.Debug.Assert(false);
        Assert.Throws<TypeInitializationException>(() => { StaticConstructorTest<string> TestObject = new(); });

        DateAndTime TestDateAndTime = new();
        Event TestEvent = new(isAutoReset: true);

        DetachableReference<Node> TestDetachableReference = new();
        OnceReference<Node> TestOnceReference = new();
        StableReference<Node> TestStableReference = new();

        SealableList<Node> TestSealableList = new();
        SealableDictionary<string, Node> TestSealableDictionary = new();
    }

    [Test]
    public static void TestLanguageClasses()
    {
        MethodInfo FunctionInfo = (MethodInfo)Type.FromTypeof<IList>().GetMember("IndexOf")[0];
        PropertyInfo IndexerInfo = Type.FromTypeof<IList>().GetProperty("Item");
        MethodInfo ProcedureInfo = (MethodInfo)Type.FromTypeof<IList>().GetMember("Clear")[0];
        PropertyInfo PropertyInfo = Type.FromTypeof<Name>().GetProperty("Text");

        FunctionEntity TestFunctionEntity = new(FunctionInfo);
        IndexerEntity TestIndexerEntity = new(IndexerInfo);
        ProcedureEntity TestProcedureEntity = new(ProcedureInfo);
        PropertyEntity TestPropertyEntity = new(PropertyInfo);

        string TestName;
        TypeEntity TestType;

        TestName = TestFunctionEntity.Name;
        TestType = TestFunctionEntity.Type;
        TestType = TestIndexerEntity.Type;
        TestType = TestPropertyEntity.Type;
        TestName = TestType.Name;

        Name TestObject = NodeHelper.CreateEmptyName();

        var TestValue = TestPropertyEntity.GetValue(TestObject);
        TestPropertyEntity.SetValue(TestObject, TestValue);
    }

    [Test]
    public static void TestDetachableReference()
    {
        Name? TestObject = null;
        bool IsAssigned;

        DetachableReference<Name> TestDetachableReference = new();
        IDetachableReference TestInterface = TestDetachableReference;

        IsAssigned = TestDetachableReference.IsAssigned;
        Assert.False(IsAssigned);

        Assert.Throws<InvalidOperationException>(() => { TestObject = TestDetachableReference.Item; });
        Assert.Throws<InvalidOperationException>(() => { TestObject = TestInterface.Item as Name; });

        TestObject = NodeHelper.CreateEmptyName();

        TestDetachableReference.Item = TestObject;
        TestInterface.Item = TestObject;

        IsAssigned = TestDetachableReference.IsAssigned;
        Assert.True(IsAssigned);

        TestObject = TestDetachableReference.Item;
        Assert.NotNull(TestObject);

        TestObject = TestInterface.Item as Name;
        Assert.NotNull(TestObject);

        Assert.Throws<InvalidOperationException>(() => { TestDetachableReference.Item = null!; });
        Assert.Throws<InvalidOperationException>(() => { TestInterface.Item = null!; });

        IsAssigned = TestDetachableReference.IsAssigned;
        Assert.True(IsAssigned);

        TestDetachableReference.Detach();

        IsAssigned = TestDetachableReference.IsAssigned;
        Assert.False(IsAssigned);
    }

    [Test]
    public static void TestOnceReference()
    {
        Name? TestObject = null;
        bool IsAssigned;

        OnceReference<Name> TestOnceReference = new();
        IOnceReference TestInterface = TestOnceReference;

        IsAssigned = TestOnceReference.IsAssigned;
        Assert.False(IsAssigned);

        Assert.Throws<InvalidOperationException>(() => { TestObject = TestOnceReference.Item; });
        Assert.Throws<InvalidOperationException>(() => { TestObject = TestInterface.Item as Name; });

        Assert.Throws<InvalidOperationException>(() => { TestOnceReference.Item = null!; });
        Assert.Throws<InvalidOperationException>(() => { TestInterface.Item = null!; });

        TestObject = NodeHelper.CreateEmptyName();

        TestInterface.Item = TestObject;
        IsAssigned = TestInterface.IsAssigned;
        Assert.True(IsAssigned);

        Assert.Throws<InvalidOperationException>(() => { TestInterface.Item = TestObject; });

        TestOnceReference = new OnceReference<Name>();
        TestOnceReference.Item = TestObject;

        IsAssigned = TestOnceReference.IsAssigned;
        Assert.True(IsAssigned);

        TestObject = TestOnceReference.Item;
        Assert.NotNull(TestObject);

        TestObject = TestInterface.Item as Name;
        Assert.NotNull(TestObject);

        if (TestObject is not null)
        {
            Assert.Throws<InvalidOperationException>(() => { TestOnceReference.Item = TestObject; });

            TestObject = NodeHelper.CreateEmptyName();

            Assert.Throws<InvalidOperationException>(() => { TestOnceReference.Item = TestObject; });
        }
    }

    [Test]
    public static void TestOptionalReference()
    {
        Name? TestObject = NodeHelper.CreateEmptyName();
        bool IsAssigned;

        OptionalReference<Name> TestOptionalReference = new(TestObject);
        IOptionalReference TestInterface = TestOptionalReference;

        IsAssigned = TestOptionalReference.IsAssigned;
        Assert.False(IsAssigned);

        TestOptionalReference.Item = TestObject;
        TestInterface.Item = TestObject;

        IsAssigned = TestOptionalReference.IsAssigned;
        Assert.True(IsAssigned);

        TestObject = TestOptionalReference.Item;
        Assert.NotNull(TestObject);

        TestObject = TestInterface.Item as Name;
        Assert.NotNull(TestObject);

        TestOptionalReference.Unassign();
        IsAssigned = TestOptionalReference.IsAssigned;
        Assert.False(IsAssigned);

        TestOptionalReference.Assign();
        IsAssigned = TestOptionalReference.IsAssigned;
        Assert.True(IsAssigned);

        Identifier? WrongTestObject = NodeHelper.CreateEmptyIdentifier();

        Assert.Throws<InvalidOperationException>(() => { TestInterface.Item = WrongTestObject; });

#if !DEBUG
        Name NullName = null!;
        Assert.Throws<ArgumentNullException>(() => { TestOptionalReference.Item = NullName; });
        Assert.Throws<ArgumentNullException>(() => { TestInterface.Item = NullName; });
#endif
    }

    [Test]
    public static void TestStableReference()
    {
        Name? TestObject = null;
        bool IsAssigned;

        StableReference<Name> TestStableReference = new();
        IStableReference TestInterface = TestStableReference;

        IsAssigned = TestStableReference.IsAssigned;
        Assert.False(IsAssigned);

        Assert.Throws<InvalidOperationException>(() => { TestObject = TestStableReference.Item; });
        Assert.Throws<InvalidOperationException>(() => { TestObject = TestInterface.Item as Name; });

        TestObject = NodeHelper.CreateEmptyName();

        TestInterface.Item = TestObject;
        IsAssigned = TestInterface.IsAssigned;
        Assert.True(IsAssigned);

        TestStableReference = new StableReference<Name>();
        TestStableReference.Item = TestObject;

        IsAssigned = TestStableReference.IsAssigned;
        Assert.True(IsAssigned);

        TestObject = TestStableReference.Item;
        Assert.NotNull(TestObject);

        TestObject = TestInterface.Item as Name;
        Assert.NotNull(TestObject);

        Assert.Throws<InvalidOperationException>(() => { TestStableReference.Item = null!; });
        Assert.Throws<InvalidOperationException>(() => { TestInterface.Item = null!; });

        IsAssigned = TestStableReference.IsAssigned;
        Assert.True(IsAssigned);

        TestObject = NodeHelper.CreateEmptyName();
        TestStableReference.Item = TestObject;

        IsAssigned = TestStableReference.IsAssigned;
        Assert.True(IsAssigned);
    }

    [Test]
    public static void TestEvent()
    {
        Event TestManualReset = new(isAutoReset: false, false);
        Assert.False(TestManualReset.IsTrue);
        Assert.False(TestManualReset.IsFalse);

        TestManualReset.Raise();

        Event TestAutoReset = new(isAutoReset: true, false);
        Assert.False(TestAutoReset.IsTrue);
        Assert.False(TestAutoReset.IsFalse);

        TestAutoReset.Raise();

        EventBase Result;
        Result = TestManualReset & TestAutoReset;
        Result = TestManualReset | TestAutoReset;
        Result = TestManualReset ^ TestAutoReset;
        Result = TestManualReset / TestAutoReset;

        bool IsTrue, IsSignaled;

        IsTrue = TestManualReset ? true : false;
        Assert.That(!TestManualReset.IsTrue);
        Assert.That(!TestManualReset.IsFalse);

        Result = TestAutoReset && TestAutoReset;
        Assert.That(!Result.IsTrue);
        Assert.That(!Result.IsFalse);

        TestManualReset.Wait();
        TestAutoReset.Wait();

        IsSignaled = TestManualReset.IsSignaled;
        Assert.True(IsSignaled);

        IsSignaled = TestAutoReset.IsSignaled;
        Assert.False(IsSignaled);
    }

    [Test]
    public static void TestSealableDictionaryFromSerializer()
    {
        Dictionary<string, string> TestDictionary = new() { { "Test", "Test" } };

        MemoryStream Stream = new();
        BinaryFormatter Formatter = new(null, new StreamingContext());
        Formatter.Serialize(Stream, TestDictionary);

        Stream.Seek(0, SeekOrigin.Begin);

        object Deserialized = Formatter.Deserialize(Stream);
    }

    [Test]
    public static void TestSealableDictionary()
    {
        SealableDictionary<string, string> TestDictionary = new();
        Assert.That(!TestDictionary.IsSealed);

        TestDictionary.Add("Key1", "Value1");
        TestDictionary.Add("Key2", "Value2");

        ICollection<string> Indexes = TestDictionary.Indexes;

        TestDictionary.Seal();
        Assert.That(TestDictionary.IsSealed);

        Assert.Throws<InvalidOperationException>(() => { TestDictionary.Add("Key", "Value"); });

        ISealableDictionary<string, string> CloneDictionary = TestDictionary.CloneUnsealed();
        Assert.That(!CloneDictionary.IsSealed);

        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.Add("Key1", "Value1"); });

        CloneDictionary.ChangeKey("Key1", "OtherKey1");

        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.ChangeKey("OtherKey1", "Key2"); });
        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.ChangeKey("OtherKey2", "Any"); });

        SealableDictionary<string, string> OtherDictionary = new();
        OtherDictionary.Add("Key3", "Value3");

        CloneDictionary.Merge(OtherDictionary);

        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.Merge(OtherDictionary); });

        OtherDictionary.Add("Key4", "Value4");

        CloneDictionary.MergeWithConflicts(OtherDictionary);

        CloneDictionary.Seal();
        Assert.That(CloneDictionary.IsSealed);

        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.Merge(OtherDictionary); });
        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.MergeWithConflicts(OtherDictionary); });
        Assert.Throws<InvalidOperationException>(() => { CloneDictionary.Seal(); });
    }

    [Test]
    public static void TestSealableList()
    {
        SealableList<string> TestList = new();
        Assert.That(!TestList.IsSealed);

        TestList.Add("Item1");

        TestList.Seal();
        Assert.That(TestList.IsSealed);

        Assert.Throws<InvalidOperationException>(() => { TestList.Add("Item1"); });

        ISealableList<string> CloneList = TestList.CloneUnsealed();
        Assert.That(!CloneList.IsSealed);

        SealableList<string> OtherList = new();
        OtherList.Add("Item2");

        CloneList.AddRange(OtherList);

        CloneList.Seal();
        Assert.That(CloneList.IsSealed);

        Assert.Throws<InvalidOperationException>(() => { CloneList.Add("Item3"); });
        Assert.Throws<InvalidOperationException>(() => { CloneList.AddRange(OtherList); });
        Assert.Throws<InvalidOperationException>(() => { CloneList.Seal(); });
    }
}
