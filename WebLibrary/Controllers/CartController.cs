using Model.General;
using Model.Services.Interfaces;
using WebLibrary.Data;

namespace WebLibrary.Controllers;

[UserAuthorization]
public class CartController(ICartService cartService) : Controller
{
    private ICartService CartService { get; } = cartService;

    #region View

    public IActionResult Index()
    {
        var model = CartService.ReturnModel(HttpContext.Session.GetString("UserId"));
        return View(model);
    }

    #endregion

    #region Api
    [Route("/api/Remove")]
    public IActionResult Remove(int productId)
    {
        try
        {
            CartService.Remove(productId, HttpContext.Session.GetString("UserId"));
            return Json(new
            {
                success = true
            });
        }
        catch (Exception)
        {
            return Json(new
            {
                success = false
            });
        }
    }
    
    public IActionResult CreateOrder(UserOrderDataModel model)
    {
        try
        {
            model.UserId = HttpContext.Session.GetString("UserId");
            CartService.CreateOrder(model);
            return Json(new
            {
                success = true
            });
        }
        catch 
        {
            return Json(new
            {
                success = false
            });
        }
    }
    
    #endregion
}