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
        void ChangeKey(TKey OldKey, TKey NewKey);
    }

    [Serializable]
    public class HashtableEx<TKey, TValue> : Dictionary<TKey, TValue>, IHashtableEx, IHashtableIndex<TKey>
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

        public HashtableEx<TKey, TValue> CloneUnsealed()
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

        public void ChangeKey(TKey OldKey, TKey NewKey)
        {
            TValue EntryValue = this[OldKey];
            Remove(OldKey);
            Add(NewKey, EntryValue);
        }

        public void Merge(HashtableEx<TKey, TValue> OtherTable)
        {
            if (IsSealed)
                throw new InvalidOperationException();

            foreach (KeyValuePair<TKey, TValue> Item in OtherTable)
                base.Add(Item.Key, Item.Value);
        }

        public void MergeWithConflicts(HashtableEx<TKey, TValue> OtherTable)
        {
            if (IsSealed)
                throw new InvalidOperationException();

            foreach (KeyValuePair<TKey, TValue> Item in OtherTable)
                if (!base.ContainsKey(Item.Key))
                    base.Add(Item.Key, Item.Value);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
