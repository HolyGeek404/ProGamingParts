using Model.Services.Interfaces;
using WebLibrary.Data;

namespace WebLibrary.Controllers;

[UserAuthorization]
public class CartController(ICartService _cartService) : Controller
{
    public IActionResult Index()
    {
        var model = _cartService.ReturnModel(HttpContext.Session.GetString("UserId"));
        return View(model);
    }
}