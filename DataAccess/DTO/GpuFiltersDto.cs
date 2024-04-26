namespace DataAccess.DTO;

public class GpuFiltersDto
{
    public string Manufacturer { get; set; } = string.Empty;
    public string AmdGpuProcessorName { get; set; } = string.Empty;
    public string NvidiaGpuProcessorName { get; set; } = string.Empty;
    public string MemorySize { get; set; } = string.Empty;
    public string MemoryType { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
}