using System.Net.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ui.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IHttpClientFactory client;
        public UserRoleController(IHttpClientFactory _client)
        {
            client = _client;
        }
        [HttpGet]
        public IActionResult AddUserRole()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddUserRole(UserRoleModel uRole)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.PostAsJsonAsync("UserRole/AddUserRole", uRole).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return RedirectToAction("PageNotFound", "Access");
            }
            return RedirectToAction("UserList", "User");
        }

        [HttpPost]
        public IActionResult DeleteUserRole(UserRoleModel uRole)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.PostAsJsonAsync("UserRole/DeleteUserRole",uRole).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "User");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return RedirectToAction("PageNotFound", "Access");
            }
            return RedirectToAction("UserList","User");
        }
    }
}