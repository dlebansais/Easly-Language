using System;

namespace Easly
{
    public interface IOptionalReference
    {
        bool IsAssigned { get; }
        bool HasItem { get; }
        object Item { get; }
        void Assign();
        void Unassign();
    }

    public interface IOptionalReference<T>
        where T: class
    {
        bool IsAssigned { get; }
        bool HasItem { get; }
        T Item { get; }
        void Assign();
        void Unassign();
    }

    [PolySerializer.Serializable]
    public class OptionalReference<T> : IOptionalReference<T>, IOptionalReference
        where T : class
    {
        #region Init
        public OptionalReference()
        {
            _Item = null;
        }

        public OptionalReference(T initialValue)
        {
            _Item = initialValue;
        }
        #endregion

        #region Properties
        public bool IsAssigned { get; private set; } = false;
        public bool HasItem { get { return _Item != null; } }

        [PolySerializer.Serializable(Condition = nameof(IsAssigned))]
        public T Item
        {
            get
            {
                if (_Item == null)
                    throw new InvalidOperationException();
                else
                    return _Item;
            }
            set
            {
                if (value != null)
                {
                    _Item = value;
                    IsAssigned = true;
                }
                else
                    throw new InvalidOperationException();
            }
        }
        object IOptionalReference.Item { get { return Item; } }

        [PolySerializer.Serializable(Exclude = true)]
        public T _Item;
        #endregion

        #region Unassigning
        public void Assign()
        {
            if (_Item == null)
                throw new InvalidOperationException();

            IsAssigned = true;
        }

        public void Unassign()
        {
            IsAssigned = false;
        }
        #endregion
    }
}
