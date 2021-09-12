#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        public static bool GetComplexifiedNode(Node node, out IList<Node> complexifiedNodeList)
        {
            complexifiedNodeList = new List<Node>();
            GetComplexifiedNodeRecursive(node, complexifiedNodeList);

            return complexifiedNodeList.Count > 0;
        }

        private static void GetComplexifiedNodeRecursive(Node node, IList<Node> complexifiedNodeList)
        {
            if (GetComplexifiedNodeNotRecursive(node, out IList ComplexifiedList))
            {
                int OldCount = complexifiedNodeList.Count;

                foreach (Node Node in ComplexifiedList)
                    complexifiedNodeList.Add(Node);

                int NewCount = complexifiedNodeList.Count;

                for (int i = OldCount; i < NewCount; i++)
                    GetComplexifiedNodeRecursive(complexifiedNodeList[i], complexifiedNodeList);
            }
        }

        private static bool GetComplexifiedNodeNotRecursive(Node node, out IList complexifiedNodeList)
        {
            bool Result = false;
            complexifiedNodeList = null;

            switch (node)
            {
                case Argument AsArgument:
                    Result = GetComplexifiedArgument(AsArgument, out IList<Argument> ComplexifiedArgumentList);
                    complexifiedNodeList = (IList)ComplexifiedArgumentList;
                    break;

                case Attachment AsAttachment:
                    Result = GetComplexifiedAttachment(AsAttachment, out IList<Attachment> ComplexifiedAttachmentList);
                    complexifiedNodeList = (IList)ComplexifiedAttachmentList;
                    break;

                case Expression AsExpression:
                    Result = GetComplexifiedExpression(AsExpression, out IList<Expression> ComplexifiedExpressionList);
                    complexifiedNodeList = (IList)ComplexifiedExpressionList;
                    break;

                case Instruction AsInstruction:
                    Result = GetComplexifiedInstruction(AsInstruction, out IList<Instruction> ComplexifiedInstructionList);
                    complexifiedNodeList = (IList)ComplexifiedInstructionList;
                    break;

                case ObjectType AsObjectType:
                    Result = GetComplexifiedObjectType(AsObjectType, out IList<ObjectType> ComplexifiedObjectTypeList);
                    complexifiedNodeList = (IList)ComplexifiedObjectTypeList;
                    break;

                case TypeArgument AsTypeArgument:
                    Result = GetComplexifiedTypeArgument(AsTypeArgument, out IList<TypeArgument> ComplexifiedTypeArgumentList);
                    complexifiedNodeList = (IList)ComplexifiedTypeArgumentList;
                    break;

                case QualifiedName AsQualifiedName:
                    Result = GetComplexifiedQualifiedName(AsQualifiedName, out IList<QualifiedName> ComplexifiedQualifiedNameList);
                    complexifiedNodeList = (IList)ComplexifiedQualifiedNameList;
                    break;

                case Conditional AsConditional:
                    Result = GetComplexifiedConditional(AsConditional, out IList<Conditional> ComplexifiedConditionalList);
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
