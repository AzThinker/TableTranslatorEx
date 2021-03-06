using System;
using System.Reflection;
using TableTranslatorEx.Exceptions;
using TableTranslatorEx.Helpers;

namespace TableTranslatorEx.Model.ColumnConfigurations.NonIdentity
{
    internal sealed class MemberColumnConfiguration : NonIdentityColumnConfiguration
    {
        private string _relativePropertyPath;
        private string _columnName;
        private string RelativePropertyPath => !string.IsNullOrEmpty(this._relativePropertyPath) ? this._relativePropertyPath : this.MemberInfo.Name;
        internal MemberInfo MemberInfo { get; private set; }

        internal MemberColumnConfiguration(MemberInfo memberInfo, int ordinal, string columnName, object nullReplacement,
            string relativePropertyPath) : base(ordinal, nullReplacement)
        {
            Initialize(memberInfo, columnName, relativePropertyPath);
        }

        // used for the all properties method, where column names are not specified
        internal MemberColumnConfiguration(MemberInfo memberInfo, int ordinal, object nullReplacement) : base(ordinal, nullReplacement)
        {
            Initialize(memberInfo, null, null);
        }

        private void Initialize(MemberInfo memberInfo, string columnName, string relativePropertyPath)
        {
            this.MemberInfo = memberInfo;
            this._relativePropertyPath = relativePropertyPath;
            this._columnName = columnName;

            ValidateInput();
        }

        public override string ColumnName => !string.IsNullOrEmpty(this._columnName) ? this._columnName : this.MemberInfo.Name;

        public override Type ColumnDataType => this.MemberInfo.GetMemberType();

        internal override object GetValueFromObject(object obj)
        {
            return ReflectionHelper.GetMemberValue(this.RelativePropertyPath, obj);
        }

        internal override void ValidateInput()
        {
            if (this.MemberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            if (this.NullReplacement != null && base.NullReplacement != DBNull.Value && this.MemberInfo.GetMemberType() != base.NullReplacement.GetType())
            {
                throw new TableTranslatorConfigurationException("Null replacement for member must be either of the same type as the member, null, or DBNull.");
            }

            if (string.IsNullOrEmpty(this.RelativePropertyPath))
            {
                throw new ArgumentNullException("relativePropertyPath");
            }

            if (!this.RelativePropertyPath.Contains(this.MemberInfo.Name))
            {
                throw new TableTranslatorConfigurationException("The full property path does not contain the name of the member info to be retrieved.");
            }
        }
    }
}