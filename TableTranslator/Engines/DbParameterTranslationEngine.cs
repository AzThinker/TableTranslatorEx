using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TableTranslatorEx.Model;
using TableTranslatorEx.Model.ColumnConfigurations.NonIdentity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Engines
{
    internal class DbParameterTranslationEngine : SimpleTranslationEngine
    {
        internal override object GetColumnValue<T>(T data, NonIdentityColumnConfiguration colConfig)
        {
            return colConfig.GetValueFromObject(data) ?? colConfig.NullReplacement ?? DBNull.Value;
        }

        internal DbParameter WrapinDbParameter(DataTable dataTable, DbParameterSettings dbParameterSettings)
        {
            if (dbParameterSettings == null)
            {
                throw new ArgumentNullException(nameof(dbParameterSettings));
            }

            dataTable.TableName = dbParameterSettings.DatabaseObjectName;

            switch (dbParameterSettings.DatabaseType)
            {
                case DatabaseType.Sql:
                    return new SqlParameter(dbParameterSettings.ParameterName, SqlDbType.Structured)
                    {
                        Value = dataTable,
                        TypeName = dbParameterSettings.DatabaseObjectName
                    };
                case DatabaseType.Oracle:
                case DatabaseType.MySql:
                    throw new NotImplementedException($"Database type {dbParameterSettings.DatabaseType} has not been implemented yet.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}