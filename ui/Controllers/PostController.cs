using System.Runtime.Serialization.Json;
using System.IO;
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
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace ui.Controllers
{
    public class PostController : Controller
    {
        private readonly IHttpClientFactory client;
        public PostController(IHttpClientFactory _client)
        {
            this.client = _client;
        }

        [HttpGet]
        public IActionResult ShowPost()
        {
            IEnumerable<PostModel> list;
            var _http = client.CreateClient("apiClient");
            var auth = _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("Post/ShowAllPost").Result;
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Access");
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                list = response.Content.ReadAsAsync<IEnumerable<PostModel>>().Result;
                return View(list);
            }
            return RedirectToAction("PageNotFound", "Access");
        }
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(PostModel post, IFormFile imageUpload)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            if (imageUpload != null)
            {
                byte[] bt;
                using (BinaryReader br = new BinaryReader(imageUpload.OpenReadStream()))
                {
                    bt = br.ReadBytes((int)imageUpload.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(bt);
                MultipartFormDataContent formData = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(post);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formData.Add(bytes, "img", imageUpload.FileName);
                formData.Add(stringContent, "formdata");
                var response = _http.PostAsync("Post/AddPost", formData).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPost", "Post");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }

            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }

        [HttpGet]
        public IActionResult GetPostByID(int id)
        {
            // IEnumerable<PostModel> list;
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.GetAsync("Post/GetPostByID/" + id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var list = response.Content.ReadAsAsync<PostModel>().Result;
                return View(list);
            }
            return RedirectToAction("PageNotFound", "Access");
        }

        public IActionResult UpdatePost(IFormFile imageUpload)
        {
            imageUpload = Request.Form.Files.FirstOrDefault();
            var formViewData = Request.Form["dataObj"];
            var jsonObj = JsonConvert.DeserializeObject(formViewData);
            if (imageUpload != null)
            {
                byte[] bt;
                using (BinaryReader br = new BinaryReader(imageUpload.OpenReadStream()))
                {
                    bt = br.ReadBytes((int)imageUpload.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(bt);
                MultipartFormDataContent formData = new MultipartFormDataContent();
                var jsonObject = JsonConvert.SerializeObject(jsonObj);
                var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                formData.Add(bytes, "img", imageUpload.FileName);
                formData.Add(stringContent, "formdata");
                var _http = client.CreateClient("apiClient");
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var response = _http.PostAsync("Post/UpdatePost", formData).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("ShowPost", "Post");
                }   
                else
                {
                    return RedirectToAction("AccessDenied", "Access");
                }
            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            var _http = client.CreateClient("apiClient");
            _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = _http.DeleteAsync("Post/DeletePost/" + id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("ShowPost", "Post");
            }
            else
            {
                return RedirectToAction("PageNotFound", "Access");
            }
        }
    }
}