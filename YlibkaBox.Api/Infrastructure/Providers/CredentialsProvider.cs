using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace YlibkaBox.Api.Infrastructure.Providers;

public static class CredentialsProvider
{
    public static string GetHash(int value)
    {
        var dataForHash = value.ToString(CultureInfo.InvariantCulture);
        return GetHash(dataForHash);
    }

    public static string GetHash(string value)
    {
        using var algorithm = SHA256.Create();
        var hash = algorithm.ComputeHash(Encoding.Unicode.GetBytes(value));
        var hashString = Encoding.Unicode.GetString(hash);

        return hashString;
    }

    public static Tuple<string, string> GenerateCredentials(string password)
    {
        var salt = GenerateSalt();
        return Tuple.Create(GetHash(password + salt), salt);
    }

    public static string HashPassword(string password, string salt)
    {
        return GetHash(password + salt);
    }

    private static string GenerateSalt()
    {
        var opts = new
        {
            RequiredLength = 8,
            RequiredSaltLength = 6,
            RequiredUniqueChars = 4,
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };

        string[] randomChars = { "ABCDEFGHJKLMNOPQRSTUVWXYZ", "abcdefghijkmnopqrstuvwxyz", "0123456789", "!@$?_-" };

        var rand = new Random(Environment.TickCount);

        var saltChars = new List<char> { 'x', 'a' };

        if (opts.RequireUppercase)
            saltChars.Insert(rand.Next(0, saltChars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]);

        if (opts.RequireLowercase)
            saltChars.Insert(rand.Next(0, saltChars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]);

        if (opts.RequireDigit)
            saltChars.Insert(rand.Next(0, saltChars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]);

        for (var i = saltChars.Count;
             i < opts.RequiredLength
             || saltChars.Distinct().Count() < opts.RequiredUniqueChars;
             i++)
        {
            var rcs = randomChars[rand.Next(0, randomChars.Length)];
            saltChars.Insert(rand.Next(0, saltChars.Count),
                rcs[rand.Next(0, rcs.Length)]);
        }


        return new string(saltChars.ToArray());
    }
}