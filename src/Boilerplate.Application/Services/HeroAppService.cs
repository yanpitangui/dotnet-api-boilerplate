using AutoMapper;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<GetHeroDTO>> GetAll() {
            return await _mapper.ProjectTo<GetHeroDTO>(_heroRepository.GetAll()).ToListAsync();
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
