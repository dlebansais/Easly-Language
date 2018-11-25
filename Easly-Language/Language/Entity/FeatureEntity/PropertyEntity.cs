﻿using System.Reflection;

namespace Easly
{
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
                PropertyInfo AsPropertyInfo = FeatureInfo as PropertyInfo;
                return TypeEntity.BuiltTypeEntity(AsPropertyInfo.PropertyType);
            }
        }

        public object GetValue(object o)
        {
            PropertyInfo AsPropertyInfo = FeatureInfo as PropertyInfo;
            return AsPropertyInfo.GetValue(o);
        }

        public void SetValue(object o, object value)
        {
            PropertyInfo AsPropertyInfo = FeatureInfo as PropertyInfo;
            AsPropertyInfo.SetValue(o, value);
        }
        #endregion
    }
}
