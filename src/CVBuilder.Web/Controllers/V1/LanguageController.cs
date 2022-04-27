using CVBuilder.Application.Language.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Language;

namespace CVBuilder.Web.Controllers.V1
{
    public class LanguageController : BaseApiController
    {
        [HttpPost(ApiRoutes.Language.CreateLanguage)]
        public async Task<IActionResult> Create([FromBody] CreateLanguage query)
        {
            var command = Mapper.Map<CreateLanguageCommand>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.Language.LanguageGetAll)]
        public async Task<IActionResult> GetAllLanguage([FromQuery] GetAllLanguages query)
        {
            var command = Mapper.Map<GetAllLanguagesCommand>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Language.GetLanguage)]
        public async Task<IActionResult> GetLanguage([FromQuery] GetLanguagesByContentText query)
        {
            var command = Mapper.Map<GetLanguageByContainInTextQuery>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
    
}