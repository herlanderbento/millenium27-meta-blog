﻿namespace M27.MetaBlog.Domain.Shared.SearchableRepository;

public interface ISearchableRepository<Taggregate>
    where Taggregate : AggregateRoot
{
    Task<SearchOutput<Taggregate>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}