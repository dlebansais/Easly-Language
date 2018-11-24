using System;

namespace Easly
{
    public class StableReference<T> where T : class
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
                if (value != null)
                {
                    _Item = value;
                    IsAssigned = true;
                }
                else
                    throw new InvalidOperationException();
            }
        }
        private T _Item = default(T);
        #endregion
    }
}
