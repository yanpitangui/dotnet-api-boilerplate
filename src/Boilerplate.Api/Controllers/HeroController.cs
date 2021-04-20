using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroAppService _heroAppService;

        public HeroController(IHeroAppService heroAppService)
        {
            _heroAppService = heroAppService;
        }


        /// <summary>
        /// Returns all heroes in the database
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetHeroDto>>> GetHeroes([FromQuery] GetHeroesFilter filter)
        {
            return Ok(await _heroAppService.GetAllHeroes(filter));
        }


        /// <summary>
        /// Get one hero by id from the database
        /// </summary>
        /// <param name="id">The hero's ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(GetHeroDto), 200)]
        public async Task<ActionResult<GetHeroDto>> GetHeroById(Guid id)
        {
            var hero = await _heroAppService.GetHeroById(id);
            if (hero == null) return NotFound();
            return Ok(hero);
        }

        /// <summary>
        /// Insert one hero in the database
        /// </summary>
        /// <param name="dto">The hero information</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GetHeroDto>> Create([FromBody] InsertHeroDto dto)
        {
            var newHero = await _heroAppService.CreateHero(dto);
            return CreatedAtAction(nameof(GetHeroById), new { id = newHero.Id }, newHero);

        }

        /// <summary>
        /// Update a hero from the database
        /// </summary>
        /// <param name="id">The hero's ID</param>
        /// <param name="dto">The update object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GetHeroDto>> Update(Guid id, [FromBody] UpdateHeroDto dto)
        {

            var updatedHero = await _heroAppService.UpdateHero(id, dto);

            if (updatedHero == null) return NotFound();

            return NoContent();
        }


        /// <summary>
        /// Delete a hero from the database
        /// </summary>
        /// <param name="id">The hero's ID</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var deleted = await _heroAppService.DeleteHero(id);
            if (deleted) return NoContent();
            return NotFound();

        }
    }
}
