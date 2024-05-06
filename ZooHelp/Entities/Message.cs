namespace ZooHelp.Entities;

public class Message
{
    public int Id { get; set; }
    
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public DateTime Time { get; set; }
    
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    
    public string Text { get; set; }
}