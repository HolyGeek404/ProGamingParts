using DataAccess.DTO;
using Model.Cpu;
using Model.Services.Categories.FilterManagers.Interfaces;

namespace Model.Services.Categories.FilterManagers;

public class CpuFilterManager : ICpuFilterManager
{
    public List<Dictionary<string, List<string>>> CreateFilterParametersList(List<CpuFiltersDto> cpuFiltersDtoList)
    {
        var filters = new List<Dictionary<string, List<string>>>();

        var sockets = GetSockets(cpuFiltersDtoList);
        var architectures = GetArchitectures(cpuFiltersDtoList);
        var unlockedMultiplier = GetUnlockedMultiplier(cpuFiltersDtoList);
        var teams = GetTeams(cpuFiltersDtoList);

        filters.Add(new Dictionary<string, List<string>>
        {
            { "Sockets", sockets }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "Architectures", architectures }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "UnlockedMultiplier", unlockedMultiplier }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "Teams", teams }
        });

        return filters;
    }

    public List<CpuProductDto> FilterOutProducts(CpuFilterParams param, List<CpuProductDto> cpuSnapshotDtoList)
    {
        var filterDtoList = new List<CpuProductDto>();

        foreach (var dto in cpuSnapshotDtoList)
        {
            if (param.Sockets.Count != 0)
            {
                if (param.Sockets.Contains(dto.Socket))
                {
                    filterDtoList.Add(dto);
                    filterDtoList = filterDtoList.GroupBy(x => x.Name).Select(z => z.First()).ToList();
                }
                else
                {
                    filterDtoList.Remove(dto);
                    continue;
                }
            }

            if (param.Architectures.Count != 0)
            {
                if (param.Architectures.Contains(dto.Architecture))
                {
                    filterDtoList.Add(dto);
                    filterDtoList = filterDtoList.GroupBy(x => x.Name).Select(z => z.First()).ToList();
                }
                else
                {
                    filterDtoList.Remove(dto);
                    continue;
                }
            }

            if (param.UnlockedMultiplers.Count != 0)
            {
                if (param.UnlockedMultiplers.Contains(dto.UnlockedMultipler))
                {
                    filterDtoList.Add(dto);
                    filterDtoList = filterDtoList.GroupBy(x => x.Name).Select(z => z.First()).ToList();
                }
                else
                {
                    filterDtoList.Remove(dto);
                }
            }
            
            if (param.Teams.Count != 0)
            {
                if (param.Teams.Contains(dto.Team))
                {
                    filterDtoList.Add(dto);
                    filterDtoList = filterDtoList.GroupBy(x => x.Name).Select(z => z.First()).ToList();
                }
                else
                {
                    filterDtoList.Remove(dto);
                }
            }
        }

        if (param.UnlockedMultiplers.Count == 0 && param.Architectures.Count == 0 &&
            param.Sockets.Count == 0 && param.Teams.Count == 0)
            filterDtoList = cpuSnapshotDtoList;

        return filterDtoList;
    }

    private static List<string> GetUnlockedMultiplier(IEnumerable<CpuFiltersDto> cpuFiltersDtoList)
    {
        var unlockedMultiplierFilterData = from dto in cpuFiltersDtoList
            where dto.UnlockedMultipler != null
            select dto.UnlockedMultipler;

        return [..unlockedMultiplierFilterData];
    }

    private static List<string> GetArchitectures(IEnumerable<CpuFiltersDto> cpuFiltersDtoList)
    {
        var architectureFilterData = from dto in cpuFiltersDtoList
            where dto.Architecture != null
            select dto.Architecture;

        return [..architectureFilterData];
    }

    private static List<string> GetSockets(IEnumerable<CpuFiltersDto> cpuFiltersDtoList)
    {
        var socketFilterData = from dto in cpuFiltersDtoList
            where dto.Socket != null
            select dto.Socket;

        return [..socketFilterData];
    }
    
    private static List<string> GetTeams(IEnumerable<CpuFiltersDto> cpuFiltersDtoList)
    {
        var teams = from dto in cpuFiltersDtoList
            where dto.Team != null
            select dto.Team;

        return teams.ToHashSet().ToList();
    }
}