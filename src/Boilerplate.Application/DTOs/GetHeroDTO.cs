using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.DTOs
{
    public class GetHeroDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }

        public int Age { get; set; }

        public string Individuality { get; set; }
        public HeroType HeroType { get; set; }

        public GetHeroTeamDTO Team { get; set; }

    }

    public class GetHeroTeamDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TeamLeaderId { get; set; }
    }
}
