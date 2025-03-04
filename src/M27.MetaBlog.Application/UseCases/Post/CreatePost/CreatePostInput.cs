using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.CreatePost;

public class CreatePostInput: IRequest<PostOutput>
{
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Published { get;  set; }
    public FileInput? Image { get; set; }

    public CreatePostInput(
        Guid authorId,
        Guid categoryId, 
        string title, 
        string description,
        bool published, 
        FileInput? image
        )
    {
        AuthorId = authorId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Published = published;
        Image = image;
    }
}