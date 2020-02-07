using Boilerplate.Domain.Core.Entities;
using System;

namespace Boilerplate.Domain.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public Hero TeamLeader { get; set; }

        public Guid TeamLeaderId { get; set; }
    }
}
