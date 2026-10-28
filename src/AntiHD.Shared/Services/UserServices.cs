using AntiHD.Models;
using AntiHD.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace AntiHD.Shared.Services;
public interface IUserServices
{
    Task<int> CreateUserAsync(CreateUserDto user);
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
    private IPasswordHasher<Users> _passwordHasher;
    private IDtoMapper _mapper;

    public UserServices(AntiHD_DbContext context, IPasswordHasher<Users> passHasher, IDtoMapper mapper)
    {
        _context = context;
        _passwordHasher = passHasher;
        _mapper = mapper;
    }

    public async Task<int> CreateUserAsync(CreateUserDto user)
    {
        try
        {
            //check email validate username with validation
            var email_check = await _context.Users.AnyAsync(e => e.Email == user.Email);
            if(email_check)
               throw new Exception("Email exists in db");
            var user_entity = _mapper.CreateDTO_to_User(user);
            //hash password
            user_entity.PasswordHash = _passwordHasher.HashPassword(user_entity, user.Password);
    
            //check if the mapper password is overwritten with hash password
            if(user_entity.PasswordHash == "temp")
               throw new Exception("Password hash failed");
            _context.Users.Add(user_entity);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception($"Error creating user: {e.Message}");
        }
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