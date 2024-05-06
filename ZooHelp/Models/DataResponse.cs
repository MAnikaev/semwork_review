using System.Text.Json.Serialization;

namespace ZooHelp.Models;

public class DataResponse
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}