using Boilerplate.Application.DTOs;
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
        private readonly ILogger<HeroController> _logger;
        private readonly IHeroAppService _heroAppService;

        public HeroController(ILogger<HeroController> logger, IHeroAppService heroAppService)
        {
            _logger = logger;
            _heroAppService = heroAppService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetHeroDTO>>> GetHeroes([FromQuery] GetHeroesFilter filter)
        {
            return Ok(await _heroAppService.GetAll(filter));
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(GetHeroDTO), 200)]
        public async Task<ActionResult<GetHeroDTO>> GetHeroById(Guid id)
        {
            var hero = await _heroAppService.GetById(id);
            if (hero == null) return NotFound();
            else return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GetHeroDTO dto)
        {

            await _heroAppService.Create(dto);
            return Ok(dto);

        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] GetHeroDTO dto)
        {

            await _heroAppService.Update(dto);
            return Ok(dto);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var deleted = await _heroAppService.Delete(id);
            if (deleted) return Ok();
            else return BadRequest(new { message = $"Could not delete Hero with Id = {id}" });

        }
    }
}
