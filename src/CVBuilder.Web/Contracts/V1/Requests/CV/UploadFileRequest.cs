namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class UploadFileRequest
    {
        public int? CvId { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
}
