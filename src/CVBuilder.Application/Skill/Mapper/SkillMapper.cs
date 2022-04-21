using CVBuilder.Application.Skill.DTOs;

namespace CVBuilder.Application.Skill.Mapper
{
    class SkillMapper : AppMapperBase
    {
        public SkillMapper()
        {
            CreateMap<Models.Entities.Skill, SkillDTO>();
        }
    }
}
