﻿using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.GetAllHeroes;

public class GetAllHeroesHandler : IRequestHandler<GetAllHeroesRequest, PaginatedList<GetHeroResponse>>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    
    public GetAllHeroesHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<PaginatedList<GetHeroResponse>> Handle(GetAllHeroesRequest request, CancellationToken cancellationToken)
    {
        var heroes = _context.Heroes
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(!string.IsNullOrEmpty(request.Nickname), x => EF.Functions.Like(x.Nickname!, $"%{request.Nickname}%"))
            .WhereIf(request.Age != null, x => x.Age == request.Age)
            .WhereIf(request.HeroType != null, x => x.HeroType == request.HeroType)
            .WhereIf(!string.IsNullOrEmpty(request.Team), x => x.Team == request.Team)
            .WhereIf(!string.IsNullOrEmpty(request.Individuality), x => EF.Functions.Like(x.Individuality!, $"%{request.Individuality}%"));
        return await _mapper.ProjectTo<GetHeroResponse>(heroes).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}