﻿using CVBuilder.Application.Proposal.Responses;
using MediatR;
using ResumeResult = CVBuilder.Application.Resume.Responses.CvResponse.ResumeResult;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateSalaryRateResumeCommand: IRequest<SmallProposalResult>
{
    public int ResumeId { get; set; }
    public decimal SalaryRate { get; set; }
}