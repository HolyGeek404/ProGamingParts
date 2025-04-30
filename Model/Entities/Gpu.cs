using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class Gpu
{
    [Key]
    public int ProductId { get; set; }
    public int Price { get; set; }
    public string Name { get; set; } = null!;
    public string Team { get; set; } = null!;
    public string Type { get; set; } = "GPU";
    public string GpuProcessorLine { get; set; } = null!;
    public string PcieType { get; set; } = null!;
    public string MemorySize { get; set; } = null!;
    public string MemoryType { get; set; } = null!;
    public string MemoryBus { get; set; } = null!;
    public string MemoryRatio { get; set; } = null!;
    public string CoreRatio { get; set; } = null!;
    public string CoresNumber { get; set; } = null!;
    public string CoolingType { get; set; } = null!;
    public string OutputsType { get; set; } = null!;
    public string SupportedLibraries { get; set; } = null!;
    public string PowerConnector { get; set; } = null!;
    public string RecommendedPsupower { get; set; } = null!;
    public string Length { get; set; } = null!;
    public string Width { get; set; } = null!;
    public string Height { get; set; } = null!;
    public string Warranty { get; set; } = null!;
    public string ProducentCode { get; set; } = null!;
    public string Pgpcode { get; set; } = null!;
    public string GpuProcessorName { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public string ProductImg { get; set; } = null!;
}
