using CVBuilder.Application.Language.DTOs;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Language.Mapper
{
    public class LanguageMapper : AppMapperBase
    {
        public LanguageMapper()
        {
            CreateMap<UserLanguage, LanguageDTO>();
            CreateMap<UserLanguage, LanguageResult>();
        }
    }
}
