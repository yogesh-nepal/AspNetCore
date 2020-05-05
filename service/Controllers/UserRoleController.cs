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
    public class UserRoleController : ControllerBase
    {
        public IConfiguration configuration;
        public IUserRole iuserrole;

        public UserRoleController(IConfiguration _configuration, IUserRole _iuserrole)
        {
            this.configuration = _configuration;
            this.iuserrole = _iuserrole;
        }

        [HttpPost("AddUserRole")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUserRole(UserRoleModel uRole)
        {
            try
            {
                iuserrole.AddUserRole(uRole);
                return Ok(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("DeleteUserRole")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUserRole(UserRoleModel uRole)
        {
            iuserrole.Delete(uRole);
            return Ok(HttpStatusCode.OK);
        }
    }
}