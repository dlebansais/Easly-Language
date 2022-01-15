namespace Easly
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
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
        /// <param name="obj">The object.</param>
        /// <returns>The entity of a provided object <paramref name="obj"/>.</returns>
        public static TypeEntity FromThis(object obj)
        {
            Contract.RequireNotNull(obj, out object o);

            return TypeEntity.BuiltTypeEntity(o.GetType());
        }

        /// <summary>
        /// Gets the entity of the current object when called in a static constructor.
        /// </summary>
        /// <returns>The entity of the current object.</returns>
        public static TypeEntity FromStaticConstructor()
        {
            StackTrace Trace = new StackTrace();

            StackFrame? Frame = Trace.GetFrame(1);
#if DEBUG
            Debug.Assert(Frame is not null);

            MethodBase? Method = Frame!.GetMethod();
            Debug.Assert(Method is not null);

            Type? DeclaringType = Method!.DeclaringType;
            Debug.Assert(DeclaringType is not null);

            Type CallerType = Method?.DeclaringType!;
#else
            MethodBase? Method = Frame?.GetMethod();
            Type? DeclaringType = Method?.DeclaringType;
            Type CallerType = DeclaringType ?? throw new InvalidOperationException("Stack frame not available in release mode");
#endif

            if (CallerType.IsGenericType)
                throw new NotSupportedException("Static generic types not supported");

            return TypeEntity.BuiltTypeEntity(CallerType);
        }
        #endregion
    }
}
