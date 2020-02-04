using System;
using System.Collections.Generic;
using System.Text;

namespace Boilerplate.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id;
        public DateTime CreatedDate { get; set; }
    }
}
