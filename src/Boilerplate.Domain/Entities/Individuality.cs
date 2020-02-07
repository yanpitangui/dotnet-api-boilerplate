using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Entities.Enums;

namespace Boilerplate.Domain.Entities
{
    public class Individuality : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IndividualityType Type { get; set; }
    }
}
