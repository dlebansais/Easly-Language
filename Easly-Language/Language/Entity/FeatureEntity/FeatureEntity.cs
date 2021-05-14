namespace Easly
{
    using System.Reflection;

    /// <summary>
    /// Represents an entity for features.
    /// </summary>
    public class FeatureEntity : Entity
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEntity"/> class.
        /// </summary>
        /// <param name="featureInfo">The feature information from reflection.</param>
        public FeatureEntity(MemberInfo featureInfo)
        {
            FeatureInfo = featureInfo;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the feature information from reflection.
        /// </summary>
        protected MemberInfo FeatureInfo { get; private set; }
        #endregion
    }
}
