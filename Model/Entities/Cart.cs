using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class Cart
{
    [Key]
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Type { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}