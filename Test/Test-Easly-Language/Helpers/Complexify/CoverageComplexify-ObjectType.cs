namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        [Category("Complexify")]
        public static void TestComplexifyAnchoredType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            AnchoredType ObjectType1 = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            QualifiedName SplittableQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            AnchoredType ObjectType2 = NodeHelper.CreateAnchoredType(SplittableQualifiedName, AnchorKinds.Declaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AnchoredType);

            QualifiedName KeywordQualifiedName = NodeHelper.CreateSimpleQualifiedName("Result");

            AnchoredType ObjectType3 = NodeHelper.CreateAnchoredType(KeywordQualifiedName, AnchorKinds.Declaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is KeywordAnchoredType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyFunctionType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();
            ObjectType DefaultReturnType = NodeHelper.CreateDefaultObjectType();

            FunctionType ObjectType1 = NodeHelper.CreateFunctionType(DefaultBaseType, DefaultReturnType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            FunctionType ObjectType2 = NodeHelper.CreateFunctionType(AnchorType, DefaultReturnType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is FunctionType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyGenericType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            List<TypeArgument> SimpleTypeArgumentList = new() { DefaultTypeArgument };

            GenericType ObjectType1 = NodeHelper.CreateGenericType(EmptyIdentifier, SimpleTypeArgumentList);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            PositionalTypeArgument SimplePositionalTypeArgument = NodeHelper.CreateSimplePositionalTypeArgument("a[b,c]");

            GenericType ObjectType2 = NodeHelper.CreateGenericType(EmptyIdentifier, new List<TypeArgument>() { SimplePositionalTypeArgument });

            //System.Diagnostics.Debugger.Launch();
            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 2);
            Assert.That(ComplexifiedNodeList[0] is GenericType);
            Assert.That(ComplexifiedNodeList[1] is GenericType);

            TypeArgument SplittableTypeArgument = NodeHelper.CreateSimplePositionalTypeArgument("a,b");

            GenericType ObjectType3 = NodeHelper.CreateGenericType(EmptyIdentifier, new List<TypeArgument>() { SplittableTypeArgument });

            Result = NodeHelper.GetComplexifiedNode(ObjectType3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is GenericType);

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            AssignmentTypeArgument SimpleAssignmentTypeArgument = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);

            GenericType ObjectType4 = NodeHelper.CreateGenericType(EmptyIdentifier, new List<TypeArgument>() { SimpleAssignmentTypeArgument });

            Result = NodeHelper.GetComplexifiedNode(ObjectType4, out _);
            Assert.False(Result);

            TypeArgument SplittableTypeArgumentNoEnd = NodeHelper.CreateSimplePositionalTypeArgument("a,");

            GenericType ObjectType5 = NodeHelper.CreateGenericType(EmptyIdentifier, new List<TypeArgument>() { SplittableTypeArgumentNoEnd });

            Result = NodeHelper.GetComplexifiedNode(ObjectType5, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyIndexerType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();
            ObjectType DefaultReturnType = NodeHelper.CreateDefaultObjectType();
            EntityDeclaration EmptyEntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();

            IndexerType ObjectType1 = NodeHelper.CreateIndexerType(DefaultBaseType, DefaultReturnType, EmptyEntityDeclaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            IndexerType ObjectType2 = NodeHelper.CreateIndexerType(AnchorType, DefaultReturnType, EmptyEntityDeclaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexerType);

            IndexerType ObjectType3 = NodeHelper.CreateIndexerType(DefaultBaseType, AnchorType, EmptyEntityDeclaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexerType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyProcedureType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();

            ProcedureType ObjectType1 = NodeHelper.CreateProcedureType(DefaultBaseType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            ProcedureType ObjectType2 = NodeHelper.CreateProcedureType(AnchorType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ProcedureType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyPropertyType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();
            ObjectType DefaultEntityType = NodeHelper.CreateDefaultObjectType();

            PropertyType ObjectType1 = NodeHelper.CreatePropertyType(DefaultBaseType, DefaultEntityType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like a");

            PropertyType ObjectType2 = NodeHelper.CreatePropertyType(AnchorType, DefaultEntityType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PropertyType);

            PropertyType ObjectType3 = NodeHelper.CreatePropertyType(DefaultBaseType, AnchorType);

            Result = NodeHelper.GetComplexifiedNode(ObjectType3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PropertyType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifySimpleType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            SimpleType ObjectType1 = NodeHelper.CreateEmptySimpleType();

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            SimpleType ObjectType2 = NodeHelper.CreateSimpleSimpleType("like a");

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AnchoredType);

            SimpleType ObjectType3 = NodeHelper.CreateSimpleSimpleType("function a");

            Result = NodeHelper.GetComplexifiedNode(ObjectType3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is FunctionType);

            SimpleType ObjectType4 = NodeHelper.CreateSimpleSimpleType("a[b]");

            Result = NodeHelper.GetComplexifiedNode(ObjectType4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is GenericType);

            SimpleType ObjectType5 = NodeHelper.CreateSimpleSimpleType("indexer a");

            Result = NodeHelper.GetComplexifiedNode(ObjectType5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexerType);

            SimpleType ObjectType6 = NodeHelper.CreateSimpleSimpleType("property a");

            Result = NodeHelper.GetComplexifiedNode(ObjectType6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PropertyType);

            SimpleType ObjectType7 = NodeHelper.CreateSimpleSimpleType("procedure a");

            Result = NodeHelper.GetComplexifiedNode(ObjectType7, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ProcedureType);

            SimpleType ObjectType8 = NodeHelper.CreateSimpleSimpleType("tuple a");

            Result = NodeHelper.GetComplexifiedNode(ObjectType8, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is TupleType);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyTupleType()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            EntityDeclaration EmptyEntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();

            TupleType ObjectType1 = NodeHelper.CreateTupleType(EmptyEntityDeclaration);

            Result = NodeHelper.GetComplexifiedNode(ObjectType1, out _);
            Assert.False(Result);

            Name SplittableName = NodeHelper.CreateSimpleName("a:b");
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            EntityDeclaration SplittableNameEntityDeclaration = NodeHelper.CreateEntityDeclaration(SplittableName, DefaultObjectType);
            IBlockList<EntityDeclaration> SplittableNameEntityDeclarationBlockList = BlockListHelper.CreateSimpleBlockList(SplittableNameEntityDeclaration);

            TupleType ObjectType2 = NodeHelper.CreateTupleType(SharingType.ReadWrite, SplittableNameEntityDeclarationBlockList);

            Result = NodeHelper.GetComplexifiedNode(ObjectType2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is TupleType);

            Name SimpleName = NodeHelper.CreateSimpleName("a");

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            AnchoredType AnchoredType = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);
            EntityDeclaration AnchorEntityDeclaration = NodeHelper.CreateEntityDeclaration(SimpleName, AnchoredType);
            IBlockList<EntityDeclaration> AnchoredDeclarationBlockList = BlockListHelper.CreateSimpleBlockList(AnchorEntityDeclaration);

            TupleType ObjectType3 = NodeHelper.CreateTupleType(SharingType.ReadWrite, AnchoredDeclarationBlockList);

            Result = NodeHelper.GetComplexifiedNode(ObjectType3, out _);
            Assert.False(Result);

            ObjectType SplittableObjectType = NodeHelper.CreateSimpleSimpleType("b,c");
            EntityDeclaration SimpleEntityDeclaration = NodeHelper.CreateEntityDeclaration(SimpleName, SplittableObjectType);
            IBlockList<EntityDeclaration> EntityDeclarationBlockList = BlockListHelper.CreateSimpleBlockList(SimpleEntityDeclaration);

            TupleType ObjectType4 = NodeHelper.CreateTupleType(SharingType.ReadWrite, EntityDeclarationBlockList);

            Result = NodeHelper.GetComplexifiedNode(ObjectType4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is TupleType);
        }
    }
}
