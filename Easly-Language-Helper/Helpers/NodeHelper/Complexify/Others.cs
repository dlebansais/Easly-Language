namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        #region Others
        private static bool GetComplexifiedAttachment(Attachment node, out IList<Attachment> complexifiedAttachmentList)
        {
            complexifiedAttachmentList = null!;

            if (GetComplexifiedObjectTypeBlockList(node.AttachTypeBlocks, out IBlockList<ObjectType> ComplexifiedAttachTypeBlocks))
            {
                Scope ClonedInstructions = (Scope)DeepCloneNode(node.Instructions, cloneCommentGuid: false);
                Attachment ComplexifiedAttachment = CreateAttachment(ComplexifiedAttachTypeBlocks, ClonedInstructions);

                complexifiedAttachmentList = new List<Attachment>() { ComplexifiedAttachment };

                return true;
            }

            complexifiedAttachmentList = null!;
            return false;
        }

        private static bool GetComplexifiedQualifiedName(QualifiedName node, out IList<QualifiedName> complexifiedQualifiedNameList)
        {
            complexifiedQualifiedNameList = null!;

            if (ComplexifyQualifiedName(node, out QualifiedName ComplexifiedQualifiedName))
                complexifiedQualifiedNameList = new List<QualifiedName>() { ComplexifiedQualifiedName };

            return complexifiedQualifiedNameList != null;
        }

        private static bool ComplexifyQualifiedName(QualifiedName node, out QualifiedName complexifiedNode)
        {
            Debug.Assert(node.Path.Count > 0, $"{nameof(node)} always has at least one element");

            complexifiedNode = null!;
            bool IsSplit = false;

            IList<Identifier> Path = new List<Identifier>();

            foreach (Identifier Item in node.Path)
            {
                string[] SplitText = Item.Text.Split('.');
                IsSplit |= SplitText.Length > 1;

                for (int i = 0; i < SplitText.Length; i++)
                {
                    Identifier Identifier = CreateSimpleIdentifier(SplitText[i]);
                    Path.Add(Identifier);
                }
            }

            bool WithSplit = IsSplit && Path.Count >= node.Path.Count;
            bool WithoutSplit = !IsSplit && Path.Count == node.Path.Count;
            Debug.Assert(WithSplit || WithoutSplit, "A split at least increases the count of elements, and no split preserves it");

            if (IsSplit)
                complexifiedNode = CreateQualifiedName(Path);

            return complexifiedNode != null;
        }

        private static bool GetComplexifiedConditional(Conditional node, out IList<Conditional> complexifiedConditionalList)
        {
            complexifiedConditionalList = null!;

            if (GetComplexifiedExpression(node.BooleanExpression, out IList<Expression> ComplexifiedBooleanExpressionList))
            {
                complexifiedConditionalList = new List<Conditional>();

                foreach (Expression ComplexifiedBooleanExpression in ComplexifiedBooleanExpressionList)
                {
                    Scope ClonedInstructions = (Scope)DeepCloneNode(node.Instructions, cloneCommentGuid: false);
                    Conditional ComplexifiedNode = CreateConditional(ComplexifiedBooleanExpression, ClonedInstructions);
                    complexifiedConditionalList.Add(ComplexifiedNode);
                }

                return true;
            }

            complexifiedConditionalList = null!;
            return false;
        }
        #endregion
    }
}
