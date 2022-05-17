using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest;
using CVBuilder.Web.Contracts.V1.Requests.Language;
using CreateLanguageCommand = CVBuilder.Application.Language.Commands.CreateLanguageCommand;
using UpdateLanguageRequest = CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest.UpdateLanguageRequest;

namespace CVBuilder.Web.Mappers
{
    public class LanguageMapper: MapperBase
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
