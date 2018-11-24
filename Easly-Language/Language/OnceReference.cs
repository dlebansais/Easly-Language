using System;

namespace Easly
{
    public interface IOnceReference
    {
        bool IsAssigned { get; }
        object Reference { get; }
    }

    public class OnceReference<T> : IOnceReference where T : class
    {
        #region Properties
        public bool IsAssigned { get; private set; } = false;

        public T Item
        {
            get
            {
                T Result;

                if (IsAssigned)
                    Result = _Item;
                else
                    throw new InvalidOperationException();

                return Result;
            }
            set
            {
                if (!IsAssigned)
                    if (value != null)
                    {
                        _Item = value;
                        IsAssigned = true;
                    }
                    else
                        throw new InvalidOperationException();
                else
                    throw new InvalidOperationException();
            }
        }
        public object Reference 
        {
            get { return _Item; }
        }
        private T _Item = default(T);
        #endregion
    }
}
