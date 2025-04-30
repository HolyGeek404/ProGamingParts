using Model.Services.ApiServices.Interfaces;

namespace WebLibrary.Controllers.ApiControllers;

[Route("HomeApi")]
public class HomeApiController(IHomeApiService homeApiService) : Controller
{
    [HttpGet]
    [ValidateAntiForgeryToken]
    [Route("Search")]
    public IActionResult Search(string input)
    {
        try
        {
            var result = homeApiService.GetMatchingProducts(input);
            return PartialView("_GlobalSearch", result);
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