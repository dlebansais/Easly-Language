namespace Easly
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Representes a once reference.
    /// </summary>
    public interface IOnceReference
    {
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        bool IsAssigned { get; }

        /// <summary>
        /// Gets the object reference.
        /// </summary>
        object? Reference { get; }
    }

    /// <summary>
    /// Representes a once reference to a <typeparamref name="T"/> object.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class OnceReference<T> : IOnceReference
        where T : class
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        public bool IsAssigned { get; private set; }

        /// <summary>
        /// Gets or sets the object.
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

        /// <summary>
        /// Gets the object reference.
        /// </summary>
        public object? Reference
        {
            get
            {
                return ItemInternal;
            }
        }

        private T? ItemInternal;
        #endregion
    }
}
