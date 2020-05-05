using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ui.Controllers
{
    public class MenuController : Controller
    {
        private readonly IHttpClientFactory client;
        public MenuController(IHttpClientFactory _client)
        {
            this.client = _client;
        }

        [HttpGet]
        public IActionResult AddMenu()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddMenu(MenuModel menu)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.PostAsJsonAsync("Menu/AddMenu", menu).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "User");
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            else
            {
                return RedirectToAction("ShowMenu");
            }
        }
        [HttpGet]
        public IActionResult DeleteMenu(int id)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.DeleteAsync("Menu/DeleteMenu/" + id).Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("ShowMenu");
        }
        [HttpGet]
        public IActionResult ShowMenu()
        {
            IEnumerable<MenuModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("Menu/ShowMenu").Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                list = response.Content.ReadAsAsync<IEnumerable<MenuModel>>().Result;
                return View(list);
            }
            // list = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            // return View(list);
            return RedirectToAction("PageNotFound", "Access");

        }
    }
}