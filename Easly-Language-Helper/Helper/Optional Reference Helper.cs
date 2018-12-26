using BaseNode;
using Easly;

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
    }
}
