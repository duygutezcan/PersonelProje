using Microsoft.AspNetCore.Mvc;

namespace ProjeYeni.Controllers
{
    public class SehirController : Controller
    {
        public IActionResult Liste()
        {
            return View();
        }
    }
}
