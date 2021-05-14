namespace Easly
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a stable object reference of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class StableReference<T>
        where T : class
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        public bool IsAssigned { get; private set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
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
