namespace Easly
{
    using System;
    using System.Diagnostics;

    public interface IOnceReference
    {
        bool IsAssigned { get; }
        object? Reference { get; }
    }

    public class OnceReference<T> : IOnceReference
        where T : class
    {
        private T? ItemInternal;

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
                if (!IsAssigned)
                    if (value != null)
                    {
                        ItemInternal = value;
                        IsAssigned = true;
                    }
                    else
                        throw new InvalidOperationException();
                else
                    throw new InvalidOperationException();
            }
        }

        public object? Reference
        {
            get
            {
                return ItemInternal;
            }
        }
        #endregion
    }
}
