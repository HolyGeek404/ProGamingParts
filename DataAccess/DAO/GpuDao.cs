using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class GpuDao(IConfiguration configuration) : BaseDao(configuration), IGpuDao
{
    public List<GpuFiltersDto> GetGpusFilterParameters()
    {
        return DbConnection.Query<GpuFiltersDto>("dbo.GPUs_SelectGpusInfoSnapschot").ToList();
    }

    public List<GpuProductDto> GetGpuProductSnapshots()
    {
        return DbConnection.Query<GpuProductDto>("dbo.GPUs_SelectGpuProductsSnapshots").ToList();
    }

    public GpuProductDto GetGpuProduct(int id)
    {
        return DbConnection.QuerySingle<GpuProductDto>("dbo.GPUs_SelectGpuProduct @id", new { id });
    }

    public List<GpuProductDto> GetGpuByTeam(string team)
    {
        return DbConnection.Query<GpuProductDto>("dbo.GPUs_SelectByTeam @team", new { team }).ToList();
    }
}