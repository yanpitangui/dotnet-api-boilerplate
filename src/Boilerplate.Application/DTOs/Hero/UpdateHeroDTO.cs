using System;

namespace Boilerplate.Application.DTOs.Hero
{
    public class UpdateHeroDTO : InsertHeroDTO
    {
        public Guid Id { get; set; }
    }
}
