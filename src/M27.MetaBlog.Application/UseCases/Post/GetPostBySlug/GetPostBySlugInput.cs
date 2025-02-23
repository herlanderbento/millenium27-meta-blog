using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.GetPostBySlug;

public class GetPostBySlugInput: IRequest<PostOutput>
{
    public string Slug { get; set; }

    public GetPostBySlugInput(string slug) => Slug = slug;
}