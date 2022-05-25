using System;
using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Models;

namespace CVBuilder.Application.Team.Mapper;

using Models.Entities;

public class CreateTeamMapper : AppMapperBase
{
    public CreateTeamMapper()
    {
        CreateMap<CreateTeamCommand, Team>()
            .ForMember(x=>x.CreatedAt, y=>y.MapFrom(z=>DateTime.UtcNow))
            .ForMember(x=>x.StatusTeam, y=>y.MapFrom(z=> StatusTeam.Created))
            .ForMember(x=>x.UpdatedAt, y=>y.MapFrom(z=>DateTime.UtcNow))
            .ForMember(x => x.CreatedUserId, y => y.MapFrom(z => z.UserId))
            .ForMember(x=>x.ClientId,y=>y.MapFrom(z=>z.ClientId));
        CreateMap<CreateResumeCommand, TeamResume>()
            .ForMember(x=>x.StatusResume, y=>y.MapFrom(z=>StatusTeamResume.NotSelected));

        CreateMap<Team, TeamResult>()
            .ForMember(x=>x.Client,y=>y.MapFrom(z=>new TeamClientResult
            {
                UserId = z.ClientId.GetValueOrDefault(),
                FirstName = z.Client.FirstName,
                LastName = z.Client.LastName
            }));
        CreateMap<TeamResume, ResumeResult>()
            .ForMember(x => x.ResumeId, y => y.MapFrom(z => z.ResumeId))
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.Resume.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.Resume.LastName))
            .ForMember(x => x.ResumeName, y => y.MapFrom(z => z.Resume.ResumeName));
    }
}