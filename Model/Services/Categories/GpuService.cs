using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Models.General;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;
using Model.Services.General;

namespace Model.Services.Categories;

public class GpuService(IGpuFilterManager gpuFilterManager, IGpuDao gpuDao, IGpuCastManager gpuCastManager) : BaseCategoryService, ICategoryService
{
    public ProductModel ReturnModel()
    {
        var gpuProductSnapshots = gpuDao.GetGpuProductSnapshots().Cast<object>().ToList();
        var gpusFiltersDtos = gpuDao.GetGpusFilterParameters();

        return new ProductModel
        {
            Products =  CreateListOfProducts(gpuProductSnapshots),
            FilterParametersList = gpuFilterManager.CreateFilterParametersList(gpusFiltersDtos),
            Controller = "Gpu",
            FilterParameterListTemplate = gpuCastManager.CastToJsonFormat(new GpuFiltersDto())
        };
    }

    public ProductModel GetProduct(int id)
    {
        return CreateProduct(gpuDao.GetGpuProduct(id));
    }

    public ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param)
    {
        var gpuFilterParams = gpuCastManager.CastToGpuFilterParams(param);
        var allGpus = gpuDao.GetGpuProductSnapshots().ToList();
        var filteredGpus = gpuFilterManager.FilterOutProducts(gpuFilterParams, allGpus);

        return new ProductModel
        {
            Products = CreateListOfProducts(filteredGpus.Cast<object>().ToList()),
            Controller = "Gpu"
        };
    }

    public void SaveProduct(ProductModel model)
    {
        var gpu = gpuCastManager.CastProductModelToCooler(model);

        gpuDao.SaveProduct(gpu);
    }

    public void AddNewProduct(ProductModel model)
    {
        var gpu = gpuCastManager.CastProductModelToCooler(model);

        gpuDao.AddNew(gpu);
    }

    public void Delete(int id)
    {
        gpuDao.Delete(id);
    }
}