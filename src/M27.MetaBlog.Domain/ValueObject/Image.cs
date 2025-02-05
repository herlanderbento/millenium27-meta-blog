
namespace M27.MetaBlog.Domain.ValueObject;

public class Image :Shared.ValueObject
{
    public string Path { get; }

    public Image(string path) => Path = path;
    
    public override bool Equals(Shared.ValueObject? other) =>
        other is Image image &&
        Path == image.Path;

    protected override int GetCustomHashCode()
        => HashCode.Combine(Path);
}