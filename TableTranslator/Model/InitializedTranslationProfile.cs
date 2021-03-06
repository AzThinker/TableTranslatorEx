﻿using TableTranslatorEx.Abstract;

namespace TableTranslatorEx.Model
{
    internal class InitializedTranslationProfile
    {
        private string ColumnNamePrefix { get; set; }
        private string ColumnNameSuffix { get; set; }
        private string ProfileName { get; set; }

        internal InitializedTranslationProfile(ICloneable<TranslationProfile> profile)
        {
            var clone = profile.DeepClone();
            this.ColumnNamePrefix = clone.ColumnNamePrefix;
            this.ColumnNameSuffix = clone.ColumnNameSuffix;
            this.ProfileName = clone.ProfileName;
        }
    }
}