using System.Collections.Generic;
using DataAccess.DTO;


namespace DataAccess.DAO.Interfaces;

public interface IHomeDao
{
    List<HomePageDto> GetHomePageData();
    List<SetsSnapshotDto> SetsSnapshots();
    List<SetsSnapshotDto> SetsPriceRanges(int timeIntervalId);
}