using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class HomeDao(IConfiguration configuration) : BaseDao(configuration), IHomeDao
{
    public List<HomePageDto> GetHomePageData()
    {
        return DbConnection.Query<HomePageDto>("dbo.GetHomePageData").ToList();
    }

    public List<SetsSnapshotDto> SetsSnapshots()
    {
        return DbConnection.Query<SetsSnapshotDto>("SELECT * FROM Sets_TimeInterval").ToList();
    }

    public List<SetsSnapshotDto> SetsPriceRanges(int timeIntervalId)
    {
        return DbConnection
            .Query<SetsSnapshotDto>("dbo.Sets_GetTimeIntervals @timeIntervalId", new { timeIntervalId }).ToList();
    }
}