using System.Security.AccessControl;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;

namespace service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICategory icategory;
        public CategoryController(IConfiguration _configuration, ICategory _icategory)
        {
            this.configuration = _configuration;
            this.icategory = _icategory;
        }
        [HttpPost("AddCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory(CategoryModel catModel)
        {
            icategory.AddCategory(catModel);
            return Ok(HttpStatusCode.OK);
        }

        [HttpDelete("DeleteCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int id)
        {
            icategory.DeleteCategory(id);
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowCategory()
        {
            var list = icategory.ShowCategory();
            return Ok(list);
        }
    }
}