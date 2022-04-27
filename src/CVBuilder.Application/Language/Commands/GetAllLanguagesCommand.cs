﻿using System.Collections.Generic;
using CVBuilder.Application.Language.Responses;
using MediatR;

namespace CVBuilder.Application.Language.Commands
{
    public class GetAllLanguagesCommand:IRequest<List<LanguageResult>>
    {
        
    }
}