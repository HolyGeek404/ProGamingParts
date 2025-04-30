using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess.Interfaces;

public interface IGpuDao
{
    GpuFiltersDto GetGpusFilterParameters();
    List<GpuDto> GetGpuProductSnapshots();
    Gpu? GetGpuProduct(int id);
    void SaveProduct(Gpu gpu);
    void AddNew(Gpu gpu);
    void Delete(int id);
}