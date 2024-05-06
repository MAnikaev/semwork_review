using Microsoft.EntityFrameworkCore;
using ZooHelp.Abstractions;
using ZooHelp.Entities;
using ZooHelp.Models;

namespace ZooHelp.Repository;

public class ChatRepository(AppDbContext context, ILogger<ChatRepository> logger) : IChatRepository
{
    public async Task<List<ChatResponse>> GetAllChatsByEmailAsync(string email)
    {
        try
        {
            var chats = await context.Chats.Where(c => c.FirstUser.Email == email)
                .Include(c => c.Messages)
                .Include(c => c.SecondUser)
                .ToListAsync();
        
            return chats.Select(c => new ChatResponse()
            {
                FirstUserEmail = c.FirstUserEmail,
                SecondUserEmail = c.SecondUserEmail,
                Id = c.Id,
                Messages = c.Messages.Select(m => new MessageResponse()
                {
                    IsMine = email == m.SenderEmail,
                    Text = m.Text,
                    Time = m.Time
                }).ToList(),
                FullName = c.SecondUser.Name + " " + c.SecondUser.Surname,
                ImageUrl = c.SecondUser.ImageUrl
            }).ToList();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting all chats by email {email}", email);
            throw;
        }
    }

    public async Task<int> AddChatAsync(string firstUserEmail, string secondUserEmail)
    {
        try
        {
            var firstUser = await context.Users.SingleAsync(u => u.Email == firstUserEmail);
            var secondUser = await context.Users.SingleAsync(u => u.Email == secondUserEmail);
            var chat = new Chat()
            {
                FirstUser = firstUser,
                SecondUser = secondUser,
                FirstUserEmail = firstUserEmail,
                SecondUserEmail = secondUserEmail,
                Id = new Random().Next(int.MinValue, int.MaxValue),
                Messages = []
            };
            await context.Chats.AddAsync(chat);
            await context.SaveChangesAsync();
            return chat.Id;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on adding chat to {email}", firstUserEmail);
            throw;
        }
    }

    public async Task<ChatResponse?> GetChatByIdAsync(int id, string email)
    {
        try
        {
            var chat = context.Chats.Where(c => c.Id == id)
                .Include(c => c.Messages)
                .Include(c => c.SecondUser);

            var buffer = await chat.Select(c => new ChatResponse()
            {
                FirstUserEmail = c.FirstUserEmail,
                SecondUserEmail = c.SecondUserEmail,
                Id = c.Id,
                Messages = c.Messages.Select(m => new MessageResponse()
                {
                    IsMine = email == m.SenderEmail,
                    Text = m.Text,
                    Time = m.Time
                }).ToList(),
                FullName = c.SecondUser.Name + " " + c.SecondUser.Surname,
                ImageUrl = c.SecondUser.ImageUrl
            }).FirstAsync();
            return buffer;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on getting chat by id {id}", id);
            throw;
        }
        
    }
}