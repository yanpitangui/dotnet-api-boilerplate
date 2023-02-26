using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Heroes;
using System.Threading.Tasks;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.DeleteHero;
using Boilerplate.Application.Features.Heroes.GetAllHeroes;
using Boilerplate.Application.Features.Heroes.GetHeroById;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class HeroController : ControllerBase
{
    private readonly IMediator _mediator;

    public HeroController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// Returns all heroes in the database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [TranslateResultToActionResult]
    public async Task<ActionResult<PaginatedList<GetHeroResponse>>> GetHeroes([FromQuery] GetAllHeroesRequest request)
    {
        return Ok(await _mediator.Send(request));
    }


    /// <summary>
    /// Get one hero by id from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [TranslateResultToActionResult]
    [ExpectedFailures(ResultStatus.NotFound)]
    public async Task<Result<GetHeroResponse>> GetHeroById(HeroId id)
    {
        var result = await _mediator.Send(new GetHeroByIdRequest(id));
        return result;
    }

    /// <summary>
    /// Insert one hero in the database
    /// </summary>
    /// <param name="request">The hero information</param>
    /// <returns></returns>
    /// 
    [HttpPost]
    [TranslateResultToActionResult]
    [ExpectedFailures(ResultStatus.Invalid)]
    public async Task<Result<GetHeroResponse>> Create([FromBody] CreateHeroRequest request)
    {
        var result = await _mediator.Send(request);
        return result;

    }

    /// <summary>
    /// Update a hero from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <param name="request">The update object</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [TranslateResultToActionResult]
    [ExpectedFailures(ResultStatus.Invalid, ResultStatus.NotFound)]
    public async Task<Result<GetHeroResponse>> Update(HeroId id, [FromBody] UpdateHeroRequest request)
    {
        var result = await _mediator.Send(request with { Id = id });
        return result;
    }


    /// <summary>
    /// Delete a hero from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [TranslateResultToActionResult]
    [ExpectedFailures(ResultStatus.Invalid, ResultStatus.NotFound)]
    public async Task<Result> Delete(HeroId id)
    {
        var result = await _mediator.Send(new DeleteHeroRequest(id));
        return result;
    }
}