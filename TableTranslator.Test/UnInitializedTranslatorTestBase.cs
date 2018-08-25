using NUnit.Framework;

namespace TableTranslatorEx.Test
{
    [Category("PreInit")]
    public class UnInitializedTranslatorTestBase
    {
        [SetUp]
        public virtual void Setup()
        {
            Translator.Uninitialize();
        }
    }
}