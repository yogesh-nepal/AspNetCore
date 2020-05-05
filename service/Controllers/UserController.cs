using System.Security.AccessControl;
using System.Net;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using service.Repositories.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration configuration;
        public IUser iuser;
        public UserController(IConfiguration _configuration, IUser _user)
        {
            this.iuser = _user;
            this.configuration = _configuration;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtProp:key"]));
            var alg = SecurityAlgorithms.HmacSha256;
            var credentials = new SigningCredentials(key, alg);
            var listrole = user.Roles;
            var claim = new List<Claim>
            {

                new Claim(ClaimTypes.Email,user.EmailID),
                new Claim("usrPassword",user.Password),
                //new Claim(ClaimTypes.Role,listrole)
            };
            //if listrole.count>0
            if (listrole.Count > 0)
            {
                foreach (var role in listrole)
                {
                    claim.Add(new Claim(ClaimTypes.Role, role.Role));
                }
            }
            else
            {
                //
            }
            var _securityToken = new JwtSecurityToken(
                issuer: configuration["jwtProp:validIssuer"],
                audience: configuration["jwtProp:validAudience"],
                expires: DateTime.Now.AddMinutes(60),
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: claim
            );
            var _token = new JwtSecurityTokenHandler().WriteToken(_securityToken);
            return _token;
        }

        [HttpPost("Auth")]
        public IActionResult AuthenticateUser(User user)
        {
            var data = iuser.UserLogin(user.EmailID, user.Password);
            if (data != null)
            {
                var _token = GenerateToken(data);
                return Ok(_token);
            }
            return NotFound();
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var list = iuser.GetAllUsers();
            return Ok(list);
        }

        //Insert and Update
        [HttpPost("AddUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser(User user)
        {
            try
            {
                iuser.AddUser(user);
                return Ok(HttpStatusCode.OK);
            } 
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            iuser.Delete(id);
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("GetAllUser")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAllUser()
        {
            var list=iuser.GetAllUser();
            return Ok(list);
        }
    }

}