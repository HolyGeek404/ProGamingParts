using Model.DataTransfer;

namespace Model.Services.Categories.FilterManagers.Interfaces;

public interface IGpuFilterManager
{
    List<Dictionary<string, List<string>>> CreateFilterParametersList(GpuFiltersDto gpusFiltersDtos);
    List<GpuDto> FilterOutProducts(GpuFiltersDto paramModel, List<GpuDto> gpuSnapshotDtos);
}