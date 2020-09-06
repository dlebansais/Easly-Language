namespace Easly
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    public abstract class Entity
    {
        #region Init
        public static TypeEntity FromThis(object o) // entityof()
        {
            Contract.Requires(o != null);

            return TypeEntity.BuiltTypeEntity(o!.GetType());
        }

        public static TypeEntity FromStaticConstructor()
        {
            StackTrace Trace = new StackTrace();

            StackFrame Frame = Trace.GetFrame(1) !;
            MethodBase? Method = Frame.GetMethod() !;
            Type? CallerType = Method.DeclaringType !;

            if (CallerType.IsGenericType)
                throw new InvalidOperationException();

            return TypeEntity.BuiltTypeEntity(CallerType);
        }
        #endregion
    }
}
