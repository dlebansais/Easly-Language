using System.Reflection;

namespace Easly
{
    public interface INamedFeatureEntity
    {
        string Name { get; }
    }

    public class NamedFeatureEntity : FeatureEntity, INamedFeatureEntity
    {
        #region Init
        public NamedFeatureEntity(MemberInfo FeatureInfo)
            : base(FeatureInfo)
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
