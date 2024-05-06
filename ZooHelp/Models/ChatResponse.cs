namespace ZooHelp.Models;

public class ChatResponse
{
    public int Id { get; set; }
    
    public string FirstUserEmail { get; set; }
    public string SecondUserEmail { get; set; }
    public string ImageUrl { get; set; }
    public string FullName { get; set; }
    
    public List<MessageResponse> Messages { get; set; }
}