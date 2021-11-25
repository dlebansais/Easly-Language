namespace BaseNodeHelper
{
    using System.Collections;
    using System.Collections.Generic;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Gets a list of nodes <paramref name="node"/> can be complexified into.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="complexifiedNodeList">The resulting list of nodes upon success. It contains at least one element.</param>
        /// <returns>True if the provided node can be complexified; otherwise, false.</returns>
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

                foreach (Node Node in SafeType.Items<Node>(ComplexifiedList))
                    complexifiedNodeList.Add(Node);

                int NewCount = complexifiedNodeList.Count;

                for (int i = OldCount; i < NewCount; i++)
                    GetComplexifiedNodeRecursive(complexifiedNodeList[i], complexifiedNodeList);
            }
        }

        private static bool GetComplexifiedNodeNotRecursive(Node node, out IList complexifiedNodeList)
        {
            bool Result;

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
                    Contracts.Contract.Unused(out complexifiedNodeList);
                    Result = false;
                    break;
            }

            return Result;
        }
    }
}
