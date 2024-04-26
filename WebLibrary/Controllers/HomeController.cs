using Model.Services.Interfaces;

namespace WebLibrary.Controllers;

public class HomeController(IHomePageService homePageService) : Controller
{
    private IHomePageService HomePageService { get; } = homePageService;

    public IActionResult Index()
    {
        return View(HomePageService.ReturnModel());
    }

    [HttpPost]
    public ActionResult PriceRanges(int timeIntervalId)
    {
        var setsSnapshotModel = HomePageService.ReturnSnapshotModel(timeIntervalId);

        return PartialView("_SetsPricesRanges", setsSnapshotModel);
    }
}