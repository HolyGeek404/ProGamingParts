using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class CpuDao(IConfiguration configuration) : BaseDao(configuration), ICpuDao
{
    public List<CpuFiltersDto> CreateFilterParametersList()
    {
        return DbConnection.Query<CpuFiltersDto>("dbo.Cpu_SelectCpuInfoSnapschot").ToList();
    }

    public List<CpuProductDto> GetCpuProductSnapshots()
    {
        return DbConnection.Query<CpuProductDto>("dbo.Cpu_SelectCpuProductsSnapshots").ToList();
    }

    public CpuProductDto GetCpuProduct(int id)
    {
        return DbConnection.QuerySingle<CpuProductDto>("dbo.Cpu_SelectCpuProduct @id", new { id });
    }

    public List<CpuProductDto> GetCpuByTeam(string team)
    {
        return DbConnection.Query<CpuProductDto>("dbo.Cpu_SelectByTeam @team", new { team }).ToList();
    }
}