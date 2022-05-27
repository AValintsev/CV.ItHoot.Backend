﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsQuery, List<SmallTeamResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Team, int> _teamRepository;

    public GetAllTeamsHandler(IMapper mapper, IRepository<Team, int> teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<List<SmallTeamResult>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        List<Team> teams = null;
        if (request.UserRoles.Contains(Enums.RoleTypes.Admin.ToString()))
        {
            teams = await _teamRepository.Table
                .Where(x => x.StatusTeam != StatusTeam.Done)
                .Include(x => x.Resumes)
                .Include(x => x.CreatedUser)
                .Include(x => x.Client)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        else if (request.UserRoles.Contains(Enums.RoleTypes.Client.ToString()))
        {
            teams = await _teamRepository.Table
                .Where(x => x.ClientId == request.UserId)
                .Include(x => x.Resumes)
                .Include(x => x.CreatedUser)
                .Include(x => x.Client)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            teams = new List<Team>();
        }

        var smallTeams = _mapper.Map<List<SmallTeamResult>>(teams);
        return smallTeams;
    }
}