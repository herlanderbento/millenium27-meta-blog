
using Microsoft.AspNetCore.Mvc;
using M27.MetaBlog.Api.Extensions;
using M27.MetaBlog.Application.UseCases.Post.UpdatePost;

namespace M27.MetaBlog.Api.ApiModels.Post;

public class UpdatePostApiInput
{
    [FromForm]
    public Guid? CategoryId { get; set; }

    [FromForm]
    public string? Title { get; set; }

    [FromForm]
    public string? Description { get; set; }

    [FromForm]
    public bool? Published { get; set; }
    
    [FromForm]
    public IFormFile? Image { get; set; }

    public UpdatePostInput ToUpdatePostInput(Guid id)
        => new(
            id,
            CategoryId,
            Title,
            Description,
            Published,
            Image?.ToFileInput()
        );
}