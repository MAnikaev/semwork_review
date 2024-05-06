using System.Security.Cryptography;
using System.Text;
using ZooHelp.Abstractions;

namespace ZooHelp.Services;

public class HasherService : IHasher
{
    public string Hash(string obj)
    {
        var bytes = Encoding.UTF8.GetBytes(obj);
        var hash = SHA256.HashData(bytes);
        return Encoding.UTF8.GetString(hash);
    }
}