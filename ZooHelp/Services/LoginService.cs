using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ZooHelp.Abstractions;
using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Services;

public class LoginService(IConfiguration configuration,
    IUserRepository repository,
    IImageService imageService,
    IHasher hasher
    ): ILoginService
{
    public async Task<string> RegisterAsync(RegistrationModel model)
    {
        try
        {
            var imageUrl = await imageService.UploadImageAsync(model.ImageUrl);
            model.ImageUrl = imageUrl;
        }
        catch(Exception ex )
        {
            Console.WriteLine("Image Loading Is Failed");
        }
        
        await repository.AddUserAsync(model);
        return GetJwt(model.Email);
    }

    public async Task<string> Authenticate(AuthModel model)
    {
        var user = await repository.GetUserByEmailAsync(model.Email);
        if (user == null || user.PasswordHash != hasher.Hash(model.Password))
            return string.Empty;
        return GetJwt(model.Email);
    }

    public async Task<bool> UpdateAsync(UserUpdateInfo model)
    {
        await repository.UpdateAsync(model);
        return true;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await repository.GetUserByEmailAsync(email);
    }

    private string GetJwt(string email)
    {
        List<Claim> claims = [
            new Claim("Email", email)
        ];
       
        var key = Encoding.UTF8.GetBytes(configuration["Authorization:JWTKey"]!);
        var jwt = new JwtSecurityToken(
            issuer: "MyAuthIssuer",
            audience: "MyAuthAudience",
            claims: claims,
            expires: DateTime.Now + TimeSpan.FromHours(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}