using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Abstractions;

public interface ILoginService
{
    Task<string> RegisterAsync(RegistrationModel model);
    Task<string> Authenticate(AuthModel model);
    Task<bool> UpdateAsync(UserUpdateInfo model);
    Task<User> GetUserByEmailAsync(string email);
}