using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boilerplate.Domain.Core.Entities;

namespace Boilerplate.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }

}

