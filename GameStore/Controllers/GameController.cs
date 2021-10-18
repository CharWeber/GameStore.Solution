using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Linq;
using GameStore.Models;


namespace GameStore.Controllers
{
  public class GameController : Controller
  {
    private readonly GameStoreContext _db;

    public GameController(GameStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Games.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Game game)
    {
      _db.Games.Add(game);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // get details
    public ActionResult Details(int id)
    {
      ViewBag.NoUsers = _db.Users.ToList().Count == 0;
      ViewBag.UserId = new SelectList(_db.Users, "UserId", "Name");
      var thisGame = _db.Games
      .Include(game => game.JoinEntities)
      .ThenInclude(join => join.User)
      .FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    // get edit
    public ActionResult Edit(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    // post edit
    [HttpPost]
    public ActionResult Edit(Game game)
    {
      _db.Entry(game).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // get delete
    public ActionResult Delete(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    // post delete (deleteconfirmed)
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      _db.Games.Remove(thisGame);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    //AddUser post
    [HttpPost]
    public ActionResult AddUser(Game game, int UserId)
    {
      if (UserId != 0)
      {
        _db.GameUsers.Add( new GameUser() { GameId = game.GameId, UserId = UserId});
      }
      _db.SaveChanges();
      return RedirectToAction ("Index");
    }

    //DeleteUser post
    [HttpPost]
    public ActionResult DeleteUser(int joinId)
    {
      var joinEntry = _db.GameUsers.FirstOrDefault(entry => entry.GameUserId == joinId);
      _db.GameUsers.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}