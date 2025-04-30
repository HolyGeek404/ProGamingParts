using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess;

public class GpuDao(DbContextOptions<PGPContext> options) : BaseDao(options), IGpuDao
{
    public GpuFiltersDto GetGpusFilterParameters()
    {
        var manufactures = PgpContext.Gpus.Select(x => x.Manufacturer).Distinct().ToList();
        var memorySize = PgpContext.Gpus.Select(x => x.MemorySize).Distinct().ToList();
        var memoryType = PgpContext.Gpus.Select(x => x.MemoryType).Distinct().ToList();
        var team = PgpContext.Gpus.Select(x => x.Team).Distinct().ToList();
        var amdGpuProcessorName = PgpContext.Gpus.Where(x => x.GpuProcessorLine == "AMD Radeon").Select(x => x.GpuProcessorName).Distinct().ToList();
        var nvidiaGpuProcessorName = PgpContext.Gpus.Where(x => x.GpuProcessorLine == "Nvidia GeForce").Select(x => x.GpuProcessorName).Distinct().ToList();

        return new GpuFiltersDto
        {
            MemoryTypeList = memoryType,
            AmdGpuProcessorNameList = amdGpuProcessorName,
            ManufactureList = manufactures,
            MemorySizeList = memorySize,
            NvidiaGpuProcessorNameList = nvidiaGpuProcessorName,
            TeamList = team
        };
    }

    public List<GpuDto> GetGpuProductSnapshots()
    {
        var result = PgpContext.Gpus.Select(GPU => new GpuDto
        {
            Price = GPU.Price.ToString(),
            Name = GPU.Name,
            ProductImg = GPU.ProductImg,
            Manufacturer = GPU.Manufacturer,
            GpuProcessorName = GPU.GpuProcessorName,
            MemorySize = GPU.MemorySize,
            MemoryType = GPU.MemoryType,
            ProductId = GPU.ProductId.ToString(),
            MemoryBus = GPU.MemoryBus,
            CoresNumber = GPU.CoresNumber,
            RecommendedPsuPower = GPU.RecommendedPsupower,
            OutputsType = GPU.OutputsType,
            Team = GPU.Team
        }).ToList();

        return result;
    }

    public Gpu? GetGpuProduct(int id)
    {
        var result = PgpContext.Gpus.SingleOrDefault(gpu => gpu.ProductId == id);

        return result;
    }

    public void SaveProduct(Gpu gpu)
    {
        PgpContext.Gpus.Attach(gpu);
        PgpContext.Entry(gpu).State = EntityState.Modified;

        PgpContext.SaveChanges();
    }

    public void AddNew(Gpu gpu)
    {
        PgpContext.Gpus.Add(gpu);
        PgpContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var gpu = new Gpu { ProductId = id };

        PgpContext.Gpus.Attach(gpu);
        PgpContext.Gpus.Remove(gpu);
        PgpContext.SaveChanges();
    }
}