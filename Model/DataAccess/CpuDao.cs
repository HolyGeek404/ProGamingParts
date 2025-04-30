using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess;

public class CpuDao(DbContextOptions<PGPContext> options) : BaseDao(options), ICpuDao
{
    public CpuFiltersDto CreateFilterParametersList()
    {
        var Socket = PgpContext.Cpus.Select(x => x.Socket).Distinct().ToList();
        var Architecture = PgpContext.Cpus.Select(x => x.Architecture).Distinct().ToList();
        var UnlockedMultipler = PgpContext.Cpus.Select(x => x.UnlockedMultipler).Distinct().ToList();
        var team = PgpContext.Cpus.Select(x => x.Team).Distinct().ToList();
       
        return new CpuFiltersDto
        {
            SocketList = Socket,
            ArchitectureList = Architecture,
            UnlockedMultiplerList = UnlockedMultipler,
            TeamList = team
        };
    }

    public List<CpuDto> GetCpuProductSnapshots()
    {
        var result = PgpContext.Cpus.Select(cpu => new CpuDto
        {
            ProductId = cpu.ProductId.ToString(),
            Name = cpu.Name,
            Team = cpu.Team,
            Price = cpu.Price.ToString(),
            Familiy = cpu.Familiy,
            Series = cpu.Series,
            Socket = cpu.Socket,
            SupportedChipsets = cpu.SupportedChipsets,
            RecomendedChipset = cpu.RecomendedChipset,
            Architecture = cpu.Architecture,
            Frequency = cpu.Frequency,
            Cores = cpu.Cores,
            Threads = cpu.Threads,
            UnlockedMultipler = cpu.UnlockedMultipler,
            CacheMemory = cpu.CacheMemory,
            IntegredGpu = cpu.IntegredGpu,
            IntegredGpuModel = cpu.IntegredGpuModel,
            SupportedRam = cpu.SupportedRam,
            Litography = cpu.Litography,
            TDP = cpu.Tdp,
            AdditionalInfo = cpu.AdditionalInfo,
            IncludedCooler = cpu.IncludedCooler,
            Warranty = cpu.Warranty,
            ProductImg = cpu.ProductImg,
            Type = "CPU"
        }).ToList();

        return result;
    }

    public Cpu? GetCpuProduct(int id)
    {
        var result = PgpContext.Cpus.SingleOrDefault(x => x.ProductId == id);

        return result;
    }

    public void SaveProduct(Cpu model)
    {
        PgpContext.Cpus.Attach(model);
        PgpContext.Entry(model).State = EntityState.Modified;

        PgpContext.SaveChanges();
    }

    public void AddNew(Cpu cpu)
    {
        PgpContext.Cpus.Add(cpu);
        PgpContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var cpu = new Cpu { ProductId = id };

        PgpContext.Cpus.Attach(cpu);
        PgpContext.Cpus.Remove(cpu);
        PgpContext.SaveChanges();
    }
}