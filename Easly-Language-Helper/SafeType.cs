namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Helper class for null-aware operations with types.
    /// </summary>
    internal static class SafeType
    {
        /// <summary>
        /// Gets the fully qualified name of the type, including its namespace but not its assembly.
        /// The type must not be a generic type parameter, an array type, pointer type, or byref type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The fully qualified name of the type, including its namespace but not its assembly.</returns>
        public static string FullName(Type type)
        {
            Debug.Assert(!IsGenericTypeParameter(type));
            Debug.Assert(!type.IsArray);
            Debug.Assert(!type.IsPointer);
            Debug.Assert(!type.IsByRef);
            Debug.Assert(!HasUnsolvedGenericParameters(type));

            string? Result = type.FullName;
            Debug.Assert(Result != null);

            return Result!;
        }

        /// <summary>
        /// Returns true for a generic type that is not a generic type definition but contains unresolved type parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if <paramref name="type"/> is a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
        public static bool HasUnsolvedGenericParameters(Type type)
        {
            if (!type.IsGenericType || type.IsGenericTypeDefinition)
                return false;

            bool Result = false;

            foreach (Type Item in type.GetGenericArguments())
                Result |= Item.IsGenericParameter;

            return Result;
        }

        /// <summary>
        /// Returns true for a generic type parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if <paramref name="type"/> is a generic type parameter.</returns>
        public static bool IsGenericTypeParameter(Type type)
        {
#if NETFRAMEWORK
            return type.IsGenericParameter;
#else
            return type.IsGenericTypeParameter;
#endif
        }
    }
}
