namespace BaseNode
{
    public interface IExternBody : IBody
    {
    }

    [System.Serializable]
    public class ExternBody : Body, IExternBody
    {
    }
}
