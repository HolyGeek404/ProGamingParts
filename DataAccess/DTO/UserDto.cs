namespace DataAccess.DTO;

public class UserDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public bool IsLogIn { get; set; }
}