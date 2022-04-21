using System.Collections.Generic;
using System.IO;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.sharedCommands;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Application.CV.Responses;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using Microsoft.AspNetCore.Http;

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

            CreateMap<Contracts.V1.Requests.CV.CVSkill, Application.CV.Commands.CVSkill>();
            CreateMap<Contracts.V1.Requests.CV.CVLanguage, Application.CV.Commands.CVLanguage>();


            CreateMap<IFormFile, CreateFileComand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(r => r.FileName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(r => r.ContentType))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(r => GetByteArray(r.OpenReadStream())));

            CreateMap<IFormFile, List<CreateFileComand>>()
                .ConvertUsing(source => MapFile(source));

            CreateMap<GetCvByIdRequest, GetCvByIdQueries>();

            //CreateMap<UpdateEducation,EducationRoute>

            CreateMap<EducationCommand, RequestEducation>().ReverseMap();
            CreateMap<ExperienceCommand, RequestExperience>().ReverseMap();
            CreateMap<SkillCommand, RequestSkill>().ReverseMap();
            CreateMap<UserLanguageCommand, RequestUserLanguage>().ReverseMap();
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

        public static List<CreateFileComand> MapFile(IFormFile source)
        {
            if (source == null)
            {
                return null;
            }

            return new List<CreateFileComand>()
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
