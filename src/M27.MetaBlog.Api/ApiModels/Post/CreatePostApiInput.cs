using M27.MetaBlog.Application.UseCases.Post.CreatePost;
using Microsoft.AspNetCore.Mvc;
using M27.MetaBlog.Api.Extensions;

namespace M27.MetaBlog.Api.ApiModels.Post;

public class CreatePostApiInput
{

    [FromForm]
    public Guid AuthorId { get; set; }

    [FromForm]
    public Guid CategoryId { get; set; }

    [FromForm]
    public required  string Title { get; set; }

    [FromForm]
    public required  string Description { get; set; }

    [FromForm]
    public bool Published { get; set; }
    
    [FromForm]
    public IFormFile? Image { get; set; }

    public CreatePostInput ToCreatePostInput()
        => new(
            AuthorId,
            CategoryId,
            Title,
            Description,
            Published,
            Image?.ToFileInput()
        );
}