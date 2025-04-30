using Model.DataTransfer;

namespace Model.Services.Categories.FilterManagers.Interfaces;

public interface ICpuFilterManager
{
    List<Dictionary<string, List<string>>> CreateFilterParametersList(CpuFiltersDto cpuFiltersDtos);
    List<CpuDto> FilterOutProducts(CpuFiltersDto param, List<CpuDto> cpuSnapshotDtos);
}