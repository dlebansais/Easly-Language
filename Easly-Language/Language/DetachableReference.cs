namespace Easly
{
    using System;

    public class DetachableReference<T>
        where T : class
    {
        #region Properties
        public virtual bool IsAssigned { get; protected set; }

        public virtual T Item
        {
            get
            {
                T Result;

                if (IsAssigned)
                    Result = ItemInternal;
                else
                    throw new InvalidOperationException();

                return Result;
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

        private T ItemInternal;
        #endregion

        #region Detaching
        public virtual void Detach()
        {
            ItemInternal = null;
            IsAssigned = false;
        }
        #endregion
    }
}
