namespace Easly
{
    using System;
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
        public IndexerEntity(MethodInfo featureInfo)
            : base(featureInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexerEntity"/> class.
        /// </summary>
        /// <param name="featureInfo">The feature information from reflection.</param>
        public IndexerEntity(PropertyInfo featureInfo)
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
                TypeEntity? Result = null!;

                switch (FeatureInfo)
                {
                    case PropertyInfo AsPropertyInfo:
                        Result = TypeEntity.BuiltTypeEntity(AsPropertyInfo.PropertyType);
                        break;
                    case MethodInfo AsMethodInfo:
                        Result = TypeEntity.BuiltTypeEntity(AsMethodInfo.ReturnType);
                        break;
                }

                System.Diagnostics.Debug.Assert(Result != null);

                return Result!;
            }
        }
        #endregion
    }
}
