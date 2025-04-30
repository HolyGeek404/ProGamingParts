using Model.DataTransfer;
using Model.Services.Categories.FilterManagers.Interfaces;

namespace Model.Services.Categories.FilterManagers.Cooler;

public class CoolerFilterManager : ICoolerFilterManager
{
    public List<Dictionary<string, List<string>>> CreateFilterParametersList(CoolerFilterDto coolerFiltersDtoList)
    {
        var filters = new List<Dictionary<string, List<string>>>
        {
            new() { { "Manufactures", coolerFiltersDtoList.Manufacture } },
            new() { { "CoolerTypes", coolerFiltersDtoList.CoolerTypes } }
        };

        return filters;
    }

    public List<Entities.Cooler> FilterOutProducts(CoolerFilterDto paramModel, List<Entities.Cooler> gpuSnapshotDtoList)
    {
        var hasManufactures = paramModel.Manufacture.Any();
        var hasCoolerTypes = paramModel.CoolerTypes.Any();

        var filterDtoList = gpuSnapshotDtoList
            .Where(gpu => (!hasManufactures || paramModel.Manufacture.Contains(gpu.Manufacture))
                          && (!hasCoolerTypes || paramModel.CoolerTypes.Contains(gpu.CoolerType))
                          && (paramModel.PriceMin == 0 || gpu.Price >= paramModel.PriceMin)
                          && (paramModel.PriceMax == 0 || gpu.Price <= paramModel.PriceMax))
            .ToList();

        return filterDtoList;

    }
}