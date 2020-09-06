namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        #region Others
        private static bool GetComplexifiedAttachment(IAttachment node, out IList<IAttachment> complexifiedAttachmentList)
        {
            complexifiedAttachmentList = null;

            if (GetComplexifiedObjectTypeBlockList(node.AttachTypeBlocks, out IBlockList<IObjectType, ObjectType> ComplexifiedAttachTypeBlocks))
            {
                IScope ClonedInstructions = (IScope)DeepCloneNode(node.Instructions, cloneCommentGuid: false);
                IAttachment ComplexifiedAttachment = CreateAttachment(ComplexifiedAttachTypeBlocks, ClonedInstructions);

                complexifiedAttachmentList = new List<IAttachment>() { ComplexifiedAttachment };

                return true;
            }

            complexifiedAttachmentList = null;
            return false;
        }

        private static bool GetComplexifiedQualifiedName(IQualifiedName node, out IList<IQualifiedName> complexifiedQualifiedNameList)
        {
            complexifiedQualifiedNameList = null;

            if (ComplexifyQualifiedName(node, out IQualifiedName ComplexifiedQualifiedName))
                complexifiedQualifiedNameList = new List<IQualifiedName>() { ComplexifiedQualifiedName };

            return complexifiedQualifiedNameList != null;
        }

        private static bool ComplexifyQualifiedName(IQualifiedName node, out IQualifiedName complexifiedNode)
        {
            Debug.Assert(node.Path.Count > 0, "A qualified name path must contain at least one element");

            complexifiedNode = null;
            bool IsSplit = false;

            IList<IIdentifier> Path = new List<IIdentifier>();

            foreach (IIdentifier Item in node.Path)
            {
                string[] SplitText = Item.Text.Split('.');
                IsSplit |= SplitText.Length > 1;

                for (int i = 0; i < SplitText.Length; i++)
                {
                    IIdentifier Identifier = CreateSimpleIdentifier(SplitText[i]);
                    Path.Add(Identifier);
                }
            }

            Debug.Assert((IsSplit && Path.Count >= node.Path.Count) || (!IsSplit && Path.Count == node.Path.Count), "A split at least increases the count of elements, and no split preserves it");

            if (IsSplit)
                complexifiedNode = CreateQualifiedName(Path);

            return complexifiedNode != null;
        }

        private static bool GetComplexifiedConditional(IConditional node, out IList<IConditional> complexifiedConditionalList)
        {
            complexifiedConditionalList = null;

            if (GetComplexifiedExpression(node.BooleanExpression, out IList<IExpression> ComplexifiedBooleanExpressionList))
            {
                complexifiedConditionalList = new List<IConditional>();

                foreach (IExpression ComplexifiedBooleanExpression in ComplexifiedBooleanExpressionList)
                {
                    IScope ClonedInstructions = (IScope)DeepCloneNode(node.Instructions, cloneCommentGuid: false);
                    IConditional ComplexifiedNode = CreateConditional(ComplexifiedBooleanExpression, ClonedInstructions);
                    complexifiedConditionalList.Add(ComplexifiedNode);
                }

                return true;
            }

            complexifiedConditionalList = null;
            return false;
        }
        #endregion
    }
}
