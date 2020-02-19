using AutoMapper;
using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Application.Extensions;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.Application.Services
{
    public class HeroAppService : IHeroAppService
    {

        private readonly IMapper _mapper;
        private readonly IHeroRepository _heroRepository;

        public HeroAppService(IMapper mapper, IHeroRepository heroRepository)
        {
            _mapper = mapper;
            _heroRepository = heroRepository;
        }

        #region Hero Methods
        public async Task<List<GetHeroDTO>> GetAllHeroes(GetHeroesFilter filter) {

            var heroes = _heroRepository
                .GetAll()
                .WhereIf(!string.IsNullOrEmpty(filter?.Name), x => x.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase))
                .WhereIf(!string.IsNullOrEmpty(filter?.Nickname), x => x.Nickname.Contains(filter.Nickname, StringComparison.InvariantCultureIgnoreCase))
                .WhereIf(filter?.Age != null, x => x.Age == filter.Age)
                .WhereIf(filter?.HeroType != null, x => x.HeroType == filter.HeroType)
                .WhereIf(!string.IsNullOrEmpty(filter?.Team), x => (x.Team == filter.Team))
                .WhereIf(!string.IsNullOrEmpty(filter?.Individuality), x => x.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase));
            return await _mapper.ProjectTo<GetHeroDTO>(heroes)
                .ToListAsync();
        }

        public async Task<GetHeroDTO> GetHeroById(Guid id)
        {
            return _mapper.Map<GetHeroDTO>(await _heroRepository.GetById(id));
        }

        public async Task<GetHeroDTO> CreateHero(InsertHeroDTO hero)
        {
            var created = _heroRepository.Create(_mapper.Map<Hero>(hero));
            await _heroRepository.SaveChangesAsync();
            return _mapper.Map<GetHeroDTO>(created);
        }

        public async Task<GetHeroDTO> UpdateHero(UpdateHeroDTO hero)
        {
            var updated = _heroRepository.Update(_mapper.Map<Hero>(hero));
            await _heroRepository.SaveChangesAsync();
            return _mapper.Map<GetHeroDTO>(updated);
        }

        public async Task<bool> DeleteHero(Guid id)
        {
           await _heroRepository.Delete(id);
           return await _heroRepository.SaveChangesAsync() > 0;
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _heroRepository.Dispose();
            }
        }

    }
}
