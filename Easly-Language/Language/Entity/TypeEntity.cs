using System;
using System.Collections.Generic;
using System.Reflection;

namespace Easly
{
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

            HashtableEx<string, MethodInfo> FlattenedMethodList = new HashtableEx<string, MethodInfo>();
            RecursiveGetMethods(TypeInfo, FlattenedMethodList);

            Features = new List<FeatureEntity>();
            Procedures = new HashtableEx<string, ProcedureEntity>();
            Functions = new HashtableEx<string, FunctionEntity>();
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

            HashtableEx<string, PropertyInfo> FlattenedPropertyList = new HashtableEx<string, PropertyInfo>();
            RecursiveGetProperties(TypeInfo, FlattenedPropertyList);

            Properties = new HashtableEx<string, PropertyEntity>();
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

        private static void RecursiveGetMethods(Type typeInfo, HashtableEx<string, MethodInfo> result)
        {
            foreach (MethodInfo Item in typeInfo.GetMethods())
                if (!result.ContainsKey(Item.Name))
                    result.Add(Item.Name, Item);

            if (typeInfo.BaseType != null)
                RecursiveGetMethods(typeInfo.BaseType, result);

            foreach (Type Item in typeInfo.GetInterfaces())
                RecursiveGetMethods(Item, result);
        }

        private static void RecursiveGetProperties(Type typeInfo, HashtableEx<string, PropertyInfo> result)
        {
            foreach (PropertyInfo Item in typeInfo.GetProperties())
                if (!result.ContainsKey(Item.Name))
                    result.Add(Item.Name, Item);

            if (typeInfo.BaseType != null)
                RecursiveGetProperties(typeInfo.BaseType, result);

            foreach (Type Item in typeInfo.GetInterfaces())
                RecursiveGetProperties(Item, result);
        }

        private static string OverloadedName(IHashtableIndex<string> table, string name)
        {
            if (table.ContainsKey(name))
            {
                int i = 1;
                while (table.ContainsKey(name + i.ToString()))
                    i++;

                return name + i.ToString();
            }
            else
                return name;
        }

        public static TypeEntity BuiltTypeEntity(Type t)
        {
            TypeEntity Result;

            if (!SpecializedTypeEntityInternal.SingletonSet.ContainsKey(t))
            {
                Type[] GenericArguments = new Type[] { t };
                Type BoundType = typeof(SpecializedTypeEntity<>).MakeGenericType(GenericArguments);
                PropertyInfo p = BoundType.GetProperty(nameof(SpecializedTypeEntity<object>.Singleton));
                Result = (TypeEntity)p.GetValue(null);
            }
            else
                Result = SpecializedTypeEntityInternal.SingletonSet[t];

            return Result;
        }

        public Type TypeInfo { get; private set; }
        #endregion

        #region Properties
        public string Name
        {
            get { return TypeInfo.Name; }
        }

        public HashtableEx<string, ProcedureEntity> Procedures { get; private set; }
        public HashtableEx<string, FunctionEntity> Functions { get; private set; }
        public OnceReference<IndexerEntity> Indexer { get; private set; }
        public HashtableEx<string, PropertyEntity> Properties { get; private set; }
        public List<FeatureEntity> Features { get; private set; }
        public List<TypeEntity> Parents { get; private set; }
        public List<TypeEntity> Ancestors { get; private set; }
        #endregion

        #region Client Interface
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
            return TypeInfo.Assembly.CreateInstance(TypeInfo.FullName);
        }
        #endregion
    }
}
