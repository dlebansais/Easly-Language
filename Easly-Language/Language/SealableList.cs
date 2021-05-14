namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents a list that can be sealed.
    /// </summary>
    public interface ISealableList
    {
        /// <summary>
        /// Gets a value indicating whether the list is sealed.
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// Seals the list.
        /// </summary>
        void Seal();
    }

    /// <summary>
    /// Represents a list ofitem type <typeparamref name="TItem"/> that can be sealed.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public interface ISealableList<TItem> : IList<TItem>, ISealableList
    {
        /// <summary>
        /// Adds a range of items to the list.
        /// </summary>
        /// <param name="other">The items to add.</param>
        void AddRange(IList<TItem> other);

        /// <summary>
        /// Checks whether the list contains some items.
        /// </summary>
        /// <param name="match">The predicate for matching items.</param>
        /// <returns>True if the list contains items as per <paramref name="match"/>.</returns>
        bool Exists(Predicate<TItem> match);

        /// <summary>
        /// Clones the list, return an unsealed copy.
        /// </summary>
        /// <returns>The unsealed copy.</returns>
        SealableList<TItem> CloneUnsealed();
    }

    /// <summary>
    /// Represents a list ofitem type <typeparamref name="TItem"/> that can be sealed.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class SealableList<TItem> : List<TItem>, ISealableList<TItem>
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="SealableList{TItem}"/> class.
        /// </summary>
        public SealableList()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the list is sealed.
        /// </summary>
        public bool IsSealed { get; private set; }
        #endregion

        #region Client Interface
        /// <summary>
        /// Seals the list.
        /// </summary>
        public void Seal()
        {
            Debug.Assert(!IsSealed, "Sealing should be done only once");

            IsSealed = true;
        }

        /// <summary>
        /// Adds a new item to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public new void Add(TItem item)
        {
            Debug.Assert(!IsSealed, "A sealed collection cannot be modified");

            base.Add(item);
        }

        /// <summary>
        /// Clones the list, return an unsealed copy.
        /// </summary>
        /// <returns>The unsealed copy.</returns>
        public SealableList<TItem> CloneUnsealed()
        {
            SealableList<TItem> CloneTable = new SealableList<TItem>();

            foreach (TItem Item in this)
                CloneTable.Add(Item);

            return CloneTable;
        }

        /// <summary>
        /// Adds a range of items to the list.
        /// </summary>
        /// <param name="other">The items to add.</param>
        public void AddRange(IList<TItem> other)
        {
            Debug.Assert(!IsSealed, "A sealed collection cannot be modified");

            base.AddRange(other);
        }
        #endregion
    }
}
