using Model.DataTransfer;

namespace Model.Services.Categories.FilterManagers.Interfaces;

public interface ICoolerFilterManager
{
    List<Dictionary<string, List<string>>> CreateFilterParametersList(CoolerFilterDto coolerFiltersDtoList);
    List<Entities.Cooler> FilterOutProducts(CoolerFilterDto paramModel, List<Entities.Cooler> gpuSnapshotDtoList);
}