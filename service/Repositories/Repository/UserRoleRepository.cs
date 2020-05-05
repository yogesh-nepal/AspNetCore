using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;

namespace service.Repositories.Repository
{
    public class UserRoleRepository : IUserRole
    {

        private readonly IConfiguration configuration;
        private readonly IDbConnection dbConnection;
        public UserRoleRepository(IConfiguration _configuration, IDbConnection _dbConnection)
        {
            this.configuration = _configuration;
            this.dbConnection = _dbConnection;
        }

        public void AddUserRole(UserRoleModel uRole)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserID", uRole.UserID);
                    param.Add("@RoleID", uRole.RoleID);
                    dbConnection.Execute("AddUserRole", param, commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception exception)
                {

                    throw exception;
                }

            }
        }

        public void Delete(UserRoleModel uRole)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserID", uRole.UserID);
                    param.Add("@RoleID", uRole.RoleID);
                    dbConnection.Execute("DeleteUserRole", param, commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        // public void Delete(int id)
        // {
        //     using (dbConnection)
        //     {
        //         try
        //         {
        //             dbConnection.Open();
        //             DynamicParameters param = new DynamicParameters();
        //             param.Add("@UserID",id);
        //             dbConnection.Execute("DeleteUserRole",param,commandType:CommandType.StoredProcedure);
        //         }
        //         catch (System.Exception exception)
        //         {

        //             throw exception;
        //         }
        //     }
        // }
    }
}