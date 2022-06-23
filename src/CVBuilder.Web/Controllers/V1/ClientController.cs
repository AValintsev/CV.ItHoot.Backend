using CVBuilder.Application.Client.Queries;
using CVBuilder.Application.Client.Responses;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Client;
using CVBuilder.Web.Contracts.V1.Requests.Resume;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using CVBuilder.Web.Contracts.V1.Responses.Pagination;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CVBuilder.Web.Controllers.V1
{
    public class ClientController : BaseAuthApiController
    {
        /// <summary>
        /// Create a new Client
        /// </summary>
        //[HttpPost(ApiRoutes.Client.CreateClient)]
        //public async Task<ActionResult<ResumeResult>> CreateClient(CreateClientRequest request)
        //{
        //    var command = Mapper.Map<CreateClientCommand>(request);
        //    command.UserId = LoggedUserId!.Value;
        //    var response = await Mediator.Send(command);
        //    return Ok(response);
        //}

        /// <summary>
        /// Get list of Clients
        /// </summary>
        [HttpGet(ApiRoutes.Client.GetAllClients)]
        public async Task<ActionResult<PagedResponse<IEnumerable<ClientListItemResponse>>>> GetAllClients(
            [FromQuery] GetAllClientsRequest request)
        {
            var validFilter = new GetAllClientsRequest(request.Page, request.PageSize, request.Term)
            {
                Sort = request.Sort,
                Order = request.Order,
            };

            var command = Mapper.Map<GetAllClientsQueries>(validFilter);
            var response = await Mediator.Send(command);

            var result = new PagedResponse<List<ClientListItemResponse>>(response.Item2, validFilter.Page, validFilter.PageSize, response.Item1);
            return Ok(result);
        }

        /// <summary>
        /// Get Client by ID
        /// </summary>
        //[HttpGet(ApiRoutes.Client.GetClientById)]
        //public async Task<ActionResult<ClientResult>> GetClientById(int id)
        //{
        //    var command = new GetClientByIdQuery
        //    {
        //        Id = id,
        //        UserId = LoggedUserId,
        //        UserRoles = LoggedUserRoles
        //    };
        //    var response = await Mediator.Send(command);

        //    return Ok(response);
        //}

        /// <summary>
        /// Updates an existing Resume
        /// </summary>
        //[HttpPut(ApiRoutes.Client.UpdateClient)]
        //public async Task<ActionResult<ClientResult>> UpdateClient([FromBody] UpdateClientRequest request)
        //{
        //    var command = Mapper.Map<UpdateClientCommand>(request);
        //    command.UserId = LoggedUserId!.Value;
        //    var response = await Mediator.Send(command);

        //    return Ok(response);
        //}
    }
}