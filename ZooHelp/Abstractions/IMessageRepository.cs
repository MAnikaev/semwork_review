namespace ZooHelp.Abstractions;

public interface IMessageRepository
{
    Task<bool> AddMessageByEmailsAsync(string senderEmail, string receiverEmail);
}