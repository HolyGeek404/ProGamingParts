using Model.DataTransfer;
using Model.Entities;
using Model.Models.General;
using Model.Services.Categories.CastManagers.Interfaces;

namespace Model.Services.Categories.CastManagers;

public class CpuCastManager : BaseCastManager, ICpuCastManager
{
    public CpuFiltersDto CastToCpuFilterParams(List<ParamBaseModel> param)
    {
        var cpuFilterParams = new CpuFiltersDto();

        foreach (var p in param)
            switch (p.Name)
            {
                case "SocketList":
                    cpuFilterParams.SocketList.Add(p.Value);
                    break;
                case "ArchitectureList":
                    cpuFilterParams.ArchitectureList.Add(p.Value);
                    break;
                case "UnlockedMultiplier":
                    cpuFilterParams.UnlockedMultiplerList.Add(p.Value);
                    break;
                case "TeamList":
                    cpuFilterParams.TeamList.Add(p.Value);
                    break;
                case "PriceMin":
                    cpuFilterParams.PriceMin = int.Parse(p.Value);
                    break;
                case "PriceMax":
                    cpuFilterParams.PriceMax = int.Parse(p.Value);
                    break;
            }

        return cpuFilterParams;
    }

    public Cpu CastProductModelToCooler(ProductModel model)
    {
        var cpu = new Cpu
        {
            ProductId = int.Parse(model.AllParameters["ProductId"]),
            Name = model.AllParameters["Name"],
            Team = model.AllParameters["Team"],
            Price = int.Parse(model.AllParameters["Price"]),
            Familiy = model.AllParameters["Familiy"],
            Series = model.AllParameters["Series"],
            Socket = model.AllParameters["Socket"],
            SupportedChipsets = model.AllParameters["SupportedChipsets"],
            RecomendedChipset = model.AllParameters["RecomendedChipset"],
            Architecture = model.AllParameters["Architecture"],
            Frequency = model.AllParameters["Frequency"],
            Cores = model.AllParameters["Cores"],
            Threads = model.AllParameters["Threads"],
            UnlockedMultipler = model.AllParameters["UnlockedMultipler"],
            CacheMemory = model.AllParameters["CacheMemory"],
            IntegredGpu = model.AllParameters["IntegredGpu"],
            IntegredGpuModel = model.AllParameters["IntegredGpuModel"],
            SupportedRam = model.AllParameters["SupportedRam"],
            Litography = model.AllParameters["Litography"],
            Tdp = model.AllParameters["Tdp"],
            AdditionalInfo = model.AllParameters["AdditionalInfo"],
            IncludedCooler = model.AllParameters["IncludedCooler"],
            Warranty = model.AllParameters["Warranty"],
            ProductImg = model.AllParameters["ProductImg"],
        };

        return cpu;
    }

}