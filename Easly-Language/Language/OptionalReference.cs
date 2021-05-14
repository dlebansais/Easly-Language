namespace Easly
{
    using System;

    /// <summary>
    /// Represents an optional reference.
    /// </summary>
    public interface IOptionalReference
    {
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        bool IsAssigned { get; }

        /// <summary>
        /// Gets a value indicating whether there is object reference to assign.
        /// </summary>
        bool HasItem { get; }

        /// <summary>
        /// Gets the object.
        /// </summary>
        object Item { get; }

        /// <summary>
        /// Assigns the object.
        /// </summary>
        void Assign();

        /// <summary>
        /// Unassigns the object.
        /// </summary>
        void Unassign();

        /// <summary>
        /// Clears the object reference.
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// Representes an optional reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IOptionalReference<T>
        where T : class
    {
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        bool IsAssigned { get; }

        /// <summary>
        /// Gets a value indicating whether there is object reference to assign.
        /// </summary>
        bool HasItem { get; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        T Item { get; set; }

        /// <summary>
        /// Assigns the object.
        /// </summary>
        void Assign();

        /// <summary>
        /// Unassigns the object.
        /// </summary>
        void Unassign();

        /// <summary>
        /// Clears the object reference.
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// Representes an optional reference to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    [PolySerializer.Serializable]
    public class OptionalReference<T> : IOptionalReference<T>, IOptionalReference
        where T : class
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalReference{T}"/> class.
        /// </summary>
        public OptionalReference()
        {
            ItemInternal = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalReference{T}"/> class.
        /// </summary>
        /// <param name="initialValue">The initial item value.</param>
        public OptionalReference(T initialValue)
        {
            ItemInternal = initialValue;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the reference is assigned.
        /// </summary>
        public bool IsAssigned { get; private set; }

        /// <summary>
        /// Gets a value indicating whether there is object reference to assign.
        /// </summary>
        public bool HasItem { get { return ItemInternal != null; } }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        [PolySerializer.Serializable(Condition = nameof(IsAssigned))]
        public T Item
        {
            get
            {
                if (ItemInternal == null)
                    throw new InvalidOperationException();
                else
                    return ItemInternal;
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

        /// <summary>
        /// Gets the object.
        /// </summary>
        object IOptionalReference.Item { get { return Item; } }

        [PolySerializer.Serializable(Exclude = true)]
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public T? ItemInternal;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion

        #region Assignment
        /// <summary>
        /// Assigns the object.
        /// </summary>
        public void Assign()
        {
            if (ItemInternal == null)
                throw new InvalidOperationException();

            IsAssigned = true;
        }

        /// <summary>
        /// Unassigns the object.
        /// </summary>
        public void Unassign()
        {
            IsAssigned = false;
        }

        /// <summary>
        /// Clears the object reference.
        /// </summary>
        public void Clear()
        {
            IsAssigned = false;
            ItemInternal = null;
        }
        #endregion
    }
}
