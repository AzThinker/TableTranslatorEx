using TableTranslatorEx.Model;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class NullNameProfile : TranslationProfile
    {
        public override string ProfileName
        {
            get { return null; }
        }

        protected override void Configure()
        {
            //throw new NotImplementedException();
        }
    }
}