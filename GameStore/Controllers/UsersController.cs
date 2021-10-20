using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using GameStore.Models;
using GameStore.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace GameStore.Controllers
{
  [Authorize]
  public class UsersController : Controller
  {
    private readonly GameStoreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager <ApplicationUser> userManager, GameStoreContext db)
    {
      _db = db;
      _userManager = userManager;
    }

    public ActionResult Index()
    {
      // List<User> model = _db.Users.ToList();
      var Users = _userManager.Users.ToList();
      return View(Users);
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