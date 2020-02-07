using Boilerplate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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




    }
}
