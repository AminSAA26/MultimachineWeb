using Microsoft.AspNetCore.Mvc;

namespace MultimachineWeb.Controllers
{
    public class MachineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Showit()
        {
            return View();
        }
    }
}
