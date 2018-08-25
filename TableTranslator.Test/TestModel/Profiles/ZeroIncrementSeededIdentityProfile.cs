using TableTranslatorEx.Model;
using TableTranslatorEx.Model.ColumnConfigurations.Identity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class ZeroIncrementSeededIdentityProfile : TranslationProfile
    {
        protected override void Configure()
        {
            AddTranslation<int>(new TranslationSettings(new LongSeededIdentityColumnConfiguration(identityIncrement: 0)))
                .AddColumnConfiguration(x => x * 10, new ColumnConfigurationSettings<int> { ColumnName = "Times10" });
        }
    }
}