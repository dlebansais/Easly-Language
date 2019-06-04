namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    public interface ISealableDictionary
    {
        bool IsSealed { get; }
        void Seal();
    }

    public interface IDictionaryIndex<TKey>
    {
        bool ContainsKey(TKey key);
        ICollection<TKey> Indexes { get; }
        void ChangeKey(TKey oldKey, TKey newKey);
    }

    public interface ISealableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISealableDictionary, IDictionaryIndex<TKey>
    {
        new bool ContainsKey(TKey key);
        ISealableDictionary<TKey, TValue> CloneUnsealed();
        void Merge(ISealableDictionary<TKey, TValue> other);
        void MergeWithConflicts(ISealableDictionary<TKey, TValue> other);
    }

    [Serializable]
    public class SealableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISealableDictionary<TKey, TValue>
    {
        public SealableDictionary()
            : base()
        {
        }

        protected SealableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public bool IsSealed { get; private set; }

        public void Seal()
        {
            Debug.Assert(!IsSealed);

            IsSealed = true;
        }

        public new void Add(TKey key, TValue value)
        {
            Debug.Assert(!IsSealed);
            Debug.Assert(!ContainsKey(key));

            base.Add(key, value);
        }

        public ISealableDictionary<TKey, TValue> CloneUnsealed()
        {
            SealableDictionary<TKey, TValue> CloneTable = new SealableDictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> Entry in this)
                CloneTable.Add(Entry.Key, Entry.Value);

            return CloneTable;
        }

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

        public void ChangeKey(TKey oldKey, TKey newKey)
        {
            Debug.Assert(ContainsKey(oldKey));

            TValue EntryValue = this[oldKey];
            Remove(oldKey);

            Debug.Assert(!ContainsKey(newKey));

            Add(newKey, EntryValue);
        }

        public void Merge(ISealableDictionary<TKey, TValue> other)
        {
            Debug.Assert(!IsSealed);

            foreach (KeyValuePair<TKey, TValue> Item in other)
            {
                Debug.Assert(!ContainsKey(Item.Key));

                base.Add(Item.Key, Item.Value);
            }
        }

        public void MergeWithConflicts(ISealableDictionary<TKey, TValue> other)
        {
            Debug.Assert(!IsSealed);

            foreach (KeyValuePair<TKey, TValue> Item in other)
                if (!ContainsKey(Item.Key))
                    base.Add(Item.Key, Item.Value);
        }
    }
}
