using CVBuilder.Application.Team.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Team;

namespace CVBuilder.Web.Mappers;

public class TeamMapper:MapperBase
{
    public TeamMapper()
    {
        CreateMap<CreateTeamRequest, CreateTeamCommand>();
        CreateMap<CreateResumeRequest, CreateResumeCommand>();
        CreateMap<UpdateTeamRequest, UpdateTeamCommand>();
        CreateMap<UpdateResumeRequest, UpdateResumeCommand>();
        CreateMap<ApproveTeamRequest, ApproveTeamCommand>();
        CreateMap<ApproveTeamResumeRequest, ApproveTeamResumeCommand>();
    }

}