using TableTranslatorEx.Model;
using TableTranslatorEx.Model.ColumnConfigurations.Identity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class ColumnConfigurationTestProfile : TranslationProfile
    { 
        protected override void Configure()
        {
            AddTranslation<bool>(new TranslationSettings("WithoutIdentityColumn"))
                .AddColumnConfiguration(x => x)
                .AddColumnConfiguration(x => x.ToString())
                .AddColumnConfiguration(2 + 2);

            AddTranslation<bool>(new TranslationSettings(new GuidIdentityColumnConfiguration(), "WithIdentityColumn"))
                .AddColumnConfiguration(x => x)
                .AddColumnConfiguration(x => x.ToString())
                .AddColumnConfiguration(2 + 2);
        }
    }
}