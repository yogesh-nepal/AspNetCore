using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ui.Controllers
{
    public class RoleController : Controller
    {
        IHttpClientFactory client;
        public RoleController(IHttpClientFactory _client)
        {
            client = _client;
        }

        [HttpGet]
        public IActionResult GetAllRole()
        {
            IEnumerable<RoleModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("Role/GetAllRole").Result;
            list = response.Content.ReadAsAsync<IEnumerable<RoleModel>>().Result;
            return Json(list);
        }
    }
}