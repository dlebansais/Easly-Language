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
        public TypeEntity(Type TypeInfo)
        {
            this.TypeInfo = TypeInfo;

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

        private void RecursiveGetMethods(Type TypeInfo, HashtableEx<string, MethodInfo> Result)
        {
            foreach (MethodInfo Item in TypeInfo.GetMethods())
                if (!Result.ContainsKey(Item.Name))
                    Result.Add(Item.Name, Item);

            if (TypeInfo.BaseType != null)
                RecursiveGetMethods(TypeInfo.BaseType, Result);

            foreach (Type Item in TypeInfo.GetInterfaces())
                RecursiveGetMethods(Item, Result);
        }

        private void RecursiveGetProperties(Type TypeInfo, HashtableEx<string, PropertyInfo> Result)
        {
            foreach (PropertyInfo Item in TypeInfo.GetProperties())
                if (!Result.ContainsKey(Item.Name))
                    Result.Add(Item.Name, Item);

            if (TypeInfo.BaseType != null)
                RecursiveGetProperties(TypeInfo.BaseType, Result);

            foreach (Type Item in TypeInfo.GetInterfaces())
                RecursiveGetProperties(Item, Result);
        }

        private string OverloadedName(IHashtableIndex<string> Table, string Name)
        {
            if (Table.ContainsKey(Name))
            {
                int i = 1;
                while (Table.ContainsKey(Name + i.ToString()))
                    i++;

                return Name + i.ToString();
            }
            else
                return Name;
        }

        public static TypeEntity BuiltTypeEntity(Type t)
        {
            TypeEntity Result;

            if (!SpecializedTypeEntityInternal.SingletonSet.ContainsKey(t))
            {
                Type[] GenericArguments = new Type[] { t };
                Type BoundType = typeof(SpecializedTypeEntity<>).MakeGenericType(GenericArguments);
                PropertyInfo p = BoundType.GetProperty("Singleton");
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
        public ProcedureEntity Procedure(string Name)
        {
            return Procedures[Name];
        }

        public FunctionEntity Function(string Name)
        {
            return Functions[Name];
        }

        public PropertyEntity Property(string Name)
        {
            return Properties[Name];
        }

        public object CreateInstance()
        {
            return TypeInfo.Assembly.CreateInstance(TypeInfo.FullName);
        }
        #endregion
    }
}
