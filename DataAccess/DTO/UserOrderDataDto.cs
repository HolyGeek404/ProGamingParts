using System.Collections.Generic;

namespace DataAccess.DTO;

public class UserOrderDataDto
{
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Block { get; set; } = string.Empty;
    public string Flat { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Pay { get; set; } = string.Empty;
    public string Shipping { get; set; } = string.Empty;
    public List<OrderDetailDto> Details { get; set; } = [];
}