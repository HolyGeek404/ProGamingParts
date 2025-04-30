using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Type { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }
}