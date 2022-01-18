namespace BaseNodeHelper;

using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    private static bool GetComplexifiedArgument(Argument node, out IList<Argument> complexifiedArgumentList)
    {
        Contract.Unused(out complexifiedArgumentList);

        bool Result = false;
        bool IsHandled = false;

        switch (node)
        {
            case AssignmentArgument AsAssignmentArgument:
                Result = GetComplexifiedAssignmentArgument(AsAssignmentArgument, out complexifiedArgumentList);
                IsHandled = true;
                break;

            case PositionalArgument AsPositionalArgument:
                Result = GetComplexifiedPositionalArgument(AsPositionalArgument, out complexifiedArgumentList);
                IsHandled = true;
                break;
        }

        Debug.Assert(IsHandled, $"All descendants of {nameof(Argument)} have been handled");

        return Result;
    }

    private static bool GetComplexifiedAssignmentArgument(AssignmentArgument node, out IList<Argument> complexifiedArgumentList)
    {
        if (GetComplexifiedIdentifierBlockList(node.ParameterBlocks, out IBlockList<Identifier> ComplexifiedParameterBlocks))
        {
            complexifiedArgumentList = new List<Argument>();

            Expression ClonedSource = (Expression)DeepCloneNode(node.Source, cloneCommentGuid: false);
            AssignmentArgument NewAssignmentArgument = CreateAssignmentArgument(ComplexifiedParameterBlocks, ClonedSource);
            complexifiedArgumentList.Add(NewAssignmentArgument);

            return true;
        }
        else if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
        {
            complexifiedArgumentList = new List<Argument>();

            foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
            {
                IBlockList<Identifier> ClonedParameterBlocks = (IBlockList<Identifier>)DeepCloneBlockListInternal((IBlockList)node.ParameterBlocks, cloneCommentGuid: false);
                AssignmentArgument NewAssignmentArgument = CreateAssignmentArgument(ClonedParameterBlocks, ComplexifiedSource);
                complexifiedArgumentList.Add(NewAssignmentArgument);
            }

            return true;
        }

        Contract.Unused(out complexifiedArgumentList);
        return false;
    }

    private static bool GetComplexifiedPositionalArgument(PositionalArgument node, out IList<Argument> complexifiedArgumentList)
    {
        if (GetComplexifiedAsAssignmentArgument(node, out AssignmentArgument ComplexifiedAssignmentArgument))
        {
            complexifiedArgumentList = new List<Argument>() { ComplexifiedAssignmentArgument };
            return true;
        }
        else if (GetComplexifiedExpression(node.Source, out IList<Expression> ComplexifiedSourceList))
        {
            complexifiedArgumentList = new List<Argument>();

            foreach (Expression ComplexifiedSource in ComplexifiedSourceList)
            {
                PositionalArgument NewPositionalArgument = CreatePositionalArgument(ComplexifiedSource);
                complexifiedArgumentList.Add(NewPositionalArgument);
            }

            return true;
        }

        Contract.Unused(out complexifiedArgumentList);
        return false;
    }

    private static bool GetComplexifiedAsAssignmentArgument(PositionalArgument node, out AssignmentArgument complexifiedNode)
    {
        if (node.Source is QueryExpression AsQueryExpression)
            if (ParsePattern(AsQueryExpression, ":=", out string BeforeText, out string AfterText))
            {
                List<Identifier> ParameterList = new() { CreateSimpleIdentifier(BeforeText) };
                CloneComplexifiedExpression(AsQueryExpression, AfterText, out Expression Source);

                complexifiedNode = CreateAssignmentArgument(ParameterList, Source);
                return true;
            }

        Contract.Unused(out complexifiedNode);
        return false;
    }
}
