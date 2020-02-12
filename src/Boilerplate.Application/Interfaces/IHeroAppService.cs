using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Application.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.Application.Interfaces
{
    public interface IHeroAppService : IDisposable
    {
        #region Hero Methods
        public Task<List<GetHeroDTO>> GetAllHeroes(GetHeroesFilter filter);

        public Task<GetHeroDTO> GetHeroById(Guid id);

        public Task<GetHeroDTO> CreateHero(InsertHeroDTO hero);

        public Task<GetHeroDTO> UpdateHero(UpdateHeroDTO hero);

        public Task<bool> DeleteHero(Guid id);

        #endregion
    }
}