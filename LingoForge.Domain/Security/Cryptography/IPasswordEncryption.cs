namespace LingoForge.Domain.Security.Cryptography;

public interface IPasswordEncryption
{
    string Encrypt(string password);
    bool Verify(string password, string passwordHash);
}
