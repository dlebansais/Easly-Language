namespace Easly;

using System.Diagnostics;
using NotNullReflection;
using Contracts;

/// <summary>
/// The base class for entities.
/// </summary>
public abstract class Entity
{
    #region Init
    /// <summary>
    /// Gets the entity of a provided object.
    /// See entityof().
    /// </summary>
    /// <param name="object">The object.</param>
    /// <returns>The entity of a provided object <paramref name="object"/>.</returns>
    public static TypeEntity FromThis(object @object)
    {
        Contract.RequireNotNull(@object, out object Object);

        return TypeEntity.BuiltTypeEntity(Type.FromGetType(Object));
    }

    /// <summary>
    /// Gets the entity of the current object when called in a static constructor.
    /// </summary>
    /// <returns>The entity of the current object.</returns>
    public static TypeEntity FromStaticConstructor()
    {
        StackTrace Trace = new();

#if DEBUG
        StackFrame Frame = Contract.NullSupressed(Trace.GetFrame(1));
        var Method = MethodBase.FromStackFrame(Frame);
#else
        StackFrame? Frame = Trace.GetFrame(1);
        var Method = MethodBase.FromStackFrame(Frame ?? throw new System.NotSupportedException("Stack frame not available in release mode"));
#endif

        Type CallerType = Method.DeclaringType;

        if (CallerType.IsGenericType)
            throw new System.NotSupportedException("Static generic types not supported");

        return TypeEntity.BuiltTypeEntity(CallerType);
    }
    #endregion
}
