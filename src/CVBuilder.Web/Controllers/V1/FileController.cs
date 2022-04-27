using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Responses.File;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CVBuilder.Web.Controllers.V1
{
    public class FileController : BaseApiController
    {
        [HttpGet(ApiRoutes.File.GetFileById)]
        //[Authorize(Policy = Constants.Policy.User)]
        public async Task<IActionResult> GetFileById ([FromQuery] GetFileByIdRequest fileByIdRequest)
        {
            var command =  Mapper.Map<GetFileByIdComand>(fileByIdRequest);
            var response = await Mediator.Send(command);
            return File(response.Data,response.ContentType,response.Name);

        }

        [HttpPost(ApiRoutes.File.CreatFile)]
        public async Task<IActionResult>UploadFile([FromForm] UploadFileRequest uploadFileRequest)
        {
            var command = Mapper.Map<UploadFileComand>(uploadFileRequest);

            var file = await Mediator.Send(command);
            var response = Mapper.Map<UploadFileResponse>(file);
            return File(response.Data,response.ContentType,response.Name);
        }
    }
}
