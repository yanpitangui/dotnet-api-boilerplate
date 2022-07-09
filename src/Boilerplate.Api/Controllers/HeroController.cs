using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Heroes;
using System;
using System.Threading.Tasks;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.DeleteHero;
using Boilerplate.Application.Features.Heroes.GetAllHeroes;
using Boilerplate.Application.Features.Heroes.GetHeroById;
using Boilerplate.Application.Features.Heroes.UpdateHero;
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
    public async Task<ActionResult<GetHeroResponse>> GetHeroById(Guid id)
    {
        var hero = await _mediator.Send(new GetHeroByIdRequest(id));
        if (hero == null) return NotFound();
        return Ok(hero);
    }

    /// <summary>
    /// Insert one hero in the database
    /// </summary>
    /// <param name="request">The hero information</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<GetHeroResponse>> Create([FromBody] CreateHeroRequest request)
    {
        var newHero = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetHeroById), new { id = newHero.Id }, newHero);

    }

    /// <summary>
    /// Update a hero from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <param name="request">The update object</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<GetHeroResponse>> Update(Guid id, [FromBody] UpdateHeroRequest request)
    {

        var updatedHero = await _mediator.Send(request with { Id = id });

        if (updatedHero == null) return NotFound();

        return NoContent();
    }


    /// <summary>
    /// Delete a hero from the database
    /// </summary>
    /// <param name="id">The hero's ID</param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteHeroRequest(id));
        if (deleted) return NoContent();
        return NotFound();
    }
}