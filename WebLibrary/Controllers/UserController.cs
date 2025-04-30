using WebLibrary.Data;

namespace WebLibrary.Controllers;

public class UserController : Controller
{
    #region Views
    [UserAuthorization]
    public IActionResult Index()
    {
        return View();
    }

    [AdminAuthorization]
    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult LogIn(string redirectUrl = "")
    {
        HttpContext.Session.SetString("RedirectLink", Uri.UnescapeDataString(redirectUrl));
        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }
    #endregion
}