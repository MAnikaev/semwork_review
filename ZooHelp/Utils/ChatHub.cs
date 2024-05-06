using Microsoft.AspNetCore.SignalR;
using ZooHelp.Abstractions;

namespace ZooHelp.Utils;

public class ChatHub(IMessageService service) : Hub
{
    private readonly Dictionary<string, string> _connectionIds = new();
    public async Task SendMessage(string senderEmail, string receiverEmail, string message)
    {
        await service.AddMessageByEmailsAsync(senderEmail, receiverEmail);
        if (_connectionIds.TryGetValue(receiverEmail, out var connectionId))
            await Clients.Client(connectionId!).SendAsync("ReceiveMessage", senderEmail, message);
        // await Clients.All.SendAsync("ReceiveMessage", senderEmail, message); ТЕста ради
    }

    public async Task Connect(string email, string connectionId)
    {
        _connectionIds[email] = connectionId;
    }

    public async Task Disconnect(string email)
    {
        _connectionIds.Remove(email);
    }
}