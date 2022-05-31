using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Complexity.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using ComplexityResult = CVBuilder.Application.Complexity.Result.ComplexityResult;

namespace CVBuilder.Web.Controllers.V1;

public class ComplexityController:BaseAuthApiController
{
    /// <summary>
    /// Get list of Complexities
    /// </summary>
    [HttpGet(ApiRoutes.Complexity.GetAllComplexities)]
    public async Task<ActionResult<List<ComplexityResult>>> GetAllTeams()
    {
        var command = new GetAllComplexitiesQuery();
        var result = await Mediator.Send(command);
        return result;
    }
}