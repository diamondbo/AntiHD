namespace AntiHD.DTOs;
public class CreateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Email { get; set; }
    public string CreateUser { get; set; } = "AntiHd";
    public int? RoleId { get; set; } 

}
public class ViewUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public int RoleId { get; set; } 
}