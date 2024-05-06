namespace ZooHelp.Entities;

public class Chat
{
    public int Id { get; set; }
    
    public string FirstUserEmail { get; set; }
    public User FirstUser { get; set; }
    public string SecondUserEmail { get; set; }
    public User SecondUser { get; set; }
    
    public List<Message> Messages { get; set; }
}