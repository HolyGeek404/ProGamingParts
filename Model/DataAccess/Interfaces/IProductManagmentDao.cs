using Model.DataTransfer;

namespace Model.DataAccess.Interfaces;

public interface IProductManagmentDao
{
    List<ProductManagmentDto> GetCpuListManagmentInfo();
    List<ProductManagmentDto> GetGpuListManagmentInfo();
    List<ProductManagmentDto> GetCoolerListManagmentInfo();
}