namespace SEP_Web.Helper.Authentication;
public static class Cryptography
{
    public static string EncryptPassword(string pass)
    {
        return BCrypt.Net.BCrypt.HashPassword(pass);
    }

    public static bool VerifyPasswordEncrypted(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}