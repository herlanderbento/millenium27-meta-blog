namespace M27.MetaBlog.Application.UseCases.Post.Common;

public record FileInput(string Extension, Stream FileStream, string ContentType);