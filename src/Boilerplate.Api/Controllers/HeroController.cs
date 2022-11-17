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
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetHeroResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHeroById(HeroId id)
    {
        var result = await _mediator.Send(new GetHeroByIdRequest(id));
        return result.Match<IActionResult>(
            valid => Ok(valid),
            notFound => NotFound()
        );
    }

    /// <summary>
    /// Insert one hero in the database
    /// </summary>
    /// <param name="request">The hero information</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<GetHeroResponse?>> Create([FromBody] CreateHeroRequest request)
    {
        var newHero = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetHeroById), new { id = newHero?.Id }, newHero);

    }

    /// <summary>
    /// Update a hero from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <param name="request">The update object</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(HeroId id, [FromBody] UpdateHeroRequest request)
    {
        var result = await _mediator.Send(request with { Id = id });

        return result.Match<IActionResult>(
            valid => NoContent(),
            notFound => NotFound()
        );
    }


    /// <summary>
    /// Delete a hero from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(HeroId id)
    {
        var result = await _mediator.Send(new DeleteHeroRequest(id));
        return result.Match<IActionResult>(
            valid => NoContent(),
            notFound => NotFound()
        );
    }
}