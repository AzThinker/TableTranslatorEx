using TableTranslatorEx.Model;
using TableTranslatorEx.Model.ColumnConfigurations.Identity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class BadTypeSeededIdentityProfile : TranslationProfile
    {
        protected override void Configure()
        {
            AddTranslation<int>(new TranslationSettings(new BadTypeSeededIdentityColumnConfiguration(true, true)))
                .AddColumnConfiguration(x => x * 10, new ColumnConfigurationSettings<int> { ColumnName = "Times10" });
        }

        public class BadTypeSeededIdentityColumnConfiguration : SeededIdentityColumnConfiguration<bool>
        {
            public BadTypeSeededIdentityColumnConfiguration(bool identitySeed, bool identityIncrement) : base(identitySeed, identityIncrement)
            {
            }

            public BadTypeSeededIdentityColumnConfiguration(bool identityIncrement, bool identitySeed, string columnName) : base(identityIncrement, identitySeed, columnName)
            {
            }
        }
    }
}