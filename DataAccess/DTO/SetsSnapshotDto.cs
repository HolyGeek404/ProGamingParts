namespace DataAccess.DTO;

public class SetsSnapshotDto
{
    public int? SnapshotId { get; set; }
    public string Month { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public int? Price { get; set; }
    public string Team { get; set; } = string.Empty;
    public int? TimeInterval { get; set; }
    public int? TimeIntervalId { get; set; }
}