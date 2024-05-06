using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZooHelp.Abstractions;
using ZooHelp.Models;

namespace ZooHelp.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(ILoginService service) : Controller
{

    [HttpPost("auth")]
    public async Task<IActionResult> Authenticate(AuthModel model)
    {
        var jwt = await service.Authenticate(model);
        return jwt.IsNullOrEmpty() ? BadRequest() : Ok(jwt);
    }

    [HttpPost("register")]
    public async Task<JsonResult> Register(RegistrationModel model, IValidationService validationService)
    {
        var errors = validationService.ValidateRegistrationModel(model);
        if (errors.Count != 0)
            return new JsonResult(errors);
        var jwt = await service.RegisterAsync(model);
        return new JsonResult(jwt);
    }

    [Authorize]
    [HttpGet("concrete")]
    public async Task<IActionResult> GetUserInfo()
    {
        var email = User.FindFirst("Email")?.Value;
        var user = await service.GetUserByEmailAsync(email);
        if (user == null)
            return Forbid();
        return Ok(new UserShortInfo()
        {
            Email = email,
            Name = user.Name + user.Surname,
            ImageUrl = user.ImageUrl,
            RegistrationDate = user.RegistrationDate
        });
    }
    
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserInfo(UserUpdateInfo model)
    {
        var isUpdateSuccessful = await service.UpdateAsync(model);
        return isUpdateSuccessful ? Ok(true) : BadRequest();
    }
}