using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Application.UseCases.Post.Common;

namespace M27.MetaBlog.Application.UseCases.Post.ListPosts;

public class ListPostsOutput: PaginatedListOutput<PostOutput>
{
    public ListPostsOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<PostOutput> items)
        :base(page, perPage, total, items)
    {}
}