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
            return View("Success"); // Views/login/Shared/Success.cshtml
        }
        else
        {
            ViewBag.message = "Login Failed, Please try again!";
            return View("Failed"); // Views/login/Shared/Failed.cshtml
        }
    }
}
