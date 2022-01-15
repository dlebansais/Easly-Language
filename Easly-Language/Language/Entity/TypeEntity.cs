namespace Easly;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Contracts;

/// <summary>
/// Represents the entity of a type.
/// </summary>
public abstract class TypeEntity : Entity
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeEntity"/> class.
    /// </summary>
    /// <param name="typeInfo">The type information from reflection.</param>
    public TypeEntity(Type typeInfo)
    {
        Contract.RequireNotNull(typeInfo, out Type TypeInfo);

        this.TypeInfo = TypeInfo;
        Name = typeInfo.Name;
        FullName = Contract.NullSupressed(typeInfo.FullName);

        SealableDictionary<string, MethodInfo> FlattenedMethodList = new();
        RecursiveGetMethods(TypeInfo, FlattenedMethodList);

        Features = new List<FeatureEntity>();
        Procedures = new SealableDictionary<string, ProcedureEntity>();
        Functions = new SealableDictionary<string, FunctionEntity>();

        foreach (KeyValuePair<string, MethodInfo> Item in FlattenedMethodList)
            if (Item.Value.ReturnType == typeof(void))
            {
                ProcedureEntity FeatureEntity = new ProcedureEntity(Item.Value);
                Features.Add(FeatureEntity);
                Procedures.Add(OverloadedName(Procedures, Item.Key), FeatureEntity);
            }
            else
            {
                FunctionEntity FeatureEntity = new FunctionEntity(Item.Value);
                Features.Add(FeatureEntity);
                Functions.Add(OverloadedName(Functions, Item.Key), FeatureEntity);
            }

        Procedures.Seal();
        Functions.Seal();

        Indexer = new OnceReference<IndexerEntity>();

        SealableDictionary<string, PropertyInfo> FlattenedPropertyList = new();
        RecursiveGetProperties(TypeInfo, FlattenedPropertyList);

        Properties = new SealableDictionary<string, PropertyEntity>();

        foreach (KeyValuePair<string, PropertyInfo> Item in FlattenedPropertyList)
        {
            PropertyEntity FeatureEntity = new(Item.Value);
            Features.Add(FeatureEntity);
            Properties.Add(OverloadedName(Properties, Item.Key), FeatureEntity);
        }

        Properties.Seal();

        Parents = new List<TypeEntity>();
        Ancestors = new List<TypeEntity>();

        if (TypeInfo.BaseType is not null && TypeInfo.BaseType != typeof(object))
        {
            TypeEntity ParentEntity = BuiltTypeEntity(TypeInfo.BaseType);

            Parents.Add(ParentEntity);
            Ancestors.AddRange(ParentEntity.Ancestors);
            Ancestors.Add(ParentEntity);
        }
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the type information from reflection.
    /// </summary>
    public Type TypeInfo { get; private set; }

    /// <summary>
    /// Gets the type name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the table of procedures of the type.
    /// </summary>
    public SealableDictionary<string, ProcedureEntity> Procedures { get; private set; }

    /// <summary>
    /// Gets the table of functions of the type.
    /// </summary>
    public SealableDictionary<string, FunctionEntity> Functions { get; private set; }

    /// <summary>
    /// Gets the indexer of the type.
    /// </summary>
    public OnceReference<IndexerEntity> Indexer { get; private set; }

    /// <summary>
    /// Gets the table of properties of the type.
    /// </summary>
    public SealableDictionary<string, PropertyEntity> Properties { get; private set; }

    /// <summary>
    /// Gets the list of features of the type.
    /// </summary>
    public List<FeatureEntity> Features { get; private set; }

    /// <summary>
    /// Gets the list of parents of the type.
    /// </summary>
    public List<TypeEntity> Parents { get; private set; }

    /// <summary>
    /// Gets the list of ancestors of the type.
    /// </summary>
    public List<TypeEntity> Ancestors { get; private set; }
    #endregion

    #region Client Interface
    /// <summary>
    /// Gets the entity from a given type <paramref name="t"/>.
    /// </summary>
    /// <param name="t">The type.</param>
    /// <returns>The entity of the type <paramref name="t"/>.</returns>
    public static TypeEntity BuiltTypeEntity(Type t)
    {
        TypeEntity Result;

        if (!SpecializedTypeEntityInternal.SingletonSet.ContainsKey(t))
        {
            Type[] GenericArguments = new Type[] { t };
            Type BoundType = typeof(SpecializedTypeEntity<>).MakeGenericType(GenericArguments);

            PropertyInfo BoundTypePropertyInfo = Contract.NullSupressed(BoundType.GetProperty(nameof(SpecializedTypeEntity<object>.Singleton)));
            object BoundTypePropertyValue = Contract.NullSupressed(BoundTypePropertyInfo.GetValue(null));

            Result = (TypeEntity)BoundTypePropertyValue;
        }
        else
            Result = SpecializedTypeEntityInternal.SingletonSet[t];

        return Result;
    }

    /// <summary>
    /// Gets the entity of the given procedure.
    /// </summary>
    /// <param name="name">The procedure name.</param>
    /// <returns>The entity of the procedure.</returns>
    public ProcedureEntity Procedure(string name)
    {
        return Procedures[name];
    }

    /// <summary>
    /// Gets the entity of the given function.
    /// </summary>
    /// <param name="name">The function name.</param>
    /// <returns>The entity of the function.</returns>
    public FunctionEntity Function(string name)
    {
        return Functions[name];
    }

    /// <summary>
    /// Gets the entity of the given property.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>The entity of the property.</returns>
    public PropertyEntity Property(string name)
    {
        return Properties[name];
    }

    /*
    /// <summary>
    /// Creates an object of the type associated to this entity.
    /// </summary>
    /// <returns>The created object.</returns>
    public object CreateInstance()
    {
        Activator.CreateInstance(TypeInfo);

        ConstructorInfo[] Constructors = TypeInfo.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        Debug.Assert(Constructors.Length == 1);

        int ParameterCount = Constructors[0].GetParameters().Length;
        object[] Arguments = new object[ParameterCount];

        object Result = Activator.CreateInstance(TypeInfo, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, binder: default, Arguments, culture: default);

        return Result;
    }
    */
    #endregion

    #region Implementation
    private static void RecursiveGetMethods(Type typeInfo, SealableDictionary<string, MethodInfo> result)
    {
        foreach (MethodInfo Item in typeInfo.GetMethods())
            if (!result.ContainsKey(Item.Name))
                result.Add(Item.Name, Item);

        if (typeInfo.BaseType is not null)
            RecursiveGetMethods(typeInfo.BaseType, result);

        foreach (Type Item in typeInfo.GetInterfaces())
            RecursiveGetMethods(Item, result);
    }

    private static void RecursiveGetProperties(Type typeInfo, SealableDictionary<string, PropertyInfo> result)
    {
        foreach (PropertyInfo Item in typeInfo.GetProperties())
            if (!result.ContainsKey(Item.Name))
                result.Add(Item.Name, Item);

        if (typeInfo.BaseType is not null)
            RecursiveGetProperties(typeInfo.BaseType, result);

        foreach (Type Item in typeInfo.GetInterfaces())
            RecursiveGetProperties(Item, result);
    }

    private static string OverloadedName(IDictionaryIndex<string> table, string name)
    {
        /*if (table.ContainsKey(name))
        {
            int i = 1;
            while (table.ContainsKey(name + i.ToString(CultureInfo.InvariantCulture)))
                i++;

            return name + i.ToString(CultureInfo.InvariantCulture);
        }
        else*/
            return name;
    }

    private string FullName;
    #endregion
}
