using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Easly
{
    public interface IHashtableEx
    {
        bool IsSealed { get; }
        void Seal();
    }

    public interface IHashtableIndex<TKey>
    {
        bool ContainsKey(TKey key);
        ICollection<TKey> Indexes { get; }
        void ChangeKey(TKey oldKey, TKey newKey);
    }

    public interface IHashtableEx<TKey, TValue> : IDictionary<TKey, TValue>, IHashtableEx, IHashtableIndex<TKey>
    {
        new bool ContainsKey(TKey key);
        IHashtableEx<TKey, TValue> CloneUnsealed();
        void Merge(IHashtableEx<TKey, TValue> otherTable);
        void MergeWithConflicts(IHashtableEx<TKey, TValue> otherTable);
    }

    [Serializable]
    public class HashtableEx<TKey, TValue> : Dictionary<TKey, TValue>, IHashtableEx<TKey, TValue>
    {
        public HashtableEx()
            : base()
        {
        }

        protected HashtableEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public bool IsSealed { get; private set; } = false;

        public void Seal()
        {
            if (IsSealed)
                throw new InvalidOperationException();

            IsSealed = true;
        }

        public new void Add(TKey key, TValue value)
        {
            try
            {
                if (IsSealed)
                    throw new InvalidOperationException();

                base.Add(key, value);
            }
            catch
            {
            }
        }

        public IHashtableEx<TKey, TValue> CloneUnsealed()
        {
            HashtableEx<TKey, TValue> CloneTable = new HashtableEx<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> Entry in this)
                CloneTable.Add(Entry.Key, Entry.Value);

            return CloneTable;
        }

        public ICollection<TKey> Indexes
        {
            get
            {
                List<TKey> Result = new List<TKey>();
                foreach (KeyValuePair<TKey, TValue> Item in this)
                    Result.Add(Item.Key);

                return Result;
            }
        }

        public void ChangeKey(TKey oldKey, TKey newKey)
        {
            TValue EntryValue = this[oldKey];
            Remove(oldKey);
            Add(newKey, EntryValue);
        }

        public void Merge(IHashtableEx<TKey, TValue> otherTable)
        {
            if (IsSealed)
                throw new InvalidOperationException();

            foreach (KeyValuePair<TKey, TValue> Item in otherTable)
                base.Add(Item.Key, Item.Value);
        }

        public void MergeWithConflicts(IHashtableEx<TKey, TValue> otherTable)
        {
            if (IsSealed)
                throw new InvalidOperationException();

            foreach (KeyValuePair<TKey, TValue> Item in otherTable)
                if (!base.ContainsKey(Item.Key))
                    base.Add(Item.Key, Item.Value);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
