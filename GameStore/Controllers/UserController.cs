using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;
using GameStore.Models;


namespace GameStore.Controllers
{
  public class UserController : Controller
  {
    private readonly GameStoreContext _db;

    public UserController(GameStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<User> model = _db.Users.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(User user)
    {
      _db.Users.Add(user);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // get details
    public ActionResult Details(int id)
    {
      ViewBag.NoGames = _db.Games.ToList().Count ==0;
      ViewBag.GameId = new SelectList(_db.Games, "GameId", "Name");
      var thisUser = _db.Users
        .Include(user => user.JoinEntities)
        .ThenInclude(join => join.Game)
        .FirstOrDefault(user => user.UserId == id);
      return View(thisUser);
    }

    // get edit
    public ActionResult Edit(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      return View(thisUser);
    }

    // post edit
    [HttpPost]
    public ActionResult Edit(User user)
    {
      _db.Entry(user).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // get delete
    public ActionResult Delete(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      return View(thisUser);
    }

    // post delete
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      _db.Users.Remove(thisUser);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    //add Game
    [HttpPost]
    public ActionResult AddGame(User user, int GameId)
    {
      if (GameId != 0)
      {
        _db.GameUsers.Add(new GameUser() { UserId = user.UserId, GameId = GameId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // Delete Game
    [HttpPost]
    public ActionResult DeleteGame(int joinId)
    {
      var thisJoin = _db.GameUsers.FirstOrDefault(entry => entry.GameUserId ==joinId);
      _db.GameUsers.Remove(thisJoin);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}