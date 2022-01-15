namespace Easly
{
    using System.Reflection;

    /// <summary>
    /// Represents an entity for properties.
    /// </summary>
    public class PropertyEntity : NamedFeatureEntity
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyEntity"/> class.
        /// </summary>
        /// <param name="featureInfo">The feature information from reflection.</param>
        public PropertyEntity(PropertyInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity for the property type.
        /// </summary>
        public TypeEntity Type
        {
            get
            {
                PropertyInfo AsPropertyInfo = (PropertyInfo)FeatureInfo;
                return TypeEntity.BuiltTypeEntity(AsPropertyInfo.PropertyType);
            }
        }
        #endregion

        #region Client Interface
        /// <summary>
        /// Gets the value of the property for an object.
        /// </summary>
        /// <param name="o">The object with the property.</param>
        /// <returns>The property value.</returns>
        public object GetValue(object o)
        {
            PropertyInfo AsPropertyInfo = (PropertyInfo)FeatureInfo;
            object? Value = AsPropertyInfo.GetValue(o);
            return Value!;
        }

        /// <summary>
        /// Sets the value of the property for an object.
        /// </summary>
        /// <param name="o">The object with the property.</param>
        /// <param name="value">The property value.</param>
        public void SetValue(object o, object value)
        {
            PropertyInfo AsPropertyInfo = (PropertyInfo)FeatureInfo;
            AsPropertyInfo.SetValue(o, value);
        }
        #endregion
    }
}
