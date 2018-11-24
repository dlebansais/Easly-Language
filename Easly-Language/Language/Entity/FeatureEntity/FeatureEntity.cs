using System.Reflection;

namespace Easly
{
    public class FeatureEntity : Entity
    {
        #region Init
        public FeatureEntity(MemberInfo FeatureInfo)
        {
            this.FeatureInfo = FeatureInfo;
        }

        protected MemberInfo FeatureInfo { get; private set; }
        #endregion
    }
}
