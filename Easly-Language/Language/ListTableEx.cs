using System;
using System.Collections.Generic;

namespace Easly
{
    public interface IListTableEx
    {
        bool IsSealed { get; }
        void Seal();
    }

    public class ListTableEx<TItem> : List<TItem>, IListTableEx
    {
        public ListTableEx()
            : base()
        {
            _IsSealed = false;
        }

        public bool IsSealed
        {
            get { return _IsSealed; }
        }
        private bool _IsSealed;

        public void Seal()
        {
            if (_IsSealed)
                throw new InvalidOperationException();

            _IsSealed = true;
        }

        public new void Add(TItem Item)
        {
            if (_IsSealed)
                throw new InvalidOperationException();

            base.Add(Item);
        }

        public ListTableEx<TItem> CloneUnsealed()
        {
            ListTableEx<TItem> CloneTable = new ListTableEx<TItem>();

            foreach (TItem Item in this)
                CloneTable.Add(Item);

            return CloneTable;
        }

        public void AddRange(ListTableEx<TItem> OtherTable)
        {
            if (_IsSealed)
                throw new InvalidOperationException();

            base.AddRange(OtherTable);
        }
    }
}
