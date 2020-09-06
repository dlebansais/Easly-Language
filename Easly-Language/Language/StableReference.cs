namespace Easly
{
    using System;

    public class StableReference<T>
        where T : class
    {
        #region Properties
        public bool IsAssigned { get; private set; }

        public T Item
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
    }
}
