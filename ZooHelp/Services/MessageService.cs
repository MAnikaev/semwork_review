using ZooHelp.Abstractions;

namespace ZooHelp.Services;

public class MessageService(IMessageRepository repository) : IMessageService
{
    public async Task<bool> AddMessageByEmailsAsync(string senderEmail, string receiverEmail)
    {
        return await repository.AddMessageByEmailsAsync(senderEmail, receiverEmail);
    }
}