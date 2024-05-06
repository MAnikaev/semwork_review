using ZooHelp.Abstractions;
using ZooHelp.Models;

namespace ZooHelp.Services;

public class ChatService(IChatRepository repository) : IChatService
{
    public async Task<List<ChatResponse?>> GetAllChatsByEmailAsync(string email)
    {
        return await repository.GetAllChatsByEmailAsync(email);
    }

    public async Task<int> AddChatAsync(string firstUserEmail, string secondUserEmail)
    {
        return await repository.AddChatAsync(firstUserEmail, secondUserEmail);
    }

    public async Task<ChatResponse?> GetChatByIdAsync(int id, string email)
    {
        return await repository.GetChatByIdAsync(id, email);
    }
}