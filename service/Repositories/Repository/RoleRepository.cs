using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;

namespace service.Repositories.Repository
{
    public class RoleRepository: IRole
    {
        private IConfiguration configuration;
        private IDbConnection dbConnection;
        public RoleRepository(IConfiguration _configuration, IDbConnection _dbConnection)
        {
            this.dbConnection = _dbConnection;
            this.configuration = _configuration;
        }

        public IEnumerable<RoleModel> GetAllRole()
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    var list = SqlMapper.Query<RoleModel>(dbConnection,"GetAllRole",commandType:CommandType.StoredProcedure).ToList();
                    return list;
                }
                catch (System.Exception ex)
                {
                    
                    throw ex;
                }
            }
        }
    }
}