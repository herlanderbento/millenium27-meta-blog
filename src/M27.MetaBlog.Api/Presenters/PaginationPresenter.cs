namespace M27.MetaBlog.Api.Presenters;

public class PaginationPresenter
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }

    public PaginationPresenter(int currentPage, int perPage, int total)
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
    }
}