using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;

namespace service.Repositories.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnection dbConnection;

        public CategoryRepository(IConfiguration _confguration, IDbConnection _dbConnection)
        {
            this.configuration = _confguration;
            this.dbConnection = _dbConnection;
        }
        public void AddCategory(CategoryModel catModel)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Category",catModel.Category);
                    dbConnection.Execute("AddCategory",param,commandType:CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    
                    throw ex;
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@CategoryID",id);
                    dbConnection.Execute("DeleteCategory",param,commandType:CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    
                    throw ex;
                }
            }
        }

        public IEnumerable<CategoryModel> ShowCategory()
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    var list = SqlMapper.Query<CategoryModel>(dbConnection,"ShowCategory",commandType:CommandType.StoredProcedure).ToList();
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