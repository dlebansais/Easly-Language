namespace BaseNode
{
    public interface IAssignmentTypeArgument : ITypeArgument
    {
        IIdentifier ParameterIdentifier { get; }
        IObjectType Source { get; }
    }

    [System.Serializable]
    public class AssignmentTypeArgument : TypeArgument, IAssignmentTypeArgument
    {
        public virtual IIdentifier ParameterIdentifier { get; set; }
        public virtual IObjectType Source { get; set; }
    }
}
