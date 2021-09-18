#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeHelper
    {
        public static AgentExpression CreateAgentExpression(Identifier delegated)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        public static AgentExpression CreateAgentExpression(Identifier delegated, ObjectType baseType)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<ObjectType>.CreateReference(baseType);
            Result.BaseType.Assign();

            return Result;
        }

        public static AssertionTagExpression CreateAssertionTagExpression(Identifier tagIdentifier)
        {
            AssertionTagExpression Result = new AssertionTagExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.TagIdentifier = tagIdentifier;

            return Result;
        }

        public static BinaryConditionalExpression CreateBinaryConditionalExpression(Expression leftExpression, ConditionalTypes conditional, Expression rightExpression)
        {
            BinaryConditionalExpression Result = new BinaryConditionalExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Conditional = conditional;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static BinaryOperatorExpression CreateBinaryOperatorExpression(Expression leftExpression, Identifier operatorName, Expression rightExpression)
        {
            BinaryOperatorExpression Result = new BinaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static ClassConstantExpression CreateClassConstantExpression(Identifier classIdentifier, Identifier constantIdentifier)
        {
            ClassConstantExpression Result = new ClassConstantExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.ConstantIdentifier = constantIdentifier;

            return Result;
        }

        public static CloneOfExpression CreateCloneOfExpression(CloneType type, Expression source)
        {
            CloneOfExpression Result = new CloneOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Type = type;
            Result.Source = source;

            return Result;
        }

        public static EntityExpression CreateEntityExpression(QualifiedName query)
        {
            EntityExpression Result = new EntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        public static EqualityExpression CreateEqualityExpression(Expression leftExpression, ComparisonType comparison, EqualityType equality, Expression rightExpression)
        {
            EqualityExpression Result = new EqualityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Comparison = comparison;
            Result.Equality = equality;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IndexQueryExpression CreateIndexQueryExpression(Expression indexedExpression, List<Argument> argumentList)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IndexQueryExpression CreateIndexQueryExpression(Expression indexedExpression, IBlockList<Argument> argumentBlocks)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static InitializedObjectExpression CreateInitializedObjectExpression(Identifier classIdentifier, List<AssignmentArgument> assignmentArgumentList)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = BlockListHelper<AssignmentArgument>.CreateBlockList(assignmentArgumentList);

            return Result;
        }

        public static InitializedObjectExpression CreateInitializedObjectExpression(Identifier classIdentifier, IBlockList<AssignmentArgument> assignmentBlocks)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = assignmentBlocks;

            return Result;
        }

        public static KeywordEntityExpression CreateKeywordEntityExpression(Keyword value)
        {
            KeywordEntityExpression Result = new KeywordEntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static KeywordExpression CreateKeywordExpression(Keyword value)
        {
            KeywordExpression Result = new KeywordExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static ManifestCharacterExpression CreateManifestCharacterExpression(string text)
        {
            ManifestCharacterExpression Result = new ManifestCharacterExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        public static ManifestNumberExpression CreateDefaultManifestNumberExpression()
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = "0";

            return Result;
        }

        public static ManifestNumberExpression CreateSimpleManifestNumberExpression(string numberText)
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = numberText;

            return Result;
        }

        public static ManifestStringExpression CreateManifestStringExpression(string text)
        {
            ManifestStringExpression Result = new ManifestStringExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        public static NewExpression CreateNewExpression(QualifiedName objectName)
        {
            NewExpression Result = new NewExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Object = objectName;

            return Result;
        }

        public static OldExpression CreateOldExpression(QualifiedName query)
        {
            OldExpression Result = new OldExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        public static PrecursorExpression CreatePrecursorExpression(List<Argument> argumentList)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static PrecursorExpression CreatePrecursorExpression(IBlockList<Argument> argumentBlocks)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static PrecursorExpression CreatePrecursorExpression(IBlockList<Argument> argumentBlocks, ObjectType ancestorType)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static PrecursorIndexExpression CreatePrecursorIndexExpression(List<Argument> argumentList)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static PrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<Argument> argumentBlocks)
        {
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static PrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<Argument> argumentBlocks, ObjectType ancestorType)
        {
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<ObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static PreprocessorExpression CreatePreprocessorExpression(PreprocessorMacro value)
        {
            PreprocessorExpression Result = new PreprocessorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static QueryExpression CreateQueryExpression(QualifiedName query, List<Argument> argumentList)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<Argument>.CreateBlockList(argumentList);

            return SimpleQueryExpression;
        }

        public static QueryExpression CreateQueryExpression(QualifiedName query, IBlockList<Argument> argumentBlocks)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = argumentBlocks;

            return SimpleQueryExpression;
        }

        public static ResultOfExpression CreateResultOfExpression(Expression source)
        {
            ResultOfExpression Result = new ResultOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }

        public static UnaryNotExpression CreateUnaryNotExpression(Expression rightExpression)
        {
            UnaryNotExpression Result = new UnaryNotExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static UnaryOperatorExpression CreateUnaryOperatorExpression(Identifier operatorName, Expression rightExpression)
        {
            UnaryOperatorExpression Result = new UnaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }
    }
}
