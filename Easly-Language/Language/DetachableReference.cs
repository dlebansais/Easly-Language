namespace Easly
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// A detachable reference of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class DetachableReference<T>
        where T : class
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the reference is assigned.
        /// </summary>
        public virtual bool IsAssigned { get; protected set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
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

        private T? ItemInternal;
        #endregion

        #region Detaching
        /// <summary>
        /// Detaches the reference.
        /// </summary>
        public virtual void Detach()
        {
            ItemInternal = null;
            IsAssigned = false;
        }
        #endregion
    }
}
