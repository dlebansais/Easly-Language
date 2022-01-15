namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using Contracts;

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

        /// <summary>
        /// Gets the assembly-qualified name of the type, which includes the name of the assembly from which this <see cref="Type"/> object was loaded.
        /// The type must not be a generic type parameter.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The assembly-qualified name of the <see cref="Type"/>, which includes the name of the <see cref="Type"/> was loaded.</returns>
        public static string AssemblyQualifiedName(Type type)
        {
            Debug.Assert(!IsGenericTypeParameter(type));

            string? Result = type.AssemblyQualifiedName;
            Debug.Assert(Result != null);

            return Result!;
        }

        /// <summary>
        /// Gets the <see cref="Type"/> with the specified name.
        /// The type must exist.
        /// </summary>
        /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
        /// <returns>The type with the specified name.</returns>
        public static Type GetType(string typeName)
        {
            Type? Result = Type.GetType(typeName);
            Debug.Assert(Result != null);

            return Result!;
        }

        /// <summary>
        /// Gets the type from which <paramref name="type"/> directly inherits.
        /// <paramref name="type"/> must not be <see cref="object"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type from which <paramref name="type"/> directly inherits.</returns>
        public static Type GetBaseType(Type type)
        {
            Type? Result = type.BaseType;
            Debug.Assert(Result != null);

            return Result!;
        }

        /// <summary>
        /// Checks if a type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The string containing the name of the public property to check.</param>
        /// <returns>True if the property exists; otherwise, false.</returns>
        public static bool IsPropertyOf(Type type, string name)
        {
            return type.GetProperty(name) != null;
        }

        /// <summary>
        /// Gets the public property with the specified name. The property must exist.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The string containing the name of the public property to get.</param>
        /// <returns>An object representing the public property with the specified name.</returns>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            PropertyInfo? Result = type.GetProperty(propertyName);
            Debug.Assert(Result != null);
            Debug.Assert(IsPropertyOf(type, propertyName));

            return Result!;
        }

        /// <summary>
        /// Checks if a type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The string containing the name of the public property to check.</param>
        /// <param name="property">If successful, an object representing the public property with the specified name; otherwise, an undefined value.</param>
        /// <returns>True if the property exists; otherwise, false.</returns>
        public static bool CheckAndGetPropertyOf(Type type, string name, out PropertyInfo property)
        {
            PropertyInfo? Result = type.GetProperty(name);

            if (Result != null)
            {
                property = Result;
                return true;
            }

            Contract.Unused(out property);
            return false;
        }

        /// <summary>
        /// Returns the property value of a specified object. Does not support properties that can take a null value.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="obj">The object whose property value will be returned.</param>
        /// <returns>The property value of the specified object.</returns>
        public static T GetPropertyValue<T>(PropertyInfo property, object obj)
            where T : class
        {
            T? Result = property.GetValue(obj) as T;
            Debug.Assert(Result != null);

            return Result!;
        }

        /// <summary>
        /// Creates an instance of the specified type from this assembly using the system activator.
        /// The specified type must have a constructor.
        /// </summary>
        /// <typeparam name="T">The type of the instance to create.</typeparam>
        /// <param name="assembly">The assembly with the specified type.</param>
        /// <param name="name">The <see cref="Type.FullName"/> of the specified type.</param>
        /// <returns>An instance of the specified type created with the default constructor.</returns>
        public static T CreateInstance<T>(Assembly assembly, string name)
            where T : class
        {
            Type? Type = assembly.GetType(name);
            Debug.Assert(Type is not null);

            ConstructorInfo[] Constructors = Type!.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(Constructors.Length == 1);

            int ParameterCount = Constructors[0].GetParameters().Length;
            object[] Arguments = new object[ParameterCount];

            T? Result = Activator.CreateInstance(Type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, binder: default, Arguments, culture: default) as T;
            Debug.Assert(Result is not null);

            return Result!;
        }

        /// <summary>
        /// Creates an instance of the specified type from this assembly using the system activator.
        /// The specified type must have a default constructor.
        /// </summary>
        /// <typeparam name="T">The type of the instance to create.</typeparam>
        /// <param name="assembly">The assembly with the specified type.</param>
        /// <param name="name">The <see cref="Type.FullName"/> of the specified type.</param>
        /// <returns>An instance of the specified type created with the default constructor.</returns>
        public static T CreateInstanceFromDefaultConstructor<T>(Assembly assembly, string name)
            where T : class
        {
            Debug.Assert(TypeHasDefaultConstructor<T>(assembly, name));

            T? Result = assembly.CreateInstance(name) as T;
            Debug.Assert(Result != null);

            return Result!;
        }

        /// <summary>
        /// Checkcs that a type has a default constructor.
        /// </summary>
        /// <typeparam name="T">The type of the instance to create.</typeparam>
        /// <param name="assembly">The assembly with the specified type.</param>
        /// <param name="name">The <see cref="Type.FullName"/> of the specified type.</param>
        /// <returns>True if the specified type has a default constructor; otherwise, false.</returns>
        public static bool TypeHasDefaultConstructor<T>(Assembly assembly, string name)
            where T : class
        {
            Type Type = assembly.GetType(name)!;
            ConstructorInfo[] Constructors = Type.GetConstructors();

            return Constructors.Length == 0 || Constructors[0].GetParameters().Length == 0;
        }

        /// <summary>
        /// Gets elements in the specified collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="list">The collection.</param>
        /// <returns>Elements in the collection.</returns>
        public static IEnumerable<T> Items<T>(System.Collections.IList list)
        {
            foreach (T? Item in list)
            {
                Debug.Assert(Item != null);
                yield return Item!;
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
            Debug.Assert(Result != null);

            return Result!;
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
            T? Result = list[index] as T;
            Debug.Assert(Result != null);

            return Result!;
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
}
