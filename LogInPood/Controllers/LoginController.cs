using LogInPood.Models;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Verify(UserModel usr)
    {
        var user = _userService.GetUser(usr.username, usr.password);

        if (user != null)
        {
            ViewBag.message = "Login Success";
            return View("Success"); // Views/login/Shared/x
        }
        else
        {
            ViewBag.message = "Login Failed, Please try again!";
            return View("Failed"); // Views/login/Shared/Failed.cshtml
        }
    }
    [HttpPost]
    public IActionResult AdminVerify(AdminModel usr)
    {
        var user = _userService.GetAdmin(usr.username, usr.password);

        if (user != null)
        {
            ViewBag.message = "ADMIN LOGIN SUCCESSFUL";
            return View("AdminLogin"); // Views/login/Shared/AdminLogin.cshtml
        }
        else
        {
            ViewBag.message = "Login Failed, Please try again!";
            return View("Failed"); // Ensure this is the correct path for Failed.cshtml
        }
    }
}
