using CVBuilder.Application.Language.DTOs;
using CVBuilder.Application.Skill.DTOs;

namespace CVBuilder.Application.Skill.Mapper
{
    public class LanguageMapper : AppMapperBase
    {
        public LanguageMapper()
        {
            CreateMap<Models.Entities.UserLanguage, LanguageDTO>();
        }
    }
}
