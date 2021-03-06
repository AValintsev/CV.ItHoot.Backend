using System;
using CVBuilder.Application.Skill.DTOs;
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

     

    }
}