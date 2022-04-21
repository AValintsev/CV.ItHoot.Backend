using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Responses;
using CVBuilder.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Files.Maper
{
    class FilesMaper : AppMapperBase
    {
        public FilesMaper()
        {
            CreateMap<File, FileResult>();
         
            CreateMap<File, FileUploadResult>()
                .ForMember(u => u.ContentType, f => f.MapFrom(t => t.ContentType))
                .ForMember(u => u.CvId, f => f.MapFrom(t => t.CvId))
                .ForMember(u => u.Data, f => f.MapFrom(t => t.Data));

            CreateMap<UploadFileComand, File>()
                .ForMember(f => f.CvId, u => u.MapFrom(t => t.IdCv))
                .ForMember(f => f.Data, u => u.MapFrom(t => t.Data))
                .ForMember(f => f.ContentType, u => u.MapFrom(t => t.ContentType));
        }
    }
}
