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
        public static IAgentExpression CreateAgentExpression(IIdentifier delegated)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());

            return Result;
        }

        public static IAgentExpression CreateAgentExpression(IIdentifier delegated, IObjectType baseType)
        {
            AgentExpression Result = new AgentExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Delegated = delegated;
            Result.BaseType = OptionalReferenceHelper<IObjectType>.CreateReference(baseType);
            Result.BaseType.Assign();

            return Result;
        }

        public static IAssertionTagExpression CreateAssertionTagExpression(IIdentifier tagIdentifier)
        {
            AssertionTagExpression Result = new AssertionTagExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.TagIdentifier = tagIdentifier;

            return Result;
        }

        public static IBinaryConditionalExpression CreateBinaryConditionalExpression(IExpression leftExpression, ConditionalTypes conditional, IExpression rightExpression)
        {
            BinaryConditionalExpression Result = new BinaryConditionalExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Conditional = conditional;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IBinaryOperatorExpression CreateBinaryOperatorExpression(IExpression leftExpression, IIdentifier operatorName, IExpression rightExpression)
        {
            BinaryOperatorExpression Result = new BinaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IClassConstantExpression CreateClassConstantExpression(IIdentifier classIdentifier, IIdentifier constantIdentifier)
        {
            ClassConstantExpression Result = new ClassConstantExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.ConstantIdentifier = constantIdentifier;

            return Result;
        }

        public static ICloneOfExpression CreateCloneOfExpression(CloneType type, IExpression source)
        {
            CloneOfExpression Result = new CloneOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Type = type;
            Result.Source = source;

            return Result;
        }

        public static IEntityExpression CreateEntityExpression(IQualifiedName query)
        {
            EntityExpression Result = new EntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        public static IEqualityExpression CreateEqualityExpression(IExpression leftExpression, ComparisonType comparison, EqualityType equality, IExpression rightExpression)
        {
            EqualityExpression Result = new EqualityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.Comparison = comparison;
            Result.Equality = equality;
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IIndexQueryExpression CreateIndexQueryExpression(IExpression indexedExpression, List<IArgument> argumentList)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IIndexQueryExpression CreateIndexQueryExpression(IExpression indexedExpression, IBlockList<IArgument, Argument> argumentBlocks)
        {
            if (argumentBlocks == null) throw new ArgumentNullException(nameof(argumentBlocks));
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            IndexQueryExpression Result = new IndexQueryExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.IndexedExpression = indexedExpression;
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IInitializedObjectExpression CreateInitializedObjectExpression(IIdentifier classIdentifier, List<IAssignmentArgument> assignmentArgumentList)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = BlockListHelper<IAssignmentArgument, AssignmentArgument>.CreateBlockList(assignmentArgumentList);

            return Result;
        }

        public static IInitializedObjectExpression CreateInitializedObjectExpression(IIdentifier classIdentifier, IBlockList<IAssignmentArgument, AssignmentArgument> assignmentBlocks)
        {
            InitializedObjectExpression Result = new InitializedObjectExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.ClassIdentifier = classIdentifier;
            Result.AssignmentBlocks = assignmentBlocks;

            return Result;
        }

        public static IKeywordEntityExpression CreateKeywordEntityExpression(Keyword value)
        {
            KeywordEntityExpression Result = new KeywordEntityExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static IKeywordExpression CreateKeywordExpression(Keyword value)
        {
            KeywordExpression Result = new KeywordExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static IManifestCharacterExpression CreateManifestCharacterExpression(string text)
        {
            ManifestCharacterExpression Result = new ManifestCharacterExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        public static IManifestNumberExpression CreateDefaultManifestNumberExpression()
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = "0";

            return Result;
        }

        public static IManifestNumberExpression CreateSimpleManifestNumberExpression(string numberText)
        {
            ManifestNumberExpression Result = new ManifestNumberExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = numberText;

            return Result;
        }

        public static IManifestStringExpression CreateManifestStringExpression(string text)
        {
            ManifestStringExpression Result = new ManifestStringExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Text = text;

            return Result;
        }

        public static INewExpression CreateNewExpression(IQualifiedName objectName)
        {
            NewExpression Result = new NewExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Object = objectName;

            return Result;
        }

        public static IOldExpression CreateOldExpression(IQualifiedName query)
        {
            OldExpression Result = new OldExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Query = query;

            return Result;
        }

        public static IPrecursorExpression CreatePrecursorExpression(List<IArgument> argumentList)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IPrecursorExpression CreatePrecursorExpression(IBlockList<IArgument, Argument> argumentBlocks)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IPrecursorExpression CreatePrecursorExpression(IBlockList<IArgument, Argument> argumentBlocks, IObjectType ancestorType)
        {
            PrecursorExpression Result = new PrecursorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IPrecursorIndexExpression CreatePrecursorIndexExpression(List<IArgument> argumentList)
        {
            if (argumentList == null) throw new ArgumentNullException(nameof(argumentList));
            if (argumentList.Count == 0) throw new ArgumentException($"{nameof(argumentList)} must have at least one argument");

            Debug.Assert(argumentList.Count > 0);

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return Result;
        }

        public static IPrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<IArgument, Argument> argumentBlocks)
        {
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(CreateDefaultType());
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IPrecursorIndexExpression CreatePrecursorIndexExpression(IBlockList<IArgument, Argument> argumentBlocks, IObjectType ancestorType)
        {
            if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)argumentBlocks)) throw new ArgumentException($"{nameof(argumentBlocks)} must not be empty");

            PrecursorIndexExpression Result = new PrecursorIndexExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AncestorType = OptionalReferenceHelper<IObjectType>.CreateReference(ancestorType);
            Result.AncestorType.Assign();
            Result.ArgumentBlocks = argumentBlocks;

            return Result;
        }

        public static IPreprocessorExpression CreatePreprocessorExpression(PreprocessorMacro value)
        {
            PreprocessorExpression Result = new PreprocessorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Value = value;

            return Result;
        }

        public static IQueryExpression CreateQueryExpression(IQualifiedName query, List<IArgument> argumentList)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = BlockListHelper<IArgument, Argument>.CreateBlockList(argumentList);

            return SimpleQueryExpression;
        }

        public static IQueryExpression CreateQueryExpression(IQualifiedName query, IBlockList<IArgument, Argument> argumentBlocks)
        {
            QueryExpression SimpleQueryExpression = new QueryExpression();
            SimpleQueryExpression.Documentation = CreateEmptyDocumentation();
            SimpleQueryExpression.Query = query;
            SimpleQueryExpression.ArgumentBlocks = argumentBlocks;

            return SimpleQueryExpression;
        }

        public static IResultOfExpression CreateResultOfExpression(IExpression source)
        {
            ResultOfExpression Result = new ResultOfExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Source = source;

            return Result;
        }

        public static IUnaryNotExpression CreateUnaryNotExpression(IExpression rightExpression)
        {
            UnaryNotExpression Result = new UnaryNotExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RightExpression = rightExpression;

            return Result;
        }

        public static IUnaryOperatorExpression CreateUnaryOperatorExpression(IIdentifier operatorName, IExpression rightExpression)
        {
            UnaryOperatorExpression Result = new UnaryOperatorExpression();
            Result.Documentation = CreateEmptyDocumentation();
            Result.Operator = operatorName;
            Result.RightExpression = rightExpression;

            return Result;
        }
    }
}
