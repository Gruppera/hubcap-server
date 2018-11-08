using Microsoft.AspNetCore.Mvc;

namespace Hubcap.Api.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}