using BaseNode;
using Easly;
using System.Diagnostics;

namespace BaseNodeHelper
{
    public class OptionalReferenceHelper<IN>
        where IN : class, INode
    {
        public static IOptionalReference<IN> CreateReference(IN item)
        {
            OptionalReference<IN> Result = new OptionalReference<IN>();
            Result.Item = item;
            Result.Unassign();

            return Result;
        }

        public static IOptionalReference<IN> CreateReferenceCopy(IOptionalReference<IN> optional)
        {
            OptionalReference<IN> Result = new OptionalReference<IN>();

            if (optional != null)
            {
                if (optional.HasItem)
                {
                    Debug.Assert(optional.Item != null);

                    IN ClonedItem = NodeHelper.DeepCloneNode(optional.Item) as IN;
                    Debug.Assert(ClonedItem != null);

                    Result.Item = ClonedItem;
                }

                if (optional.IsAssigned)
                    Result.Assign();
            }

            return Result;
        }
    }
}
