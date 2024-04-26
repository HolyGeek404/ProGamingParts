using System.Collections.Generic;
using DataAccess.DTO;

namespace DataAccess.DAO.Interfaces;

public interface IGpuDao
{
    List<GpuFiltersDto> GetGpusFilterParameters();
    List<GpuProductDto> GetGpuProductSnapshots();
    GpuProductDto GetGpuProduct(int id);
    List<GpuProductDto> GetGpuByTeam(string team);
}