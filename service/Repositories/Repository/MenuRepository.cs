using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;
using Dapper;
using System.Linq;

namespace service.Repositories.Repository
{
    public class MenuRepository : IMenu
    {

        private readonly IConfiguration configuration;
        private readonly IDbConnection dbConnection;

        public MenuRepository(IConfiguration _configuration, IDbConnection _dbConnection)
        {
            this.configuration = _configuration;
            this.dbConnection = _dbConnection;
        }

        public void AddMenu(MenuModel menu)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Menu", menu.Menu);
                    param.Add("@Isopen", menu.IsOpen);
                    param.Add("@MenuURL", menu.MenuURL);
                    param.Add("@Under", menu.Under);
                    param.Add("@IsActive", menu.IsActive);
                    dbConnection.Execute("AddMenu",param,commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }

        public void DeleteMenu(int id)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@MenuID", id);
                    dbConnection.Execute("DeleteMenu", param, commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<MenuModel> ShowMenu()
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    var list = SqlMapper.Query<MenuModel>(dbConnection, "ShowMenu", commandType: CommandType.StoredProcedure).ToList();
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