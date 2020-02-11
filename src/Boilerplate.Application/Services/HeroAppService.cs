using AutoMapper;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.Extensions;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<List<GetHeroDTO>> GetAll(GetHeroesFilter filter) {

            var heroes = _heroRepository
                .GetAll()
                .WhereIf(!string.IsNullOrEmpty(filter?.Name), x => x.Name.StartsWith(filter.Name, StringComparison.InvariantCultureIgnoreCase))
                .WhereIf(!string.IsNullOrEmpty(filter?.Nickname), x => x.Name.StartsWith(filter.Nickname, StringComparison.InvariantCultureIgnoreCase))
                .WhereIf(filter?.Age != null, x => x.Age == filter.Age)
                .WhereIf(filter?.HeroType != null, x => x.HeroType == filter.HeroType)
                .WhereIf(!string.IsNullOrEmpty(filter?.Individuality), x => x.Name.StartsWith(filter.Name, StringComparison.InvariantCultureIgnoreCase));
            return await _mapper.ProjectTo<GetHeroDTO>(heroes).ToListAsync();
        }

        public async Task<GetHeroDTO> GetById(Guid id)
        {
            return _mapper.Map<GetHeroDTO>(await _heroRepository.GetById(id));
        }

        public async Task Create(GetHeroDTO hero) //TODO: create another DTO
        {
            await _heroRepository.Create(_mapper.Map<Hero>(hero));
        }

        public async Task Update(GetHeroDTO hero)
        {
            await _heroRepository.Update(_mapper.Map<Hero>(hero));
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _heroRepository.Delete(id);
        }
        
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
