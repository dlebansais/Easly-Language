using System;

namespace Easly
{
    public class DetachableReference<T> where T : class
    {
        #region Properties
        public virtual bool IsAssigned { get; protected set; } = false;

        public virtual T Item
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
        private T _Item = null;
        #endregion

        #region Detaching
        public virtual void Detach()
        {
            _Item = null;
            IsAssigned = false;
        }
        #endregion
    }
}
