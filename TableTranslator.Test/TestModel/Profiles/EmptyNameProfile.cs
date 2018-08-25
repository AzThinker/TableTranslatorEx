using TableTranslatorEx.Model;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class EmptyNameProfile : TranslationProfile
    {
        public override string ProfileName
        {
            get { return string.Empty; }
        }

        protected override void Configure()
        {
        }
    }
}