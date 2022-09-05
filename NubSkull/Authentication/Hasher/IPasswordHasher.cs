namespace NubSkull.Authentication.Hasher;

public interface IPasswordHasher
{
    string GenerateHash(string Password);
    bool ValidatePassword(string SavedPassword, string Password);
}
