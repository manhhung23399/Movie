using Microsoft.AspNetCore.Mvc;

namespace Movie.ApiIntegration.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
