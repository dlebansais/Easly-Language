namespace Easly
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a once reference.
    /// </summary>
    public interface IOnceReference
    {
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        bool IsAssigned { get; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        object Item { get; set; }
    }

    /// <summary>
    /// Represents a once reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IOnceReference<T>
        where T : class
    {
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        bool IsAssigned { get; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        T Item { get; set; }
    }

    /// <summary>
    /// Represents a once reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class OnceReference<T> : IOnceReference<T>, IOnceReference
        where T : class
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        public bool IsAssigned { get; private set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public T Item
        {
            get
            {
                Debug.Assert(IsAssigned == (ItemInternal != null), $"{nameof(IsAssigned)} is always true if {nameof(ItemInternal)} has been assigned, and it can only be to a non-null value");

                if (ItemInternal != null)
                    return ItemInternal;
                else
                    throw new InvalidOperationException();
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

        /// <inheritdoc/>
        object IOnceReference.Item
        {
            get
            {
                return Item;
            }
            set
            {
                if (!IsAssigned)
                    if (value is T AsItem)
                    {
                        ItemInternal = AsItem;
                        IsAssigned = true;
                    }
                    else
                        throw new InvalidOperationException();
                else
                    throw new InvalidOperationException();
            }
        }

        private T? ItemInternal;
        #endregion
    }
}
