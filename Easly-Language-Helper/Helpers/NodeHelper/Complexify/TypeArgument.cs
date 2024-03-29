﻿namespace BaseNodeHelper;

using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    private static bool GetComplexifiedTypeArgument(TypeArgument node, out IList<TypeArgument> complexifiedTypeArgumentList)
    {
        Contract.Unused(out complexifiedTypeArgumentList);

        bool Result = false;
        bool IsHandled = false;

        switch (node)
        {
            case AssignmentTypeArgument AsAssignmentTypeArgument:
                Result = GetComplexifiedAssignmentTypeArgument(AsAssignmentTypeArgument, out complexifiedTypeArgumentList);
                IsHandled = true;
                break;

            case PositionalTypeArgument AsPositionalTypeArgument:
                Result = GetComplexifiedPositionalTypeArgument(AsPositionalTypeArgument, out complexifiedTypeArgumentList);
                IsHandled = true;
                break;
        }

        Debug.Assert(IsHandled, $"All descendants of {nameof(TypeArgument)} have been handled");

        return Result;
    }

    private static bool GetComplexifiedAssignmentTypeArgument(AssignmentTypeArgument node, out IList<TypeArgument> complexifiedTypeArgumentList)
    {
        if (GetComplexifiedObjectType(node.Source, out IList<ObjectType> ComplexifiedSourceList))
        {
            complexifiedTypeArgumentList = new List<TypeArgument>();

            foreach (ObjectType ComplexifiedSource in ComplexifiedSourceList)
            {
                Identifier ClonedParameterIdentifier = (Identifier)DeepCloneNode(node.ParameterIdentifier, cloneCommentGuid: false);
                AssignmentTypeArgument NewAssignmentTypeArgument = CreateAssignmentTypeArgument(ClonedParameterIdentifier, ComplexifiedSource);
                complexifiedTypeArgumentList.Add(NewAssignmentTypeArgument);
            }

            return true;
        }

        Contract.Unused(out complexifiedTypeArgumentList);
        return false;
    }

    private static bool GetComplexifiedPositionalTypeArgument(PositionalTypeArgument node, out IList<TypeArgument> complexifiedTypeArgumentList)
    {
        if (GetComplexifiedObjectType(node.Source, out IList<ObjectType> ComplexifiedSourceList))
        {
            complexifiedTypeArgumentList = new List<TypeArgument>();

            foreach (ObjectType ComplexifiedSource in ComplexifiedSourceList)
            {
                PositionalTypeArgument NewPositionalTypeArgument = CreatePositionalTypeArgument(ComplexifiedSource);
                complexifiedTypeArgumentList.Add(NewPositionalTypeArgument);
            }

            return true;
        }
        else if (ComplexifyAsAssignmentTypeArgument(node, out AssignmentTypeArgument ComplexifiedAssignmentTypeArgument))
        {
            complexifiedTypeArgumentList = new List<TypeArgument>() { ComplexifiedAssignmentTypeArgument };
            return true;
        }

        Contract.Unused(out complexifiedTypeArgumentList);
        return false;
    }

    private static bool ComplexifyAsAssignmentTypeArgument(PositionalTypeArgument node, out AssignmentTypeArgument complexifiedNode)
    {
        if (node.Source is SimpleType AsSimpleType)
        {
            string Text = AsSimpleType.ClassIdentifier.Text;

            if (ParsePattern(Text, ":=", out string BeforeText, out string AfterText))
            {
                Identifier AssignmentTarget = CreateSimpleIdentifier(BeforeText);
                SimpleType AssignmentType = CreateSimpleSimpleType(AfterText);

                complexifiedNode = CreateAssignmentTypeArgument(AssignmentTarget, AssignmentType);
                return true;
            }
        }
        else if (node.Source is GenericType AsGenericType)
        {
            string Text = AsGenericType.ClassIdentifier.Text;

            if (ParsePattern(Text, ":=", out string BeforeText, out string AfterText))
            {
                Identifier AssignmentTarget = CreateSimpleIdentifier(BeforeText);
                Identifier NewClassIdentifier = CreateSimpleIdentifier(AfterText);
                IBlockList<TypeArgument> ClonedTypeArgumentBlocks = (IBlockList<TypeArgument>)DeepCloneBlockListInternal((IBlockList)AsGenericType.TypeArgumentBlocks, cloneCommentGuid: false);
                GenericType NewGenericType = CreateGenericType(SharingType.NotShared, NewClassIdentifier, ClonedTypeArgumentBlocks);

                complexifiedNode = CreateAssignmentTypeArgument(AssignmentTarget, NewGenericType);
                return true;
            }
        }

        Contract.Unused(out complexifiedNode);
        return false;
    }
}
