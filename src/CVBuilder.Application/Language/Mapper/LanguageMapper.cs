using CVBuilder.Application.Language.DTOs;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Language.Mapper
{
    public class LanguageMapper : AppMapperBase
    {
        public LanguageMapper()
        {
            CreateMap<Models.Entities.Language, LanguageDTO>();
            CreateMap<Models.Entities.Language, LanguageResult>();
        }
    }
}
