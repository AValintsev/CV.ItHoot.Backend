using CVBuilder.Application.Team.Commands;
using CVBuilder.Models;

namespace CVBuilder.Application.Team.Mapper;
using Models.Entities;
public class UpdateTeamMapper: AppMapperBase
{
    public UpdateTeamMapper()
    {
        CreateMap<UpdateTeamCommand, Team>();
        CreateMap<UpdateResumeCommand, TeamResume>()
            .ForMember(x=>x.StatusResume,y=>y.MapFrom(z=>StatusTeamResume.NotSelected));
    }
}