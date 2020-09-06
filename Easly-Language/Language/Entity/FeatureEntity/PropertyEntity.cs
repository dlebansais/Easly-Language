namespace Easly
{
    using System.Reflection;

    public class PropertyEntity : NamedFeatureEntity
    {
        #region Init
        public PropertyEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Client Interface
        public TypeEntity Type
        {
            get
            {
                PropertyInfo AsPropertyInfo = (PropertyInfo)FeatureInfo;
                return TypeEntity.BuiltTypeEntity(AsPropertyInfo.PropertyType);
            }
        }

        public object GetValue(object o)
        {
            PropertyInfo AsPropertyInfo = (PropertyInfo)FeatureInfo;
            return AsPropertyInfo.GetValue(o) !;
        }

        public void SetValue(object o, object value)
        {
            PropertyInfo AsPropertyInfo = (PropertyInfo)FeatureInfo;
            AsPropertyInfo.SetValue(o, value);
        }
        #endregion
    }
}
