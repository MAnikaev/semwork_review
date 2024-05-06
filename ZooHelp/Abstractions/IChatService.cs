using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Abstractions;

public interface IChatService
{
    Task<List<ChatResponse?>> GetAllChatsByEmailAsync(string email);
    Task<int> AddChatAsync(string firstUserEmail, string secondUserEmail);
    Task<ChatResponse?> GetChatByIdAsync(int id, string email);
}