using System.Web.Mvc;

namespace SAU.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }
    }
}