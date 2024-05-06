namespace ZooHelp.Abstractions;

public interface IMessageService
{
    Task<bool> AddMessageByEmailsAsync(string senderEmail, string receiverEmail);
}