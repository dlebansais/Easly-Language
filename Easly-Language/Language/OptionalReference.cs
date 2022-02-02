namespace Easly;

using System;
using Contracts;

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
}

/// <summary>
/// Represents an optional reference to an object of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the object.</typeparam>
[PolySerializer.Serializable]
public class OptionalReference<T> : IOptionalReference<T>, IOptionalReference
    where T : class
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public OptionalReference()
#pragma warning restore SA1600 // Elements should be documented
    {
        ItemInternal = default!;
    }
#endif

    #region Init
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
    /// Gets or sets the reference.
    /// </summary>
    [PolySerializer.Serializable(Condition = nameof(IsAssigned))]
    public T Item
    {
        get { return ItemInternal; }
        set
        {
            Contract.RequireNotNull(value, out T Value);

            ItemInternal = Value;
            IsAssigned = true;
        }
    }

    /// <inheritdoc/>
    object IOptionalReference.Item
    {
        get { return Item; }
        set
        {
            Contract.RequireNotNull(value, out object Value);

            if (Value is T AsItem)
            {
                ItemInternal = AsItem;
                IsAssigned = true;
            }
            else
                throw new InvalidOperationException();
        }
    }

    [PolySerializer.Serializable(Exclude = true)]
    private T ItemInternal;
    #endregion

    #region Assignment
    /// <summary>
    /// Assigns the reference.
    /// </summary>
    public void Assign()
    {
        IsAssigned = true;
    }

    /// <summary>
    /// Unassigns the reference.
    /// </summary>
    public void Unassign()
    {
        IsAssigned = false;
    }
    #endregion
}
