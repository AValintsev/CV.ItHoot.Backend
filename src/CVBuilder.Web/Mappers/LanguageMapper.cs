using CVBuilder.Application.Language.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Language;
using CreateLanguageCommand = CVBuilder.Application.Language.Commands.CreateLanguageCommand;

namespace CVBuilder.Web.Mappers
{
    public class LanguageMapper : MapperBase
    {
        public LanguageMapper()
        {
            CreateMap<CreateLanguage, CreateLanguageCommand>();
            CreateMap<GetLanguageByContainInTextQuery, GetLanguageByContainInTextQuery>();
            CreateMap<GetLanguagesByContentText, GetLanguageByContainInTextQuery>();
            CreateMap<GetAllLanguages, GetAllLanguagesQuery>();
        }
    }
}