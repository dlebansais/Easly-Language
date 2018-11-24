using System.Reflection;

namespace Easly
{
    public class IndexerEntity : FeatureEntity
    {
        #region Init
        public IndexerEntity(MemberInfo FeatureInfo)
            : base(FeatureInfo)
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
