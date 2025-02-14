using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace M27.MetaBlog.Domain.ValueObject;

public class Slug: Shared.ValueObject
{
    public string Value { get; private set; }

    private Slug(string value)
    {
        Value = value;
    }

    public static Slug Create(string value)
    {
        return new Slug(value);
    }
    
    public static Slug CreateFromText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text cannot be empty.", nameof(text));
            
        }

        string slugText = RemoveDiacritics(text)
            .ToLower()
            .Trim()
            .Replace(" ", "-")
            .Replace("_", "-");

        slugText = Regex.Replace(slugText, @"[^\w-]", "");  // Remove caracteres especiais
        slugText = Regex.Replace(slugText, @"-{2,}", "-");  // Remove múltiplos hífens
        slugText = slugText.Trim('-');                     // Remove hífens no final

        string code = GenerateHexadecimalCode();

        string slug = $"{slugText}-{code}";

        return new Slug(slug);
    }
    
    private static string RemoveDiacritics(string text)
    {
        return string.Concat(
            text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
        ).Normalize(NormalizationForm.FormC);
    }

    private static string GenerateHexadecimalCode()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] bytes = new byte[4];
            rng.GetBytes(bytes);
            int number = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF; // Garante um número positivo
            return number.ToString("x"); // Converte para hexadecimal
        }
    }

    
    public override bool Equals(Shared.ValueObject? other) 
        => other is Slug slug && slug.Value == Value;

    protected override int GetCustomHashCode()
        => HashCode.Combine(Value);
}