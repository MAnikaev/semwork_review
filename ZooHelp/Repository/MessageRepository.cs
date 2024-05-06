using Microsoft.EntityFrameworkCore;
using ZooHelp.Abstractions;
using ZooHelp.Entities;

namespace ZooHelp.Repository;

public class MessageRepository(AppDbContext context, ILogger<MessageRepository> logger) : IMessageRepository
{
    public async Task<bool> AddMessageByEmailsAsync(string senderEmail, string receiverEmail)
    {
        try
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c =>
                c.FirstUserEmail == senderEmail && c.SecondUserEmail == receiverEmail ||
                c.FirstUserEmail == receiverEmail && c.SecondUserEmail == senderEmail);
            var message = new Message()
            {
                SenderEmail = senderEmail,
                ReceiverEmail = receiverEmail,
                Id = new Random().Next(int.MinValue, int.MaxValue),
                Time = DateTime.Now,
                Chat = chat,
                ChatId = chat.Id
            };
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error on adding message to sender {senderEmail}", senderEmail);
            return false;
        }
        
    }
}