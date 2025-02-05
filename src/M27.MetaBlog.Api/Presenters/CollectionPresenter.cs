namespace M27.MetaBlog.Api.Presenters;

public class CollectionPresenter<TData>
{
    public TData Data { get; private set; }

    public CollectionPresenter(TData data) 
        => Data = data;
}