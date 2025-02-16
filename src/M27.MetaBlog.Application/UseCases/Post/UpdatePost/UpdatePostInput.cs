using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.UpdatePost;

public class UpdatePostInput: IRequest<PostOutput>
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get;  set; }
    public bool? Published { get; set; }
    public FileInput? Image { get; set; }

    public UpdatePostInput(
        Guid id, 
        Guid? categoryId, 
        string? title, 
        string? description,
        bool? published, 
        FileInput? image
        )
    {
        Id = id;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Published = published;
        Image = image;
    }
}