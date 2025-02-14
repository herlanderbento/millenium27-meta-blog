namespace M27.MetaBlog.Infra.Storage.Configuration;

public class StorageServiceOptions
{
    public const string ConfigurationSection = "Storage";
    public string BucketName { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string AccessKey { get; set; } = null!; // Adicionado
    public string SecretKey { get; set; } = null!; // Adicionado

}