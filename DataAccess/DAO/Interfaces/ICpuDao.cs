using System.Collections.Generic;
using DataAccess.DTO;

namespace DataAccess.DAO.Interfaces;

public interface ICpuDao
{
    List<CpuFiltersDto> CreateFilterParametersList();
    List<CpuProductDto> GetCpuProductSnapshots();
    CpuProductDto GetCpuProduct(int id);
    List<CpuProductDto> GetCpuByTeam(string team);
}