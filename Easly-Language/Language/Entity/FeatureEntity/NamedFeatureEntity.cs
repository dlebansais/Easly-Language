namespace Easly
{
    using System.Reflection;

    public interface INamedFeatureEntity
    {
        string Name { get; }
    }

    public class NamedFeatureEntity : FeatureEntity, INamedFeatureEntity
    {
        #region Init
        public NamedFeatureEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return FeatureInfo.Name; }
        }
        #endregion
    }
}
