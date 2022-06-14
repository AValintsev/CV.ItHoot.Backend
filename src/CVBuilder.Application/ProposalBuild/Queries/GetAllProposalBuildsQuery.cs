using System.Collections.Generic;
using CVBuilder.Application.ProposalBuild.Result;
using MediatR;

namespace CVBuilder.Application.ProposalBuild.Queries;

public class GetAllProposalBuildsQuery : IRequest<List<ProposalBuildResult>>
{
}