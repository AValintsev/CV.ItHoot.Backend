using System.Collections.Generic;
using System.IO;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.SharedCommands;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Application.CV.Responses;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using Microsoft.AspNetCore.Http;
using CVEducationRequest = CVBuilder.Web.Contracts.V1.Requests.CV.CVEducationRequest;
using CVExperienceRequest = CVBuilder.Web.Contracts.V1.Requests.CV.CVExperienceRequest;
using CVSkillRequest = CVBuilder.Web.Contracts.V1.Requests.CV.CVSkillRequest;

namespace CVBuilder.Web.Mappers
{
    public class CVMapper : MapperBase
    {
        public CVMapper()
        {
            CreateMap<GetAllCvCardRequest, GetAllCvCardQueries>();
            CreateMap<GetAllCvCardResult, GetAllCvCardResponse>();
            CreateMap<CvCardResult, CvCardResponse>();
            CreateMap<CreateCvRequest, CreateCvCommand>();
            CreateMap<CVEducationRequest, CVEducation>();
            CreateMap<CVExperienceRequest, CVExperience>();
            CreateMap<CVSkillRequest, CVSkill>();
            CreateMap<CVLanguageRequesst, CVLanguage>();
            
            CreateMap<IFormFile, CreateFileCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(r => r.FileName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(r => r.ContentType))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(r => GetByteArray(r.OpenReadStream())));

            CreateMap<IFormFile, List<CreateFileCommand>>()
                .ConvertUsing(source => MapFile(source));

            CreateMap<GetCvByIdRequest, GetCvByIdQueries>();
            CreateMap<UpdateCvCommand, RequestCvUpdate>().ReverseMap();

        }
        private static byte[] GetByteArray(Stream stream)
        {
            using (var inputStream = stream)
            {
                if (inputStream is MemoryStream memoryStream)
                {
                    return memoryStream.ToArray();
                }

                memoryStream = new MemoryStream();
                inputStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        private static List<CreateFileCommand> MapFile(IFormFile source)
        {
            if (source == null)
            {
                return null;
            }

            return new List<CreateFileCommand>()
            {
                new ()
                {
                    Name = source.FileName,
                    ContentType = source.ContentType,
                    Data = GetByteArray(source.OpenReadStream()),
                }
            };
        }
    }
}
