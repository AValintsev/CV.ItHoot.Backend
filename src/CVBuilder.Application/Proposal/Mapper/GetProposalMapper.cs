using System.Linq;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Application.Proposal.Responses;
using ResumeResult = CVBuilder.Application.Proposal.Responses.ResumeResult;
using SkillResult = CVBuilder.Application.Skill.DTOs.SkillResult;

namespace CVBuilder.Application.Proposal.Mapper;

using Models.Entities;

public class GetProposalMapper : AppMapperBase
{
    public GetProposalMapper()
    {
        CreateMap<Proposal, SmallProposalResult>()
            .ForMember(x => x.ProposalSize, y => y.MapFrom(z => z.Resumes.Count))
            .ForMember(x=>x.Positions,y=>y.MapFrom(z => z.Resumes
                    .Select(x => x.Resume.Position)
                    .DistinctBy(x => x.Id)
                    .Select(x => new PositionResult()
                    {
                        PositionId = x.Id,
                        PositionName = x.PositionName
                    }).ToList()
            ))
            .ForMember(x => x.StatusProposal, y => y.MapFrom(z => z.StatusProposal.ToString()))
            .ForMember(x => x.LastUpdated, y => y.MapFrom(z => z.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss UTC")))
            .ForMember(x => x.CreatedUserName, y => y.MapFrom(z => z.CreatedUser.FullName))
            .ForMember(x => x.ClientUserName, y => y.MapFrom(z => z.Client.FullName));

        #region Result

        CreateMap<Proposal, ProposalResult>();
        CreateMap<Models.User, ProposalClientResult>()
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.ShortAuthUrl, y => y.MapFrom(z => z.ShortUrl.Url));

        CreateMap<ProposalResume, ResumeResult>()
            .ForMember(x => x.ResumeId, y => y.MapFrom(z => z.ResumeId))
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.Resume.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.Resume.LastName))
            .ForMember(x => x.ResumeName, y => y.MapFrom(z => z.Resume.ResumeName))
            .ForMember(x => x.Skills, y => y.MapFrom(z => z.Resume.LevelSkills))
            .ForMember(x => x.Picture, y => y.MapFrom(z => z.Resume.Image.ImagePath))
            .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Resume.PositionId))
            .ForMember(x => x.PositionName, y => y.MapFrom(z => z.Resume.Position.PositionName))
            .ForMember(x => x.ShortUrl, y => y.MapFrom(z => z.ShortUrl.Url));

        CreateMap<LevelSkill, SkillResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Skill.Name));

        #endregion
    }
}