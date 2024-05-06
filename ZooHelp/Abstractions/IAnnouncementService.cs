using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Abstractions;

public interface IAnnouncementService
{
    public Task AddAnnouncementToUserByEmailAsync(Announcement? announcement);
    public Task<List<Announcement?>> GetAllAnnouncementsAsync();
    public Task<bool> UpdateAnnouncementAsync(Announcement announcement);
    public Task<UserShortInfo> GetAnnouncementAuthorAsync(int announcementId);
    public Task<Announcement?> GetAnnouncementByIdAsync(int id);
    public Task<List<Announcement?>> GetAllAnnouncementsByBreedAsync(string breed);
}