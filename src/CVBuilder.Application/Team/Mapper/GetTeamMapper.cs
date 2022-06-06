using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Application.Team.Responses;
using ResumeResult = CVBuilder.Application.Team.Responses.ResumeResult;
using SkillResult = CVBuilder.Application.Skill.DTOs.SkillResult;

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
            .ForMember(x => x.CreatedUserName, y => y.MapFrom(z => z.CreatedUser.FullName))
            .ForMember(x => x.ClientUserName, y => y.MapFrom(z => z.Client.FullName));
            
        #region Result

        CreateMap<Team, TeamResult>()
            .ForMember(x=>x.Client,y=>y.MapFrom(z=>new TeamClientResult
            {
                UserId = z.ClientId.GetValueOrDefault(),
                FirstName = z.Client.FirstName,
                LastName = z.Client.LastName,
                ShortAuthUrl = z.Client.ShortAuthUrl
            }));
        CreateMap<TeamResume, ResumeResult>()
            .ForMember(x => x.ResumeId, y => y.MapFrom(z => z.ResumeId))
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.Resume.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.Resume.LastName))
            .ForMember(x => x.ResumeName, y => y.MapFrom(z => z.Resume.ResumeName))
            .ForMember(x => x.Skills, y => y.MapFrom(z => z.Resume.LevelSkills))
            .ForMember(x => x.Picture, y => y.MapFrom(z => z.Resume.Image.ImagePath))
            .ForMember(x=>x.PositionId,y=>y.MapFrom(z=>z.Resume.PositionId))
            .ForMember(x=>x.PositionName,y=>y.MapFrom(z=>z.Resume.Position.PositionName));

        CreateMap<LevelSkill, SkillResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Skill.Name));

        #endregion
    }
}