namespace Easly
{
    using System.Reflection;

    public class FeatureEntity : Entity
    {
        #region Init
        public FeatureEntity(MemberInfo featureInfo)
        {
            FeatureInfo = featureInfo;
        }
        #endregion

        #region Properties
        protected MemberInfo FeatureInfo { get; private set; }
        #endregion
    }
}
