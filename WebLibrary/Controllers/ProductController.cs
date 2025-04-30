using Model.Factories;
using Model.Models.General;

namespace WebLibrary.Controllers;

[Route("Product/{type:alpha}")]
public class ProductController(ICategoryServiceFactory serviceFactory) : Controller
{
    [Route("")]
    public IActionResult Index(string type, string? teamQuickFilter)
    {
        var service = serviceFactory.GetService(type);
        var model = service.ReturnModel();

        if (string.IsNullOrEmpty(teamQuickFilter))
            return View(model);

        model.QuickFilter = teamQuickFilter;
        var filteredModel = service.GenerateListOfFilteredProducts([
            new ParamBaseModel
            {
                Name =  type.Equals("cooler") ? "CoolerTypes" : "TeamList",
                Value = teamQuickFilter
            }
        ]);

        model.Products = filteredModel.Products;
        return View(model);
    }

    [Route("{produtId:int}")]
    public IActionResult Product(string type, int produtId)
    {
        var model = serviceFactory.GetService(type).GetProduct(produtId);

        return View(model);
    }
}