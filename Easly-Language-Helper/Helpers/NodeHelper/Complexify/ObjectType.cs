namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    private static bool GetComplexifiedObjectType(ObjectType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        Contract.Unused(out complexifiedObjectTypeList);

        bool Result = false;
        bool IsHandled = false;

        switch (node)
        {
            case AnchoredType AsAnchoredType:
                Result = GetComplexifiedAnchoredType(AsAnchoredType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case FunctionType AsFunctionType:
                Result = GetComplexifiedFunctionType(AsFunctionType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case GenericType AsGenericType:
                Result = GetComplexifiedGenericType(AsGenericType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case IndexerType AsIndexerType:
                Result = GetComplexifiedIndexerType(AsIndexerType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case KeywordAnchoredType AsKeywordAnchoredType:
                IsHandled = true;
                break;

            case ProcedureType AsProcedureType:
                Result = GetComplexifiedProcedureType(AsProcedureType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case PropertyType AsPropertyType:
                Result = GetComplexifiedPropertyType(AsPropertyType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case SimpleType AsSimpleType:
                Result = GetComplexifiedSimpleType(AsSimpleType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;

            case TupleType AsTupleType:
                Result = GetComplexifiedTupleType(AsTupleType, out complexifiedObjectTypeList);
                IsHandled = true;
                break;
        }

        Debug.Assert(IsHandled, $"All descendants of {nameof(ObjectType)} have been handled");

        return Result;
    }

    private static bool GetComplexifiedAnchoredType(AnchoredType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (ComplexifyQualifiedName(node.AnchoredName, out QualifiedName ComplexifiedAnchoredName))
        {
            AnchoredType NewAnchoredType = CreateAnchoredType(ComplexifiedAnchoredName, node.AnchorKind);
            complexifiedObjectTypeList = new List<ObjectType>() { NewAnchoredType };
            return true;
        }
        else if (node.AnchoredName.Path.Count == 1 && StringToKeyword(node.AnchoredName.Path[0].Text, out Keyword Value))
        {
            KeywordAnchoredType NewKeywordAnchoredType = CreateKeywordAnchoredType(Value);
            complexifiedObjectTypeList = new List<ObjectType>() { NewKeywordAnchoredType };
            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }

    private static bool GetComplexifiedFunctionType(FunctionType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (GetComplexifiedObjectType(node.BaseType, out IList<ObjectType> ComplexifiedBaseTypeList))
        {
            complexifiedObjectTypeList = new List<ObjectType>();

            foreach (ObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
            {
                IBlockList<QueryOverloadType> ClonedOverloadBlocks = (IBlockList<QueryOverloadType>)DeepCloneBlockList((IBlockList)node.OverloadBlocks, cloneCommentGuid: false);
                FunctionType NewFunctionType = CreateFunctionType(ComplexifiedBaseType, ClonedOverloadBlocks);
                complexifiedObjectTypeList.Add(NewFunctionType);
            }

            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }

    private static bool GetComplexifiedGenericType(GenericType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (GetComplexifiedTypeArgumentBlockList(node.TypeArgumentBlocks, out IBlockList<TypeArgument> ComplexifiedTypeArgumentBlocks))
        {
            Identifier ClonedClassIdentifier = (Identifier)DeepCloneNode(node.ClassIdentifier, cloneCommentGuid: false);

            GenericType NewGenericType = CreateGenericType(node.Sharing, ClonedClassIdentifier, ComplexifiedTypeArgumentBlocks);
            complexifiedObjectTypeList = new List<ObjectType>() { NewGenericType };
            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }

    private static bool GetComplexifiedIndexerType(IndexerType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (GetComplexifiedObjectType(node.BaseType, out IList<ObjectType> ComplexifiedBaseTypeList))
        {
            complexifiedObjectTypeList = new List<ObjectType>();

            foreach (ObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
            {
                ObjectType ClonedEntityType = (ObjectType)DeepCloneNode(node.EntityType, cloneCommentGuid: false);
                IBlockList<EntityDeclaration> ClonedIndexParameterBlocks = (IBlockList<EntityDeclaration>)DeepCloneBlockList((IBlockList)node.IndexParameterBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedGetRequireBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.GetRequireBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedGetEnsureBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedSetRequireBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedSetEnsureBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.SetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                IndexerType NewIndexerType = CreateIndexerType(ComplexifiedBaseType, ClonedEntityType, ClonedIndexParameterBlocks, node.ParameterEnd, node.IndexerKind, ClonedGetRequireBlocks, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetEnsureBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedObjectTypeList.Add(NewIndexerType);
            }

            return true;
        }
        else if (GetComplexifiedObjectType(node.EntityType, out IList<ObjectType> ComplexifiedEntityTypeList))
        {
            complexifiedObjectTypeList = new List<ObjectType>();

            foreach (ObjectType ComplexifiedEntityType in ComplexifiedEntityTypeList)
            {
                ObjectType ClonedBaseType = (ObjectType)DeepCloneNode(node.BaseType, cloneCommentGuid: false);
                IBlockList<EntityDeclaration> ClonedIndexParameterBlocks = (IBlockList<EntityDeclaration>)DeepCloneBlockList((IBlockList)node.IndexParameterBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedGetRequireBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.GetRequireBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedGetEnsureBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedSetRequireBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedSetEnsureBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.SetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                IndexerType NewIndexerType = CreateIndexerType(ClonedBaseType, ComplexifiedEntityType, ClonedIndexParameterBlocks, node.ParameterEnd, node.IndexerKind, ClonedGetRequireBlocks, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetEnsureBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedObjectTypeList.Add(NewIndexerType);
            }

            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }

    private static bool GetComplexifiedProcedureType(ProcedureType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (GetComplexifiedObjectType(node.BaseType, out IList<ObjectType> ComplexifiedBaseTypeList))
        {
            complexifiedObjectTypeList = new List<ObjectType>();

            foreach (ObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
            {
                IBlockList<CommandOverloadType> ClonedOverloadBlocks = (IBlockList<CommandOverloadType>)DeepCloneBlockList((IBlockList)node.OverloadBlocks, cloneCommentGuid: false);
                ProcedureType NewProcedureType = CreateProcedureType(ComplexifiedBaseType, ClonedOverloadBlocks);
                complexifiedObjectTypeList.Add(NewProcedureType);
            }

            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }

    private static bool GetComplexifiedPropertyType(PropertyType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (GetComplexifiedObjectType(node.BaseType, out IList<ObjectType> ComplexifiedBaseTypeList))
        {
            complexifiedObjectTypeList = new List<ObjectType>();

            foreach (ObjectType ComplexifiedBaseType in ComplexifiedBaseTypeList)
            {
                ObjectType ClonedEntityType = (ObjectType)DeepCloneNode(node.EntityType, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedGetEnsureBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedSetRequireBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                PropertyType NewPropertyType = CreatePropertyType(ComplexifiedBaseType, ClonedEntityType, node.PropertyKind, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedObjectTypeList.Add(NewPropertyType);
            }

            return true;
        }
        else if (GetComplexifiedObjectType(node.EntityType, out IList<ObjectType> ComplexifiedEntityTypeList))
        {
            complexifiedObjectTypeList = new List<ObjectType>();

            foreach (ObjectType ComplexifiedEntityType in ComplexifiedEntityTypeList)
            {
                ObjectType ClonedBaseType = (ObjectType)DeepCloneNode(node.BaseType, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedGetEnsureBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.GetEnsureBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedGetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.GetExceptionIdentifierBlocks, cloneCommentGuid: false);
                IBlockList<Assertion> ClonedSetRequireBlocks = (IBlockList<Assertion>)DeepCloneBlockList((IBlockList)node.SetRequireBlocks, cloneCommentGuid: false);
                IBlockList<Identifier> ClonedSetExceptionIdentifierBlocks = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)node.SetExceptionIdentifierBlocks, cloneCommentGuid: false);

                PropertyType NewPropertyType = CreatePropertyType(ClonedBaseType, ComplexifiedEntityType, node.PropertyKind, ClonedGetEnsureBlocks, ClonedGetExceptionIdentifierBlocks, ClonedSetRequireBlocks, ClonedSetExceptionIdentifierBlocks);
                complexifiedObjectTypeList.Add(NewPropertyType);
            }

            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }

    private static bool GetComplexifiedSimpleType(SimpleType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (ComplexifyAsAnchoredType(node, out AnchoredType ComplexifiedAnchoredType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedAnchoredType };
        else if (ComplexifyAsFunctionType(node, out FunctionType ComplexifiedFunctionType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedFunctionType };
        else if (ComplexifyAsGenericType(node, out GenericType ComplexifiedGenericType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedGenericType };
        else if (ComplexifyAsIndexerType(node, out IndexerType ComplexifiedIndexerType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedIndexerType };
        else if (ComplexifyAsPropertyType(node, out PropertyType ComplexifiedPropertyType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedPropertyType };
        else if (ComplexifyAsProcedureType(node, out ProcedureType ComplexifiedProcedureType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedProcedureType };
        else if (ComplexifyAsTupleType(node, out TupleType ComplexifiedTupleType))
            complexifiedObjectTypeList = new List<ObjectType>() { ComplexifiedTupleType };
        else
        {
            Contract.Unused(out complexifiedObjectTypeList);
            return false;
        }

        return true;
    }

    private static bool ComplexifyAsAnchoredType(SimpleType node, out AnchoredType complexifiedNode)
    {
        string ClassIdentifierText = node.ClassIdentifier.Text;

        if (ClassIdentifierText.StartsWith("like", StringComparison.InvariantCulture))
        {
            string Text = ClassIdentifierText.Substring(4).Trim();
            QualifiedName AnchoredName = CreateSimpleQualifiedName(Text);
            complexifiedNode = CreateAnchoredType(AnchoredName, AnchorKinds.Declaration);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsFunctionType(SimpleType node, out FunctionType complexifiedNode)
    {
        string Text = node.ClassIdentifier.Text;

        if (Text.StartsWith("function ", StringComparison.InvariantCulture))
        {
            SimpleType BaseType = CreateSimpleSimpleType(Text.Substring(9));
            SimpleType ReturnType = CreateEmptySimpleType();

            complexifiedNode = CreateFunctionType(BaseType, ReturnType);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsGenericType(SimpleType node, out GenericType complexifiedNode)
    {
        string Text = node.ClassIdentifier.Text;
        int GenericBeginIndex = Text.IndexOf("[", StringComparison.InvariantCulture);

        if (GenericBeginIndex >= 0 && Text.EndsWith("]", StringComparison.InvariantCulture))
        {
            Identifier ClassIdentifier = CreateSimpleIdentifier(Text.Substring(0, GenericBeginIndex));
            SimpleType TypeSource = CreateSimpleSimpleType(Text.Substring(GenericBeginIndex + 1, Text.Length - GenericBeginIndex - 2));
            TypeArgument TypeArgument = CreatePositionalTypeArgument(TypeSource);

            complexifiedNode = CreateGenericType(ClassIdentifier, new List<TypeArgument>() { TypeArgument });
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsIndexerType(SimpleType node, out IndexerType complexifiedNode)
    {
        string Text = node.ClassIdentifier.Text;

        if (Text.StartsWith("indexer ", StringComparison.InvariantCulture))
        {
            SimpleType BaseType = CreateSimpleSimpleType(Text.Substring(8));
            SimpleType EntityType = CreateEmptySimpleType();
            Name ParameterName = CreateEmptyName();
            SimpleType ParameterType = CreateEmptySimpleType();
            EntityDeclaration Parameter = CreateEntityDeclaration(ParameterName, ParameterType);

            complexifiedNode = CreateIndexerType(BaseType, EntityType, Parameter);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsPropertyType(SimpleType node, out PropertyType complexifiedNode)
    {
        string Text = node.ClassIdentifier.Text;

        if (Text.StartsWith("property ", StringComparison.InvariantCulture))
        {
            SimpleType BaseType = CreateSimpleSimpleType(Text.Substring(9));
            SimpleType EntityType = CreateEmptySimpleType();

            complexifiedNode = CreatePropertyType(BaseType, EntityType);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsProcedureType(SimpleType node, out ProcedureType complexifiedNode)
    {
        string Text = node.ClassIdentifier.Text;

        if (Text.StartsWith("procedure ", StringComparison.InvariantCulture))
        {
            SimpleType BaseType = CreateSimpleSimpleType(Text.Substring(10));

            complexifiedNode = CreateProcedureType(BaseType);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool ComplexifyAsTupleType(SimpleType node, out TupleType complexifiedNode)
    {
        string Text = node.ClassIdentifier.Text;

        if (Text.StartsWith("tuple ", StringComparison.InvariantCulture))
        {
            Name EntityName = CreateEmptyName();
            SimpleType EntityType = CreateSimpleSimpleType(Text.Substring(6));
            EntityDeclaration Entity = CreateEntityDeclaration(EntityName, EntityType);

            complexifiedNode = CreateTupleType(Entity);
            return true;
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }

    private static bool GetComplexifiedTupleType(TupleType node, out IList<ObjectType> complexifiedObjectTypeList)
    {
        if (GetComplexifiedEntityDeclarationBlockList(node.EntityDeclarationBlocks, out IBlockList<EntityDeclaration> ComplexifiedEntityDeclarationBlocks))
        {
            TupleType NewTupleType = CreateTupleType(node.Sharing, ComplexifiedEntityDeclarationBlocks);
            complexifiedObjectTypeList = new List<ObjectType>() { NewTupleType };
            return true;
        }

        Contract.Unused(out complexifiedObjectTypeList);
        return false;
    }
}
