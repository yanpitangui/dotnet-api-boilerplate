using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Entities.Enums;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities
{
    public class Hero : Entity
    {
        public string Name { get; set; }
        public string Nickname { get; set; }

        public int Age { get; set; }

        public IList<Individuality> Individualities { get; set; }

        public HeroType HeroType { get; set; }

    }
}
