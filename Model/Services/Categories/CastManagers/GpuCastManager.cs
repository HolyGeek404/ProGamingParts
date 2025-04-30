using Model.DataTransfer;
using Model.Entities;
using Model.Models.General;
using Model.Services.Categories.CastManagers.Interfaces;

namespace Model.Services.Categories.CastManagers;

public class GpuCastManager : BaseCastManager, IGpuCastManager
{
    public GpuFiltersDto CastToGpuFilterParams(List<ParamBaseModel> param)
    {
        var gpuFilterParams = new GpuFiltersDto();

        foreach (var p in param)
            switch (p.Name)
            {
                case "Manufactures":
                    gpuFilterParams.ManufactureList.Add(p.Value);
                    break;
                case "AmdGpuProcessorNames":
                case "NvidiaGpuProcessorNames":
                case "GpuProcessorLine":
                    gpuFilterParams.GpuProcessorNameList.Add(p.Value);
                    break;

                case "MemorySizes":
                    gpuFilterParams.MemorySizeList.Add(p.Value);
                    break;

                case "MemoryTypes":
                    gpuFilterParams.MemoryTypeList.Add(p.Value);
                    break;
                case "TeamList":
                    gpuFilterParams.TeamList.Add(p.Value);
                    break;
                case "PriceMin":
                    gpuFilterParams.PriceMin = int.Parse(p.Value);
                    break;
                case "PriceMax":
                    gpuFilterParams.PriceMax = int.Parse(p.Value);
                    break;
            }

        return gpuFilterParams;
    }

    public Gpu CastProductModelToCooler(ProductModel model)
    {
        var gpu = new Gpu
        {
            //ProductId = int.Parse(model.AllParameters["ProductId"]),
            Price = int.Parse(model.AllParameters["Price"]),
            Name = model.AllParameters["Name"],
            Team = model.AllParameters["Team"],
            GpuProcessorLine = model.AllParameters["GpuProcessorLine"],
            PcieType = model.AllParameters["PcieType"],
            MemorySize = model.AllParameters["MemorySize"],
            MemoryType = model.AllParameters["MemoryType"],
            MemoryBus = model.AllParameters["MemoryBus"],
            MemoryRatio = model.AllParameters["MemoryRatio"],
            CoreRatio = model.AllParameters["CoreRatio"],
            CoresNumber = model.AllParameters["CoresNumber"],
            CoolingType = model.AllParameters["CoolingType"],
            OutputsType = model.AllParameters["OutputsType"],
            SupportedLibraries = model.AllParameters["SupportedLibraries"],
            PowerConnector = model.AllParameters["PowerConnector"],
            RecommendedPsupower = model.AllParameters["RecommendedPsupower"],
            Length = model.AllParameters["Length"],
            Width = model.AllParameters["Width"],
            Height = model.AllParameters["Height"],
            Warranty = model.AllParameters["Warranty"],
            ProducentCode = model.AllParameters["ProducentCode"],
            Pgpcode = model.AllParameters["Pgpcode"],
            GpuProcessorName = model.AllParameters["GpuProcessorName"],
            Manufacturer = model.AllParameters["Manufacturer"],
            ProductImg = model.AllParameters["ProductImg"],
            Type = model.AllParameters["Type"]
        };

        return gpu;
    }

}