namespace BaseNode
{
    public interface IPreprocessorExpression : IExpression
    {
        PreprocessorMacro Value { get; }
    }

    [System.Serializable]
    public class PreprocessorExpression : Expression, IPreprocessorExpression
    {
        public virtual PreprocessorMacro Value { get; set; }
    }
}
