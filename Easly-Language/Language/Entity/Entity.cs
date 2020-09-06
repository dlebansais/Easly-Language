namespace Easly
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public abstract class Entity
    {
        #region Init
        public static TypeEntity FromThis(object o) // entityof()
        {
            return TypeEntity.BuiltTypeEntity(o.GetType());
        }

        public static TypeEntity FromStaticConstructor()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            MethodBase mb = sf.GetMethod();
            Type CallerType = mb.DeclaringType;
            if (CallerType.IsGenericType)
                throw new InvalidOperationException();

            return TypeEntity.BuiltTypeEntity(CallerType);
        }
        #endregion
    }
}
