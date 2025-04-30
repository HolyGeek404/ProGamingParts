using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess;

public class CoolerDao(DbContextOptions<PGPContext> options) : BaseDao(options), ICoolerDao
{
    public CoolerFilterDto GetFilterParams()
    {
        var manufactureList = PgpContext.Coolers.Select(x => x.Manufacture).Distinct().ToList();
        var coolerTypeList = PgpContext.Coolers.Select(x => x.CoolerType).Distinct().ToList();

        var result = new CoolerFilterDto
        {
            CoolerTypes = coolerTypeList,
            Manufacture = manufactureList,
        };
        return result;
    }
    public Cooler SelectById(int id)
    {
        var result = PgpContext.Coolers.First(x => x.ProductId == id);

        return result;
    }
    public List<Cooler> SelectAll()
    {
        var result = PgpContext.Coolers.ToList();

        return result;
    } 
    public void SaveProduct(Cooler cooler)
    {
        PgpContext.Coolers.Attach(cooler);
        PgpContext.Entry(cooler).State = EntityState.Modified;

        PgpContext.SaveChanges();
    }
    public void AddNew(Cooler cooler)
    {
        PgpContext.Coolers.Add(cooler);
        PgpContext.SaveChanges();
    }    
    public void Delete(int id)
    {
        var cooler = new Cooler { ProductId = id };

        PgpContext.Coolers.Attach(cooler);
        PgpContext.Coolers.Remove(cooler);
        PgpContext.SaveChanges();
    }
}