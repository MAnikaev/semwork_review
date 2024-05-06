namespace ZooHelp.Entities;

public class User
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Phone { get; set; }
    public string ImageUrl { get; set; }
    
    public List<Announcement> Announcements { get; set; }
    public List<Chat> Chats { get; set; }
}