namespace Easly
{
    using System;

    internal class SpecializedTypeEntityInternal
    {
        public static ISealableDictionary<Type, TypeEntity> SingletonSet { get; } = new SealableDictionary<Type, TypeEntity>();
    }
}
