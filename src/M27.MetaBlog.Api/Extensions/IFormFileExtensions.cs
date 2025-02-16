using M27.MetaBlog.Application.UseCases.Post.Common;

namespace M27.MetaBlog.Api.Extensions;

public static class IFormFileExtensions
{
    public static FileInput? ToFileInput(this IFormFile? formFile)
    {
        if (formFile == null) return null;
        var fileStream = new MemoryStream();
        formFile.CopyTo(fileStream);
        return new FileInput(
            Path.GetExtension(formFile.FileName),
            fileStream,
            formFile.ContentType);
    }
}