using System;
using System.Linq.Expressions;
using System.Reflection;
using TableTranslatorEx.Model.ColumnConfigurations.NonIdentity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Abstract
{
    internal interface IColumnConfigurationBuilder
    {
        NonIdentityColumnConfiguration BuildColumnConfiguration<T>(T value, ColumnConfigurationSettings<T> columnConfigurationSettings);
        NonIdentityColumnConfiguration BuildColumnConfiguration<T, K>(Expression<Func<T, K>> func, ColumnConfigurationSettings<K> columnConfigurationSettings);
        NonIdentityColumnConfiguration BuildColumnConfiguration<T>(MemberInfo memberInfo, int ordinal) where T : new();
    }
}