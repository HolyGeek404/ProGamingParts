namespace Model.DataTransfer;

public class MatchedProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
}