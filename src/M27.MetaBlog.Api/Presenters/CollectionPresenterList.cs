using M27.MetaBlog.Application.Common;

namespace M27.MetaBlog.Api.Presenters;

public class CollectionPresenterList<TItemData>
: CollectionPresenter<IReadOnlyList<TItemData>>
{
    public PaginationPresenter Meta { get; private set; }
    
    public CollectionPresenterList(
        int currentPage,
        int perPage,
        int total,
        IReadOnlyList<TItemData> data
    ) : base(data)
    {
        Meta = new PaginationPresenter(currentPage, perPage, total);
    }
    
    public CollectionPresenterList(
        PaginatedListOutput<TItemData> paginatedListOutput
    ) : base(paginatedListOutput.Items)
    {
        Meta = new PaginationPresenter(
            paginatedListOutput.Page,
            paginatedListOutput.PerPage,
            paginatedListOutput.Total
        );
    }

}