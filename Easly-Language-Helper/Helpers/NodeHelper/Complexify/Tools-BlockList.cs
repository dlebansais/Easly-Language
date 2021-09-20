#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

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
                        newBlockList = (IBlockList<Identifier>)DeepCloneBlockList((IBlockList)identifierBlockList, cloneCommentGuid: false);

                        newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                        for (int i = 0; i < Split.Count; i++)
                            newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                        return true;
                    }
                }
            }

            newBlockList = null!;
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
                        newBlockList = (IBlockList<Name>)DeepCloneBlockList((IBlockList)nameBlockList, cloneCommentGuid: false);

                        newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                        for (int i = 0; i < Split.Count; i++)
                            newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                        return true;
                    }
                }
            }

            newBlockList = null!;
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

            split = null!;
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
                        newArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

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
                        newArgumentBlocks = (IBlockList<Argument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedArgument;
                        return true;
                    }
                }
            }

            newArgumentBlocks = null!;
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
                        AssignmentArgument ComplexifiedAssignmentArgument = (AssignmentArgument)ComplexifiedAssignmentArgumentList[0];
                        Debug.Assert(ComplexifiedAssignmentArgument != null, $"The list can't contain anything else than {nameof(AssignmentArgument)} elements");

                        newAssignmentArgumentBlocks = (IBlockList<AssignmentArgument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newAssignmentArgumentBlocks.NodeBlockList[BlockIndex];
                        Debug.Assert(Block != null);

                        if (Block == null)
                            return false;

                        IList<AssignmentArgument> NodeList = Block.NodeList;

                        Debug.Assert(ComplexifiedAssignmentArgument != null);

                        if (ComplexifiedAssignmentArgument == null)
                            return false;

                        NodeList[NodeIndex] = ComplexifiedAssignmentArgument;
                        return true;
                    }
                }
            }

            newAssignmentArgumentBlocks = null!;
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
                        newQualifiedNameBlocks = (IBlockList<QualifiedName>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newQualifiedNameBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedQualifiedName;
                        return true;
                    }
                }
            }

            newQualifiedNameBlocks = null!;
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
                        newBlockList = (IBlockList<EntityDeclaration>)DeepCloneBlockList((IBlockList)identifierBlockList, cloneCommentGuid: false);

                        newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                        for (int i = 0; i < Split.Count; i++)
                            newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                        return true;
                    }
                }
            }

            newBlockList = null!;
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
                        newTypeArgumentBlocks = (IBlockList<TypeArgument>)DeepCloneBlockList((IBlockList)typeArgumentBlocks, cloneCommentGuid: false);

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
                        newTypeArgumentBlocks = (IBlockList<TypeArgument>)DeepCloneBlockList((IBlockList)typeArgumentBlocks, cloneCommentGuid: false);

                        Block = newTypeArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedTypeArgument;
                        return true;
                    }
                }
            }

            newTypeArgumentBlocks = null!;
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
                        newObjectTypeBlocks = (IBlockList<ObjectType>)DeepCloneBlockList((IBlockList)objectTypeBlocks, cloneCommentGuid: false);

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
                        newObjectTypeBlocks = (IBlockList<ObjectType>)DeepCloneBlockList((IBlockList)objectTypeBlocks, cloneCommentGuid: false);

                        Block = newObjectTypeBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedObjectType;
                        return true;
                    }
                }
            }

            newObjectTypeBlocks = null!;
            return false;
        }
    }
}
