using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooHelp.Abstractions;
using ZooHelp.Entities;

namespace ZooHelp.Controllers;

[ApiController]
[Route("[controller]")]
public class AnnouncementsController(IAnnouncementService service) : Controller
{
    [Authorize]
    [HttpGet("all")]
    public async Task<JsonResult> GetAllAnnouncements()
    {
        return new JsonResult(await service.GetAllAnnouncementsAsync());
    }
    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddAnnouncement([FromBody] Announcement? announcement)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
        if (email != announcement.UserEmail)
            return Forbid();
        await service.AddAnnouncementToUserByEmailAsync(announcement);
        return Ok(true);
    }

    [Authorize]
    [HttpGet("author")]
    public async Task<IActionResult> GetAnnouncementAuthor([FromQuery] int id)
    {
        var user = await service.GetAnnouncementAuthorAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [Authorize]
    [HttpGet("breed")]
    public async Task<JsonResult> GetAllAnnouncementsByBreed([FromQuery] string breed)
    {
        return new JsonResult(await service.GetAllAnnouncementsByBreedAsync(breed));
    }

    [Authorize]
    [HttpGet("concrete")]
    public async Task<IActionResult> GetAnnouncementById([FromQuery] int id)
    {
        var announcement = await service.GetAnnouncementByIdAsync(id);
        if (announcement == null)
            return NotFound();
        return Ok(announcement);
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAnnouncement([FromBody] Announcement announcement)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
        if (email != announcement.UserEmail)
            return Forbid();
        return Ok(await service.UpdateAnnouncementAsync(announcement));
    }
}