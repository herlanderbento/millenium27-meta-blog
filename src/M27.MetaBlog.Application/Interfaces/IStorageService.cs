﻿namespace M27.MetaBlog.Application.Interfaces;

public interface IStorageService
{
    Task Delete(string filePath, CancellationToken cancellationToken);

    Task<string> Upload(
        string fileName, 
        Stream fileStream, 
        string contentType,
        CancellationToken cancellationToken);
}
