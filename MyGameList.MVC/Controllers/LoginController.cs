using Microsoft.AspNetCore.Mvc;
using MyGameList.Models;
using MyGameList.WebService.Application;
using MyGameList.Domain.Request;
using Microsoft.AspNetCore.Session;
using System.Security.Cryptography;
using MyGameList.Helper;
using Newtonsoft.Json;
using psdtest.Domain.Response;

namespace MyGameList.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel user)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                var hash = System.Text.Encoding.ASCII.GetString(data);
                var req = new UserRequestDTO
                {
                    Username = user.Name,
                    Password = hash
                };
                var baseUrl = "https://" + HttpContext.Request.Host.Value + "/user_api/login";
                var result = HttpHelper.Post(baseUrl,req).Result;
                var response = JsonConvert.DeserializeObject<UserResponseDTO>(result.Content.ReadAsStringAsync().Result);

                if (response != null)
                {
                    var userId = response.UserId;
                    HttpContext.Session.SetString("UserId", userId.ToString());
                    HttpContext.Session.SetString("UserName", user.Name);
                    return RedirectToAction("Index", "Game");
                }
                else
                {
                    ViewBag.Message = "Username or password is incorrect";
                    return View();
                }
            }
            
        }
    }
}
