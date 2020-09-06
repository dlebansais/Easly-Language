namespace Easly
{
    using System;

    public interface IOptionalReference
    {
        bool IsAssigned { get; }
        bool HasItem { get; }
        object Item { get; }
        void Assign();
        void Unassign();
        void Clear();
    }

    public interface IOptionalReference<T>
        where T : class
    {
        bool IsAssigned { get; }
        bool HasItem { get; }
        T Item { get; set; }
        void Assign();
        void Unassign();
        void Clear();
    }

    [PolySerializer.Serializable]
    public class OptionalReference<T> : IOptionalReference<T>, IOptionalReference
        where T : class
    {
        #region Implementation
        [PolySerializer.Serializable(Exclude = true)]
        public T? ItemInternal;
        #endregion

        #region Init
        public OptionalReference()
        {
            ItemInternal = null;
        }

        public OptionalReference(T initialValue)
        {
            ItemInternal = initialValue;
        }
        #endregion

        #region Properties
        public bool IsAssigned { get; private set; }
        public bool HasItem { get { return ItemInternal != null; } }

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

        object IOptionalReference.Item { get { return Item; } }
        #endregion

        #region Assignment
        public void Assign()
        {
            if (ItemInternal == null)
                throw new InvalidOperationException();

            IsAssigned = true;
        }

        public void Unassign()
        {
            IsAssigned = false;
        }

        public void Clear()
        {
            IsAssigned = false;
            ItemInternal = null;
        }
        #endregion
    }
}
