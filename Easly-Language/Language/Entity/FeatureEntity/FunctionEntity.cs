namespace Easly
{
    using System.Reflection;

    public class FunctionEntity : NamedFeatureEntity
    {
        #region Init
        public FunctionEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Properties
        public TypeEntity Type
        {
            get
            {
                MethodInfo AsPropertyInfo = (MethodInfo)FeatureInfo;
                return TypeEntity.BuiltTypeEntity(AsPropertyInfo.ReturnType);
            }
        }
        #endregion
    }
}
