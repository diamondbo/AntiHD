namespace AntiHD.Models;

public class Users
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    public string CreateUser { get; set; } = "AntiHd";
    public int RoleId { get; set; } = 1;

}

public class Roles
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Users> Users { get; set; } = new List<Users>();
}