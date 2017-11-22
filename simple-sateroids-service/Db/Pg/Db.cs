using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dapper;
using Npgsql;

namespace simple_sateroids_service.Db.Pg
{
    public static class Db
    {
        /// <summary>
        /// Обернутый query чтобы using каждый раз не делать
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="queryParams"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="connectionString">Коннекшин стринг по дефолту к СУПР</param>     
        /// <returns></returns>
        public static List<T> Query<T>(string query, object queryParams = null,
            SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = 0,
            CommandType? commandType = null, string connectionString = null)
        {
            using (var conn = new NpgsqlConnection(connectionString ?? Config.AsteroidsDb))
            {
                PresetQueryParameters(conn, transaction: transaction);
                return conn.Query<T>(query, queryParams, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryParams"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="connectionString">Коннекшин стринг по дефолту к СУПР</param>     
        /// <returns></returns>
        public static IEnumerable<dynamic> Query(string query, object queryParams = null,
            SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = 0,
            CommandType? commandType = null, string connectionString = null)
        {
            using (var conn = new NpgsqlConnection())
            {
                PresetQueryParameters(conn, transaction: transaction);
                return conn.Query(query, queryParams, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryParams"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="connectionString">Коннекшин стринг по дефолту к СУПР</param>     
        /// <returns></returns>
        public static int Execute(string query, object queryParams = null,
            SqlTransaction transaction = null, int? commandTimeout = 0,
            CommandType? commandType = null, string connectionString = null)
        {
            using (var conn = new NpgsqlConnection(connectionString ?? Config.AsteroidsDb))
            {
                PresetQueryParameters(conn, transaction: transaction);
                return conn.Execute(query, queryParams, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// Проинсертить DTO в базу (создать новую запись)
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        /// <param name="tableName">имя таблицы в которую инсертить</param>
        /// <param name="dto">сама DTO</param>
        /// <param name="excludedProps">Список пропертей, которые инсертить не надо (например identity поля типа Id)</param>
        /// <param name="connectionString">Коннекшин стринг по дефолту к СУПР</param>     
        public static void Add<T>(string tableName, T dto, string[] excludedProps = null, bool returnId = true,
            string connectionString = null, int timeOut = 60)
        {
            using (var conn = new NpgsqlConnection(connectionString ?? Config.AsteroidsDb))
            {
                excludedProps = excludedProps ?? new string[0];
                var props = dto.GetType().GetProperties().Where(p => !excludedProps.Contains(p.Name));
                var sqlQuery = String.Format(@"
                INSERT INTO ""{0}"" (
                    {1}
                ) VALUES (
                    {2}
                ) ", tableName, String.Join(",", props.Select(p => $@"""{p.Name}""")),
                    String.Join(",", props.Select(p => $"@{p.Name}")));
                conn.Execute(sqlQuery, dto, commandTimeout: timeOut);
            }
        }

        /// <summary>
        /// Проинсертить много DTO в базу (создать новые записи)
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        /// <param name="tableName">имя таблицы в которую инсертить</param>
        /// <param name="dtos">список с DTO</param>
        /// <param name="excludedProps">Список пропертей, которые инсертить не надо (например identity поля типа Id)</param>
        /// <param name="connectionString">Коннекшин стринг по дефолту к СУПР</param>     
        public static void AddRange<T>(string tableName, List<T> dtos, string[] excludedProps = null, string connectionString = null)
        {
            using (var conn = new NpgsqlConnection(connectionString ?? Config.AsteroidsDb))
            {
                excludedProps = excludedProps ?? new string[0];
                var props = dtos.FirstOrDefault().GetType().GetProperties().Where(p => !excludedProps.Contains(p.Name));
                var sqlQuery = String.Format(@"
                INSERT INTO {0} (
                    {1}
                ) VALUES (
                    {2}
                )
                SELECT SCOPE_IDENTITY() 
            ", tableName, String.Join(",", props.Select(p => p.Name)),
                    String.Join(",", props.Select(p => String.Format("@{0}", p.Name))));
                conn.Execute(sqlQuery, dtos);
            }
        }

        public static void PresetQueryParameters(NpgsqlConnection conn, string query = null, SqlTransaction transaction = null)
        {
            return;
            string defaulParams = @"
                SET ARITHABORT ON
            ";
            conn.Execute(query ?? defaulParams, transaction: transaction);
        }
    }
}