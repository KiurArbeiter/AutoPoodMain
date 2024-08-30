using LogInPood.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogInPood.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public List<UserModel> PutValue()
        {
            var users = new List<UserModel>
            {
                new UserModel{id=1,username="Kiur",password="Kiur"},
                new UserModel{id=2,username="Marken",password="Marken"},
                new UserModel{id=3,username="Henri",password="Henri"},
            };

            return users;
        }

        [HttpPost]
        public IActionResult Verify(UserModel usr)
        {
            var u = PutValue();

            var ue = u.Where(u => u.username.Equals(usr.username));

            var up = ue.Where(p => p.password.Equals(usr.password));

            if (up.Count() == 1)
            {
                ViewBag.message = "Login Success";
                return View("Index");
            }
            else
            {
                ViewBag.message = "Login Failed";
                return View("Privacy");
            }
        }
    }
}
