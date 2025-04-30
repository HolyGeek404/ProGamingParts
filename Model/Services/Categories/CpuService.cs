using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Models.General;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;
using Model.Services.General;

namespace Model.Services.Categories;

public class CpuService(ICpuFilterManager cpuFilterManager, ICpuDao cpuDao, ICpuCastManager cpuCastManager) : BaseCategoryService, ICategoryService
{
    public ProductModel ReturnModel()
    {
        var cpuProductSnapshots = cpuDao.GetCpuProductSnapshots().Cast<object>().ToList();
        var cpuFiltersDtoList = cpuDao.CreateFilterParametersList();

        return new ProductModel
        {
            Products = CreateListOfProducts(cpuProductSnapshots),
            FilterParametersList = cpuFilterManager.CreateFilterParametersList(cpuFiltersDtoList),
            Controller = "Cpu",
            FilterParameterListTemplate = cpuCastManager.CastToJsonFormat(new CpuFiltersDto())
        };
    }

    public ProductModel GetProduct(int id)
    {
        return CreateProduct(cpuDao.GetCpuProduct(id));
    }

    public ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param)
    {
        var cpuFilterParams = cpuCastManager.CastToCpuFilterParams(param);
        var allCpus = cpuDao.GetCpuProductSnapshots().ToList();
        var filteredCpus = cpuFilterManager.FilterOutProducts(cpuFilterParams, allCpus);

        return new ProductModel
        {
            Controller = "Cpu",
            Products = CreateListOfProducts(filteredCpus.Cast<object>().ToList()),
        };
    }

    public void SaveProduct(ProductModel model)
    {
        var cpu = cpuCastManager.CastProductModelToCooler(model);

        cpuDao.SaveProduct(cpu);
    }

    public void AddNewProduct(ProductModel model)
    {
        var cpu = cpuCastManager.CastProductModelToCooler(model);

        cpuDao.AddNew(cpu);
    }

    public void Delete(int id)
    {
        cpuDao.Delete(id);
    }
}