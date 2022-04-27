using CVBuilder.Application.Skill.DTOs;
using MediatR;


namespace CVBuilder.Application.Skill.Commands
{
    public class CreateSkillCommand : IRequest<SkillDTO>
    {
        public string Name { get; set; }
    }
}
