using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Abstractions;

public interface IUserRepository
{
    public Task<User> GetUserByEmailAsync(string email);
    public Task AddUserAsync(RegistrationModel model);
    public Task<User> UpdateAsync(UserUpdateInfo user);
}