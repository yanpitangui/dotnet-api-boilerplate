using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<GetHeroDTO>>> GetHeroes([FromQuery] GetHeroesFilter filter)
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
        [ProducesResponseType(typeof(GetHeroDTO), 200)]
        public async Task<ActionResult<GetHeroDTO>> GetHeroById(Guid id)
        {
            var hero = await _heroAppService.GetHeroById(id);
            if (hero == null) return NotFound();
            else return Ok(hero);
        }

        /// <summary>
        /// Insert one hero in the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GetHeroDTO>> Create([FromBody] InsertHeroDTO dto)
        {
            return Ok(await _heroAppService.CreateHero(dto));

        }

        /// <summary>
        /// Update a hero from the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<GetHeroDTO>> Update([FromBody] UpdateHeroDTO dto)
        {

            return Ok(await _heroAppService.UpdateHero(dto));

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
            else return NotFound();

        }
    }
}
