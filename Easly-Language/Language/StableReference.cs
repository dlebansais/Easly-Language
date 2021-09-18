namespace Easly
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a stable reference.
    /// </summary>
    public interface IStableReference
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
    /// Represents a stable reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IStableReference<T>
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
    /// Represents a stable reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class StableReference<T> : IStableReference<T>, IStableReference
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

        /// <inheritdoc/>
        object IStableReference.Item { get { return Item; } set { ItemInternal = (T)value; } }

        private T? ItemInternal;
        #endregion
    }
}
