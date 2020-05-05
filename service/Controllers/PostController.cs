using System.Security.AccessControl;
using System.Runtime.Serialization.Json;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using service.Repositories.Interface;
using Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IConfiguration configuration;
        private readonly IPost ipost;
        private readonly IWebHostEnvironment _env;

        public PostController(IConfiguration _configuration, IPost _ipost, IWebHostEnvironment _env)
        {
            this._env = _env;
            this.configuration = _configuration;
            this.ipost = _ipost;

        }
        [HttpPost("AddPost")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult AddPost([FromForm]PostModel model)
        {

            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            var formData = Request.Form["formdata"];
            var pModel = JsonConvert.DeserializeObject<PostModel>(formData);

            if (file != null)
            {

                var dir = Path.Combine(_env.WebRootPath, "Resource");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var fileName = Guid.NewGuid().ToString() + "" + file.FileName + "" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(dir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream).GetAwaiter().GetResult();
                    stream.FlushAsync().GetAwaiter().GetResult();
                }

                pModel.ImageURL = fileName;
            }

            ipost.AddPost(pModel);
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowAllPost")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult ShowPost()
        {
            var list = ipost.ShowAllPost();
            return Ok(list);
        }

        [HttpGet("GetPostByID/{id}")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult GetPostByID(int id)
        {
            var list = ipost.GetPostByID(id);
            return Ok(list);
        }

        [HttpPost("UpdatePost")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult UpdatePost([FromForm]PostModel post)
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            var formData = Request.Form["formdata"];
            var pModel = JsonConvert.DeserializeObject<PostModel>(formData);
            if (file != null)
            {
                var dir = Path.Combine(_env.WebRootPath, "Resource");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var fileName = Guid.NewGuid().ToString() + "" + file.FileName + "" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(dir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream).GetAwaiter().GetResult();
                    stream.FlushAsync().GetAwaiter().GetResult();
                }

                pModel.ImageURL = fileName;
            }
            ipost.UpdatePost(pModel);
            return Ok(HttpStatusCode.OK);
        }

        [HttpDelete("DeletePost/{id}")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                ipost.DeletePost(id);
                return Ok(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}