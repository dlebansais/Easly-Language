namespace Easly;

using System;

/// <summary>
/// Represents an optional reference.
/// </summary>
public interface IOptionalReference
{
    /// <summary>
    /// Gets a value indicating whether the reference is assigned.
    /// </summary>
    bool IsAssigned { get; }

    /// <summary>
    /// Gets a value indicating whether there is a reference to assign.
    /// </summary>
    bool HasItem { get; }

    /// <summary>
    /// Gets or sets the reference.
    /// </summary>
    object Item { get; set; }

    /// <summary>
    /// Assigns the reference.
    /// </summary>
    void Assign();

    /// <summary>
    /// Unassigns the reference.
    /// </summary>
    void Unassign();

    /// <summary>
    /// Clears the reference.
    /// </summary>
    void Clear();
}

/// <summary>
/// Represents an optional reference to an object of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the object.</typeparam>
public interface IOptionalReference<T>
    where T : class
{
    /// <summary>
    /// Gets a value indicating whether the reference is assigned.
    /// </summary>
    bool IsAssigned { get; }

    /// <summary>
    /// Gets a value indicating whether there is a reference to assign.
    /// </summary>
    bool HasItem { get; }

    /// <summary>
    /// Gets or sets the reference.
    /// </summary>
    T Item { get; set; }

    /// <summary>
    /// Assigns the reference.
    /// </summary>
    void Assign();

    /// <summary>
    /// Unassigns the reference.
    /// </summary>
    void Unassign();

    /// <summary>
    /// Clears the reference.
    /// </summary>
    void Clear();
}

/// <summary>
/// Represents an optional reference to an object of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the object.</typeparam>
[PolySerializer.Serializable]
public class OptionalReference<T> : IOptionalReference<T>, IOptionalReference
    where T : class
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="OptionalReference{T}"/> class.
    /// </summary>
    public OptionalReference()
    {
        ItemInternal = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionalReference{T}"/> class.
    /// </summary>
    /// <param name="initialValue">The initial item value.</param>
    public OptionalReference(T initialValue)
    {
        ItemInternal = initialValue;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the reference is assigned.
    /// </summary>
    public bool IsAssigned { get; private set; }

    /// <summary>
    /// Gets a value indicating whether there is object reference to assign.
    /// </summary>
    public bool HasItem { get { return ItemInternal is not null; } }

    /// <summary>
    /// Gets or sets the reference.
    /// </summary>
    [PolySerializer.Serializable(Condition = nameof(IsAssigned))]
    public T Item
    {
        get
        {
            if (ItemInternal is null)
                throw new InvalidOperationException();
            else
                return ItemInternal;
        }
        set
        {
            if (value is not null)
            {
                ItemInternal = value;
                IsAssigned = true;
            }
            else
                throw new InvalidOperationException();
        }
    }

    /// <inheritdoc/>
    object IOptionalReference.Item
    {
        get
        {
            return Item;
        }
        set
        {
            if (value is T AsItem)
            {
                ItemInternal = AsItem;
                IsAssigned = true;
            }
            else
                throw new InvalidOperationException();
        }
    }

    [PolySerializer.Serializable(Exclude = true)]
    private T? ItemInternal;
    #endregion

    #region Assignment
    /// <summary>
    /// Assigns the reference.
    /// </summary>
    public void Assign()
    {
        if (ItemInternal is null)
            throw new InvalidOperationException();

        IsAssigned = true;
    }

    /// <summary>
    /// Unassigns the reference.
    /// </summary>
    public void Unassign()
    {
        IsAssigned = false;
    }

    /// <summary>
    /// Clears the reference.
    /// </summary>
    public void Clear()
    {
        IsAssigned = false;
        ItemInternal = null;
    }
    #endregion
}
