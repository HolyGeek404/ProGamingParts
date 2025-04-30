using Model.DataTransfer;
using Model.Models.Cart;
using Model.Services.Interfaces;

namespace WebLibrary.Controllers.ApiControllers;

[Route("CartApi")]
public class CartApiController(ICartService _cartService) : Controller
{
    [HttpPost]
    [Route("Add")]
    public IActionResult Add([FromBody]AddCartModel addCartModel)
    {
        try
        {
            _cartService.Add(addCartModel, HttpContext.Session.GetString("UserId"));
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

    [HttpDelete]
    [Route("Remove/{productId:int}")]
    public IActionResult Remove(int productId)
    {
        try
        {
            _cartService.Remove(productId, HttpContext.Session.GetString("UserId"));
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("CreateOrder")]
    public IActionResult CreateOrder(UserOrderDataDto model)
    {
        try
        {
            model.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            _cartService.CreateOrder(model);
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
}