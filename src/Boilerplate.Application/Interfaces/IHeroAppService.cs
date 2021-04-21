using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Application.Filters;

namespace Boilerplate.Application.Interfaces
{
    public interface IHeroAppService : IDisposable
    {
        #region Hero Methods

        public Task<PaginatedList<GetHeroDto>> GetAllHeroes(GetHeroesFilter filter);

        public Task<GetHeroDto> GetHeroById(Guid id);

        public Task<GetHeroDto> CreateHero(InsertHeroDto hero);

        public Task<GetHeroDto> UpdateHero(Guid id, UpdateHeroDto updatedHero);

        public Task<bool> DeleteHero(Guid id);

        #endregion
    }
}