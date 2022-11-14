using Microsoft.AspNetCore.Mvc;

namespace ProjeYeni.Controllers
{
    public class PersonelController : Controller
    {
        public IActionResult Liste()
        {
            return View();
        }
    }
}
