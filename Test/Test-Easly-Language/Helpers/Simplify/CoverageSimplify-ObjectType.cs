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
    public partial class CoverageSet
    {
        [Test]
        [Category("Simplify")]
        public static void TestSimplifyAnchoredType()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            AnchoredType ObjectType1 = NodeHelper.CreateAnchoredType(SimpleQualifiedName, AnchorKinds.Declaration);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyFunctionType()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();
            ObjectType DefaultReturnType = NodeHelper.CreateDefaultObjectType();

            FunctionType ObjectType1 = NodeHelper.CreateFunctionType(DefaultBaseType, DefaultReturnType);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyGenericType()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            List<TypeArgument> SimpleTypeArgumentList = new() { DefaultTypeArgument };

            GenericType ObjectType1 = NodeHelper.CreateGenericType(EmptyIdentifier, SimpleTypeArgumentList);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);

            TypeArgument FirstTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            TypeArgument SecondTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            List<TypeArgument> NotSimpleTypeArgumentList = new() { FirstTypeArgument, SecondTypeArgument };

            GenericType ObjectType2 = NodeHelper.CreateGenericType(EmptyIdentifier, NotSimpleTypeArgumentList);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            AssignmentTypeArgument AssignmentTypeArgument = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);
            List<TypeArgument> AssignmentTypeArgumentList = new() { AssignmentTypeArgument };

            GenericType ObjectType3 = NodeHelper.CreateGenericType(EmptyIdentifier, AssignmentTypeArgumentList);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType3, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyIndexerType()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();
            ObjectType DefaultReturnType = NodeHelper.CreateDefaultObjectType();
            EntityDeclaration EmptyEntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();

            IndexerType ObjectType1 = NodeHelper.CreateIndexerType(DefaultBaseType, DefaultReturnType, EmptyEntityDeclaration);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyKeywordAnchoredType()
        {
            bool Result;
            Node SimplifiedNode;

            KeywordAnchoredType ObjectType1 = NodeHelper.CreateKeywordAnchoredType(Keyword.Result);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyProcedureType()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();

            ProcedureType ObjectType1 = NodeHelper.CreateProcedureType(DefaultBaseType);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyPropertyType()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultBaseType = NodeHelper.CreateDefaultObjectType();
            ObjectType DefaultEntityType = NodeHelper.CreateDefaultObjectType();

            PropertyType ObjectType1 = NodeHelper.CreatePropertyType(DefaultBaseType, DefaultEntityType);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyTupleType()
        {
            bool Result;
            Node SimplifiedNode;

            EntityDeclaration EmptyEntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();

            TupleType ObjectType1 = NodeHelper.CreateTupleType(EmptyEntityDeclaration);

            Result = NodeHelper.GetSimplifiedObjectType(ObjectType1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is SimpleType);
        }
    }
}
