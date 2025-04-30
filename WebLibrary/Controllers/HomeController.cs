namespace WebLibrary.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}