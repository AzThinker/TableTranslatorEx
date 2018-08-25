using System;
using System.Linq.Expressions;
using System.Reflection;
using TableTranslatorEx.Abstract;
using TableTranslatorEx.Helpers;
using TableTranslatorEx.Model.ColumnConfigurations.NonIdentity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.ConfigurationBuilders
{
    internal class ColumnConfigurationBuilder : IColumnConfigurationBuilder
    {
        public NonIdentityColumnConfiguration BuildColumnConfiguration<T>(T value, ColumnConfigurationSettings<T> columnConfigurationSettings)
        {
            return new SimpleValueColumnConfiguration(value, value?.GetType() ?? typeof(T), columnConfigurationSettings.Ordinal, columnConfigurationSettings.ColumnName, columnConfigurationSettings.NullReplacement);
        }

        public NonIdentityColumnConfiguration BuildColumnConfiguration<T, K>(Expression<Func<T, K>> func, ColumnConfigurationSettings<K> columnConfigurationSettings)
        {
            if (func.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberInfo = ReflectionHelper.GetMemberInfoFromLambda(func);
                var fullPropertyPath = ReflectionHelper.GetMemberRelativePathNameFromLambda(func);
                return new MemberColumnConfiguration(memberInfo, columnConfigurationSettings.Ordinal, columnConfigurationSettings.ColumnName, columnConfigurationSettings.NullReplacement, fullPropertyPath);
            }

            var delegateSettings = new DelegateSettings(func.Compile());
            return new DelegateColumnConfiguration(delegateSettings, columnConfigurationSettings.Ordinal, columnConfigurationSettings.ColumnName, columnConfigurationSettings.NullReplacement);
        }

        public NonIdentityColumnConfiguration BuildColumnConfiguration<T>(MemberInfo memberInfo, int ordinal) where T : new()
        {
            return new MemberColumnConfiguration(memberInfo, ordinal, memberInfo.GetMemberType().GetDefaultValue());
        }
    }
}