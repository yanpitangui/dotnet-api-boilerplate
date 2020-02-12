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

        [HttpGet]
        public async Task<ActionResult<List<GetHeroDTO>>> GetHeroes([FromQuery] GetHeroesFilter filter)
        {
            return Ok(await _heroAppService.GetAllHeroes(filter));
        }


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

        [HttpPost]
        public async Task<ActionResult<GetHeroDTO>> Create([FromBody] InsertHeroDTO dto)
        {
            return Ok(await _heroAppService.CreateHero(dto));

        }

        [HttpPut]
        public async Task<ActionResult<GetHeroDTO>> Update([FromBody] UpdateHeroDTO dto)
        {

            return Ok(await _heroAppService.UpdateHero(dto));

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var deleted = await _heroAppService.DeleteHero(id);
            if (deleted) return Ok();
            else return BadRequest(new { message = $"Could not delete Hero with Id = {id}" });

        }
    }
}
