using Microsoft.AspNetCore.Mvc;

namespace Autopood.Models
{
    public class LogInModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
