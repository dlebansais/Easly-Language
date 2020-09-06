namespace Easly
{
    using System;

    public class SpecializedTypeEntity<T> : TypeEntity
    {
        #region Init
        private SpecializedTypeEntity(Type t)
            : base(t)
        {
        }
        #endregion

        #region Properties
        public static SpecializedTypeEntity<T> Singleton
        {
            get
            {
                Type t = typeof(T);

                if (!SpecializedTypeEntityInternal.SingletonSet.ContainsKey(t))
                {
                    SpecializedTypeEntity<T> NewEntity = new SpecializedTypeEntity<T>(t);
                    SpecializedTypeEntityInternal.SingletonSet.Add(t, NewEntity);
                }

                return (SpecializedTypeEntity<T>)SpecializedTypeEntityInternal.SingletonSet[t];
            }
        }
        #endregion
    }
}
