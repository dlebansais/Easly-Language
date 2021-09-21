namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a dictionary that can be sealed.
    /// </summary>
    public interface ISealableDictionary
    {
        /// <summary>
        /// Gets a value indicating whether the dictionary is sealed.
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// Seals the dictionary.
        /// </summary>
        void Seal();
    }

    /// <summary>
    /// Represents a dictionary of key type <typeparamref name="TKey"/> that can be sealed.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IDictionaryIndex<TKey>
    {
        /// <summary>
        /// Gets a value indicating whether the dictionary contains the given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the dictionary containsn <paramref name="key"/>; otherwise, false.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Gets the collection of keys in the dictionary.
        /// </summary>
        ICollection<TKey> Indexes { get; }

        /// <summary>
        /// Replaces a key while keeping the value.
        /// </summary>
        /// <param name="oldKey">The old key.</param>
        /// <param name="newKey">The new key.</param>
        void ChangeKey(TKey oldKey, TKey newKey);
    }

    /// <summary>
    /// Represents a dictionary of key type <typeparamref name="TKey"/> and value type <typeparamref name="TValue"/> that can be sealed.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface ISealableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISealableDictionary, IDictionaryIndex<TKey>
        where TKey : notnull
    {
        /// <summary>
        /// Gets a value indicating whether the dictionary contains the given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the dictionary containsn <paramref name="key"/>; otherwise, false.</returns>
        new bool ContainsKey(TKey key);

        /// <summary>
        /// Clones the dictionary, returning an unsealed copy.
        /// </summary>
        /// <returns>The copy.</returns>
        ISealableDictionary<TKey, TValue> CloneUnsealed();

        /// <summary>
        /// Merges the dictionary with keys and values from another.
        /// </summary>
        /// <param name="other">The other dictionary.</param>
        void Merge(ISealableDictionary<TKey, TValue> other);

        /// <summary>
        /// Merges the dictionary with keys and values from another.
        /// This version ignores key conflicts.
        /// </summary>
        /// <param name="other">The other dictionary.</param>
        void MergeWithConflicts(ISealableDictionary<TKey, TValue> other);
    }

    /// <summary>
    /// Represents a dictionary of key type <typeparamref name="TKey"/> and value type <typeparamref name="TValue"/> that can be sealed.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [Serializable]
    public class SealableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISealableDictionary<TKey, TValue>
        where TKey : notnull
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="SealableDictionary{TKey, TValue}"/> class.
        /// </summary>
        public SealableDictionary()
            : base()
        {
        }

        /*
        /// <summary>
        /// Initializes a new instance of the <see cref="SealableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="info">Data to deserialize the content of <paramref name="context"/>.</param>
        /// <param name="context">Data source.</param>
        protected SealableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        */
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the dictionary is sealed.
        /// </summary>
        public bool IsSealed { get; private set; }

        /// <summary>
        /// Gets the collection of keys in the dictionary.
        /// </summary>
        public ICollection<TKey> Indexes
        {
            get
            {
                IList<TKey> Result = new List<TKey>();
                foreach (KeyValuePair<TKey, TValue> Item in this)
                    Result.Add(Item.Key);

                return Result;
            }
        }
        #endregion

        #region Client Interface
        /// <summary>
        /// Seals the dictionary.
        /// </summary>
        public void Seal()
        {
            if (IsSealed)
                throw new InvalidOperationException("Sealing should be done only once");

            IsSealed = true;
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public new void Add(TKey key, TValue value)
        {
            if (IsSealed)
                throw new InvalidOperationException("A sealed collection cannot be modified");

            if (ContainsKey(key))
                throw new InvalidOperationException("A key must not be added more than once");

            base.Add(key, value);
        }

        /// <summary>
        /// Clones the dictionary, returning an unsealed copy.
        /// </summary>
        /// <returns>The copy.</returns>
        public ISealableDictionary<TKey, TValue> CloneUnsealed()
        {
            SealableDictionary<TKey, TValue> CloneTable = new SealableDictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> Entry in this)
                CloneTable.Add(Entry.Key, Entry.Value);

            return CloneTable;
        }

        /// <summary>
        /// Replaces a key while keeping the value.
        /// </summary>
        /// <param name="oldKey">The old key.</param>
        /// <param name="newKey">The new key.</param>
        public void ChangeKey(TKey oldKey, TKey newKey)
        {
            if (!ContainsKey(oldKey))
                throw new InvalidOperationException("The collection must contain the changed key");

            TValue EntryValue = this[oldKey];
            Remove(oldKey);

            if (ContainsKey(newKey))
                throw new InvalidOperationException("The collection must not contain the new key already");

            Add(newKey, EntryValue);
        }

        /// <summary>
        /// Merges the dictionary with keys and values from another.
        /// </summary>
        /// <param name="other">The other dictionary.</param>
        public void Merge(ISealableDictionary<TKey, TValue> other)
        {
            Contract.Requires(other != null);

            if (IsSealed)
                throw new InvalidOperationException("A sealed collection cannot be modified");

            foreach (KeyValuePair<TKey, TValue> Item in other!)
            {
                if (ContainsKey(Item.Key))
                    throw new InvalidOperationException("Two merged collections must not contain the same key");

                base.Add(Item.Key, Item.Value);
            }
        }

        /// <summary>
        /// Merges the dictionary with keys and values from another.
        /// This version ignores key conflicts.
        /// </summary>
        /// <param name="other">The other dictionary.</param>
        public void MergeWithConflicts(ISealableDictionary<TKey, TValue> other)
        {
            Contract.Requires(other != null);

            if (IsSealed)
                throw new InvalidOperationException("A sealed collection cannot be modified");

            foreach (KeyValuePair<TKey, TValue> Item in other!)
                if (!ContainsKey(Item.Key))
                    base.Add(Item.Key, Item.Value);
        }
        #endregion
    }
}
