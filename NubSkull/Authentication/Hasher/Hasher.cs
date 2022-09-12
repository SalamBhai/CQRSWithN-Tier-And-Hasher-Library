using System.Security.Cryptography;
using System.Text;
using NubSkull.Exceptions;

namespace NubSkull.Authentication.Hasher;

public class Hasher : IHasher
{
    public string GenerateHash(string Password)
    {
        try
        {
            return PrependHashPasswordWithSalt(Password);
        }
        catch (HasherException ex)
        {
            throw ex;
        }
        finally
        {
            Console.WriteLine("Hashing Process For Password..");
        }
    }

    private string PrependHashPasswordWithSalt(string passwordToHash)
    {
        var hashWithSalt = "";
        string salt;
        var hashText = HashPasswordTextWithSalt(passwordToHash, out salt);
        hashWithSalt = salt + "@" + hashText;
        return hashWithSalt;
    }
    private string HashPasswordTextWithSalt(string passwordText, out string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            salt = GenerateSalt();
            var textToHash = passwordText + salt;
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textToHash));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        }
    }

    private string GenerateSalt()
    {
        byte[] bytes = new byte[128 / 8];
        using (var randomGenerator = RandomNumberGenerator.Create())
        {
            randomGenerator.GetBytes(bytes);
            var salt = BitConverter.ToString(bytes).Replace("-", "");
            return salt;
        }
    }

    private string GenerateHashForValidation(string passwordText, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            if (salt == null) return "";
            var textToHash = passwordText + salt;
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textToHash));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    public bool ValidatePassword(string SavedPassword, string Password)
    {
        try
        {
            var hashedPasswordAndSalt = SavedPassword.Split('@');
            if (hashedPasswordAndSalt == null || hashedPasswordAndSalt.Length != 2)
            {
                return false;
            }
            var salt = hashedPasswordAndSalt[0];
            if (salt == null)
            {
                return false;
            }
            var hashOfPasswordToCheck = GenerateHashForValidation(Password, salt);
            Console.WriteLine(hashOfPasswordToCheck);
            if (String.Compare(hashedPasswordAndSalt[1], hashOfPasswordToCheck) == 0)
            {
                return true;
            }
            return false;
        }
        catch (HasherException ex)
        {
            throw ex;
        }
    }
}
