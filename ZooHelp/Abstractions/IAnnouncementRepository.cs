using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Abstractions;

public interface IAnnouncementRepository
{
    public Task AddAnnouncementToUserByEmailAsync(Announcement? announcement);
    public Task<List<Announcement?>> GetAllAnnouncementsAsync();
    public Task<UserShortInfo> GetAnnouncementAuthorAsync(int announcementId);
    public Task<Announcement> UpdateAnnouncementAsync(Announcement announcement);
    public Task SaveChangesAsync();

    public Task<Announcement?> GetAnnouncementByIdAsync(int id);
    public Task<List<Announcement?>> GetAllAnnouncementsByBreedAsync(string breed);
}