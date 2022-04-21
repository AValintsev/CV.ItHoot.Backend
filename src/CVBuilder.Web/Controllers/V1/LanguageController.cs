using CVBuilder.Application.Language.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Responses.Language;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CVBuilder.Web.Controllers.V1
{
    public class LanguageController : BaseApiController
    {

        [HttpPost(ApiRoutes.Language.CreateLanguage)]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpGet(ApiRoutes.Language.LanguageGetAll)]
        public async Task<IActionResult> GetAllLanguage()
        {
            return Ok(new LanguagesResponse()
            {
                Languages = new List<LanguageResponse>()
                {
                    new LanguageResponse()
                    {
                        Id = 1,
                        Name = "English"

                    },
                    new LanguageResponse()
                    {
                        Id = 2,
                        Name = "Spanish"
                    }
                }
            });
        }

        //[HttpGet(ApiRoutes.Language.GetLanguage)]
        //public async Task<IActionResult> GetLanguage([FromQuery] int id)
        //{
        //    return Ok(new LanguageResponse()
        //    {
        //        Id = id,
        //        Name = "English"
        //    });
        //}

        [HttpGet(ApiRoutes.Language.GetLanguage)]
        public async Task<IActionResult> GetSkill([FromQuery] GetLanguagesByContnentText query)
        {
            var command = Mapper.Map<GetLanguageByContainInTextQuery>(query);
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }

    public class GetLanguagesByContnentText
    {
        public string Content { get;set; }
    }
}
