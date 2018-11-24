using BaseNode;
using Easly;

namespace BaseNodeHelper
{
    public class OptionalReferenceHelper<IN>
        where IN : class, INode
    {
        public static OptionalReference<IN> CreateReference(IN Item)
        {
            OptionalReference<IN> Result = new OptionalReference<IN>();
            Result.Item = Item;
            Result.Unassign();

            return Result;
        }
    }
}
