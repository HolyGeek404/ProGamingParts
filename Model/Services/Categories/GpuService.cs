using DataAccess.DAO.Interfaces;
using Model.General;
using Model.Gpu;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;

namespace Model.Services.Categories;

public class GpuService(IGpuFilterManager gpuFilterManager, IGpuDao gpuDao, IGpuCastManager gpuCastManager) : BaseService, IGpuService
{
    private IGpuFilterManager GpuFilterManager { get; } = gpuFilterManager;
    private IGpuDao GpuDao { get; } = gpuDao;
    private IGpuCastManager GpuCastManager { get; } = gpuCastManager;

    public ProductModel ReturnModel()
    {
        var gpuProductSnapshots = GpuDao.GetGpuProductSnapshots().Cast<object>().ToList();
        var gpusFiltersDtos = GpuDao.GetGpusFilterParameters();

        return new ProductModel
        {
            Products =  CreateListOfProducts(gpuProductSnapshots),
            FilterParametersList = GpuFilterManager.CreateFilterParametersList(gpusFiltersDtos),
            Controller = "Gpu",
            FilterParameterListTemplate = GpuCastManager.CastToJsonFormat(new GpuFilterParamsModel())
        };
    }

    public ProductModel GetProduct(int id)
    {
        return CreateProduct(GpuDao.GetGpuProduct(id));
    }

    public ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param)
    {
        var gpuFilterParams = GpuCastManager.CastToGpuFilterParams(param);
        var allGpus = GpuDao.GetGpuProductSnapshots().ToList();
        var filteredGpus = GpuFilterManager.FilterOutProducts(gpuFilterParams, allGpus);

        return new ProductModel
        {
            Products = CreateListOfProducts(filteredGpus.Cast<object>().ToList()),
            Controller = "Gpu"
        };
    }
}