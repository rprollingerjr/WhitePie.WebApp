using Microsoft.AspNetCore.Mvc;

namespace WhitePie.WebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
