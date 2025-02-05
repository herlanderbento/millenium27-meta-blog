﻿namespace M27.MetaBlog.Application.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string? message) : base(message)
    {}

    public static void ThrowIfNotNull(object? @object, string exceptionMessage)
    {
        if (@object != null)
            throw new ConflictException(exceptionMessage);
    }
}