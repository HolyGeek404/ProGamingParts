using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class User
{
    [Key]
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsActive { get; set; }
    public Guid? Key { get; set; }

    public int DeliveryAddressId { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public List<Cart> CartList { get; set; }
    public List<Order> OrderList { get; set; }
}
