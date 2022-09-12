namespace NubSkull.Authentication.Hasher;

public interface IHasher
{
    string GenerateHash(string Password);
    bool ValidatePassword(string SavedPassword, string Password);
}
