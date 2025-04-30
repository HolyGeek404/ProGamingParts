using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;

namespace Model.DataAccess;

public class ProductManagmentDao(DbContextOptions<PGPContext> options) : BaseDao(options), IProductManagmentDao
{
    public List<ProductManagmentDto> GetCpuListManagmentInfo()
    {
        var result = PgpContext.Cpus.Select(x => new ProductManagmentDto
        {
            Name = x.Name,
            Id = x.ProductId,
            Price = x.Price,
            Type = "CPU"
        }).ToList();

        return result;
    }

    public List<ProductManagmentDto> GetGpuListManagmentInfo()
    {
        var result = PgpContext.Gpus.Select(x => new ProductManagmentDto
        {
            Name = x.Name,
            Id = x.ProductId,
            Price = x.Price,
            Type = "GPU"
        }).ToList();

        return result;
    }

    public List<ProductManagmentDto> GetCoolerListManagmentInfo()
    {
        var result = PgpContext.Coolers.Select(x => new ProductManagmentDto
        {
            Name = x.Name,
            Id = x.ProductId,
            Price = x.Price,
            Type = "COOLER"
        }).ToList();

        return result;
    }
}