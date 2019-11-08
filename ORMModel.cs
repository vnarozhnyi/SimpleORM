using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ORM
{
    public class ORMModel
    {
        public string SqlInsert()
        {
            var stringBuilder = new StringBuilder();
            var fields = new StringBuilder("(");
            var values = new StringBuilder(" values (");

            Type type = GetType();
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);

            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = stringBuilder.ToString();
                command.ExecuteNonQuery();
            }
            if (tableAttributes.Length == 1)
            {
                stringBuilder.Append(string.Format("INSERT INTO [User] ", ((TableAttribute)tableAttributes[0]).Name));

                int fieldCount = 0;

                foreach (var propertyInfo in type.GetProperties())
                {
                    object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(SqlColumnAttribute), true);
                    if (columnAttributes.Length == 1)
                    {
                        var columnAttribute = columnAttributes[0] as SqlColumnAttribute;
                        if (columnAttribute != null && columnAttribute.IsPrimaryKey && columnAttribute.IsAutoIncrement)
                            continue;
                        if (fieldCount == 0)
                        {
                            if (columnAttribute != null)
                            {
                                fields.Append(columnAttribute.ColumnName);
                                values.Append("@" + columnAttribute.ColumnName);
                            }
                        }
                        else
                        {
                            if (columnAttribute != null)
                            {
                                fields.Append("," + columnAttribute.ColumnName);
                                values.Append(",@" + columnAttribute.ColumnName);
                            }
                        }
                        fieldCount++;
                    }
                }
                fields.Append(")");
                values.Append(")");
                stringBuilder.Append(fields);
                stringBuilder.Append(values);
            }
            return stringBuilder.ToString();
        }

        public string SqlSelect()
        {
            var stringBuilder = new StringBuilder();
            var fieldsSql = new StringBuilder("");
            var fromSql = new StringBuilder();
            Type type = GetType();
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (tableAttributes.Length == 1)
            {
                stringBuilder.Append(string.Format("SELECT "));
                fromSql.Append(string.Format(" FROM {0}", ((TableAttribute)tableAttributes[0]).Name));
                int fieldCount = 0;
                foreach (var propertyInfo in type.GetProperties())
                {
                    object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(SqlColumnAttribute), true);
                    if (columnAttributes.Length == 1)
                    {
                        var columnAttribute = columnAttributes[0] as SqlColumnAttribute;
                        if (fieldCount == 0)
                        {
                            if (columnAttribute != null)
                            {
                                fieldsSql.Append(columnAttribute.ColumnName);
                            }
                        }
                        else
                        {
                            if (columnAttribute != null)
                            {
                                fieldsSql.Append("," + columnAttribute.ColumnName);
                            }
                        }
                        fieldCount++;
                    }
                }
                stringBuilder.Append(fieldsSql);
                stringBuilder.Append(fromSql);
            }
            return stringBuilder.ToString();
        }

        public string SqlUpdate()
        {
            var stringBuilder = new StringBuilder();
            var fieldsSql = new StringBuilder("");
            //var whereSql = new StringBuilder(" WHERE ");
            Type type = GetType();
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);

            if (tableAttributes.Length == 1)
            {
                stringBuilder.Append(string.Format("UPDATE {0} SET ", ((TableAttribute)tableAttributes[0]).Name));
                int fieldCount = 0;
                foreach (var propertyInfo in type.GetProperties())
                {
                    object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(SqlColumnAttribute), true);

                    if (columnAttributes.Length == 1)
                    {
                        var columnAttribute = columnAttributes[0] as SqlColumnAttribute;

                        //if (columnAttribute != null && columnAttribute.IsPrimaryKey)
                        //{
                        //    whereSql.Append(string.Format("{0}=@{0}", columnAttribute.ColumnName));
                        //}
                        if (fieldCount == 0)
                        {
                            if (columnAttribute != null && !columnAttribute.IsAutoIncrement)
                            {
                                fieldsSql.Append(string.Format("{0}=@{0}", columnAttribute.ColumnName));
                                fieldCount++;
                            }
                        }
                        else
                        {
                            if (columnAttribute != null && !columnAttribute.IsAutoIncrement)
                            {
                                fieldsSql.Append(string.Format(" ,{0}=@{0}", columnAttribute.ColumnName));
                                fieldCount++;
                            }
                        }

                    }
                }
                stringBuilder.Append(fieldsSql);
                //stringBuilder.Append(whereSql);
            }
            return stringBuilder.ToString();
        }

        public string SqlDelete()
        {
            var stringBuilder = new StringBuilder();
            var fieldsSql = new StringBuilder("");
            var whereSql = new StringBuilder(" WHERE ");
            Type type = GetType();
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);

            if (tableAttributes.Length == 1)
            {
                stringBuilder.Append(string.Format("DELETE FROM {0} ", ((TableAttribute)tableAttributes[0]).Name));
                foreach (var propertyInfo in type.GetProperties())
                {
                    object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(SqlColumnAttribute), true);

                    if (columnAttributes.Length == 1)
                    {
                        var columnAttribute = columnAttributes[0] as SqlColumnAttribute;
                        if (columnAttribute != null && columnAttribute.IsPrimaryKey)
                        {
                            whereSql.Append(string.Format("{0}=@{0}", columnAttribute.ColumnName));
                            break;
                        }
                    }
                }
                stringBuilder.Append(fieldsSql);
                stringBuilder.Append(whereSql);
            }
            Console.WriteLine(stringBuilder);
            return stringBuilder.ToString();
        }

        public List<T> Select<T>(IDbConnection dbconnection)
        {
            return dbconnection.Query<T>(SqlSelect()).ToList();
        }

        public int Insert(IDbConnection dbconnection)
        {
            var rowsAffected = dbconnection.Execute(SqlInsert(), this);

            if (PrimaryKeyPropertyInfo != null)
            {
                var pinfo = PrimaryKeyPropertyInfo;
                object[] columnAttributes = pinfo.GetCustomAttributes(typeof(SqlColumnAttribute), true);

                if (columnAttributes.Length == 1)
                {
                    var columnAttribute = columnAttributes[0] as SqlColumnAttribute;

                    if (columnAttribute != null && columnAttribute.IsPrimaryKey && columnAttribute.IsAutoIncrement)
                    {
                        dynamic identity = dbconnection.Query("SELECT @@IDENTITY AS Id").Single();
                        pinfo.SetValue(this, Convert.ChangeType(identity.Id, TypeCode.Int32), null);
                        var i = identity.Id;
                    }
                }
            }
            return rowsAffected;
        }
        public int Update(IDbConnection dbconnection)
        {
            var rowsAffected = dbconnection.Execute(SqlUpdate(), this);
            return rowsAffected;
        }
        public int Delete(IDbConnection dbconnection)
        {
            var rowsAffected = dbconnection.Execute(SqlDelete(), this);
            return rowsAffected;
        }

        protected PropertyInfo PrimaryKeyPropertyInfo
        {
            get
            {
                Type type = GetType();
                object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);

                if (tableAttributes.Length == 1)
                {
                    foreach (var propertyInfo in type.GetProperties())
                    {
                        object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(SqlColumnAttribute), true);
                        if (columnAttributes.Length == 1)
                        {
                            var columnAttribute = columnAttributes[0] as SqlColumnAttribute;
                            if (columnAttribute != null && columnAttribute.IsPrimaryKey)
                                return propertyInfo;
                        }
                    }
                }
                return null;
            }
        }
    }
}
