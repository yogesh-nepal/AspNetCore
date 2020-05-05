using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using service.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;

namespace service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMenu imenu;
        public MenuController(IConfiguration _confguration, IMenu _imenu)
        {
            this.configuration = _confguration;
            this.imenu = _imenu;
        }

        [HttpPost("AddMenu")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddMenu(MenuModel menu)
        {
            try
            {
                imenu.AddMenu(menu);
                return Ok(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        [HttpDelete("DeleteMenu/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMenu(int id)
        {
            try
            {
                imenu.DeleteMenu(id);
                return Ok(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("ShowMenu")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowMenu()
        {
            try
            {
               var list = imenu.ShowMenu();
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }
    }
}