using M27.MetaBlog.Application.UseCases.Post.Common;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using DomainEntities = M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Application.UseCases.Post.ListPosts;

public class ListPosts: IListPosts
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ListPosts(
        IPostRepository postRepository, 
        IUserRepository userRepository, 
        ICategoryRepository categoryRepository
        )
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ListPostsOutput> Handle(
        ListPostsInput input, 
        CancellationToken cancellationToken
        )
    {
        var searchOutput = await _postRepository.Search(
            input.ToSearchInput(), 
            cancellationToken);
        
        return await ToOutput(searchOutput, cancellationToken);
    }

    private async Task<ListPostsOutput> ToOutput(
        SearchOutput<Domain.Entity.Post> searchOutput,
        CancellationToken cancellationToken
        )
    {
        IReadOnlyList<DomainEntities.User>? users = null;
        var usersIdRelated = searchOutput.Items.Select(u => u.AuthorId)
            .Distinct()
            .ToList();
        
        if (usersIdRelated.Any())
            users = await _userRepository.GetListByIds(usersIdRelated, cancellationToken);
        
        IReadOnlyList<DomainEntities.Category>? categories = null;
        var categoriesIdRelated = 
            searchOutput.Items.Select(c => c.CategoryId)
                .Distinct()
                .ToList();
        
        if (categoriesIdRelated.Any())
            categories = await _categoryRepository.GetListByIds(categoriesIdRelated, cancellationToken);
        
        var output = new ListPostsOutput(
            searchOutput.CurrentPage, 
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items.Select(item =>
            {
                var author = users?.FirstOrDefault(u => u.Id == item.AuthorId);
                var category = categories?.FirstOrDefault(c => c.Id == item.CategoryId);

                return PostOutput.FromPost(item, author, category);
            }).ToList()
        );

        return output;
    }
}