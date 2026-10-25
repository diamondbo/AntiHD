namespace AntiHD.Shared.Models;

public class Users
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
    public int RoleId { get; set; } = 1;

}

public class Roles
{
    public int Id { get; set; }
    public required string Name { get; set; }
}