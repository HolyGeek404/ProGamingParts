using Model.DataTransfer;
using Model.Services.Categories.FilterManagers.Interfaces;

namespace Model.Services.Categories.FilterManagers.CPU;

public class CpuFilterManager : ICpuFilterManager
{
    public List<Dictionary<string, List<string>>> CreateFilterParametersList(CpuFiltersDto cpuFiltersDtoList)
    {
        var filters = new List<Dictionary<string, List<string>>>
        {
            new() { { "SocketList", cpuFiltersDtoList.SocketList } },
            new() { { "ArchitectureList", cpuFiltersDtoList.ArchitectureList } },
            new() { { "UnlockedMultiplier", cpuFiltersDtoList.UnlockedMultiplerList } },
            new() { { "TeamList", cpuFiltersDtoList.TeamList } }
        };

        return filters;
    }
    public List<CpuDto> FilterOutProducts(CpuFiltersDto paramModel, List<CpuDto> cpuSnapshotDtoList)
    {
        var hasArchitectureList = paramModel.ArchitectureList.Any();
        var hasSocketList = paramModel.SocketList.Any();
        var hasUnlockedMultiplerList = paramModel.UnlockedMultiplerList.Any();
        var hasTeamList = paramModel.TeamList.Any();

        var filterDtoList = cpuSnapshotDtoList
            .Where(cpu => (!hasArchitectureList || paramModel.ArchitectureList.Contains(cpu.Architecture))
                          && (!hasSocketList || paramModel.SocketList.Contains(cpu.Socket))
                          && (!hasUnlockedMultiplerList || paramModel.UnlockedMultiplerList.Contains(cpu.UnlockedMultipler))
                          && (!hasTeamList || paramModel.TeamList.Contains(cpu.Team))
                          && (paramModel.PriceMin == 0 || int.Parse(cpu.Price) >= paramModel.PriceMin)
                          && (paramModel.PriceMax == 0 || int.Parse(cpu.Price) <= paramModel.PriceMax))
            .ToList();

        return filterDtoList;
    }
}