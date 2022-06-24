using Microsoft.AspNetCore.Mvc;
using MyGameList.Models;
using MyGameList.WebService.Application;
using MyGameList.Domain.Request;

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
            var req = new UserRequestDTO
            {
                Username = user.Name,
                Password = hash
            };
            if (!ModelState.IsValid)
            {
                return View();
            }
            else if (UserManager.RegisterUser(req))
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
