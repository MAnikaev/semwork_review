namespace ZooHelp.Models;

public class MessageResponse
{
    public string Text { get; set; }
    public bool IsMine { get; set; }
    public DateTime Time { get; set; }
}