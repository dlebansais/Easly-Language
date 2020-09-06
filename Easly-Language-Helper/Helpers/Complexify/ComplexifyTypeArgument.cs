namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        private static bool GetComplexifiedTypeArgument(ITypeArgument node, out IList<ITypeArgument> ComplexifiedTypeArgumentList)
        {
            ComplexifiedTypeArgumentList = null;
            bool Result = false;
            bool IsHandled = false;

            switch (node)
            {
                case IAssignmentTypeArgument AsAssignmentTypeArgument:
                    Result = GetComplexifiedAssignmentTypeArgument(AsAssignmentTypeArgument, out ComplexifiedTypeArgumentList);
                    IsHandled = true;
                    break;

                case IPositionalTypeArgument AsPositionalTypeArgument:
                    Result = GetComplexifiedPositionalTypeArgument(AsPositionalTypeArgument, out ComplexifiedTypeArgumentList);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled, $"All descendants of {nameof(ITypeArgument)} have been handled");

            return Result;
        }

        private static bool GetComplexifiedAssignmentTypeArgument(IAssignmentTypeArgument node, out IList<ITypeArgument> ComplexifiedTypeArgumentList)
        {
            ComplexifiedTypeArgumentList = null;

            if (GetComplexifiedObjectType(node.Source, out IList<IObjectType> ComplexifiedSourceList))
            {
                ComplexifiedTypeArgumentList = new List<ITypeArgument>();

                foreach (IObjectType ComplexifiedSource in ComplexifiedSourceList)
                {
                    IIdentifier ClonedParameterIdentifier = (IIdentifier)DeepCloneNode(node.ParameterIdentifier, cloneCommentGuid: false);
                    IAssignmentTypeArgument NewAssignmentTypeArgument = CreateAssignmentTypeArgument(ClonedParameterIdentifier, ComplexifiedSource);
                    ComplexifiedTypeArgumentList.Add(NewAssignmentTypeArgument);
                }
            }

            return ComplexifiedTypeArgumentList != null;
        }

        private static bool GetComplexifiedPositionalTypeArgument(IPositionalTypeArgument node, out IList<ITypeArgument> ComplexifiedTypeArgumentList)
        {
            ComplexifiedTypeArgumentList = null;

            if (GetComplexifiedObjectType(node.Source, out IList<IObjectType> ComplexifiedSourceList))
            {
                ComplexifiedTypeArgumentList = new List<ITypeArgument>();

                foreach (IObjectType ComplexifiedSource in ComplexifiedSourceList)
                {
                    IPositionalTypeArgument NewPositionalTypeArgument = CreatePositionalTypeArgument(ComplexifiedSource);
                    ComplexifiedTypeArgumentList.Add(NewPositionalTypeArgument);
                }
            }
            else if (ComplexifyAsAssignmentTypeArgument(node, out IAssignmentTypeArgument ComplexifiedAssignmentTypeArgument))
                ComplexifiedTypeArgumentList = new List<ITypeArgument>() { ComplexifiedAssignmentTypeArgument };

            return ComplexifiedTypeArgumentList != null;
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
            else if (node.Source is IGenericType AsGenericType)
            {
                string Text = AsGenericType.ClassIdentifier.Text;

                if (ParsePattern(Text, ":=", out string BeforeText, out string AfterText))
                {
                    IIdentifier AssignmentTarget = CreateSimpleIdentifier(BeforeText);
                    IIdentifier NewClassIdentifier = CreateSimpleIdentifier(AfterText);
                    IBlockList<ITypeArgument, TypeArgument> ClonedTypeArgumentBlocks = (IBlockList<ITypeArgument, TypeArgument>)DeepCloneBlockList((IBlockList)AsGenericType.TypeArgumentBlocks, cloneCommentGuid: false);
                    IGenericType NewGenericType = CreateGenericType(SharingType.NotShared, NewClassIdentifier, ClonedTypeArgumentBlocks);

                    complexifiedNode = CreateAssignmentTypeArgument(AssignmentTarget, NewGenericType);
                }
            }

            return complexifiedNode != null;
        }
    }
}
