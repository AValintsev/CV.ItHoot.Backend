using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Language;

namespace CVBuilder.Web.Mappers
{
    public class LanguageMapper: MapperBase
    {
        public LanguageMapper()
        {
            CreateMap<GetLanguageByContainInTextQuery, GetLanguageByContainInTextQuery>();
            CreateMap<GetAllLanguages, GetAllLanguagesCommand>();
            CreateMap<CreateLanguage, CreateLanguageCommand>();
        }
    }
}
