using NUnit.Framework;
using TableTranslatorEx.Exceptions;
using TableTranslatorEx.Test.TestModel.Profiles;

namespace TableTranslatorEx.Test.ColumnConfiguration_Test.IdentityColumnConfiguration_Test.SadPath
{
    [TestFixture]
    public class Sad_path : InitializedTranslatorTestBase
    {
        [Test]
        public void DataType_and_return_type_of_get_value_throws_exception_when_not_the_same()
        {
            Translator.AddProfile<WrongTypeIdentityProfile>();
            Assert.Throws<TableTranslatorConfigurationException>(() => Translator.ApplyUpdates());
        }
    }
}