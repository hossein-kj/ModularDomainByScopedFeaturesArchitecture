using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MDSF.BuildingBlocks.Data.ADO
{
    public static class SqlHelper
    {
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
           params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandTimeout = 720;
                        command.CommandType = cmdType;
                        if (commandParameters != null)
                            command.Parameters.AddRange(commandParameters);
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        public async static Task<int> ExecuteNonQueryAsync(string connectionString, CommandType cmdType, string cmdText, CancellationToken cancellationToken,
           params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandTimeout = 720;
                        command.CommandType = cmdType;
                        if (commandParameters != null)
                            command.Parameters.AddRange(commandParameters);
                        await connection.OpenAsync(cancellationToken);
                        return await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

        }
        //public static async Task<int> ExecuteNonQueryAsync(CommandType cmdType, string cmdText,
        //    params SqlParameter[] commandParameters)
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        using (var command = new SqlCommand(cmdText, connection))
        //        {
        //            try
        //            {
        //                command.CommandType = cmdType;
        //                if (commandParameters != null)
        //                command.Parameters.AddRange(commandParameters);
        //                connection.Open();
        //                return await command.ExecuteNonQueryAsync();
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }

        //}

        public async static Task<List<T>> ExecuteReaderAsync<T>(string connectionString, CommandType cmdType,
            string cmdText, Func<IDataReader, T> transform, CancellationToken cancellationToken, params SqlParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    command.CommandTimeout = 720;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        //if (await reader.ReadAsync())
                        //{
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            myList.Add(transform(reader));
                        }
                        //}


                    }
                }
                await connection.CloseAsync();

            }
            return myList;
        }

        public static List<T> ExecuteReader<T>(string connectionString, CommandType cmdType,
          string cmdText, Func<IDataReader, T> transform, params SqlParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandTimeout = 720;
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //if (reader.Read())
                        //{
                        while (reader.Read())
                        {
                            myList.Add(transform(reader));
                        }
                        //}


                    }
                }
                connection.Close();

            }
            return myList;
        }


        public static List<T> ExecuteReader<T>(string connectionString, CommandType cmdType,
           string cmdText, IList<Func<IDataReader, T>> transform, params SqlParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    command.CommandTimeout = 720;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //if (await reader.ReadAsync())
                        //{
                        while (reader.Read())
                        {
                            myList.Add(transform[0](reader));
                        }
                        var transformCounter = transform.Count();
                        var transformIndex = 1;
                        while (transformCounter > 1)
                        {
                            if (transform.Count() > 1)
                            {
                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        myList.Add(transform[transformIndex](reader));
                                    }
                                }
                            }
                            transformIndex++;
                            transformCounter--;
                        }

                        //}
                    }
                }
                connection.Close();

            }
            return myList;
        }


        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandTimeout = 720;
                        command.CommandType = cmdType;
                        command.Parameters.AddRange(commandParameters);
                        connection.Open();
                        return command.ExecuteScalar();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        public static async Task<object> ExecuteScalarAsync(string connectionString, CommandType cmdType, string cmdText, CancellationToken cancellationToken,
            params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandType = cmdType;
                        command.CommandTimeout = 720;
                        command.Parameters.AddRange(commandParameters);
                        await connection.OpenAsync(cancellationToken);
                        return await command.ExecuteScalarAsync(cancellationToken);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        public static IList<string> ExecuteReaderFieldByField(string connectionString, CommandType cmdType,
       string cmdText, params SqlParameter[] commandParameters)
        {
            var myList = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandTimeout = 720;
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //if (reader.Read())
                        //{
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                try
                                {
                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        myList.Add(reader.GetName(index) + ":" + reader.GetString(index));
                                    }
                                }
                                catch (Exception)
                                {

                                    try
                                    {
                                        for (int index = 0; index < reader.FieldCount; index++)
                                        {
                                            myList.Add(reader.GetName(index) + ":" + reader.GetInt32(index));
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        throw new Exception("ecError:" + "دیتا موجود نمی باشد");
                                    }
                                }
                            }

                        }
                        //}


                    }
                }
            }
            return myList;
        }

        public static async Task<IList<string>> ExecuteReaderFieldByFieldAsync(string connectionString, CommandType cmdType,
  string cmdText, CancellationToken cancellationToken, params SqlParameter[] commandParameters)
        {
            var myList = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandTimeout = 720;
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        //if (reader.Read())
                        //{
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            if (reader.HasRows)
                            {
                                try
                                {
                                    for (int index = 0; index < reader.FieldCount; index++)
                                    {
                                        myList.Add(reader.GetName(index) + ":" + reader.GetString(index));
                                    }
                                }
                                catch (Exception)
                                {

                                    try
                                    {
                                        for (int index = 0; index < reader.FieldCount; index++)
                                        {
                                            myList.Add(reader.GetName(index) + ":" + reader.GetInt32(index));
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        throw new Exception("ecError:" + "دیتا موجود نمی باشد");
                                    }
                                }
                            }

                        }
                        //}


                    }
                }
            }
            return myList;
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

    }
}
