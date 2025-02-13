using M27.MetaBlog.Application.UseCases.Category.Common;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.Shared.SearchableRepository;

namespace M27.MetaBlog.Application.UseCases.Category.ListCategories;

public class ListCategories : IListCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ListCategoriesOutput> Handle(
        ListCategoriesInput input, 
        CancellationToken cancellationToken
        )
    {
        var searchInput = new SearchInput(
            input.Page,
            input.PerPage,
            input.Search,
            input.Sort,
            input.Dir
        );
        
        var searchOutput = await _categoryRepository.Search(searchInput,cancellationToken);
        
        return await ToOutput(searchOutput);
    }

    private Task<ListCategoriesOutput> ToOutput(SearchOutput<Domain.Entity.Category> searchOutput)
    {
        return Task.FromResult(new ListCategoriesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(CategoryOutput.FromCategory)
                .ToList()
        ));
    }


}