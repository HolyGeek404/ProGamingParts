using Model.Factories;
using Model.Models.General;
using System.Collections.Generic;
using Model.Entities;
using Model.Services.Interfaces;
using Newtonsoft.Json;
using WebLibrary.Data;

namespace WebLibrary.Controllers.ApiControllers;

[ApiController]
[Route("ProductApi")]
public class ProductApiController(ICategoryServiceFactory _categoryServiceFactory, IProductManagmentService productManagmentService) : Controller
{
    [HttpPost]
    [Route("{type:alpha}/Filter")]
    public IActionResult Filter([FromBody]List<ParamBaseModel> filterList, string type)
    {
        var filterDtoList = _categoryServiceFactory.GetService(type).GenerateListOfFilteredProducts(filterList);
        return PartialView("~/Views/Product/_FilteredProdcts.cshtml", filterDtoList);
    }

    [HttpGet]
    [WarehouseAuthorization]
    [Route("LoadProducts")]
    public IActionResult LoadProducts()
    {
        var result = productManagmentService.GetProductManagmentModel();
        return PartialView("~/Views/Product/_ProductManagment.cshtml", result);
    }

    [HttpGet]
    [WarehouseAuthorization]
    [Route("LoadProductToEdit/{type:alpha}/{id:int}")]
    public IActionResult LoadProductToEdit(string type, int id)
    {
        var product = _categoryServiceFactory.GetService(type).GetProduct(id);
        return PartialView("~/Views/Product/_EditProduct.cshtml", product);
    }

    [HttpGet]
    [WarehouseAuthorization]
    [Route("LoadProductToAdd/{type:alpha}")]
    public IActionResult LoadProductToAdd(string type)
    {
        var product = new ProductModel();
        
        switch (type)
        {
            case "CPU":
                var jsonCpu = JsonConvert.SerializeObject(new Cpu());
                product.AllParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCpu);
                break;
            case "GPU":
                var jsonGpu = JsonConvert.SerializeObject(new Gpu());
                product.AllParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonGpu);
                break;
            case "COOLER":
                var jsonCooler = JsonConvert.SerializeObject(new Cooler());
                product.AllParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCooler);
                break;
        }

        return PartialView("~/Views/Product/_AddProduct.cshtml", product);
    }

    [HttpGet]
    [WarehouseAuthorization]
    [Route("SaveProduct")]
    public IActionResult SaveProduct(string type, string model)
    {
        var convertedModel = new ProductModel
        {
            AllParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(model)
        };

        _categoryServiceFactory.GetService(type).SaveProduct(convertedModel);

        return Ok();
    }

    [HttpPost]
    [WarehouseAuthorization]
    [Route("AddNew")]
    public IActionResult AddNew(AddNewProductModel newProductModel)
    {
        var convertedModel = new ProductModel
        {
            AllParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(newProductModel.newModel)
        };

        _categoryServiceFactory.GetService(newProductModel.type).AddNewProduct(convertedModel);

        return Ok();
    }

    [HttpDelete]
    [WarehouseAuthorization]
    [Route("Delete/{type:alpha}/{id:int}")]
    public IActionResult Delete(string type, int id)
    {
        _categoryServiceFactory.GetService(type).Delete(id);
        return Ok();
    }
}