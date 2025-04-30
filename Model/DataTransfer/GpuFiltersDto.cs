namespace Model.DataTransfer;

public class GpuFiltersDto
{
    public List<string> ManufactureList = [];
    public List<string> AmdGpuProcessorNameList = [];
    public List<string> NvidiaGpuProcessorNameList = [];
    public List<string> MemorySizeList = [];
    public List<string> MemoryTypeList = [];
    public List<string> TeamList = [];
    public List<string> GpuProcessorNameList = [];
    public int PriceMin = 0;
    public int PriceMax = 0;
}