namespace Easly
{
    using System.Reflection;

    /// <summary>
    /// Represents an entity for named features.
    /// </summary>
    public interface INamedFeatureEntity
    {
        /// <summary>
        /// Gets the feature name.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Represents an entity for named features.
    /// </summary>
    public class NamedFeatureEntity : FeatureEntity, INamedFeatureEntity
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFeatureEntity"/> class.
        /// </summary>
        /// <param name="featureInfo">The feature information from reflection.</param>
        public NamedFeatureEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the feature name.
        /// </summary>
        public string Name
        {
            get { return FeatureInfo.Name; }
        }
        #endregion
    }
}
