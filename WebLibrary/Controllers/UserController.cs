using System.Web;
using DataAccess.DTO;
using Model.General;
using Model.Services.Interfaces;
using WebLibrary.Data;

namespace WebLibrary.Controllers;

public class UserController(IUserService userService, IOrderService orderService) : Controller
{
    private IUserService UserService { get; } = userService;
    private IOrderService OrderService { get; } = orderService;

    #region Views

    [UserAuthorization]
    public IActionResult Index()
    {
        return View();
    }

    [UserAuthorization]
    public IActionResult Orders()
    {
        var model = OrderService.GetOrdersBaseInfoModel(HttpContext.Session.GetString("UserId"));
        return PartialView("_Orders", model);
    }

    [UserAuthorization]
    public IActionResult Settings()
    {
        return PartialView("_Settings");
    }
    
    [UserAuthorization]
    public IActionResult LogOut()
    {   
        UserService.LogOut();
        return RedirectToAction("Index", "Home");
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

    #region API

    public void PrepareOrder(PrepareOrderModel prepareOrderModel)
    {
        OrderService.PrepareOrder(prepareOrderModel);
    }

    public IActionResult CreateOrder(int productId)
    {
        OrderService.CreateOrder(productId, HttpContext.Session.GetString("UserId"));

        return RedirectToAction("Index", "User", "Orders");
    }


    [Route("/api/login")]
    public IActionResult LogInApi(string email, string password)
    {
        var emailEncoded = HttpUtility.HtmlEncode(email);
        var passwordEncoded = HttpUtility.HtmlEncode(password);

        if (!UserService.LogIn(emailEncoded, passwordEncoded))
        {
            return Json(new
            {
                success = false
            });
        }
     
        return Json(new
        {
            redirectLink = HttpContext.Session.GetString("RedirectLink"),
            success = true
        });
    }

    [Route("/api/registration")]
    public IActionResult RegistrationApi(RegistrationFormModel model)
    {
        var userDto = new UserDto
        {
            Name = HttpUtility.HtmlEncode(model.FirstName),
            Surname = HttpUtility.HtmlEncode(model.LastName),
            Email = HttpUtility.HtmlEncode(model.Email),
            Password = UserService.HashPassword(HttpUtility.HtmlEncode(model.Password))
        };

        if (!UserService.Register(userDto))
        {
            return Json(new
            {
                success = false
            });
        }

        return Json(new
        {
            success = true
        });
    }

    #endregion
}