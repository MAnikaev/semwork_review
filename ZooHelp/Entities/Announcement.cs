using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZooHelp.Entities;

public class Announcement
{
    public int Id { get; set; }
    public DateTime PublicationDate { get; set; }
    public string City { get; set; }
    public int Price { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string AnimalName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Breed { get; set; }
    public List<string> Requirements { get; set; }
    public string ImageUrl { get; set; }
    public string UserEmail { get; set; }
    [ValidateNever]
    public User User { get; set; }
}