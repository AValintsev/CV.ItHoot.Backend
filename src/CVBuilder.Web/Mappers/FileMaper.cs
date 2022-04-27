using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Queries;
using CVBuilder.Application.Files.Responses;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Responses.File;
using System.IO;

namespace CVBuilder.Web.Mappers
{
    public class FileMaper : MapperBase
    {
        public FileMaper()
        {
            CreateMap<GetFileByIdRequest, GetFileByIdComand>();

            CreateMap<UploadFileRequest, UploadFileComand>()
                .ForMember(opt => opt.ContentType, aft => aft.MapFrom(o => o.File.ContentType))
                .ForMember(opt => opt.Name, aft => aft.MapFrom(o => o.File.Name))
                .ForMember(opt => opt.Data, aft => aft.MapFrom(o => GetByteArray(o.File.OpenReadStream())))
                .ForMember(opt => opt.IdCv, aft => aft.MapFrom(o => o.CvId));

            CreateMap<FileUploadResult, UploadFileResponse>();
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
    }
}
