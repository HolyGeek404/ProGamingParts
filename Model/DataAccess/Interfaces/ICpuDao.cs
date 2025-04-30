using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess.Interfaces;

public interface ICpuDao
{
    CpuFiltersDto CreateFilterParametersList();
    List<CpuDto> GetCpuProductSnapshots();
    Cpu? GetCpuProduct(int id);
    void SaveProduct(Cpu model);
    void AddNew(Cpu cpu);
    void Delete(int id);
}