using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Mapper
{
    using Models.Entities;
    class UpdateCvMapper : AppMapperBase
    {
        public UpdateCvMapper()
        {
            #region Request

            CreateMap<UpdateCvCommand, Cv>()
                .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills))
                .ForMember(x => x.LevelLanguages, y => y.MapFrom(z => z.UserLanguages))
                .ForMember(x => x.Educations, y => y.MapFrom(x => x.Educations))
                .ForMember(x => x.Experiences, y => y.MapFrom(x => x.Experiences));

            #endregion


            #region Result

            CreateMap<Cv, UpdateCvResult>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

            #endregion
        }
    }
}
