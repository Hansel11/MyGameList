using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MyGameList.Models;
using MyGameList.WebService.Application;
using MyGameList.Domain.Request;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;
using MyGameList.Helper;
using MyGameList.Domain.Response;
using Newtonsoft.Json;

namespace MyGameList.Controllers;

[Route("[controller]")]
public class GameController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.Get("UserId") == null)
        {
            return RedirectToAction("Index", "Login");
        }
        ViewBag.username = HttpContext.Session.GetString("UserName");

        var baseUrl = GetApiUrl() + "get_all";
        var request = new GameRequestDTO
        {
            UserId = GetUserId()
        };
        
        var result = HttpHelper.Post(baseUrl, request).Result;
        var response = JsonConvert.DeserializeObject<List<Game>>(result.Content.ReadAsStringAsync().Result);
        var game = new GameViewModel
        {
            Games = response
        };

        return View(game);
    }

    [HttpGet]
    [Route("Create")]
    public IActionResult Create()
    {
        Game game = new();
        ViewBag.Title = "Add";
        ViewBag.Action = "Create";
        return PartialView("_GameModelPartial", game);
    }

    [HttpPost]
    [Route("Create")]
    public IActionResult Create(Game game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }

        var baseUrl = GetApiUrl() + "add";
        var request = new GameRequestDTO
        {
            Id = game.Id,
            UserId = GetUserId(),
            Name = game.Name,
            Rating = game.Rating,
            Genre = game.Genre
        };

        var result = HttpHelper.Post(baseUrl, request).Result;
        if (result.IsSuccessStatusCode)
        {
            return PartialView("_GameModelPartial", game);
        }
        else return View();
    }

    [HttpGet]
    [Route("Update/{id}")]
    public IActionResult Edit(Guid id)
    {
        var baseUrl = GetApiUrl() + "get";
        var request = new GameRequestDTO
        {
            Id = id
        };

        var result = HttpHelper.Post(baseUrl, request).Result;
        var game = JsonConvert.DeserializeObject<Game>(result.Content.ReadAsStringAsync().Result);
        
        ViewBag.Title = "Edit";
        ViewBag.Action = "Update";
        return PartialView("_GameModelPartial", game);
    }

    [HttpPost]
    [Route("Update")]
    public IActionResult Edit(Game game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }

        var baseUrl = GetApiUrl() + "edit";
        var request = new GameRequestDTO
        {
            Id = game.Id,
            UserId = GetUserId(),
            Name = game.Name,
            Rating = game.Rating,
            Genre = game.Genre
        };

        var result = HttpHelper.Post(baseUrl, request).Result;
        if (result.IsSuccessStatusCode)
        {
            return PartialView("_GameModelPartial", game);
        }
        else return View();
    }

    [HttpGet]
    [Route("Delete")]
    public IActionResult Remove()
    {
        return PartialView("_DeleteGamePartial");
    }

    [HttpPost]
    [Route("Delete")]
    public IActionResult Remove(Game game)
    {
        var baseUrl = GetApiUrl() + "remove";
        var request = new GameRequestDTO
        {
            Id = game.Id
        };

        var result = HttpHelper.Post(baseUrl, request).Result;
        if (result.IsSuccessStatusCode)
        {
            return PartialView("_GameModelPartial", game);
        }
        else return View();
    }

    [HttpGet]
    [Route("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    private Guid GetUserId()
    {
        _ = Guid.TryParse(HttpContext.Session.GetString("UserId"), out Guid userId);
        return userId;
    }

    public string GetApiUrl()
    {
        return "https://" + HttpContext.Request.Host.Value + "/game_api/";
    }
}
