using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Responses;

namespace CVBuilder.Application.Team.Mapper;

using Models.Entities;

public class CreateTeamMapper : AppMapperBase
{
    public CreateTeamMapper()
    {
        CreateMap<CreateTeamCommand, Team>()
            .ForMember(x => x.CreatedUserId, y => y.MapFrom(z => z.UserId));
        CreateMap<CreateResumeCommand, TeamResume>();

        CreateMap<Team, TeamResult>();
        CreateMap<TeamResume, ResumeResult>()
            .ForMember(x => x.ResumeId, y => y.MapFrom(z => z.ResumeId));
    }
}