using CVBuilder.Application.Team.Responses;

namespace CVBuilder.Application.Team.Mapper;

using Models.Entities;

public class GetTeamMapper : AppMapperBase
{
    public GetTeamMapper()
    {
        CreateMap<Team, SmallTeamResult>()
            .ForMember(x => x.CountResumes, y => y.MapFrom(z => z.Resumes.Count))
            .ForMember(x => x.StatusTeam, y => y.MapFrom(z => z.StatusTeam.ToString()));
    }
}