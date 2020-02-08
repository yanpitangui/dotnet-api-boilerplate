using Boilerplate.Application.DTOs;
using Boilerplate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<List<GetHeroDTO>>> GetHeroes()
        {
            return await _heroAppService.GetAll();
        }


        [HttpGet]
        [Route("/{id}")]
        public async Task<ActionResult<GetHeroDTO>> GetHeroById(Guid id)
        {
            return await _heroAppService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GetHeroDTO dto)
        {

            await _heroAppService.Create(dto);
            return CreatedAtAction("api/hero/", dto);

        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] GetHeroDTO dto)
        {

            await _heroAppService.Update(dto);
            return CreatedAtAction("api/hero/", dto);

        }

        [HttpDelete]
        [Route("/id")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var deleted = await _heroAppService.Delete(id);
            if (deleted) return Ok();
            else return BadRequest(new { message = $"Could not delete Hero with Id = {id}"});

        }
    }
}
