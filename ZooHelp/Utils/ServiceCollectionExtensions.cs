using ZooHelp.Abstractions;
using ZooHelp.Repository;
using ZooHelp.Services;

namespace ZooHelp.Utils;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IHasher, HasherService>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IAnnouncementService, AnnouncementService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        return services;
    }
}