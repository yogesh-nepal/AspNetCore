using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using service.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;

namespace service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IConfiguration configuration;
        public IRole role;
        public RoleController(IConfiguration _configuration, IRole _role)
        {
            configuration = _configuration;
            role = _role;
        }
        [HttpGet("GetAllRole")]
        // [Authorize(Roles="Admin")]
        public IActionResult GetAllRole()
        {
            var list=role.GetAllRole();
            return Ok(list);
        }
    }
}