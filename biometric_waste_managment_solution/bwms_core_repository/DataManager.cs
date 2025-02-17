﻿using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_repository
{
    public class DataManager<TEntity> where TEntity : class
    {
        private string _connectionString;

        public DataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool InsertData(string procedureName, string jsonString)
        {
            bool status = false;

            try
            {

                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {

                        try
                        {

                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@jsonString", jsonString);

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output,
                            };

                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;
                        }
                        catch (Exception ex)
                        {
                            return status;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public bool UpdateData(string procedureName, string jsonString)
        {
            bool status = false;
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@jsonString", jsonString);

                        var executionStatusParam = new SqlParameter
                        {
                            ParameterName = "@executionStatus",
                            SqlDbType = SqlDbType.Bit,
                            Direction = ParameterDirection.Output
                        };
                        sqlCommand.Parameters.Add(executionStatusParam);

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();

                        status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public ICollection<TEntity> RetrieveData(string procedureName, SqlParameter[] parameters = null)
        {
            ICollection<TEntity> data = new List<TEntity>();

            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();

                        var jsonResult = new StringBuilder();
                        var reader = sqlCommand.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            jsonResult.Append("[]");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                jsonResult.Append(reader.GetValue(0).ToString());
                            }
                        }

                        data = JsonConvert.DeserializeObject<ICollection<TEntity>>(jsonResult.ToString()) ?? new List<TEntity>();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return data;
        }



        public bool DeleteData(string procedureName, SqlParameter[] parameters = null)
        {
            bool status = false;

            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        try
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters);
                            }

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output
                            };

                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {

                            sqlConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return status;
            }



        }
    }
}
