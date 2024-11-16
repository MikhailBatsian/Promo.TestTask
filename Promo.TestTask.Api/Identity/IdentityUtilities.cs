using Microsoft.AspNetCore.Identity;

namespace Promo.TestTask.Api.Identity;

public static class IdentityUtilities
{
    public static string ComputeHash(string value)
    {
        return new PasswordHasher<string>().HashPassword(null, value);
    }

    public static bool Verify(string valueHash, string value)
    {
        return new PasswordHasher<string>()
            .VerifyHashedPassword(null, valueHash, value) == PasswordVerificationResult.Success;
    }
}
