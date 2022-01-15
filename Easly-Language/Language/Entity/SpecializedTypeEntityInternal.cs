namespace Easly;

using System;

/// <summary>
/// Represents internal info about type entities.
/// </summary>
internal class SpecializedTypeEntityInternal
{
    /// <summary>
    /// Gets the table of type entities already evaluated.
    /// </summary>
    public static ISealableDictionary<Type, TypeEntity> SingletonSet { get; } = new SealableDictionary<Type, TypeEntity>();
}
