﻿namespace PasswordGen;

public class Password
{
    private readonly Dictionary<string, string> chunk;
    private readonly char[] password;
    private readonly int length;

    public Password(int total, int count)
    {
        length = 4 * total;
        password = new char[length];
        chunk = new Dictionary<string, string>
        {
           {"upper", "ABCDEFGHJKLMNOPQRSTUVWXYZ"},
           {"lower", "abcdefghijklmnopqrstuvwxyz"},
           {"numbers","0123456789"},
           {"special", "!@#$%^&*?_-"}
        };
    }

    private Task SetPassword(string chars, int start)
    {
        Random random = new();
        for (int i = start; i < length; i += 4)
        {
            char c = chars[random.Next(0, chars.Length)];
            if (password.Any(x => x.Equals(c)))
            {
                i -= 4;
                continue;
            }
            password[i] = c;
        }
        return Task.CompletedTask;
    }

    public async Task<string> CreatePasswordAsync()
    {
        int idx = 0;
        foreach (var item in chunk)
        {
            await SetPassword(item.Value, idx++);
        }
        return new string(password);

    }
}