using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ui.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory client;
        public CategoryController(IHttpClientFactory _client)
        {
            this.client = _client;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel catModel)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.PostAsJsonAsync("Category/AddCategory", catModel).Result;
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
                return RedirectToAction("ShowCategory");
            }
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.DeleteAsync("Category/DeleteCategory/" + id).Result;
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
                return RedirectToAction("ShowCategory");
            }
        }

        [HttpGet]
        public IActionResult ShowCategory()
        {
            IEnumerable<CategoryModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("Category/ShowCategory").Result;

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                list = response.Content.ReadAsAsync<IEnumerable<CategoryModel>>().Result;
                return View(list);
            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            IEnumerable<CategoryModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("Category/ShowCategory").Result;
            list = response.Content.ReadAsAsync<IEnumerable<CategoryModel>>().Result;
            return Json(list);
        }
    }
}