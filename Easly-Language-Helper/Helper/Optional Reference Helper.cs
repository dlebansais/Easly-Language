using BaseNode;
using Easly;

namespace BaseNodeHelper
{
    public class OptionalReferenceHelper<IN>
        where IN : class, INode
    {
        public static OptionalReference<IN> CreateReference(IN item)
        {
            OptionalReference<IN> Result = new OptionalReference<IN>();
            Result.Item = item;
            Result.Unassign();

            return Result;
        }
    }
}
