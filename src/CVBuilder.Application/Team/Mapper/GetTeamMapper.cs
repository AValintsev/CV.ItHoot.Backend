using CVBuilder.Application.Team.Responses;

namespace CVBuilder.Application.Team.Mapper;

using Models.Entities;

public class GetTeamMapper : AppMapperBase
{
    public GetTeamMapper()
    {
        CreateMap<Team, SmallTeamResult>()
            .ForMember(x => x.TeamSize, y => y.MapFrom(z => z.Resumes.Count))
            .ForMember(x => x.StatusTeam, y => y.MapFrom(z => z.StatusTeam.ToString()))
            .ForMember(x => x.LastUpdated, y => y.MapFrom(z => z.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss UTC")))
            .ForMember(x=>x.CreatedUserName,y=>y.MapFrom(z=>z.CreatedUser.FullName))
            .ForMember(x=>x.ClientUserName,y=>y.MapFrom(z=>z.Client.FullName));
    }
}