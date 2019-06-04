namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public interface ISealableList
    {
        bool IsSealed { get; }
        void Seal();
    }

    public interface ISealableList<TItem> : IList<TItem>, ISealableList
    {
        void AddRange(IList<TItem> other);
        bool Exists(Predicate<TItem> match);
        SealableList<TItem> CloneUnsealed();
    }

    public class SealableList<TItem> : List<TItem>, ISealableList<TItem>
    {
        public SealableList()
            : base()
        {
        }

        public bool IsSealed { get; private set; }

        public void Seal()
        {
            Debug.Assert(!IsSealed);

            IsSealed = true;
        }

        public new void Add(TItem item)
        {
            Debug.Assert(!IsSealed);

            base.Add(item);
        }

        public SealableList<TItem> CloneUnsealed()
        {
            SealableList<TItem> CloneTable = new SealableList<TItem>();

            foreach (TItem Item in this)
                CloneTable.Add(Item);

            return CloneTable;
        }

        public void AddRange(IList<TItem> other)
        {
            Debug.Assert(!IsSealed);

            base.AddRange(other);
        }
    }
}
