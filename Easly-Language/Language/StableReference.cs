namespace Easly
{
    using System;
    using System.Diagnostics;

    public class StableReference<T>
        where T : class
    {
        #region Properties
        public bool IsAssigned { get; private set; }

        public T Item
        {
            get
            {
                T Result;

                Debug.Assert(IsAssigned == (ItemInternal != null), $"{nameof(IsAssigned)} is always true if {nameof(ItemInternal)} has been assigned, and it can only be to a non-null value");

                if (ItemInternal != null)
                    Result = ItemInternal;
                else
                    throw new InvalidOperationException();

                return Result;
            }
            set
            {
                if (value != null)
                {
                    ItemInternal = value;
                    IsAssigned = true;
                }
                else
                    throw new InvalidOperationException();
            }
        }

        private T? ItemInternal;
        #endregion
    }
}
