using DataAccess.DTO;
using Model.Gpu;
using Model.Services.Categories.FilterManagers.Interfaces;

namespace Model.Services.Categories.FilterManagers;

public class GpuFilterManager : IGpuFilterManager
{
    public List<Dictionary<string, List<string>>> CreateFilterParametersList(List<GpuFiltersDto> gpusFiltersDtoList)
    {
        var filters = new List<Dictionary<string, List<string>>>();

        var manufactures = GetManufactures(gpusFiltersDtoList);
        var amdGpuProcessorNames = GetAmdGpuProcessorNames(gpusFiltersDtoList);
        var nvidiaGpuProcessorNames = GetNvidiaGpuProcessorNames(gpusFiltersDtoList);
        var memorySizes = GetMemorySizes(gpusFiltersDtoList);
        var memoryTypes = GetMemoryTypes(gpusFiltersDtoList);
        var teams = GetTeam(gpusFiltersDtoList);

        filters.Add(new Dictionary<string, List<string>>
        {
            { "Manufactures", manufactures }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "AmdGpuProcessorNames", amdGpuProcessorNames }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "NvidiaGpuProcessorNames", nvidiaGpuProcessorNames }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "MemorySizes", memorySizes }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "MemoryTypes", memoryTypes }
        });
        filters.Add(new Dictionary<string, List<string>>
        {
            { "Teams", teams }
        });

        return filters;
    }

    public List<GpuProductDto> FilterOutProducts(GpuFilterParamsModel paramModel, List<GpuProductDto> gpuSnapshotDtoList)
    {
        var filterDtoList = new List<GpuProductDto>();

        foreach (var dto in gpuSnapshotDtoList)
        {
            if (paramModel.Manufactures.Any())
            {
                if (paramModel.Manufactures.Contains(dto.Manufacturer))
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

            if (paramModel.GpuProcessorName.Any())
            {
                if (paramModel.GpuProcessorName.Contains(dto.GpuProcessorName))
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

            if (paramModel.MemorySizes.Any())
            {
                if (paramModel.MemorySizes.Contains(dto.MemorySize))
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

            if (paramModel.MemoryTypes.Any())
            {
                if (paramModel.MemoryTypes.Contains(dto.MemoryType))
                {
                    filterDtoList.Add(dto);
                    filterDtoList = filterDtoList.GroupBy(x => x.Name).Select(z => z.First()).ToList();
                }
                else
                {
                    filterDtoList.Remove(dto);
                }
            }

            if (paramModel.Teams.Any())
            {
                if (paramModel.Teams.Contains(dto.Team))
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

        if (!paramModel.Manufactures.Any() && !paramModel.GpuProcessorName.Any() &&
            !paramModel.MemorySizes.Any() && !paramModel.MemoryTypes.Any() && !paramModel.Teams.Any())
            filterDtoList = gpuSnapshotDtoList;

        return filterDtoList;
    }

    private static List<string> GetManufactures(IEnumerable<GpuFiltersDto> gpusFiltersDtoList)
    {
        var manufactureFilterData = from dto in gpusFiltersDtoList
            where dto.Manufacturer != null
            select dto.Manufacturer;

        return [..manufactureFilterData];
    }

    private static List<string> GetAmdGpuProcessorNames(IEnumerable<GpuFiltersDto> gpusFiltersDtoList)
    {
        var amdGpus = from dto in gpusFiltersDtoList
            where dto.AmdGpuProcessorName != null
            select dto.AmdGpuProcessorName;

        return [..amdGpus];
    }

    private static List<string> GetNvidiaGpuProcessorNames(IEnumerable<GpuFiltersDto> gpusFiltersDtoList)
    {
        var nvidiaGpus = from dto in gpusFiltersDtoList
            where dto.NvidiaGpuProcessorName != null
            select dto.NvidiaGpuProcessorName;

        return [..nvidiaGpus];
    }

    private static List<string> GetMemorySizes(IEnumerable<GpuFiltersDto> gpusFiltersDtoList)
    {
        var memorySizes = from dto in gpusFiltersDtoList
            where dto.MemorySize != null
            select dto.MemorySize;

        return [..memorySizes];
    }

    private static List<string> GetMemoryTypes(IEnumerable<GpuFiltersDto> gpusFiltersDtoList)
    {
        var memoryTypes = from dto in gpusFiltersDtoList
            where dto.MemoryType != null
            select dto.MemoryType;

        return [..memoryTypes];
    }

    private static List<string> GetTeam(IEnumerable<GpuFiltersDto> gpusFiltersDtoList)
    {
        var teams = from dto in gpusFiltersDtoList
                          where dto.Team != null
                          select dto.Team;

        return teams.ToHashSet().ToList();
    }
}