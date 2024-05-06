using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooHelp.Abstractions;

namespace ZooHelp.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController(IChatService service) : Controller
{
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllChatsByEmail()
    {
        var email = User.Claims.Single(c => c.Type == "Email").Value;
        var chats = await service.GetAllChatsByEmailAsync(email);
        return Ok(chats);
    }

    [Authorize]
    [HttpGet("concrete")]
    public async Task<IActionResult> GetChatById([FromQuery] int id)
    {
        var email = User.Claims.Single(c => c.Type == "Email").Value;
        var chat = await service.GetChatByIdAsync(id, email);
        return chat == null ? NotFound() : Ok(chat);
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddChat([FromBody] string secondUserEmail)
    {
        var email = User.Claims.Single(c => c.Type == "Email").Value;
        return Ok(await service.AddChatAsync(email, secondUserEmail));
    }
}