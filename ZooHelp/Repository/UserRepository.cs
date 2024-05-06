using Microsoft.EntityFrameworkCore;
using ZooHelp.Abstractions;
using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Repository;

public class UserRepository(AppDbContext context, IHasher hasher, ILogger<UserRepository> logger): IUserRepository
{
    public async Task<User> GetUserByEmailAsync(string email)
    {
        try
        {
            return (await context.Users.Include(u => u.Announcements)
                .Include(u => u.Chats)
                .FirstOrDefaultAsync(u => u.Email == email))!;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting user {email}", email);
            throw;
        }
    }

    public async Task AddUserAsync(RegistrationModel model)
    {
        var user = new User()
        {
            Chats = [],
            Announcements = [],
            Email = model.Email,
            ImageUrl = model.ImageUrl,
            Name = model.Name,
            PasswordHash = hasher.Hash(model.Password),
            Phone = model.Phone,
            RegistrationDate = DateTime.Now,
            Surname = model.Surname
        };
        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on adding user {email}", model.Email);
        };
    }

    public async Task<User> UpdateAsync(UserUpdateInfo model)
    {
        try
        {
            var oldUser = await context.Users.Include(u => u.Announcements)
                .Include(u => u.Chats)
                .SingleAsync(u => u.Email == model.Email);
            var newUser = new User()
            {
                Email = oldUser.Email,
                RegistrationDate = oldUser.RegistrationDate,
                Announcements = oldUser.Announcements,
                ImageUrl = oldUser.ImageUrl,
                Name = model.Name,
                Surname = model.Surname,
                PasswordHash = hasher.Hash(model.Password),
                Phone = oldUser.Phone,
                Chats = oldUser.Chats
            };
            var result = context.Users.Update(newUser).Entity;
            await context.SaveChangesAsync();
            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on updating user {userEmail}", model.Email);
            throw;
        }
    }
}