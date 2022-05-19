using CVBuilder.Application.Team.Commands;

namespace CVBuilder.Application.Team.Mapper;
using Models.Entities;
public class UpdateTeamMapper: AppMapperBase
{
    public UpdateTeamMapper()
    {
        CreateMap<UpdateTeamCommand, Team>();
        CreateMap<UpdateResumeCommand, TeamResume>();

    }
}