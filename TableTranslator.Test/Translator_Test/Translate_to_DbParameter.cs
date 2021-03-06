using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using TableTranslatorEx.Model.Settings;
using TableTranslatorEx.Test.TestModel;
using TableTranslatorEx.Test.TestModel.Profiles;

namespace TableTranslatorEx.Test.Translator_Test
{
    [TestFixture]
    public class Translate_to_DbParameter : InitializedTranslatorTestBase
    {
        private readonly List<TestPerson> people = new List<TestPerson>
        {
            new TestPerson {PublicProperty = "Chris", PublicField = 100},
            new TestPerson {PublicProperty = "Aubrey", PublicField = 101},
            new TestPerson {PublicProperty = "John", PublicField = 102}
        };

        [Test]
        public void Translate_to_DbParameters_table_name_is_name_of_database_object()
        {
            Translator.AddProfile<BasicProfile>();
            Translator.ApplyUpdates();
            var dbParam = Translator.TranslateToDbParameter<BasicProfile, TestPerson>(people,
                new DbParameterSettings("myParam", "myDbObjectName"), "Translation1");
            Assert.AreEqual("myDbObjectName", ((DataTable)dbParam.Value).TableName);
        }

        [Test]
        public void Translate_to_DbParameters_parameter_name_is_assigned()
        {
            Translator.AddProfile<BasicProfile>();
            Translator.ApplyUpdates();
            var dbParam = Translator.TranslateToDbParameter<BasicProfile, TestPerson>(people,
                new DbParameterSettings("myParam", "myDbObjectName"), "Translation1");
            Assert.AreEqual("myParam", dbParam.ParameterName);
        }

        [Test]
        public void Translate_to_DbParameters_db_type_is_assigned()
        {
            Translator.AddProfile<BasicProfile>();
            Translator.ApplyUpdates();
            var dbParam = Translator.TranslateToDbParameter<BasicProfile, TestPerson>(people,
                new DbParameterSettings("myParam", "myDbObjectName"), "Translation1");
            Assert.AreEqual(DbType.Object, dbParam.DbType);
            Assert.AreEqual(SqlDbType.Structured, ((SqlParameter)dbParam).SqlDbType);
        }

        [Test]
        public void Translate_to_DbParameters_assigns_null_value_to_DBNull()
        {
            Translator.AddProfile<BasicProfile>();
            Translator.ApplyUpdates();
            var dbParam = Translator.TranslateToDbParameter<BasicProfile, TestPerson>(new List<TestPerson> {new TestPerson {PublicProperty = null, PublicField = 102}},
                new DbParameterSettings("myParam", "myDbObjectName"), "Translation1");
            Assert.AreEqual(((DataTable) dbParam.Value).Rows[0][0], DBNull.Value);
        }
    }
}