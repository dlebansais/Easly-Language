using System;

namespace Easly
{
    public interface IOptionalReference
    {
        bool IsAssigned { get; }
        object AnyItem { get; }
        void Assign();
        void Unassign();
        void Hack(object Item); //TODO: remove
    }

    public interface IOptionalReference<T> : IOptionalReference
        where T: class
    {
        T Item { get; }
    }

    [PolySerializer.Serializable]
    public class OptionalReference<T> : IOptionalReference, IOptionalReference<T> where T : class
    {
        public void Hack(object Item) //TODO: remove
        {
            this.Item = Item as T;
        }

        #region Init
        public OptionalReference()
        {
            _Item = null;
        }

        public OptionalReference(T InitialValue)
        {
            _Item = InitialValue;
        }
        #endregion

        #region Properties
        public bool IsAssigned { get; private set; } = false;

        public object AnyItem
        {
            get { return Item; }
        }

        [PolySerializer.Serializable(Condition ="IsAssigned")]
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
