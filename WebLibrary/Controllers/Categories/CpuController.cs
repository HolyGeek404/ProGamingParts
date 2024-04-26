using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using Model.General;
using Model.Services.Categories.Interfaces;
using Model.Services.Interfaces;

namespace WebLibrary.Controllers.Categories;

public class CpuController(ICpuService cpuService, ICartService cartService) : Controller
{
    private ICpuService CpuService { get; } = cpuService;
    private ICartService CartService { get; } = cartService;

    public IActionResult Index(string? team = null)
    {
        var model = CpuService.ReturnModel();
        model.QuickFilter = team;
        return View("~/Views/Product/Index.cshtml", model);
    }

    public IActionResult Filter(List<ParamBaseModel> param)
    {
        var filterDtoList = CpuService.GenerateListOfFilteredProducts(param);
        return PartialView("~/Views/Product/_FilteredProdcts.cshtml", filterDtoList);
    }

    public IActionResult GetProduct(int id)
    {
        var cpuProduct = CpuService.GetProduct(id);
        cpuProduct.RedirectUrl = Request.GetEncodedUrl();
        return View("~/Views/Product/Product.cshtml", cpuProduct);
    }

    [Route("GpuProduct/Add")]
    public IActionResult Add(string productId, string quantity)
    {
        try
        {
            CartService.Add(int.Parse(productId), int.Parse(quantity), HttpContext.Session.GetString("UserId"));
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