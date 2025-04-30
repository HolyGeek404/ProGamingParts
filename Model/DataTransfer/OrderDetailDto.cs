namespace Model.DataTransfer;

public class OrderDetailDto
{
    public int ProductId { set; get; } = 0;
    public int Quantity { set; get; } = 0;
    public string Type { get; set; } = string.Empty;
}