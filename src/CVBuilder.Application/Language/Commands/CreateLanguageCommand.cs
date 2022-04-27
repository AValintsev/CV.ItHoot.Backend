using CVBuilder.Application.Language.DTOs;
using MediatR;

namespace CVBuilder.Application.Language.Commands
{
    public class CreateLanguageCommand: IRequest<LanguageDTO>
    {
        public string Name { get; set; }
    }
}