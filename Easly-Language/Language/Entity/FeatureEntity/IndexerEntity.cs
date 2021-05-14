namespace Easly
{
    using System.Reflection;

    /// <summary>
    /// Represents an entity for indexers.
    /// </summary>
    public class IndexerEntity : FeatureEntity
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexerEntity"/> class.
        /// </summary>
        /// <param name="featureInfo">The feature information from reflection.</param>
        public IndexerEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity of the return type.
        /// </summary>
        public TypeEntity Type
        {
            get
            {
                MethodInfo AsPropertyInfo = (MethodInfo)FeatureInfo;
                return TypeEntity.BuiltTypeEntity(AsPropertyInfo.ReturnType);
            }
        }
        #endregion
    }
}
