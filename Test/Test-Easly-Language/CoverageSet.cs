namespace Test
{
    using BaseNode;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    [TestFixture]
    public class CoverageSet
    {
        [Test]
        public static void TestNodeInitializers()
        {
            Node[] InitializedObjects = new Node[]
            {
                new Assertion(),
                new Attachment(),
                new Class(),
                new ClassReplicate(),
                new CommandOverload(),
                new CommandOverloadType(),
                new Conditional(),
                new Constraint(),
                new Continuation(),
                new Discrete(),
                new EntityDeclaration(),
                new ExceptionHandler(),
                new Export(),
                new ExportChange(),
                new Generic(),
                new GlobalReplicate(),
                new Identifier(),
                new Import(),
                new Inheritance(),
                new Library(),
                new Name(),
                new Pattern(),
                new QualifiedName(),
                new QueryOverload(),
                new QueryOverloadType(),
                new Range(),
                new Rename(),
                new Root(),
                new Scope(),
                new Typedef(),
                new With(),
                new AssignmentArgument(),
                new PositionalArgument(),
                new DeferredBody(),
                new EffectiveBody(),
                new ExternBody(),
                new PrecursorBody(),
                new AgentExpression(),
                new AssertionTagExpression(),
                new BinaryConditionalExpression(),
                new BinaryOperatorExpression(),
                new ClassConstantExpression(),
                new CloneOfExpression(),
                new EntityExpression(),
                new EqualityExpression(),
                new IndexQueryExpression(),
                new InitializedObjectExpression(),
                new KeywordEntityExpression(),
                new KeywordExpression(),
                new ManifestCharacterExpression(),
                new ManifestNumberExpression(),
                new ManifestStringExpression(),
                new NewExpression(),
                new OldExpression(),
                new PrecursorExpression(),
                new PrecursorIndexExpression(),
                new PreprocessorExpression(),
                new QueryExpression(),
                new ResultOfExpression(),
                new UnaryNotExpression(),
                new UnaryOperatorExpression(),
                new IndexerFeature(),
                new AttributeFeature(),
                new ConstantFeature(),
                new CreationFeature(),
                new FunctionFeature(),
                new ProcedureFeature(),
                new PropertyFeature(),
                new AsLongAsInstruction(),
                new AssignmentInstruction(),
                new AttachmentInstruction(),
                new CheckInstruction(),
                new CommandInstruction(),
                new CreateInstruction(),
                new DebugInstruction(),
                new ForLoopInstruction(),
                new IfThenElseInstruction(),
                new IndexAssignmentInstruction(),
                new InspectInstruction(),
                new KeywordAssignmentInstruction(),
                new OverLoopInstruction(),
                new PrecursorIndexAssignmentInstruction(),
                new PrecursorInstruction(),
                new RaiseEventInstruction(),
                new ReleaseInstruction(),
                new ThrowInstruction(),
                new AnchoredType(),
                new FunctionType(),
                new IndexerType(),
                new KeywordAnchoredType(),
                new ProcedureType(),
                new PropertyType(),
                new GenericType(),
                new SimpleType(),
                new TupleType(),
                new AssignmentTypeArgument(),
                new PositionalTypeArgument(),
            };
        }

        [Test]
        public static void TestBlockInitializers()
        {
            Block<Node> TestBlock = new();
            BlockList<Node> TestBlockList = new();
            TestBlockList.Documentation = new Document();

            IList<Node> NodeList = TestBlock.NodeList;
            IList NonGenericNodeList = ((IBlock)TestBlock).NodeList;

            IList<IBlock<Node>> BlockList = TestBlockList.NodeBlockList;
            IList NonGenericBlockList = ((IBlockList)TestBlockList).NodeBlockList;
            Document TestDocument = TestBlockList.Documentation;
            TestDocument = ((IBlockList)TestBlockList).Documentation;

            string Comment = TestDocument.Comment;
            Guid Uuid = TestDocument.Uuid;
        }

        [Test]
        public static void TestLanguageInitializers()
        {
            MemberInfo FunctionInfo = typeof(IList).GetMember("IndexOf")[0];
            PropertyInfo IndexerInfo = typeof(IList).GetProperty("Item");
            MemberInfo ProcedureInfo = typeof(IList).GetMember("Clear")[0];
            PropertyInfo PropertyInfo = typeof(Name).GetProperty("Text");

            FeatureEntity TestFeatureEntity = new(FunctionInfo);
            FunctionEntity TestFunctionEntity = new(FunctionInfo);
            IndexerEntity TestIndexerEntity = new(IndexerInfo);
            ProcedureEntity TestProcedureEntity = new(ProcedureInfo);
            PropertyEntity TestPropertyEntity = new(PropertyInfo);

            SpecializedTypeEntity<Class> TestSpecializedTypeEntity = SpecializedTypeEntity<Class>.Singleton;
            TestSpecializedTypeEntity = SpecializedTypeEntity<Class>.Singleton; // Class twice to cover different branches in the code.

            PropertyFeature TestFeature = new();
            Entity TestEntity = Entity.FromThis(TestFeature);
            Entity TestStatisEntity = Entity.FromStaticConstructor();

            DateAndTime TestDateAndTime = new();
            Event TestEvent = new(isAutoReset: true);

            DetachableReference<Node> TestDetachableReference = new();
            OnceReference<Node> TestOnceReference = new();
            OptionalReference<Node> TestOptionalReference = new();
            StableReference<Node> TestStableReference = new();

            SealableList<Node> TestSealableList = new();
            SealableDictionary<string, Node> TestSealableDictionary = new();
        }

        [Test]
        public static void TestLanguageClasses()
        {
            MemberInfo FunctionInfo = typeof(IList).GetMember("IndexOf")[0];
            PropertyInfo IndexerInfo = typeof(IList).GetProperty("Item");
            MemberInfo ProcedureInfo = typeof(IList).GetMember("Clear")[0];
            PropertyInfo PropertyInfo = typeof(Name).GetProperty("Text");

            FunctionEntity TestFunctionEntity = new(FunctionInfo);

            /*
            PropertyInfo[] AllMembers = typeof(IList).GetProperties();
            string MemberString = string.Empty;
            foreach (PropertyInfo Item in AllMembers)
                MemberString += "\n" + Item.Name;

            System.Diagnostics.Debug.Assert(false, MemberString);
            */

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

            Name TestObject = new();

            var TestValue = TestPropertyEntity.GetValue(TestObject);
            TestPropertyEntity.SetValue(TestObject, TestValue);
        }

        [Test]
        public static void TestDetachableReference()
        {
            Name TestObject = null;
            bool IsAssigned;

            DetachableReference<Name> TestDetachableReference = new();
            IDetachableReference TestInterface = TestDetachableReference;

            IsAssigned = TestDetachableReference.IsAssigned;
            Assert.False(IsAssigned);

            Assert.Throws<InvalidOperationException>(() => { TestObject = TestDetachableReference.Item; });
            Assert.Throws<InvalidOperationException>(() => { TestObject = TestInterface.Item as Name; });

            TestDetachableReference.Item = new Name();
            IsAssigned = TestDetachableReference.IsAssigned;
            Assert.True(IsAssigned);

            TestObject = TestDetachableReference.Item;
            Assert.NotNull(TestObject);

            TestObject = TestInterface.Item as Name;
            Assert.NotNull(TestObject);

            Assert.Throws<InvalidOperationException>(() => { TestDetachableReference.Item = null; });
            Assert.Throws<InvalidOperationException>(() => { TestInterface.Item = null; });

            TestDetachableReference.Detach();
        }
    }
}
