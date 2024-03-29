﻿namespace BaseNodeHelper;

using System.Collections.Generic;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    private static bool GetComplexifiedIdentifierBlockList(IBlockList<Identifier> identifierBlockList, out IBlockList<Identifier> newBlockList)
    {
        for (int BlockIndex = 0; BlockIndex < identifierBlockList.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<Identifier> Block = identifierBlockList.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                Identifier Identifier = Block.NodeList[NodeIndex];
                if (SplitIdentifier(Identifier, ',', ',', out IList<Identifier> Split))
                {
                    newBlockList = (IBlockList<Identifier>)DeepCloneBlockListInternal((IBlockList)identifierBlockList, cloneCommentGuid: false);

                    newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                    for (int i = 0; i < Split.Count; i++)
                        newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                    return true;
                }
            }
        }

        Contract.Unused(out newBlockList);
        return false;
    }

    private static bool GetComplexifiedNameBlockList(IBlockList<Name> nameBlockList, out IBlockList<Name> newBlockList)
    {
        for (int BlockIndex = 0; BlockIndex < nameBlockList.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<Name> Block = nameBlockList.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                Name Name = Block.NodeList[NodeIndex];
                if (SplitName(Name, ',', ',', out IList<Name> Split))
                {
                    newBlockList = (IBlockList<Name>)DeepCloneBlockListInternal((IBlockList)nameBlockList, cloneCommentGuid: false);

                    newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                    for (int i = 0; i < Split.Count; i++)
                        newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                    return true;
                }
            }
        }

        Contract.Unused(out newBlockList);
        return false;
    }

    private static bool SplitName(Name name, char startTag, char endTag, out IList<Name> split)
    {
        IList<string> SplitList = SplitString(name.Text, startTag, endTag);

        if (SplitList.Count > 1)
        {
            split = new List<Name>();

            foreach (string Item in SplitList)
            {
                Name NewName = CreateSimpleName(Item.Trim());
                split.Add(NewName);
            }

            return true;
        }

        Contract.Unused(out split);
        return false;
    }

    private static bool GetComplexifiedArgumentBlockList(IBlockList<Argument> argumentBlocks, out IBlockList<Argument> newArgumentBlocks)
    {
        for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<Argument> Block = argumentBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                Argument Argument = Block.NodeList[NodeIndex];

                if (SplitArgument(Argument, out IList<Argument> SplitArgumentList))
                {
                    newArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockListInternal((IBlockList)argumentBlocks, cloneCommentGuid: false);

                    Block = newArgumentBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList.RemoveAt(NodeIndex);

                    for (int i = 0; i < SplitArgumentList.Count; i++)
                        Block.NodeList.Insert(NodeIndex + i, SplitArgumentList[i]);
                    return true;
                }
            }
        }

        for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<Argument> Block = argumentBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                Argument Argument = Block.NodeList[NodeIndex];

                if (GetComplexifiedArgument(Argument, out IList<Argument> ComplexifiedArgumentList))
                {
                    Argument ComplexifiedArgument = ComplexifiedArgumentList[0];
                    newArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockListInternal((IBlockList)argumentBlocks, cloneCommentGuid: false);

                    Block = newArgumentBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList[NodeIndex] = ComplexifiedArgument;
                    return true;
                }
            }
        }

        Contract.Unused(out newArgumentBlocks);
        return false;
    }

    private static bool GetComplexifiedAssignmentArgumentBlockList(IBlockList<AssignmentArgument> argumentBlocks, out IBlockList<AssignmentArgument> newAssignmentArgumentBlocks)
    {
        for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<AssignmentArgument> Block = argumentBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                AssignmentArgument AssignmentArgument = Block.NodeList[NodeIndex];

                if (GetComplexifiedAssignmentArgument(AssignmentArgument, out IList<Argument> ComplexifiedAssignmentArgumentList))
                {
                    AssignmentArgument ComplexifiedAssignmentArgument = (AssignmentArgument)SafeList.ItemAt<Argument>(ComplexifiedAssignmentArgumentList, 0);

                    newAssignmentArgumentBlocks = (IBlockList<AssignmentArgument>)DeepCloneBlockListInternal((IBlockList)argumentBlocks, cloneCommentGuid: false);

                    Block = newAssignmentArgumentBlocks.NodeBlockList[BlockIndex];
                    IList<AssignmentArgument> NodeList = Block.NodeList;

                    SafeList.SetAt<AssignmentArgument>(NodeList, NodeIndex, ComplexifiedAssignmentArgument);
                    return true;
                }
            }
        }

        Contract.Unused(out newAssignmentArgumentBlocks);
        return false;
    }

    private static bool GetComplexifiedQualifiedNameBlockList(IBlockList<QualifiedName> argumentBlocks, out IBlockList<QualifiedName> newQualifiedNameBlocks)
    {
        for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<QualifiedName> Block = argumentBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                QualifiedName QualifiedName = Block.NodeList[NodeIndex];

                if (GetComplexifiedQualifiedName(QualifiedName, out IList<QualifiedName> ComplexifiedQualifiedNameList))
                {
                    QualifiedName ComplexifiedQualifiedName = ComplexifiedQualifiedNameList[0];
                    newQualifiedNameBlocks = (IBlockList<QualifiedName>)DeepCloneBlockListInternal((IBlockList)argumentBlocks, cloneCommentGuid: false);

                    Block = newQualifiedNameBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList[NodeIndex] = ComplexifiedQualifiedName;
                    return true;
                }
            }
        }

        Contract.Unused(out newQualifiedNameBlocks);
        return false;
    }

    private static bool GetComplexifiedEntityDeclarationBlockList(IBlockList<EntityDeclaration> identifierBlockList, out IBlockList<EntityDeclaration> newBlockList)
    {
        for (int BlockIndex = 0; BlockIndex < identifierBlockList.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<EntityDeclaration> Block = identifierBlockList.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                EntityDeclaration EntityDeclaration = Block.NodeList[NodeIndex];
                if (SplitEntityDeclaration(EntityDeclaration, out IList<EntityDeclaration> Split))
                {
                    newBlockList = (IBlockList<EntityDeclaration>)DeepCloneBlockListInternal((IBlockList)identifierBlockList, cloneCommentGuid: false);

                    newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                    for (int i = 0; i < Split.Count; i++)
                        newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                    return true;
                }
            }
        }

        Contract.Unused(out newBlockList);
        return false;
    }

    private static bool GetComplexifiedTypeArgumentBlockList(IBlockList<TypeArgument> typeArgumentBlocks, out IBlockList<TypeArgument> newTypeArgumentBlocks)
    {
        for (int BlockIndex = 0; BlockIndex < typeArgumentBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<TypeArgument> Block = typeArgumentBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                TypeArgument TypeArgument = Block.NodeList[NodeIndex];

                if (SplitTypeArgument(TypeArgument, out IList<TypeArgument> SplitTypeArgumentList))
                {
                    newTypeArgumentBlocks = (IBlockList<TypeArgument>)DeepCloneBlockListInternal((IBlockList)typeArgumentBlocks, cloneCommentGuid: false);

                    Block = newTypeArgumentBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList.RemoveAt(NodeIndex);

                    for (int i = 0; i < SplitTypeArgumentList.Count; i++)
                        Block.NodeList.Insert(NodeIndex + i, SplitTypeArgumentList[i]);
                    return true;
                }
            }
        }

        for (int BlockIndex = 0; BlockIndex < typeArgumentBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<TypeArgument> Block = typeArgumentBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                TypeArgument TypeArgument = Block.NodeList[NodeIndex];

                if (GetComplexifiedTypeArgument(TypeArgument, out IList<TypeArgument> ComplexifiedTypeArgumentList))
                {
                    TypeArgument ComplexifiedTypeArgument = ComplexifiedTypeArgumentList[0];
                    newTypeArgumentBlocks = (IBlockList<TypeArgument>)DeepCloneBlockListInternal((IBlockList)typeArgumentBlocks, cloneCommentGuid: false);

                    Block = newTypeArgumentBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList[NodeIndex] = ComplexifiedTypeArgument;
                    return true;
                }
            }
        }

        Contract.Unused(out newTypeArgumentBlocks);
        return false;
    }

    private static bool GetComplexifiedObjectTypeBlockList(IBlockList<ObjectType> objectTypeBlocks, out IBlockList<ObjectType> newObjectTypeBlocks)
    {
        for (int BlockIndex = 0; BlockIndex < objectTypeBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<ObjectType> Block = objectTypeBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                ObjectType ObjectType = Block.NodeList[NodeIndex];

                if (SplitObjectType(ObjectType, out IList<ObjectType> SplitObjectTypeList))
                {
                    newObjectTypeBlocks = (IBlockList<ObjectType>)DeepCloneBlockListInternal((IBlockList)objectTypeBlocks, cloneCommentGuid: false);

                    Block = newObjectTypeBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList.RemoveAt(NodeIndex);

                    for (int i = 0; i < SplitObjectTypeList.Count; i++)
                        Block.NodeList.Insert(NodeIndex + i, SplitObjectTypeList[i]);
                    return true;
                }
            }
        }

        for (int BlockIndex = 0; BlockIndex < objectTypeBlocks.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<ObjectType> Block = objectTypeBlocks.NodeBlockList[BlockIndex];

            for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
            {
                ObjectType ObjectType = Block.NodeList[NodeIndex];

                if (GetComplexifiedObjectType(ObjectType, out IList<ObjectType> ComplexifiedObjectTypeList))
                {
                    ObjectType ComplexifiedObjectType = ComplexifiedObjectTypeList[0];
                    newObjectTypeBlocks = (IBlockList<ObjectType>)DeepCloneBlockListInternal((IBlockList)objectTypeBlocks, cloneCommentGuid: false);

                    Block = newObjectTypeBlocks.NodeBlockList[BlockIndex];
                    Block.NodeList[NodeIndex] = ComplexifiedObjectType;
                    return true;
                }
            }
        }

        Contract.Unused(out newObjectTypeBlocks);
        return false;
    }
}
