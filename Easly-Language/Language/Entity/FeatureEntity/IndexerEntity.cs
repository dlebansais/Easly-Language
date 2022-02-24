namespace Easly;

using System;
using System.Diagnostics;
using NotNullReflection;

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
        TypeEntityConstructor = () => { return TypeEntity.BuiltTypeEntity(((MethodInfo)FeatureInfo).ReturnType); };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexerEntity"/> class.
    /// </summary>
    /// <param name="featureInfo">The feature information from reflection.</param>
    public IndexerEntity(PropertyInfo featureInfo)
        : base(featureInfo)
    {
        TypeEntityConstructor = () => { return TypeEntity.BuiltTypeEntity(((PropertyInfo)FeatureInfo).PropertyType); };
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the entity of the return type.
    /// </summary>
    public TypeEntity Type { get { return TypeEntityConstructor(); } }
    #endregion

    #region Implementation
    private Func<TypeEntity> TypeEntityConstructor;
    #endregion
}
