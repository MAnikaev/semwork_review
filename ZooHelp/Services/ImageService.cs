using System.Text.Json;
using ZooHelp.Abstractions;
using ZooHelp.Models;

namespace ZooHelp.Services;

public class ImageService(HttpClient client, IConfiguration configuration, ILogger<ImageService> logger) : IImageService
{
    public async Task<string> UploadImageAsync(string imgBase64)
    {
        try
        {
            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(imgBase64), "image");
            var response = await client.PostAsync($"https://api.imgbb.com/1/upload?key={configuration["ImageHosting:APIKey"]}", requestContent);
            var imageResponse = JsonSerializer.Deserialize<ImageResponse>(await response.Content.ReadAsStringAsync());
            return imageResponse.Data.Url;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured with uploading image");
            return string.Empty;
        }
    }
}