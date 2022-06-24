using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MyGameList.Models;
using MyGameList.WebService.Application;
using MyGameList.Domain.Request;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;

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
        var result = GameManager.GetAllGame(GetUserId());

        var dto = new GameViewModel
        {
            Games = (from r in result
                    select new Game
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Rating = r.Rating,
                        Genre = r.Genre
                    }).ToList()
        };

        return View(dto);
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
            //return PartialView("_GameModelPartial",game);
            //return RedirectToAction("index");
        }
        var req = game.CreateDto();
        req.UserId = GetUserId();
        GameManager.AddGame(req);
        //var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Controller");
        //return Json(new { Url = redirectUrl });
        return PartialView("_GameModelPartial", game);
    }

    [HttpGet]
    [Route("Update/{id}")]
    public IActionResult Edit(Guid id)
    {
        var data = GameManager.GetGame(id);
        var game = new Game
        {
            Id = data.Id,
            UserId = GetUserId(),
            Name = data.Name,
            Rating = data.Rating,
            Genre = data.Genre
        };
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
        var req = game.CreateDto();
        req.UserId = GetUserId();
        GameManager.EditGame(req);
        //return RedirectToAction("Index");
        return PartialView("_GameModelPartial", game);
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
        GameManager.RemoveGame(game.Id);
        return PartialView("_DeleteGamePartial");
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
}
