namespace Easly
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a detachable reference.
    /// </summary>
    public interface IDetachableReference
    {
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        bool IsAssigned { get; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        object Item { get; set; }

        /// <summary>
        /// Detaches the reference.
        /// </summary>
        void Detach();
    }

    /// <summary>
    /// Represents a detachable reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IDetachableReference<T>
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

        /// <summary>
        /// Detaches the reference.
        /// </summary>
        void Detach();
    }

    /// <summary>
    /// Represents a detachable reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class DetachableReference<T> : IDetachableReference<T>, IDetachableReference
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

        /// <inheritdoc/>
        object IDetachableReference.Item { get { return Item; } set { ItemInternal = (T)value; } }

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
