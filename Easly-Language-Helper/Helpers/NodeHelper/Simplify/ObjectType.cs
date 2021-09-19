#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        public static bool GetSimplifiedObjectType(ObjectType nodeObjectType, out Node simplifiedNode)
        {
            switch (nodeObjectType)
            {
                case AnchoredType AsAnchoredType:
                    return SimplifyAnchoredType(AsAnchoredType, out simplifiedNode);
                case KeywordAnchoredType AsKeywordAnchoredType:
                    return SimplifyKeywordAnchoredType(AsKeywordAnchoredType, out simplifiedNode);
                case FunctionType AsFunctionType:
                    return SimplifyFunctionType(AsFunctionType, out simplifiedNode);
                case GenericType AsGenericType:
                    return SimplifyGenericType(AsGenericType, out simplifiedNode);
                case IndexerType AsIndexerType:
                    return SimplifyIndexerType(AsIndexerType, out simplifiedNode);
                case PropertyType AsPropertyType:
                    return SimplifyPropertyType(AsPropertyType, out simplifiedNode);
                case ProcedureType AsProcedureType:
                    return SimplifyProcedureType(AsProcedureType, out simplifiedNode);
                case TupleType AsTupleType:
                    return SimplifyTupleType(AsTupleType, out simplifiedNode);
                default:
                    simplifiedNode = null;
                    return false;
            }
        }

        private static bool SimplifyAnchoredType(AnchoredType node, out Node simplifiedNode)
        {
            Debug.Assert(node.AnchoredName.Path.Count > 0, "The path of an anchor is never empty");
            simplifiedNode = CreateSimpleSimpleType(node.AnchoredName.Path[0].Text);
            return true;
        }

        private static bool SimplifyKeywordAnchoredType(KeywordAnchoredType node, out Node simplifiedNode)
        {
            simplifiedNode = CreateSimpleSimpleType(node.Anchor.ToString());
            return true;
        }

        private static bool SimplifyFunctionType(FunctionType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyGenericType(GenericType node, out Node simplifiedNode)
        {
            string Text = node.ClassIdentifier.Text;

            if (BlockListHelper<TypeArgument>.IsSimple(node.TypeArgumentBlocks))
            {
                TypeArgument FirstArgument = node.TypeArgumentBlocks.NodeBlockList[0].NodeList[0];
                if (FirstArgument is PositionalTypeArgument AsPositionalTypeArgument && AsPositionalTypeArgument.Source is SimpleType AsSimpleType)
                {
                    Text += AsSimpleType.ClassIdentifier.Text;
                }
            }

            simplifiedNode = CreateSimpleSimpleType(Text);
            return true;
        }

        private static bool SimplifyIndexerType(IndexerType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyPropertyType(PropertyType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyProcedureType(ProcedureType node, out Node simplifiedNode)
        {
            simplifiedNode = DeepCloneNode(node.BaseType, cloneCommentGuid: false) as ObjectType;
            return true;
        }

        private static bool SimplifyTupleType(TupleType node, out Node simplifiedNode)
        {
            EntityDeclaration FirstField = node.EntityDeclarationBlocks.NodeBlockList[0].NodeList[0];
            simplifiedNode = DeepCloneNode(FirstField.EntityType, cloneCommentGuid: false) as ObjectType;
            return true;
        }
    }
}
