using CVBuilder.Application.Language.DTOs;
using MediatR;

namespace CVBuilder.Application.Language.Commands
{
    public class UpdateLanguageCommand:IRequest<LanguageDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}