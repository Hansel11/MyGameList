using Microsoft.AspNetCore.Mvc;
using MyGameList.Models;
using MyGameList.WebService.Application;
using MyGameList.Domain.Request;
using MyGameList.Helper;
using Newtonsoft.Json;
using psdtest.Domain.Response;

namespace MyGameList.Controllers
{
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel user)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            var hash = System.Text.Encoding.ASCII.GetString(data);
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            var req = new UserRequestDTO
            {
                Username = user.Name,
                Password = hash
            };
            var baseUrl = "https://" + HttpContext.Request.Host.Value + "/user_api/register";
            var result = HttpHelper.Post(baseUrl, req).Result;
            var response = JsonConvert.DeserializeObject<UserResponseDTO>(result.Content.ReadAsStringAsync().Result);

            if (response!=null)
            {
                TempData["msg"] = "<script>alert('User added succesfully');</script>";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Message = "Username already taken";
                return View();
            }
        }
    }
}
