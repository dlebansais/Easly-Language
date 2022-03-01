namespace BaseNodeHelper;

using System.Collections.Generic;
using Contracts;

/// <summary>
/// Helper class for null-aware operations with types.
/// </summary>
internal static class SafeList
{
    /// <summary>
    /// Gets elements in the specified collection.
    /// </summary>
    /// <typeparam name="T">The collection type.</typeparam>
    /// <param name="list">The collection.</param>
    /// <returns>Elements in the collection.</returns>
    public static IEnumerable<T> Items<T>(System.Collections.IList list)
        where T : class
    {
        foreach (T? Item in list)
        {
            yield return Contract.NullSupressed(Item);
        }
    }

    /// <summary>
    /// Gets the element at the specified index in the specified collection.
    /// </summary>
    /// <typeparam name="T">The collection type.</typeparam>
    /// <param name="list">The collection.</param>
    /// <param name="index">The item index.</param>
    /// <returns>The element in the collection.</returns>
    public static T ItemAt<T>(System.Collections.IList list, int index)
        where T : class
    {
        T? Result = list[index] as T;
        return Contract.NullSupressed(Result);
    }

    /// <summary>
    /// Gets the element at the specified index in the specified collection.
    /// </summary>
    /// <typeparam name="T">The collection type.</typeparam>
    /// <param name="list">The collection.</param>
    /// <param name="index">The item index.</param>
    /// <returns>The element in the collection.</returns>
    public static T ItemAt<T>(IList<T> list, int index)
        where T : class
    {
        T? Result = list[index];
        return Contract.NullSupressed(Result);
    }

    /// <summary>
    /// Sets the element at the specified index in the specified collection.
    /// </summary>
    /// <typeparam name="T">The collection type.</typeparam>
    /// <param name="list">The collection.</param>
    /// <param name="index">The item index.</param>
    /// <param name="item">The item to set.</param>
    public static void SetAt<T>(IList<T> list, int index, T item)
        where T : notnull
    {
        list[index] = item;
    }
}
