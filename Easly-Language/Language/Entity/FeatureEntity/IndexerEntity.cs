namespace Easly
{
    using System.Reflection;

    public class IndexerEntity : FeatureEntity
    {
        #region Init
        public IndexerEntity(MemberInfo featureInfo)
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
