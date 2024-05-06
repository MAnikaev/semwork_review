using ZooHelp.Abstractions;
using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Services;

public class AnnouncementService(
    IAnnouncementRepository announcementRepository,
    IImageService imageService
    ) : IAnnouncementService
{
    public async Task AddAnnouncementToUserByEmailAsync(Announcement? announcement)
    {
        var imageUrl = await imageService.UploadImageAsync(announcement.ImageUrl);
        announcement.ImageUrl = imageUrl;
        await announcementRepository.AddAnnouncementToUserByEmailAsync(announcement);
    }

    public async Task<List<Announcement?>> GetAllAnnouncementsAsync()
    {
        return await announcementRepository.GetAllAnnouncementsAsync();
    }

    public async Task<bool> UpdateAnnouncementAsync(Announcement announcement)
    {
        return (await announcementRepository.UpdateAnnouncementAsync(announcement)) is not null;
    }

    public async Task<UserShortInfo> GetAnnouncementAuthorAsync(int announcementId)
    {
        return await announcementRepository.GetAnnouncementAuthorAsync(announcementId);
    }

    public async Task<Announcement?> GetAnnouncementByIdAsync(int id)
    {
        return await announcementRepository.GetAnnouncementByIdAsync(id);
    }

    public async Task<List<Announcement?>> GetAllAnnouncementsByBreedAsync(string breed)
    {
        return await announcementRepository.GetAllAnnouncementsByBreedAsync(breed);
    }
}