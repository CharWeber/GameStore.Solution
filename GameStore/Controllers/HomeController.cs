using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}