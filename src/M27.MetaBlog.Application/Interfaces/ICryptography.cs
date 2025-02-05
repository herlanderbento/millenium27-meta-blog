﻿namespace M27.MetaBlog.Application.Interfaces;

public interface ICryptography
{
    Task<string> HashPassword(string password, CancellationToken cancellationToken);
    Task Verify(string password, string passwordHash, CancellationToken cancellationToken);
}