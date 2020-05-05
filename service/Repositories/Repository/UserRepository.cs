using System.Data;
using Microsoft.Extensions.Configuration;
using service.Repositories.Interface;
using Dapper;
using System.Linq;
using System;
using Models;
using System.Collections.Generic;

namespace service.Repositories.Repository
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnection dbConnection;
        public UserRepository(IConfiguration _configuration, IDbConnection _dbConnection)
        {
            this.configuration = _configuration;
            this.dbConnection = _dbConnection;
        }
        public void AddUser(User user)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserName",user.UserName);
                    param.Add("@Address",user.Address);
                    param.Add("@Phone",user.Phone);
                    param.Add("@EmailID",user.EmailID);
                    param.Add("@Gender",user.Gender);
                    param.Add("@IsActive",user.IsActive);
                    param.Add("@Remarks",user.Remarks);
                    param.Add("@Password",user.Password);
                    dbConnection.Execute("AddUser",param,commandType:CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(int id)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserID",id);
                    dbConnection.Execute("DeleteUser",param,commandType:CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    
                    throw ex;
                }
            }
        }

        public IEnumerable<User> GetAllUser()
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    var list = SqlMapper.Query<User>(dbConnection,"GetAllUser",commandType:CommandType.StoredProcedure).ToList();
                    return list;
                }
                catch (System.Exception ex)
                {
                    
                    throw ex;
                }
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    var userlist = dbConnection.Query<User>("UserRole",commandType:CommandType.StoredProcedure).ToList();
                    return userlist;
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserID",user.UserID);
                    param.Add("@UserName",user.UserName);
                    param.Add("@Address",user.Address);
                    param.Add("@Phone",user.Phone);
                    param.Add("@EmailID",user.EmailID);
                    param.Add("@Gender",user.Gender);
                    param.Add("@IsActive",user.IsActive);
                    param.Add("@Remarks",user.Remarks);
                    param.Add("@Password",user.Password);
                    dbConnection.Execute("UpdateUser",param,commandType:CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public User UserLogin(string EmailID, string Password)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@EmailID",EmailID);
                    param.Add("@Password", Password);
                    var loginData = dbConnection.Query<User>("SelectForLogin",param,commandType:CommandType.StoredProcedure).FirstOrDefault();
                    if (loginData != null)
                    {
                        var loginrole = dbConnection.Query<RoleModel>("LoginRole",new{loginData.UserID},commandType:CommandType.StoredProcedure);
                        loginData.Roles=loginrole.ToList();
                    }
                    return loginData;
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
        }
    }
}