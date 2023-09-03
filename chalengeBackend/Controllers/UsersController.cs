using Microsoft.AspNetCore.Mvc;

namespace ChalengeBackend.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
