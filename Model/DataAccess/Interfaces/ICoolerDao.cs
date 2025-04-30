using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess.Interfaces;

public interface ICoolerDao
{
    CoolerFilterDto GetFilterParams();
    Cooler SelectById(int id);
    List<Cooler> SelectAll();
    void SaveProduct(Cooler cooler);
    void AddNew(Cooler cooler);
    void Delete(int id);
}