using Microsoft.AspNetCore.Http;

namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class UploadFileRequest
    {
        public int? CvId { get; set; }
        public IFormFile File { get; set; }
    }
}
