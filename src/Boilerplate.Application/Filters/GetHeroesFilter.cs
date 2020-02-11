using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boilerplate.Application.Filters
{
    public class GetHeroesFilter
    {
        public string Name { get; set; }
        public string Nickname { get; set; }

        public int? Age { get; set; }

        public string Individuality { get; set; }
        public HeroType? HeroType { get; set; }

    }
}
