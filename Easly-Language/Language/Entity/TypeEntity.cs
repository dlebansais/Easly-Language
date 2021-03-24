namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    public interface ITypeEntity
    {
        string Name { get; }
    }

    public abstract class TypeEntity : Entity, ITypeEntity
    {
        #region Init
        public TypeEntity(Type typeInfo)
        {
            TypeInfo = typeInfo;

            SealableDictionary<string, MethodInfo> FlattenedMethodList = new SealableDictionary<string, MethodInfo>();
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

            SealableDictionary<string, PropertyInfo> FlattenedPropertyList = new SealableDictionary<string, PropertyInfo>();
            RecursiveGetProperties(TypeInfo, FlattenedPropertyList);

            Properties = new SealableDictionary<string, PropertyEntity>();

            foreach (KeyValuePair<string, PropertyInfo> Item in FlattenedPropertyList)
            {
                PropertyEntity FeatureEntity = new PropertyEntity(Item.Value);
                Features.Add(FeatureEntity);
                Properties.Add(OverloadedName(Properties, Item.Key), FeatureEntity);
            }

            Properties.Seal();

            Parents = new List<TypeEntity>();
            Ancestors = new List<TypeEntity>();

            if (TypeInfo.BaseType != null && TypeInfo.BaseType != typeof(object))
            {
                TypeEntity ParentEntity = BuiltTypeEntity(TypeInfo.BaseType);

                Parents.Add(ParentEntity);
                Ancestors.AddRange(ParentEntity.Ancestors);
                Ancestors.Add(ParentEntity);
            }
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return TypeInfo.Name; }
        }

        public SealableDictionary<string, ProcedureEntity> Procedures { get; private set; }
        public SealableDictionary<string, FunctionEntity> Functions { get; private set; }
        public OnceReference<IndexerEntity> Indexer { get; private set; }
        public SealableDictionary<string, PropertyEntity> Properties { get; private set; }
        public List<FeatureEntity> Features { get; private set; }
        public List<TypeEntity> Parents { get; private set; }
        public List<TypeEntity> Ancestors { get; private set; }
        public Type TypeInfo { get; private set; }
        #endregion

        #region Client Interface
        public static TypeEntity BuiltTypeEntity(Type t)
        {
            TypeEntity Result;

            if (!SpecializedTypeEntityInternal.SingletonSet.ContainsKey(t))
            {
                Type[] GenericArguments = new Type[] { t };
                Type BoundType = typeof(SpecializedTypeEntity<>).MakeGenericType(GenericArguments);

                PropertyInfo BoundTypePropertyInfo = BoundType.GetProperty(nameof(SpecializedTypeEntity<object>.Singleton))!;
                object BoundTypePropertyValue = BoundTypePropertyInfo.GetValue(null)!;

                Result = (TypeEntity)BoundTypePropertyValue;
            }
            else
                Result = SpecializedTypeEntityInternal.SingletonSet[t];

            return Result;
        }

        public ProcedureEntity Procedure(string name)
        {
            return Procedures[name];
        }

        public FunctionEntity Function(string name)
        {
            return Functions[name];
        }

        public PropertyEntity Property(string name)
        {
            return Properties[name];
        }

        public object CreateInstance()
        {
            string FullName = TypeInfo.FullName !;
            return TypeInfo.Assembly.CreateInstance(FullName)!;
        }
        #endregion

        #region Implementation
        private static void RecursiveGetMethods(Type typeInfo, SealableDictionary<string, MethodInfo> result)
        {
            foreach (MethodInfo Item in typeInfo.GetMethods())
                if (!result.ContainsKey(Item.Name))
                    result.Add(Item.Name, Item);

            if (typeInfo.BaseType != null)
                RecursiveGetMethods(typeInfo.BaseType, result);

            foreach (Type Item in typeInfo.GetInterfaces())
                RecursiveGetMethods(Item, result);
        }

        private static void RecursiveGetProperties(Type typeInfo, SealableDictionary<string, PropertyInfo> result)
        {
            foreach (PropertyInfo Item in typeInfo.GetProperties())
                if (!result.ContainsKey(Item.Name))
                    result.Add(Item.Name, Item);

            if (typeInfo.BaseType != null)
                RecursiveGetProperties(typeInfo.BaseType, result);

            foreach (Type Item in typeInfo.GetInterfaces())
                RecursiveGetProperties(Item, result);
        }

        private static string OverloadedName(IDictionaryIndex<string> table, string name)
        {
            if (table.ContainsKey(name))
            {
                int i = 1;
                while (table.ContainsKey(name + i.ToString(CultureInfo.InvariantCulture)))
                    i++;

                return name + i.ToString(CultureInfo.InvariantCulture);
            }
            else
                return name;
        }
        #endregion
    }
}
