namespace Easly
{
    using System;

    /// <summary>
    /// Represents the entity of a particular type with generic parameter <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the type parameter.</typeparam>
    public class SpecializedTypeEntity<T> : TypeEntity
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecializedTypeEntity{T}"/> class.
        /// </summary>
        /// <param name="t">The type from reflection.</param>
        private SpecializedTypeEntity(Type t)
            : base(t)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity of the type.
        /// </summary>
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
