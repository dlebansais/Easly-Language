namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static void GetComplexifiedTypeArgument(ITypeArgument node, List<INode> complexifiedNodeList)
        {
            bool IsHandled = false;

            switch (node)
            {
                case IAssignmentTypeArgument AsAssignmentTypeArgument:
                    GetComplexifiedAssignmentTypeArgument(AsAssignmentTypeArgument, complexifiedNodeList);
                    IsHandled = true;
                    break;

                case IPositionalTypeArgument AsPositionalTypeArgument:
                    GetComplexifiedPositionalTypeArgument(AsPositionalTypeArgument, complexifiedNodeList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        private static void GetComplexifiedAssignmentTypeArgument(IAssignmentTypeArgument node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> complexifiedSourceList) && complexifiedSourceList[0] is IObjectType AsComplexifiedSource)
            {
                IIdentifier ClonedParameterIdentifier = (IIdentifier)DeepCloneNode(node.ParameterIdentifier, cloneCommentGuid: false);
                IAssignmentTypeArgument NewAssignmentTypeArgument = CreateAssignmentTypeArgument(ClonedParameterIdentifier, AsComplexifiedSource);
                complexifiedNodeList.Add(NewAssignmentTypeArgument);
            }
        }

        private static void GetComplexifiedPositionalTypeArgument(IPositionalTypeArgument node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.Source, out List<INode> complexifiedSourceList) && complexifiedSourceList[0] is IObjectType AsComplexifiedSource)
            {
                IPositionalTypeArgument NewPositionalTypeArgument = CreatePositionalTypeArgument(AsComplexifiedSource);
                complexifiedNodeList.Add(NewPositionalTypeArgument);
            }
            else if (ComplexifyAsAssignmentTypeArgument(node, out IAssignmentTypeArgument ComplexifiedNode))
                complexifiedNodeList.Add(ComplexifiedNode);
        }

        private static bool ComplexifyAsAssignmentTypeArgument(IPositionalTypeArgument node, out IAssignmentTypeArgument complexifiedNode)
        {
            complexifiedNode = null;

            if (node.Source is ISimpleType AsSimpleType)
            {
                string Text = AsSimpleType.ClassIdentifier.Text;

                if (ParsePattern(Text, ":=", out string BeforeText, out string AfterText))
                {
                    IIdentifier AssignmentTarget = CreateSimpleIdentifier(BeforeText);
                    ISimpleType AssignmentType = CreateSimpleSimpleType(AfterText);

                    complexifiedNode = CreateAssignmentTypeArgument(AssignmentTarget, AssignmentType);
                }
            }

            return complexifiedNode != null;
        }
    }
}
