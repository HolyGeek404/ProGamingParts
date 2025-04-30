using Model.DataTransfer;
using Model.Models.General;
using Model.Services.Interfaces;
using System.Web;
using Model.Entities;
using WebLibrary.Data;

namespace WebLibrary.Controllers.ApiControllers;

[Route("UserApi")]
public class UserApiController(IValidationService validationService, IUserService userService, IOrderService orderService) : Controller
{
    private IUserService UserService { get; } = userService;
    private IOrderService OrderService { get; } = orderService; 

    private IValidationService ValidationService { get; } = validationService;

    #region API
    [HttpGet]
    [Route("Orders")]
    [UserAuthorization]
    public IActionResult Orders()
    {
        var model = OrderService.GetOrdersBaseInfoModel(int.Parse(HttpContext.Session.GetString("UserId")));
        return PartialView("~/Views/User/_Orders.cshtml", model);
    }

    [HttpGet]
    [Route("Settings")]
    [UserAuthorization]
    public IActionResult Settings()
    {
        var userData = UserService.GetUserByEmail(HttpContext.Session.GetString("Email"));
        return PartialView("~/Views/User/_Settings.cshtml", userData);
    }

    [Route("LogOut")]
    [UserAuthorization]
    public IActionResult LogOut()
    {
        UserService.LogOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    [Route("login")]
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("registration")]
    public IActionResult RegistrationApi([FromBody]RegistrationFormModel model)
    {
        if (!ValidationService.ValidateRegistrationData(model))
        {
            return Json(new
            {
                success = false
            });
        }

        var activationKey = Guid.NewGuid();
        var userDto = new UserDto
        {
            Name = HttpUtility.HtmlEncode(model.FirstName),
            Surname = HttpUtility.HtmlEncode(model.LastName),
            Email = HttpUtility.HtmlEncode(model.Email),
            Password = UserService.HashPassword(HttpUtility.HtmlEncode(model.Password)),
            Key = activationKey
        };

        if (!UserService.Register(userDto))
        {
            return Json(new
            {
                success = false
            });
        }

        UserService.SendVerficationEmail(userDto.Email, activationKey);

        return Json(new
        {
            success = true
        });
    }

    [HttpPut]
    [Route("SaveNameAndSurnameSetting")]
    public IActionResult SaveNameAndSurnameSetting([FromBody]SaveNameAndSurnameRequest request)
    {
        if (ValidationService.ValidateNameAndSurname(request.name, request.surname))
        {
            var userId = HttpContext.Session.GetString("UserIdToUpdate") ?? HttpContext.Session.GetString("UserId");
            var result = UserService.UpdateNameAndSurnameSetting(userId, request.name, request.surname);
            return Json(new
            {
                success = result
            });
        }

        return Json(new
        {
            success = false
        });

    }

    [HttpPut]
    [Route("SavePasswordSetting")]
    public IActionResult SavePasswordSetting([FromBody]SavePasswordRequest savePasswordRequest)
    {
        if (ValidationService.ValidatePassword(savePasswordRequest.password1, savePasswordRequest.password2))
        {
            var userId = HttpContext.Session.GetString("UserIdToUpdate") ?? HttpContext.Session.GetString("UserId");
            var result = UserService.UpdatePasswordSetting(savePasswordRequest.password1, userId);
            return Json(new
            {
                success = result
            });
        }

        return Json(new
        {
            success = false
        });

    }

    [HttpPut]
    [Route("SaveEmailSetting")]
    public IActionResult SaveEmailSetting([FromBody]string email)
    {
        if (ValidationService.ValidateEmail(email))
        {
            var userId = HttpContext.Session.GetString("UserIdToUpdate") ?? HttpContext.Session.GetString("UserId");
            var result = UserService.UpdateEmailSetting(userId, email);
            return Json(new
            {
                success = result
            });
        }

        return Json(new
        {
            success = false
        });

    }

    [HttpGet]
    [Route("LoadUsersTable")]
    public IActionResult LoadUsersTable()
    {
        var users = UserService.LoadUsers();
        return PartialView("~/Views/User/_UsersTable.cshtml", users);
    }

    [HttpGet]
    [Route("LoadUserOrders")]
    public IActionResult LoadUserOrders(int userId)
    {
        var orders = OrderService.GetOrdersBaseInfoModelJson(userId);
        return Ok(orders);
    }

    [HttpDelete]
    [Route("DeleteUser/{userId}")]
    public void DeleteUser(string userId)
    {
        UserService.DeleteUser(userId);
    }

    [HttpGet]
    [Route("LoadUser")]
    public IActionResult LoadUser(string userId)
    {
        HttpContext.Session.SetString("UserIdToUpdate", userId);
        var userDto = UserService.GetUserById(userId);
        return PartialView("~/Views/User/_Settings.cshtml", userDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("SaveDeliveryAddress")]
    public IActionResult SaveDeliveryAddress([FromBody] DeliveryAddress address)
    {
        try
        {
            address.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            UserService.SaveDeliveryAddress(address);
        }
        catch
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