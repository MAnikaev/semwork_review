using Microsoft.EntityFrameworkCore;
using ZooHelp.Abstractions;
using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Repository;

public class AnnouncementRepository(AppDbContext context, ILogger<AnnouncementRepository> logger) : IAnnouncementRepository
{
    public async Task AddAnnouncementToUserByEmailAsync(Announcement? announcement)
    {
        try
        {
            await context.Announcements.AddAsync(announcement);
            await context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception on adding announcement to user");
        }
        
    }

    public async Task<Announcement> UpdateAnnouncementAsync(Announcement announcement)
    {
        try
        {
            var result = context.Announcements.Update(announcement).Entity;
            await context.SaveChangesAsync();
            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on updating announcement {announcementId}", announcement.Id);
            return null;
        }
    }

    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public async Task<Announcement?> GetAnnouncementByIdAsync(int id)
    {
        try
        {
            return await context.Announcements.SingleOrDefaultAsync(a => a.Id == id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting announcement by id ({id})", id);
            return null;
        }
        
    }

    public Task<List<Announcement?>> GetAllAnnouncementsAsync()
    {
        try
        {
            return context.Announcements.ToListAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting all announcements");
            return null;
        }
        
    }

    public async Task<UserShortInfo> GetAnnouncementAuthorAsync(int announcementId)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Announcements.Any(a => a.Id == announcementId));
            return new UserShortInfo()
            {
                Email = user.Email,
                Name = user.Name + " " + user.Surname,
                ImageUrl = user.ImageUrl,
                RegistrationDate = user.RegistrationDate
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting announcement author {announcementId}", announcementId);
            throw;
        }
        
    }

    public async Task<List<Announcement?>> GetAllAnnouncementsByBreedAsync(string breed)
    {
        try
        {
            return await context.Announcements.Where(a => a.Breed == breed).ToListAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting all announcements by breed {breed}", breed);
            throw;
        }
        
    }
}