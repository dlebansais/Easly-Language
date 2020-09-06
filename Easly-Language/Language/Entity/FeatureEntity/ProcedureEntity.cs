namespace Easly
{
    using System.Reflection;

    public class ProcedureEntity : NamedFeatureEntity
    {
        #region Init
        public ProcedureEntity(MemberInfo featureInfo)
            : base(featureInfo)
        {
        }
        #endregion
    }
}
