using AntiHD.Models;
using AntiHD.DTOs;
namespace AntiHD.Shared.Services;
public interface IDtoMapper
{
    public Users CreateDTO_to_User(CreateUserDto dto);
}
public class DtoMapper:IDtoMapper
{
    public Users CreateDTO_to_User(CreateUserDto dto)
    {
        var NewU = new Users()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            PasswordHash = "temp",
            Email = dto.Email,
            RoleId = dto.RoleId ?? 1
        };
        return NewU;
    }
}