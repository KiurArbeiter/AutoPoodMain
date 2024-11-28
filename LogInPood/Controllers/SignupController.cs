using LogInPood.Models;
using Microsoft.AspNetCore.Mvc;

public class SignupController : Controller
{
    private readonly IUserService _userService;

    public SignupController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserModel usr)
    {
        var existingUser = _userService.GetUsers().Any(u => u.username == usr.username);
        if (existingUser)
        {
            ViewBag.message = "Username already exists. Please try another.";
            return View("Signup");
        }

        _userService.AddUser(usr);
        ViewBag.message = "Signup Success";
        return View("SuccessSignup");
    }
}
