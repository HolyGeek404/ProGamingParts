using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class Order
{
    [Key]
    public int OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Block { get; set; } = null!;
    public string Flat { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Pay { get; set; } = null!;
    public string Shipping { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } 
    public List<OrderDetail> OrderDetails { get; set; }
}