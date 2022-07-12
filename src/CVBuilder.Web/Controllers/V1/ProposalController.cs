using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Proposal;
using CVBuilder.Web.Contracts.V1.Responses.Pagination;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class ProposalController : BaseAuthApiController
{
    /// <summary>
    /// Create a new Proposal
    /// </summary>
    [HttpPost(ApiRoutes.Proposal.CreateProposal)]
    public async Task<ActionResult<ProposalResult>> CreateProposal(CreateProposalRequest request)
    {
        var command = Mapper.Map<CreateProposalCommand>(request);
        command.UserId = LoggedUserId!.Value;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Approve Proposal
    /// </summary>
    [HttpPost(ApiRoutes.Proposal.ApproveProposal)]
    public async Task<ActionResult<ProposalResult>> ApproveProposal(ApproveProposalRequest request)
    {
        var command = Mapper.Map<ApproveProposalCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing Proposal
    /// </summary>
    [HttpPut(ApiRoutes.Proposal.UpdateProposal)]
    public async Task<ActionResult<ProposalResult>> UpdateProposal(UpdateProposalRequest request)
    {
        var command = Mapper.Map<UpdateProposalCommand>(request);
        command.UserId = LoggedUserId!.Value;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get resume from Proposal
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Proposal.GetProposalResume)]
    public async Task<ActionResult<ProposalResumeResult>> GetProposalResume(int proposalId, int proposalResumeId)
    {
        var command = new GetProposalResumeQuery()
        {
            UserRoles = LoggedUserRoles.ToList(),
            UserId = LoggedUserId,
            ProposalId = proposalId,
            ProposalResumeId = proposalResumeId,
        };
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get resume html from Proposal
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Proposal.GetProposalResumeHtml)]
    public async Task<ActionResult<ProposalResumeResult>> GetProposalResumeHtml(int proposalId, int proposalResumeId)
    {
        var command = new GetProposalResumeHtmlQuery
        {
            UserRoles = LoggedUserRoles.ToList(),
            UserId = LoggedUserId,
            ProposalId = proposalId,
            ProposalResumeId = proposalResumeId
        };
        var result = await Mediator.Send(command);

        return Ok(new {Html = result});
    }


    /// <summary>
    /// Get resume pdf from Proposal
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetPdfProposalResume)]
    public async Task<ActionResult<ProposalResumeResult>> GetPdfProposalResume(int proposalId, int proposalResumeId)
    {
        var command = new GetProposalResumePdfQuery
        {
            ProposalId = proposalId,
            ProposalResumeId = proposalResumeId,
            UserId = LoggedUserId,
            UserRoles = LoggedUserRoles.ToList(),
            JwtToken = $"{Request.Headers["Authorization"]}".Replace("Bearer ","")

        };
        var result = await Mediator.Send(command);
        return File(result, "application/octet-stream", "resume.pdf");
    }

    /// <summary>
    /// Get list of Proposal
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetAllProposals)]
    public async Task<ActionResult<PagedResponse<List<SmallProposalResult>>>> GetAllProposals(
        [FromQuery] GetAllProposalsRequest request)
    {
        var validFilter = new GetAllProposalsRequest(request.Page, request.PageSize, request.Term, request.Clients,
            request.Statuses)
        {
            Sort = request.Sort,
            Order = request.Order,
        };

        var command = Mapper.Map<GetAllProposalsQuery>(validFilter);

        command.UserId = LoggedUserId!.Value;
        command.UserRoles = LoggedUserRoles.ToList();

        var response = await Mediator.Send(command);

        var result = new PagedResponse<List<SmallProposalResult>>(response.Item2, validFilter.Page,
            validFilter.PageSize, response.Item1);
        return result;
    }

    /// <summary>
    /// Get list of archive Proposals
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetAllArchiveProposals)]
    public async Task<ActionResult<PagedResponse<List<SmallProposalResult>>>> GetAllArchiveProposals(
        [FromQuery] GetAllProposalsRequest request)
    {
        var validFilter = new GetAllProposalsRequest(request.Page, request.PageSize, request.Term, request.Clients,
            request.Statuses)
        {
            Sort = request.Sort,
            Order = request.Order,
        };

        var command = Mapper.Map<GetAllArchiveProposalsQuery>(validFilter);

        var response = await Mediator.Send(command);

        var result = new PagedResponse<List<SmallProposalResult>>(response.Item2, validFilter.Page,
            validFilter.PageSize, response.Item1);
        return result;
    }


    /// <summary>
    /// Get Proposal by ID
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetProposalById)]
    public async Task<ActionResult<ProposalResult>> GetProposalById([FromRoute] int id)
    {
        var command = new GetProposalByIdQuery()
        {
            Id = id
        };
        var result = await Mediator.Send(command);
        return result;
    }

    /// <summary>
    /// Get resume by ShortUrl
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Proposal.GetProposalResumeByUrl)]
    public async Task<IActionResult> GetProposalResumeByUrl(string url)
    {
        var command = new GetProposalResumeByUrlQuery()
        {
            ShortUrl = url,
            UserRoles = LoggedUserRoles.ToList(),
            UserId = LoggedUserId
        };

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get resume pdf by ShortUrl
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Proposal.GetPdfProposalResumeByUrl)]
    public async Task<IActionResult> GetPdfProposalResumeByUrl(string url)
    {
        var command = new GetProposalResumePdfByUrlQuery()
        {
            ShortUrl = url,
            UserRoles = LoggedUserRoles.ToList(),
            UserId = LoggedUserId,
            JwtToken = $"{Request.Headers["Authorization"]}".Replace("Bearer ","")
        };
        var result = await Mediator.Send(command);
        return File(result, "application/octet-stream", "resume.pdf");
    }

    /// <summary>
    /// Get resume pdf from Proposal
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetDocxProposalResume)]
    public async Task<ActionResult<ProposalResumeResult>> GetDocxProposalResume(int proposalId, int proposalResumeId)
    {
        var command = new GetProposalResumeDocxQuery
        {
            ProposalId = proposalId,
            ProposalResumeId = proposalResumeId,
            UserRoles = LoggedUserRoles.ToList(),
            UserId = LoggedUserId,
        };
        var result = await Mediator.Send(command);
        return File(result, "application/octet-stream", "resume.docx");
    }

    /// <summary>
    /// Get resume pdf by ShortUrl
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Proposal.GetDocxProposalResumeByUrl)]
    public async Task<IActionResult> GetDocxProposalResumeByUrl(string url)
    {
        var command = new GetProposalResumeDocxByUrlQuery()
        {
            ShortUrl = url,
            UserRoles = LoggedUserRoles.ToList(),
            UserId = LoggedUserId,
        };
        var result = await Mediator.Send(command);
        return File(result, "application/octet-stream", "resume.docx");
    }
}