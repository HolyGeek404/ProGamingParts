using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class DeliveryAddress
{
    [Key]
    public int DeliveryAddressId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string BlockNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public string Email { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}