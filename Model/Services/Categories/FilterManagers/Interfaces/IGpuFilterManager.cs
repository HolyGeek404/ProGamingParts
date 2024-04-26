using DataAccess.DTO;
using Model.Gpu;

namespace Model.Services.Categories.FilterManagers.Interfaces;

public interface IGpuFilterManager
{
    List<Dictionary<string, List<string>>> CreateFilterParametersList(List<GpuFiltersDto> gpusFiltersDtos);
    List<GpuProductDto> FilterOutProducts(GpuFilterParamsModel paramModel, List<GpuProductDto> gpuSnapshotDtos);
}