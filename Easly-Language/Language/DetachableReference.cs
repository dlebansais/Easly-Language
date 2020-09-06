namespace Easly
{
    using System;
    using System.Diagnostics;

    public class DetachableReference<T>
        where T : class
    {
        #region Properties
        private T? ItemInternal;

        public virtual bool IsAssigned { get; protected set; }

        public virtual T Item
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
        #endregion

        #region Detaching
        public virtual void Detach()
        {
            ItemInternal = null;
            IsAssigned = false;
        }
        #endregion
    }
}
