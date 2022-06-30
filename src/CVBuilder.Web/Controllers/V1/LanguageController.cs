using System.Collections.Generic;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.DTOs;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Web.Contracts.V1.Requests.Language;

namespace CVBuilder.Web.Controllers.V1
{
    public class LanguageController : BaseApiController
    {
        /// <summary>
        /// Create a new Language
        /// </summary>
        [HttpPost(ApiRoutes.Language.CreateLanguage)]
        public async Task<ActionResult<LanguageDTO>> Create([FromBody] CreateLanguageRequest query)
        {
            var command = Mapper.Map<CreateLanguageCommand>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// Get list of Languages
        /// </summary>
        [HttpGet(ApiRoutes.Language.LanguageGetAll)]
        public async Task<ActionResult<LanguageResult>> GetAllLanguage()
        {
            // [FromQuery] GetAllLanguages query
            var query = new GetAllLanguagesReqiest();
            var command = Mapper.Map<GetAllLanguagesQuery>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing Language
        /// </summary>
        [HttpPut(ApiRoutes.Language.UpdateLanguage)]
        public async Task<ActionResult<LanguageDTO>> UpdateLanguage(UpdateLanguageRequest request)
        {
            var command = Mapper.Map<UpdateLanguageCommand>(request);
            var result = await Mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// Deleting an existing Language
        /// </summary>
        [HttpDelete(ApiRoutes.Language.DeleteLanguage)]
        public async Task<ActionResult> DeleteSkill([FromRoute] int id)
        {
            var command = new DeleteLanguageCommand
            {
                Id = id
            };
            var result = await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get Language by content Text
        /// </summary>
        [HttpGet(ApiRoutes.Language.GetLanguage)]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> GetLanguage(
            [FromQuery] GetLanguagesByContentText query)
        {
            var command = Mapper.Map<GetLanguageByContainInTextQuery>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}