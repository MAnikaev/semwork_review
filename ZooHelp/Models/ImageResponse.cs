using System.Text.Json.Serialization;

namespace ZooHelp.Models;

public class ImageResponse
{
    [JsonPropertyName("data")]
    public DataResponse Data { get; set; }
}