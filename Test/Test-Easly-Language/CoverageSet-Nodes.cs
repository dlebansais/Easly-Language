namespace TestEaslyLanguage
{
    using BaseNode;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    [TestFixture]
    public partial class CoverageSet
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
    }
}
