namespace Easly
{
    using System;

    public interface IOnceReference
    {
        bool IsAssigned { get; }
        object Reference { get; }
    }

    public class OnceReference<T> : IOnceReference
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
                if (!IsAssigned)
                    if (value != null)
                    {
                        ItemInternal = value;
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
            get { return ItemInternal; }
        }

        private T ItemInternal;
        #endregion
    }
}
