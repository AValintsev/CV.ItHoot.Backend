using System;

namespace CVBuilder.Application.Core.Exceptions;

public class ConflictException:Exception
{
    public ConflictException(string message):base(message){}
}