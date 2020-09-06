namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        public static bool GetComplexifiedNode(INode node, out IList<INode> complexifiedNodeList)
        {
            complexifiedNodeList = new List<INode>();
            GetComplexifiedNodeRecursive(node, complexifiedNodeList);

            return complexifiedNodeList.Count > 0;
        }

        private static void GetComplexifiedNodeRecursive(INode node, IList<INode> complexifiedNodeList)
        {
            if (GetComplexifiedNodeNotRecursive(node, out IList ComplexifiedList))
            {
                int OldCount = complexifiedNodeList.Count;

                foreach (INode Node in ComplexifiedList)
                    complexifiedNodeList.Add(Node);

                int NewCount = complexifiedNodeList.Count;

                for (int i = OldCount; i < NewCount; i++)
                    GetComplexifiedNodeRecursive(complexifiedNodeList[i], complexifiedNodeList);
            }
        }

        private static bool GetComplexifiedNodeNotRecursive(INode node, out IList complexifiedNodeList)
        {
            bool Result = false;
            complexifiedNodeList = null;

            switch (node)
            {
                case IArgument AsArgument:
                    Result = GetComplexifiedArgument(AsArgument, out IList<IArgument> ComplexifiedArgumentList);
                    complexifiedNodeList = (IList)ComplexifiedArgumentList;
                    break;

                case IAttachment AsAttachment:
                    Result = GetComplexifiedAttachment(AsAttachment, out IList<IAttachment> ComplexifiedAttachmentList);
                    complexifiedNodeList = (IList)ComplexifiedAttachmentList;
                    break;

                case IExpression AsExpression:
                    Result = GetComplexifiedExpression(AsExpression, out IList<IExpression> ComplexifiedExpressionList);
                    complexifiedNodeList = (IList)ComplexifiedExpressionList;
                    break;

                case IInstruction AsInstruction:
                    Result = GetComplexifiedInstruction(AsInstruction, out IList<IInstruction> ComplexifiedInstructionList);
                    complexifiedNodeList = (IList)ComplexifiedInstructionList;
                    break;

                case IObjectType AsObjectType:
                    Result = GetComplexifiedObjectType(AsObjectType, out IList<IObjectType> ComplexifiedObjectTypeList);
                    complexifiedNodeList = (IList)ComplexifiedObjectTypeList;
                    break;

                case ITypeArgument AsTypeArgument:
                    Result = GetComplexifiedTypeArgument(AsTypeArgument, out IList<ITypeArgument> ComplexifiedTypeArgumentList);
                    complexifiedNodeList = (IList)ComplexifiedTypeArgumentList;
                    break;

                case IQualifiedName AsQualifiedName:
                    Result = GetComplexifiedQualifiedName(AsQualifiedName, out IList<IQualifiedName> ComplexifiedQualifiedNameList);
                    complexifiedNodeList = (IList)ComplexifiedQualifiedNameList;
                    break;

                case IConditional AsConditional:
                    Result = GetComplexifiedConditional(AsConditional, out IList<IConditional> ComplexifiedConditionalList);
                    complexifiedNodeList = (IList)ComplexifiedConditionalList;
                    break;

                default:
                    complexifiedNodeList = null;
                    return false;
            }

            return Result;
        }
    }
}
