using CVBuilder.Application.Language.DTOs;
using MediatR;
using System.Collections.Generic;

namespace CVBuilder.Application.Language.Queries
{
    public class GetLanguageByContainInTextQuery : IRequest<IEnumerable<LanguageDTO>>
    {
        public string Content { get; set; }
    }
}