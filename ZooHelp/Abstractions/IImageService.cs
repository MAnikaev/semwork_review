namespace ZooHelp.Abstractions;

public interface IImageService
{
    Task<string> UploadImageAsync(string imgBase64);
}