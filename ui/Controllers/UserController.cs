using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ui.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory client;

        public UserController(IHttpClientFactory _client) 
        {
            client = _client;
        }
        public IActionResult Index()
        {
            var _http = client.CreateClient("apiClient");
            var response = _http.GetAsync("weatherforecast").GetAwaiter().GetResult().Content;
            return Json(response.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string EmailID, string Password, User user)
        {
            var _http = client.CreateClient("apiClient");
            var response = _http.PostAsJsonAsync("User/Auth", user).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return RedirectToAction("PageNotFound","Access");
            }
            var _token = response.Content.ReadAsAsync<string>().GetAwaiter().GetResult();
            HttpContext.Session.SetString("token", _token.ToString());
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public IActionResult UserList()
        {
            IEnumerable<User> list;
            var _http = client.CreateClient("apiClient");
            var auth = _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("User/getAll").Result;
            // HttpStatusCode.Forbidden
            // HttpStatusCode.OK
            // HttpStatusCode.BadRequest
            // HttpStatusCode.InternalServerError
            // HttpStatusCode.NotFound
            // HttpStatusCode.NoContent
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                list = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
                return View(list);
            }
            // list = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            // return View(list);
            return RedirectToAction("PageNotFound", "Access");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var _http = client.CreateClient("apiClient");
            var auth = _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.PostAsJsonAsync("User/AddUser", user).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return RedirectToAction("UserList");
            }

        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.DeleteAsync("User/DeleteUser/" + id).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("UserList");
        }

        // public void AuthorizeUser()
        // {
        //     var _http = client.CreateClient("apiClient");
        //      var auth = _http.DefaultRequestHeaders.Authorization =
        //     new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
        // }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            IEnumerable<User> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("User/GetAllUser").Result;
            list = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            return Json(list);
        }
    }
}