using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Queries;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Responses.File;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
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
            var comand =  Mapper.Map<GetFileByIdComand>(fileByIdRequest);
            var response = await Mediator.Send(comand);
            return File(response.Data,response.ContentType,response.Name);

        }

        [HttpPost(ApiRoutes.File.CreatFile)]
        public async Task<IActionResult>UploadeFile([FromForm] UploadFileRequest uploadFileRequest)
        {
            var comand = Mapper.Map<UploadFileComand>(uploadFileRequest);

            var fiel = await Mediator.Send(comand);
            var respons = Mapper.Map<UploadFileRespons>(fiel);
            return File(respons.Data,respons.ContentType,respons.Name);
        }
    }
}
