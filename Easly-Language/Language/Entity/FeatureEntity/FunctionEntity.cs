using System.Reflection;

namespace Easly
{
    public class FunctionEntity : NamedFeatureEntity
    {
        #region Init
        public FunctionEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Client Interface
        public TypeEntity Type
        {
            get
            {
                MethodInfo AsPropertyInfo = FeatureInfo as MethodInfo;
                return TypeEntity.BuiltTypeEntity(AsPropertyInfo.ReturnType);
            }
        }
        #endregion
    }
}
