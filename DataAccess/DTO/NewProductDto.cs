namespace DataAccess.DTO;

public class NewProductDto
{
    public int? NewProductId { get; set; }
    public string NewProductName { get; set; } = string.Empty;
    public string NewProductPrice { get; set; } = string.Empty;
}