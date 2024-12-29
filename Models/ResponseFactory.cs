using Microsoft.AspNetCore.SignalR;
using SchoolSystem.Models.Response;
using System.Data;
using Dapper;
using SchoolSystem.Models.Manager;

namespace SchoolSystem.Models
{
    public class ResponseFactory(IDbConnection dbConnection)
    {
        public async Task<WebResponse> QueryAsync<T>(string query)
        {
            var response = new WebResponse();
            try
            {
                var result = dbConnection.QueryAsync<T>(query);

                response.Success = 1;
                response.Data = await result;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<WebResponse> QueryAsync<T>(string query, object? parameters)
        {
            var response = new WebResponse();
            try
            {
                var result = dbConnection.QueryAsync<T>(query, parameters);

                response.Success = 1;
                response.Data = await result;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<WebResponse> ExecuteAsync(string query, object? parameters = null, IDbTransaction? transaction = null)
        {
            var response = new WebResponse();
            try
            {
                var result = dbConnection.ExecuteAsync(query, parameters, transaction);

                response.Success = 1;
                response.Data = await result;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = 0;
            }
            return response;
        }

        public async Task<WebResponse> CallStoreProcedureAsync(string storeProcedureName, object? parameters)
        {
            var response = new WebResponse();
            try
            {
                var result = dbConnection.QueryAsync(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                response.Success = 1;
                response.Data = await result;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<WebResponse> ExecuteFromManager(IManager manager)
        {
            dbConnection.Open();

            var transaction = dbConnection.BeginTransaction();

            WebResponse response = new();

            try
            {
                var rowsAffected = 0;
                foreach (var (query, parameters) in manager.Queries())
                {
                    rowsAffected += await dbConnection.ExecuteAsync(query, parameters, transaction);
                }
                response.Success = 1;
                response.Data = rowsAffected;
                transaction.Commit();
            }
            catch (Exception e)
            {
                response.Success = 0;
                response.Message = e.Message;
                transaction.Rollback();
            }

            dbConnection.Close();

            return response;
        }

    }
}
