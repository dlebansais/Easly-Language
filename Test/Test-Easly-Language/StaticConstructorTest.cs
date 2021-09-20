namespace TestEaslyLanguage
{
    using Easly;

    public class StaticConstructorTest<T>
        where T : class
    {
        static StaticConstructorTest()
        {
            Entity.FromStaticConstructor();
        }

        public T Item { get; set; } = null!;
    }
}
