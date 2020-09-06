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
        #region Init
        public SealableList()
            : base()
        {
        }
        #endregion

        #region Properties
        public bool IsSealed { get; private set; }
        #endregion

        #region Client Interface
        public void Seal()
        {
            Debug.Assert(!IsSealed, "Sealing should be done only once");

            IsSealed = true;
        }

        public new void Add(TItem item)
        {
            Debug.Assert(!IsSealed, "A sealed collection cannot be modified");

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
            Debug.Assert(!IsSealed, "A sealed collection cannot be modified");

            base.AddRange(other);
        }
        #endregion
    }
}
