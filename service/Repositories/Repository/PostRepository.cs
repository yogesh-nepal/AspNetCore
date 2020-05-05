using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;

namespace service.Repositories.Repository
{
    public class PostRepository : IPost
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnection dbConnection;
        private IWebHostEnvironment hostingEnvironment;
        public PostRepository(IConfiguration _configuration, IDbConnection _dbConnection, IWebHostEnvironment _hostingEnvironment)
        {
            this.configuration = _configuration;
            this.dbConnection = _dbConnection;
            this.hostingEnvironment = _hostingEnvironment;
        }

        public void AddPost(PostModel post)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@CategoryID", post.CategoryID);
                    param.Add("@Title", post.Title);
                    param.Add("@ShortDescription", post.ShortDescription);
                    param.Add("@FullDescription", post.FullDescription);
                    param.Add("@ImageURL", post.ImageURL);
                    param.Add("@PublishDate", post.PublishDate);
                    param.Add("@IsActive", post.IsActive);
                    param.Add("@Remarks", post.Remarks);
                    param.Add("@AuthorName", post.AuthorName);
                    param.Add("@Tag", post.Tag);
                    dbConnection.Execute("AddPost", param, commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }

        public void DeletePost(int id)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@PostID", id);
                    dbConnection.Execute("DeletePost", param, commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }

        public PostModel GetPostByID(int id)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@PostID", id);
                    var list = SqlMapper.Query<PostModel>(dbConnection, "ShowPostByID", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return list;
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }

        public IEnumerable<PostModel> ShowAllPost()
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    var list = SqlMapper.Query<PostModel>(dbConnection, "ShowPost", commandType: CommandType.StoredProcedure).ToList();
                    return list;
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }

        public void UpdatePost(PostModel post)
        {
            using (dbConnection)
            {
                try
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@PostID", post.PostID);
                    param.Add("@CategoryID", post.CategoryID);
                    param.Add("@Title", post.Title);
                    param.Add("@ShortDescription", post.ShortDescription);
                    param.Add("@FullDescription", post.FullDescription);
                    param.Add("@ImageURL", post.ImageURL);
                    param.Add("@UpdatedTime", post.UpdatedTime);
                    param.Add("@IsActive", post.IsActive);
                    param.Add("@Remarks", post.Remarks);
                    param.Add("@UpdatedBy", post.UpdatedBy);
                    param.Add("@Tag", post.Tag);
                    dbConnection.Execute("UpdatePost", param, commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}