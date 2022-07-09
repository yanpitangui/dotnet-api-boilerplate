using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.Features.Hero;
using Boilerplate.Application.Features.Hero.GetAllHeroes;
using Boilerplate.Application.Features.Hero.UpdateHero;
using Boilerplate.Application.Filters;

namespace Boilerplate.Application.Interfaces;

public interface IHeroService : IDisposable
{
    #region Hero Methods
    public Task<GetHeroResponse> UpdateHero(Guid id, UpdateHeroRequest updatedHero);

    #endregion
}