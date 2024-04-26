using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using Model.General;
using Model.Services.Categories.Interfaces;
using Model.Services.Interfaces;

namespace WebLibrary.Controllers.Categories;

[Route("Gpu")]
public class GpuController(IGpuService gpuService, ICartService cartService) : Controller
{
    private ICartService CartService { get; } = cartService;
    private IGpuService GpuService { get; } = gpuService;


    [Route("")]
    public IActionResult Index(string? team = null)
    {
        var model = GpuService.ReturnModel();
        model.QuickFilter = team;
        return View("~/Views/Product/Index.cshtml", model);
    }

    [Route("Filter")]
    public IActionResult Filter(List<ParamBaseModel> param)
    {
        var filterDtos = GpuService.GenerateListOfFilteredProducts(param);
        return PartialView("~/Views/Product/_FilteredProdcts.cshtml", filterDtos);
    }

    [Route("GpuProduct/{id:int}")]
    public IActionResult GetProduct(int id)
    {
        var gpuProduct = GpuService.GetProduct(id);
        gpuProduct.RedirectUrl = Request.GetEncodedUrl();
        return View("~/Views/Product/Product.cshtml", gpuProduct);
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