using Model.Entities;

namespace Model.DataTransfer;

public class UserDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsLogIn { get; set; }
    public bool IsActive { get; set; }
    public Guid Key { get; set; }

    public DeliveryAddress? DeliveryAddress { get; set; }
}