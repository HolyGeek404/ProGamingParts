using Model.DataAccess.Interfaces;
using Model.Models.General;
using Model.Services.Interfaces;

namespace Model.Services.User;

public class ProductManagmentService(IProductManagmentDao productManagmentDao) : IProductManagmentService
{
    public ProductManagmentModel GetProductManagmentModel()
    {
        var model = new ProductManagmentModel
        {
            CpuList = productManagmentDao.GetCpuListManagmentInfo(),
            GpuList = productManagmentDao.GetGpuListManagmentInfo(),
            CoolerList = productManagmentDao.GetCoolerListManagmentInfo()
        };

        return model;
    }
}