namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedObjectType(IObjectType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;
            bool Result = false;
            bool IsHandled = false;

            switch (node)
            {
                case IAnchoredType AsAnchoredType:
                    Result = GetComplexifiedAnchoredType(AsAnchoredType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case IFunctionType AsFunctionType:
                    Result = GetComplexifiedFunctionType(AsFunctionType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case IGenericType AsGenericType:
                    Result = GetComplexifiedGenericType(AsGenericType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case IIndexerType AsIndexerType:
                    Result = GetComplexifiedIndexerType(AsIndexerType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case IKeywordAnchoredType AsKeywordAnchoredType:
                    IsHandled = true;
                    break;

                case IProcedureType AsProcedureType:
                    Result = GetComplexifiedProcedureType(AsProcedureType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case IPropertyType AsPropertyType:
                    Result = GetComplexifiedPropertyType(AsPropertyType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case ISimpleType AsSimpleType:
                    Result = GetComplexifiedSimpleType(AsSimpleType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;

                case ITupleType AsTupleType:
                    Result = GetComplexifiedTupleType(AsTupleType, out complexifiedObjectTypeList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);

            return Result;
        }

        private static bool GetComplexifiedAnchoredType(IAnchoredType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (ComplexifyQualifiedName(node.AnchoredName, out IQualifiedName ComplexifiedAnchoredName))
            {
                IAnchoredType NewAnchoredType = CreateAnchoredType(ComplexifiedAnchoredName, node.AnchorKind);
                complexifiedObjectTypeList = new List<IObjectType>() { NewAnchoredType };
            }
            else if (node.AnchoredName.Path.Count == 1 && StringToKeyword(node.AnchoredName.Path[0].Text, out Keyword Value))
            {
                IKeywordAnchoredType NewKeywordAnchoredType = CreateKeywordAnchoredType(Value);
                complexifiedObjectTypeList = new List<IObjectType>() { NewKeywordAnchoredType };
            }

            return complexifiedObjectTypeList != null;
        }

        private static bool GetComplexifiedFunctionType(IFunctionType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (GetComplexifiedObjectType(node.BaseType, out IList<IObjectType> ComplexifiedBaseTypeList))
            {
                complexifiedObjectTypeList = new List<IObjectType>();

                foreach (IObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
                {
                    IBlockList<IQueryOverloadType, QueryOverloadType> ClonedOverloadBlocks = (IBlockList<IQueryOverloadType, QueryOverloadType>)DeepCloneBlockList((IBlockList)node.OverloadBlocks, cloneCommentGuid: false);
                    IFunctionType NewFunctionType = CreateFunctionType(ComplexifiedBaseType, ClonedOverloadBlocks);
                    complexifiedObjectTypeList.Add(NewFunctionType);
                }
            }

            return complexifiedObjectTypeList != null;
        }

        private static bool GetComplexifiedGenericType(IGenericType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (GetComplexifiedTypeArgumentBlockList(node.TypeArgumentBlocks, out IBlockList<ITypeArgument, TypeArgument> ComplexifiedTypeArgumentBlocks))
            {
                IIdentifier ClonedClassIdentifier = (IIdentifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);

                IGenericType NewGenericType = CreateGenericType(node.Sharing, ClonedClassIdentifier, ComplexifiedTypeArgumentBlocks);
                complexifiedObjectTypeList = new List<IObjectType>() { NewGenericType };
            }

            return complexifiedObjectTypeList != null;
        }

        private static bool GetComplexifiedIndexerType(IIndexerType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (GetComplexifiedObjectType(node.BaseType, out IList<IObjectType> ComplexifiedBaseTypeList))
            {
                complexifiedObjectTypeList = new List<IObjectType>();

                foreach (IObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
                {
                    IObjectType ClonedEntityType = (IObjectType)DeepCloneNode(node.EntityType, cloneCommentGuid: false);
                    IBlockList<IEntityDeclaration, EntityDeclaration> ClonedIndexParameterBlocks = (IBlockList<IEntityDeclaration, EntityDeclaration>)DeepCloneBlockList((IBlockList)node.IndexParameterBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedGetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetRequireBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedSetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetEnsureBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                    IIndexerType NewIndexerType = CreateIndexerType(ComplexifiedBaseType, ClonedEntityType, ClonedIndexParameterBlocks, node.ParameterEnd, node.IndexerKind, ClonedGetRequireBlocks, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetEnsureBlocks, ClonedSetExceptionIdentifierBlocks);
                    complexifiedObjectTypeList.Add(NewIndexerType);
                }
            }
            else if (GetComplexifiedObjectType(node.EntityType, out IList<IObjectType> ComplexifiedEntityTypeList))
            {
                complexifiedObjectTypeList = new List<IObjectType>();

                foreach (IObjectType ComplexifiedEntityType in ComplexifiedEntityTypeList)
                {
                    IObjectType ClonedBaseType = (IObjectType)DeepCloneNode(node.BaseType, cloneCommentGuid: false);
                    IBlockList<IEntityDeclaration, EntityDeclaration> ClonedIndexParameterBlocks = (IBlockList<IEntityDeclaration, EntityDeclaration>)DeepCloneBlockList((IBlockList)node.IndexParameterBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedGetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetRequireBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedSetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetEnsureBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                    IIndexerType NewIndexerType = CreateIndexerType(ClonedBaseType, ComplexifiedEntityType, ClonedIndexParameterBlocks, node.ParameterEnd, node.IndexerKind, ClonedGetRequireBlocks, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetEnsureBlocks, ClonedSetExceptionIdentifierBlocks);
                    complexifiedObjectTypeList.Add(NewIndexerType);
                }
            }

            return complexifiedObjectTypeList != null;
        }

        private static bool GetComplexifiedProcedureType(IProcedureType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (GetComplexifiedObjectType(node.BaseType, out IList<IObjectType> ComplexifiedBaseTypeList))
            {
                complexifiedObjectTypeList = new List<IObjectType>();

                foreach (IObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
                {
                    IBlockList<ICommandOverloadType, CommandOverloadType> ClonedOverloadBlocks = (IBlockList<ICommandOverloadType, CommandOverloadType>)DeepCloneBlockList((IBlockList)node.OverloadBlocks, cloneCommentGuid: false);
                    IProcedureType NewProcedureType = CreateProcedureType(ComplexifiedBaseType, ClonedOverloadBlocks);
                    complexifiedObjectTypeList.Add(NewProcedureType);
                }
            }

            return complexifiedObjectTypeList != null;
        }

        private static bool GetComplexifiedPropertyType(IPropertyType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (GetComplexifiedObjectType(node.BaseType, out IList<IObjectType> ComplexifiedBaseTypeList))
            {
                complexifiedObjectTypeList = new List<IObjectType>();

                foreach (IObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
                {
                    IObjectType ClonedEntityType = (IObjectType)DeepCloneNode(node.EntityType, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                    IPropertyType NewPropertyType = CreatePropertyType(ComplexifiedBaseType, ClonedEntityType, node.PropertyKind, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetExceptionIdentifierBlocks);
                    complexifiedObjectTypeList.Add(NewPropertyType);
                }
            }
            else if (GetComplexifiedObjectType(node.EntityType, out IList<IObjectType> ComplexifiedEntityTypeList))
            {
                complexifiedObjectTypeList = new List<IObjectType>();

                foreach (IObjectType ComplexifiedEntityType in ComplexifiedEntityTypeList)
                {
                    IObjectType ClonedBaseType = (IObjectType)DeepCloneNode(node.BaseType, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedGetEnsureBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                    IBlockList<IAssertion, Assertion> ClonedSetRequireBlocks = (IBlockList<IAssertion, Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                    IBlockList<IIdentifier, Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                    IPropertyType NewPropertyType = CreatePropertyType(ClonedBaseType, ComplexifiedEntityType, node.PropertyKind, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetExceptionIdentifierBlocks);
                    complexifiedObjectTypeList.Add(NewPropertyType);
                }
            }

            return complexifiedObjectTypeList != null;
        }

        private static bool GetComplexifiedSimpleType(ISimpleType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (ComplexifyAsAnchoredType(node, out IAnchoredType ComplexifiedAnchoredType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedAnchoredType };
            else if (ComplexifyAsFunctionType(node, out IFunctionType ComplexifiedFunctionType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedFunctionType };
            else if (ComplexifyAsGenericType(node, out IGenericType ComplexifiedGenericType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedGenericType };
            else if (ComplexifyAsIndexerType(node, out IIndexerType ComplexifiedIndexerType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedIndexerType };
            else if (ComplexifyAsPropertyType(node, out IPropertyType ComplexifiedPropertyType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedPropertyType };
            else if (ComplexifyAsProcedureType(node, out IProcedureType ComplexifiedProcedureType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedProcedureType };
            else if (ComplexifyAsTupleType(node, out ITupleType ComplexifiedTupleType))
                complexifiedObjectTypeList = new List<IObjectType>() { ComplexifiedTupleType };

            return complexifiedObjectTypeList != null;
        }

        private static bool ComplexifyAsAnchoredType(ISimpleType node, out IAnchoredType complexifiedNode)
        {
            complexifiedNode = null;

            string ClassIdentifierText = node.ClassIdentifier.Text;

            if (ClassIdentifierText.StartsWith("like"))
            {
                string Text = ClassIdentifierText.Substring(4).Trim();
                IQualifiedName AnchoredName = CreateSimpleQualifiedName(Text);
                complexifiedNode = CreateAnchoredType(AnchoredName, AnchorKinds.Declaration);
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

        private static bool GetComplexifiedTupleType(ITupleType node, out IList<IObjectType> complexifiedObjectTypeList)
        {
            complexifiedObjectTypeList = null;

            if (GetComplexifiedEntityDeclarationBlockList(node.EntityDeclarationBlocks, out IBlockList<IEntityDeclaration, EntityDeclaration> ComplexifiedEntityDeclarationBlocks))
            {
                ITupleType NewTupleType = CreateTupleType(node.Sharing, ComplexifiedEntityDeclarationBlocks);
                complexifiedObjectTypeList = new List<IObjectType>() { NewTupleType };
            }

            return complexifiedObjectTypeList != null;
        }
    }
}
