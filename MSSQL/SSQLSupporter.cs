using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PDK.SQL
{
    public class SSQLSupporter : IDisposable
    {
        public Exception LastException { get; internal set; }
        public SqlConnection SqlConnection { get; internal set; }


        public SSQLSupporter() { }
        public SSQLSupporter(string sqlConnectionString) : this(new SqlConnection(sqlConnectionString)) { }
        public SSQLSupporter(SqlConnection sqlConnection) => SqlConnection = sqlConnection;


        public T Table2First<T>(ref string query, SqlConnection sqlConnection = null) where T : class, new() => Table2List<T>(ref query, sqlConnection)?.FirstOrDefault();
        public List<T> Table2List<T>(ref string query, SqlConnection sqlConnection = null) where T : class, new()
        {
            var ds = ToDataSet(query, sqlConnection);
            if (ds is null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            return Table2ListStatic<T>(ref query, ds.Tables[0]);
        }


        public T Table2First<T>(ref string query, DataTable dataTable) where T : class, new() => Table2FirstStatic<T>(ref query, dataTable);
        public T RowToClass<T>(ref string query, DataRow dataRow, DataColumnCollection dataColumnCollection) where T : class, new() => RowToClassStatic<T>(ref query, dataRow, dataColumnCollection);
        public List<T> Table2List<T>(ref string query, DataTable dataTable) where T : class, new() => Table2ListStatic<T>(ref query, dataTable);


        public static List<T> Table2ListStatic<T>(ref string query, DataTable dataTable) where T : class, new()
        {
            if (dataTable is null)
                return null;

            List<T> targetList = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                var target = RowToClassStatic<T>(ref query, row, dataTable.Columns);

                targetList.Add(target);
            }

            return targetList;
        }
        public static T RowToClassStatic<T>(ref string query, DataRow dataRow, DataColumnCollection dataColumnCollection) where T : class, new()
        {
            query ??= "";

            var target = new T();

            var targetType = target.GetType();

            var targetProperties = targetType.GetProperties().ToList();

            foreach (DataColumn column in dataColumnCollection)
            {
                try
                {
                    if (targetProperties.FirstOrDefault(prop => prop.Name == column.ColumnName) != null)
                    {
                        var value = dataRow[column.ColumnName];

                        if (value.GetType() == typeof(DBNull))
                        {
                            value = null;
                        }

                        targetType.GetProperty(column.ColumnName).SetValue(target, value);
                    }
                }
                catch (Exception ex)
                {
                    query += $@"{Environment.NewLine}{ex.Message}";
                }
            }
            return target;
        }
        public static T Table2FirstStatic<T>(ref string query, DataTable dataTable) where T : class, new() => Table2ListStatic<T>(ref query, dataTable)?.FirstOrDefault();

        public void SetDefaultSqlConnection(string sqlConnectionString) => SetDefaultSqlConnection(new SqlConnection(sqlConnectionString));
        public void SetDefaultSqlConnection(SqlConnection sqlConnection) => SqlConnection = sqlConnection;


        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="connection"></param>
        public void ConnectionOpen(SqlConnection sqlConnection = null)
        {
            sqlConnection = ConnectionControl(sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken)
                sqlConnection.Open();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">This method uses to default sql connection if parameter connection is null</param>
        public void ConnectionClose(SqlConnection sqlConnection = null)
        {
            sqlConnection = ConnectionControl(sqlConnection);
            if (sqlConnection.State != ConnectionState.Closed || sqlConnection.State != ConnectionState.Broken)
                sqlConnection.Close();
        }

        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public SqlConnection ConnectionControl(SqlConnection sqlConnection)
        {
            if (sqlConnection is null)
                if (this.SqlConnection is null)
                    throw new Exception("Parameter connection and default connection null. At least one connection require to not null.");
                else
                    return this.SqlConnection;
            return sqlConnection;
        }

        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public Task<DataSet> ToDataSetAsync(string query, SqlConnection sqlConnection = null) => Task.Run(() => ToDataSet(query, sqlConnection));

        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public Task<bool> ToQueryAsync(string query, SqlConnection sqlConnection = null) => Task.Run(() => ToQuery(query, sqlConnection));
        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public Task<object> ToScalarAsync(string query, SqlConnection sqlConnection = null) => Task.Run(() => ToScalar(query, sqlConnection));


        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public DataSet ToDataSet(string query, SqlConnection sqlConnection = null)
        {
            SqlConnection sqlConnectionForReader = null;
            try
            {
                SqlDataReader sqlDataReader = ToReader(out sqlConnectionForReader, query, sqlConnection);

                DataSet dataSet = new DataSet();

                while (!sqlDataReader.IsClosed)
                    dataSet.Tables.Add().Load(sqlDataReader);

                return dataSet;
            }
            catch (Exception ex)
            {
                LastException = ex;

                return default;
            }
            finally
            {
                ConnectionClose(sqlConnectionForReader);
            }
        }
        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public SqlDataReader ToReader(out SqlConnection sqlConnectionForReader, string query, SqlConnection sqlConnection = null)
        {
            sqlConnectionForReader = null;
            try
            {
                ConnectionOpen(sqlConnection);

                sqlConnectionForReader = new SqlConnection(SqlConnection.ConnectionString);

                ConnectionClose(sqlConnection);

                ConnectionOpen(sqlConnectionForReader);

                SqlCommand sqlCommand = new SqlCommand(query, ConnectionControl(sqlConnectionForReader));

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                return sqlDataReader;
            }
            catch (Exception ex)
            {
                LastException = ex;

                return default;
            }
        }
        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public bool ToQuery(string query, SqlConnection sqlConnection = null)
        {
            try
            {
                ConnectionOpen(sqlConnection);

                using SqlCommand sqlCommand = new SqlCommand(query, ConnectionControl(sqlConnection));

                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                LastException = ex;

                return false;
            }
            finally
            {
                ConnectionClose(sqlConnection);
            }
        }
        /// <summary>
        /// This method uses to default sql connection if parameter connection is null
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public object ToScalar(string query, SqlConnection sqlConnection = null)
        {
            try
            {
                ConnectionOpen(sqlConnection);

                using SqlCommand sqlCommand = new SqlCommand(query, ConnectionControl(sqlConnection));

                object obj = sqlCommand.ExecuteScalar();

                return obj;
            }
            catch (Exception ex)
            {
                LastException = ex;

                return default;
            }
            finally
            {
                ConnectionClose(sqlConnection);
            }
        }


        public void Dispose() => SqlConnection?.Dispose();
    }
}
