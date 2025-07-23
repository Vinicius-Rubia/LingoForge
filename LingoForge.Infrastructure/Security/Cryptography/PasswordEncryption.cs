using LingoForge.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace LingoForge.Infrastructure.Security.Cryptography;

internal class PasswordEncryption : IPasswordEncryption
{
    public string Encrypt(string password) => BC.HashPassword(password);

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}
