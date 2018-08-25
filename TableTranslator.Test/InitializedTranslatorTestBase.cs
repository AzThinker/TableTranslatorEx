using NUnit.Framework;

namespace TableTranslatorEx.Test
{
    public class InitializedTranslatorTestBase
    {
        [SetUp]
        public virtual void Setup()
        {
            TestHelper.ResetTranslator();
        }
    }
}