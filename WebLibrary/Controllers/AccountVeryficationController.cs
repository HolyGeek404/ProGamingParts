using Model.Services.Interfaces;

namespace WebLibrary.Controllers
{
    public class AccountVerificationController(IAccountVerifycationService accountService) : Controller
    {
        public IActionResult Index(string userEmail, Guid key)
        {
            var result = accountService.VerifyAccount(userEmail, key);
            return View(result);
        }
    }
}
