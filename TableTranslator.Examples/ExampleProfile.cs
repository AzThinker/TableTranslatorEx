using System;
using System.Linq;
using System.Reflection;
using TableTranslatorEx.Examples.Model;
using TableTranslatorEx.Model;
using TableTranslatorEx.Model.ColumnConfigurations.Identity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Examples
{
    class ExampleProfile : TranslationProfile
    {
        // 设置当前配置文件一个明确的名字（可选，默认情况下，配置文件是类的名称）
        // Giving my profile an explicit name (this is optional, by default the profile will be name of the class)
        public override string ProfileName => "MyProfile";


        /// <summary>
        /// 使用Configure来添加您的转换。可以有任意多的转换，就像你想要的一样多。
        /// Use Configure to add your translations. There can be as many translations as you want for as many types as you want.
        /// </summary>
        protected override void Configure()
        {
            // 标准操作
            AddTranslation<Person>(new TranslationSettings("Miscellaneous"))
                // some members of Person
                // Person 类成员
                .AddColumnConfiguration(x => x.Name)
                .AddColumnConfiguration(x => x.Test, new ColumnConfigurationSettings<int?> { NullReplacement = null })

                .AddColumnConfiguration(x => x.Birthday)
                .AddColumnConfiguration(x => x.IsMarried)
                // static member of Person
                // Person 类成员
                .AddColumnConfiguration(x => Person.IsWarmBlooded)
                // some members from Address that belongs to Person
                // 复合类
                .AddColumnConfiguration(x => x.Address.Street)
                .AddColumnConfiguration(x => x.Address.City)
                .AddColumnConfiguration(x => x.Address.State)
                .AddColumnConfiguration(x => x.Address.PostalCode)

                // use an expression using an instance of Person
                // 使用表达式
                // 配置一个新的列（字段），是  Person 类不存在的。
                .AddColumnConfiguration(x => string.Format("{0}|{1}|{2}|{3}", x.Address.Street, x.Address.City, x.Address.State, x.Address.PostalCode),
                    new ColumnConfigurationSettings<string> { ColumnName = "AddressOneLiner" })
                // call a method you have defined without referencing an instance of Person
                //使用方法
                .AddColumnConfiguration(Multiply(2, 5), new ColumnConfigurationSettings<int> { ColumnName = "SomeSimpleMath" })
                // use a built in method without referencing an instance of Person
                //内建方法
                .AddColumnConfiguration(string.Join("", "Hello".Reverse()), new ColumnConfigurationSettings<string> { ColumnName = "ReverseMe!" })
                // simple constant value
                // 简单值
                .AddColumnConfiguration(8, new ColumnConfigurationSettings<int> { ColumnName = "OxygenAtomicNumber" });

            //绑定所有已知列
            AddTranslation<Person>(new TranslationSettings("AllMembers"))
                // adds column configurations for all members using these BindingFlags 
                // (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .AddColumnConfigurationForAllMembers();

            // 条件列
            AddTranslation<Person>(new TranslationSettings("AllMembersWithPredicate"))
                // adds column configurations for all members whose names contains 'r'
                .AddColumnConfigurationForAllMembers(new GetAllMemberSettings
                {
                    Predicate = x => x.Name.Contains("r")
                });

            // 按指定顺序
            AddTranslation<Person>(new TranslationSettings("AllMembersWithOrderer"))
                // adds column configurations for all members and orders them according to the provided IComparer<MemberInfo> (MemberNameLengthDescendingOrderer in this example)
                .AddColumnConfigurationForAllMembers(new GetAllMemberSettings
                {
                    Orderer = new MemberNameLengthDescendingOrderer()
                });

            // 指定绑定标记
            AddTranslation<Person>(new TranslationSettings("AllMembersWithBindingFlags"))
                // adds column configurations for all members using custom BindingFlags (this is excluding static members)
                .AddColumnConfigurationForAllMembers(new GetAllMemberSettings
                {
                    BindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                });

            // 各种可选的设置
            AddTranslation<Person>(
                new TranslationSettings(
                    new GuidIdentityColumnConfiguration("MyGuidId"), //在转换中添加一个标识列配置  add an identity column configuration to the translation
                    "VariousOptionalSettings", //转换名 translation name
                    "PREFIX_", //列前缀 prefix for all column names in the translation
                    "_SUFFIX")) //列后缀 suffix for all column names in the translation
                .AddColumnConfiguration(x => x.Name,
                    new ColumnConfigurationSettings<string>
                    {
                        ColumnName = "DifferentName", // column name
                        NullReplacement = "I am NULL!" // if value is null, replace with this value
                    });

            // 自定结构 (不是预先的类)
            AddTranslation<int>(new TranslationSettings("ForAStruct"))
                .AddColumnConfiguration(x => x, new ColumnConfigurationSettings<int> { ColumnName = "X" })
                .AddColumnConfiguration(x => x * 10, new ColumnConfigurationSettings<int> { ColumnName = "X Times 10" })
                .AddColumnConfiguration(x => DateTime.Now.AddDays(x), new ColumnConfigurationSettings<DateTime> { ColumnName = "X Days From Now" });
        }



        static int Multiply(int x, int y)
        {
            return x * y;
        }
    }
}
