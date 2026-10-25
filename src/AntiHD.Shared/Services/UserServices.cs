using AntiHD.Models;
using Microsoft.EntityFrameworkCore;
namespace AntiHD.Shared.Services;
public interface IUserServices
{
    Task<int> CreateUserAsync(Users user);
    Task<Users?> GetUserByIdAsync(Guid userId);
    Task<Users?> GetUserByUsernameAsync(string username);
    Task<Users?> GetUserByEmailAsync(string email);
    Task<List<Users>> GetAllUsersAsync();
    Task AddUserAsync(Users user);
    Task UpdateUserAsync(Users user);
    Task DeleteUserAsync(Guid userId);
}

public class UserServices: IUserServices
{
    private readonly AntiHD_DbContext _context;

    public UserServices(AntiHD_DbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateUserAsync(Users user)
    {
        _context.Users.Add(user);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<Users?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<Users?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Users?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<Users>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddUserAsync(Users user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(Users user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}