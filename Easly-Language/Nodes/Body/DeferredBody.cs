namespace BaseNode
{
    public interface IDeferredBody : IBody
    {
    }

    [System.Serializable]
    public class DeferredBody : Body, IDeferredBody
    {
    }
}
