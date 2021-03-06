using CVBuilder.Application.Skill.DTOs;
using MediatR;

namespace CVBuilder.Application.Skill.Commands
{
    public class UpdateSkillCommand: IRequest<SkillResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}