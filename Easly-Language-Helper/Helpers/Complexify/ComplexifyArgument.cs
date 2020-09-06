namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedArgument(IArgument node, out IList<IArgument> complexifiedArgumentList)
        {
            complexifiedArgumentList = null;
            bool Result = false;
            bool IsHandled = false;

            switch (node)
            {
                case IAssignmentArgument AsAssignmentArgument:
                    Result = GetComplexifiedAssignmentArgument(AsAssignmentArgument, out complexifiedArgumentList);
                    IsHandled = true;
                    break;

                case IPositionalArgument AsPositionalArgument:
                    Result = GetComplexifiedPositionalArgument(AsPositionalArgument, out complexifiedArgumentList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(IArgument)} have been handled");

            return Result;
        }

        private static bool GetComplexifiedAssignmentArgument(IAssignmentArgument node, out IList<IArgument> complexifiedArgumentList)
        {
            if (GetComplexifiedIdentifierBlockList(node.ParameterBlocks, out IBlockList<IIdentifier, Identifier> ComplexifiedParameterBlocks))
            {
                complexifiedArgumentList = new List<IArgument>();

                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IAssignmentArgument NewAssignmentArgument = CreateAssignmentArgument(ComplexifiedParameterBlocks, ClonedSource);
                complexifiedArgumentList.Add(NewAssignmentArgument);

                return true;
            }
            else if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedArgumentList = new List<IArgument>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IBlockList<IIdentifier, Identifier> ClonedParameterBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.ParameterBlocks, cloneCommentGuid: false);
                    IAssignmentArgument NewAssignmentArgument = CreateAssignmentArgument(ClonedParameterBlocks, ComplexifiedSource);
                    complexifiedArgumentList.Add(NewAssignmentArgument);
                }

                return true;
            }

            complexifiedArgumentList = null;
            return false;
        }

        private static bool GetComplexifiedPositionalArgument(IPositionalArgument node, out IList<IArgument> complexifiedArgumentList)
        {
            if (GetComplexifiedExpression(node.Source, out IList<IExpression> ComplexifiedSourceList))
            {
                complexifiedArgumentList = new List<IArgument>();

                foreach (IExpression ComplexifiedSource in ComplexifiedSourceList)
                {
                    IPositionalArgument NewPositionalArgument = CreatePositionalArgument(ComplexifiedSource);
                    complexifiedArgumentList.Add(NewPositionalArgument);
                }

                return true;
            }
            else if (GetComplexifiedAsAssignmentArgument(node, out IAssignmentArgument ComplexifiedAssignmentArgument))
            {
                complexifiedArgumentList = new List<IArgument>() { ComplexifiedAssignmentArgument };
                return true;
            }

            complexifiedArgumentList = null;
            return false;
        }

        private static bool GetComplexifiedAsAssignmentArgument(IPositionalArgument node, out IAssignmentArgument complexifiedNode)
        {
            if (node.Source is IQueryExpression AsQueryExpression)
                if (ParsePattern(AsQueryExpression, ":=", out string BeforeText, out string AfterText))
                {
                    List<IIdentifier> ParameterList = new List<IIdentifier>() { CreateSimpleIdentifier(BeforeText) };
                    CloneComplexifiedExpression(AsQueryExpression, AfterText, out IExpression Source);

                    complexifiedNode = CreateAssignmentArgument(ParameterList, Source);
                    return true;
                }

            complexifiedNode = null;
            return false;
        }
    }
}
