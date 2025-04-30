using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Models.General;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;
using Model.Services.General;

namespace Model.Services.Categories;

public class CoolerService(ICoolerFilterManager coolerFilterManager, ICoolerDao coolerDao, ICoolerCastManager coolerCastManager) : BaseCategoryService, ICategoryService
{
    public ProductModel ReturnModel()
    {
        var coolerProductSnapshots = coolerDao.SelectAll().Cast<object>().ToList();
        var coolerFiltersDtoList = coolerDao.GetFilterParams();

        return new ProductModel
        {
            Products = CreateListOfProducts(coolerProductSnapshots),
            FilterParametersList = coolerFilterManager.CreateFilterParametersList(coolerFiltersDtoList),
            Controller = "Cooler",
            FilterParameterListTemplate = coolerCastManager.CastToJsonFormat(new CoolerFilterDto())
        };
    }

    public ProductModel GetProduct(int id)
    {
        return CreateProduct(coolerDao.SelectById(id));
    }

    public ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param)
    {
        var coolerFilterParams = coolerCastManager.CastToCoolerFilterParams(param);
        var allCoolers = coolerDao.SelectAll().ToList();
        var filteredCoolers = coolerFilterManager.FilterOutProducts(coolerFilterParams, allCoolers);

        return new ProductModel
        {
            Controller = "Cooler",
            Products = CreateListOfProducts(filteredCoolers.Cast<object>().ToList())
        };
    }

    public void SaveProduct(ProductModel model)
    {
        var cooler = coolerCastManager.CastProductModelToCooler(model);

        coolerDao.SaveProduct(cooler);
    }

    public void AddNewProduct(ProductModel model)
    {
        var cooler = coolerCastManager.CastProductModelToCooler(model);

        coolerDao.AddNew(cooler);
    }

    public void Delete(int id)
    {
        coolerDao.Delete(id);
    }
}