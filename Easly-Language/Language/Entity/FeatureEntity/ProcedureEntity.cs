namespace Easly
{
    using System.Reflection;

    /// <summary>
    /// Represents an entity for procedures.
    /// </summary>
    public class ProcedureEntity : NamedFeatureEntity
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureEntity"/> class.
        /// </summary>
        /// <param name="featureInfo">The feature information from reflection.</param>
        public ProcedureEntity(MethodInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion
    }
}
