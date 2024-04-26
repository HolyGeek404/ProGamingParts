using DataAccess.DAO.Interfaces;
using Model.Cpu;
using Model.General;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;

namespace Model.Services.Categories;

public class CpuService : BaseService, ICpuService
{
    public CpuService(ICpuFilterManager cpuFilterManager, ICpuDao cpuDao, ICpuCastManager cpuCastManager)
    {
        CpuFilterManager = cpuFilterManager;
        CpuDao = cpuDao;
        CpuCastManager = cpuCastManager;
    }

    private ICpuFilterManager CpuFilterManager { get; }
    private ICpuDao CpuDao { get; }
    private ICpuCastManager CpuCastManager { get; }

    public ProductModel ReturnModel()
    {
        var cpuProductSnapshots = CpuDao.GetCpuProductSnapshots().Cast<object>().ToList();
        var cpuFiltersDtos = CpuDao.CreateFilterParametersList();

        return new ProductModel
        {
            Products = CreateListOfProducts(cpuProductSnapshots),
            FilterParametersList = CpuFilterManager.CreateFilterParametersList(cpuFiltersDtos),
            Controller = "Cpu",
            FilterParameterListTemplate = CpuCastManager.CastToJsonFormat(new CpuFilterParams())
        };
    }

    public ProductModel GetProduct(int id)
    {
        return CreateProduct(CpuDao.GetCpuProduct(id));
    }

    public ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param)
    {
        var cpuFilterParams = CpuCastManager.CastToCpuFilterParams(param);
        var allCpus = CpuDao.GetCpuProductSnapshots().ToList();
        var filteredCpus = CpuFilterManager.FilterOutProducts(cpuFilterParams, allCpus);

        return new ProductModel
        {
            Controller = "Cpu",
            Products = CreateListOfProducts(filteredCpus.Cast<object>().ToList())
        };
    }
}