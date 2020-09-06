#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
