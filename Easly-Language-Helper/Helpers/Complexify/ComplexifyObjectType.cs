namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static void GetComplexifiedObjectType(IObjectType node, List<INode> complexifiedNodeList)
        {
            bool IsHandled = false;

            switch (node)
            {
                case IAnchoredType AsAnchoredType:
                    GetComplexifiedAnchoredType(AsAnchoredType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IFunctionType AsFunctionType:
                    GetComplexifiedFunctionType(AsFunctionType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IGenericType AsGenericType:
                    GetComplexifiedGenericType(AsGenericType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IIndexerType AsIndexerType:
                    GetComplexifiedIndexerType(AsIndexerType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IKeywordAnchoredType AsKeywordAnchoredType:
                    IsHandled = true;
                    break;

                case IProcedureType AsProcedureType:
                    GetComplexifiedProcedureType(AsProcedureType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPropertyType AsPropertyType:
                    GetComplexifiedPropertyType(AsPropertyType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case ISimpleType AsSimpleType:
                    GetComplexifiedSimpleType(AsSimpleType, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case ITupleType AsTupleType:
                    GetComplexifiedTupleType(AsTupleType, complexifiedNodeList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        private static void GetComplexifiedAnchoredType(IAnchoredType node, IList<INode> complexifiedNodeList)
        {
            if (ComplexifyQualifiedName(node.AnchoredName, out IQualifiedName ComplexifiedAnchoredName))
            {
                IAnchoredType NewAnchoredType = CreateAnchoredType(ComplexifiedAnchoredName, node.AnchorKind);
                complexifiedNodeList.Add(NewAnchoredType);
            }
        }

        private static void GetComplexifiedFunctionType(IFunctionType node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.BaseType, out List<INode> ComplexifiedBaseTypeList) && ComplexifiedBaseTypeList[0] is IObjectType AsComplexifiedBaseType)
            {
                IBlockList<IQueryOverloadType, QueryOverloadType> ClonedOverloadBlocks = (IBlockList<IQueryOverloadType, QueryOverloadType>)DeepCloneBlockList((IBlockList)node.OverloadBlocks, cloneCommentGuid: false);
                IFunctionType NewFunctionType = CreateFunctionType(AsComplexifiedBaseType, ClonedOverloadBlocks);
                complexifiedNodeList.Add(NewFunctionType);
            }
        }

        private static void GetComplexifiedGenericType(IGenericType node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedTypeArgumentBlockList(node.TypeArgumentBlocks, out IBlockList<ITypeArgument, TypeArgument> ComplexifiedTypeArgumentBlocks))
            {
                IIdentifier ClonedClassIdentifier = (IIdentifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);

                IGenericType NewGenericType = CreateGenericType(node.Sharing, ClonedClassIdentifier, ComplexifiedTypeArgumentBlocks);
                complexifiedNodeList.Add(NewGenericType);
            }
        }

        private static void GetComplexifiedIndexerType(IIndexerType node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.BaseType, out List<INode> ComplexifiedBaseTypeList) && ComplexifiedBaseTypeList[0] is IObjectType AsComplexifiedBaseType)
            {
                IObjectType ClonedEntityType = (IObjectType)DeepCloneNode(node.EntityType, cloneCommentGuid: false);
                IBlockList<IEntityDeclaration, EntityDeclaration> ClonedIndexParameterBlocks = (IBlockList<IEntityDeclaration, EntityDeclaration>)DeepCloneBlockList((IBlockList)node.IndexParameterBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedGetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetRequireBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedSetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                IIndexerType NewIndexerType = CreateIndexerType(AsComplexifiedBaseType, ClonedEntityType, ClonedIndexParameterBlocks, node.ParameterEnd, node.IndexerKind, ClonedGetRequireBlocks, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetEnsureBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedNodeList.Add(NewIndexerType);
            }
            else if (GetComplexifiedNode(node.EntityType, out List<INode> ComplexifiedEntityTypeList) && ComplexifiedEntityTypeList[0] is IObjectType AsComplexifiedEntityType)
            {
                IObjectType ClonedBaseType = (IObjectType)DeepCloneNode(node.BaseType, cloneCommentGuid: false);
                IBlockList<IEntityDeclaration, EntityDeclaration> ClonedIndexParameterBlocks = (IBlockList<IEntityDeclaration, EntityDeclaration>)DeepCloneBlockList((IBlockList)node.IndexParameterBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedGetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetRequireBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedSetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                IIndexerType NewIndexerType = CreateIndexerType(ClonedBaseType, AsComplexifiedEntityType, ClonedIndexParameterBlocks, node.ParameterEnd, node.IndexerKind, ClonedGetRequireBlocks, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetEnsureBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedNodeList.Add(NewIndexerType);
            }
        }

        private static void GetComplexifiedProcedureType(IProcedureType node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.BaseType, out List<INode> ComplexifiedBaseTypeList) && ComplexifiedBaseTypeList[0] is IObjectType AsComplexifiedBaseType)
            {
                IBlockList<ICommandOverloadType, CommandOverloadType> ClonedOverloadBlocks = (IBlockList<ICommandOverloadType, CommandOverloadType>)DeepCloneBlockList((IBlockList)node.OverloadBlocks, cloneCommentGuid: false);
                IProcedureType NewProcedureType = CreateProcedureType(AsComplexifiedBaseType, ClonedOverloadBlocks);
                complexifiedNodeList.Add(NewProcedureType);
            }
        }

        private static void GetComplexifiedPropertyType(IPropertyType node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.BaseType, out List<INode> ComplexifiedBaseTypeList) && ComplexifiedBaseTypeList[0] is IObjectType AsComplexifiedBaseType)
            {
                IObjectType ClonedEntityType = (IObjectType)DeepCloneNode(node.EntityType, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                IPropertyType NewPropertyType = CreatePropertyType(AsComplexifiedBaseType, ClonedEntityType, node.PropertyKind, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedNodeList.Add(NewPropertyType);
            }
            else if (GetComplexifiedNode(node.EntityType, out List<INode> ComplexifiedEntityTypeList) && ComplexifiedEntityTypeList[0] is IObjectType AsComplexifiedEntityType)
            {
                IObjectType ClonedBaseType = (IObjectType)DeepCloneNode(node.BaseType, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                IPropertyType NewPropertyType = CreatePropertyType(ClonedBaseType, AsComplexifiedEntityType, node.PropertyKind, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedNodeList.Add(NewPropertyType);
            }
        }

        private static void GetComplexifiedSimpleType(ISimpleType node, IList<INode> complexifiedNodeList)
        {
            if (ComplexifyAsAnchoredType(node, out IAnchoredType ComplexifiedAnchoredType, out IKeywordAnchoredType ComplexifiedKeywordAnchoredType))
            {
                complexifiedNodeList.Add(ComplexifiedAnchoredType);
                if (ComplexifiedKeywordAnchoredType != null)
                    complexifiedNodeList.Add(ComplexifiedKeywordAnchoredType);
            }
            else if (ComplexifyAsFunctionType(node, out IFunctionType ComplexifiedFunctionType))
                complexifiedNodeList.Add(ComplexifiedFunctionType);
            else if (ComplexifyAsGenericType(node, out IGenericType ComplexifiedGenericType))
                complexifiedNodeList.Add(ComplexifiedGenericType);
            else if (ComplexifyAsIndexerType(node, out IIndexerType ComplexifiedIndexerType))
                complexifiedNodeList.Add(ComplexifiedIndexerType);
            else if (ComplexifyAsPropertyType(node, out IPropertyType ComplexifiedPropertyType))
                complexifiedNodeList.Add(ComplexifiedPropertyType);
            else if (ComplexifyAsProcedureType(node, out IProcedureType ComplexifiedProcedureType))
                complexifiedNodeList.Add(ComplexifiedProcedureType);
            else if (ComplexifyAsTupleType(node, out ITupleType ComplexifiedTupleType))
                complexifiedNodeList.Add(ComplexifiedTupleType);
        }

        private static bool ComplexifyAsAnchoredType(ISimpleType node, out IAnchoredType complexifiedNode, out IKeywordAnchoredType complexifiedKeywordNode)
        {
            complexifiedNode = null;
            complexifiedKeywordNode = null;

            string ClassIdentifierText = node.ClassIdentifier.Text;

            if (ClassIdentifierText.StartsWith("like "))
            {
                string Text = ClassIdentifierText.Substring(5).Trim();
                IQualifiedName AnchoredName = StringToQualifiedName(Text);
                complexifiedNode = CreateAnchoredType(AnchoredName, AnchorKinds.Declaration);

                if (StringToKeyword(Text, out Keyword Value))
                    complexifiedKeywordNode = CreateKeywordAnchoredType(Value);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsFunctionType(ISimpleType node, out IFunctionType complexifiedNode)
        {
            complexifiedNode = null;

            string Text = node.ClassIdentifier.Text;

            if (Text.StartsWith("function "))
            {
                ISimpleType BaseType = CreateSimpleSimpleType(Text.Substring(9));
                ISimpleType ReturnType = CreateEmptySimpleType();
                complexifiedNode = CreateFunctionType(BaseType, ReturnType);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsGenericType(ISimpleType node, out IGenericType complexifiedNode)
        {
            complexifiedNode = null;

            string Text = node.ClassIdentifier.Text;
            int GenericBeginIndex = Text.IndexOf("[");

            if (GenericBeginIndex >= 0 && Text.EndsWith("]"))
            {
                IIdentifier ClassIdentifier = CreateSimpleIdentifier(Text.Substring(0, GenericBeginIndex));
                ISimpleType TypeSource = CreateSimpleSimpleType(Text.Substring(GenericBeginIndex + 1, Text.Length - GenericBeginIndex - 2));
                ITypeArgument TypeArgument = CreatePositionalTypeArgument(TypeSource);

                complexifiedNode = CreateGenericType(ClassIdentifier, new List<ITypeArgument>() { TypeArgument });
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsIndexerType(ISimpleType node, out IIndexerType complexifiedNode)
        {
            complexifiedNode = null;

            string Text = node.ClassIdentifier.Text;

            if (Text.StartsWith("indexer "))
            {
                ISimpleType BaseType = CreateSimpleSimpleType(Text.Substring(8));
                ISimpleType EntityType = CreateEmptySimpleType();
                IName ParameterName = CreateEmptyName();
                ISimpleType ParameterType = CreateEmptySimpleType();
                IEntityDeclaration Parameter = CreateEntityDeclaration(ParameterName, ParameterType);
                complexifiedNode = CreateIndexerType(BaseType, EntityType, Parameter);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsPropertyType(ISimpleType node, out IPropertyType complexifiedNode)
        {
            complexifiedNode = null;

            string Text = node.ClassIdentifier.Text;

            if (Text.StartsWith("property "))
            {
                ISimpleType BaseType = CreateSimpleSimpleType(Text.Substring(9));
                ISimpleType EntityType = CreateEmptySimpleType();
                complexifiedNode = CreatePropertyType(BaseType, EntityType);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsProcedureType(ISimpleType node, out IProcedureType complexifiedNode)
        {
            complexifiedNode = null;

            string Text = node.ClassIdentifier.Text;

            if (Text.StartsWith("procedure "))
            {
                ISimpleType BaseType = CreateSimpleSimpleType(Text.Substring(10));
                complexifiedNode = CreateProcedureType(BaseType);
            }

            return complexifiedNode != null;
        }

        private static bool ComplexifyAsTupleType(ISimpleType node, out ITupleType complexifiedNode)
        {
            complexifiedNode = null;

            string Text = node.ClassIdentifier.Text;

            if (Text.StartsWith("tuple "))
            {
                IName EntityName = CreateEmptyName();
                ISimpleType EntityType = CreateSimpleSimpleType(Text.Substring(6));
                IEntityDeclaration Entity = CreateEntityDeclaration(EntityName, EntityType);
                complexifiedNode = CreateTupleType(Entity);
            }

            return complexifiedNode != null;
        }

        private static void GetComplexifiedTupleType(ITupleType node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedEntityDeclarationBlockList(node.EntityDeclarationBlocks, out IBlockList<IEntityDeclaration, EntityDeclaration> ComplexifiedEntityDeclarationBlocks))
            {
                ITupleType NewTupleType = CreateTupleType(node.Sharing, ComplexifiedEntityDeclarationBlocks);
                complexifiedNodeList.Add(NewTupleType);
            }
        }
    }
}
