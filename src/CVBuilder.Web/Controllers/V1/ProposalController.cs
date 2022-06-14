﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Proposal;
using CVBuilder.Web.Contracts.V1.Responses.Identity;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            ProposalResumeId = proposalResumeId
        };
        var result = await Mediator.Send(command);
        return Ok(result);
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
            JwtToken = $"{Request.Headers["Authorization"]}".Replace("Bearer ","")
        };
        var result = await Mediator.Send(command);
        return File(result, "application/octet-stream", "resume.pdf");
    }
    
    /// <summary>
    /// Get list of Proposal
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetAllProposals)]
    public async Task<ActionResult<List<SmallProposalResult>>> GetAllProposals()
    {
        var command = new GetAllProposalsQuery()
        {
            UserId = LoggedUserId!.Value,
            UserRoles = LoggedUserRoles.ToList()
        };
        var result = await Mediator.Send(command);
        return result;
    }
    
    /// <summary>
    /// Get list of archive Proposals
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetAllArchiveProposals)]
    public async Task<ActionResult<List<SmallProposalResult>>> GetAllArchiveProposals()
    {
        var command = new GetAllArchiveProposalsQuery();
        var result = await Mediator.Send(command);
        return result;
    }

    
    /// <summary>
    /// Get Proposal by ID
    /// </summary>
    [HttpGet(ApiRoutes.Proposal.GetProposalById)]
    public async Task<ActionResult<ProposalResult>> GetProposalById([FromRoute]int id)
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
            JwtToken = $"{Request.Headers["Authorization"]}".Replace("Bearer ","")
        };
        var result = await Mediator.Send(command);
        return File(result, "application/octet-stream", "resume.pdf");
    }
}