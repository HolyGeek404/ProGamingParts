namespace Model.DataTransfer;

public class OrdersBaseInfoDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductImg { get; set; } = "";
    public string Type { get; set; } = "";
    public DateTime CreationDate { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
}