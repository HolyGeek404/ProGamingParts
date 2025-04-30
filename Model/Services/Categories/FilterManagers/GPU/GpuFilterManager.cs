using Model.DataTransfer;
using Model.Services.Categories.FilterManagers.Interfaces;

namespace Model.Services.Categories.FilterManagers.GPU;

public class GpuFilterManager : IGpuFilterManager
{
    public List<Dictionary<string, List<string>>> CreateFilterParametersList(GpuFiltersDto gpuFiltersDtoList)
    {
        var filters = new List<Dictionary<string, List<string>>>
        {
            new() { { "Manufactures", gpuFiltersDtoList.ManufactureList } },
            new() { { "AmdGpuProcessorNames", gpuFiltersDtoList.AmdGpuProcessorNameList } },
            new() { { "NvidiaGpuProcessorNames", gpuFiltersDtoList.NvidiaGpuProcessorNameList } },
            new() { { "MemorySizes", gpuFiltersDtoList.MemorySizeList } },
            new() { { "MemoryTypes", gpuFiltersDtoList.MemoryTypeList } },
            new() { { "TeamList", gpuFiltersDtoList.TeamList } }
        };

        return filters;
    }
    public List<GpuDto> FilterOutProducts(GpuFiltersDto paramModel, List<GpuDto> gpuSnapshotDtoList)
    {
        var hasManufactures = paramModel.ManufactureList.Any();
        var hasGpuProcessorName = paramModel.GpuProcessorNameList.Any();
        var hasTeamList = paramModel.TeamList.Any();
        var hasMemorySizes = paramModel.MemorySizeList.Any();
        var hasMemoryTypes = paramModel.MemoryTypeList.Any();

        var filterDtoList = gpuSnapshotDtoList
            .Where(gpu => (!hasManufactures || paramModel.ManufactureList.Contains(gpu.Manufacturer))
                          && (!hasGpuProcessorName || paramModel.GpuProcessorNameList.Contains(gpu.GpuProcessorName))
                          && (!hasTeamList || paramModel.TeamList.Contains(gpu.Team))
                          && (!hasMemorySizes || paramModel.MemorySizeList.Contains(gpu.MemorySize))
                          && (!hasMemoryTypes || paramModel.MemoryTypeList.Contains(gpu.MemoryType))
                          && (paramModel.PriceMin == 0 || int.Parse(gpu.Price) >= paramModel.PriceMin)
                          && (paramModel.PriceMax == 0 || int.Parse(gpu.Price) <= paramModel.PriceMax))
            .ToList();

        return filterDtoList;
    }
}