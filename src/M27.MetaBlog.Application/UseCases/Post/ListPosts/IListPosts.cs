using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.ListPosts;

public interface IListPosts: IRequestHandler<ListPostsInput, ListPostsOutput>
{
    
}