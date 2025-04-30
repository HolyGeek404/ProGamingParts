namespace Model.DataTransfer;

public class CartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProductImg { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int TotalPrice { get; set; }
}