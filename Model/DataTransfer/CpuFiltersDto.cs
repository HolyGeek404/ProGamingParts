namespace Model.DataTransfer;

public class CpuFiltersDto
{
    public List<string> SocketList = [];
    public List<string> ArchitectureList = [];
    public List<string> UnlockedMultiplerList = [];
    public List<string> TeamList = [];
    public int PriceMin = 0;
    public int PriceMax = 0;
}