using System.Reflection;

namespace Easly
{
    public class FeatureEntity : Entity
    {
        #region Init
        public FeatureEntity(MemberInfo featureInfo)
        {
            FeatureInfo = featureInfo;
        }

        protected MemberInfo FeatureInfo { get; private set; }
        #endregion
    }
}
