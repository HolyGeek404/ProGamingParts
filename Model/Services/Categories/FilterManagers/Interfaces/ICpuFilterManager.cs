using DataAccess.DTO;
using Model.Cpu;

namespace Model.Services.Categories.FilterManagers.Interfaces;

public interface ICpuFilterManager
{
    List<Dictionary<string, List<string>>> CreateFilterParametersList(List<CpuFiltersDto> cpuFiltersDtos);
    List<CpuProductDto> FilterOutProducts(CpuFilterParams param, List<CpuProductDto> cpuSnapshotDtos);
}