using CVBuilder.Application.Language.Queries;
using CVBuilder.Web.Controllers.V1;

namespace CVBuilder.Web.Mappers
{
    public class LanguageMapper: MapperBase
    {
        public LanguageMapper()
        {
            CreateMap<GetLanguagesByContnentText, GetLanguageByContainInTextQuery>();
        }
    }
}
