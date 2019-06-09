namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static void GetComplexifiedArgument(IArgument node, List<INode> complexifiedNodeList)
        {
            bool IsHandled = false;

            switch (node)
            {
                case IAssignmentArgument AsAssignmentArgument:
                    GetComplexifiedAssignmentArgument(AsAssignmentArgument, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPositionalArgument AsPositionalArgument:
                    GetComplexifiedPositionalArgument(AsPositionalArgument, complexifiedNodeList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        private static void GetComplexifiedAssignmentArgument(IAssignmentArgument node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedIdentifierBlockList(node.ParameterBlocks, out IBlockList<IIdentifier, Identifier> ComplexifiedParameterBlocks))
            {
                IExpression ClonedSource = (IExpression)DeepCloneNode(node.Source, cloneCommentGuid: false);
                IAssignmentArgument NewAssignmentArgument = CreateAssignmentArgument(ComplexifiedParameterBlocks, ClonedSource);
                complexifiedNodeList.Add(NewAssignmentArgument);
            }
            else if (GetComplexifiedNode(node.Source, out List<INode> complexifiedSourceList) && complexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IBlockList<IIdentifier, Identifier> ClonedParameterBlocks = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)node.ParameterBlocks, cloneCommentGuid: false);
                IAssignmentArgument NewAssignmentArgument = CreateAssignmentArgument(ClonedParameterBlocks, AsComplexifiedSource);
                complexifiedNodeList.Add(NewAssignmentArgument);
            }
        }

        private static void GetComplexifiedPositionalArgument(IPositionalArgument node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedAsAssignmentArgument(node, out IAssignmentArgument ComplexifiedAsAssignmentArgument))
                complexifiedNodeList.Add(ComplexifiedAsAssignmentArgument);
            else if (GetComplexifiedNode(node.Source, out List<INode> complexifiedSourceList) && complexifiedSourceList[0] is IExpression AsComplexifiedSource)
            {
                IPositionalArgument NewPositionalArgument = CreatePositionalArgument(AsComplexifiedSource);
                complexifiedNodeList.Add(NewPositionalArgument);
            }
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
