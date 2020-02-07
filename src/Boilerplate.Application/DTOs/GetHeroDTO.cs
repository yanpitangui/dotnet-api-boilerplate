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

        public List<GetHeroIndividualityDTO> Individualities { get; }

        public HeroType HeroType { get; set; }

        public GetHeroTeamDTO Team { get; set; }

    }

    public class GetHeroTeamDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TeamLeaderId { get; set; }
    }

    public class GetHeroIndividualityDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IndividualityType Type { get; set; }
    }
}
